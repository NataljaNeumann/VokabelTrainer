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

            int row = 1;
            foreach (var pair in oWordPairs)
            {
                ctlTable.RowCount++;

                var lblFirst = new Label
                {
                    Text = pair.Key,
                    Font = m_oFont,
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleRight,
                    Anchor = AnchorStyles.Right,
                    Margin = new Padding(0, 0, 30, 5)
                };
                var lblSecond = new Label
                {
                    Text = pair.Value,
                    Font = m_oFont,
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0, 0, 0, 5)
                };

                ctlTable.Controls.Add(lblFirst, 0, row);
                ctlTable.Controls.Add(lblSecond, 1, row);
                row++;
            }

            Controls.Add(ctlTable);
            Text = strHeading;

            // Center ctlTable in the form and resize form to fit ctlTable
            ctlTable.Location = ClientRectangle.Location;
            this.MinimumSize = this.Size = new Size(
                ctlTable.PreferredSize.Width + Width-ClientRectangle.Width,
                ctlTable.PreferredSize.Height + Height-ClientRectangle.Height);
            this.AutoSize = false;
            this.StartPosition = FormStartPosition.CenterParent;

            ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar + 
                "CourseProgressHorizontalHeader.jpg");
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
