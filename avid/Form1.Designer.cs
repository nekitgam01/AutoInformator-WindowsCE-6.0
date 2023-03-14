namespace avid
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.cbRecord = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbRoute = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbPath = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.cbRecord);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 142);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.Location = new System.Drawing.Point(0, 94);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(638, 48);
            this.button4.TabIndex = 8;
            this.button4.Text = "Приветствие";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // cbRecord
            // 
            this.cbRecord.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbRecord.Location = new System.Drawing.Point(0, 71);
            this.cbRecord.Name = "cbRecord";
            this.cbRecord.Size = new System.Drawing.Size(638, 23);
            this.cbRecord.TabIndex = 5;
            this.cbRecord.SelectedIndexChanged += new System.EventHandler(this.cbRecord_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(638, 20);
            this.label2.Text = "Текущая запись";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(638, 51);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cbRoute);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(337, 51);
            // 
            // cbRoute
            // 
            this.cbRoute.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbRoute.Items.Add("Вперед");
            this.cbRoute.Items.Add("Обратно");
            this.cbRoute.Location = new System.Drawing.Point(0, 20);
            this.cbRoute.Name = "cbRoute";
            this.cbRoute.Size = new System.Drawing.Size(337, 23);
            this.cbRoute.TabIndex = 6;
            this.cbRoute.SelectedIndexChanged += new System.EventHandler(this.cbRoute_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(337, 20);
            this.label3.Text = "Маршрут";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbPath);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(337, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 51);
            // 
            // cbPath
            // 
            this.cbPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbPath.Items.Add("Вперед");
            this.cbPath.Items.Add("Обратно");
            this.cbPath.Location = new System.Drawing.Point(0, 20);
            this.cbPath.Name = "cbPath";
            this.cbPath.Size = new System.Drawing.Size(301, 23);
            this.cbPath.TabIndex = 6;
            this.cbPath.SelectedIndexChanged += new System.EventHandler(this.cbPath_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 20);
            this.label1.Text = "Направление";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 142);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(638, 313);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(290, 313);
            this.button2.TabIndex = 6;
            this.button2.Text = "Повторить";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Right;
            this.button3.Location = new System.Drawing.Point(290, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(348, 313);
            this.button3.TabIndex = 7;
            this.button3.Text = ">>";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(638, 313);
            this.label4.Text = "Ожидайте, воспроизводится";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Простой голосовой процессор";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbRecord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cbRoute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;

    }
}

