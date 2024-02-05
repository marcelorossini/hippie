using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hippie.Models;
using Hippie.Repositories;

namespace Hippie.View
{
    public partial class formSettings : Form
    {
        public formSettings()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting();
            setting.DefaultDir = textBox1.Text;
            Settings.Save(setting);
            //Close();
            Application.Restart();
        }

        private void formSettings_Load(object sender, EventArgs e)
        {
            textBox1.Text = Settings.Current.DefaultDir;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Cria uma instância do FolderBrowserDialog
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // Define o título do diálogo
            folderBrowserDialog.Description = "Selecione uma pasta";

            // Exibe o diálogo e verifica se o usuário clicou em OK
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog.SelectedPath;
            }
            else
            {
                MessageBox.Show("Por favor selecione a parta com arquivos das estampas");
            }
        }
    }
}
