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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtJobNo = new System.Windows.Forms.TextBox();
            this.txtJobName = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxDataSummary = new System.Windows.Forms.CheckBox();
            this.checkBoxProductionReport = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxCustomer = new System.Windows.Forms.ComboBox();
            this.btnSecondStream = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextJobDirectory = new System.Windows.Forms.RichTextBox();
            this.btnAddStream = new System.Windows.Forms.Button();
            this.numericUpDownStreamQty = new System.Windows.Forms.NumericUpDown();
            this.listBoxStreams = new System.Windows.Forms.ListBox();
            this.cmbStream = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBoxStock = new System.Windows.Forms.GroupBox();
            this.txtStockDescription = new System.Windows.Forms.TextBox();
            this.rbMSOLStock = new System.Windows.Forms.RadioButton();
            this.rbCustomerStock = new System.Windows.Forms.RadioButton();
            this.lblPrintDescription = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownUp = new System.Windows.Forms.NumericUpDown();
            this.groupBoxPlex = new System.Windows.Forms.GroupBox();
            this.rbDuplex = new System.Windows.Forms.RadioButton();
            this.rbSimplex = new System.Windows.Forms.RadioButton();
            this.groupBoxPaper = new System.Windows.Forms.GroupBox();
            this.txtCustomPaperSize = new System.Windows.Forms.TextBox();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.rbA3 = new System.Windows.Forms.RadioButton();
            this.rbSRA3 = new System.Windows.Forms.RadioButton();
            this.rbA4 = new System.Windows.Forms.RadioButton();
            this.cmbPrintJobs = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnChangeJobDirectory = new System.Windows.Forms.Button();
            this.groupBoxColour = new System.Windows.Forms.GroupBox();
            this.radioButtonBlack = new System.Windows.Forms.RadioButton();
            this.rbColour = new System.Windows.Forms.RadioButton();
            this.checkBoxInkJet = new System.Windows.Forms.CheckBox();
            this.checkBoxDuplo = new System.Windows.Forms.CheckBox();
            this.checkBox7100 = new System.Windows.Forms.CheckBox();
            this.checkBox8120 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbFileName = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtJobDirectory = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.btnImportFromDolphin = new System.Windows.Forms.Button();
            this.btnSampleSheet = new System.Windows.Forms.Button();
            this.richTexNotes = new System.Windows.Forms.RichTextBox();
            this.groupBoxBranch = new System.Windows.Forms.GroupBox();
            this.rbArtwork = new System.Windows.Forms.RadioButton();
            this.rbDatabase = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStreamQty)).BeginInit();
            this.groupBoxStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUp)).BeginInit();
            this.groupBoxPlex.SuspendLayout();
            this.groupBoxPaper.SuspendLayout();
            this.groupBoxColour.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxBranch.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Job Number";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(515, 469);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 56);
            this.button1.TabIndex = 7;
            this.button1.Text = "Create PDF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtJobNo
            // 
            this.txtJobNo.Location = new System.Drawing.Point(93, 16);
            this.txtJobNo.Name = "txtJobNo";
            this.txtJobNo.Size = new System.Drawing.Size(100, 20);
            this.txtJobNo.TabIndex = 0;
            // 
            // txtJobName
            // 
            this.txtJobName.Location = new System.Drawing.Point(350, 70);
            this.txtJobName.Name = "txtJobName";
            this.txtJobName.Size = new System.Drawing.Size(252, 20);
            this.txtJobName.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(109, 175);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Job Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Customer";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(515, 531);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 56);
            this.button2.TabIndex = 15;
            this.button2.Text = "Print";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 175);
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
            this.checkBoxDataSummary.Location = new System.Drawing.Point(515, 386);
            this.checkBoxDataSummary.Name = "checkBoxDataSummary";
            this.checkBoxDataSummary.Size = new System.Drawing.Size(95, 17);
            this.checkBoxDataSummary.TabIndex = 5;
            this.checkBoxDataSummary.Text = "Data Summary";
            this.checkBoxDataSummary.UseVisualStyleBackColor = true;
            this.checkBoxDataSummary.CheckedChanged += new System.EventHandler(this.checkBoxDataSummary_CheckedChanged);
            // 
            // checkBoxProductionReport
            // 
            this.checkBoxProductionReport.AutoSize = true;
            this.checkBoxProductionReport.Checked = true;
            this.checkBoxProductionReport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProductionReport.Location = new System.Drawing.Point(515, 409);
            this.checkBoxProductionReport.Name = "checkBoxProductionReport";
            this.checkBoxProductionReport.Size = new System.Drawing.Size(112, 17);
            this.checkBoxProductionReport.TabIndex = 6;
            this.checkBoxProductionReport.Text = "Production Report";
            this.checkBoxProductionReport.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(257, 230);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(200, 368);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(254, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "DT Fileds";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AllowDrop = true;
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.flowLayoutPanel1.AutoScrollMinSize = new System.Drawing.Size(10, 10);
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(18, 230);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 368);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragDrop);
            this.flowLayoutPanel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragEnter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Source Fileds";
            // 
            // comboBoxCustomer
            // 
            this.comboBoxCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCustomer.FormattingEnabled = true;
            this.comboBoxCustomer.Items.AddRange(new object[] {
            " ",
            "SCOTT PRINT",
            "HYDRAULIC SUPERMARKET",
            "ADVANCE PRESS",
            "QUALITY PRESS"});
            this.comboBoxCustomer.Location = new System.Drawing.Point(938, 14);
            this.comboBoxCustomer.Name = "comboBoxCustomer";
            this.comboBoxCustomer.Size = new System.Drawing.Size(85, 21);
            this.comboBoxCustomer.TabIndex = 1;
            this.comboBoxCustomer.Visible = false;
            this.comboBoxCustomer.SelectedIndexChanged += new System.EventHandler(this.comboBoxCustomer_SelectedIndexChanged);
            // 
            // btnSecondStream
            // 
            this.btnSecondStream.Enabled = false;
            this.btnSecondStream.Location = new System.Drawing.Point(335, 192);
            this.btnSecondStream.Name = "btnSecondStream";
            this.btnSecondStream.Size = new System.Drawing.Size(122, 32);
            this.btnSecondStream.TabIndex = 24;
            this.btnSecondStream.Text = "Add Second Stream";
            this.btnSecondStream.UseVisualStyleBackColor = true;
            this.btnSecondStream.Click += new System.EventHandler(this.btnSecondStream_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(858, 605);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.btnSecondStream);
            this.tabPage1.Controls.Add(this.checkBoxProductionReport);
            this.tabPage1.Controls.Add(this.checkBoxDataSummary);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.flowLayoutPanel2);
            this.tabPage1.Controls.Add(this.dateTimePicker1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(850, 579);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data Summary";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBoxBranch);
            this.tabPage2.Controls.Add(this.richTexNotes);
            this.tabPage2.Controls.Add(this.richTextJobDirectory);
            this.tabPage2.Controls.Add(this.btnAddStream);
            this.tabPage2.Controls.Add(this.numericUpDownStreamQty);
            this.tabPage2.Controls.Add(this.listBoxStreams);
            this.tabPage2.Controls.Add(this.cmbStream);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.groupBoxStock);
            this.tabPage2.Controls.Add(this.lblPrintDescription);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.numericUpDownUp);
            this.tabPage2.Controls.Add(this.groupBoxPlex);
            this.tabPage2.Controls.Add(this.groupBoxPaper);
            this.tabPage2.Controls.Add(this.cmbPrintJobs);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.btnChangeJobDirectory);
            this.tabPage2.Controls.Add(this.groupBoxColour);
            this.tabPage2.Controls.Add(this.checkBoxInkJet);
            this.tabPage2.Controls.Add(this.checkBoxDuplo);
            this.tabPage2.Controls.Add(this.checkBox7100);
            this.tabPage2.Controls.Add(this.checkBox8120);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.cmbFileName);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtJobDirectory);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(850, 579);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Print Spec Sheet";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextJobDirectory
            // 
            this.richTextJobDirectory.Location = new System.Drawing.Point(89, 19);
            this.richTextJobDirectory.Name = "richTextJobDirectory";
            this.richTextJobDirectory.Size = new System.Drawing.Size(299, 53);
            this.richTextJobDirectory.TabIndex = 42;
            this.richTextJobDirectory.Text = "";
            // 
            // btnAddStream
            // 
            this.btnAddStream.Location = new System.Drawing.Point(740, 189);
            this.btnAddStream.Name = "btnAddStream";
            this.btnAddStream.Size = new System.Drawing.Size(75, 23);
            this.btnAddStream.TabIndex = 41;
            this.btnAddStream.Text = "Add";
            this.btnAddStream.UseVisualStyleBackColor = true;
            this.btnAddStream.Click += new System.EventHandler(this.btnAddStream_Click);
            // 
            // numericUpDownStreamQty
            // 
            this.numericUpDownStreamQty.Location = new System.Drawing.Point(667, 192);
            this.numericUpDownStreamQty.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.numericUpDownStreamQty.Name = "numericUpDownStreamQty";
            this.numericUpDownStreamQty.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownStreamQty.TabIndex = 40;
            // 
            // listBoxStreams
            // 
            this.listBoxStreams.FormattingEnabled = true;
            this.listBoxStreams.Location = new System.Drawing.Point(581, 261);
            this.listBoxStreams.Name = "listBoxStreams";
            this.listBoxStreams.Size = new System.Drawing.Size(234, 95);
            this.listBoxStreams.TabIndex = 39;
            // 
            // cmbStream
            // 
            this.cmbStream.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbStream.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbStream.FormattingEnabled = true;
            this.cmbStream.Location = new System.Drawing.Point(571, 192);
            this.cmbStream.Name = "cmbStream";
            this.cmbStream.Size = new System.Drawing.Size(61, 21);
            this.cmbStream.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(565, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Add Streams";
            // 
            // groupBoxStock
            // 
            this.groupBoxStock.Controls.Add(this.txtStockDescription);
            this.groupBoxStock.Controls.Add(this.rbMSOLStock);
            this.groupBoxStock.Controls.Add(this.rbCustomerStock);
            this.groupBoxStock.Location = new System.Drawing.Point(22, 471);
            this.groupBoxStock.Name = "groupBoxStock";
            this.groupBoxStock.Size = new System.Drawing.Size(424, 71);
            this.groupBoxStock.TabIndex = 36;
            this.groupBoxStock.TabStop = false;
            this.groupBoxStock.Text = "Stock Description";
            // 
            // txtStockDescription
            // 
            this.txtStockDescription.Location = new System.Drawing.Point(157, 45);
            this.txtStockDescription.Name = "txtStockDescription";
            this.txtStockDescription.Size = new System.Drawing.Size(261, 20);
            this.txtStockDescription.TabIndex = 33;
            // 
            // rbMSOLStock
            // 
            this.rbMSOLStock.AutoSize = true;
            this.rbMSOLStock.Location = new System.Drawing.Point(162, 19);
            this.rbMSOLStock.Name = "rbMSOLStock";
            this.rbMSOLStock.Size = new System.Drawing.Size(55, 17);
            this.rbMSOLStock.TabIndex = 23;
            this.rbMSOLStock.TabStop = true;
            this.rbMSOLStock.Text = "MSOL";
            this.rbMSOLStock.UseVisualStyleBackColor = true;
            // 
            // rbCustomerStock
            // 
            this.rbCustomerStock.AutoSize = true;
            this.rbCustomerStock.Location = new System.Drawing.Point(263, 19);
            this.rbCustomerStock.Name = "rbCustomerStock";
            this.rbCustomerStock.Size = new System.Drawing.Size(69, 17);
            this.rbCustomerStock.TabIndex = 22;
            this.rbCustomerStock.TabStop = true;
            this.rbCustomerStock.Text = "Customer";
            this.rbCustomerStock.UseVisualStyleBackColor = true;
            // 
            // lblPrintDescription
            // 
            this.lblPrintDescription.AutoSize = true;
            this.lblPrintDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrintDescription.Location = new System.Drawing.Point(354, 149);
            this.lblPrintDescription.Name = "lblPrintDescription";
            this.lblPrintDescription.Size = new System.Drawing.Size(0, 17);
            this.lblPrintDescription.TabIndex = 35;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(366, 394);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "UP";
            // 
            // numericUpDownUp
            // 
            this.numericUpDownUp.Location = new System.Drawing.Point(314, 390);
            this.numericUpDownUp.Name = "numericUpDownUp";
            this.numericUpDownUp.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownUp.TabIndex = 33;
            // 
            // groupBoxPlex
            // 
            this.groupBoxPlex.Controls.Add(this.rbDuplex);
            this.groupBoxPlex.Controls.Add(this.rbSimplex);
            this.groupBoxPlex.Location = new System.Drawing.Point(22, 375);
            this.groupBoxPlex.Name = "groupBoxPlex";
            this.groupBoxPlex.Size = new System.Drawing.Size(224, 71);
            this.groupBoxPlex.TabIndex = 32;
            this.groupBoxPlex.TabStop = false;
            this.groupBoxPlex.Text = "Sides";
            // 
            // rbDuplex
            // 
            this.rbDuplex.AutoSize = true;
            this.rbDuplex.Location = new System.Drawing.Point(97, 15);
            this.rbDuplex.Name = "rbDuplex";
            this.rbDuplex.Size = new System.Drawing.Size(58, 17);
            this.rbDuplex.TabIndex = 23;
            this.rbDuplex.TabStop = true;
            this.rbDuplex.Text = "Duplex";
            this.rbDuplex.UseVisualStyleBackColor = true;
            // 
            // rbSimplex
            // 
            this.rbSimplex.AutoSize = true;
            this.rbSimplex.Location = new System.Drawing.Point(15, 15);
            this.rbSimplex.Name = "rbSimplex";
            this.rbSimplex.Size = new System.Drawing.Size(61, 17);
            this.rbSimplex.TabIndex = 22;
            this.rbSimplex.TabStop = true;
            this.rbSimplex.Text = "Simplex";
            this.rbSimplex.UseVisualStyleBackColor = true;
            // 
            // groupBoxPaper
            // 
            this.groupBoxPaper.Controls.Add(this.txtCustomPaperSize);
            this.groupBoxPaper.Controls.Add(this.rbCustom);
            this.groupBoxPaper.Controls.Add(this.rbA3);
            this.groupBoxPaper.Controls.Add(this.rbSRA3);
            this.groupBoxPaper.Controls.Add(this.rbA4);
            this.groupBoxPaper.Location = new System.Drawing.Point(22, 285);
            this.groupBoxPaper.Name = "groupBoxPaper";
            this.groupBoxPaper.Size = new System.Drawing.Size(451, 71);
            this.groupBoxPaper.TabIndex = 31;
            this.groupBoxPaper.TabStop = false;
            this.groupBoxPaper.Text = "Paper Size";
            // 
            // txtCustomPaperSize
            // 
            this.txtCustomPaperSize.Location = new System.Drawing.Point(255, 38);
            this.txtCustomPaperSize.Name = "txtCustomPaperSize";
            this.txtCustomPaperSize.Size = new System.Drawing.Size(169, 20);
            this.txtCustomPaperSize.TabIndex = 32;
            // 
            // rbCustom
            // 
            this.rbCustom.AutoSize = true;
            this.rbCustom.Location = new System.Drawing.Point(255, 15);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(83, 17);
            this.rbCustom.TabIndex = 25;
            this.rbCustom.TabStop = true;
            this.rbCustom.Text = "Custom Size";
            this.rbCustom.UseVisualStyleBackColor = true;
            // 
            // rbA3
            // 
            this.rbA3.AutoSize = true;
            this.rbA3.Location = new System.Drawing.Point(192, 15);
            this.rbA3.Name = "rbA3";
            this.rbA3.Size = new System.Drawing.Size(38, 17);
            this.rbA3.TabIndex = 24;
            this.rbA3.TabStop = true;
            this.rbA3.Text = "A3";
            this.rbA3.UseVisualStyleBackColor = true;
            // 
            // rbSRA3
            // 
            this.rbSRA3.AutoSize = true;
            this.rbSRA3.Location = new System.Drawing.Point(97, 15);
            this.rbSRA3.Name = "rbSRA3";
            this.rbSRA3.Size = new System.Drawing.Size(53, 17);
            this.rbSRA3.TabIndex = 23;
            this.rbSRA3.TabStop = true;
            this.rbSRA3.Text = "SRA3";
            this.rbSRA3.UseVisualStyleBackColor = true;
            // 
            // rbA4
            // 
            this.rbA4.AutoSize = true;
            this.rbA4.Location = new System.Drawing.Point(15, 15);
            this.rbA4.Name = "rbA4";
            this.rbA4.Size = new System.Drawing.Size(38, 17);
            this.rbA4.TabIndex = 22;
            this.rbA4.TabStop = true;
            this.rbA4.Text = "A4";
            this.rbA4.UseVisualStyleBackColor = true;
            // 
            // cmbPrintJobs
            // 
            this.cmbPrintJobs.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPrintJobs.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPrintJobs.FormattingEnabled = true;
            this.cmbPrintJobs.Location = new System.Drawing.Point(89, 146);
            this.cmbPrintJobs.Name = "cmbPrintJobs";
            this.cmbPrintJobs.Size = new System.Drawing.Size(303, 21);
            this.cmbPrintJobs.TabIndex = 29;
            this.cmbPrintJobs.SelectedIndexChanged += new System.EventHandler(this.cmbPrintJobs_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Print Jobs";
            // 
            // btnChangeJobDirectory
            // 
            this.btnChangeJobDirectory.Location = new System.Drawing.Point(398, 49);
            this.btnChangeJobDirectory.Name = "btnChangeJobDirectory";
            this.btnChangeJobDirectory.Size = new System.Drawing.Size(75, 23);
            this.btnChangeJobDirectory.TabIndex = 24;
            this.btnChangeJobDirectory.Text = "Change";
            this.btnChangeJobDirectory.UseVisualStyleBackColor = true;
            this.btnChangeJobDirectory.Click += new System.EventHandler(this.btnChangeJobDirectory_Click);
            // 
            // groupBoxColour
            // 
            this.groupBoxColour.Controls.Add(this.radioButtonBlack);
            this.groupBoxColour.Controls.Add(this.rbColour);
            this.groupBoxColour.Enabled = false;
            this.groupBoxColour.Location = new System.Drawing.Point(245, 199);
            this.groupBoxColour.Name = "groupBoxColour";
            this.groupBoxColour.Size = new System.Drawing.Size(80, 71);
            this.groupBoxColour.TabIndex = 23;
            this.groupBoxColour.TabStop = false;
            // 
            // radioButtonBlack
            // 
            this.radioButtonBlack.AutoSize = true;
            this.radioButtonBlack.Location = new System.Drawing.Point(15, 41);
            this.radioButtonBlack.Name = "radioButtonBlack";
            this.radioButtonBlack.Size = new System.Drawing.Size(52, 17);
            this.radioButtonBlack.TabIndex = 23;
            this.radioButtonBlack.Text = "Black";
            this.radioButtonBlack.UseVisualStyleBackColor = true;
            // 
            // rbColour
            // 
            this.rbColour.AutoSize = true;
            this.rbColour.Checked = true;
            this.rbColour.Location = new System.Drawing.Point(15, 15);
            this.rbColour.Name = "rbColour";
            this.rbColour.Size = new System.Drawing.Size(55, 17);
            this.rbColour.TabIndex = 22;
            this.rbColour.TabStop = true;
            this.rbColour.Text = "Colour";
            this.rbColour.UseVisualStyleBackColor = true;
            // 
            // checkBoxInkJet
            // 
            this.checkBoxInkJet.AutoSize = true;
            this.checkBoxInkJet.Location = new System.Drawing.Point(413, 213);
            this.checkBoxInkJet.Name = "checkBoxInkJet";
            this.checkBoxInkJet.Size = new System.Drawing.Size(52, 17);
            this.checkBoxInkJet.TabIndex = 21;
            this.checkBoxInkJet.Text = "Inkjet";
            this.checkBoxInkJet.UseVisualStyleBackColor = true;
            // 
            // checkBoxDuplo
            // 
            this.checkBoxDuplo.AutoSize = true;
            this.checkBoxDuplo.Location = new System.Drawing.Point(357, 213);
            this.checkBoxDuplo.Name = "checkBoxDuplo";
            this.checkBoxDuplo.Size = new System.Drawing.Size(54, 17);
            this.checkBoxDuplo.TabIndex = 20;
            this.checkBoxDuplo.Text = "Duplo";
            this.checkBoxDuplo.UseVisualStyleBackColor = true;
            // 
            // checkBox7100
            // 
            this.checkBox7100.AutoSize = true;
            this.checkBox7100.Location = new System.Drawing.Point(189, 213);
            this.checkBox7100.Name = "checkBox7100";
            this.checkBox7100.Size = new System.Drawing.Size(50, 17);
            this.checkBox7100.TabIndex = 19;
            this.checkBox7100.Text = "7100";
            this.checkBox7100.UseVisualStyleBackColor = true;
            this.checkBox7100.CheckedChanged += new System.EventHandler(this.checkBox7100_CheckedChanged);
            // 
            // checkBox8120
            // 
            this.checkBox8120.AutoSize = true;
            this.checkBox8120.Location = new System.Drawing.Point(121, 214);
            this.checkBox8120.Name = "checkBox8120";
            this.checkBox8120.Size = new System.Drawing.Size(50, 17);
            this.checkBox8120.TabIndex = 18;
            this.checkBox8120.Text = "8120";
            this.checkBox8120.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 214);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Print Machine";
            // 
            // cmbFileName
            // 
            this.cmbFileName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFileName.FormattingEnabled = true;
            this.cmbFileName.Location = new System.Drawing.Point(89, 94);
            this.cmbFileName.Name = "cmbFileName";
            this.cmbFileName.Size = new System.Drawing.Size(303, 21);
            this.cmbFileName.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "File Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Job Directory";
            // 
            // txtJobDirectory
            // 
            this.txtJobDirectory.Location = new System.Drawing.Point(37, 596);
            this.txtJobDirectory.Name = "txtJobDirectory";
            this.txtJobDirectory.Size = new System.Drawing.Size(303, 20);
            this.txtJobDirectory.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(850, 579);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Job Worksheet";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(850, 579);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Production Report";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnSampleSheet);
            this.splitContainer1.Panel1.Controls.Add(this.txtCustomer);
            this.splitContainer1.Panel1.Controls.Add(this.btnImportFromDolphin);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxCustomer);
            this.splitContainer1.Panel1.Controls.Add(this.txtJobName);
            this.splitContainer1.Panel1.Controls.Add(this.txtJobNo);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(858, 735);
            this.splitContainer1.SplitterDistance = 126;
            this.splitContainer1.TabIndex = 26;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(350, 16);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(252, 20);
            this.txtCustomer.TabIndex = 26;
            // 
            // btnImportFromDolphin
            // 
            this.btnImportFromDolphin.Location = new System.Drawing.Point(118, 42);
            this.btnImportFromDolphin.Name = "btnImportFromDolphin";
            this.btnImportFromDolphin.Size = new System.Drawing.Size(75, 23);
            this.btnImportFromDolphin.TabIndex = 25;
            this.btnImportFromDolphin.Text = "Import Data";
            this.btnImportFromDolphin.UseVisualStyleBackColor = true;
            this.btnImportFromDolphin.Click += new System.EventHandler(this.btnImportFromDolphin_Click);
            // 
            // btnSampleSheet
            // 
            this.btnSampleSheet.Location = new System.Drawing.Point(714, 68);
            this.btnSampleSheet.Name = "btnSampleSheet";
            this.btnSampleSheet.Size = new System.Drawing.Size(132, 23);
            this.btnSampleSheet.TabIndex = 27;
            this.btnSampleSheet.Text = "Sample Sheet";
            this.btnSampleSheet.UseVisualStyleBackColor = true;
            this.btnSampleSheet.Click += new System.EventHandler(this.btnSampleSheet_Click);
            // 
            // richTexNotes
            // 
            this.richTexNotes.Location = new System.Drawing.Point(516, 412);
            this.richTexNotes.Name = "richTexNotes";
            this.richTexNotes.Size = new System.Drawing.Size(299, 130);
            this.richTexNotes.TabIndex = 43;
            this.richTexNotes.Text = "";
            // 
            // groupBoxBranch
            // 
            this.groupBoxBranch.Controls.Add(this.rbArtwork);
            this.groupBoxBranch.Controls.Add(this.rbDatabase);
            this.groupBoxBranch.Location = new System.Drawing.Point(505, 44);
            this.groupBoxBranch.Name = "groupBoxBranch";
            this.groupBoxBranch.Size = new System.Drawing.Size(224, 42);
            this.groupBoxBranch.TabIndex = 44;
            this.groupBoxBranch.TabStop = false;
            // 
            // rbArtwork
            // 
            this.rbArtwork.AutoSize = true;
            this.rbArtwork.Location = new System.Drawing.Point(97, 15);
            this.rbArtwork.Name = "rbArtwork";
            this.rbArtwork.Size = new System.Drawing.Size(61, 17);
            this.rbArtwork.TabIndex = 23;
            this.rbArtwork.TabStop = true;
            this.rbArtwork.Text = "Artwork";
            this.rbArtwork.UseVisualStyleBackColor = true;
            // 
            // rbDatabase
            // 
            this.rbDatabase.AutoSize = true;
            this.rbDatabase.Location = new System.Drawing.Point(15, 15);
            this.rbDatabase.Name = "rbDatabase";
            this.rbDatabase.Size = new System.Drawing.Size(71, 17);
            this.rbDatabase.TabIndex = 22;
            this.rbDatabase.TabStop = true;
            this.rbDatabase.Text = "Database";
            this.rbDatabase.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnImportFromDolphin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 735);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Job Docs";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStreamQty)).EndInit();
            this.groupBoxStock.ResumeLayout(false);
            this.groupBoxStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUp)).EndInit();
            this.groupBoxPlex.ResumeLayout(false);
            this.groupBoxPlex.PerformLayout();
            this.groupBoxPaper.ResumeLayout(false);
            this.groupBoxPaper.PerformLayout();
            this.groupBoxColour.ResumeLayout(false);
            this.groupBoxColour.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxBranch.ResumeLayout(false);
            this.groupBoxBranch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtJobNo;
        private System.Windows.Forms.TextBox txtJobName;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxDataSummary;
        private System.Windows.Forms.CheckBox checkBoxProductionReport;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBoxCustomer;
        private System.Windows.Forms.Button btnSecondStream;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBoxColour;
        private System.Windows.Forms.RadioButton radioButtonBlack;
        private System.Windows.Forms.RadioButton rbColour;
        private System.Windows.Forms.CheckBox checkBoxInkJet;
        private System.Windows.Forms.CheckBox checkBoxDuplo;
        private System.Windows.Forms.CheckBox checkBox7100;
        private System.Windows.Forms.CheckBox checkBox8120;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbFileName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtJobDirectory;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnChangeJobDirectory;
        private System.Windows.Forms.Button btnImportFromDolphin;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.GroupBox groupBoxPaper;
        private System.Windows.Forms.TextBox txtCustomPaperSize;
        private System.Windows.Forms.RadioButton rbCustom;
        private System.Windows.Forms.RadioButton rbA3;
        private System.Windows.Forms.RadioButton rbSRA3;
        private System.Windows.Forms.RadioButton rbA4;
        private System.Windows.Forms.ComboBox cmbPrintJobs;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDownUp;
        private System.Windows.Forms.GroupBox groupBoxPlex;
        private System.Windows.Forms.RadioButton rbDuplex;
        private System.Windows.Forms.RadioButton rbSimplex;
        private System.Windows.Forms.Label lblPrintDescription;
        private System.Windows.Forms.GroupBox groupBoxStock;
        private System.Windows.Forms.TextBox txtStockDescription;
        private System.Windows.Forms.RadioButton rbMSOLStock;
        private System.Windows.Forms.RadioButton rbCustomerStock;
        private System.Windows.Forms.Button btnAddStream;
        private System.Windows.Forms.NumericUpDown numericUpDownStreamQty;
        private System.Windows.Forms.ListBox listBoxStreams;
        private System.Windows.Forms.ComboBox cmbStream;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RichTextBox richTextJobDirectory;
        private System.Windows.Forms.Button btnSampleSheet;
        private System.Windows.Forms.RichTextBox richTexNotes;
        private System.Windows.Forms.GroupBox groupBoxBranch;
        private System.Windows.Forms.RadioButton rbArtwork;
        private System.Windows.Forms.RadioButton rbDatabase;
    }
}

