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
            this.labelFirstLanguage = new System.Windows.Forms.Label();
            this.textBoxFirstLanguage = new System.Windows.Forms.TextBox();
            this.labelSecondLanguage = new System.Windows.Forms.Label();
            this.textBoxSecondLanguage = new System.Windows.Forms.TextBox();
            this.buttonNext = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelFirstLanguage
            // 
            this.labelFirstLanguage.AutoSize = true;
            this.labelFirstLanguage.Location = new System.Drawing.Point(16, 33);
            this.labelFirstLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFirstLanguage.Name = "labelFirstLanguage";
            this.labelFirstLanguage.Size = new System.Drawing.Size(45, 16);
            this.labelFirstLanguage.TabIndex = 0;
            this.labelFirstLanguage.Text = "label1";
            // 
            // textBoxFirstLanguage
            // 
            this.textBoxFirstLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFirstLanguage.Location = new System.Drawing.Point(20, 53);
            this.textBoxFirstLanguage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxFirstLanguage.Name = "textBoxFirstLanguage";
            this.textBoxFirstLanguage.Size = new System.Drawing.Size(560, 22);
            this.textBoxFirstLanguage.TabIndex = 1;
            this.textBoxFirstLanguage.Leave += new System.EventHandler(this.textBoxFirstLanguage_Leave);
            // 
            // labelSecondLanguage
            // 
            this.labelSecondLanguage.AutoSize = true;
            this.labelSecondLanguage.Location = new System.Drawing.Point(20, 103);
            this.labelSecondLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSecondLanguage.Name = "labelSecondLanguage";
            this.labelSecondLanguage.Size = new System.Drawing.Size(45, 16);
            this.labelSecondLanguage.TabIndex = 2;
            this.labelSecondLanguage.Text = "label2";
            // 
            // textBoxSecondLanguage
            // 
            this.textBoxSecondLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSecondLanguage.Location = new System.Drawing.Point(24, 124);
            this.textBoxSecondLanguage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSecondLanguage.Name = "textBoxSecondLanguage";
            this.textBoxSecondLanguage.Size = new System.Drawing.Size(556, 22);
            this.textBoxSecondLanguage.TabIndex = 3;
            this.textBoxSecondLanguage.TextChanged += new System.EventHandler(this.textBoxSecondLanguage_TextChanged);
            this.textBoxSecondLanguage.Leave += new System.EventHandler(this.textBoxSecondLanguage_Leave);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Location = new System.Drawing.Point(263, 198);
            this.buttonNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonNext.MaximumSize = new System.Drawing.Size(100, 28);
            this.buttonNext.MinimumSize = new System.Drawing.Size(100, 28);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(100, 28);
            this.buttonNext.TabIndex = 4;
            this.buttonNext.Text = "Nächste";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(373, 198);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "Letzte";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(481, 198);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 6;
            this.button2.Text = "Abbrechen";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // NewDictionaryPair
            // 
            this.AcceptButton = this.buttonNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(597, 241);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.textBoxSecondLanguage);
            this.Controls.Add(this.labelSecondLanguage);
            this.Controls.Add(this.textBoxFirstLanguage);
            this.Controls.Add(this.labelFirstLanguage);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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

        public System.Windows.Forms.Label labelFirstLanguage;
        public System.Windows.Forms.TextBox textBoxFirstLanguage;
        public System.Windows.Forms.Label labelSecondLanguage;
        public System.Windows.Forms.TextBox textBoxSecondLanguage;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}