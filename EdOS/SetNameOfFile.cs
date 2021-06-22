using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp7.Resources
{
    public partial class SetNameOfFile : Form
    {
        public string name { get; set; }
        public SetNameOfFile()
        {
            InitializeComponent();
            name = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            this.Close();
        }
    }
}
