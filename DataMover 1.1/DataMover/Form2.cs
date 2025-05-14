using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMover
{
    public partial class Form2 : Form
    {
        public static string text;
        public Form2()
        {
            InitializeComponent();
            label1.Text = text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
