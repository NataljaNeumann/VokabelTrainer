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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
        /// Constructs a new form
        /// </summary>
        /// <param name="bUseESpeak">Indicates that eSpeak shall be used</param>
        /// <param name="strEspeakPath">eSpeak path</param>
        //===================================================================================================
        public NewDictionaryPair(bool bUseESpeak, string strEspeakPath)
        {
            InitializeComponent();

            m_bUseESpeak = bUseESpeak;
            m_strEspeakPath = strEspeakPath;

        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user click "next"
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void buttonNext_Click(object oSender, EventArgs oArgs)
        {
            if (!m_bAlreadySaid && textBoxSecondLanguage.Text.Length > 0)
            {
                Speaker.Say(m_lblSecondLanguage.Text, textBoxSecondLanguage.Text, true, 
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
        private void m_btnEnteredLast_Click(object oSender, EventArgs oArgs)
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
        private void m_btnCancel_Click(object oSender, EventArgs oArgs)
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
        private void NewDictionaryPair_Shown(object oSender, EventArgs oArgs)
        {
            if (m_tbxFirstLanguage.Text.Trim().Length == 0)
                m_tbxFirstLanguage.Focus();
            else
                if (textBoxSecondLanguage.Text.Trim().Length == 0)
                    textBoxSecondLanguage.Focus();
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when focus leaves first language box
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oArgs">Event args</param>
        //===================================================================================================
        private void textBoxFirstLanguage_Leave(object oSender, EventArgs oArgs)
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
        private void textBoxSecondLanguage_Leave(object oSender, EventArgs oArgs)
        {
            Speaker.Say(m_lblSecondLanguage.Text, textBoxSecondLanguage.Text, true, 
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
        private void textBoxSecondLanguage_TextChanged(object oSender, EventArgs oArgs)
        {
            m_bAlreadySaid = false;
        }
    }
}
