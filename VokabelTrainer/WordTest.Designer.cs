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
            this.m_lblShownText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblShownText.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblShownText.Location = new System.Drawing.Point(12, 9);
            this.m_lblShownText.Name = "m_lblShownText";
            this.m_lblShownText.Size = new System.Drawing.Size(681, 88);
            this.m_lblShownText.TabIndex = 0;
            this.m_lblShownText.Text = "...";
            this.m_lblShownText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblAskedTranslation
            // 
            this.m_lblAskedTranslation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAskedTranslation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblAskedTranslation.Location = new System.Drawing.Point(12, 101);
            this.m_lblAskedTranslation.Name = "m_lblAskedTranslation";
            this.m_lblAskedTranslation.Size = new System.Drawing.Size(681, 23);
            this.m_lblAskedTranslation.TabIndex = 1;
            this.m_lblAskedTranslation.Text = "...";
            this.m_lblAskedTranslation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_tbxAskedTranslation
            // 
            this.m_tbxAskedTranslation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbxAskedTranslation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_tbxAskedTranslation.Location = new System.Drawing.Point(18, 128);
            this.m_tbxAskedTranslation.Name = "m_tbxAskedTranslation";
            this.m_tbxAskedTranslation.Size = new System.Drawing.Size(675, 26);
            this.m_tbxAskedTranslation.TabIndex = 2;
            this.m_tbxAskedTranslation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_tbxAskedTranslation.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // m_btnNext
            // 
            this.m_btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnNext.Enabled = false;
            this.m_btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnNext.Location = new System.Drawing.Point(361, 171);
            this.m_btnNext.Name = "m_btnNext";
            this.m_btnNext.Size = new System.Drawing.Size(97, 29);
            this.m_btnNext.TabIndex = 3;
            this.m_btnNext.Text = "Nächste";
            this.m_btnNext.UseVisualStyleBackColor = true;
            this.m_btnNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // m_btnLast
            // 
            this.m_btnLast.Enabled = false;
            this.m_btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnLast.Location = new System.Drawing.Point(477, 171);
            this.m_btnLast.Name = "m_btnLast";
            this.m_btnLast.Size = new System.Drawing.Size(97, 29);
            this.m_btnLast.TabIndex = 4;
            this.m_btnLast.Text = "Letzte";
            this.m_btnLast.UseVisualStyleBackColor = true;
            this.m_btnLast.Click += new System.EventHandler(this.buttonLast_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnCancel.Location = new System.Drawing.Point(595, 171);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(98, 29);
            this.m_btnCancel.TabIndex = 5;
            this.m_btnCancel.Text = "Abbrechen";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // WordTest
            // 
            this.AcceptButton = this.m_btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(705, 212);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Training";
            this.Shown += new System.EventHandler(this.WordTest_Shown);
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