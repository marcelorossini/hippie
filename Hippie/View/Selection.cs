using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hippie.Repositories;
using System.Windows.Forms;

namespace Hippie.View
{
    public partial class FormSelection : Form
    {
        private static string _frontFile;
        private static string _backFile;

        public FormSelection(string frontFile, string backFile)
        {
            _frontFile = frontFile;
            _backFile = backFile;
            InitializeComponent();
        }

        private void formSelecion_Load(object sender, EventArgs e)
        {

        }

        private void formSelecion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 1)
                buttonFront.PerformClick();
            else if (e.KeyChar == 2)
                buttonFront.PerformClick();
        }

        private void buttonFront_Click(object sender, EventArgs e)
        {
            Automation.OpenFile(_frontFile, null).Wait();
            Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Automation.OpenFile(_backFile, null).Wait();
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
