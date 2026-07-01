using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
// VokabelTrainer v1.6
// Copyright (C) 2026 NataljaNeumann@gmx.de
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

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VokabelTrainer.Forms
{
    //*******************************************************************************************************
    /// <summary>
    /// Implemented variants of Chinese language in vocabulary books
    /// </summary>
    //*******************************************************************************************************
    [Flags]
    public enum ChineseVariantType
    {
        None = 0,
        ChinesePeoplesRepublicOfChina = 1,
        ChineseOfPeoplesRepublicOfChinaPinyinWriting = 2,
        TraditionalChineseLanguageOfTaiwan = 4,
        ChineseOfTaiwanPinyinWriting = 8,
        ZhuyinFuhaoWritingOfTaiwan = 16,
        All = 31
    }

    //*******************************************************************************************************
    /// <summary>
    /// This form provides means for selection of a variant of chinese language
    /// </summary>
    //*******************************************************************************************************
    public partial class ChineseVariantSelection : Form
    {

        //===================================================================================================
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eVariants">Available variants, must not be None</param>
        /// <exception cref="InvalidEnumArgumentException">In case there are no variants</exception>
        //===================================================================================================
        public ChineseVariantSelection(ChineseVariantType eVariants)
        {
            if (eVariants == ChineseVariantType.None)
                throw new InvalidEnumArgumentException(nameof(eVariants));

            InitializeComponent();

            m_rbChinesePeoplesRepublicOfChina.Visible =
                m_rbChinesePeoplesRepublicOfChina.Enabled =
                    (eVariants & ChineseVariantType.ChinesePeoplesRepublicOfChina) != ChineseVariantType.None;

            m_rbChinesePeplesRepublicOfChinaPinyinWriting.Visible =
                m_rbChinesePeplesRepublicOfChinaPinyinWriting.Enabled =
                    (eVariants & ChineseVariantType.ChineseOfPeoplesRepublicOfChinaPinyinWriting) != ChineseVariantType.None;

            m_rbTraditionalChineseLanguageOfTaiwan.Visible =
                m_rbTraditionalChineseLanguageOfTaiwan.Enabled =
                    (eVariants & ChineseVariantType.TraditionalChineseLanguageOfTaiwan) != ChineseVariantType.None;

            m_rbPinyinChineseOfTaiwanPinyinWriting.Visible =
                m_rbPinyinChineseOfTaiwanPinyinWriting.Enabled =
                    (eVariants & ChineseVariantType.ChineseOfPeoplesRepublicOfChinaPinyinWriting) != ChineseVariantType.None;

            m_rbZhuyinFuhaoWritingOfTaiwan.Visible =
                m_rbZhuyinFuhaoWritingOfTaiwan.Enabled =
                    (eVariants & ChineseVariantType.ZhuyinFuhaoWritingOfTaiwan) != ChineseVariantType.None;

            if (m_rbChinesePeoplesRepublicOfChina.Enabled)
                m_rbZhuyinFuhaoWritingOfTaiwan.Checked = true;
            else if (m_rbTraditionalChineseLanguageOfTaiwan.Enabled)
                m_rbTraditionalChineseLanguageOfTaiwan.Checked = true;
            else if (m_rbZhuyinFuhaoWritingOfTaiwan.Enabled)
                m_rbZhuyinFuhaoWritingOfTaiwan.Checked = true;
            else if (m_rbPinyinChineseOfTaiwanPinyinWriting.Enabled)
                m_rbPinyinChineseOfTaiwanPinyinWriting.Checked = true;
            else if (m_rbChinesePeplesRepublicOfChinaPinyinWriting.Enabled)
                m_rbChinesePeplesRepublicOfChinaPinyinWriting.Checked = true;
            else
                throw new InvalidEnumArgumentException(nameof(eVariants));

            ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                 "ChineseVariantHeader.jpg");
        }


        //===================================================================================================
        /// <summary>
        /// The chosen variant
        /// </summary>
        public ChineseVariantType Choice {  get; private set; }


        //===================================================================================================
        /// <summary>
        /// This is executed when user checks the corresponding radio button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_rbChinesePeoplesRepublicOfChina_CheckedChanged(object oSender, EventArgs oArgs)
        {
            Choice = ChineseVariantType.ChinesePeoplesRepublicOfChina;
        }
        //===================================================================================================
        /// <summary>
        /// This is executed when user checks the corresponding radio button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_rbChineseOfPeoplesRepublicOfChinaPinyinWriting_CheckedChanged(object oSender, EventArgs oArgs)
        {
            Choice = ChineseVariantType.ChineseOfPeoplesRepublicOfChinaPinyinWriting;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user checks the corresponding radio button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_rbTraditionalChineseLanguageOfTaiwan_CheckedChanged(object oSender, EventArgs oArgs)
        {
            Choice = ChineseVariantType.TraditionalChineseLanguageOfTaiwan;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user checks the corresponding radio button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_rbPinyinChineseOfTaiwanPinyinWriting_CheckedChanged(object oSender, EventArgs oArgs)
        {
            Choice = ChineseVariantType.ChineseOfTaiwanPinyinWriting;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user checks the corresponding radio button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void m_rbZhuyinFuhaoWritingOfTaiwan_CheckedChanged(object oSender, EventArgs oArgs)
        {
            Choice = ChineseVariantType.ZhuyinFuhaoWritingOfTaiwan;
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
