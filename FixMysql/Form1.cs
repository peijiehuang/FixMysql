using System.ServiceProcess;

namespace FixMysql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    var status = GetServiceStatus(textBox1.Text);
                    Invoke(() => lb_MysqlStatus.Text = $" {status}");
                    await Task.Delay(100); // 每100毫秒刷新一次
                }
            });
        }

        /// <summary>
        /// 获取Windows服务状态
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private string GetServiceStatus(string serviceName)
        {
            try
            {
                ServiceController service = new ServiceController(serviceName);
                string status = service.Status switch
                {
                    ServiceControllerStatus.Running => "运行中",
                    ServiceControllerStatus.Stopped => "已停止",
                    ServiceControllerStatus.Paused => "已暂停",
                    _ => "未知状态"
                };
                return status;
            }
            catch (Exception ex)
            {
                return $"抛出异常: {ex.Message}";
            }
        }

        /// <summary>
        /// 启动、重启和停止mysql服务的方法
        /// </summary>
        /// <param name="action"></param>
        private void ManageMysqlService(ServiceAction action, string serviceName)
        {
            try
            {
                ServiceController service = new ServiceController(serviceName);

                switch (action)
                {
                    case ServiceAction.Start:
                        if (service.Status == ServiceControllerStatus.Stopped)
                        {
                            service.Start();
                            service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                            MessageBox.Show("MySQL服务已启动", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("MySQL服务已在运行中", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case ServiceAction.Stop:
                        if (service.Status == ServiceControllerStatus.Running)
                        {
                            service.Stop();
                            service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                            MessageBox.Show("MySQL服务已停止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("MySQL服务已停止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case ServiceAction.Restart:
                        if (service.Status == ServiceControllerStatus.Running)
                        {
                            service.Stop();
                            service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                        }
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                        MessageBox.Show("MySQL服务已重启", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    default:
                        MessageBox.Show("无效的操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private enum ServiceAction
        {
            Start,
            Stop,
            Restart
        }     

        private void button1_Click(object sender, EventArgs e)
        {
            ManageMysqlService(ServiceAction.Start, textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManageMysqlService(ServiceAction.Stop, textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ManageMysqlService(ServiceAction.Restart, textBox1.Text);
        }
    }
}
