using System.Linq;
using System.Windows.Forms;

namespace JasonsRPGCharacterTool.Global_stuff
{
    class lockControls
    {
        public static void lockUnlockCont(bool isLocked, Panel parentPanel)
        {
            if (isLocked)
            {
                foreach (NumericUpDown nUpDown in parentPanel.Controls.OfType<NumericUpDown>())
                {
                    if (nUpDown.Tag != null && nUpDown.Tag.ToString() == "Lockable")
                    {
                        nUpDown.Enabled = false;
                    }
                }

                foreach (Button btn in parentPanel.Controls.OfType<Button>())
                {
                    if (btn.Tag != null && btn.Tag.ToString() == "Lockable")
                    {
                        btn.Enabled = false;
                    }
                }

                foreach (CheckBox chckBox in parentPanel.Controls.OfType<CheckBox>())
                {
                    if (chckBox.Tag != null && chckBox.Tag.ToString() == "Lockable")
                    {
                        chckBox.Enabled = false;
                    }
                }
            }
            else
            {
                foreach (NumericUpDown nUpDown in parentPanel.Controls.OfType<NumericUpDown>())
                {
                    if (nUpDown.Tag != null && nUpDown.Tag.ToString() == "Lockable")
                    {
                        nUpDown.Enabled = true;
                    }
                }

                foreach (Button btn in parentPanel.Controls.OfType<Button>())
                {
                    if (btn.Tag != null && btn.Tag.ToString() == "Lockable")
                    {
                        btn.Enabled = true;
                    }
                }

                foreach (CheckBox chckBox in parentPanel.Controls.OfType<CheckBox>())
                {
                    if (chckBox.Tag != null && chckBox.Tag.ToString() == "Lockable")
                    {
                        chckBox.Enabled = true;
                    }
                }
            }
        }
    }
}
