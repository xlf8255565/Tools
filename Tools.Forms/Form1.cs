using Pos.Component.Helpers;
using System.Windows.Forms;

namespace Tools.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HotkeyHelper2.RegisterHotkey(this.Handle, Keys.F1, () =>
            {

            });
        }
    }
}