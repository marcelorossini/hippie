﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            Close();
        }

        private void formSettings_Load(object sender, EventArgs e)
        {
            var garmentPath = FileHelpers.FindGarmentCreatorDir();
            textBoxGarmentCreatorPath.Text = garmentPath;
        }
    }
}
