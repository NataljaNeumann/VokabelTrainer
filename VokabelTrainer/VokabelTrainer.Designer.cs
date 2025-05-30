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
    partial class VokabelTrainer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VokabelTrainer));
            this.m_btnLoadLanguageFile = new System.Windows.Forms.Button();
            this.m_btnEnterVocabulary = new System.Windows.Forms.Button();
            this.m_btnExerciseSecondToFirst = new System.Windows.Forms.Button();
            this.m_btnExerciseFirstToSecond = new System.Windows.Forms.Button();
            this.m_btnNewLanguageFile = new System.Windows.Forms.Button();
            this.m_dlgOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_btnIntensiveSecondToFirst = new System.Windows.Forms.Button();
            this.m_btnIntensiveFirstToSecond = new System.Windows.Forms.Button();
            this.m_btnMostIntensiveSecondToFirst = new System.Windows.Forms.Button();
            this.m_btnMostIntensiveFirstToSecond = new System.Windows.Forms.Button();
            this.m_lblShowLicense = new System.Windows.Forms.LinkLabel();
            this.m_lblShowAbout = new System.Windows.Forms.LinkLabel();
            this.m_btnShowDesktopKeyboard = new System.Windows.Forms.Button();
            this.m_lblReader = new System.Windows.Forms.Label();
            this.m_cbxReader = new System.Windows.Forms.ComboBox();
            this.m_lblDownloadESpeak = new System.Windows.Forms.LinkLabel();
            this.m_chkUseESpeak = new System.Windows.Forms.CheckBox();
            this.m_tbxESpeakPath = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.m_btnSearchESpeak = new System.Windows.Forms.Button();
            this.m_lblDontLearnAquire = new System.Windows.Forms.LinkLabel();
            this.m_btnOsLanguageAndKeyboardSettings = new System.Windows.Forms.Button();
            this.m_btnStats = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnLoadLanguageFile
            // 
            this.m_btnLoadLanguageFile.AccessibleDescription = null;
            this.m_btnLoadLanguageFile.AccessibleName = null;
            resources.ApplyResources(this.m_btnLoadLanguageFile, "m_btnLoadLanguageFile");
            this.m_btnLoadLanguageFile.BackgroundImage = null;
            this.m_btnLoadLanguageFile.Name = "m_btnLoadLanguageFile";
            this.m_btnLoadLanguageFile.UseVisualStyleBackColor = true;
            this.m_btnLoadLanguageFile.Click += new System.EventHandler(this.OnLoadLanguageFileClick);
            // 
            // m_btnEnterVocabulary
            // 
            this.m_btnEnterVocabulary.AccessibleDescription = null;
            this.m_btnEnterVocabulary.AccessibleName = null;
            resources.ApplyResources(this.m_btnEnterVocabulary, "m_btnEnterVocabulary");
            this.m_btnEnterVocabulary.BackgroundImage = null;
            this.m_btnEnterVocabulary.Name = "m_btnEnterVocabulary";
            this.m_btnEnterVocabulary.UseVisualStyleBackColor = true;
            this.m_btnEnterVocabulary.Click += new System.EventHandler(this.m_btnEnterVocabulary_Click);
            // 
            // m_btnExerciseSecondToFirst
            // 
            this.m_btnExerciseSecondToFirst.AccessibleDescription = null;
            this.m_btnExerciseSecondToFirst.AccessibleName = null;
            resources.ApplyResources(this.m_btnExerciseSecondToFirst, "m_btnExerciseSecondToFirst");
            this.m_btnExerciseSecondToFirst.BackgroundImage = null;
            this.m_btnExerciseSecondToFirst.Name = "m_btnExerciseSecondToFirst";
            this.m_btnExerciseSecondToFirst.UseVisualStyleBackColor = true;
            this.m_btnExerciseSecondToFirst.Click += new System.EventHandler(this.OnExerciseSecondToFirstClick);
            // 
            // m_btnExerciseFirstToSecond
            // 
            this.m_btnExerciseFirstToSecond.AccessibleDescription = null;
            this.m_btnExerciseFirstToSecond.AccessibleName = null;
            resources.ApplyResources(this.m_btnExerciseFirstToSecond, "m_btnExerciseFirstToSecond");
            this.m_btnExerciseFirstToSecond.BackgroundImage = null;
            this.m_btnExerciseFirstToSecond.Name = "m_btnExerciseFirstToSecond";
            this.m_btnExerciseFirstToSecond.UseVisualStyleBackColor = true;
            this.m_btnExerciseFirstToSecond.Click += new System.EventHandler(this.OnExerciseFirstToSecondClick);
            // 
            // m_btnNewLanguageFile
            // 
            this.m_btnNewLanguageFile.AccessibleDescription = null;
            this.m_btnNewLanguageFile.AccessibleName = null;
            resources.ApplyResources(this.m_btnNewLanguageFile, "m_btnNewLanguageFile");
            this.m_btnNewLanguageFile.BackgroundImage = null;
            this.m_btnNewLanguageFile.Name = "m_btnNewLanguageFile";
            this.m_btnNewLanguageFile.UseVisualStyleBackColor = true;
            this.m_btnNewLanguageFile.Click += new System.EventHandler(this.OnNewLanguageClick);
            // 
            // m_dlgOpenFileDialog
            // 
            this.m_dlgOpenFileDialog.DefaultExt = "Vocabulary.xml";
            resources.ApplyResources(this.m_dlgOpenFileDialog, "m_dlgOpenFileDialog");
            // 
            // m_btnIntensiveSecondToFirst
            // 
            this.m_btnIntensiveSecondToFirst.AccessibleDescription = null;
            this.m_btnIntensiveSecondToFirst.AccessibleName = null;
            resources.ApplyResources(this.m_btnIntensiveSecondToFirst, "m_btnIntensiveSecondToFirst");
            this.m_btnIntensiveSecondToFirst.BackgroundImage = null;
            this.m_btnIntensiveSecondToFirst.Name = "m_btnIntensiveSecondToFirst";
            this.m_btnIntensiveSecondToFirst.UseVisualStyleBackColor = true;
            this.m_btnIntensiveSecondToFirst.Click += new System.EventHandler(this.OnIntensiveSecondToFirstClick);
            // 
            // m_btnIntensiveFirstToSecond
            // 
            this.m_btnIntensiveFirstToSecond.AccessibleDescription = null;
            this.m_btnIntensiveFirstToSecond.AccessibleName = null;
            resources.ApplyResources(this.m_btnIntensiveFirstToSecond, "m_btnIntensiveFirstToSecond");
            this.m_btnIntensiveFirstToSecond.BackgroundImage = null;
            this.m_btnIntensiveFirstToSecond.Name = "m_btnIntensiveFirstToSecond";
            this.m_btnIntensiveFirstToSecond.UseVisualStyleBackColor = true;
            this.m_btnIntensiveFirstToSecond.Click += new System.EventHandler(this.OnIntensiveFirstToSecondClick);
            // 
            // m_btnMostIntensiveSecondToFirst
            // 
            this.m_btnMostIntensiveSecondToFirst.AccessibleDescription = null;
            this.m_btnMostIntensiveSecondToFirst.AccessibleName = null;
            resources.ApplyResources(this.m_btnMostIntensiveSecondToFirst, "m_btnMostIntensiveSecondToFirst");
            this.m_btnMostIntensiveSecondToFirst.BackgroundImage = null;
            this.m_btnMostIntensiveSecondToFirst.Name = "m_btnMostIntensiveSecondToFirst";
            this.m_btnMostIntensiveSecondToFirst.UseVisualStyleBackColor = true;
            this.m_btnMostIntensiveSecondToFirst.Click += new System.EventHandler(this.OnMostIntensiveSecondToFirstClick);
            // 
            // m_btnMostIntensiveFirstToSecond
            // 
            this.m_btnMostIntensiveFirstToSecond.AccessibleDescription = null;
            this.m_btnMostIntensiveFirstToSecond.AccessibleName = null;
            resources.ApplyResources(this.m_btnMostIntensiveFirstToSecond, "m_btnMostIntensiveFirstToSecond");
            this.m_btnMostIntensiveFirstToSecond.BackgroundImage = null;
            this.m_btnMostIntensiveFirstToSecond.Name = "m_btnMostIntensiveFirstToSecond";
            this.m_btnMostIntensiveFirstToSecond.UseVisualStyleBackColor = true;
            this.m_btnMostIntensiveFirstToSecond.Click += new System.EventHandler(this.OnMostIntensiveFirstToSecondClick);
            // 
            // m_lblShowLicense
            // 
            this.m_lblShowLicense.AccessibleDescription = null;
            this.m_lblShowLicense.AccessibleName = null;
            resources.ApplyResources(this.m_lblShowLicense, "m_lblShowLicense");
            this.m_lblShowLicense.Name = "m_lblShowLicense";
            this.m_lblShowLicense.TabStop = true;
            this.m_lblShowLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lblShowLicence_LinkClicked);
            // 
            // m_lblShowAbout
            // 
            this.m_lblShowAbout.AccessibleDescription = null;
            this.m_lblShowAbout.AccessibleName = null;
            resources.ApplyResources(this.m_lblShowAbout, "m_lblShowAbout");
            this.m_lblShowAbout.Name = "m_lblShowAbout";
            this.m_lblShowAbout.TabStop = true;
            this.m_lblShowAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lblShowAbout_LinkClicked);
            // 
            // m_btnShowDesktopKeyboard
            // 
            this.m_btnShowDesktopKeyboard.AccessibleDescription = null;
            this.m_btnShowDesktopKeyboard.AccessibleName = null;
            resources.ApplyResources(this.m_btnShowDesktopKeyboard, "m_btnShowDesktopKeyboard");
            this.m_btnShowDesktopKeyboard.BackgroundImage = null;
            this.m_btnShowDesktopKeyboard.Name = "m_btnShowDesktopKeyboard";
            this.m_btnShowDesktopKeyboard.UseVisualStyleBackColor = true;
            this.m_btnShowDesktopKeyboard.Click += new System.EventHandler(this.m_btnShowDesktopKeyboard_Click);
            // 
            // m_lblReader
            // 
            this.m_lblReader.AccessibleDescription = null;
            this.m_lblReader.AccessibleName = null;
            resources.ApplyResources(this.m_lblReader, "m_lblReader");
            this.m_lblReader.Name = "m_lblReader";
            // 
            // m_cbxReader
            // 
            this.m_cbxReader.AccessibleDescription = null;
            this.m_cbxReader.AccessibleName = null;
            resources.ApplyResources(this.m_cbxReader, "m_cbxReader");
            this.m_cbxReader.BackgroundImage = null;
            this.m_cbxReader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbxReader.FormattingEnabled = true;
            this.m_cbxReader.Items.AddRange(new object[] {
            resources.GetString("m_cbxReader.Items"),
            resources.GetString("m_cbxReader.Items1"),
            resources.GetString("m_cbxReader.Items2"),
            resources.GetString("m_cbxReader.Items3"),
            resources.GetString("m_cbxReader.Items4")});
            this.m_cbxReader.Name = "m_cbxReader";
            // 
            // m_lblDownloadESpeak
            // 
            this.m_lblDownloadESpeak.AccessibleDescription = null;
            this.m_lblDownloadESpeak.AccessibleName = null;
            resources.ApplyResources(this.m_lblDownloadESpeak, "m_lblDownloadESpeak");
            this.m_lblDownloadESpeak.Name = "m_lblDownloadESpeak";
            this.m_lblDownloadESpeak.TabStop = true;
            this.m_lblDownloadESpeak.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lblDownloadESpeak_LinkClicked);
            // 
            // m_chkUseESpeak
            // 
            this.m_chkUseESpeak.AccessibleDescription = null;
            this.m_chkUseESpeak.AccessibleName = null;
            resources.ApplyResources(this.m_chkUseESpeak, "m_chkUseESpeak");
            this.m_chkUseESpeak.BackgroundImage = null;
            this.m_chkUseESpeak.Name = "m_chkUseESpeak";
            this.m_chkUseESpeak.UseVisualStyleBackColor = true;
            this.m_chkUseESpeak.CheckedChanged += new System.EventHandler(this.m_chkUseESpeak_CheckedChanged);
            // 
            // m_tbxESpeakPath
            // 
            this.m_tbxESpeakPath.AccessibleDescription = null;
            this.m_tbxESpeakPath.AccessibleName = null;
            resources.ApplyResources(this.m_tbxESpeakPath, "m_tbxESpeakPath");
            this.m_tbxESpeakPath.BackgroundImage = null;
            this.m_tbxESpeakPath.Name = "m_tbxESpeakPath";
            // 
            // m_btnSearchESpeak
            // 
            this.m_btnSearchESpeak.AccessibleDescription = null;
            this.m_btnSearchESpeak.AccessibleName = null;
            resources.ApplyResources(this.m_btnSearchESpeak, "m_btnSearchESpeak");
            this.m_btnSearchESpeak.BackgroundImage = null;
            this.m_btnSearchESpeak.Font = null;
            this.m_btnSearchESpeak.Name = "m_btnSearchESpeak";
            this.m_btnSearchESpeak.UseVisualStyleBackColor = true;
            this.m_btnSearchESpeak.Click += new System.EventHandler(this.m_btnSearchESpeak_Click);
            // 
            // m_lblDontLearnAquire
            // 
            this.m_lblDontLearnAquire.AccessibleDescription = null;
            this.m_lblDontLearnAquire.AccessibleName = null;
            resources.ApplyResources(this.m_lblDontLearnAquire, "m_lblDontLearnAquire");
            this.m_lblDontLearnAquire.Name = "m_lblDontLearnAquire";
            this.m_lblDontLearnAquire.TabStop = true;
            this.m_lblDontLearnAquire.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lblDontLearnAquire_LinkClicked);
            // 
            // m_btnOsLanguageAndKeyboardSettings
            // 
            this.m_btnOsLanguageAndKeyboardSettings.AccessibleDescription = null;
            this.m_btnOsLanguageAndKeyboardSettings.AccessibleName = null;
            resources.ApplyResources(this.m_btnOsLanguageAndKeyboardSettings, "m_btnOsLanguageAndKeyboardSettings");
            this.m_btnOsLanguageAndKeyboardSettings.BackgroundImage = null;
            this.m_btnOsLanguageAndKeyboardSettings.FlatAppearance.BorderSize = 0;
            this.m_btnOsLanguageAndKeyboardSettings.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.m_btnOsLanguageAndKeyboardSettings.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.m_btnOsLanguageAndKeyboardSettings.ForeColor = System.Drawing.Color.Blue;
            this.m_btnOsLanguageAndKeyboardSettings.Name = "m_btnOsLanguageAndKeyboardSettings";
            this.m_btnOsLanguageAndKeyboardSettings.UseVisualStyleBackColor = false;
            this.m_btnOsLanguageAndKeyboardSettings.Click += new System.EventHandler(this.m_btnOsLanguageAndKeyboardSettings_Click);
            // 
            // m_btnStats
            // 
            this.m_btnStats.AccessibleDescription = null;
            this.m_btnStats.AccessibleName = null;
            resources.ApplyResources(this.m_btnStats, "m_btnStats");
            this.m_btnStats.BackgroundImage = null;
            this.m_btnStats.FlatAppearance.BorderSize = 0;
            this.m_btnStats.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.m_btnStats.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.m_btnStats.ForeColor = System.Drawing.Color.Blue;
            this.m_btnStats.Name = "m_btnStats";
            this.m_btnStats.UseVisualStyleBackColor = true;
            this.m_btnStats.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnButtonStatsClick);
            // 
            // VokabelTrainer
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.m_btnStats);
            this.Controls.Add(this.m_btnOsLanguageAndKeyboardSettings);
            this.Controls.Add(this.m_lblDontLearnAquire);
            this.Controls.Add(this.m_btnSearchESpeak);
            this.Controls.Add(this.m_tbxESpeakPath);
            this.Controls.Add(this.m_chkUseESpeak);
            this.Controls.Add(this.m_lblDownloadESpeak);
            this.Controls.Add(this.m_cbxReader);
            this.Controls.Add(this.m_lblReader);
            this.Controls.Add(this.m_btnShowDesktopKeyboard);
            this.Controls.Add(this.m_lblShowAbout);
            this.Controls.Add(this.m_lblShowLicense);
            this.Controls.Add(this.m_btnMostIntensiveFirstToSecond);
            this.Controls.Add(this.m_btnMostIntensiveSecondToFirst);
            this.Controls.Add(this.m_btnIntensiveFirstToSecond);
            this.Controls.Add(this.m_btnIntensiveSecondToFirst);
            this.Controls.Add(this.m_btnNewLanguageFile);
            this.Controls.Add(this.m_btnExerciseFirstToSecond);
            this.Controls.Add(this.m_btnExerciseSecondToFirst);
            this.Controls.Add(this.m_btnEnterVocabulary);
            this.Controls.Add(this.m_btnLoadLanguageFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "VokabelTrainer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.VokabelTrainer_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VokabelTrainer_MouseMove);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.OnHelpRequested);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnLoadLanguageFile;
        private System.Windows.Forms.Button m_btnEnterVocabulary;
        private System.Windows.Forms.Button m_btnExerciseSecondToFirst;
        private System.Windows.Forms.Button m_btnExerciseFirstToSecond;
        private System.Windows.Forms.Button m_btnNewLanguageFile;
        private System.Windows.Forms.OpenFileDialog m_dlgOpenFileDialog;
        private System.Windows.Forms.Button m_btnIntensiveSecondToFirst;
        private System.Windows.Forms.Button m_btnIntensiveFirstToSecond;
        private System.Windows.Forms.Button m_btnMostIntensiveSecondToFirst;
        private System.Windows.Forms.Button m_btnMostIntensiveFirstToSecond;
        private System.Windows.Forms.LinkLabel m_lblShowLicense;
        private System.Windows.Forms.LinkLabel m_lblShowAbout;
        private System.Windows.Forms.Button m_btnShowDesktopKeyboard;
        private System.Windows.Forms.Label m_lblReader;
        private System.Windows.Forms.ComboBox m_cbxReader;
        private System.Windows.Forms.LinkLabel m_lblDownloadESpeak;
        private System.Windows.Forms.CheckBox m_chkUseESpeak;
        private System.Windows.Forms.TextBox m_tbxESpeakPath;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button m_btnSearchESpeak;
        private System.Windows.Forms.LinkLabel m_lblDontLearnAquire;
        private System.Windows.Forms.Button m_btnOsLanguageAndKeyboardSettings;
        private System.Windows.Forms.Button m_btnStats;
    }
}

