// Vokabel-Trainer v1.2
// Copyright (C) 2019 NataljaNeumann@gmx.de
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
    public partial class NewDictionaryPair : Form
    {
        bool _bAlreadySaid;

        public NewDictionaryPair()
        {
            InitializeComponent();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (!_bAlreadySaid && textBoxSecondLanguage.Text.Length > 0)
            {
                Speaker.Say(m_lblSecondLanguage.Text, textBoxSecondLanguage.Text, true);
                _bAlreadySaid = true;
            }

            DialogResult = DialogResult.Retry;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void NewDictionaryPair_Shown(object sender, EventArgs e)
        {
            if (m_tbxFirstLanguage.Text.Trim().Length == 0)
                m_tbxFirstLanguage.Focus();
            else
                if (textBoxSecondLanguage.Text.Trim().Length == 0)
                    textBoxSecondLanguage.Focus();
        }

        private void textBoxFirstLanguage_Leave(object sender, EventArgs e)
        {
            Speaker.Say(m_lblFirstLanguage.Text, m_tbxFirstLanguage.Text, true);
        }

        private void textBoxSecondLanguage_Leave(object sender, EventArgs e)
        {
            Speaker.Say(m_lblSecondLanguage.Text, textBoxSecondLanguage.Text, true);
            _bAlreadySaid = true;
        }

        private void textBoxSecondLanguage_TextChanged(object sender, EventArgs e)
        {
            _bAlreadySaid = false;
        }
    }
}
