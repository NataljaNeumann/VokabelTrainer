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
            this.m_lblFirstLanguage.AutoSize = true;
            this.m_lblFirstLanguage.Location = new System.Drawing.Point(16, 11);
            this.m_lblFirstLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblFirstLanguage.Name = "m_lblFirstLanguage";
            this.m_lblFirstLanguage.Size = new System.Drawing.Size(96, 16);
            this.m_lblFirstLanguage.TabIndex = 0;
            this.m_lblFirstLanguage.Text = "Erste Sprache:";
            // 
            // m_tbxFirstLanguage
            // 
            this.m_tbxFirstLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbxFirstLanguage.Location = new System.Drawing.Point(20, 31);
            this.m_tbxFirstLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.m_tbxFirstLanguage.Name = "m_tbxFirstLanguage";
            this.m_tbxFirstLanguage.Size = new System.Drawing.Size(481, 22);
            this.m_tbxFirstLanguage.TabIndex = 1;
            this.m_tbxFirstLanguage.TextChanged += new System.EventHandler(this.textBoxFirstLanguage_TextChanged);
            // 
            // m_tbxSecondLanguage
            // 
            this.m_tbxSecondLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbxSecondLanguage.Location = new System.Drawing.Point(20, 95);
            this.m_tbxSecondLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.m_tbxSecondLanguage.Name = "m_tbxSecondLanguage";
            this.m_tbxSecondLanguage.Size = new System.Drawing.Size(481, 22);
            this.m_tbxSecondLanguage.TabIndex = 3;
            this.m_tbxSecondLanguage.Text = "Deutsch";
            this.m_tbxSecondLanguage.TextChanged += new System.EventHandler(this.textBoxSecondLanguage_TextChanged);
            // 
            // m_lblSecondLanguage
            // 
            this.m_lblSecondLanguage.AutoSize = true;
            this.m_lblSecondLanguage.Location = new System.Drawing.Point(17, 71);
            this.m_lblSecondLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblSecondLanguage.Name = "m_lblSecondLanguage";
            this.m_lblSecondLanguage.Size = new System.Drawing.Size(104, 16);
            this.m_lblSecondLanguage.TabIndex = 2;
            this.m_lblSecondLanguage.Text = "Zweite Sprache:";
            // 
            // m_btnCreateLanguageFile
            // 
            this.m_btnCreateLanguageFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCreateLanguageFile.Enabled = false;
            this.m_btnCreateLanguageFile.Location = new System.Drawing.Point(295, 177);
            this.m_btnCreateLanguageFile.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnCreateLanguageFile.Name = "m_btnCreateLanguageFile";
            this.m_btnCreateLanguageFile.Size = new System.Drawing.Size(100, 28);
            this.m_btnCreateLanguageFile.TabIndex = 6;
            this.m_btnCreateLanguageFile.Text = "Erstellen";
            this.m_btnCreateLanguageFile.UseVisualStyleBackColor = true;
            this.m_btnCreateLanguageFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(403, 177);
            this.m_btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(100, 28);
            this.m_btnCancel.TabIndex = 7;
            this.m_btnCancel.Text = "Abbrechen";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_chkLanguageFileUnderGPL2
            // 
            this.m_chkLanguageFileUnderGPL2.AutoSize = true;
            this.m_chkLanguageFileUnderGPL2.Checked = true;
            this.m_chkLanguageFileUnderGPL2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkLanguageFileUnderGPL2.Location = new System.Drawing.Point(19, 136);
            this.m_chkLanguageFileUnderGPL2.Name = "m_chkLanguageFileUnderGPL2";
            this.m_chkLanguageFileUnderGPL2.Size = new System.Drawing.Size(251, 20);
            this.m_chkLanguageFileUnderGPL2.TabIndex = 4;
            this.m_chkLanguageFileUnderGPL2.Text = "Sprachdatei unter GPL2-Lizenz stellen";
            this.m_chkLanguageFileUnderGPL2.UseVisualStyleBackColor = true;
            this.m_chkLanguageFileUnderGPL2.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // m_chkLanguageFileModifiable
            // 
            this.m_chkLanguageFileModifiable.AutoSize = true;
            this.m_chkLanguageFileModifiable.Checked = true;
            this.m_chkLanguageFileModifiable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkLanguageFileModifiable.Enabled = false;
            this.m_chkLanguageFileModifiable.Location = new System.Drawing.Point(19, 162);
            this.m_chkLanguageFileModifiable.Name = "m_chkLanguageFileModifiable";
            this.m_chkLanguageFileModifiable.Size = new System.Drawing.Size(180, 20);
            this.m_chkLanguageFileModifiable.TabIndex = 5;
            this.m_chkLanguageFileModifiable.Text = "Sprachdatei modifizierbar";
            this.m_chkLanguageFileModifiable.UseVisualStyleBackColor = true;
            // 
            // m_chkFirstLanguageRTL
            // 
            this.m_chkFirstLanguageRTL.AutoSize = true;
            this.m_chkFirstLanguageRTL.Location = new System.Drawing.Point(193, 10);
            this.m_chkFirstLanguageRTL.Name = "m_chkFirstLanguageRTL";
            this.m_chkFirstLanguageRTL.Size = new System.Drawing.Size(209, 20);
            this.m_chkFirstLanguageRTL.TabIndex = 8;
            this.m_chkFirstLanguageRTL.Text = "Schreibweise rechts nach links";
            this.m_chkFirstLanguageRTL.UseVisualStyleBackColor = true;
            this.m_chkFirstLanguageRTL.Visible = false;
            // 
            // m_chkSecondLanguageRTL
            // 
            this.m_chkSecondLanguageRTL.AutoSize = true;
            this.m_chkSecondLanguageRTL.Location = new System.Drawing.Point(193, 71);
            this.m_chkSecondLanguageRTL.Name = "m_chkSecondLanguageRTL";
            this.m_chkSecondLanguageRTL.Size = new System.Drawing.Size(209, 20);
            this.m_chkSecondLanguageRTL.TabIndex = 9;
            this.m_chkSecondLanguageRTL.Text = "Schreibweise rechts nach links";
            this.m_chkSecondLanguageRTL.UseVisualStyleBackColor = true;
            this.m_chkSecondLanguageRTL.Visible = false;
            // 
            // NewLanguageFile
            // 
            this.AcceptButton = this.m_btnCreateLanguageFile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(519, 220);
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
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewLanguageFile";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Neue Sprachdatei";
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
        private System.Windows.Forms.CheckBox m_chkFirstLanguageRTL;
        private System.Windows.Forms.CheckBox m_chkSecondLanguageRTL;
    }
}