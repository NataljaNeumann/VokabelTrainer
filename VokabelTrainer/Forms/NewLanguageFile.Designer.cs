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
    partial class NewLanguageFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewLanguageFile));
            this.m_lblFirstLanguage = new System.Windows.Forms.Label();
            this.m_tbxFirstLanguage = new System.Windows.Forms.TextBox();
            this.m_tbxSecondLanguage = new System.Windows.Forms.TextBox();
            this.m_lblSecondLanguage = new System.Windows.Forms.Label();
            this.m_btnCreateLanguageFile = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_chkLanguageFileUnderGPL2 = new System.Windows.Forms.CheckBox();
            this.m_chkLanguageFileModifiable = new System.Windows.Forms.CheckBox();
            this.m_chkFirstLanguageRTL = new System.Windows.Forms.CheckBox();
            this.m_chkSecondLanguageRTL = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_lblFirstLanguage
            // 
            this.m_lblFirstLanguage.AccessibleDescription = null;
            this.m_lblFirstLanguage.AccessibleName = null;
            resources.ApplyResources(this.m_lblFirstLanguage, "m_lblFirstLanguage");
            this.m_lblFirstLanguage.Name = "m_lblFirstLanguage";
            // 
            // m_tbxFirstLanguage
            // 
            this.m_tbxFirstLanguage.AccessibleDescription = null;
            this.m_tbxFirstLanguage.AccessibleName = null;
            resources.ApplyResources(this.m_tbxFirstLanguage, "m_tbxFirstLanguage");
            this.m_tbxFirstLanguage.BackgroundImage = null;
            this.m_tbxFirstLanguage.Name = "m_tbxFirstLanguage";
            this.m_tbxFirstLanguage.TextChanged += new System.EventHandler(this.textBoxFirstLanguage_TextChanged);
            // 
            // m_tbxSecondLanguage
            // 
            this.m_tbxSecondLanguage.AccessibleDescription = null;
            this.m_tbxSecondLanguage.AccessibleName = null;
            resources.ApplyResources(this.m_tbxSecondLanguage, "m_tbxSecondLanguage");
            this.m_tbxSecondLanguage.BackgroundImage = null;
            this.m_tbxSecondLanguage.Name = "m_tbxSecondLanguage";
            this.m_tbxSecondLanguage.TextChanged += new System.EventHandler(this.textBoxSecondLanguage_TextChanged);
            // 
            // m_lblSecondLanguage
            // 
            this.m_lblSecondLanguage.AccessibleDescription = null;
            this.m_lblSecondLanguage.AccessibleName = null;
            resources.ApplyResources(this.m_lblSecondLanguage, "m_lblSecondLanguage");
            this.m_lblSecondLanguage.Name = "m_lblSecondLanguage";
            // 
            // m_btnCreateLanguageFile
            // 
            this.m_btnCreateLanguageFile.AccessibleDescription = null;
            this.m_btnCreateLanguageFile.AccessibleName = null;
            resources.ApplyResources(this.m_btnCreateLanguageFile, "m_btnCreateLanguageFile");
            this.m_btnCreateLanguageFile.BackgroundImage = null;
            this.m_btnCreateLanguageFile.Name = "m_btnCreateLanguageFile";
            this.m_btnCreateLanguageFile.UseVisualStyleBackColor = true;
            this.m_btnCreateLanguageFile.Click += new System.EventHandler(this.m_btnCreateLanguageFile_Click);
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
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_chkLanguageFileUnderGPL2
            // 
            this.m_chkLanguageFileUnderGPL2.AccessibleDescription = null;
            this.m_chkLanguageFileUnderGPL2.AccessibleName = null;
            resources.ApplyResources(this.m_chkLanguageFileUnderGPL2, "m_chkLanguageFileUnderGPL2");
            this.m_chkLanguageFileUnderGPL2.BackgroundImage = null;
            this.m_chkLanguageFileUnderGPL2.Checked = true;
            this.m_chkLanguageFileUnderGPL2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkLanguageFileUnderGPL2.Name = "m_chkLanguageFileUnderGPL2";
            this.m_chkLanguageFileUnderGPL2.UseVisualStyleBackColor = true;
            this.m_chkLanguageFileUnderGPL2.CheckedChanged += new System.EventHandler(this.m_chkGPL2_CheckedChanged);
            // 
            // m_chkLanguageFileModifiable
            // 
            this.m_chkLanguageFileModifiable.AccessibleDescription = null;
            this.m_chkLanguageFileModifiable.AccessibleName = null;
            resources.ApplyResources(this.m_chkLanguageFileModifiable, "m_chkLanguageFileModifiable");
            this.m_chkLanguageFileModifiable.BackgroundImage = null;
            this.m_chkLanguageFileModifiable.Checked = true;
            this.m_chkLanguageFileModifiable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkLanguageFileModifiable.Name = "m_chkLanguageFileModifiable";
            this.m_chkLanguageFileModifiable.UseVisualStyleBackColor = true;
            // 
            // m_chkFirstLanguageRTL
            // 
            this.m_chkFirstLanguageRTL.AccessibleDescription = null;
            this.m_chkFirstLanguageRTL.AccessibleName = null;
            resources.ApplyResources(this.m_chkFirstLanguageRTL, "m_chkFirstLanguageRTL");
            this.m_chkFirstLanguageRTL.BackgroundImage = null;
            this.m_chkFirstLanguageRTL.Name = "m_chkFirstLanguageRTL";
            this.m_chkFirstLanguageRTL.UseVisualStyleBackColor = true;
            this.m_chkFirstLanguageRTL.CheckedChanged += new System.EventHandler(this.m_chkFirstLanguageRTL_CheckedChanged);
            // 
            // m_chkSecondLanguageRTL
            // 
            this.m_chkSecondLanguageRTL.AccessibleDescription = null;
            this.m_chkSecondLanguageRTL.AccessibleName = null;
            resources.ApplyResources(this.m_chkSecondLanguageRTL, "m_chkSecondLanguageRTL");
            this.m_chkSecondLanguageRTL.BackgroundImage = null;
            this.m_chkSecondLanguageRTL.Name = "m_chkSecondLanguageRTL";
            this.m_chkSecondLanguageRTL.UseVisualStyleBackColor = true;
            this.m_chkSecondLanguageRTL.CheckedChanged += new System.EventHandler(this.m_chkSecondLanguageRTL_CheckedChanged);
            // 
            // NewLanguageFile
            // 
            this.AcceptButton = this.m_btnCreateLanguageFile;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.m_btnCancel;
            this.Controls.Add(this.m_chkSecondLanguageRTL);
            this.Controls.Add(this.m_chkFirstLanguageRTL);
            this.Controls.Add(this.m_chkLanguageFileModifiable);
            this.Controls.Add(this.m_chkLanguageFileUnderGPL2);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnCreateLanguageFile);
            this.Controls.Add(this.m_lblSecondLanguage);
            this.Controls.Add(this.m_tbxSecondLanguage);
            this.Controls.Add(this.m_tbxFirstLanguage);
            this.Controls.Add(this.m_lblFirstLanguage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewLanguageFile";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.OnHelpRequested);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblFirstLanguage;
        private System.Windows.Forms.Label m_lblSecondLanguage;
        private System.Windows.Forms.Button m_btnCreateLanguageFile;
        private System.Windows.Forms.Button m_btnCancel;
        public System.Windows.Forms.TextBox m_tbxFirstLanguage;
        public System.Windows.Forms.TextBox m_tbxSecondLanguage;
        public System.Windows.Forms.CheckBox m_chkLanguageFileUnderGPL2;
        public System.Windows.Forms.CheckBox m_chkLanguageFileModifiable;
        public System.Windows.Forms.CheckBox m_chkFirstLanguageRTL;
        public System.Windows.Forms.CheckBox m_chkSecondLanguageRTL;
    }
}