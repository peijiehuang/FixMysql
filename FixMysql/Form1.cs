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
        private void ManageMysqlService(ServiceActionEnum action, string serviceName)
        {
            try
            {
                ServiceController service = new ServiceController(serviceName);

                switch (action)
                {
                    case ServiceActionEnum.Start:
                        if (service.Status == ServiceControllerStatus.Stopped)
                        {
                            service.Start();
                            service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                            SetLog("MySQL����������");
                            MessageBox.Show("MySQL����������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            SetLog("MySQL��������������");
                            MessageBox.Show("MySQL��������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case ServiceActionEnum.Stop:
                        if (service.Status == ServiceControllerStatus.Running)
                        {
                            service.Stop();
                            service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                            SetLog("MySQL������ֹͣ");
                            MessageBox.Show("MySQL������ֹͣ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            SetLog("MySQL������ֹͣ");
                            MessageBox.Show("MySQL������ֹͣ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case ServiceActionEnum.Restart:
                        if (service.Status == ServiceControllerStatus.Running)
                        {
                            service.Stop();
                            service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                        }
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                        SetLog("MySQL����������");
                        MessageBox.Show("MySQL����������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    default:
                        SetLog("��Ч�Ĳ���");
                        MessageBox.Show("��Ч�Ĳ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                SetLog($"�׳��쳣: {ex.Message}");
                MessageBox.Show($"�׳��쳣: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btn_Start_Click(object sender, EventArgs e)
        {
            btn_Start.Enabled = false;
            SetLog("���������ť");
            ManageMysqlService(ServiceActionEnum.Start, textBox1.Text);
            btn_Start.Enabled = true;
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            btn_Stop.Enabled = false;
            SetLog("���ֹͣ��ť");
            ManageMysqlService(ServiceActionEnum.Stop, textBox1.Text);
            btn_Stop.Enabled = true;
        }

        private void btn_Restart_Click(object sender, EventArgs e)
        {
            btn_Restart.Enabled = false;
            SetLog("���������ť");
            ManageMysqlService(ServiceActionEnum.Restart, textBox1.Text);
            btn_Restart.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sourceFilePath = textBox2.Text;// @"C:\ProgramData\MySQL\MySQL Server 8.0\my.ini";
            string backupDirPath = textBox3.Text;//@"C:\ProgramData\MySQL\MySQL Server 8.0\bak";
            string backupFilePath = Path.Combine(backupDirPath, DateTime.Now.ToString("yyyy-MM-dd"));
            string newFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "myFix.ini");

            try
            {
                // ���Ŀ��Ŀ¼�����ڣ��򴴽�Ŀ¼
                if (!Directory.Exists(backupDirPath))
                {
                    Directory.CreateDirectory(backupDirPath);
                    SetLog($"Ŀ��Ŀ¼�����ڣ��򴴽�Ŀ¼{backupDirPath}");
                }

                // ���Ŀ��Ŀ¼���ڱ����ļ����򲻱���
                if (!File.Exists(backupFilePath))
                {
                    File.Copy(sourceFilePath, backupFilePath);
                    SetLog($"Ŀ��Ŀ¼{backupFilePath}���ڱ����ļ����򲻱���");
                }

                // ɾ��ԭʼ my.ini �ļ�
                if (File.Exists(sourceFilePath))
                {
                    File.Delete(sourceFilePath);
                    SetLog($"ɾ��ԭʼ my.ini �ļ�,{sourceFilePath}");
                }

                // �����г���ĵ�ǰĿ¼���� myFix.ini ��Ŀ��·����������Ϊ my.ini
                if (File.Exists(newFilePath))
                {
                    File.Copy(newFilePath, sourceFilePath);
                    SetLog($"�����г���ĵ�ǰĿ¼���� myFix.ini ��Ŀ��·����������Ϊ my.ini,{sourceFilePath}");
                }
                else
                {
                    SetLog($"{newFilePath}�����г���Ŀ¼�� myFix.ini �ļ�������");
                    MessageBox.Show("���г���Ŀ¼�� myFix.ini �ļ�������", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SetLog($"{sourceFilePath}��my.ini �ļ��ѳɹ��滻");
                MessageBox.Show("my.ini �ļ��ѳɹ��滻", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SetLog($"�׳��쳣: {ex.Message}");
                MessageBox.Show($"�׳��쳣: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ������־���
        /// </summary>
        /// <param name="log"></param>
        private void SetLog(string log)
        {
            if (txt_Log.InvokeRequired)
            {
                // �����ǰ�̲߳��� UI �̣߳�ʹ�� Invoke �л��� UI �߳�
                txt_Log.Invoke(() => txt_Log.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}��{log}{Environment.NewLine}"));
            }
            else
            {
                // �����ǰ�߳��� UI �̣߳�ֱ�Ӳ���
                txt_Log.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}��{log}{Environment.NewLine}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
