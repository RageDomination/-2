using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Курсовая_работа2
{
    public partial class Form3 : Form
    {
        public void SetListBoxItems(ListBox.ObjectCollection items)
        {
            listBox1.Items.Clear();
            foreach (var item in items)
            {
                listBox1.Items.Add(item);
            }
            listBox2.Items.Clear();
            foreach (var item in items)
            {
                listBox2.Items.Add(item);
            }
        }

        public Form3()
        {
            InitializeComponent();

            ToolTip toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 5000; // Время, в течение которого подсказка отображается
            toolTip1.InitialDelay = 350; // Задержка перед появлением подсказки
            toolTip1.ReshowDelay = 500; //  Время повторного отображения подсказки
            toolTip1.ShowAlways = true; // Показывать подсказку всегда, даже если форма не активна

            // информация на картинках
            toolTip1.SetToolTip(this.pictureBox1, "Стерти");
            toolTip1.SetToolTip(this.pictureBox3, "Завантажити");
            toolTip1.SetToolTip(this.pictureBox7, "Завантажити");
            toolTip1.SetToolTip(this.pictureBox8, "Стерти");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // загрузка XML-файла
                    XDocument xmlDoc = XDocument.Load(openFileDialog1.FileName);

                    // очистка listBox1 перед загрузкой данных
                    listBox1.Items.Clear();

                    bool firstElement = true;

                    // считывание данных из XML-файла и добавление их в listBox1
                    foreach (XElement element in xmlDoc.Root.Elements())
                    {
                        listBox1.Items.Add(element.Value);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка: " + ex.Message);
                }
            }
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); // очистка listBox1
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear(); // очистка listBox1
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XDocument xmlDoc = XDocument.Load(openFileDialog1.FileName);

                    listBox2.Items.Clear();

                    foreach (XElement element in xmlDoc.Root.Elements())
                    {
                        listBox2.Items.Add(element.Value);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка: " + ex.Message);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime rangeStart = dateTimePicker2.Value;
            DateTime rangeEnd = dateTimePicker3.Value;

            // проверка на совпадение начала и конца диалога
            if (rangeEnd == rangeStart)
            {
                MessageBox.Show("Початок і кінець діапазону однакові.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Завершаем выполнение метода
            }

            // Проверяем, если дата в dateTimePicker3 старше даты в dateTimePicker2
            if (rangeEnd < rangeStart)
            {
                MessageBox.Show("Дата закінчення діапазону не може бути раніше дати початку діапазону.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Завершаем выполнение метода
            }

            // Создаем список для хранения информации о датах и связанных с ними текстах
            List<string> registryInfo = new List<string>();

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string item = listBox1.Items[i].ToString();
                
                if (item.Contains("Дата занесення до запасу:"))
                {
                    int dateIndex = i;
                    int textIndex = Math.Max(0, dateIndex - 4);
                    string text = listBox1.Items[textIndex].ToString().Trim();

                    // получение даты
                    string dateString = item.Replace("Дата занесення до запасу:", "").Trim();
                    DateTime date;
                    if (DateTime.TryParse(dateString, out date))
                    {
                        // проверка на попадение в диапазон
                        if (date >= rangeStart && date <= rangeEnd)
                        {
                            // Формируем строку с датой и текстом через запятую
                            string combinedText = $"{text}, {date}";

                            // добавление строки в список
                            registryInfo.Add(combinedText);
                        }
                    }
                }
            }

            // диалоговое окно с информацией
            ShowInfoForm(registryInfo);
        }
        private void ShowInfoForm(List<string> info)
        {
            // новое окно для отображения информации
            Form infoForm = new Form();
            infoForm.Text = "Список інформації";

            // текстовое поле для вывода информации
            TextBox textBox = new TextBox();
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Dock = DockStyle.Fill;

            // Добавляем информацию в текстовое поле, разделяя каждую пару текст-дата запятой
            foreach (string line in info)
            {
                // Добавляем пробел после запятой и выводим "Дата занесення до запасу: " перед датой
                string formattedLine = line.Replace(", ", ", Дата занесення до запасу: ");
                textBox.Text += formattedLine + Environment.NewLine;
            }
            infoForm.Controls.Add(textBox);

            // размеры формы
            infoForm.Size = new Size(500, 400);
            infoForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
