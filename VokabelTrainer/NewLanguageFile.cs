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
    }
}
