namespace JobDocs
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtJobNo = new System.Windows.Forms.TextBox();
            this.txtJobName = new System.Windows.Forms.TextBox();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxDataSummary = new System.Windows.Forms.CheckBox();
            this.checkBoxProductionReport = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Title",
            "First Name",
            "Last Name",
            "Company Name",
            "Address Line 1",
            "Address Line 2",
            "Address Line 3",
            "Address Line 4",
            "Address Line 5",
            "Locality",
            "State",
            "Postcode",
            "Country"});
            this.comboBox1.Location = new System.Drawing.Point(129, 128);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(252, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Job Number";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(639, 339);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 56);
            this.button1.TabIndex = 2;
            this.button1.Text = "Create PDF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtJobNo
            // 
            this.txtJobNo.Location = new System.Drawing.Point(129, 46);
            this.txtJobNo.Name = "txtJobNo";
            this.txtJobNo.Size = new System.Drawing.Size(100, 20);
            this.txtJobNo.TabIndex = 9;
            // 
            // txtJobName
            // 
            this.txtJobName.Location = new System.Drawing.Point(129, 87);
            this.txtJobName.Name = "txtJobName";
            this.txtJobName.Size = new System.Drawing.Size(252, 20);
            this.txtJobName.TabIndex = 10;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(422, 132);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(100, 20);
            this.txtCustomer.TabIndex = 11;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(110, 172);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Job Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Customer";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(639, 401);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 56);
            this.button2.TabIndex = 15;
            this.button2.Text = "Print";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AllowDrop = true;
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(27, 246);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 263);
            this.flowLayoutPanel1.TabIndex = 17;
            this.flowLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragDrop);
            this.flowLayoutPanel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragEnter);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(233, 246);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(200, 263);
            this.flowLayoutPanel2.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Lodge Date";
            // 
            // checkBoxDataSummary
            // 
            this.checkBoxDataSummary.AutoSize = true;
            this.checkBoxDataSummary.Checked = true;
            this.checkBoxDataSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDataSummary.Location = new System.Drawing.Point(639, 246);
            this.checkBoxDataSummary.Name = "checkBoxDataSummary";
            this.checkBoxDataSummary.Size = new System.Drawing.Size(95, 17);
            this.checkBoxDataSummary.TabIndex = 20;
            this.checkBoxDataSummary.Text = "Data Summary";
            this.checkBoxDataSummary.UseVisualStyleBackColor = true;
            // 
            // checkBoxProductionReport
            // 
            this.checkBoxProductionReport.AutoSize = true;
            this.checkBoxProductionReport.Checked = true;
            this.checkBoxProductionReport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProductionReport.Location = new System.Drawing.Point(639, 281);
            this.checkBoxProductionReport.Name = "checkBoxProductionReport";
            this.checkBoxProductionReport.Size = new System.Drawing.Size(112, 17);
            this.checkBoxProductionReport.TabIndex = 21;
            this.checkBoxProductionReport.Text = "Production Report";
            this.checkBoxProductionReport.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Source Fileds";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "DT Fileds";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 524);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxProductionReport);
            this.Controls.Add(this.checkBoxDataSummary);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.txtJobName);
            this.Controls.Add(this.txtJobNo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtJobNo;
        private System.Windows.Forms.TextBox txtJobName;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxDataSummary;
        private System.Windows.Forms.CheckBox checkBoxProductionReport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

