using System.Diagnostics;
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
            string backupDirPath = Path.Combine(textBox3.Text, DateTime.Now.ToString("yyyy-MM-dd"));//@"C:\ProgramData\MySQL\MySQL Server 8.0\bak";
            string backupFilePath = Path.Combine(backupDirPath, "my.ini");
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

                }
                else
                {
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

                // ���� MySQL ����
                ManageMysqlService(ServiceActionEnum.Restart, textBox1.Text);

                SetLog($"{sourceFilePath}��my.ini �ļ��ѳɹ��滻");
                MessageBox.Show($"{sourceFilePath}��my.ini �ļ��ѳɹ��滻", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);



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
            // ���ݿ������ַ���
            string connectionString = "Server=127.0.0.1;Database=CloudBatteryTestSystem;Port=3306;charset=utf8;uid=root;pwd=hyn@123;SslMode=none;AllowPublicKeyRetrieval=true;Pooling=true";

            var result = ExtractUidAndPwd(connectionString);

            // ��Ҫ���ݵ����ݿ�
            string databaseName = "CloudBatteryTestSystem";

            // �����ļ�·��
            string backupFilePath = @"C:\CloudBatteryTestSystem.sql";

            // �����ļ�·��
            string sourceFilePath = @"C:\ProgramData\MySQL\MySQL Server 8.0\my.ini";
            string newFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my.ini");

            try
            {
                // Step 1: �������ݿ�
                using (var process = new Process())
                {
                    if (File.Exists(backupFilePath))
                    {
                        File.Delete(backupFilePath);
                        SetLog($"��ɾ�����ڵı����ļ�: {backupFilePath}");
                    }

                  
                    // �޸� mysqldump ·��Ϊ����·�������� MySQL ��װ��Ĭ��Ŀ¼
                    string mysqlBinPath = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqldump.exe";
                    if (!File.Exists(mysqlBinPath))
                    {
                        SetLog($"δ�ҵ� mysqldump.exe������ MySQL ��װ·��: {mysqlBinPath}");
                        MessageBox.Show($"δ�ҵ� mysqldump.exe������ MySQL ��װ·��: {mysqlBinPath}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    process.StartInfo.FileName = mysqlBinPath;
                    process.StartInfo.Arguments = $"-u {result.Uid} -p{result.Pwd} --databases {databaseName} -r \"{backupFilePath}\"";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        SetLog($"���ݿⱸ�ݳɹ�: {backupFilePath}");
                        MessageBox.Show("���ݿⱸ�ݳɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string error = process.StandardError.ReadToEnd();
                        SetLog($"���ݿⱸ��ʧ��: {error}");
                        MessageBox.Show($"���ݿⱸ��ʧ��: {error}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Step 2: �滻 my.ini �ļ�
                if (File.Exists(sourceFilePath))
                {
                    File.Delete(sourceFilePath);
                    SetLog($"ɾ��ԭʼ my.ini �ļ�: {sourceFilePath}");
                }

                if (File.Exists(newFilePath))
                {
                    File.Copy(newFilePath, sourceFilePath);
                    SetLog($"�滻 my.ini �ļ��ɹ�: {sourceFilePath}");
                }
                else
                {
                    SetLog($"���г���Ŀ¼�� my.ini �ļ�������: {newFilePath}");
                    MessageBox.Show("���г���Ŀ¼�� my.ini �ļ�������", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Step 3: ���� MySQL ����
                ManageMysqlService(ServiceActionEnum.Restart, textBox1.Text);
            }
            catch (Exception ex)
            {
                SetLog($"����ʧ��: {ex.Message}");
                MessageBox.Show($"����ʧ��: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string backupFilePath = @"C:\CloudBatteryTestSystem.sql";
            string connectionString = "Server=127.0.0.1;Database=CloudBatteryTestSystem;Port=3306;charset=utf8;uid=root;pwd=hyn@123;SslMode=none;AllowPublicKeyRetrieval=true;Pooling=true";
            var result = ExtractUidAndPwd(connectionString);

            string oldDatabaseName = "CloudBatteryTestSystem";
            string newDatabaseName = $"{oldDatabaseName}_Backup_{DateTime.Now:yyyyMMddHHmmss}";

            if (!File.Exists(backupFilePath))
            {
                SetLog($"�����ļ�������: {backupFilePath}��������ָ�����");
                MessageBox.Show("�����ļ�������", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Step 1: ���������ݿ�
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "mysql";
                    process.StartInfo.Arguments = $"-u {result.Uid} -p{result.Pwd} -e \"CREATE DATABASE {newDatabaseName}; USE {newDatabaseName}; SHOW TABLES FROM {oldDatabaseName};\"";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        SetLog($"���ݿ��������ɹ�: {oldDatabaseName} -> {newDatabaseName}");
                    }
                    else
                    {
                        string error = process.StandardError.ReadToEnd();
                        SetLog($"���ݿ�������ʧ��: {error}");
                        MessageBox.Show($"���ݿ�������ʧ��: {error}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                // Step 2: ɾ�����ָ����ݿ�
                using (var process = new Process())
                {
                    // ɾ��Ŀ�����ݿ�
                    process.StartInfo.FileName = "mysql";
                    process.StartInfo.Arguments = $"-u {result.Uid} -p{result.Pwd} -e \"DROP DATABASE IF EXISTS {oldDatabaseName}; CREATE DATABASE {oldDatabaseName};\"";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        SetLog($"���ݿ�ɾ�������´����ɹ�: {oldDatabaseName}");
                    }
                    else
                    {
                        string error = process.StandardError.ReadToEnd();
                        SetLog($"���ݿ�ɾ���򴴽�ʧ��: {error}");
                        MessageBox.Show($"���ݿ�ɾ���򴴽�ʧ��: {error}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // �ָ����ݿ�
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "mysql";
                    process.StartInfo.Arguments = $"-u {result.Uid} -p{result.Pwd} {oldDatabaseName} < \"{backupFilePath}\"";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        SetLog("���ݿ�ָ��ɹ�");
                        MessageBox.Show("���ݿ�ָ��ɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string error = process.StandardError.ReadToEnd();
                        SetLog($"���ݿ�ָ�ʧ��: {error}");
                        MessageBox.Show($"���ݿ�ָ�ʧ��: {error}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                SetLog($"����ʧ��: {ex.Message}");
                MessageBox.Show($"����ʧ��: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ��ȡ�����ַ����е��û���������
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private (string Uid, string Pwd) ExtractUidAndPwd(string connectionString)
        {
            var parameters = connectionString.Split(';');
            string uid = parameters.FirstOrDefault(p => p.StartsWith("uid=", StringComparison.OrdinalIgnoreCase))?.Split('=')[1] ?? string.Empty;
            string pwd = parameters.FirstOrDefault(p => p.StartsWith("pwd=", StringComparison.OrdinalIgnoreCase))?.Split('=')[1] ?? string.Empty;
            return (uid, pwd);
        }
    }
}
