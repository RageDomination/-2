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
            this.StartPosition = FormStartPosition.CenterScreen;

            ToolTip toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 5000; // Время, в течение которого подсказка отображается
            toolTip1.InitialDelay = 350; // Задержка перед появлением подсказки
            toolTip1.ReshowDelay = 500; // Время повторного отображения подсказки
            toolTip1.ShowAlways = true; // Показывать подсказку всегда, даже если форма не активна

            // информация на картинках
            toolTip1.SetToolTip(this.pictureBox1, "Стерти");
            toolTip1.SetToolTip(this.pictureBox3, "Завантажити");
            toolTip1.SetToolTip(this.pictureBox7, "Завантажити");
            toolTip1.SetToolTip(this.pictureBox8, "Стерти");
            toolTip1.SetToolTip(this.button1, "Вивести інформацію по всім військовозобов'язаним, звільненим в запас в період введеного інтервалу дат");
            toolTip1.SetToolTip(this.button2, "Підрахувати кількість призваних на службу в період введеного інтервалу дат");
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
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

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public class ReserveInfo
        {
            public string Text { get; set; }
            public DateTime Date { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime rangeStart = dateTimePicker2.Value;
            DateTime rangeEnd = dateTimePicker3.Value;

            // проверка на совпадение начала и конца диапазона
            if (rangeEnd == rangeStart)
            {
                MessageBox.Show("Початок і кінець діапазону однакові.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rangeEnd < rangeStart)
            {
                MessageBox.Show("Дата закінчення діапазону не може бути раніше дати початку діапазону.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<ReserveInfo> registryInfo = new List<ReserveInfo>();

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string item = listBox1.Items[i].ToString();

                if (item.Contains("Дата занесення до запасу:"))
                {
                    int dateIndex = i;
                    int textIndex = Math.Max(0, dateIndex - 4);
                    string text = listBox1.Items[textIndex].ToString().Trim();

                    string dateString = item.Replace("Дата занесення до запасу:", "").Trim();
                    DateTime date;
                    if (DateTime.TryParse(dateString, out date))
                    {
                        if (date >= rangeStart && date <= rangeEnd)
                        {
                            ReserveInfo info = new ReserveInfo
                            {
                                Text = text,
                                Date = date
                            };

                            registryInfo.Add(info);
                        }
                    }
                }
            }

            // сортировка по дате занесения до запаса
            registryInfo.Sort((info1, info2) => info1.Date.CompareTo(info2.Date));

            // диалоговое окно с информацией
            ShowInfoForm(registryInfo);
        }

        private void ShowInfoForm(List<ReserveInfo> info)
        {
            Form infoForm = new Form
            {
                Text = "Список інформації",
                StartPosition = FormStartPosition.CenterScreen
            };

            TextBox textBox = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                ReadOnly = true
            };

            foreach (ReserveInfo reserveInfo in info)
            {
                string formattedLine = $"{reserveInfo.Text}, Дата занесення до запасу: {reserveInfo.Date}";
                textBox.Text += formattedLine + Environment.NewLine;
            }

            infoForm.Controls.Add(textBox);
            infoForm.Size = new Size(500, 400);
            infoForm.ShowDialog();
        }

        public class RecruitmentInfo
        {
            public string MilitaryNumber { get; set; }
            public DateTime Date { get; set; }
            public string Info { get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime rangeStart = dateTimePicker5.Value;
            DateTime rangeEnd = dateTimePicker4.Value;

            // проверка если дата в dateTimePicker4 старше даты в dateTimePicker5
            if (rangeEnd < rangeStart)
            {
                MessageBox.Show("Дата закінчення діапазону не може бути раніше дати початку діапазону.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // проверка на совпадение начала и конца диапазона
            if (rangeEnd == rangeStart)
            {
                MessageBox.Show("Початок і кінець діапазону однакові.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<RecruitmentInfo> registryInfo = new List<RecruitmentInfo>();

            // проверка элементов в listBox2
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                string item = listBox2.Items[i].ToString();

                if (item.Contains("Дата призову:"))
                {
                    // получение даты
                    string dateString = item.Replace("Дата призову:", "").Trim();
                    DateTime date;
                    if (DateTime.TryParse(dateString, out date))
                    {
                        // проверка на попадение в диапазон
                        if (date >= rangeStart && date <= rangeEnd)
                        {
                            int textIndex = Math.Max(0, i - 3); // Поднимаемся на 3 строки вверх
                            string militaryNumber = listBox2.Items[textIndex].ToString().Trim(); // Получаем информацию о номере военнообязанного
                            RecruitmentInfo info = new RecruitmentInfo
                            {
                                MilitaryNumber = militaryNumber,
                                Date = date,
                                Info = item
                            };

                            // добавление объекта в список
                            registryInfo.Add(info);
                        }
                    }
                }
            }

            // сортировка списка объектов по дате призыва
            registryInfo.Sort((info1, info2) => info1.Date.CompareTo(info2.Date));
            DisplayRecruitmentInfo(registryInfo);
        }

        private void DisplayRecruitmentInfo(List<RecruitmentInfo> info)
        {
            // новое окно для отображения информации
            Form infoForm = new Form
            {
                Text = "Інформація про призов",
                StartPosition = FormStartPosition.CenterScreen // Устанавливаем окно по центру экрана
            };

            // текстовое поле для вывода информации
            TextBox textBox = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                ReadOnly = true
            };

            foreach (RecruitmentInfo infoItem in info)
            {
                textBox.Text += $"{infoItem.MilitaryNumber}, {infoItem.Info}" + Environment.NewLine;
            }

            infoForm.Controls.Add(textBox);
            infoForm.Size = new Size(500, 400);
            infoForm.ShowDialog();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }
    }
}
