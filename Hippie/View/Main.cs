using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using Hippie.Repositories;
using Hippie.View;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Hippie
{
    public partial class FormMain : Form
    {
        private List<FileInfo> _files;
        public FormMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
       
        private async void button1_Click(object sender, EventArgs e)
        {
            string filename = dataGridView.CurrentCell.Value.ToString();
            string fileFullPath = _files.Find(i => i.Name == filename).FullName;
            await Automation.OpenFile(fileFullPath, this);
        }

        private void Main_Load(object sender, EventArgs e)
        {            
            Settings.Read();

            if (string.IsNullOrEmpty(Settings.Current.DefaultDir))
            {
                buttonSettings.PerformClick();
            }
            
            _files = FileHelpers.GetAllFiles(Settings.Current.DefaultDir, "*.gcr");
            foreach (var file in _files)
                dataGridView.Rows.Add(file.Name);

            dataGridView.Columns[0].HeaderText = Settings.Current.DefaultDir;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://soluplim.com.br/");
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            buttonPrint.Enabled = true;
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            var form = new formSettings();
            form.ShowDialog();  
        }

        private async void textBoxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {             
                string text = Helpers.RemoveAccents(textBoxCode.Text.Trim().ToLower())+" ";
                var textsplit = text.Split('-');
                text = textsplit.Count() > 0 ? textsplit[0] : text;
                var foundFiles = _files.Where(i => Helpers.RemoveAccents(i.FullName.ToLower()).Contains(text)).ToList();
                textBoxCode.Text = "";

                if (foundFiles.Count() == 1)
                {
                    await Automation.OpenFile(foundFiles.First().FullName, this);
                    return;
                }
                else if (foundFiles.Count() == 2)
                {
                    List<string> words = new List<string>() { "verso", "costa", "tras", "traz"};
                    bool CheckBackFile(string filename)
                    {
                        filename = Helpers.RemoveAccents(filename.Trim().ToLower());
                        foreach (string word in words)
                        {
                            if (filename.Contains(word))
                                return true;
                        }
                        return false;
                    }

                    var backFile = foundFiles.FirstOrDefault(i => CheckBackFile(i.FullName));
                    var frontFile = foundFiles.FirstOrDefault(i => !CheckBackFile(i.FullName));

                    if (backFile == null || frontFile == null)
                    {
                        MessageBox.Show("Arquivos fora do padrão, não foi possivel executar a automação!");
                        return;
                    }

                    Form form = new FormSelection(this, frontFile.FullName, backFile.FullName);
                    form.ShowDialog();
                    return;
                }
                else if (foundFiles.Count() > 2)
                {
                    MessageBox.Show("Arquivos fora do padrão, não foi possivel executar a automação!");
                    return;
                }
                MessageBox.Show("Nenhum arquivo encontrado com esse filtro!");
                return;
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            textBoxCode.Focus();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormMain_ResizeEnd(object sender, EventArgs e)
        {
            textBoxCode.Focus();
        }
    }
}
