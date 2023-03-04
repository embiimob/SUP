﻿using SUP.P2FK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUP
{
    public partial class ObjectDetailsControl : UserControl
    {
        public ObjectDetailsControl(string address = "")
        {
            InitializeComponent();

            ObjectDetails control = new ObjectDetails(address, true);
            control.TopLevel = false;
            control.Visible = true;
            control.ControlBox = false; // Remove minimize, maximize, and close buttons
            panel1.Controls.Add(control);

        }
    }
}
