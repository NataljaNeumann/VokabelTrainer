// VokabelTrainer v1.4
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
using System.Globalization;

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
        /// Keyboard settings for text box languages
        /// </summary>
        private Dictionary<TextBox, string> oTextBoxLanguages = new Dictionary<TextBox, string>();

        //===================================================================================================
        /// <summary>
        /// Gets or sets language code for the first text box
        /// </summary>
        public string FirstLanguageCode
        {
            get
            {
                return oTextBoxLanguages[m_tbxFirstLanguage];
            }
            set
            {
                oTextBoxLanguages[m_tbxFirstLanguage] = value;
                SetKeyboardLayout(m_tbxFirstLanguage, value);
            }
        }


        //===================================================================================================
        /// <summary>
        /// Gets or sets language code for the second text box
        /// </summary>
        public string SecondLanguageCode
        {
            get
            {
                return oTextBoxLanguages[m_tbxSecondLanguage];
            }
            set
            {
                oTextBoxLanguages[m_tbxSecondLanguage] = value;
            }
        }

        //===================================================================================================
        /// <summary>
        /// Constructs a new form
        /// </summary>
        /// <param name="bUseESpeak">Indicates that eSpeak shall be used</param>
        /// <param name="strEspeakPath">eSpeak path</param>
        //===================================================================================================
        public NewDictionaryPair(
            bool bUseESpeak,
            string strEspeakPath,
            string strFirstLanguage,
            string strSecondLanguage
            )
        {
            InitializeComponent();
            m_lblFirstLanguage.Text = strFirstLanguage + ":";
            m_lblSecondLanguage.Text = strSecondLanguage + ":";
            ReadyToUseImageInjection("Images\\NewDictionaryPairHeader.jpg");
            m_bUseESpeak = bUseESpeak;
            m_strEspeakPath = strEspeakPath;

            string strCode = Program.LanguageCodeFromName(strFirstLanguage);
            oTextBoxLanguages[m_tbxFirstLanguage] = strCode;
            SetKeyboardLayout(m_tbxFirstLanguage, strCode);
            strCode = Program.LanguageCodeFromName(strSecondLanguage);
            oTextBoxLanguages[m_tbxSecondLanguage] = strCode;

            m_tbxFirstLanguage.Leave += new EventHandler(OnTextBoxLeft);
            m_tbxSecondLanguage.Leave += new EventHandler(OnTextBoxLeft);
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user click "next"
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void buttonNext_Click(
            object oSender,
            EventArgs oArgs
            )
        {
            if (!m_bAlreadySaid && m_tbxSecondLanguage.Text.Length > 0)
            {
                Speaker.Say(m_lblSecondLanguage.Text, m_tbxSecondLanguage.Text, true, 
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
        private void m_btnEnteredLast_Click(
            object oSender,
            EventArgs oArgs
            )
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
        private void m_btnCancel_Click(
            object oSender,
            EventArgs oArgs
            )
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
        private void NewDictionaryPair_Shown(
            object oSender,
            EventArgs oArgs
            )
        {
            if (m_tbxFirstLanguage.Text.Trim().Length == 0)
                m_tbxFirstLanguage.Focus();
            else
                if (m_tbxSecondLanguage.Text.Trim().Length == 0)
                    m_tbxSecondLanguage.Focus();
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when focus leaves first language box
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void textBoxFirstLanguage_Leave(
            object oSender,
            EventArgs oArgs
            )
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
        private void textBoxSecondLanguage_Leave(
            object oSender, 
            EventArgs oArgs
            )
        {
            Speaker.Say(m_lblSecondLanguage.Text, m_tbxSecondLanguage.Text, true, 
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
        private void textBoxSecondLanguage_TextChanged(
            object oSender, 
            EventArgs oArgs
            )
        {
            m_bAlreadySaid = false;
        }

        //===================================================================================================
        /// <summary>
        /// This is execute when user presses F1 key
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Even args</param>
        //===================================================================================================
        private void OnHelpRequested(
            object oSender, 
            HelpEventArgs oEventArgs
            )
        {
            System.Diagnostics.Process.Start(System.IO.Path.Combine(Application.StartupPath, "Readme.html"));
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when one of the two text boxes is entered
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void OnTextBoxEntered(
            object oSender,
            EventArgs oEventArgs
            )
        {
            try
            {
                TextBox ctlTextBox = oSender as TextBox;
                if (ctlTextBox != null)
                {
                    string strSavedLanguage;
                    // Check if the text box has a saved language setting
                    if (oTextBoxLanguages.TryGetValue(ctlTextBox, out strSavedLanguage))
                    {
                        // Restore the saved language setting
                        SetKeyboardLayout(ctlTextBox, strSavedLanguage);
                    }
                }
            }
            catch
            {
            }
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when a text box is left
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void OnTextBoxLeft(
            object oSender,
            EventArgs oEventArgs
            )
        {
            try
            {
                TextBox ctlTextBox = oSender as TextBox;
                if (ctlTextBox != null)
                {
                    // Save the current keyboard layout for the text box
                    string strCurrentLanguage = GetCurrentKeyboardLayout();
                    oTextBoxLanguages[ctlTextBox] = strCurrentLanguage;
                }
            }
            catch
            {
            }
        }

        //===================================================================================================
        /// <summary>
        /// Gets current keyboart layout
        /// </summary>
        /// <returns>Current keyboard layoutt</returns>
        //===================================================================================================
        public string GetCurrentKeyboardLayout()
        {
            InputLanguage oCurrentInputLang = InputLanguage.CurrentInputLanguage;
            CultureInfo oCurrentCulture = oCurrentInputLang.Culture;
            return oCurrentCulture.Name;
        }

        //===================================================================================================
        /// <summary>
        /// Sets keyboard layout for a text box
        /// </summary>
        /// <param name="ctlTextBox">Text box</param>
        /// <param name="strLanguageCode">Language code</param>
        //===================================================================================================
        public void SetKeyboardLayout(
            Control ctlTextBox,
            string strLanguageCode
            )
        {
            try
            {
                InputLanguage oNewInputLang = InputLanguage.FromCulture(new CultureInfo(strLanguageCode));
                InputLanguage.CurrentInputLanguage = oNewInputLang;
                ctlTextBox.Focus();
            }
            catch
            {
            }
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
        /// <param name="nHeightChange">The change in height, compared to previous</param>
        //===================================================================================================
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
