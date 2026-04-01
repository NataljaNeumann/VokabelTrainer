namespace VokabelTrainer.Forms
{
    partial class MistakesDisplayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MistakesDisplayForm));
            this.m_lblText = new System.Windows.Forms.Label();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lblText
            // 
            this.m_lblText.AutoSize = true;
            this.m_lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblText.Location = new System.Drawing.Point(10, 9);
            this.m_lblText.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.m_lblText.MaximumSize = new System.Drawing.Size(412, 455);
            this.m_lblText.Name = "m_lblText";
            this.m_lblText.Size = new System.Drawing.Size(18, 25);
            this.m_lblText.TabIndex = 0;
            this.m_lblText.Text = ".";
            this.m_lblText.SizeChanged += new System.EventHandler(this.OnSizeOfLabelChanged);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnOk.Location = new System.Drawing.Point(65, 79);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(100, 37);
            this.m_btnOk.TabIndex = 1;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.OnOkClicked);
            // 
            // MistakesDisplayForm
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(177, 128);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_lblText);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MistakesDisplayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MistakesDisplayForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblText;
        private System.Windows.Forms.Button m_btnOk;
    }
}