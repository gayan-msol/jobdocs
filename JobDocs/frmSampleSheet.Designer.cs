namespace JobDocs
{
    partial class frmSampleSheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSampleSheet));
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddPageNumbers = new System.Windows.Forms.Button();
            this.numUpDownPageNumbers = new System.Windows.Forms.NumericUpDown();
            this.listBoxPageNumbers = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbColumn = new System.Windows.Forms.ComboBox();
            this.checkBoxUncheckAll = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanelColumns = new System.Windows.Forms.FlowLayoutPanel();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewSample = new System.Windows.Forms.DataGridView();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPageNumbers)).BeginInit();
            this.wizardPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSample)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackColor = System.Drawing.Color.White;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.Add(this.wizardPage1);
            this.wizardControl1.Pages.Add(this.wizardPage3);
            this.wizardControl1.Size = new System.Drawing.Size(806, 552);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Text = "Sample Sheet Creation Wizard";
            this.wizardControl1.Title = "Sample Sheet Creation Wizard";
            this.wizardControl1.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl1.TitleIcon")));
            this.wizardControl1.SelectedPageChanged += new System.EventHandler(this.wizardControl1_SelectedPageChanged);
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.label2);
            this.wizardPage1.Controls.Add(this.btnAddPageNumbers);
            this.wizardPage1.Controls.Add(this.numUpDownPageNumbers);
            this.wizardPage1.Controls.Add(this.listBoxPageNumbers);
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Controls.Add(this.cmbColumn);
            this.wizardPage1.Controls.Add(this.checkBoxUncheckAll);
            this.wizardPage1.Controls.Add(this.flowLayoutPanelColumns);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(759, 398);
            this.wizardPage1.TabIndex = 0;
            this.wizardPage1.Text = "Import Output File";
            this.wizardPage1.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage1_Commit);
            this.wizardPage1.Enter += new System.EventHandler(this.wizardPage1_Enter);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(103, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "Include following records";
            // 
            // btnAddPageNumbers
            // 
            this.btnAddPageNumbers.Location = new System.Drawing.Point(168, 269);
            this.btnAddPageNumbers.Name = "btnAddPageNumbers";
            this.btnAddPageNumbers.Size = new System.Drawing.Size(75, 23);
            this.btnAddPageNumbers.TabIndex = 10;
            this.btnAddPageNumbers.Text = "Add";
            this.btnAddPageNumbers.UseVisualStyleBackColor = true;
            this.btnAddPageNumbers.Click += new System.EventHandler(this.btnAddPageNumbers_Click);
            // 
            // numUpDownPageNumbers
            // 
            this.numUpDownPageNumbers.Location = new System.Drawing.Point(106, 272);
            this.numUpDownPageNumbers.Name = "numUpDownPageNumbers";
            this.numUpDownPageNumbers.Size = new System.Drawing.Size(56, 23);
            this.numUpDownPageNumbers.TabIndex = 9;
            this.numUpDownPageNumbers.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // listBoxPageNumbers
            // 
            this.listBoxPageNumbers.FormattingEnabled = true;
            this.listBoxPageNumbers.ItemHeight = 15;
            this.listBoxPageNumbers.Location = new System.Drawing.Point(106, 298);
            this.listBoxPageNumbers.Name = "listBoxPageNumbers";
            this.listBoxPageNumbers.Size = new System.Drawing.Size(137, 64);
            this.listBoxPageNumbers.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(98, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 43);
            this.label1.TabIndex = 7;
            this.label1.Text = "Pick a column to include all the distinct values in samples";
            // 
            // cmbColumn
            // 
            this.cmbColumn.FormattingEnabled = true;
            this.cmbColumn.Location = new System.Drawing.Point(106, 173);
            this.cmbColumn.Name = "cmbColumn";
            this.cmbColumn.Size = new System.Drawing.Size(121, 23);
            this.cmbColumn.TabIndex = 6;
            // 
            // checkBoxUncheckAll
            // 
            this.checkBoxUncheckAll.AutoSize = true;
            this.checkBoxUncheckAll.Location = new System.Drawing.Point(101, 37);
            this.checkBoxUncheckAll.Name = "checkBoxUncheckAll";
            this.checkBoxUncheckAll.Size = new System.Drawing.Size(119, 19);
            this.checkBoxUncheckAll.TabIndex = 5;
            this.checkBoxUncheckAll.Text = "Remove All Fileds";
            this.checkBoxUncheckAll.UseVisualStyleBackColor = true;
            this.checkBoxUncheckAll.CheckedChanged += new System.EventHandler(this.checkBoxUncheckAll_CheckedChanged);
            // 
            // flowLayoutPanelColumns
            // 
            this.flowLayoutPanelColumns.AutoScroll = true;
            this.flowLayoutPanelColumns.AutoScrollMargin = new System.Drawing.Size(5, 5);
            this.flowLayoutPanelColumns.AutoScrollMinSize = new System.Drawing.Size(5, 5);
            this.flowLayoutPanelColumns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelColumns.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelColumns.Location = new System.Drawing.Point(268, 37);
            this.flowLayoutPanelColumns.Name = "flowLayoutPanelColumns";
            this.flowLayoutPanelColumns.Size = new System.Drawing.Size(384, 325);
            this.flowLayoutPanelColumns.TabIndex = 3;
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.splitContainer1);
            this.wizardPage3.IsFinishPage = true;
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.ShowNext = false;
            this.wizardPage3.Size = new System.Drawing.Size(759, 398);
            this.wizardPage3.TabIndex = 2;
            this.wizardPage3.Text = "Sample Records";
            this.wizardPage3.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage3_Commit);
            this.wizardPage3.Leave += new System.EventHandler(this.wizardPage3_Leave);
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
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewSample);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnExcel);
            this.splitContainer1.Panel2.Controls.Add(this.btnPrint);
            this.splitContainer1.Size = new System.Drawing.Size(759, 398);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 2;
            // 
            // dataGridViewSample
            // 
            this.dataGridViewSample.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSample.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSample.Name = "dataGridViewSample";
            this.dataGridViewSample.Size = new System.Drawing.Size(759, 350);
            this.dataGridViewSample.TabIndex = 0;
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(491, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(77, 38);
            this.btnExcel.TabIndex = 2;
            this.btnExcel.Text = "Create Excel sheet";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(646, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 38);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // frmSampleSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 552);
            this.Controls.Add(this.wizardControl1);
            this.Name = "frmSampleSheet";
            this.Text = "SampleSheet";
            this.Load += new System.EventHandler(this.frmSampleSheet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPageNumbers)).EndInit();
            this.wizardPage3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSample)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage wizardPage1;
        private AeroWizard.WizardPage wizardPage3;
        private System.Windows.Forms.DataGridView dataGridViewSample;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.CheckBox checkBoxUncheckAll;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelColumns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbColumn;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.NumericUpDown numUpDownPageNumbers;
        private System.Windows.Forms.ListBox listBoxPageNumbers;
        private System.Windows.Forms.Button btnAddPageNumbers;
        private System.Windows.Forms.Label label2;
    }
}