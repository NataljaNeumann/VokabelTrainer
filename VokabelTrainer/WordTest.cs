// VokabelTrainer v1.2
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
    /// Form for testing single words
    /// </summary>
    //*******************************************************************************************************
    public partial class WordTest : Form
    {
        //===================================================================================================
        /// <summary>
        /// Constructs a new word test object
        /// </summary>
        //===================================================================================================
        public WordTest()
        {
            InitializeComponent();
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when text has changed in text box
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void textBox1_TextChanged(object oSender, EventArgs oEventArgs)
        {
            m_btnNext.Enabled = m_btnLast.Enabled = m_tbxAskedTranslation.Text.Length > 0;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when 'next' button is clicked
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void buttonNext_Click(object oSender, EventArgs oEventArgs)
        {
            DialogResult = DialogResult.Retry;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when 'last' button is clicked
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void buttonLast_Click(object oSender, EventArgs oEventArgs)
        {
            DialogResult = DialogResult.OK;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when 'cancel' button is clicked
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void buttonCancel_Click(object oSender, EventArgs oEventArgs)
        {
            DialogResult = DialogResult.Cancel;
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when the form is shown
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private void WordTest_Shown(object oSender, EventArgs oEventArgs)
        {

        }
    }
}
