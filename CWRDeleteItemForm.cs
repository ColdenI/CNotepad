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
    public partial class CWRDeleteItemForm : Form
    {
        public CWRDeleteItemForm()
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

        private string[] GetItems()
        {
            MainForm main = this.Owner as MainForm;
            string _text = main.textBox1.Text;


            string[] i_item_1_a = _text.Split('/');
            List<string> i_item_1_l = new List<string>();

            foreach (string i in i_item_1_a)
                i_item_1_l.Add(i.Split('>')[0]);

            i_item_1_l.RemoveAt(0);

            string[] i_item_2_a = new string[i_item_1_l.Count];
            for (int i = 0; i < i_item_1_l.Count; i++)
            {
                i_item_2_a[i] = i_item_1_l[i];
            }
            return i_item_2_a;
        }

        public bool ContainsItem(string Item)
        {
            foreach (string i in this.GetItems())
            {
                if (i == Item)
                    return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            string _text = main.textBox1.Text;
            string _item = textBox1.Text;
            string res = "";          

            if (ContainsItem(textBox1.Text) && _text.Contains("<"+_item+">") && _text.Contains("</"+_item+">"))
            {
                res = _text.Remove(_text.IndexOf("<" + _item + ">"));
                string _st = Reverse(_text);
                _st = _st.Remove(_st.IndexOf(Reverse("</" + _item + ">")));
                res += Reverse(_st);
                main.textBox1.Text = res;
                this.Close();
            }
            else
            {
                MessageBox.Show("Элемент не найден!", "CWR - Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
