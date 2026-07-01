namespace VokabelTrainer.Forms
{
    partial class JapaneseVariantSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JapaneseVariantSelection));
            this.m_lblJapaneseVariant = new System.Windows.Forms.Label();
            this.m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage = new System.Windows.Forms.RadioButton();
            this.m_rbJapaneseHiraganaOnly = new System.Windows.Forms.RadioButton();
            this.m_rbJapaneseRomajiOnly = new System.Windows.Forms.RadioButton();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_rbJapaneseKatakanaAndHiragadaOnly = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // m_lblJapaneseVariant
            // 
            resources.ApplyResources(this.m_lblJapaneseVariant, "m_lblJapaneseVariant");
            this.m_lblJapaneseVariant.Name = "m_lblJapaneseVariant";
            // 
            // m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage
            // 
            resources.ApplyResources(this.m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage, "m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage");
            this.m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage.Checked = true;
            this.m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage.Name = "m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage";
            this.m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage.TabStop = true;
            this.m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage.UseVisualStyleBackColor = true;
            this.m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage.CheckedChanged += new System.EventHandler(this.m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage_CheckedChanged);
            // 
            // m_rbJapaneseHiraganaOnly
            // 
            resources.ApplyResources(this.m_rbJapaneseHiraganaOnly, "m_rbJapaneseHiraganaOnly");
            this.m_rbJapaneseHiraganaOnly.Name = "m_rbJapaneseHiraganaOnly";
            this.m_rbJapaneseHiraganaOnly.UseVisualStyleBackColor = true;
            this.m_rbJapaneseHiraganaOnly.CheckedChanged += new System.EventHandler(this.m_rbJapaneseHiraganaOnly_CheckedChanged);
            // 
            // m_rbJapaneseRomajiOnly
            // 
            resources.ApplyResources(this.m_rbJapaneseRomajiOnly, "m_rbJapaneseRomajiOnly");
            this.m_rbJapaneseRomajiOnly.Name = "m_rbJapaneseRomajiOnly";
            this.m_rbJapaneseRomajiOnly.UseVisualStyleBackColor = true;
            this.m_rbJapaneseRomajiOnly.CheckedChanged += new System.EventHandler(this.m_rbJapaneseRomajiOnly_CheckedChanged);
            // 
            // m_btnOk
            // 
            resources.ApplyResources(this.m_btnOk, "m_btnOk");
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.UseVisualStyleBackColor = true;
            // 
            // m_rbJapaneseKatakanaAndHiragadaOnly
            // 
            resources.ApplyResources(this.m_rbJapaneseKatakanaAndHiragadaOnly, "m_rbJapaneseKatakanaAndHiragadaOnly");
            this.m_rbJapaneseKatakanaAndHiragadaOnly.Name = "m_rbJapaneseKatakanaAndHiragadaOnly";
            this.m_rbJapaneseKatakanaAndHiragadaOnly.UseVisualStyleBackColor = true;
            this.m_rbJapaneseKatakanaAndHiragadaOnly.CheckedChanged += new System.EventHandler(this.m_rbJapaneseKatakanaAndHiragadaOnly_CheckedChanged);
            // 
            // JapaneseVariantSelection
            // 
            this.AcceptButton = this.m_btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_rbJapaneseKatakanaAndHiragadaOnly);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_rbJapaneseRomajiOnly);
            this.Controls.Add(this.m_rbJapaneseHiraganaOnly);
            this.Controls.Add(this.m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage);
            this.Controls.Add(this.m_lblJapaneseVariant);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JapaneseVariantSelection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblJapaneseVariant;
        private System.Windows.Forms.RadioButton m_rbKanjiAndHiraganaAndKatakanaNormalJapaneseLanguage;
        private System.Windows.Forms.RadioButton m_rbJapaneseHiraganaOnly;
        private System.Windows.Forms.RadioButton m_rbJapaneseRomajiOnly;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.RadioButton m_rbJapaneseKatakanaAndHiragadaOnly;
    }
}