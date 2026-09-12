// VokabelTrainer v1.7
// Copyright (C) 2019-2026 NataljaNeumann@gmx.de
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
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace VokabelTrainer
{
    //*******************************************************************************************************
    /// <summary>
    /// Shows a simple message box
    /// </summary>
    //*******************************************************************************************************
    public partial class NewMessageBox : Form
    {
        //===================================================================================================
        /// <summary>
        /// Old size
        /// </summary>
        System.Drawing.Size m_oOldSize;

        string m_strImagePath;

        bool m_bSkipRecursion;

        //===================================================================================================
        /// <summary>
        /// Constructs a new message box
        /// </summary>
        //===================================================================================================
        public NewMessageBox(
            bool bOk,
            bool bCancel,
            bool bYesNo, 
            string strImagePath)
        {
            InitializeComponent();

            if (bOk)
            {
                if (!bCancel)
                {
                    Size oSize = m_btnCancel.Size;
                    Point oLocation = m_btnCancel.Location;

                    m_btnCancel.Hide();
                    m_btnOK.Location = oLocation;
                    m_btnOK.Size = oSize;

                    m_btnYes.Hide();
                    m_btnNo.Hide();
                }
            } else
            {
                Size oSize = m_btnCancel.Size;
                Point oLocation = m_btnCancel.Location;

                m_btnCancel.Hide();
                m_btnNo.Location = oLocation;
                m_btnNo.Size = oSize;

                oSize = m_btnOK.Size;
                oLocation = m_btnOK.Location;

                m_btnOK.Hide();
                m_btnYes.Location = oLocation;
                m_btnYes.Size = oSize;
            }

            m_strImagePath = strImagePath;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when the message box is shown
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void NewMessageBox_Shown(object oSender, EventArgs oEventArgs)
        {
            m_btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            m_btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            m_btnYes.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            m_btnNo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            m_lblMessageText.Anchor = AnchorStyles.Bottom;

            ReadyToUseImageInjection(m_strImagePath);

            m_lblMessageText.Anchor = AnchorStyles.Top;

            m_oOldSize = m_lblMessageText.Size;
            m_lblMessageText.AutoSize = true;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when the size of the label changes
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void label1_SizeChanged(object oSender, EventArgs oEventArgs)
        {
            if (m_oOldSize != null && !m_lblMessageText.Text.Equals("label1"))
            {
                if (m_lblMessageText.Width > m_lblMessageText.Height * 3 && !m_bSkipRecursion)
                {
                    int nLeg = (int)Math.Sqrt(m_lblMessageText.Width * m_lblMessageText.Height);

                    m_bSkipRecursion  = true;
                    m_lblMessageText.MaximumSize = new Size(Math.Max(nLeg*2, m_btnNo.Right - m_btnYes.Left),
                        nLeg*2);

                    m_bSkipRecursion = false;
                }
                else
                {
                    Size = new Size(Math.Max(Size.Width + ((Label)oSender).Size.Width - m_oOldSize.Width, m_btnNo.Right - m_btnYes.Left + (m_btnNo.Left-m_btnYes.Right)*3),
                                    Size.Height + ((Label)oSender).Size.Height - m_oOldSize.Height);

                    m_lblMessageText.Top = m_ctlPictureBox.Bottom + (m_btnNo.Left - m_btnYes.Right)*2;
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Shows a message box
        /// </summary>
        /// <param name="iOwner">Owner window</param>
        /// <param name="strMessage">Message to show</param>
        /// <param name="strHeader">Header of the window</param>
        /// <param name="strSay">Text to say</param>
        /// <returns>Result of the dialog</returns>
        //===================================================================================================
        public static DialogResult Show(
            IWin32Window iOwner, 
            string strMessage,
            string strHeader, 
            string strSay,
            bool bOk, 
            bool bCancel,
            bool bYesNo, 
            string strImageHint
            )
        {
            using (NewMessageBox mb = new NewMessageBox(
                bOk, bCancel, bYesNo,
                "Images" + Path.DirectorySeparatorChar + 
                "MessageBox" +strImageHint + "HorizontalHeader.jpg"))
            {
                mb.m_lblMessageText.Text = strMessage;
                mb.Text = strHeader;

                /*
                if (!string.IsNullOrEmpty(say))
                    Speaker.Say("Deutsch", say, true, m_chkUseESpeak.Checked, m_tbxESpeakPath);
                 */

                return mb.ShowDialog(iOwner);
            }
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when the user clicks OK
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void m_btnOk_Click(object oSender, EventArgs oEventArgs)
        {
            DialogResult = DialogResult.OK;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when the user clicks Cancel
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void m_btnCancel_Click(object oSender, EventArgs oEventArgs)
        {
            DialogResult = DialogResult.Cancel;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when the user clicks Yes
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void m_btnYes_Click(object oSender, EventArgs oEventArgs)
        {
            DialogResult = DialogResult.Yes;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when the user clicks No
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void m_btnNo_Click(object oSender, EventArgs oEventArgs)
        {
            DialogResult = DialogResult.No;
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
        private bool ReadyToUseImageInjection(
            string strImageName
            )
        {
            string strImagePath = System.IO.Path.Combine(Application.StartupPath, strImageName);
            if (System.IO.File.Exists(strImagePath))
            {
                m_ctlPictureBox = new PictureBox();
                m_ctlPictureBox.Location = this.ClientRectangle.Location;
                m_ctlPictureBox.Size = new Size(0, 0);
                Controls.Add(m_ctlPictureBox);

                LoadAndResizeImage(strImagePath);

                this.Resize += new EventHandler(ResizeImageAlongWithForm);
                return true;
            }
            else
            {
                return false;
            }
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image along with the form
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void ResizeImageAlongWithForm(
            object oSender,
            EventArgs oEventArgs
            )
        {
            ResizeImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Loads an image and resizes it to the width of client area
        /// </summary>
        /// <param name="strImagePath"></param>
        //===================================================================================================
        private void LoadAndResizeImage(
            string strImagePath
            )
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
        private void ShiftOtherElementsUpOrDown(
            int nHeightChange
            )
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
