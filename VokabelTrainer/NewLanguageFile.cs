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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VokabelTrainer
{
    public partial class NewLanguageFile : Form
    {
        public NewLanguageFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBoxFirstLanguage_TextChanged(object sender, EventArgs e)
        {
            m_btnCreateLanguageFile.Enabled = m_tbxFirstLanguage.Text.Length > 0 && m_tbxSecondLanguage.Text.Length > 0;
        }

        private void textBoxSecondLanguage_TextChanged(object sender, EventArgs e)
        {
            m_btnCreateLanguageFile.Enabled = m_tbxFirstLanguage.Text.Length > 0 && m_tbxSecondLanguage.Text.Length > 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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

        private void m_chkFirstLanguageRTL_CheckedChanged(object sender, EventArgs e)
        {
            m_tbxFirstLanguage.RightToLeft = m_chkFirstLanguageRTL.Checked ? RightToLeft.Yes : RightToLeft.No;
        }

        private void m_chkSecondLanguageRTL_CheckedChanged(object sender, EventArgs e)
        {
            m_tbxSecondLanguage.RightToLeft = m_chkSecondLanguageRTL.Checked ? RightToLeft.Yes : RightToLeft.No;
        }
    }
}
