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
            button4.Location = new Point(210, 554);
            button4.Name = "button4";
            button4.Size = new Size(167, 47);
            button4.TabIndex = 2;
            button4.Text = "修改配置文件";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(405, 554);
            button5.Name = "button5";
            button5.Size = new Size(167, 47);
            button5.TabIndex = 2;
            button5.Text = "备份数据库";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Enabled = false;
            button6.Location = new Point(591, 554);
            button6.Name = "button6";
            button6.Size = new Size(167, 47);
            button6.TabIndex = 2;
            button6.Text = "恢复数据库";
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
            button1.Location = new Point(12, 554);
            button1.Name = "button1";
            button1.Size = new Size(167, 47);
            button1.TabIndex = 2;
            button1.Text = "清空异常数据";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2057, 663);
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
            Controls.Add(button5);
            Controls.Add(button1);
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
    }
}
