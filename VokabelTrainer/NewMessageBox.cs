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
using System.Data;
using System.Drawing;
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

        //===================================================================================================
        /// <summary>
        /// Constructs a new message box
        /// </summary>
        //===================================================================================================
        public NewMessageBox()
        {
            InitializeComponent();
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
            if (m_oOldSize != null)
            {
                Size = new Size(Size.Width + ((Label)oSender).Size.Width - m_oOldSize.Width,
                                Size.Height + ((Label)oSender).Size.Height - m_oOldSize.Height);
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
        public static DialogResult Show(IWin32Window iOwner, string strMessage, string strHeader, string strSay)
        {
            using (NewMessageBox mb = new NewMessageBox())
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
    }
}
