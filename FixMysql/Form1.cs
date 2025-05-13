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
                            SetLog("MySQL服务已启动");
                            MessageBox.Show("MySQL服务已启动", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            SetLog("MySQL服务已在运行中");
                            MessageBox.Show("MySQL服务已在运行中", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case ServiceActionEnum.Stop:
                        if (service.Status == ServiceControllerStatus.Running)
                        {
                            service.Stop();
                            service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                            SetLog("MySQL服务已停止");
                            MessageBox.Show("MySQL服务已停止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            SetLog("MySQL服务已停止");
                            MessageBox.Show("MySQL服务已停止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        SetLog("MySQL服务已重启");
                        MessageBox.Show("MySQL服务已重启", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    default:
                        SetLog("无效的操作");
                        MessageBox.Show("无效的操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                SetLog($"抛出异常: {ex.Message}");
                MessageBox.Show($"抛出异常: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btn_Start_Click(object sender, EventArgs e)
        {
            btn_Start.Enabled = false;
            SetLog("点击启动按钮");
            ManageMysqlService(ServiceActionEnum.Start, textBox1.Text);
            btn_Start.Enabled = true;
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            btn_Stop.Enabled = false;
            SetLog("点击停止按钮");
            ManageMysqlService(ServiceActionEnum.Stop, textBox1.Text);
            btn_Stop.Enabled = true;
        }

        private void btn_Restart_Click(object sender, EventArgs e)
        {
            btn_Restart.Enabled = false;
            SetLog("点击重启按钮");
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
                // 如果目标目录不存在，则创建目录
                if (!Directory.Exists(backupDirPath))
                {
                    Directory.CreateDirectory(backupDirPath);
                    SetLog($"目标目录不存在，则创建目录{backupDirPath}");
                }

                // 如果目标目录存在备份文件，则不备份
                if (!File.Exists(backupFilePath))
                {
                    File.Copy(sourceFilePath, backupFilePath);

                }
                else
                {
                    SetLog($"目标目录{backupFilePath}存在备份文件，则不备份");
                }

                // 删除原始 my.ini 文件
                if (File.Exists(sourceFilePath))
                {
                    File.Delete(sourceFilePath);
                    SetLog($"删除原始 my.ini 文件,{sourceFilePath}");
                }

                // 从运行程序的当前目录复制 myFix.ini 到目标路径并重命名为 my.ini
                if (File.Exists(newFilePath))
                {
                    File.Copy(newFilePath, sourceFilePath);
                    SetLog($"从运行程序的当前目录复制 myFix.ini 到目标路径并重命名为 my.ini,{sourceFilePath}");
                }
                else
                {
                    SetLog($"{newFilePath}，运行程序目录下 myFix.ini 文件不存在");
                    MessageBox.Show("运行程序目录下 myFix.ini 文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 重启 MySQL 服务
                ManageMysqlService(ServiceActionEnum.Restart, textBox1.Text);

                SetLog($"{sourceFilePath}，my.ini 文件已成功替换");
                MessageBox.Show($"{sourceFilePath}，my.ini 文件已成功替换", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);



            }
            catch (Exception ex)
            {
                SetLog($"抛出异常: {ex.Message}");
                MessageBox.Show($"抛出异常: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 设置日志输出
        /// </summary>
        /// <param name="log"></param>
        private void SetLog(string log)
        {
            if (txt_Log.InvokeRequired)
            {
                // 如果当前线程不是 UI 线程，使用 Invoke 切换到 UI 线程
                txt_Log.Invoke(() => txt_Log.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}：{log}{Environment.NewLine}"));
            }
            else
            {
                // 如果当前线程是 UI 线程，直接操作
                txt_Log.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}：{log}{Environment.NewLine}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 数据库连接字符串
            string connectionString = "Server=127.0.0.1;Database=CloudBatteryTestSystem;Port=3306;charset=utf8;uid=root;pwd=hyn@123;SslMode=none;AllowPublicKeyRetrieval=true;Pooling=true";

            var result = ExtractUidAndPwd(connectionString);

            // 需要备份的数据库
            string databaseName = "CloudBatteryTestSystem";

            // 备份文件路径
            string backupFilePath = @"C:\CloudBatteryTestSystem.sql";

            // 配置文件路径
            string sourceFilePath = @"C:\ProgramData\MySQL\MySQL Server 8.0\my.ini";
            string newFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my.ini");

            try
            {
                // Step 1: 备份数据库
                using (var process = new Process())
                {
                    if (File.Exists(backupFilePath))
                    {
                        File.Delete(backupFilePath);
                        SetLog($"已删除存在的备份文件: {backupFilePath}");
                    }

                  
                    // 修改 mysqldump 路径为绝对路径，假设 MySQL 安装在默认目录
                    string mysqlBinPath = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqldump.exe";
                    if (!File.Exists(mysqlBinPath))
                    {
                        SetLog($"未找到 mysqldump.exe，请检查 MySQL 安装路径: {mysqlBinPath}");
                        MessageBox.Show($"未找到 mysqldump.exe，请检查 MySQL 安装路径: {mysqlBinPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        SetLog($"数据库备份成功: {backupFilePath}");
                        MessageBox.Show("数据库备份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string error = process.StandardError.ReadToEnd();
                        SetLog($"数据库备份失败: {error}");
                        MessageBox.Show($"数据库备份失败: {error}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Step 2: 替换 my.ini 文件
                if (File.Exists(sourceFilePath))
                {
                    File.Delete(sourceFilePath);
                    SetLog($"删除原始 my.ini 文件: {sourceFilePath}");
                }

                if (File.Exists(newFilePath))
                {
                    File.Copy(newFilePath, sourceFilePath);
                    SetLog($"替换 my.ini 文件成功: {sourceFilePath}");
                }
                else
                {
                    SetLog($"运行程序目录下 my.ini 文件不存在: {newFilePath}");
                    MessageBox.Show("运行程序目录下 my.ini 文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Step 3: 重启 MySQL 服务
                ManageMysqlService(ServiceActionEnum.Restart, textBox1.Text);
            }
            catch (Exception ex)
            {
                SetLog($"操作失败: {ex.Message}");
                MessageBox.Show($"操作失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                SetLog($"备份文件不存在: {backupFilePath}，不允许恢复数据");
                MessageBox.Show("备份文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Step 1: 重命名数据库
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
                        SetLog($"数据库重命名成功: {oldDatabaseName} -> {newDatabaseName}");
                    }
                    else
                    {
                        string error = process.StandardError.ReadToEnd();
                        SetLog($"数据库重命名失败: {error}");
                        MessageBox.Show($"数据库重命名失败: {error}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                // Step 2: 删除并恢复数据库
                using (var process = new Process())
                {
                    // 删除目标数据库
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
                        SetLog($"数据库删除并重新创建成功: {oldDatabaseName}");
                    }
                    else
                    {
                        string error = process.StandardError.ReadToEnd();
                        SetLog($"数据库删除或创建失败: {error}");
                        MessageBox.Show($"数据库删除或创建失败: {error}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // 恢复数据库
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
                        SetLog("数据库恢复成功");
                        MessageBox.Show("数据库恢复成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string error = process.StandardError.ReadToEnd();
                        SetLog($"数据库恢复失败: {error}");
                        MessageBox.Show($"数据库恢复失败: {error}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                SetLog($"操作失败: {ex.Message}");
                MessageBox.Show($"操作失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 提取连接字符串中的用户名和密码
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
