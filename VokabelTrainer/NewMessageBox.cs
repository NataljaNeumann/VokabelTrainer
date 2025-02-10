using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VokabelTrainer
{
    public partial class NewMessageBox : Form
    {
        System.Drawing.Size _oldSize;

        public NewMessageBox()
        {
            InitializeComponent();
        }

        private void NewMessageBox_Shown(object sender, EventArgs e)
        {
            _oldSize = m_lblMessageText.Size;
            m_lblMessageText.AutoSize = true;
        }

        private void label1_SizeChanged(object sender, EventArgs e)
        {
            if (_oldSize != null)
            {
                //button1.Location = new Point(button1.Location.X + ((Label)sender).Size.Width - _oldSize.Width,
                //    button1.Location.Y + ((Label)sender).Size.Height - _oldSize.Height);
                Size = new Size(Size.Width + ((Label)sender).Size.Width - _oldSize.Width,
                                Size.Height + ((Label)sender).Size.Height - _oldSize.Height);
            }
        }

        public static DialogResult Show(IWin32Window owner, string message, string header, string say)
        {
            using (NewMessageBox mb = new NewMessageBox())
            {
                mb.m_lblMessageText.Text = message;
                mb.Text = header;

                /*
                if (!string.IsNullOrEmpty(say))
                    Speaker.Say("Deutsch", say, true, m_chkUseESpeak.Checked, m_tbxESpeakPath);
                 */

                return mb.ShowDialog(owner);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
