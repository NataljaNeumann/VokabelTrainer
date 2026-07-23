namespace VokabelTrainer.Forms
{
    partial class ChineseVariantSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChineseVariantSelection));
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_rbTraditionalChineseLanguageOfTaiwan = new System.Windows.Forms.RadioButton();
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting = new System.Windows.Forms.RadioButton();
            this.m_rbChinesePeoplesRepublicOfChina = new System.Windows.Forms.RadioButton();
            this.m_lblChineseVariant = new System.Windows.Forms.Label();
            this.m_rbPinyinChineseOfTaiwanPinyinWriting = new System.Windows.Forms.RadioButton();
            this.m_rbZhuyinFuhaoWritingOfTaiwan = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // m_btnOk
            // 
            resources.ApplyResources(this.m_btnOk, "m_btnOk");
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_rbTraditionalChineseLanguageOfTaiwan
            // 
            resources.ApplyResources(this.m_rbTraditionalChineseLanguageOfTaiwan, "m_rbTraditionalChineseLanguageOfTaiwan");
            this.m_rbTraditionalChineseLanguageOfTaiwan.Name = "m_rbTraditionalChineseLanguageOfTaiwan";
            this.m_rbTraditionalChineseLanguageOfTaiwan.UseVisualStyleBackColor = true;
            this.m_rbTraditionalChineseLanguageOfTaiwan.CheckedChanged += new System.EventHandler(this.m_rbTraditionalChineseLanguageOfTaiwan_CheckedChanged);
            // 
            // m_rbChinesePeplesRepublicOfChinaPinyinWriting
            // 
            resources.ApplyResources(this.m_rbChinesePeplesRepublicOfChinaPinyinWriting, "m_rbChinesePeplesRepublicOfChinaPinyinWriting");
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.Name = "m_rbChinesePeplesRepublicOfChinaPinyinWriting";
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.UseVisualStyleBackColor = true;
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.CheckedChanged += new System.EventHandler(this.m_rbChineseOfPeoplesRepublicOfChinaPinyinWriting_CheckedChanged);
            // 
            // m_rbChinesePeoplesRepublicOfChina
            // 
            resources.ApplyResources(this.m_rbChinesePeoplesRepublicOfChina, "m_rbChinesePeoplesRepublicOfChina");
            this.m_rbChinesePeoplesRepublicOfChina.Name = "m_rbChinesePeoplesRepublicOfChina";
            this.m_rbChinesePeoplesRepublicOfChina.TabStop = true;
            this.m_rbChinesePeoplesRepublicOfChina.UseVisualStyleBackColor = true;
            this.m_rbChinesePeoplesRepublicOfChina.CheckedChanged += new System.EventHandler(this.m_rbChinesePeoplesRepublicOfChina_CheckedChanged);
            // 
            // m_lblChineseVariant
            // 
            resources.ApplyResources(this.m_lblChineseVariant, "m_lblChineseVariant");
            this.m_lblChineseVariant.Name = "m_lblChineseVariant";
            // 
            // m_rbPinyinChineseOfTaiwanPinyinWriting
            // 
            resources.ApplyResources(this.m_rbPinyinChineseOfTaiwanPinyinWriting, "m_rbPinyinChineseOfTaiwanPinyinWriting");
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.Name = "m_rbPinyinChineseOfTaiwanPinyinWriting";
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.UseVisualStyleBackColor = true;
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.CheckedChanged += new System.EventHandler(this.m_rbPinyinChineseOfTaiwanPinyinWriting_CheckedChanged);
            // 
            // m_rbZhuyinFuhaoWritingOfTaiwan
            // 
            resources.ApplyResources(this.m_rbZhuyinFuhaoWritingOfTaiwan, "m_rbZhuyinFuhaoWritingOfTaiwan");
            this.m_rbZhuyinFuhaoWritingOfTaiwan.Name = "m_rbZhuyinFuhaoWritingOfTaiwan";
            this.m_rbZhuyinFuhaoWritingOfTaiwan.UseVisualStyleBackColor = true;
            this.m_rbZhuyinFuhaoWritingOfTaiwan.CheckedChanged += new System.EventHandler(this.m_rbZhuyinFuhaoWritingOfTaiwan_CheckedChanged);
            // 
            // ChineseVariantSelection
            // 
            this.AcceptButton = this.m_btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_rbZhuyinFuhaoWritingOfTaiwan);
            this.Controls.Add(this.m_rbPinyinChineseOfTaiwanPinyinWriting);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_rbTraditionalChineseLanguageOfTaiwan);
            this.Controls.Add(this.m_rbChinesePeplesRepublicOfChinaPinyinWriting);
            this.Controls.Add(this.m_rbChinesePeoplesRepublicOfChina);
            this.Controls.Add(this.m_lblChineseVariant);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChineseVariantSelection";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.RadioButton m_rbTraditionalChineseLanguageOfTaiwan;
        private System.Windows.Forms.RadioButton m_rbChinesePeplesRepublicOfChinaPinyinWriting;
        private System.Windows.Forms.RadioButton m_rbChinesePeoplesRepublicOfChina;
        private System.Windows.Forms.Label m_lblChineseVariant;
        private System.Windows.Forms.RadioButton m_rbPinyinChineseOfTaiwanPinyinWriting;
        private System.Windows.Forms.RadioButton m_rbZhuyinFuhaoWritingOfTaiwan;
    }
}