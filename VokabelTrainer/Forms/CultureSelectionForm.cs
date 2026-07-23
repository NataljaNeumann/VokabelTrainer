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
    /// This form allows user to choose one of the available GUI languages/cultures
    /// </summary>
    //*******************************************************************************************************
    public partial class CultureSelectionForm : Form
    {
        /*
         PSEUDOCODE / PLAN (detailed):
         1. Validate 'oLanguages' is not null (existing behavior).
         2. Convert 'oLanguages' to a List<KeyValuePair<string,string>> named 'items' for binding.
         3. Configure combo box: DisplayMember = "Value", ValueMember = "Key", DataSource = new BindingSource(items, null).
         4. Selection strategy (in order):
            a. If the current property 'SelectedLanguage' is not null/empty AND there exists an item
               in 'items' whose Key equals 'SelectedLanguage' (case-insensitive), then set the combo
               selection to that key and assign the canonical Key from the list to 'SelectedLanguage'.
            b. Otherwise, get the current thread UI culture (System.Threading.Thread.CurrentThread.CurrentUICulture).
               Try to find an item where Key equals culture.Name (case-insensitive).
               If not found, try culture.TwoLetterISOLanguageName.
               If a match is found, set combo selection to that key and assign it to 'SelectedLanguage'.
            c. If neither step (a) nor (b) matched, fall back to previous behavior:
               - If combo has at least one item and SelectedItem is a KeyValuePair<string,string>, use its Key.
               - Otherwise set SelectedLanguage = null.
         5. Setting 'm_cbLanguage.SelectedValue' will update the combo selection; explicitly assign 'SelectedLanguage'
            to the canonical key from the matched item to ensure exact casing/format.
        */
        public CultureSelectionForm(
            IDictionary<string, string> oLanguages
            )
        {
            InitializeComponent();

            if (oLanguages == null)
            {
                throw new ArgumentNullException(nameof(oLanguages));
            }

            oLanguages.Add("", "");

            // Convert dictionary to a list so it can be safely bound to the ComboBox.
            var items = oLanguages
                .Select(kvp => new KeyValuePair<string, string>(kvp.Key, kvp.Value))
                .ToList();

            // Configure ComboBox to display language names and return keys as values.
            m_cbLanguage.DisplayMember = "Value";
            m_cbLanguage.ValueMember = "Key";
            m_cbLanguage.DataSource = new BindingSource(items, null);

            this.m_cbLanguage.SelectedIndexChanged += new System.EventHandler(this.OnLanguage_SelectedIndexChanged);

            // Selection logic:
            // 1) If SelectedLanguage already set and present in items -> select it.
            if (!string.IsNullOrEmpty(SelectedLanguage) &&
                items.Any(kvp => string.Equals(kvp.Key, SelectedLanguage, StringComparison.OrdinalIgnoreCase)))
            {
                // Use the exact key from the list to ensure canonical value.
                var canonical = items.First(kvp => string.Equals(kvp.Key, SelectedLanguage, StringComparison.OrdinalIgnoreCase)).Key;
                m_cbLanguage.SelectedValue = canonical;
                SelectedLanguage = canonical;
            }
            else
            {
                // 2) Try to match current UI culture (Name, then two-letter code).
                var uiCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
                KeyValuePair<string, string> found = default(KeyValuePair<string, string>);

                if (uiCulture != null)
                {
                    found = items.FirstOrDefault(kvp => string.Equals(kvp.Key, uiCulture.Name, StringComparison.OrdinalIgnoreCase));

                    if (found.Key == null)
                    {
                        found = items.FirstOrDefault(kvp => string.Equals(kvp.Key, uiCulture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase));
                    }
                }

                if (found.Key != null)
                {
                    m_cbLanguage.SelectedValue = found.Key;
                    SelectedLanguage = found.Key;
                }
                else
                {
                    // 3) Fallback to previous initialization behavior.
                    if (m_cbLanguage.Items.Count > 0 &&
                        m_cbLanguage.SelectedItem is KeyValuePair<string, string> initialKvp)
                    {
                        SelectedLanguage = initialKvp.Key;
                    }
                    else
                    {
                        SelectedLanguage = null;
                    }
                }
            }

            ReadyToUseImageInjection("Images" + System.IO.Path.DirectorySeparatorChar +
                 "CultureSelectionHeader.jpg");
        }

        //===================================================================================================
        /// <summary>
        /// Gets the code of selected language
        /// </summary>
        public string SelectedLanguage
        {
            get; private set;
        }



        //===================================================================================================
        /// <summary>
        /// This is executed when selected index in combo changes
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnLanguage_SelectedIndexChanged(
            object oSender,
            EventArgs oArgs
            )
        {
            if (m_cbLanguage.SelectedItem is KeyValuePair<string, string> kvp)
            {
                SelectedLanguage = kvp.Key;
            }
            else
            {
                SelectedLanguage = null;
            }
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks OK
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnOk_Click(
            object oSender, 
            EventArgs oArgs
            )
        {
            DialogResult = DialogResult.OK;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks OK
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void OnCancel_Click(
            object oSender, 
            EventArgs oArgs
            )
        {
            DialogResult= DialogResult.Cancel;
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
