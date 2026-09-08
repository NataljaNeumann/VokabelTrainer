// VokabelTrainer v1.7
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
    /// The form for showing new words in a course
    /// </summary>
    //*******************************************************************************************************
    public partial class NewWordsInCourseForm : Form, IDisposable
    {
        //===================================================================================================
        /// <summary>
        /// The font for words
        /// </summary>
        Font m_oFont;

        //===================================================================================================
        /// <summary>
        /// Constructs a new form object
        /// </summary>
        /// <param name="strHeading">The heading of the object</param>
        /// <param name="strFirstLanguage">The name of the first language</param>
        /// <param name="strSecondLanguage">The name of the second language</param>
        /// <param name="oWordPairs">New word pairs</param>
        //===================================================================================================
        public NewWordsInCourseForm(
            string strHeading, 
            string strFirstLanguage, 
            string strSecondLanguage, 
            IEnumerable<KeyValuePair<string, string>> oWordPairs
            )
        {

            InitializeComponent();

            // Set up font
            m_oFont = new Font(family: Font.FontFamily, emSize: 22, style: FontStyle.Regular);

            // Create a TableLayoutPanel for two columns
            var ctlTable = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(20),
            };

            ctlTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            ctlTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            // Header labels
            var lblFirstLang = new Label
            {
                Text = strFirstLanguage,
                Font = m_oFont,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Right,
                Margin = new Padding(0, 0, 30, 10)
            };
            var lblSecondLang = new Label
            {
                Text = strSecondLanguage,
                Font = m_oFont,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 0, 0, 10)
            };

            ctlTable.Controls.Add(lblFirstLang, 0, 0);
            ctlTable.Controls.Add(lblSecondLang, 1, 0);

            int nRow = 1;
            foreach (var oPair in oWordPairs)
            {
                ctlTable.RowCount++;

                var lblFirst = new Label
                {
                    Text = oPair.Key,
                    Font = m_oFont,
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleRight,
                    Anchor = AnchorStyles.Right,
                    Margin = new Padding(0, 0, 30, 5)
                };
                var lblSecond = new Label
                {
                    Text = oPair.Value,
                    Font = m_oFont,
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0, 0, 0, 5)
                };

                ctlTable.Controls.Add(lblFirst, 0, nRow);
                ctlTable.Controls.Add(lblSecond, 1, nRow);
                nRow++;
            }

            Controls.Add(ctlTable);
            Text = strHeading;

            // Center ctlTable in the form and resize form to fit ctlTable
            ctlTable.Location = ClientRectangle.Location;
            this.MinimumSize = this.Size = new Size(
                ctlTable.PreferredSize.Width + Width-ClientRectangle.Width,
                ctlTable.PreferredSize.Height + Height-ClientRectangle.Height +
                m_btnOk.Height + (ClientRectangle.Height - m_btnOk.Bottom)*2);
            this.AutoSize = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Decide, vertical or horizontal image
            if (this.MinimumSize.Width * 6 < this.MinimumSize.Height * 5)
            {
                // For islamic countries: try the islamic learning header
                if (Program.IslamicCountry &&
                    ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderMuslim.jpg"))
                {
                    return;
                }

                // the IETF language tag for decisions about the headers
                string strIetf = System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;

                // If language indicates it is eastern Asia, then try east Asia header
                if ((strIetf.StartsWith("ja") || strIetf.StartsWith("zh") || strIetf.StartsWith("ms") ||
                    strIetf.StartsWith("id") || strIetf.StartsWith("fil") || strIetf.StartsWith("ko") ||
                    strIetf.StartsWith("lo")) &&
                    ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderEastAsia.jpg"))
                {
                    return;
                }

                // If language indicates it is Africa, then try African header
                if ((strIetf.StartsWith("yo") || strIetf.StartsWith("ig") || strIetf.StartsWith("so") ||
                    strIetf.StartsWith("af") || strIetf.StartsWith("am") || strIetf.StartsWith("ig") ||
                    strIetf.StartsWith("wo") || strIetf.StartsWith("rw") || strIetf.StartsWith("ti")) &&
                    ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderAfrica.jpg"))
                {
                    return;
                }

                // If language indicates it is latin America, then try latin American header
                if ((strIetf.StartsWith("es") || strIetf.StartsWith("pt"))
                    && !strIetf.Equals("es-ES") && !strIetf.Equals("pt-PT") &&
                    ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderLatinAmerica.jpg"))
                {
                    return;
                }


                // Try the standard learning header
                if (ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeader.jpg"))
                {
                    return;
                }

                // In case of failure, then try all headers, maybe still one exists
                if (ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderEastAsia.jpg"))
                {
                    return;
                }

                if (ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderLatinAmerica.jpg"))
                {
                    return;
                }

                if (ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderMuslim.jpg"))
                {
                    return;
                }

                if (ReadyToUseSideImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressVerticalHeaderAfrica.jpg"))
                {
                    return;
                }
            }
            else
            {

                // For islamic countries: try the islamic learning header
                if (Program.IslamicCountry &&
                    ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderMuslim.jpg"))
                {
                    return;
                }

                // the IETF language tag for decisions about the headers
                string strIetf = System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;

                // If language indicates it is eastern Asia, then try east Asia header
                if ((strIetf.StartsWith("ja") || strIetf.StartsWith("zh") || strIetf.StartsWith("ms") ||
                    strIetf.StartsWith("id") || strIetf.StartsWith("fil") || strIetf.StartsWith("ko") ||
                    strIetf.StartsWith("lo")) &&
                    ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderEastAsia.jpg"))
                {
                    return;
                }

                // If language indicates it is Africa, then try African header
                if ((strIetf.StartsWith("yo") || strIetf.StartsWith("ig") || strIetf.StartsWith("so") ||
                    strIetf.StartsWith("af") || strIetf.StartsWith("am") || strIetf.StartsWith("ig") ||
                    strIetf.StartsWith("wo") || strIetf.StartsWith("rw") || strIetf.StartsWith("ti")) &&
                    ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderAfrica.jpg"))
                {
                    return;
                }

                // If language indicates it is latin America, then try latin American header
                if ((strIetf.StartsWith("es") || strIetf.StartsWith("pt"))
                    && !strIetf.Equals("es-ES") && !strIetf.Equals("pt-PT") &&
                    ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderLatinAmerica.jpg"))
                {
                    return;
                }


                // Try the standard learning header
                if (ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeader.jpg"))
                {
                    return;
                }

                // In case of failure, then try all headers, maybe still one exists
                if (ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderEastAsia.jpg"))
                {
                    return;
                }

                if (ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderLatinAmerica.jpg"))
                {
                    return;
                }

                if (ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderMuslim.jpg"))
                {
                    return;
                }

                if (ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                    "CourseProgressHorizontalHeaderAfrica.jpg"))
                {
                    return;
                }
            }

        }

        //===================================================================================================
        /// <summary>
        /// Disposes the object and its disposablee contents
        /// </summary>
        //===================================================================================================
        public void Dispose()
        {
            base.Dispose();
            m_oFont.Dispose();

            if (m_oLoadedImage != null)
            {
                m_oLoadedImage.Dispose();
            }
        }


        #region side image injection part
        //===================================================================================================
        /// <summary>
        /// Picture box control
        /// </summary>
        private PictureBox m_ctlPictureBox;
        //===================================================================================================
        /// <summary>
        /// Image
        /// </summary>
        private System.Drawing.Image m_oLoadedImage;
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
        private bool ReadyToUseSideImageInjection(
            string strImageName
            )
        {
            bool bResult = false;
            string strImagePath = System.IO.Path.Combine(Application.StartupPath, strImageName);
            if (System.IO.File.Exists(strImagePath))
            {
                m_oOriginalPositions = new Dictionary<Control, int>();
                foreach (Control ctl in Controls)
                {
                    m_oOriginalPositions[ctl] = ctl.Left;
                }

                m_ctlPictureBox = new PictureBox();
                m_ctlPictureBox.Location = this.ClientRectangle.Location;
                m_ctlPictureBox.Size = new Size(0, 0);
                Controls.Add(m_ctlPictureBox);

                LoadAndResizeSideImage(strImagePath);

                bResult = true;

                this.Resize += new EventHandler(ResizeSideImageAlongWithForm);
            }

            return bResult;
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image along with the form
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void ResizeSideImageAlongWithForm(
            object oSender,
            EventArgs oEventArgs
            )
        {
            ResizeSideImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Loads an image and resizes it to the width of client area
        /// </summary>
        /// <param name="strImagePath"></param>
        //===================================================================================================
        private void LoadAndResizeSideImage(
            string strImagePath
            )
        {
            m_oLoadedImage = Image.FromFile(strImagePath);
            ResizeSideImageAndShiftElements();
        }

        //===================================================================================================
        /// <summary>
        /// Resizes image and shifts other elements
        /// </summary>
        //===================================================================================================
        private void ResizeSideImageAndShiftElements()
        {
            if (m_oLoadedImage != null)
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    float fAspectRatio = (float)m_oLoadedImage.Width / (float)m_oLoadedImage.Height;

                    int nNewHeight = this.ClientSize.Height;
                    if (nNewHeight != 0)
                    {
                        int nNewWidth = (int)(nNewHeight * fAspectRatio);

                        int nWidthChange = nNewWidth - m_ctlPictureBox.Width;

                        this.m_ctlPictureBox.Image = new Bitmap(m_oLoadedImage, nNewWidth, nNewHeight);
                        this.m_ctlPictureBox.Size = new Size(nNewWidth, nNewHeight);

                        if (nWidthChange != 0)
                        {
                            this.Width += nWidthChange;
                            ShiftOtherElementsLeftOrRight(nWidthChange);
                        }
                    }
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Shifts elements, apart from the image box up or down
        /// </summary>
        /// <param name="nNewPictureWidth">New width of the picture</param>
        //===================================================================================================
        private void ShiftOtherElementsLeftOrRight(
            int nWidthChange
            )
        {
            foreach (Control ctl in m_oOriginalPositions.Keys)
            {
                if ((ctl.Anchor & AnchorStyles.Right) == AnchorStyles.None)
                {
                    ctl.Left += nWidthChange;
                }
                else
                    if ((ctl.Anchor & AnchorStyles.Left) != AnchorStyles.None)
                {
                    ctl.Left += nWidthChange;
                    //ctl.Width -= nWidthChange;
                }
            }
        }
        #endregion

        #region image injection part
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
            bool bResult = false;
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

                bResult = true;

                this.Resize += new EventHandler(ResizeImageAlongWithForm);
            }

            return bResult;
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
                        ShiftOtherElementsUpOrDown(nNewHeight);
                    }
                }
            }
        }

        //===================================================================================================
        /// <summary>
        /// Shifts elements, apart from the image box up or down
        /// </summary>
        /// <param name="nNewPictureHeight">New height of the picture</param>
        //===================================================================================================
        private void ShiftOtherElementsUpOrDown(
            int nNewPictureHeight
            )
        {
            foreach (Control ctl in m_oOriginalPositions.Keys)
            {
                if ((ctl.Anchor & AnchorStyles.Bottom) == AnchorStyles.None)
                    ctl.Top = m_oOriginalPositions[ctl] + nNewPictureHeight;
            }
        }
        #endregion


        //===================================================================================================
        /// <summary>
        /// Handles key down events to close the form on Space, Enter, or Escape
        /// </summary>
        /// <param name="roMsg">The reference to processed message</param>
        /// <param name="oKeyData">Key event arguments</param>
        //===================================================================================================
        protected override bool ProcessCmdKey(
            ref Message roMsg,
            Keys oKeyData
            )
        {
            if (oKeyData == Keys.Space || oKeyData == Keys.Enter || oKeyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref roMsg, oKeyData);
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks on the OK button
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void m_btnOk_Click(
            object oSender, 
            EventArgs oEventArgs
            )
        {
            this.Close();
        }
    }
}
