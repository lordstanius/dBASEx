using System.Windows.Forms;

namespace dBASEDiffGUI
{
    public partial class EmailInput : Form
    {
        public EmailInput()
        {
            InitializeComponent();
        }

        private void EmailInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
