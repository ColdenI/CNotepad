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
    public partial class CWRGetValueForm : Form
    {
        public CWRGetValueForm()
        {
            InitializeComponent();
        }

        public string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }

        

        private string GetValueItem(string item)
        {
            MainForm main = this.Owner as MainForm;
            string _text = main.textBox1.Text;
            string res = "";

            if (_text.Contains("<" + item + ">") && _text.Contains("</" + item + ">"))
            {
                res = _text.Remove(_text.IndexOf("</" + item + ">"));
                res = Reverse(res);
                res = res.Remove(res.IndexOf(Reverse("<" + item + ">")));
                res = Reverse(res);

            }
            else
            {
                MessageBox.Show("Элемент ненайден или повреждён!", "CWR Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = GetValueItem(textBox1.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
