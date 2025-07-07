using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SingBoxWrapper
{
    public class MainForm : Form
    {
        private Button btnChoose;
        private Button btnStart;
        private Button btnStop;
        private Label lblConfig;
        private string configPath = "";
        private Process singBoxProcess;

        public MainForm()
        {
            this.Text = "SingBox Wrapper";
            this.Size = new System.Drawing.Size(400, 200);

            btnChoose = new Button { Text = "Выбрать конфиг", Left = 10, Top = 10, Width = 150 };
            btnStart = new Button { Text = "Старт", Left = 10, Top = 50, Width = 150 };
            btnStop = new Button { Text = "Стоп", Left = 10, Top = 90, Width = 150, Enabled = false };
            lblConfig = new Label { Text = "Файл не выбран", Left = 180, Top = 15, Width = 200 };

            btnChoose.Click += BtnChoose_Click;
            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;

            Controls.Add(btnChoose);
            Controls.Add(btnStart);
            Controls.Add(btnStop);
            Controls.Add(lblConfig);
        }

        private void BtnChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                configPath = dialog.FileName;
                lblConfig.Text = Path.GetFileName(configPath);
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(configPath) || !File.Exists(configPath))
            {
                MessageBox.Show("Выберите файл конфигурации.");
                return;
            }

            singBoxProcess = new Process();
            singBoxProcess.StartInfo.FileName = "sing-box.exe";
            singBoxProcess.StartInfo.Arguments = $"-c \"{configPath}\" run";
            singBoxProcess.StartInfo.UseShellExecute = false;
            singBoxProcess.StartInfo.CreateNoWindow = true;

            try
            {
                singBoxProcess.Start();
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка запуска: " + ex.Message);
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (singBoxProcess != null && !singBoxProcess.HasExited)
                {
                    singBoxProcess.Kill();
                    singBoxProcess.Dispose();
                }

                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка остановки: " + ex.Message);
            }
        }
    }
}
