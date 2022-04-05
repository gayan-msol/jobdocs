namespace JobDocs
{
    partial class FormCount
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
            this.numericUpDownCount = new System.Windows.Forms.NumericUpDown();
            this.btnColseCount = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownCount
            // 
            this.numericUpDownCount.Location = new System.Drawing.Point(84, 41);
            this.numericUpDownCount.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownCount.Name = "numericUpDownCount";
            this.numericUpDownCount.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownCount.TabIndex = 0;
            // 
            // btnColseCount
            // 
            this.btnColseCount.Location = new System.Drawing.Point(84, 97);
            this.btnColseCount.Name = "btnColseCount";
            this.btnColseCount.Size = new System.Drawing.Size(120, 43);
            this.btnColseCount.TabIndex = 1;
            this.btnColseCount.Text = "Close";
            this.btnColseCount.UseVisualStyleBackColor = true;
            this.btnColseCount.Click += new System.EventHandler(this.btnColseCount_Click);
            // 
            // FormCount
            // 
            this.AcceptButton = this.btnColseCount;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 186);
            this.Controls.Add(this.btnColseCount);
            this.Controls.Add(this.numericUpDownCount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormCount";
            this.Text = "Enter the Count";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownCount;
        private System.Windows.Forms.Button btnColseCount;
    }
}