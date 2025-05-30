﻿// VokabelTrainer v1.4
// Copyright (C) 2019-2025 NataljaNeumann@gmx.de
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VokabelTrainer
{
    //*******************************************************************************************************
    /// <summary>
    /// Form for creation of new vocabulary files
    /// </summary>
    //*******************************************************************************************************
    public partial class NewLanguageFile : Form
    {
        //===================================================================================================
        /// <summary>
        /// Constructs a new form
        /// </summary>
        //===================================================================================================
        public NewLanguageFile()
        {
            InitializeComponent();
            ReadyToUseImageInjection("Images\\NewLanguageFileHeader.jpg");
            if (string.IsNullOrEmpty(m_tbxFirstLanguage.Text) &&
                string.IsNullOrEmpty(m_tbxSecondLanguage.Text))
            {
                m_chkFirstLanguageRTL.Checked = m_chkSecondLanguageRTL.Checked = RightToLeft == RightToLeft.Yes;
            }
            // set direction for right to left
            if (!string.IsNullOrEmpty(m_tbxSecondLanguage.Text) && RightToLeft == RightToLeft.Yes)
                m_chkSecondLanguageRTL.Checked = true;

            m_tbxFirstLanguage.RightToLeft = m_chkFirstLanguageRTL.Checked ? RightToLeft.Yes : RightToLeft.No;
            m_tbxSecondLanguage.RightToLeft = m_chkSecondLanguageRTL.Checked ? RightToLeft.Yes : RightToLeft.No;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks create 
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void m_btnCreateLanguageFile_Click(object oSender, EventArgs oEventArgs)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks cancel
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void m_btnCancel_Click(object oSender, EventArgs oEventArgs)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when text in first language box changes
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void textBoxFirstLanguage_TextChanged(object oSender, EventArgs oEventArgs)
        {
            m_btnCreateLanguageFile.Enabled = 
                m_tbxFirstLanguage.Text.Length > 0 && m_tbxSecondLanguage.Text.Length > 0;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when text is second language box changes
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void textBoxSecondLanguage_TextChanged(object oSender, EventArgs oEventArgs)
        {
            m_btnCreateLanguageFile.Enabled = 
                m_tbxFirstLanguage.Text.Length > 0 && m_tbxSecondLanguage.Text.Length > 0;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks create 
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void m_chkGPL2_CheckedChanged(object oSender, EventArgs oEventArgs)
        {
            if (m_chkLanguageFileUnderGPL2.Checked)
            {
                m_chkLanguageFileModifiable.Checked = true;
                m_chkLanguageFileModifiable.Enabled = false;
            }
            else
            {
                m_chkLanguageFileModifiable.Enabled = true;
            }
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when checkbox first language RTL
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void m_chkFirstLanguageRTL_CheckedChanged(object oSender, EventArgs oEventArgs)
        {
            m_tbxFirstLanguage.RightToLeft = m_chkFirstLanguageRTL.Checked ? RightToLeft.Yes : RightToLeft.No;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when checkbox second language RTL
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void m_chkSecondLanguageRTL_CheckedChanged(object oSender, EventArgs oEventArgs)
        {
            m_tbxSecondLanguage.RightToLeft = m_chkSecondLanguageRTL.Checked ? RightToLeft.Yes : RightToLeft.No;
        }

        //===================================================================================================
        /// <summary>
        /// This is execute when user presses F1 key
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Even args</param>
        //===================================================================================================
        private void OnHelpRequested(object oSender, HelpEventArgs oEventArgs)
        {
            System.Diagnostics.Process.Start(System.IO.Path.Combine(Application.StartupPath, "Readme.html"));
        }


        #region image injection part
        //===================================================================================================
        /// <summary>
        /// Picture box control
        /// </summary>
        private PictureBox m_ctlPictureBox;
        //===================================================================================================
        /// <summary>
        /// Image
        /// </summary>
        private Image m_oLoadedImage;
        //===================================================================================================
        /// <summary>
        /// A dictionary with positions of other elements
        /// </summary>
        private Dictionary<Control, int> m_oOriginalPositions;

        //===================================================================================================
        /// <summary>
        /// Loads an image from application startup path and shows it at the top of the window
        /// </summary>
        /// <param name="strName">Name of the image, without directory specifications</param>
        //===================================================================================================
        private void ReadyToUseImageInjection(string strImageName)
        {
            string strImagePath = System.IO.Path.Combine(Application.StartupPath, strImageName);
            if (System.IO.File.Exists(strImagePath))
            {
                m_oOriginalPositions = new Dictionary<Control, int>();
                foreach (Control ctl in Controls)
                {
                    m_oOriginalPositions[ctl] = ctl.Top;
                }

                m_ctlPictureBox = new PictureBox();
                m_ctlPictureBox.Location = this.ClientRectangle.Location;
                m_ctlPictureBox.Size = new Size(0, 0);
                Controls.Add(m_ctlPictureBox);

                LoadAndResizeImage(strImagePath);

                this.Resize += new EventHandler(ResizeImageAlongWithForm);
            }
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image along with the form
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void ResizeImageAlongWithForm(object oSender, EventArgs oEventArgs)
        {
            ResizeImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Loads an image and resizes it to the width of client area
        /// </summary>
        /// <param name="strImagePath"></param>
        //===================================================================================================
        private void LoadAndResizeImage(string strImagePath)
        {
            m_oLoadedImage = Image.FromFile(strImagePath);
            ResizeImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image and shifts other elements
        /// </summary>
        //===================================================================================================
        private void ResizeImageAndShiftElements()
        {
            if (m_oLoadedImage != null)
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    float fAspectRatio = (float)m_oLoadedImage.Width / (float)m_oLoadedImage.Height;

                    int nNewWidth = this.ClientSize.Width;
                    if (nNewWidth != 0)
                    {
                        int nNewHeight = (int)(nNewWidth / fAspectRatio);

                        int nHeightChange = nNewHeight - m_ctlPictureBox.Height;

                        this.m_ctlPictureBox.Image = new Bitmap(m_oLoadedImage, nNewWidth, nNewHeight);
                        this.m_ctlPictureBox.Size = new Size(nNewWidth, nNewHeight);

                        ShiftOtherElementsUpOrDown(nHeightChange);
                        this.Height += nHeightChange;
                    }
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Shifts elements, apart from the image box up or down
        /// </summary>
        /// <param name="nHeightChange"></param>
        private void ShiftOtherElementsUpOrDown(int nHeightChange)
        {
            foreach (Control ctl in m_oOriginalPositions.Keys)
            {
                if ((ctl.Anchor & AnchorStyles.Bottom) == AnchorStyles.None)
                    ctl.Top += nHeightChange;
            }
        }
        #endregion


    }
}
