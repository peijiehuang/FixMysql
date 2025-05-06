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

    }
}
