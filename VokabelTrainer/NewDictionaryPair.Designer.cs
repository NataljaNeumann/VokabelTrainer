﻿/*  Vokabel-Trainer aims to provide you a simple vocabulary training app
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDictionaryPair));
            this.m_lblFirstLanguage = new System.Windows.Forms.Label();
            this.m_tbxFirstLanguage = new System.Windows.Forms.TextBox();
            this.m_lblSecondLanguage = new System.Windows.Forms.Label();
            this.m_tbxSecondLanguage = new System.Windows.Forms.TextBox();
            this.m_btnEnterNext = new System.Windows.Forms.Button();
            this.m_btnEnteredLast = new System.Windows.Forms.Button();
            this.m_btnCancelEntering = new System.Windows.Forms.Button();
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
            this.m_tbxFirstLanguage.Leave += new System.EventHandler(this.textBoxFirstLanguage_Leave);
            this.m_tbxFirstLanguage.Enter += new System.EventHandler(this.OnTextBoxEntered);
            // 
            // m_lblSecondLanguage
            // 
            this.m_lblSecondLanguage.AccessibleDescription = null;
            this.m_lblSecondLanguage.AccessibleName = null;
            resources.ApplyResources(this.m_lblSecondLanguage, "m_lblSecondLanguage");
            this.m_lblSecondLanguage.Name = "m_lblSecondLanguage";
            // 
            // m_tbxSecondLanguage
            // 
            this.m_tbxSecondLanguage.AccessibleDescription = null;
            this.m_tbxSecondLanguage.AccessibleName = null;
            resources.ApplyResources(this.m_tbxSecondLanguage, "m_tbxSecondLanguage");
            this.m_tbxSecondLanguage.BackgroundImage = null;
            this.m_tbxSecondLanguage.Name = "m_tbxSecondLanguage";
            this.m_tbxSecondLanguage.TextChanged += new System.EventHandler(this.textBoxSecondLanguage_TextChanged);
            this.m_tbxSecondLanguage.Leave += new System.EventHandler(this.textBoxSecondLanguage_Leave);
            this.m_tbxSecondLanguage.Enter += new System.EventHandler(this.OnTextBoxEntered);
            // 
            // m_btnEnterNext
            // 
            this.m_btnEnterNext.AccessibleDescription = null;
            this.m_btnEnterNext.AccessibleName = null;
            resources.ApplyResources(this.m_btnEnterNext, "m_btnEnterNext");
            this.m_btnEnterNext.BackgroundImage = null;
            this.m_btnEnterNext.Name = "m_btnEnterNext";
            this.m_btnEnterNext.UseVisualStyleBackColor = true;
            this.m_btnEnterNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // m_btnEnteredLast
            // 
            this.m_btnEnteredLast.AccessibleDescription = null;
            this.m_btnEnteredLast.AccessibleName = null;
            resources.ApplyResources(this.m_btnEnteredLast, "m_btnEnteredLast");
            this.m_btnEnteredLast.BackgroundImage = null;
            this.m_btnEnteredLast.Name = "m_btnEnteredLast";
            this.m_btnEnteredLast.UseVisualStyleBackColor = true;
            this.m_btnEnteredLast.Click += new System.EventHandler(this.m_btnEnteredLast_Click);
            // 
            // m_btnCancelEntering
            // 
            this.m_btnCancelEntering.AccessibleDescription = null;
            this.m_btnCancelEntering.AccessibleName = null;
            resources.ApplyResources(this.m_btnCancelEntering, "m_btnCancelEntering");
            this.m_btnCancelEntering.BackgroundImage = null;
            this.m_btnCancelEntering.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancelEntering.Name = "m_btnCancelEntering";
            this.m_btnCancelEntering.UseVisualStyleBackColor = true;
            this.m_btnCancelEntering.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // NewDictionaryPair
            // 
            this.AcceptButton = this.m_btnEnterNext;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.m_btnCancelEntering;
            this.Controls.Add(this.m_btnCancelEntering);
            this.Controls.Add(this.m_btnEnteredLast);
            this.Controls.Add(this.m_btnEnterNext);
            this.Controls.Add(this.m_tbxSecondLanguage);
            this.Controls.Add(this.m_lblSecondLanguage);
            this.Controls.Add(this.m_tbxFirstLanguage);
            this.Controls.Add(this.m_lblFirstLanguage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewDictionaryPair";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Shown += new System.EventHandler(this.NewDictionaryPair_Shown);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.OnHelpRequested);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label m_lblFirstLanguage;
        public System.Windows.Forms.TextBox m_tbxFirstLanguage;
        public System.Windows.Forms.Label m_lblSecondLanguage;
        public System.Windows.Forms.TextBox m_tbxSecondLanguage;
        private System.Windows.Forms.Button m_btnEnterNext;
        private System.Windows.Forms.Button m_btnEnteredLast;
        private System.Windows.Forms.Button m_btnCancelEntering;
    }
}