using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNotepad
{
    public partial class CWRViewAllItemsForm : Form
    {
        public CWRViewAllItemsForm()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateWindowData();
        }

        private void CWRViewAllItemsForm_Load(object sender, EventArgs e)
        {
            UpdateWindowData();
        }

        private void UpdateWindowData()
        {
            MainForm main = this.Owner as MainForm;
            string _text = main.textBox1.Text;


            string[] i_item_1_a = _text.Split('/');
            List<string> i_item_1_l = new List<string>();

            foreach (string i in i_item_1_a)
                i_item_1_l.Add(i.Split('>')[0]);

            i_item_1_l.RemoveAt(0);

            listBox1.Items.Clear();
            foreach (string i in i_item_1_l) {
                listBox1.Items.Add(i);
            }
            
            
        }
    }
}
