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

namespace CNotepad
{
    public partial class HTMLForm : Form
    {

        FileInfo fileInfo;

        public HTMLForm()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void HTMLForm_Load(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            main.SaveOrAsSaveFile();

            try
            {
                fileInfo = new FileInfo(main.docPath);
                string _url = "file:///";
                _url += fileInfo.DirectoryName.Replace("\\", "/");
                _url += "/";
                _url += fileInfo.Name;

                Console.WriteLine(_url);
                webBrowser1.Url = new Uri(_url);
                webBrowser1.Update();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Ошибка пути к файлу.\nПроверьте файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            main.SaveOrAsSaveFile();

            try
            {
                fileInfo = new FileInfo(main.docPath);
                string _url = "file:///";
                _url += fileInfo.DirectoryName.Replace("\\", "/");
                _url += "/";
                _url += fileInfo.Name;

                Console.WriteLine(_url);
                webBrowser1.Url = new Uri(_url);
                webBrowser1.Update();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Ошибка пути к файлу.\nПроверьте файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
    }
}
