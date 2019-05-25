using System;
using System.Windows.Forms;

namespace dBASEDiffGUI
{
    public partial class MainWindow : Form
    {
        private readonly DiffHandler _diff = new DiffHandler();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAddFolder_Click(object sender, EventArgs e)
        {
            if (dlgBrowseFolder.ShowDialog() != DialogResult.OK)
                return;

            if (!lbTracked.Items.Contains(dlgBrowseFolder.SelectedPath))
                lbTracked.Items.Add(dlgBrowseFolder.SelectedPath);

            btnStartTracking.Enabled = lbTracked.Items.Count > 0;
        }

        private void LbTracked_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && lbTracked.SelectedItem != null)
                listBoxContext.Show(lbTracked, e.X, e.Y);
        }

        private void RemoveFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbTracked.Items.Remove(lbTracked.SelectedItem);
            btnStartTracking.Enabled = lbTracked.Items.Count > 0;
        }

        private void BtnStartTracking_Click(object sender, EventArgs e)
        {
            if (!_diff.IsTracking)
                StartTracking();
            else
                StopTracking();
        }

        private void StartTracking()
        {
            foreach (var item in lbTracked.Items)
                _diff.Enumerate(item as string);

            _diff.IsTracking = true;
            btnStartTracking.Text = "Stop tracking";
            btnStartTracking.Image = Properties.Resources.Stop_16x;
        }

        private void StopTracking()
        {
            _diff.IsTracking = false;
            btnStartTracking.Text = "Start tracking";
            btnStartTracking.Image = Properties.Resources.Run_16x;

            using (var input = new EmailInput())
            {
                input.Owner = this;
                try
                {
                    if (input.ShowDialog() != DialogResult.OK)
                        return;

                    Cursor = Cursors.WaitCursor;

                    _diff.SendResult(input.txtEmail.Text, input.txtPass.Text);

                    MessageBox.Show(
                        $"Result sent to {input.txtEmail.Text}",
                        "Success!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Sending result failed with exception:",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                }
                finally
                {
                    Cursor = Cursors.Default;
                    _diff.Cleanup();
                }
            }
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _diff.Cleanup();
        }
    }
}
