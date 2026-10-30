using System;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace VokabelTrainer
{

    //*******************************************************************************************************
    /// <summary>
    /// Objects of this class provide theme for the controls
    /// </summary>
    //*******************************************************************************************************
    public static class ThemeManager
    {
        //===================================================================================================
        /// <summary>
        /// Sets window attribute
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns>-1 in case of errors</returns>
        //===================================================================================================
        [DllImport("dwmapi.dll", ExactSpelling = true)]
        private static extern int DwmSetWindowAttribute(
            IntPtr hwnd,
            int attribute,
            ref int value,
            int size);


        //===================================================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oForm">Form for applying title bar</param>
        /// <param name="bDark">Indicates, if dark mode</param>
        private static void ApplyTitleBar(Form oForm, bool bDark)
        {
            if (!oForm.IsHandleCreated)
            {
                return;
            }

            int nValue = bDark ? 1 : 0;

            
            int nResult = DwmSetWindowAttribute(
                oForm.Handle, 20, ref nValue, sizeof(int));

            if (nResult < 0)
            {
                DwmSetWindowAttribute(
                    oForm.Handle, 19, ref nValue, sizeof(int));
            }
            
        }


        //===================================================================================================
        /// <summary>
        /// Attaches to a form
        /// </summary>
        /// <param name="oForm">Form to attach to</param>
        /// <exception cref="ArgumentNullException">If form is null</exception>
        //===================================================================================================
        public static void Attach(Form oForm)
        {

            if (oForm == null)
            {
                throw new ArgumentNullException(nameof(oForm));
            }

            // Bei Änderungen der Windows-Einstellungen neu anwenden.
            UserPreferenceChangedEventHandler dHandler = (oSender, oArgs) =>
            {
                if (oForm.IsDisposed || !oForm.IsHandleCreated)
                {
                    return;
                }

                try
                {
                    oForm.BeginInvoke((Action)(() =>
                    {
                        if (!oForm.IsDisposed)
                            Apply(oForm);
                    }));
                }
                catch (InvalidOperationException)
                {
                    // ignore
                }
            };

            /*
            SystemEvents.UserPreferenceChanged += dHandler;

            // Wichtig: statisches Ereignis wieder abmelden.
            oForm.Disposed += (oSender, oArgs) =>
            {
                SystemEvents.UserPreferenceChanged -= dHandler;
            };
            */

            oForm.HandleCreated += (oSender, oArgs) =>
            {
                Apply(oForm);
            };

            Apply(oForm);
        }

        //===================================================================================================
        /// <summary>
        /// Applies theme to the whole control tree
        /// </summary>
        /// <param name="ctlRoot">The root of the control tree</param>
        //===================================================================================================
        public static void Apply(Control ctlRoot)
        {
            // Kontrastdesigns haben Vorrang vor dem Dunkelmodus.
            bool bDark = !SystemInformation.HighContrast && ReadDarkMode();

            ApplyControl(ctlRoot, bDark);

            Form oForm = ctlRoot as Form;

            if (oForm != null)
            {
                ApplyTitleBar(oForm, bDark);
            }

            ctlRoot.Invalidate(true);
        }

        //===================================================================================================
        /// <summary>
        /// Reads, if the control shall 
        /// </summary>
        /// <returns></returns>
        //===================================================================================================
        private static bool ReadDarkMode()
        {
            bool bDarkFromColor = SystemColors.ControlText.GetBrightness() >
                SystemColors.Control.GetBrightness();

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    object oValue = key?.GetValue("AppsUseLightTheme");
                    return oValue is int && (int)oValue == 0;
                }
            }
            catch (SecurityException)
            { 
                return bDarkFromColor; 
            }
            catch (UnauthorizedAccessException)
            { 
                return bDarkFromColor; 
            }
            catch (IOException)
            { 
                return false; 
            }
        }

        //===================================================================================================
        /// <summary>
        /// Applies the theme to the control
        /// </summary>
        /// <param name="oControl"></param>
        /// <param name="bDark"></param>
        //===================================================================================================
        private static void ApplyControl(Control oControl, bool bDark)
        {
            // Only adapt colors, if in dark mode (works only once)
            if (!bDark)
                return;

            Color background = bDark
                ? Color.FromArgb(20, 20, 20)
                : SystemColors.Control;

            Color foreground = bDark
                ? Color.FromArgb(240, 240, 240)
                : SystemColors.ControlText;

            Color inputBackground = bDark
                ? Color.FromArgb(45, 45, 45)
                : SystemColors.Window;

            Color inputForeground = bDark
                ? Color.FromArgb(240, 240, 240)
                : SystemColors.WindowText;

            Color buttonBackground = bDark
                ? Color.FromArgb(45, 45, 45)
                : SystemColors.ControlLight;

            Color buttonForeground = bDark
                ? Color.FromArgb(240, 240, 240)
                : SystemColors.ControlText;

            
            bool hasVokabelTrainerParentOrSelf = false;
            Control ctlParent = oControl;

            while (ctlParent != null)
            {
                if (ctlParent is VokabelTrainer)
                {
                    hasVokabelTrainerParentOrSelf = true;
                    break;
                }

                ctlParent = ctlParent.Parent;
            }

            if (hasVokabelTrainerParentOrSelf)
            {
                Color swappedColor = background;
                background = inputBackground;
                inputBackground = swappedColor;
            }
            



            if (oControl.ForeColor.B > oControl.ForeColor.R * 2)
                oControl.BackColor = oControl.Parent.BackColor;
            else
                oControl.BackColor = background;

            if (oControl.ForeColor.B > oControl.ForeColor.R * 2)
                oControl.ForeColor = Color.FromArgb(110, 180, 255);
            else
                oControl.ForeColor = foreground;


            Button btnButton = oControl as Button;

            if (
                oControl is TextBoxBase ||
                oControl is ComboBox ||
                oControl is ListBox ||
                oControl is UpDownBase)
            {
                oControl.BackColor = inputBackground;
                oControl.ForeColor = inputForeground;
            }


            if (btnButton != null)
            {
                if (bDark)
                {
                    btnButton.FlatStyle = FlatStyle.Flat;

                    btnButton.UseVisualStyleBackColor = false;

                    if (oControl.ForeColor.B > oControl.ForeColor.R * 2)
                        btnButton.BackColor = background;
                    else
                        btnButton.BackColor = inputBackground;

                    btnButton.FlatAppearance.BorderColor =
                        Color.FromArgb(90, 90, 90);

                    if (btnButton.FlatAppearance.MouseOverBackColor == SystemColors.Control)
                    {
                        btnButton.FlatAppearance.MouseOverBackColor = background;
                    }

                    if (btnButton.FlatAppearance.MouseDownBackColor == SystemColors.Control)
                    {
                        btnButton.FlatAppearance.MouseDownBackColor = background;
                    }

                }
            }

            LinkLabel btnLink = oControl as LinkLabel;

            if (btnLink != null)
            {
                btnLink.LinkColor = bDark
                    ? Color.FromArgb(110, 180, 255)
                    : SystemColors.HotTrack;

                btnLink.ActiveLinkColor = bDark
                    ? Color.FromArgb(170, 210, 255)
                    : SystemColors.HotTrack;

                btnLink.VisitedLinkColor = bDark
                    ? Color.FromArgb(200, 160, 255)
                    : SystemColors.HotTrack;

                btnLink.DisabledLinkColor = bDark
                    ? Color.FromArgb(150, 150, 150)
                    : SystemColors.GrayText;
            }

            ComboBox ctlCombo = oControl as ComboBox;

            if (ctlCombo != null)
            {
                ctlCombo.BackColor = inputBackground;
                ctlCombo.ForeColor = inputForeground;

                if (ctlCombo.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    ctlCombo.DrawMode = DrawMode.OwnerDrawFixed;

                    // Bei erneutem Anwenden nicht mehrfach registrieren.
                    ctlCombo.DrawItem -= DrawComboBoxItem;
                    ctlCombo.DrawItem += DrawComboBoxItem;
                }

                ctlCombo.Invalidate();
            }

            foreach (Control ctlChild in oControl.Controls)
                ApplyControl(ctlChild, bDark);
        }

        //===================================================================================================
        /// <summary>
        /// Draws combo box
        /// </summary>
        /// <param name="oSender">Sender object</param>
        /// <param name="oEventArgs">Event args</param>
        //===================================================================================================
        private static void DrawComboBoxItem(
            object oSender, 
            DrawItemEventArgs oEventArgs
            )
        {
            ComboBox combo = (ComboBox)oSender;

            bool selected =
                (oEventArgs.State & DrawItemState.Selected) != 0;

            Color background = selected
                ? SystemColors.Highlight
                : combo.BackColor;

            Color foreground = !combo.Enabled
                ? SystemColors.GrayText
                : selected
                    ? SystemColors.HighlightText
                    : combo.ForeColor;

            using (SolidBrush brush = new SolidBrush(background))
            {
                oEventArgs.Graphics.FillRectangle(brush, oEventArgs.Bounds);
            }

            string text = oEventArgs.Index >= 0
                ? combo.GetItemText(combo.Items[oEventArgs.Index])
                : combo.Text;

            Rectangle bounds = oEventArgs.Bounds;
            bounds.Inflate(-2, 0);

            TextFormatFlags flags =
                TextFormatFlags.Left |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine |
                TextFormatFlags.EndEllipsis |
                TextFormatFlags.NoPrefix;

            if (combo.RightToLeft == RightToLeft.Yes)
            {
                flags |= TextFormatFlags.RightToLeft |
                         TextFormatFlags.Right;
            }

            TextRenderer.DrawText(
                oEventArgs.Graphics, text, oEventArgs.Font ?? combo.Font,
                bounds, foreground, flags);

            oEventArgs.DrawFocusRectangle();
        }
    }
}
