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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMessageBox));
            this.m_lblMessageText = new System.Windows.Forms.Label();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnNo = new System.Windows.Forms.Button();
            this.m_btnYes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lblMessageText
            // 
            resources.ApplyResources(this.m_lblMessageText, "m_lblMessageText");
            this.m_lblMessageText.Name = "m_lblMessageText";
            this.m_lblMessageText.SizeChanged += new System.EventHandler(this.label1_SizeChanged);
            // 
            // m_btnOK
            // 
            resources.ApplyResources(this.m_btnOK, "m_btnOK");
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            resources.ApplyResources(this.m_btnCancel, "m_btnCancel");
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_btnNo
            // 
            resources.ApplyResources(this.m_btnNo, "m_btnNo");
            this.m_btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.m_btnNo.Name = "m_btnNo";
            this.m_btnNo.UseVisualStyleBackColor = true;
            this.m_btnNo.Click += new System.EventHandler(this.m_btnNo_Click);
            // 
            // m_btnYes
            // 
            resources.ApplyResources(this.m_btnYes, "m_btnYes");
            this.m_btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_btnYes.Name = "m_btnYes";
            this.m_btnYes.UseVisualStyleBackColor = true;
            this.m_btnYes.Click += new System.EventHandler(this.m_btnYes_Click);
            // 
            // NewMessageBox
            // 
            this.AcceptButton = this.m_btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.Controls.Add(this.m_btnYes);
            this.Controls.Add(this.m_btnNo);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_lblMessageText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewMessageBox";
            this.Shown += new System.EventHandler(this.NewMessageBox_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblMessageText;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Button m_btnNo;
        private System.Windows.Forms.Button m_btnYes;
    }
}