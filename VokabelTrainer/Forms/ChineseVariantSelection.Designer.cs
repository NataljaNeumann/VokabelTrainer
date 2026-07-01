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
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_btnOk.Location = new System.Drawing.Point(646, 247);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(114, 38);
            this.m_btnOk.TabIndex = 10;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.UseVisualStyleBackColor = true;
            // 
            // m_rbTraditionalChineseLanguageOfTaiwan
            // 
            this.m_rbTraditionalChineseLanguageOfTaiwan.AutoSize = true;
            this.m_rbTraditionalChineseLanguageOfTaiwan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_rbTraditionalChineseLanguageOfTaiwan.Location = new System.Drawing.Point(20, 160);
            this.m_rbTraditionalChineseLanguageOfTaiwan.Name = "m_rbTraditionalChineseLanguageOfTaiwan";
            this.m_rbTraditionalChineseLanguageOfTaiwan.Size = new System.Drawing.Size(388, 29);
            this.m_rbTraditionalChineseLanguageOfTaiwan.TabIndex = 9;
            this.m_rbTraditionalChineseLanguageOfTaiwan.Text = "Traditional chinese language, Taiwan";
            this.m_rbTraditionalChineseLanguageOfTaiwan.UseVisualStyleBackColor = true;
            this.m_rbTraditionalChineseLanguageOfTaiwan.CheckedChanged += new System.EventHandler(this.m_rbTraditionalChineseLanguageOfTaiwan_CheckedChanged);
            // 
            // m_rbChinesePeplesRepublicOfChinaPinyinWriting
            // 
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.AutoSize = true;
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.Location = new System.Drawing.Point(20, 125);
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.Name = "m_rbChinesePeplesRepublicOfChinaPinyinWriting";
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.Size = new System.Drawing.Size(221, 29);
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.TabIndex = 8;
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.Text = "Hanyu Pinyin (PRC)";
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.UseVisualStyleBackColor = true;
            this.m_rbChinesePeplesRepublicOfChinaPinyinWriting.CheckedChanged += new System.EventHandler(this.m_rbChineseOfPeoplesRepublicOfChinaPinyinWriting_CheckedChanged);
            // 
            // m_rbChinesePeoplesRepublicOfChina
            // 
            this.m_rbChinesePeoplesRepublicOfChina.AutoSize = true;
            this.m_rbChinesePeoplesRepublicOfChina.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_rbChinesePeoplesRepublicOfChina.Location = new System.Drawing.Point(20, 90);
            this.m_rbChinesePeoplesRepublicOfChina.Name = "m_rbChinesePeoplesRepublicOfChina";
            this.m_rbChinesePeoplesRepublicOfChina.Size = new System.Drawing.Size(585, 29);
            this.m_rbChinesePeoplesRepublicOfChina.TabIndex = 7;
            this.m_rbChinesePeoplesRepublicOfChina.Text = "Chinese language of peoples republic of China (Standard)";
            this.m_rbChinesePeoplesRepublicOfChina.UseVisualStyleBackColor = true;
            this.m_rbChinesePeoplesRepublicOfChina.CheckedChanged += new System.EventHandler(this.m_rbChinesePeoplesRepublicOfChina_CheckedChanged);
            // 
            // m_lblChineseVariant
            // 
            this.m_lblChineseVariant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblChineseVariant.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_lblChineseVariant.Location = new System.Drawing.Point(15, 9);
            this.m_lblChineseVariant.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.m_lblChineseVariant.Name = "m_lblChineseVariant";
            this.m_lblChineseVariant.Size = new System.Drawing.Size(767, 59);
            this.m_lblChineseVariant.TabIndex = 6;
            this.m_lblChineseVariant.Text = "Please select the variant of the Chinese language that you want to use";
            this.m_lblChineseVariant.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // m_rbPinyinChineseOfTaiwanPinyinWriting
            // 
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.AutoSize = true;
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.Location = new System.Drawing.Point(20, 195);
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.Name = "m_rbPinyinChineseOfTaiwanPinyinWriting";
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.Size = new System.Drawing.Size(246, 29);
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.TabIndex = 11;
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.Text = "Hanyu Pinyin (Taiwan)";
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.UseVisualStyleBackColor = true;
            this.m_rbPinyinChineseOfTaiwanPinyinWriting.CheckedChanged += new System.EventHandler(this.m_rbPinyinChineseOfTaiwanPinyinWriting_CheckedChanged);
            // 
            // m_rbZhuyinFuhaoWritingOfTaiwan
            // 
            this.m_rbZhuyinFuhaoWritingOfTaiwan.AutoSize = true;
            this.m_rbZhuyinFuhaoWritingOfTaiwan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.m_rbZhuyinFuhaoWritingOfTaiwan.Location = new System.Drawing.Point(20, 230);
            this.m_rbZhuyinFuhaoWritingOfTaiwan.Name = "m_rbZhuyinFuhaoWritingOfTaiwan";
            this.m_rbZhuyinFuhaoWritingOfTaiwan.Size = new System.Drawing.Size(251, 29);
            this.m_rbZhuyinFuhaoWritingOfTaiwan.TabIndex = 12;
            this.m_rbZhuyinFuhaoWritingOfTaiwan.Text = "Zhuyin Fuhao (Taiwan)";
            this.m_rbZhuyinFuhaoWritingOfTaiwan.UseVisualStyleBackColor = true;
            this.m_rbZhuyinFuhaoWritingOfTaiwan.CheckedChanged += new System.EventHandler(this.m_rbZhuyinFuhaoWritingOfTaiwan_CheckedChanged);
            // 
            // ChineseVariantSelection
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 317);
            this.Controls.Add(this.m_rbZhuyinFuhaoWritingOfTaiwan);
            this.Controls.Add(this.m_rbPinyinChineseOfTaiwanPinyinWriting);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_rbTraditionalChineseLanguageOfTaiwan);
            this.Controls.Add(this.m_rbChinesePeplesRepublicOfChinaPinyinWriting);
            this.Controls.Add(this.m_rbChinesePeoplesRepublicOfChina);
            this.Controls.Add(this.m_lblChineseVariant);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ChineseVariantSelection";
            this.Text = "Selection of Variant of Chinese Language";
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