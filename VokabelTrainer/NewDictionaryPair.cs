// VokabelTrainer v1.3
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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VokabelTrainer
{
    //*******************************************************************************************************
    /// <summary>
    /// Form for entering new words
    /// </summary>
    //*******************************************************************************************************
    public partial class NewDictionaryPair : Form
    {
        //===================================================================================================
        /// <summary>
        /// Indicates, if the word already has been said
        /// </summary>
        bool m_bAlreadySaid;

        //===================================================================================================
        /// <summary>
        /// Indicates, if eSpeak shall be used
        /// </summary>
        bool m_bUseESpeak;

        //===================================================================================================
        /// <summary>
        /// The path of eSpeak executable
        /// </summary>
        string m_strEspeakPath;

        //===================================================================================================
        /// <summary>
        /// Constructs a new form
        /// </summary>
        /// <param name="bUseESpeak">Indicates that eSpeak shall be used</param>
        /// <param name="strEspeakPath">eSpeak path</param>
        //===================================================================================================
        public NewDictionaryPair(bool bUseESpeak, string strEspeakPath)
        {
            InitializeComponent();
            ReadyToUseImageInjection("NewDictionaryPairHeader.jpg");
            m_bUseESpeak = bUseESpeak;
            m_strEspeakPath = strEspeakPath;

        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user click "next"
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void buttonNext_Click(object oSender, EventArgs oArgs)
        {
            if (!m_bAlreadySaid && textBoxSecondLanguage.Text.Length > 0)
            {
                Speaker.Say(m_lblSecondLanguage.Text, textBoxSecondLanguage.Text, true, 
                    m_bUseESpeak, m_strEspeakPath);

                m_bAlreadySaid = true;
            }

            DialogResult = DialogResult.Retry;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user click "last"
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_btnEnteredLast_Click(object oSender, EventArgs oArgs)
        {
            DialogResult = DialogResult.OK;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user click "cancel"
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_btnCancel_Click(object oSender, EventArgs oArgs)
        {
            DialogResult = DialogResult.Cancel;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when the form is shown
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void NewDictionaryPair_Shown(object oSender, EventArgs oArgs)
        {
            if (m_tbxFirstLanguage.Text.Trim().Length == 0)
                m_tbxFirstLanguage.Focus();
            else
                if (textBoxSecondLanguage.Text.Trim().Length == 0)
                    textBoxSecondLanguage.Focus();
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when focus leaves first language box
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void textBoxFirstLanguage_Leave(object oSender, EventArgs oArgs)
        {
            Speaker.Say(m_lblFirstLanguage.Text, m_tbxFirstLanguage.Text, true, 
                m_bUseESpeak, m_strEspeakPath);
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when focus leaves second language box
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void textBoxSecondLanguage_Leave(object oSender, EventArgs oArgs)
        {
            Speaker.Say(m_lblSecondLanguage.Text, textBoxSecondLanguage.Text, true, 
                m_bUseESpeak, m_strEspeakPath);
            m_bAlreadySaid = true;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when the text of second box changes
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void textBoxSecondLanguage_TextChanged(object oSender, EventArgs oArgs)
        {
            m_bAlreadySaid = false;
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
                ctl.Top = m_oOriginalPositions[ctl] + nHeightChange;
            }
        }
        #endregion
    }
}
