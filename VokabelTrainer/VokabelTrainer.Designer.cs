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
            this.m_lblShowLicenceLocalised = new System.Windows.Forms.LinkLabel();
            this.m_lblShowLicense = new System.Windows.Forms.LinkLabel();
            this.m_lblShowAbout = new System.Windows.Forms.LinkLabel();
            this.m_btnShowDesktopKeyboard = new System.Windows.Forms.Button();
            this.m_lblReader = new System.Windows.Forms.Label();
            this.m_cbxReader = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // m_btnLoadLanguageFile
            // 
            this.m_btnLoadLanguageFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnLoadLanguageFile.Location = new System.Drawing.Point(12, 79);
            this.m_btnLoadLanguageFile.Name = "m_btnLoadLanguageFile";
            this.m_btnLoadLanguageFile.Size = new System.Drawing.Size(148, 61);
            this.m_btnLoadLanguageFile.TabIndex = 1;
            this.m_btnLoadLanguageFile.Text = "Sprachdatei laden";
            this.m_btnLoadLanguageFile.UseVisualStyleBackColor = true;
            this.m_btnLoadLanguageFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_btnEnterVocabulary
            // 
            this.m_btnEnterVocabulary.Enabled = false;
            this.m_btnEnterVocabulary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnEnterVocabulary.Location = new System.Drawing.Point(12, 146);
            this.m_btnEnterVocabulary.Name = "m_btnEnterVocabulary";
            this.m_btnEnterVocabulary.Size = new System.Drawing.Size(148, 61);
            this.m_btnEnterVocabulary.TabIndex = 2;
            this.m_btnEnterVocabulary.Text = "Vokabeln eingeben";
            this.m_btnEnterVocabulary.UseVisualStyleBackColor = true;
            this.m_btnEnterVocabulary.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_btnExerciseSecondToFirst
            // 
            this.m_btnExerciseSecondToFirst.Enabled = false;
            this.m_btnExerciseSecondToFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnExerciseSecondToFirst.Location = new System.Drawing.Point(186, 12);
            this.m_btnExerciseSecondToFirst.Name = "m_btnExerciseSecondToFirst";
            this.m_btnExerciseSecondToFirst.Size = new System.Drawing.Size(213, 61);
            this.m_btnExerciseSecondToFirst.TabIndex = 3;
            this.m_btnExerciseSecondToFirst.UseVisualStyleBackColor = true;
            this.m_btnExerciseSecondToFirst.Click += new System.EventHandler(this.button3_Click);
            // 
            // m_btnExerciseFirstToSecond
            // 
            this.m_btnExerciseFirstToSecond.Enabled = false;
            this.m_btnExerciseFirstToSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnExerciseFirstToSecond.Location = new System.Drawing.Point(406, 12);
            this.m_btnExerciseFirstToSecond.Name = "m_btnExerciseFirstToSecond";
            this.m_btnExerciseFirstToSecond.Size = new System.Drawing.Size(213, 61);
            this.m_btnExerciseFirstToSecond.TabIndex = 4;
            this.m_btnExerciseFirstToSecond.UseVisualStyleBackColor = true;
            this.m_btnExerciseFirstToSecond.Click += new System.EventHandler(this.button4_Click);
            // 
            // m_btnNewLanguageFile
            // 
            this.m_btnNewLanguageFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnNewLanguageFile.Location = new System.Drawing.Point(12, 12);
            this.m_btnNewLanguageFile.Name = "m_btnNewLanguageFile";
            this.m_btnNewLanguageFile.Size = new System.Drawing.Size(148, 61);
            this.m_btnNewLanguageFile.TabIndex = 0;
            this.m_btnNewLanguageFile.Text = "Neue Sprachdatei";
            this.m_btnNewLanguageFile.UseVisualStyleBackColor = true;
            this.m_btnNewLanguageFile.Click += new System.EventHandler(this.button5_Click);
            // 
            // m_dlgOpenFileDialog
            // 
            this.m_dlgOpenFileDialog.DefaultExt = "Vokabeln.xml";
            this.m_dlgOpenFileDialog.FileName = "Englisch-Deutsch.Vokabeln.xml";
            this.m_dlgOpenFileDialog.Filter = "Vokabel-Dateien|*.Vokabeln.xml";
            // 
            // m_btnIntensiveSecondToFirst
            // 
            this.m_btnIntensiveSecondToFirst.Enabled = false;
            this.m_btnIntensiveSecondToFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnIntensiveSecondToFirst.Location = new System.Drawing.Point(186, 79);
            this.m_btnIntensiveSecondToFirst.Name = "m_btnIntensiveSecondToFirst";
            this.m_btnIntensiveSecondToFirst.Size = new System.Drawing.Size(213, 61);
            this.m_btnIntensiveSecondToFirst.TabIndex = 5;
            this.m_btnIntensiveSecondToFirst.UseVisualStyleBackColor = true;
            this.m_btnIntensiveSecondToFirst.Click += new System.EventHandler(this.button6_Click);
            // 
            // m_btnIntensiveFirstToSecond
            // 
            this.m_btnIntensiveFirstToSecond.Enabled = false;
            this.m_btnIntensiveFirstToSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnIntensiveFirstToSecond.Location = new System.Drawing.Point(406, 79);
            this.m_btnIntensiveFirstToSecond.Name = "m_btnIntensiveFirstToSecond";
            this.m_btnIntensiveFirstToSecond.Size = new System.Drawing.Size(213, 61);
            this.m_btnIntensiveFirstToSecond.TabIndex = 6;
            this.m_btnIntensiveFirstToSecond.UseVisualStyleBackColor = true;
            this.m_btnIntensiveFirstToSecond.Click += new System.EventHandler(this.button7_Click);
            // 
            // m_btnMostIntensiveSecondToFirst
            // 
            this.m_btnMostIntensiveSecondToFirst.Enabled = false;
            this.m_btnMostIntensiveSecondToFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnMostIntensiveSecondToFirst.Location = new System.Drawing.Point(186, 146);
            this.m_btnMostIntensiveSecondToFirst.Name = "m_btnMostIntensiveSecondToFirst";
            this.m_btnMostIntensiveSecondToFirst.Size = new System.Drawing.Size(213, 61);
            this.m_btnMostIntensiveSecondToFirst.TabIndex = 7;
            this.m_btnMostIntensiveSecondToFirst.UseVisualStyleBackColor = true;
            this.m_btnMostIntensiveSecondToFirst.Click += new System.EventHandler(this.button8_Click);
            // 
            // m_btnMostIntensiveFirstToSecond
            // 
            this.m_btnMostIntensiveFirstToSecond.Enabled = false;
            this.m_btnMostIntensiveFirstToSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnMostIntensiveFirstToSecond.Location = new System.Drawing.Point(406, 146);
            this.m_btnMostIntensiveFirstToSecond.Name = "m_btnMostIntensiveFirstToSecond";
            this.m_btnMostIntensiveFirstToSecond.Size = new System.Drawing.Size(213, 61);
            this.m_btnMostIntensiveFirstToSecond.TabIndex = 8;
            this.m_btnMostIntensiveFirstToSecond.UseVisualStyleBackColor = true;
            this.m_btnMostIntensiveFirstToSecond.Click += new System.EventHandler(this.button9_Click);
            // 
            // m_lblShowLicenceLocalised
            // 
            this.m_lblShowLicenceLocalised.AutoSize = true;
            this.m_lblShowLicenceLocalised.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblShowLicenceLocalised.Location = new System.Drawing.Point(391, 246);
            this.m_lblShowLicenceLocalised.Name = "m_lblShowLicenceLocalised";
            this.m_lblShowLicenceLocalised.Size = new System.Drawing.Size(228, 16);
            this.m_lblShowLicenceLocalised.TabIndex = 11;
            this.m_lblShowLicenceLocalised.TabStop = true;
            this.m_lblShowLicenceLocalised.Text = "Lizenztext auf Deutsch (nicht bindend)";
            this.m_lblShowLicenceLocalised.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // m_lblShowLicense
            // 
            this.m_lblShowLicense.AutoSize = true;
            this.m_lblShowLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblShowLicense.Location = new System.Drawing.Point(183, 246);
            this.m_lblShowLicense.Name = "m_lblShowLicense";
            this.m_lblShowLicense.Size = new System.Drawing.Size(154, 16);
            this.m_lblShowLicense.TabIndex = 10;
            this.m_lblShowLicense.TabStop = true;
            this.m_lblShowLicense.Text = "Lizenz (GPL v2, bindend)";
            this.m_lblShowLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // m_lblShowAbout
            // 
            this.m_lblShowAbout.AutoSize = true;
            this.m_lblShowAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblShowAbout.Location = new System.Drawing.Point(459, 213);
            this.m_lblShowAbout.Name = "m_lblShowAbout";
            this.m_lblShowAbout.Size = new System.Drawing.Size(160, 16);
            this.m_lblShowAbout.TabIndex = 9;
            this.m_lblShowAbout.TabStop = true;
            this.m_lblShowAbout.Text = "Info über Vokabel-Trainer";
            this.m_lblShowAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // m_btnShowDesktopKeyboard
            // 
            this.m_btnShowDesktopKeyboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnShowDesktopKeyboard.Location = new System.Drawing.Point(12, 213);
            this.m_btnShowDesktopKeyboard.Name = "m_btnShowDesktopKeyboard";
            this.m_btnShowDesktopKeyboard.Size = new System.Drawing.Size(148, 49);
            this.m_btnShowDesktopKeyboard.TabIndex = 12;
            this.m_btnShowDesktopKeyboard.Text = "Bildschirmtastatur (osk.exe)";
            this.m_btnShowDesktopKeyboard.UseVisualStyleBackColor = true;
            this.m_btnShowDesktopKeyboard.Click += new System.EventHandler(this.button10_Click);
            // 
            // m_lblReader
            // 
            this.m_lblReader.AutoSize = true;
            this.m_lblReader.Location = new System.Drawing.Point(183, 216);
            this.m_lblReader.Name = "m_lblReader";
            this.m_lblReader.Size = new System.Drawing.Size(51, 13);
            this.m_lblReader.TabIndex = 13;
            this.m_lblReader.Text = "Vorlesen:";
            // 
            // m_cbxReader
            // 
            this.m_cbxReader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbxReader.Enabled = false;
            this.m_cbxReader.FormattingEnabled = true;
            this.m_cbxReader.Items.AddRange(new object[] {
            "Angezeigte Sprache",
            "Geprüfte Sprache",
            "Erste Sprache",
            "Zweite Sprache",
            "Nur bei Fehlern"});
            this.m_cbxReader.Location = new System.Drawing.Point(240, 212);
            this.m_cbxReader.Name = "m_cbxReader";
            this.m_cbxReader.Size = new System.Drawing.Size(213, 21);
            this.m_cbxReader.TabIndex = 14;
            // 
            // VokabelTrainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 274);
            this.Controls.Add(this.m_cbxReader);
            this.Controls.Add(this.m_lblReader);
            this.Controls.Add(this.m_btnShowDesktopKeyboard);
            this.Controls.Add(this.m_lblShowAbout);
            this.Controls.Add(this.m_lblShowLicense);
            this.Controls.Add(this.m_lblShowLicenceLocalised);
            this.Controls.Add(this.m_btnMostIntensiveFirstToSecond);
            this.Controls.Add(this.m_btnMostIntensiveSecondToFirst);
            this.Controls.Add(this.m_btnIntensiveFirstToSecond);
            this.Controls.Add(this.m_btnIntensiveSecondToFirst);
            this.Controls.Add(this.m_btnNewLanguageFile);
            this.Controls.Add(this.m_btnExerciseFirstToSecond);
            this.Controls.Add(this.m_btnExerciseSecondToFirst);
            this.Controls.Add(this.m_btnEnterVocabulary);
            this.Controls.Add(this.m_btnLoadLanguageFile);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "VokabelTrainer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Vokabel-Trainer";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VokabelTrainer_MouseMove);
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
        private System.Windows.Forms.LinkLabel m_lblShowLicenceLocalised;
        private System.Windows.Forms.LinkLabel m_lblShowLicense;
        private System.Windows.Forms.LinkLabel m_lblShowAbout;
        private System.Windows.Forms.Button m_btnShowDesktopKeyboard;
        private System.Windows.Forms.Label m_lblReader;
        private System.Windows.Forms.ComboBox m_cbxReader;
    }
}

