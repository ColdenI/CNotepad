using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CNotepad
{
    public static class TextWork
    {
        // Метод поиска текста в TextBox
        // Для использования создаем в форме поиска глобальную переменную 
        // типа int = 0 для стартовой позиции поиска,
        // передаем в метод ссылки на TextBox'ы с исходным и искомым текстами,
        // а также необходимо указать, учитывать ли регистр букв при поиске (True - учитывать, False - не учитывать)

      


        public static int FindTextBox(ref RichTextBox textBox, string findText, ref int findCutLength, bool register)
        {
            // Поиск с учетом регистра
            if (register == true)
            {
                if (textBox.Text.Contains(findText))
                {
                    // Заносим текст в переменную string, удаляем из него уже пройденный 
                    // текст (findCutLength) в переменной nextText
                    string text = textBox.Text;
                    string nextText = text.Remove(0, findCutLength);
                    // Ищем в nextText
                    int resultPosition = nextText.IndexOf(findText);
                    // Если искомое выражение найдено - выделяем его, добавляем его позицию и длину 
                    // к значению пройденного текста (findCutLenght)
                    if (resultPosition != -1)
                    {
                        textBox.Select(resultPosition + findCutLength, findText.Length);
                        textBox.ScrollToCaret();
                        textBox.Focus();
                        findCutLength += findText.Length + resultPosition;
                    }
                    // Если попытка поиска не первая, и больше совпадений в тексте нет - обнуляем
                    // значение пройденного текста и начинаем поиск сначала
                    else if (resultPosition == -1 && findCutLength != 0)
                    {
                        findCutLength = 0;
                        return FindTextBox(ref textBox, findText, ref findCutLength, register);
                    }
                }
                else
                {
                    findCutLength = 0;
                    MessageBox.Show("По вашему запросу ничего не нашлось.", "Совпадений не найдено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            // Поиск без учета регистра
            else if (register == false)
            {
                if (textBox.Text.ToLower().Contains(findText.ToLower()))
                {
                    string text = textBox.Text.ToLower();
                    string nextText = text.Remove(0, findCutLength);
                    int resultPosition = nextText.IndexOf(findText.ToLower());

                    if (resultPosition != -1)
                    {
                        textBox.Select(resultPosition + findCutLength, findText.Length);
                        textBox.ScrollToCaret();
                        textBox.Focus();
                        findCutLength += findText.Length + resultPosition;
                    }
                    else if (resultPosition == -1 && findCutLength != 0)
                    {
                        findCutLength = 0;
                        return FindTextBox(ref textBox, findText, ref findCutLength, register);
                    }
                }
                // Если текст изначально не содержит результатов поиска - обнуляем findCutLength, выводим сообщение
                else
                {
                    findCutLength = 0;
                    MessageBox.Show("По вашему запросу ничего не нашлось.", "Совпадений не найдено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            return 0;
        }

        // Метод "Заменить"
        public static int ReplaceTextBox(ref RichTextBox textBox, string findText, string replaceText, ref int findCutLength, bool register)
        {
            if (register == true)
            {
                if (textBox.Text.Contains(findText))
                {
                    if (textBox.SelectedText == "" || textBox.SelectedText != findText)
                    {
                        string text = textBox.Text;
                        string nextText = text.Remove(0, findCutLength);
                        int resultPosition = nextText.IndexOf(findText);
                        if (resultPosition != -1)
                        {
                            textBox.Select(resultPosition + findCutLength, findText.Length);
                            textBox.ScrollToCaret();
                            textBox.Focus();
                            findCutLength += findText.Length + resultPosition;
                        }
                        else if (resultPosition == -1 && findCutLength != 0)
                        {
                            findCutLength = 0;
                            return ReplaceTextBox(ref textBox, findText, replaceText, ref findCutLength, register);
                        }
                    }
                    else if (textBox.SelectedText == findText)
                    {
                        textBox.SelectedText = replaceText;
                    }
                }
                else
                {
                    findCutLength = 0;
                    MessageBox.Show("По вашему запросу ничего не нашлось.", "Совпадений не найдено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (register == false)
            {
                if (textBox.Text.ToLower().Contains(findText.ToLower()))
                {
                    if (textBox.SelectedText == "" || textBox.SelectedText.ToLower() != findText.ToLower())
                    {
                        string text = textBox.Text.ToLower();
                        string nextText = text.Remove(0, findCutLength);
                        int resultPosition = nextText.IndexOf(findText.ToLower());
                        if (resultPosition != -1)
                        {
                            textBox.Select(resultPosition + findCutLength, findText.Length);
                            textBox.ScrollToCaret();
                            textBox.Focus();
                            findCutLength += findText.Length + resultPosition;
                        }
                        else if (resultPosition == -1 && findCutLength != 0)
                        {
                            findCutLength = 0;
                            return ReplaceTextBox(ref textBox, findText, replaceText, ref findCutLength, register);
                        }
                    }
                    else if (textBox.SelectedText.ToLower() == findText.ToLower())
                    {
                        textBox.SelectedText = replaceText;
                    }
                }
                else
                {
                    findCutLength = 0;
                    MessageBox.Show("По вашему запросу ничего не нашлось.", "Совпадений не найдено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return 0;
        }

        // Метод "Заменить всё"
        public static int ReplaceAllTextBox(ref RichTextBox textBox, string findText, string replaceText, bool register)
        {
            if (register == true)
            {
                string text = textBox.Text;
                string words = findText;
                if (textBox.Text.Contains(words))
                {
                    int startPosition = text.IndexOf(words);
                    textBox.Select(startPosition, words.Length);
                    textBox.SelectedText = replaceText;
                    return ReplaceAllTextBox(ref textBox, findText, replaceText, register);
                }
                else
                {
                    MessageBox.Show("Замены произведены успешно.", "Заменить всё", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (register == false)
            {
                string text = textBox.Text.ToLower();
                string words = findText.ToLower();
                if (text.Contains(words))
                {
                    int startPosition = text.IndexOf(words);
                    textBox.Select(startPosition, findText.Length);
                    textBox.SelectedText = replaceText;
                    return ReplaceAllTextBox(ref textBox, findText, replaceText, register);
                }
                else
                {
                    MessageBox.Show("Замены произведены успешно.", "Заменить всё", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return 0;
        }

        public static void mEditEnableds(ref RichTextBox notebox, ref ToolStripMenuItem mEditCopy, ref ToolStripMenuItem mEditCut, ref ToolStripMenuItem mEditDel, ref ToolStripMenuItem mEditFind, ref ToolStripMenuItem mEditFind_, ref ToolStripMenuItem mEditGo, ref MenuStrip menuStrip, ref ToolStripMenuItem item1, ref ToolStripMenuItem item2, ref ToolStripMenuItem item3)
        {
            if (notebox.Text.Length < 1)
            {
                mEditCopy.Enabled = false;
                mEditCut.Enabled = false;
                mEditDel.Enabled = false;
                mEditFind.Enabled = false;
                mEditFind_.Enabled = false;
                mEditGo.Enabled = false;

                item1.Enabled = false;
                item2.Enabled = false;
                item3.Enabled = false;

                menuStrip.Enabled = false;
            }
            else
            {
                mEditCopy.Enabled = true;
                mEditCut.Enabled = true;
                mEditDel.Enabled = true;
                mEditFind.Enabled = true;
                mEditFind_.Enabled = true;
                mEditGo.Enabled = true;

                item1.Enabled = true;
                item2.Enabled = true;
                item3.Enabled = true;
 
                menuStrip.Enabled = true;
            }
        }

        public static void StatusAnalize(ref RichTextBox notebox, ref ToolStripStatusLabel statusLinesCount, ref ToolStripStatusLabel statusWordsCount, ref ToolStripStatusLabel statusCharSpaceCount, ref ToolStripStatusLabel statusCharCount)
        {
            string text = notebox.Text;
            // Количество строк в тексте
            statusLinesCount.Text = "Количество строк: " + notebox.Lines.Count().ToString();
            // Количество слов в тексте
            statusWordsCount.Text = "Количество слов: " + text.Split(new Char[] { ' ', '\t', '\n', '\r', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-',
                '_', '+', '=', '[', '{', ']', '}', '/', '\\', '|', '"', ':', ';', '.', ',', '>', '<' }, StringSplitOptions.RemoveEmptyEntries).Length.ToString();
            // Количество символов без пробелов
            statusCharCount.Text = "Символов с пробелами: " + text.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "").ToCharArray().Length.ToString();
            // Количество символов с пробелами
            statusCharSpaceCount.Text = "Символов с пробелами: " + text.ToCharArray().Length.ToString();
        }
    }
}