namespace FixMysql
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lb_MysqlStatus = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            btn_Start = new Button();
            btn_Stop = new Button();
            btn_Restart = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label4 = new Label();
            label5 = new Label();
            textBox4 = new TextBox();
            label2 = new Label();
            textBox5 = new TextBox();
            label6 = new Label();
            txt_Log = new TextBox();
            textBox6 = new TextBox();
            label7 = new Label();
            button1 = new Button();
            button2 = new Button();
            label8 = new Label();
            checkBox1 = new CheckBox();
            btn_ReInstall = new Button();
            label9 = new Label();
            textBox7 = new TextBox();
            label10 = new Label();
            txt_DamagedTables = new TextBox();
            button3 = new Button();
            button7 = new Button();
            button8 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 80);
            label1.Name = "label1";
            label1.Size = new Size(162, 24);
            label1.TabIndex = 0;
            label1.Text = "MySQL服务状态：";
            // 
            // lb_MysqlStatus
            // 
            lb_MysqlStatus.AutoSize = true;
            lb_MysqlStatus.Location = new Point(249, 80);
            lb_MysqlStatus.Name = "lb_MysqlStatus";
            lb_MysqlStatus.Size = new Size(46, 24);
            lb_MysqlStatus.TabIndex = 0;
            lb_MysqlStatus.Text = "未知";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(191, 21);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(198, 30);
            textBox1.TabIndex = 1;
            textBox1.Text = "MySql80";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(52, 21);
            label3.Name = "label3";
            label3.Size = new Size(100, 24);
            label3.TabIndex = 0;
            label3.Text = "服务名称：";
            // 
            // btn_Start
            // 
            btn_Start.Location = new Point(44, 148);
            btn_Start.Name = "btn_Start";
            btn_Start.Size = new Size(105, 47);
            btn_Start.TabIndex = 2;
            btn_Start.Text = "启动";
            btn_Start.UseVisualStyleBackColor = true;
            btn_Start.Click += btn_Start_Click;
            // 
            // btn_Stop
            // 
            btn_Stop.Location = new Point(191, 148);
            btn_Stop.Name = "btn_Stop";
            btn_Stop.Size = new Size(105, 47);
            btn_Stop.TabIndex = 2;
            btn_Stop.Text = "停止";
            btn_Stop.UseVisualStyleBackColor = true;
            btn_Stop.Click += btn_Stop_Click;
            // 
            // btn_Restart
            // 
            btn_Restart.Location = new Point(326, 148);
            btn_Restart.Name = "btn_Restart";
            btn_Restart.Size = new Size(105, 47);
            btn_Restart.TabIndex = 2;
            btn_Restart.Text = "重启";
            btn_Restart.UseVisualStyleBackColor = true;
            btn_Restart.Click += btn_Restart_Click;
            // 
            // button4
            // 
            button4.Location = new Point(18, 582);
            button4.Name = "button4";
            button4.Size = new Size(239, 59);
            button4.TabIndex = 2;
            button4.Text = "1、修改配置文件（只读）";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(283, 582);
            button5.Name = "button5";
            button5.Size = new Size(239, 59);
            button5.TabIndex = 2;
            button5.Text = "2、备份数据库";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(18, 663);
            button6.Name = "button6";
            button6.Size = new Size(239, 59);
            button6.TabIndex = 2;
            button6.Text = "4、恢复数据库";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(283, 263);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(490, 30);
            textBox2.TabIndex = 1;
            textBox2.Text = "C:\\ProgramData\\MySQL\\MySQL Server 8.0\\my.ini";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(282, 316);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(490, 30);
            textBox3.TabIndex = 3;
            textBox3.Text = "C:\\ProgramData\\MySQL\\MySQL Server 8.0\\bak";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(44, 269);
            label4.Name = "label4";
            label4.Size = new Size(136, 24);
            label4.TabIndex = 4;
            label4.Text = "配置文件路径：";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(44, 320);
            label5.Name = "label5";
            label5.Size = new Size(136, 24);
            label5.TabIndex = 5;
            label5.Text = "备份配置路径：";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(282, 376);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(490, 30);
            textBox4.TabIndex = 3;
            textBox4.Text = "CloudBatteryTestSystem";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 380);
            label2.Name = "label2";
            label2.Size = new Size(144, 24);
            label2.TabIndex = 5;
            label2.Text = "MySQL数据库：";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(282, 476);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(490, 30);
            textBox5.TabIndex = 3;
            textBox5.Text = "C:\\MysqlBak";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(44, 480);
            label6.Name = "label6";
            label6.Size = new Size(216, 24);
            label6.TabIndex = 5;
            label6.Text = "MySQL数据库备份路径：";
            // 
            // txt_Log
            // 
            txt_Log.Location = new Point(840, 39);
            txt_Log.Multiline = true;
            txt_Log.Name = "txt_Log";
            txt_Log.ScrollBars = ScrollBars.Both;
            txt_Log.Size = new Size(1174, 562);
            txt_Log.TabIndex = 6;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(283, 424);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(490, 30);
            textBox6.TabIndex = 3;
            textBox6.Text = "Server=127.0.0.1;Database=CloudBatteryTestSystem;Port=3306;charset=utf8;uid=root;pwd=hyn@123;SslMode=none;AllowPublicKeyRetrieval=true;Pooling=true";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(45, 428);
            label7.Name = "label7";
            label7.Size = new Size(172, 24);
            label7.TabIndex = 5;
            label7.Text = "数据库连接字符串：";
            // 
            // button1
            // 
            button1.Location = new Point(559, 663);
            button1.Name = "button1";
            button1.Size = new Size(239, 59);
            button1.TabIndex = 2;
            button1.Text = "6、清空异常数据";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(282, 663);
            button2.Name = "button2";
            button2.Size = new Size(239, 59);
            button2.TabIndex = 2;
            button2.Text = "5、恢复配置文件";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(65, 757);
            label8.Name = "label8";
            label8.Size = new Size(637, 144);
            label8.TabIndex = 7;
            label8.Text = "正常处理操作：1》2》3 》4 选中备份出来的数据\r\n\r\n如果点击2无法备份数据库，那么步骤2和4需要使用SQLyog 小海豚手动处理，\r\n先使用SQLyog 小海豚备份然\r\n后点步骤3一键重装MySQL，然后再用SQLyog 小海豚恢复数据\r\n\r\n";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(303, 524);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(252, 28);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "备份数据库后恢复配置文件";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // btn_ReInstall
            // 
            btn_ReInstall.Location = new Point(557, 582);
            btn_ReInstall.Name = "btn_ReInstall";
            btn_ReInstall.Size = new Size(239, 59);
            btn_ReInstall.TabIndex = 2;
            btn_ReInstall.Text = "3、一键重装MySQL";
            btn_ReInstall.UseVisualStyleBackColor = true;
            btn_ReInstall.Click += btn_ReInstall_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(43, 220);
            label9.Name = "label9";
            label9.Size = new Size(162, 24);
            label9.TabIndex = 10;
            label9.Text = "MySQL程序路径：";
            // 
            // textBox7
            // 
            textBox7.Location = new Point(282, 214);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(490, 30);
            textBox7.TabIndex = 9;
            textBox7.Text = "C:\\ProgramData\\MySQL\\MySQL Server 8.0";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(840, 651);
            label10.Name = "label10";
            label10.Size = new Size(523, 24);
            label10.TabIndex = 11;
            label10.Text = "过滤表，只需要导出表结构（正常不需要改动里面 格式：,隔开）";
            // 
            // txt_DamagedTables
            // 
            txt_DamagedTables.Location = new Point(846, 697);
            txt_DamagedTables.Multiline = true;
            txt_DamagedTables.Name = "txt_DamagedTables";
            txt_DamagedTables.Size = new Size(1150, 155);
            txt_DamagedTables.TabIndex = 12;
            txt_DamagedTables.Text = "BusinessStatisticsHistoricaldata";
            // 
            // button3
            // 
            button3.Location = new Point(18, 582);
            button3.Name = "button3";
            button3.Size = new Size(239, 59);
            button3.TabIndex = 2;
            button3.Text = "1、修改配置文件（只读）";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button4_Click;
            // 
            // button7
            // 
            button7.Location = new Point(557, 582);
            button7.Name = "button7";
            button7.Size = new Size(239, 59);
            button7.TabIndex = 2;
            button7.Text = "3、一键重装MySQL";
            button7.UseVisualStyleBackColor = true;
            button7.Click += btn_ReInstall_Click;
            // 
            // button8
            // 
            button8.Location = new Point(283, 582);
            button8.Name = "button8";
            button8.Size = new Size(239, 59);
            button8.TabIndex = 2;
            button8.Text = "2、备份数据库";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2043, 929);
            Controls.Add(txt_DamagedTables);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(textBox7);
            Controls.Add(checkBox1);
            Controls.Add(label8);
            Controls.Add(txt_Log);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(textBox5);
            Controls.Add(textBox6);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(btn_Restart);
            Controls.Add(btn_Stop);
            Controls.Add(button6);
            Controls.Add(button8);
            Controls.Add(button5);
            Controls.Add(button7);
            Controls.Add(btn_ReInstall);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(btn_Start);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(lb_MysqlStatus);
            Controls.Add(label1);
            Name = "Form1";
            Text = "FixMysql";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lb_MysqlStatus;
        private TextBox textBox1;
        private Label label3;
        private Button btn_Start;
        private Button btn_Stop;
        private Button btn_Restart;
        private Button button4;
        private Button button5;
        private Button button6;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label4;
        private Label label5;
        private TextBox textBox4;
        private Label label2;
        private TextBox textBox5;
        private Label label6;
        private TextBox txt_Log;
        private TextBox textBox6;
        private Label label7;
        private Button button1;
        private Button button2;
        private Label label8;
        private CheckBox checkBox1;
        private Button btn_ReInstall;
        private Label label9;
        private TextBox textBox7;
        private Label label10;
        private TextBox txt_DamagedTables;
        private Button button3;
        private Button button7;
        private Button button8;
    }
}
