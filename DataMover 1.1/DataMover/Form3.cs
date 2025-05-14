using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Net.Mime.MediaTypeNames;

namespace DataMover
{
    public partial class Form3 : Form
    {
        public static string text;
        public Form3()
        {
            InitializeComponent();
            label1.Text = "DataMover can be used to automatically create dated folders and sub folders.";
            label2.Text = "You can transfer data from one folder to another and back.";
            label3.Text = "DataMover saves the choices from the current creation and transfer in a txt.";
            label4.Text = "DataMover autofills text fields on start up after a working folder with a txt save file is chosen.";



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
