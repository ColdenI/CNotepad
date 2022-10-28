using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;

// незабыть вставить в конструктор (MainForm.Design.cs) строку - this.FormClosing += (o, e) => this.Form1_Closing(o, e);

namespace CNotepad
{
    public partial class MainForm : Form
    {
        public bool tbChange = false;
        public string docPath = "";
        public int mashtab = 100;
        public string _fileName = Properties.Settings.Default.newDocName;
        public string programmName = Properties.Settings.Default.programmName;


        public MainForm()
        {
            InitializeComponent();
            this.Text = Properties.Settings.Default.newDocName + " - " + Properties.Settings.Default.programmName;

        }

        public MainForm(string fileName) // Открытие программы документом
        {
            InitializeComponent();



            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                try
                {
                    
                    FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    StreamReader reader = new StreamReader(file, Encoding.Default);
                    textBox1.Text = reader.ReadToEnd();
                    reader.Close();
                    docPath = fileName;
                    tbChange = false;
                    this._fileName = fileName;
                    this.Text = Path.GetFileName(fileName) + " — " + programmName;
                    textBox1.Select(0, 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.ContextMenuStrip = contextMenuStripMain;

            set_ZoomFactor(Properties.Settings.Default.zoomText);
            this.Width = Properties.Settings.Default.formWidth;
            this.Height = Properties.Settings.Default.formHeight;
            textBox1.Font = Properties.Settings.Default.textFont;
            richTextBox1.Font = textBox1.Font;
            richTextBox1.ZoomFactor = textBox1.ZoomFactor;
            if (Properties.Settings.Default.FastPanelVisible) { панельБыстрогоДоступаToolStripMenuItem.CheckState = CheckState.Checked; } else { панельБыстрогоДоступаToolStripMenuItem.CheckState = CheckState.Unchecked; }
            if (Properties.Settings.Default.addedCWRVis) { паельCWRToolStripMenuItem.CheckState = CheckState.Checked; } else { паельCWRToolStripMenuItem.CheckState = CheckState.Unchecked; }
            if (Properties.Settings.Default.addedPanelVisible) { панельДополненийToolStripMenuItem.CheckState = CheckState.Checked; } else { панельДополненийToolStripMenuItem.CheckState = CheckState.Unchecked; }
            if (Properties.Settings.Default.wordWarp) { autoWordWarpCh.CheckState = CheckState.Checked; } else { autoWordWarpCh.CheckState = CheckState.Unchecked; }
            if (Properties.Settings.Default.statusStripVisible == true){mViewStatusStrip.CheckState = CheckState.Checked;}else{ mViewStatusStrip.CheckState = CheckState.Unchecked; }      
            if (Properties.Settings.Default.textTransfer == true){ mFormatTransfer.CheckState = CheckState.Checked; } else{ mFormatTransfer.CheckState = CheckState.Unchecked; }
            if (mViewStatusStrip.CheckState == CheckState.Checked){statusStrip1.Visible = true;}else{statusStrip1.Visible = false;}
            if (mFormatTransfer.CheckState == CheckState.Checked)
            {
                textBox1.WordWrap = true;
                textBox1.ScrollBars = RichTextBoxScrollBars.Both;
                mEditGo.Enabled = false;
                statusLinesCount.Visible = false;
                toolStripStatusLabel2.Visible = false;
            }
            else{
                textBox1.WordWrap = false;
                textBox1.ScrollBars = RichTextBoxScrollBars.Both;
                mEditGo.Enabled = true;
                statusLinesCount.Visible = true;
                toolStripStatusLabel2.Visible = true;
            }
            if (autoWordWarpCh.CheckState == CheckState.Checked) { textBox1.WordWrap = true; }
            else if (autoWordWarpCh.CheckState == CheckState.Unchecked) { textBox1.WordWrap = false; }

            if (панельДополненийToolStripMenuItem.CheckState == CheckState.Checked) { flowLayoutPanel1.Visible = true; }
            else if (панельДополненийToolStripMenuItem.CheckState == CheckState.Unchecked) { flowLayoutPanel1.Visible = false; }

            if (панельБыстрогоДоступаToolStripMenuItem.CheckState == CheckState.Checked) { menuStrip2.Visible = true; }
            else if (панельБыстрогоДоступаToolStripMenuItem.CheckState == CheckState.Unchecked) { menuStrip2.Visible = false; }

            if (паельCWRToolStripMenuItem.CheckState == CheckState.Checked) { groupBoxCWR.Visible = true; }
            else if (паельCWRToolStripMenuItem.CheckState == CheckState.Unchecked) { groupBoxCWR.Visible = false; }



            TextWork.mEditEnableds(ref textBox1, ref mEditCopy, ref mEditCut, ref mEditDel, ref mEditFind, ref mEditFind_, ref mEditGo, ref menuStrip2, ref вырезатьToolStripMenuItem, ref копироватьToolStripMenuItem, ref удалитьToolStripMenuItem);
        }

       

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.formWidth = this.Width;
            Properties.Settings.Default.formHeight = this.Height;
            Properties.Settings.Default.textTransfer = textBox1.WordWrap;
            Properties.Settings.Default.textFont = textBox1.Font;
            Properties.Settings.Default.statusStripVisible = statusStrip1.Visible;
            Properties.Settings.Default.zoomText = textBox1.ZoomFactor;
            Properties.Settings.Default.wordWarp = textBox1.WordWrap;
            Properties.Settings.Default.addedPanelVisible = flowLayoutPanel1.Visible;
            Properties.Settings.Default.FastPanelVisible = menuStrip2.Visible;
            Properties.Settings.Default.addedCWRVis = groupBoxCWR.Visible;
            Properties.Settings.Default.Save();
            if (tbChange == true)
            {
                DialogResult message = MessageBox.Show("Сохранить текущий документ перед выходом?", "Выход из программы", MessageBoxButtons.YesNoCancel);
                if (message == DialogResult.Yes)
                {
                    if (docPath != "")
                    {
                        FileWork.SaveFile(ref textBox1, ref tbChange, ref docPath);
                        Application.Exit();
                    }
                    else if (docPath == "")
                    {
                        FileWork.SaveAsFile(ref textBox1, ref tbChange, ref docPath);
                        Application.Exit();
                    }
                }
                else if (message == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void перейтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToForm gotoform = new GoToForm();
            gotoform.Owner = this;
            gotoform.tbLineNum.Minimum = 0;
            gotoform.tbLineNum.Maximum = textBox1.Lines.Count();
            gotoform.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tbChange = true;
            TextWork.StatusAnalize(ref textBox1, ref statusLinesCount, ref statusWordsCount, ref statusCharSpaceCount, ref statusCharCount);
            TextWork.mEditEnableds(ref textBox1, ref mEditCopy, ref mEditCut, ref mEditDel, ref mEditFind, ref mEditFind_, ref mEditGo, ref menuStrip2, ref вырезатьToolStripMenuItem, ref копироватьToolStripMenuItem, ref удалитьToolStripMenuItem);
            TextBoxNumerUpdate();
            richTextBox1.Font = textBox1.Font;
            richTextBox1.ZoomFactor = textBox1.ZoomFactor;
        }


        private void CreateNewFile()
        {
            if (tbChange == true)
            {
                DialogResult message = MessageBox.Show("Сохранить текущий документ перед созданием нового?", "Создание документа", MessageBoxButtons.YesNoCancel);
                if (message == DialogResult.Yes)
                {
                    if (docPath != "")
                    {
                        FileWork.SaveFile(ref textBox1, ref tbChange, ref docPath);
                        FileWork.CreateFile(ref textBox1, ref tbChange, ref docPath);
                    }
                    else if (docPath == "")
                    {
                        FileWork.SaveAsFile(ref textBox1, ref tbChange, ref docPath);
                        FileWork.CreateFile(ref textBox1, ref tbChange, ref docPath);
                    }
                }
                else if (message == DialogResult.No)
                {
                    FileWork.CreateFile(ref textBox1, ref tbChange, ref docPath);
                }
            }
            else
            {
                FileWork.CreateFile(ref textBox1, ref tbChange, ref docPath);
            }
        }
        private void OpenFile()
        {
            if (tbChange == true)
            {
                DialogResult message = MessageBox.Show("Сохранить текущий документ перед открытием нового?", "Открытие документа", MessageBoxButtons.YesNoCancel);
                if (message == DialogResult.Yes)
                {
                    if (docPath != "")
                    {
                        FileWork.SaveFile(ref textBox1, ref tbChange, ref docPath);
                        FileWork.OpenFile(ref textBox1, ref tbChange, ref docPath);
                    }
                    else if (docPath == "")
                    {
                        FileWork.SaveAsFile(ref textBox1, ref tbChange, ref docPath);
                        FileWork.OpenFile(ref textBox1, ref tbChange, ref docPath);
                    }
                }
                else if (message == DialogResult.No)
                {
                    FileWork.OpenFile(ref textBox1, ref tbChange, ref docPath);
                }
                else
                {
                    return;
                }
            }
            else
            {
                FileWork.OpenFile(ref textBox1, ref tbChange, ref docPath);
            }
        }
        public void SaveOrAsSaveFile()
        {
            if (docPath != "")
            {
                FileWork.SaveFile(ref textBox1, ref tbChange, ref docPath);
            }
            else
            {
                FileWork.SaveAsFile(ref textBox1, ref tbChange, ref docPath);
            }
        }
        private void PrintFile()
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printDocument.Print();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка параметров печати.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void создатьCtrlNToolStripMenuItem_Click(object sender, EventArgs e) => CreateNewFile();
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) => OpenFile();
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrAsSaveFile();
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileWork.SaveAsFile(ref textBox1, ref tbChange, ref docPath);
        }
        private void параметрыПечатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            pageSetupDialog.PageSettings = new System.Drawing.Printing.PageSettings();
            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.DefaultPageSettings = pageSetupDialog.PageSettings;
            }
        }
        private void печатьCTRLPToolStripMenuItem_Click(object sender, EventArgs e) => PrintFile();
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }
        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
            {
                textBox1.Cut();
            }
        }
        private void printDocument_PrintPage(object sender, PrintPageEventArgs e) // Отправка документа на печать
        {
            e.Graphics.DrawString(textBox1.Text, textBox1.Font, Brushes.Black, 10, 10);
        }
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
            PasetUpdate();
        }
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
            {
                textBox1.Copy();
            }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
            {
                textBox1.SelectedText = "";
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SearchForm findText = new SearchForm();
            findText.Owner = this;
            findText.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SearchForm findText = new SearchForm();
            findText.Owner = this;
            findText.Show();
        }

        private void выделитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void времяИДатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteTimeAndData();
        }

        private void mFormatFont_Click(object sender, EventArgs e)
        {
            fontDialog.Font = textBox1.Font;
            DialogResult = fontDialog.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                textBox1.Font = fontDialog.Font;
                richTextBox1.Font = textBox1.Font;
            }
        }

        private void mFormatTransfer_Click(object sender, EventArgs e)
        {
            if (mFormatTransfer.CheckState == CheckState.Checked)
            {
                textBox1.WordWrap = true;
                textBox1.ScrollBars = RichTextBoxScrollBars.Both;
                mEditGo.Enabled = false;
                statusLinesCount.Visible = false;
                toolStripStatusLabel2.Visible = false;
            }
            else
            {
                textBox1.WordWrap = false;
                textBox1.ScrollBars = RichTextBoxScrollBars.Both;
                mEditGo.Enabled = true;
                statusLinesCount.Visible = true;
                toolStripStatusLabel2.Visible = true;
            }
        }

        private void строкаСостоянияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mViewStatusStrip.CheckState == CheckState.Checked)
            {
                statusStrip1.Visible = true;
            }
            else
            {
                statusStrip1.Visible = false;
            }
        }

        private void set_ZoomFactor(float value)
        {
            if (value <= 0) value = 0.1f;
            textBox1.ZoomFactor = value;
            StatusLabelMash.Text = "Маштаб: " + Convert.ToInt32(value * 100).ToString() + "%";
            richTextBox1.ZoomFactor = value;
            richTextBox1.Font = textBox1.Font;
        }
        private void set_ZoomFactor_p(float value)
        {
            set_ZoomFactor(textBox1.ZoomFactor + value);
        }

        private void востановитьToolStripMenuItem_Click(object sender, EventArgs e) => set_ZoomFactor(1f);
        private void toolStripMenuItem2_Click_1(object sender, EventArgs e) => set_ZoomFactor(0.1f);
        private void toolStripMenuItem3_Click(object sender, EventArgs e) => set_ZoomFactor(0.3f);
        private void toolStripMenuItem4_Click(object sender, EventArgs e) => set_ZoomFactor(0.5f);
        private void toolStripMenuItem5_Click(object sender, EventArgs e) => set_ZoomFactor(0.8f);
        private void toolStripMenuItem6_Click(object sender, EventArgs e) => set_ZoomFactor(1f);
        private void toolStripMenuItem7_Click(object sender, EventArgs e) => set_ZoomFactor(1.5f);
        private void toolStripMenuItem8_Click(object sender, EventArgs e) => set_ZoomFactor(2f);
        private void toolStripMenuItem9_Click(object sender, EventArgs e) => set_ZoomFactor(3f);
        private void toolStripMenuItem10_Click(object sender, EventArgs e) => set_ZoomFactor(5f);
        private void на10ToolStripMenuItem_Click(object sender, EventArgs e) => set_ZoomFactor_p(0.1f);
        private void на20ToolStripMenuItem_Click(object sender, EventArgs e) => set_ZoomFactor_p(0.3f);
        private void на50ToolStripMenuItem_Click(object sender, EventArgs e) => set_ZoomFactor_p(0.5f);
        private void на100ToolStripMenuItem_Click(object sender, EventArgs e) => set_ZoomFactor_p(1f);
        private void на200ToolStripMenuItem_Click(object sender, EventArgs e) => set_ZoomFactor_p(2f);
        private void на500ToolStripMenuItem_Click(object sender, EventArgs e) => set_ZoomFactor_p(5f);
        private void на10ToolStripMenuItem1_Click(object sender, EventArgs e) => set_ZoomFactor_p(-0.1f);
        private void на30ToolStripMenuItem_Click(object sender, EventArgs e) => set_ZoomFactor_p(-0.3f);
        private void на50ToolStripMenuItem1_Click(object sender, EventArgs e) => set_ZoomFactor_p(-0.5f);
        private void на100ToolStripMenuItem1_Click(object sender, EventArgs e) => set_ZoomFactor_p(-1f);
        private void на200ToolStripMenuItem1_Click(object sender, EventArgs e) => set_ZoomFactor_p(-2f);
        private void на500ToolStripMenuItem1_Click(object sender, EventArgs e) => set_ZoomFactor_p(-5f);

        private void toolStripMenuItem14_Click(object sender, EventArgs e) => set_ZoomFactor_p(-0.1f);
        private void toolStripMenuItem11_Click(object sender, EventArgs e) => set_ZoomFactor_p(0.1f);
        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e) => set_ZoomFactor(1f);
        private void autoWordWarpCh_Click(object sender, EventArgs e)
        {
            if (autoWordWarpCh.CheckState == CheckState.Checked) { textBox1.WordWrap = true; }
            else if (autoWordWarpCh.CheckState == CheckState.Unchecked) { textBox1.WordWrap = false; }
        }
        private void показатьВHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTMLForm htmlForm = new HTMLForm();
            htmlForm.Owner = this;
            htmlForm.Show();
        }
   
        private void панельДополненийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (панельДополненийToolStripMenuItem.CheckState == CheckState.Checked) { flowLayoutPanel1.Visible = true; }
            else if (панельДополненийToolStripMenuItem.CheckState == CheckState.Unchecked) { flowLayoutPanel1.Visible = false; }
        }
        private void панельБыстрогоДоступаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (панельБыстрогоДоступаToolStripMenuItem.CheckState == CheckState.Checked) { menuStrip2.Visible = true; }
            else if (панельБыстрогоДоступаToolStripMenuItem.CheckState == CheckState.Unchecked) { menuStrip2.Visible = false; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CWRGetValueForm cWRGetValueForm = new CWRGetValueForm();
            cWRGetValueForm.Owner = this;
            cWRGetValueForm.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            CWRViewAllItemsForm cWRGetValueForm = new CWRViewAllItemsForm();
            cWRGetValueForm.Owner = this;
            cWRGetValueForm.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            CWRNewItemForm cWRGetValueForm = new CWRNewItemForm();
            cWRGetValueForm.Owner = this;
            cWRGetValueForm.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            CWREditItemForm cWRGetValueForm = new CWREditItemForm();
            cWRGetValueForm.Owner = this;
            cWRGetValueForm.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            CWRDeleteItemForm cWRGetValueForm = new CWRDeleteItemForm();
            cWRGetValueForm.Owner = this;
            cWRGetValueForm.Show();
        }
        private void оДополненииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данные по дополнению не найдены!", "Дополнение - CWR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void паельCWRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (паельCWRToolStripMenuItem.CheckState == CheckState.Checked) { groupBoxCWR.Visible = true; }
            else if (паельCWRToolStripMenuItem.CheckState == CheckState.Unchecked) { groupBoxCWR.Visible = false; }
        }

        private void PasteTimeAndData()
        {
            string _text = Convert.ToString(System.DateTime.Now);
            textBox1.Text = textBox1.Text.Insert(textBox1.SelectionStart, _text);
            textBox1.SelectionStart = textBox1.SelectionStart + _text.Length;
        }
        private void PasetUpdate()
        {
            textBox1.Font = textBox1.Font;
            textBox1.ForeColor = textBox1.ForeColor;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5) PasteTimeAndData();
            if (keyData == (Keys.Control | Keys.S)) SaveOrAsSaveFile();
            if (keyData == (Keys.Control | Keys.V)) PasetUpdate();
            if (keyData == (Keys.Control | Keys.N)) CreateNewFile();
            if (keyData == (Keys.Control | Keys.O)) OpenFile();
            if (keyData == (Keys.Control | Keys.S | Keys.Shift)) FileWork.SaveAsFile(ref textBox1, ref tbChange, ref docPath);
            if (keyData == (Keys.Control | Keys.P)) PrintFile();
            if (mEditFind.Enabled && keyData == (Keys.Control | Keys.F))
            {
                SearchForm findText = new SearchForm();
                findText.Owner = this;
                findText.Show();
            }
            if (mEditFind.Enabled && keyData == (Keys.Control | Keys.H))
            {
                SearchForm findText = new SearchForm();
                findText.Owner = this;
                findText.Show();
            }
            if (mEditFind.Enabled && keyData == (Keys.Control | Keys.G))
            {
                GoToForm gotoform = new GoToForm();
                gotoform.Owner = this;
                gotoform.tbLineNum.Minimum = 0;
                gotoform.tbLineNum.Maximum = textBox1.Lines.Count();
                gotoform.ShowDialog();
            }
            if (keyData == (Keys.Alt | Keys.F4)) Application.Exit();
            if (keyData == (Keys.Alt | Keys.H))
            {
                HTMLForm htmlForm = new HTMLForm();
                htmlForm.Owner = this;
                htmlForm.Show();
            }

            //Console.WriteLine(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void IsSetZoomFactor(object sender, MouseEventArgs e)
        {
            set_ZoomFactor(textBox1.ZoomFactor);
        }
        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            set_ZoomFactor(textBox1.ZoomFactor);
        }

        private void TextBoxNumerUpdate()
        {
            string _text = textBox1.Text;

            richTextBox1.Text = "";

            for (int i = 0; i < _text.Split('\n').Length; i++)
            {
                richTextBox1.Text += (i + 1).ToString() + "\n";
            }

            richTextBox1.Select(richTextBox1.TextLength, richTextBox1.TextLength);
            richTextBox1.ScrollToCaret();
            richTextBox1.ZoomFactor = textBox1.ZoomFactor;
            richTextBox1.Font = textBox1.Font;
            
            
        }

    }
}
