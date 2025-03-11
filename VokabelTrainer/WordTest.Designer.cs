/*  Vokabel-Trainer aims to provide you a simple vocabulary training app
    Copyright (C) 2024 NataljaNeumann@gmx.de

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License along
    with this program; if not, write to the Free Software Foundation, Inc.,
    51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/

namespace VokabelTrainer
{
    partial class WordTest
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordTest));
            this.m_lblShownText = new System.Windows.Forms.Label();
            this.m_lblAskedTranslation = new System.Windows.Forms.Label();
            this.m_tbxAskedTranslation = new System.Windows.Forms.TextBox();
            this.m_btnNext = new System.Windows.Forms.Button();
            this.m_btnLast = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lblShownText
            // 
            this.m_lblShownText.AccessibleDescription = null;
            this.m_lblShownText.AccessibleName = null;
            resources.ApplyResources(this.m_lblShownText, "m_lblShownText");
            this.m_lblShownText.Name = "m_lblShownText";
            // 
            // m_lblAskedTranslation
            // 
            this.m_lblAskedTranslation.AccessibleDescription = null;
            this.m_lblAskedTranslation.AccessibleName = null;
            resources.ApplyResources(this.m_lblAskedTranslation, "m_lblAskedTranslation");
            this.m_lblAskedTranslation.Name = "m_lblAskedTranslation";
            // 
            // m_tbxAskedTranslation
            // 
            this.m_tbxAskedTranslation.AccessibleDescription = null;
            this.m_tbxAskedTranslation.AccessibleName = null;
            resources.ApplyResources(this.m_tbxAskedTranslation, "m_tbxAskedTranslation");
            this.m_tbxAskedTranslation.BackgroundImage = null;
            this.m_tbxAskedTranslation.Name = "m_tbxAskedTranslation";
            this.m_tbxAskedTranslation.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // m_btnNext
            // 
            this.m_btnNext.AccessibleDescription = null;
            this.m_btnNext.AccessibleName = null;
            resources.ApplyResources(this.m_btnNext, "m_btnNext");
            this.m_btnNext.BackgroundImage = null;
            this.m_btnNext.Name = "m_btnNext";
            this.m_btnNext.UseVisualStyleBackColor = true;
            this.m_btnNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // m_btnLast
            // 
            this.m_btnLast.AccessibleDescription = null;
            this.m_btnLast.AccessibleName = null;
            resources.ApplyResources(this.m_btnLast, "m_btnLast");
            this.m_btnLast.BackgroundImage = null;
            this.m_btnLast.Name = "m_btnLast";
            this.m_btnLast.UseVisualStyleBackColor = true;
            this.m_btnLast.Click += new System.EventHandler(this.buttonLast_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.AccessibleDescription = null;
            this.m_btnCancel.AccessibleName = null;
            resources.ApplyResources(this.m_btnCancel, "m_btnCancel");
            this.m_btnCancel.BackgroundImage = null;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // WordTest
            // 
            this.AcceptButton = this.m_btnNext;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.m_btnCancel;
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnLast);
            this.Controls.Add(this.m_btnNext);
            this.Controls.Add(this.m_tbxAskedTranslation);
            this.Controls.Add(this.m_lblAskedTranslation);
            this.Controls.Add(this.m_lblShownText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WordTest";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Shown += new System.EventHandler(this.WordTest_Shown);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.OnHelpRequested);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnNext;
        private System.Windows.Forms.Button m_btnLast;
        private System.Windows.Forms.Button m_btnCancel;
        public System.Windows.Forms.Label m_lblShownText;
        public System.Windows.Forms.Label m_lblAskedTranslation;
        public System.Windows.Forms.TextBox m_tbxAskedTranslation;
    }
}