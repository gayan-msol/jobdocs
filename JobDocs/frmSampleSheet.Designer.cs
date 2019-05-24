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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbComma = new System.Windows.Forms.RadioButton();
            this.rbTab = new System.Windows.Forms.RadioButton();
            this.richTextOutputFilePath = new System.Windows.Forms.RichTextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.checkBoxUncheckAll = new System.Windows.Forms.CheckBox();
            this.checkBoxExcludeDTFields = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanelColumns = new System.Windows.Forms.FlowLayoutPanel();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewSample = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
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
            this.wizardControl1.Pages.Add(this.wizardPage2);
            this.wizardControl1.Pages.Add(this.wizardPage3);
            this.wizardControl1.Size = new System.Drawing.Size(806, 552);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Text = "Sample Sheet Creation Wizard";
            this.wizardControl1.Title = "Sample Sheet Creation Wizard";
            this.wizardControl1.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl1.TitleIcon")));
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.groupBox1);
            this.wizardPage1.Controls.Add(this.richTextOutputFilePath);
            this.wizardPage1.Controls.Add(this.btnBrowse);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(759, 398);
            this.wizardPage1.TabIndex = 0;
            this.wizardPage1.Text = "Import Output File";
            this.wizardPage1.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage1_Commit);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbComma);
            this.groupBox1.Controls.Add(this.rbTab);
            this.groupBox1.Location = new System.Drawing.Point(412, 182);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 48);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Delimiter";
            // 
            // rbComma
            // 
            this.rbComma.AutoSize = true;
            this.rbComma.Location = new System.Drawing.Point(73, 22);
            this.rbComma.Name = "rbComma";
            this.rbComma.Size = new System.Drawing.Size(68, 19);
            this.rbComma.TabIndex = 1;
            this.rbComma.Text = "Comma";
            this.rbComma.UseVisualStyleBackColor = true;
            this.rbComma.CheckedChanged += new System.EventHandler(this.rbComma_CheckedChanged);
            // 
            // rbTab
            // 
            this.rbTab.AutoSize = true;
            this.rbTab.Checked = true;
            this.rbTab.Location = new System.Drawing.Point(7, 23);
            this.rbTab.Name = "rbTab";
            this.rbTab.Size = new System.Drawing.Size(45, 19);
            this.rbTab.TabIndex = 0;
            this.rbTab.TabStop = true;
            this.rbTab.Text = "Tab";
            this.rbTab.UseVisualStyleBackColor = true;
            this.rbTab.CheckedChanged += new System.EventHandler(this.rbTab_CheckedChanged);
            // 
            // richTextOutputFilePath
            // 
            this.richTextOutputFilePath.Location = new System.Drawing.Point(84, 79);
            this.richTextOutputFilePath.Name = "richTextOutputFilePath";
            this.richTextOutputFilePath.Size = new System.Drawing.Size(487, 53);
            this.richTextOutputFilePath.TabIndex = 2;
            this.richTextOutputFilePath.Text = "";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(577, 109);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.checkBoxUncheckAll);
            this.wizardPage2.Controls.Add(this.checkBoxExcludeDTFields);
            this.wizardPage2.Controls.Add(this.flowLayoutPanelColumns);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(759, 398);
            this.wizardPage2.TabIndex = 1;
            this.wizardPage2.Text = "Select Columns";
            this.wizardPage2.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage2_Commit);
            // 
            // checkBoxUncheckAll
            // 
            this.checkBoxUncheckAll.AutoSize = true;
            this.checkBoxUncheckAll.Location = new System.Drawing.Point(87, 78);
            this.checkBoxUncheckAll.Name = "checkBoxUncheckAll";
            this.checkBoxUncheckAll.Size = new System.Drawing.Size(86, 19);
            this.checkBoxUncheckAll.TabIndex = 2;
            this.checkBoxUncheckAll.Text = "Remove All";
            this.checkBoxUncheckAll.UseVisualStyleBackColor = true;
            this.checkBoxUncheckAll.CheckedChanged += new System.EventHandler(this.checkBoxUncheckAll_CheckedChanged);
            // 
            // checkBoxExcludeDTFields
            // 
            this.checkBoxExcludeDTFields.AutoSize = true;
            this.checkBoxExcludeDTFields.Checked = true;
            this.checkBoxExcludeDTFields.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExcludeDTFields.Location = new System.Drawing.Point(87, 31);
            this.checkBoxExcludeDTFields.Name = "checkBoxExcludeDTFields";
            this.checkBoxExcludeDTFields.Size = new System.Drawing.Size(117, 19);
            this.checkBoxExcludeDTFields.TabIndex = 1;
            this.checkBoxExcludeDTFields.Text = "Exclude DT Fileds";
            this.checkBoxExcludeDTFields.UseVisualStyleBackColor = true;
            this.checkBoxExcludeDTFields.CheckedChanged += new System.EventHandler(this.checkBoxExcludeDTFields_CheckedChanged);
            // 
            // flowLayoutPanelColumns
            // 
            this.flowLayoutPanelColumns.AutoScroll = true;
            this.flowLayoutPanelColumns.AutoScrollMargin = new System.Drawing.Size(5, 5);
            this.flowLayoutPanelColumns.AutoScrollMinSize = new System.Drawing.Size(5, 5);
            this.flowLayoutPanelColumns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelColumns.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelColumns.Location = new System.Drawing.Point(249, 31);
            this.flowLayoutPanelColumns.Name = "flowLayoutPanelColumns";
            this.flowLayoutPanelColumns.Size = new System.Drawing.Size(384, 325);
            this.flowLayoutPanelColumns.TabIndex = 0;
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
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
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
        private System.Windows.Forms.Button btnBrowse;
        private AeroWizard.WizardPage wizardPage2;
        private System.Windows.Forms.RichTextBox richTextOutputFilePath;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelColumns;
        private System.Windows.Forms.CheckBox checkBoxExcludeDTFields;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbComma;
        private System.Windows.Forms.RadioButton rbTab;
        private AeroWizard.WizardPage wizardPage3;
        private System.Windows.Forms.DataGridView dataGridViewSample;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.CheckBox checkBoxUncheckAll;
    }
}