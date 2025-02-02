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
    partial class NewMessageBox
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
            this.m_lblMessageText = new System.Windows.Forms.Label();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lblMessageText
            // 
            this.m_lblMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblMessageText.Location = new System.Drawing.Point(16, 11);
            this.m_lblMessageText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblMessageText.Name = "m_lblMessageText";
            this.m_lblMessageText.Size = new System.Drawing.Size(382, 87);
            this.m_lblMessageText.TabIndex = 0;
            this.m_lblMessageText.Text = "label1";
            this.m_lblMessageText.SizeChanged += new System.EventHandler(this.label1_SizeChanged);
            // 
            // m_btnOK
            // 
            this.m_btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOK.Location = new System.Drawing.Point(295, 119);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(103, 26);
            this.m_btnOK.TabIndex = 1;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // NewMessageBox
            // 
            this.AcceptButton = this.m_btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 157);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_lblMessageText);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NewMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vokabel-Trainer";
            this.Shown += new System.EventHandler(this.NewMessageBox_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblMessageText;
        private System.Windows.Forms.Button m_btnOK;
    }
}