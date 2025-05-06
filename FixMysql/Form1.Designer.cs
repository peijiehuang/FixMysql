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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label4 = new Label();
            label5 = new Label();
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
            // button1
            // 
            button1.Location = new Point(44, 148);
            button1.Name = "button1";
            button1.Size = new Size(105, 47);
            button1.TabIndex = 2;
            button1.Text = "启动";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(191, 148);
            button2.Name = "button2";
            button2.Size = new Size(105, 47);
            button2.TabIndex = 2;
            button2.Text = "停止";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(326, 148);
            button3.Name = "button3";
            button3.Size = new Size(105, 47);
            button3.TabIndex = 2;
            button3.Text = "重启";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(44, 395);
            button4.Name = "button4";
            button4.Size = new Size(167, 47);
            button4.TabIndex = 2;
            button4.Text = "修改配置文件";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(264, 395);
            button5.Name = "button5";
            button5.Size = new Size(167, 47);
            button5.TabIndex = 2;
            button5.Text = "备份数据库";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(514, 395);
            button6.Name = "button6";
            button6.Size = new Size(167, 47);
            button6.TabIndex = 2;
            button6.Text = "恢复数据库";
            button6.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(191, 263);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(490, 30);
            textBox2.TabIndex = 1;
            textBox2.Text = "C:\\ProgramData\\MySQL\\MySQL Server 8.0\\my.ini";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(190, 316);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(897, 524);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(textBox3);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button1);
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
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label4;
        private Label label5;
    }
}
