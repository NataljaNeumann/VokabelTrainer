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
            this.m_lblTotal.AccessibleDescription = null;
            this.m_lblTotal.AccessibleName = null;
            resources.ApplyResources(this.m_lblTotal, "m_lblTotal");
            this.m_lblTotal.Name = "m_lblTotal";
            // 
            // m_lblWords
            // 
            this.m_lblWords.AccessibleDescription = null;
            this.m_lblWords.AccessibleName = null;
            resources.ApplyResources(this.m_lblWords, "m_lblWords");
            this.m_lblWords.Name = "m_lblWords";
            // 
            // m_lblWordsLearned
            // 
            this.m_lblWordsLearned.AccessibleDescription = null;
            this.m_lblWordsLearned.AccessibleName = null;
            resources.ApplyResources(this.m_lblWordsLearned, "m_lblWordsLearned");
            this.m_lblWordsLearned.Name = "m_lblWordsLearned";
            // 
            // GraphsForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = null;
            this.Controls.Add(this.m_lblWordsLearned);
            this.Controls.Add(this.m_lblWords);
            this.Controls.Add(this.m_lblTotal);
            this.Name = "GraphsForm";
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