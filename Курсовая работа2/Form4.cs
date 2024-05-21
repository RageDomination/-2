using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_работа2
{
    public partial class Form4 : Form
    {
        public void SetListBoxItems(List<string> items)
        {
            listBox1.Items.AddRange(items.ToArray());
        }
        public Form4()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        public void SetListBoxItemsFromSecondForm(List<string> items)
        {
            listBox1.Items.AddRange(items.ToArray());
        }

        public void ShowReserveInfo(string militaryNumber, string dateString)
        {
            string reserveInfo = $"[ЗАПАС] Військовообов'язаний {militaryNumber}, Дата занесення до запасу: {dateString}";

            Form reserveInfoForm = new Form();
            reserveInfoForm.Text = "Інформація про занесення до запасу";

            Label label = new Label();
            label.Text = reserveInfo;
            label.AutoSize = true;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;

            reserveInfoForm.Controls.Add(label);

            reserveInfoForm.Size = new Size(300, 100);

            reserveInfoForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Создаем список для хранения информации о Військовозабов'язаних и датах занесения в запас
            List<(DateTime, string, string)> militaryAndDates = new List<(DateTime, string, string)>();

            // Перебираем элементы listBox1 и добавляем информацию о Військовозабов'язаних, датах и строках перед датами в список
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string itemText = listBox1.Items[i].ToString();
                if (itemText.Contains("Дата занесення до запасу:"))
                {
                    // Находим строку на 4 позиции выше текущей
                    int index = Math.Max(0, i - 4);
                    string previousItemText = listBox1.Items[index].ToString();

                    // Извлекаем дату
                    int startIndex = itemText.IndexOf(":") + 1;
                    string dateString = itemText.Substring(startIndex).Trim();

                    DateTime date;
                    if (DateTime.TryParse(dateString, out date))
                    {
                        militaryAndDates.Add((date, previousItemText, itemText));
                    }
                }
            }

            // Сортируем список по дате занесения до запасу
            militaryAndDates.Sort((x, y) => DateTime.Compare(y.Item1, x.Item1));

            // Создаем новое окно для отображения отсортированной информации
            Form infoForm = new Form();
            infoForm.Text = "Інформація про Військовозабов'язаних і дати занесення до запасу";

            // Создаем текстовое поле для отображения информации
            TextBox textBox = new TextBox();
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Dock = DockStyle.Fill;

            // Добавляем отсортированные элементы в текстовое поле
            foreach (var item in militaryAndDates)
            {
                // Формируем строку для отображения
                string info = $"{item.Item2}, {item.Item3}";

                // Добавляем информацию в текстовое поле
                textBox.AppendText(info + Environment.NewLine);
            }

            // Добавляем текстовое поле на форму
            infoForm.Controls.Add(textBox);

            // Устанавливаем размеры формы
            infoForm.Size = new Size(500, 400);

            // Показываем форму
            infoForm.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
