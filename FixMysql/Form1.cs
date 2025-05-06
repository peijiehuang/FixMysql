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
                    await Task.Delay(100); // ÿ100����ˢ��һ��
                }
            });
        }

        /// <summary>
        /// ��ȡWindows����״̬
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
                    ServiceControllerStatus.Running => "������",
                    ServiceControllerStatus.Stopped => "��ֹͣ",
                    ServiceControllerStatus.Paused => "����ͣ",
                    _ => "δ֪״̬"
                };
                return status;
            }
            catch (Exception ex)
            {
                return $"�׳��쳣: {ex.Message}";
            }
        }

        /// <summary>
        /// ������������ֹͣmysql����ķ���
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
                            MessageBox.Show("MySQL����������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("MySQL��������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case ServiceAction.Stop:
                        if (service.Status == ServiceControllerStatus.Running)
                        {
                            service.Stop();
                            service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                            MessageBox.Show("MySQL������ֹͣ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("MySQL������ֹͣ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("MySQL����������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    default:
                        MessageBox.Show("��Ч�Ĳ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"����ʧ��: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
