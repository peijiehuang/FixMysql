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
            button4.Enabled = false;
            SetLog("1、修改配置文件处理中");
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
                    //MessageBox.Show("运行程序目录下 myFix.ini 文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

             
                SetLog($"{sourceFilePath}，my.ini 文件已成功替换");
                //MessageBox.Show($"{sourceFilePath}，my.ini 文件已成功替换", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 重启 MySQL 服务
                ManageMysqlService(ServiceActionEnum.Restart, textBox1.Text);



            }
            catch (Exception ex)
            {
                SetLog($"抛出异常: {ex.Message}");
                MessageBox.Show($"抛出异常: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            button4.Enabled = true;

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
            button5.Enabled = false;

            SetLog("2、备份数据库处理中");


            // 数据库连接字符串
            string connectionString = textBox6.Text;

            var result = ExtractUidAndPwd(connectionString);

            // 需要备份的数据库
            string databaseName = textBox4.Text;

            // 备份文件路径(文件名为当前时间年月日时分秒.sql，默认目录=C:\MysqlBak)
            string backupDir = string.IsNullOrWhiteSpace(textBox5.Text) ? @"C:\MysqlBak" : textBox5.Text;
            if (!Directory.Exists(backupDir))
            {
                Directory.CreateDirectory(backupDir);
            }
            string backupFileName = $"{DateTime.Now:yyyyMMddHHmmss}.sql";
            string backupFilePath = Path.Combine(backupDir, backupFileName);

            // 配置文件路径
            string sourceFilePath = textBox2.Text;
            string newFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my.ini");

            try
            {
                // Step 1: 备份数据库
                using (var process = new Process())
                {
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
            button5.Enabled = true;

        }


        /// <summary>
        /// 清空指定数据库中指定表的数据
        /// </summary>
        /// <param name="mysqlExePath">mysql.exe 的绝对路径</param>
        /// <param name="uid">数据库用户名</param>
        /// <param name="pwd">数据库密码</param>
        /// <param name="databaseName">数据库名</param>
        /// <param name="tableNames">要清空的表名集合</param>
        /// <returns>是否全部清空成功</returns>
        private bool ClearTables(string uid, string pwd, string databaseName, IEnumerable<string> tableNames)
        {
            string mysqlExePath = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe";
            if (!File.Exists(mysqlExePath))
            {
                SetLog($"未找到 mysql.exe，请检查 MySQL 安装路径: {mysqlExePath}");
                MessageBox.Show($"未找到 mysql.exe，请检查 MySQL 安装路径: {mysqlExePath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (var table in tableNames)
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = mysqlExePath;
                    process.StartInfo.Arguments = $"-u {uid} -p{pwd} -e \"USE {databaseName}; DELETE FROM {table};\"";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.WaitForExit();
                    if (process.ExitCode == 0)
                    {
                        SetLog($"清空异常数据成功 {table}");
                    }
                    else
                    {
                        string error = process.StandardError.ReadToEnd();
                        SetLog($"清空异常数据失败: {table}，错误信息: {error}");
                        MessageBox.Show($"清空异常数据失败: {table}\r\n{error}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;

            SetLog("3、恢复数据库处理中");


            // 弹出对话框，在 textBox5.Text 路径中选择备份文件
            string initialDir = string.IsNullOrWhiteSpace(textBox5.Text) ? @"C:\" : textBox5.Text;
            string backupFilePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = initialDir;
                openFileDialog.Filter = "SQL 文件 (*.sql)|*.sql|所有文件 (*.*)|*.*";
                openFileDialog.Title = "选择要恢复的数据库备份文件";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    backupFilePath = openFileDialog.FileName;
                }
                else
                {
                    SetLog("未选择备份文件，操作已取消");
                    return;
                }
            }

            string connectionString = textBox6.Text;
            var result = ExtractUidAndPwd(connectionString);
            string oldDatabaseName = textBox4.Text;

            if (!File.Exists(backupFilePath))
            {
                SetLog($"备份文件不存在: {backupFilePath}，不允许恢复数据");
                MessageBox.Show("备份文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 删除并创建数据库
                using (var process = new Process())
                {
                    string mysqlExePath = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe";
                    if (!File.Exists(mysqlExePath))
                    {
                        SetLog($"未找到 mysql.exe，请检查 MySQL 安装路径: {mysqlExePath}");
                        MessageBox.Show($"未找到 mysql.exe，请检查 MySQL 安装路径: {mysqlExePath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    process.StartInfo.FileName = mysqlExePath;
                    process.StartInfo.Arguments = $"-u {result.Uid} -p{result.Pwd} -e \"DROP DATABASE IF EXISTS `{oldDatabaseName}`; CREATE DATABASE `{oldDatabaseName}`;\"";
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
                    string mysqlExePath = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe";
                    if (!File.Exists(mysqlExePath))
                    {
                        SetLog($"未找到 mysql.exe，请检查 MySQL 安装路径: {mysqlExePath}");
                        MessageBox.Show($"未找到 mysql.exe，请检查 MySQL 安装路径: {mysqlExePath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // 使用 cmd.exe 执行带有重定向的命令
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = $"/c \"\"{mysqlExePath}\" -u{result.Uid} -p{result.Pwd} {oldDatabaseName} < \"{backupFilePath}\"\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        SetLog("数据库恢复成功");
                        MessageBox.Show("数据库恢复成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
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
            button6.Enabled = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            SetLog("5、清空异常数据处理中");


            // 数据库连接字符串
            string connectionString = textBox6.Text;

            var result = ExtractUidAndPwd(connectionString);

            // 需要备份的数据库
            string databaseName = textBox4.Text;


            ClearTables(result.Uid, result.Pwd, databaseName, ["BusinessStatisticsHistoricaldata"]);

            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            SetLog("4、恢复配置文件处理中");


            //恢复配置文件
            string newFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my.ini");

            // 配置文件路径
            string sourceFilePath = textBox2.Text;

            File.Copy(newFilePath, sourceFilePath, true);
            SetLog($"恢复配置文件 my.ini 文件成功: {sourceFilePath}");

            // 重启 MySQL 服务
            ManageMysqlService(ServiceActionEnum.Restart, textBox1.Text);

            button2.Enabled = true;

        }
    }
}
