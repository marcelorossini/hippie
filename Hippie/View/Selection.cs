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
        private static FormMain _mainForm;


        public FormSelection(FormMain mainForm, string frontFile, string backFile)
        {
            _mainForm = mainForm;
            _frontFile = frontFile;
            _backFile = backFile;
            InitializeComponent();
        }

        private void formSelecion_Load(object sender, EventArgs e)
        {

        }

        private void formSelecion_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressAction(e);
        }

        private void buttonFront_Click(object sender, EventArgs e)
        {
            Automation.OpenFile(_frontFile, _mainForm).Wait();
            Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Automation.OpenFile(_backFile, _mainForm).Wait();
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonFront_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressAction(e);
        }

        private void buttonBack_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressAction(e);
        }

        private void KeyPressAction(KeyPressEventArgs e)
        {
            if (e.KeyChar == 49)
            {
                Hide();
                Automation.OpenFile(_frontFile, _mainForm).Wait();
            }
            else if (e.KeyChar == 50)
            {
                Hide();
                Automation.OpenFile(_backFile, _mainForm).Wait();
            }
            Close();
        }


    }
}
