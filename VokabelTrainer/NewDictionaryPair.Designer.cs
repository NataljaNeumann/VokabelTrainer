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
    partial class NewDictionaryPair
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
            this.m_lblSecondLanguage = new System.Windows.Forms.Label();
            this.textBoxSecondLanguage = new System.Windows.Forms.TextBox();
            this.m_btnEnterNext = new System.Windows.Forms.Button();
            this.m_btnEnteredLast = new System.Windows.Forms.Button();
            this.m_btnCancelEntering = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lblFirstLanguage
            // 
            this.m_lblFirstLanguage.AutoSize = true;
            this.m_lblFirstLanguage.Location = new System.Drawing.Point(16, 33);
            this.m_lblFirstLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblFirstLanguage.Name = "m_lblFirstLanguage";
            this.m_lblFirstLanguage.Size = new System.Drawing.Size(93, 16);
            this.m_lblFirstLanguage.TabIndex = 0;
            this.m_lblFirstLanguage.Text = "First language";
            // 
            // m_tbxFirstLanguage
            // 
            this.m_tbxFirstLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbxFirstLanguage.Location = new System.Drawing.Point(20, 53);
            this.m_tbxFirstLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.m_tbxFirstLanguage.Name = "m_tbxFirstLanguage";
            this.m_tbxFirstLanguage.Size = new System.Drawing.Size(560, 22);
            this.m_tbxFirstLanguage.TabIndex = 1;
            this.m_tbxFirstLanguage.Leave += new System.EventHandler(this.textBoxFirstLanguage_Leave);
            // 
            // m_lblSecondLanguage
            // 
            this.m_lblSecondLanguage.AutoSize = true;
            this.m_lblSecondLanguage.Location = new System.Drawing.Point(20, 103);
            this.m_lblSecondLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblSecondLanguage.Name = "m_lblSecondLanguage";
            this.m_lblSecondLanguage.Size = new System.Drawing.Size(115, 16);
            this.m_lblSecondLanguage.TabIndex = 2;
            this.m_lblSecondLanguage.Text = "Second language";
            // 
            // textBoxSecondLanguage
            // 
            this.textBoxSecondLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSecondLanguage.Location = new System.Drawing.Point(24, 124);
            this.textBoxSecondLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSecondLanguage.Name = "textBoxSecondLanguage";
            this.textBoxSecondLanguage.Size = new System.Drawing.Size(556, 22);
            this.textBoxSecondLanguage.TabIndex = 3;
            this.textBoxSecondLanguage.TextChanged += new System.EventHandler(this.textBoxSecondLanguage_TextChanged);
            this.textBoxSecondLanguage.Leave += new System.EventHandler(this.textBoxSecondLanguage_Leave);
            // 
            // m_btnEnterNext
            // 
            this.m_btnEnterNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnEnterNext.Location = new System.Drawing.Point(263, 198);
            this.m_btnEnterNext.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnEnterNext.MaximumSize = new System.Drawing.Size(100, 28);
            this.m_btnEnterNext.MinimumSize = new System.Drawing.Size(100, 28);
            this.m_btnEnterNext.Name = "m_btnEnterNext";
            this.m_btnEnterNext.Size = new System.Drawing.Size(100, 28);
            this.m_btnEnterNext.TabIndex = 4;
            this.m_btnEnterNext.Text = "Nächste";
            this.m_btnEnterNext.UseVisualStyleBackColor = true;
            this.m_btnEnterNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // m_btnEnteredLast
            // 
            this.m_btnEnteredLast.Location = new System.Drawing.Point(373, 198);
            this.m_btnEnteredLast.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnEnteredLast.Name = "m_btnEnteredLast";
            this.m_btnEnteredLast.Size = new System.Drawing.Size(100, 28);
            this.m_btnEnteredLast.TabIndex = 5;
            this.m_btnEnteredLast.Text = "Letzte";
            this.m_btnEnteredLast.UseVisualStyleBackColor = true;
            this.m_btnEnteredLast.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_btnCancelEntering
            // 
            this.m_btnCancelEntering.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancelEntering.Location = new System.Drawing.Point(481, 198);
            this.m_btnCancelEntering.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnCancelEntering.Name = "m_btnCancelEntering";
            this.m_btnCancelEntering.Size = new System.Drawing.Size(100, 28);
            this.m_btnCancelEntering.TabIndex = 6;
            this.m_btnCancelEntering.Text = "Abbrechen";
            this.m_btnCancelEntering.UseVisualStyleBackColor = true;
            this.m_btnCancelEntering.Click += new System.EventHandler(this.button2_Click);
            // 
            // NewDictionaryPair
            // 
            this.AcceptButton = this.m_btnEnterNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancelEntering;
            this.ClientSize = new System.Drawing.Size(597, 241);
            this.Controls.Add(this.m_btnCancelEntering);
            this.Controls.Add(this.m_btnEnteredLast);
            this.Controls.Add(this.m_btnEnterNext);
            this.Controls.Add(this.textBoxSecondLanguage);
            this.Controls.Add(this.m_lblSecondLanguage);
            this.Controls.Add(this.m_tbxFirstLanguage);
            this.Controls.Add(this.m_lblFirstLanguage);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewDictionaryPair";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vokabeln eingeben";
            this.Shown += new System.EventHandler(this.NewDictionaryPair_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label m_lblFirstLanguage;
        public System.Windows.Forms.TextBox m_tbxFirstLanguage;
        public System.Windows.Forms.Label m_lblSecondLanguage;
        public System.Windows.Forms.TextBox textBoxSecondLanguage;
        private System.Windows.Forms.Button m_btnEnterNext;
        private System.Windows.Forms.Button m_btnEnteredLast;
        private System.Windows.Forms.Button m_btnCancelEntering;
    }
}