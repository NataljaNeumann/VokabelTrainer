namespace VokabelTrainer
{
    partial class GraphsForm
    {


        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphsForm));
            this.m_lblTotal = new System.Windows.Forms.Label();
            this.m_lblWords = new System.Windows.Forms.Label();
            this.m_lblWordsLearned = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_lblTotal
            // 
            this.m_lblTotal.AutoSize = true;
            this.m_lblTotal.Location = new System.Drawing.Point(12, 44);
            this.m_lblTotal.Name = "m_lblTotal";
            this.m_lblTotal.Size = new System.Drawing.Size(78, 13);
            this.m_lblTotal.TabIndex = 0;
            this.m_lblTotal.Text = "Total exercises";
            this.m_lblTotal.Visible = false;
            // 
            // m_lblWords
            // 
            this.m_lblWords.AutoSize = true;
            this.m_lblWords.Location = new System.Drawing.Point(12, 71);
            this.m_lblWords.Name = "m_lblWords";
            this.m_lblWords.Size = new System.Drawing.Size(87, 13);
            this.m_lblWords.TabIndex = 1;
            this.m_lblWords.Text = "Number of words";
            this.m_lblWords.Visible = false;
            // 
            // m_lblWordsLearned
            // 
            this.m_lblWordsLearned.AutoSize = true;
            this.m_lblWordsLearned.Location = new System.Drawing.Point(12, 94);
            this.m_lblWordsLearned.Name = "m_lblWordsLearned";
            this.m_lblWordsLearned.Size = new System.Drawing.Size(77, 13);
            this.m_lblWordsLearned.TabIndex = 2;
            this.m_lblWordsLearned.Text = "Learned words";
            this.m_lblWordsLearned.Visible = false;
            // 
            // GraphsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(648, 301);
            this.Controls.Add(this.m_lblWordsLearned);
            this.Controls.Add(this.m_lblWords);
            this.Controls.Add(this.m_lblTotal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 182);
            this.Name = "GraphsForm";
            this.Text = "Learning history";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaintForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblTotal;
        private System.Windows.Forms.Label m_lblWords;
        private System.Windows.Forms.Label m_lblWordsLearned;
    }
}