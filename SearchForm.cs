using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CNotepad
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        private void SearchForm_Shown(object sender, EventArgs e) // Событие при открытии формы поиска и замены
        {
            tbFind.Focus();
        }

        int findCutLength = 0; // На сколько символов обрезаем текст для поиска

        private void tbFind_TextChanged(object sender, EventArgs e) // Cобытие при изменении текста в tbFind
        {
            findCutLength = 0;
        }

        private void tbReplace_TextChanged(object sender, EventArgs e) // Событие при изменении текста в tbReplace
        {
            findCutLength = 0;
        }

        private void cbReg_CheckStateChanged(object sender, EventArgs e) // Событие при изменении статуса cbReg
        {
            findCutLength = 0;
        }

        private void SearchForm_FormClosing(object sender, FormClosingEventArgs e) // Событие при закрытии формы (до закрытия)
        {
            findCutLength = 0;
        }

        private void butFind_Click(object sender, EventArgs e) // Кнопка "Найти"
        {
            MainForm main = this.Owner as MainForm;
            if (main != null)
            {
                if (cbReg.CheckState == CheckState.Checked)
                {
                    TextWork.FindTextBox(ref main.textBox1, tbFind.Text, ref findCutLength, true);
                }
                else
                {
                    TextWork.FindTextBox(ref main.textBox1, tbFind.Text, ref findCutLength, false);
                }
            }
        }

        private void butReplace_Click(object sender, EventArgs e) // Кнопка "Заменить"
        {
            MainForm main = this.Owner as MainForm;
            if (main != null)
            {
                if (cbReg.CheckState == CheckState.Checked)
                {
                    TextWork.ReplaceTextBox(ref main.textBox1, tbFind.Text, tbReplace.Text, ref findCutLength, true);
                }
                else
                {
                    TextWork.ReplaceTextBox(ref main.textBox1, tbFind.Text, tbReplace.Text, ref findCutLength, false);
                }
            }
        }

        private void butReplaceAll_Click(object sender, EventArgs e) // Кнопка "Заменить всё"
        {
            MainForm main = this.Owner as MainForm;
            if (main != null)
            {
                if (cbReg.CheckState == CheckState.Checked)
                {
                    TextWork.ReplaceAllTextBox(ref main.textBox1, tbFind.Text, tbReplace.Text, true);
                }
                else
                {
                    TextWork.ReplaceAllTextBox(ref main.textBox1, tbFind.Text, tbReplace.Text, false);
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}