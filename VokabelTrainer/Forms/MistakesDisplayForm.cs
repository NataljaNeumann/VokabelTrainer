/*  VokabelTrainer v1.7
    Copyright (C) 2026 NataljaNeumann@gmx.de

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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VokabelTrainer.Forms
{
    //*******************************************************************************************************
    /// <summary>
    /// This form can be used for displaying mistakes
    /// </summary>
    //*******************************************************************************************************
    public partial class MistakesDisplayForm : Form
    {
        //===================================================================================================
        /// <summary>
        /// Constructs a new 
        /// </summary>
        /// <param name="strText">Text to show</param>
        //===================================================================================================
        public MistakesDisplayForm(
            string strText,
            string strHeader,
            bool bRightToLeft
            )
        {

            InitializeComponent();

            this.RightToLeftLayout = bRightToLeft;
            this.RightToLeft = bRightToLeft ? RightToLeft.Yes : RightToLeft.No;

            // Set maximum size to 3/4 of the main screen size
            var oScreenRect = Screen.PrimaryScreen.WorkingArea;
            MaximumSize = new Size
            (
                (oScreenRect.Width * 6 / 7),
                (oScreenRect.Height * 6 / 7)
            );

            m_lblText.AutoSize = true;
            m_lblText.MaximumSize = new Size
            (
                (oScreenRect.Width * 6 / 8),
                (oScreenRect.Height * 6 / 8)
            ); 
            m_lblText.UseCompatibleTextRendering = false;
            m_lblText.Text = strText;

            ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                "MistakeDisplayHeader.jpg");
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when the label automatically resizes
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void OnSizeOfLabelChanged(
            object oSender,
            EventArgs oEventArgs
            )
        {
            if (!(oSender is Label oLabel))
                return;

            // Calculate the offset between label's origin and client rectangle's origin
            var clientOrigin = ClientRectangle.Location;
            int offsetX = oLabel.Location.X - clientOrigin.X;
            int offsetY = oLabel.Location.Y - clientOrigin.Y;

            // Calculate new client size required to fit the label
            int nRequiredClientWidth = offsetX*2 + oLabel.Width;
            int nRequiredClientHeight = offsetY*4 + oLabel.Height + m_btnOk.Height;

            // Calculate the difference between form size and client size
            int nExtraWidth = this.Width - this.ClientSize.Width;
            int nExtraHeight = this.Height - this.ClientSize.Height;

            // Calculate new form size to ensure the label fits inside the client area
            int nNewWidth = nRequiredClientWidth + nExtraWidth;
            int nNewHeight = nRequiredClientHeight + nExtraHeight;

            // Apply min/max constraints
            nNewWidth = Math.Max(this.MinimumSize.Width, Math.Min(nNewWidth, this.MaximumSize.Width));
            nNewHeight = Math.Max(this.MinimumSize.Height, Math.Min(nNewHeight, this.MaximumSize.Height));

            // Set the new size
            if (nNewWidth != 0 && nNewHeight !=0)
                this.Size = new Size(nNewWidth, nNewHeight);
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks OK
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void OnOkClicked(
            object oSender,
            EventArgs oEventArgs
            )
        {
            this.DialogResult = DialogResult.OK;
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
