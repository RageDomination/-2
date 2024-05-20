using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Курсовая_работа2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.SetListBoxItems(listBox1.Items);
            form3.Show();
            this.Hide();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.HorizontalScrollbar = true;
            listBox1.SelectionMode = SelectionMode.MultiExtended;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private int militaryCounter = 1;
        private void button3_Click(object sender, EventArgs e)
        {
            // Проверка, что все текстовые поля заполнены
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Всі поля мають бути заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // строка для хранения информации о военнообязанных
            string info = "[ЗАПАС] Військовообов'язаний " + militaryCounter + "\n";

            // инфо о военнообязанном в ListBox
            listBox1.Items.Add(info);

            // информация из каждого текстового поля поочередно
            info = "ПІБ: " + textBox1.Text + "\n";
            listBox1.Items.Add(info);

            info = "Звання: " + textBox2.Text + "\n";
            listBox1.Items.Add(info);

            info = "Дата призову: " + textBox3.Text + "\n";
            listBox1.Items.Add(info);

            info = "Дата занесення до запасу: " + textBox4.Text + "\n";
            listBox1.Items.Add(info);

            info = "Військова частина: " + textBox5.Text + "\n";
            listBox1.Items.Add(info);

            listBox1.Items.Add("___________________");

            // счетчик для следующего военнообязанного
            militaryCounter++;

            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Проверка, что все текстовые поля заполнены
            if (string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox9.Text) ||
                string.IsNullOrWhiteSpace(textBox10.Text) ||
                string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("Всі поля мають бути заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // строка хранения информации о военнообязанных
            string info = "Військовозабов'язаний " + militaryCounter + "\n";

            // инфо о военнообязанном в ListBox
            listBox1.Items.Add(info);

            // инфо из каждого текстового поля поочередно
            info = "ПІБ: " + textBox6.Text + "\n";
            listBox1.Items.Add(info);

            info = "Рік народження: " + textBox7.Text + "\n";
            listBox1.Items.Add(info);

            info = "Дата призову: " + textBox8.Text + "\n";
            listBox1.Items.Add(info);

            info = "Дата занесення до реєстру: " + textBox9.Text + "\n";
            listBox1.Items.Add(info);

            info = "Громадянство: " + textBox10.Text + "\n";
            listBox1.Items.Add(info);

            info = "Освіта, професія: " + textBox11.Text + "\n";
            listBox1.Items.Add(info);

            // строка с символами подчеркивания
            listBox1.Items.Add("___________________");

            // счетчик+1 для следующего военнообязанного
            militaryCounter++;

            // очистка текстбоксов 6-11
            ClearTextBoxes();
        }
        private void ClearTextBoxes()
        {
            textBox6.Clear();
            textBox7.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            // создание нового документа
            XDocument doc = new XDocument();
            XElement rootElement = new XElement("Items");

            foreach (var item in listBox1.Items)
            {
                XElement itemElement = new XElement("Item", item.ToString());
                rootElement.Add(itemElement);
            }

            doc.Add(rootElement);

            // открытие диалога
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // система сохранения + меню
                doc.Save(saveFileDialog.FileName);
                MessageBox.Show("Файл успішно збережено!", "Збереження", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // диалог для выбора XML файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // загружка XML документа
                    XDocument doc = XDocument.Load(openFileDialog.FileName);

                    listBox1.Items.Clear();

                    // добавление в listBox
                    foreach (XElement itemElement in doc.Descendants("Item"))
                    {
                        listBox1.Items.Add(itemElement.Value);
                    }

                    MessageBox.Show("Файл успішно завантажений!", "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при завантаженні: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }
    }
}
