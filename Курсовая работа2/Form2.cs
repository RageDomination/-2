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
            this.StartPosition = FormStartPosition.CenterScreen;

            System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();

            toolTip1.SetToolTip(pictureBox1, "Підрахунки за діапазонами дат");
            toolTip1.SetToolTip(button6, "Завантажити XML файл із комп'ютера");
            toolTip1.SetToolTip(button5, "Зберегти XML файл на комп'ютер");
            toolTip1.SetToolTip(button3, "Додати до запасу зі спеціальним тегом [ЗАПАС]");
            toolTip1.SetToolTip(button1, "Додати до реєстру військовозобов'язаного");


            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 350;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
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

            List<string> items = new List<string>();
            foreach (var item in listBox1.Items)
            {
                items.Add(item.ToString());
            }
            form4.SetListBoxItems(items);
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
            // проверка полей
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Всі поля мають бути заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string info = "[ЗАПАС] Військовозабов'язаний " + militaryCounter + "\n";

            listBox1.Items.Add(info);

            info = "ПІБ: " + textBox1.Text + "\n";
            listBox1.Items.Add(info);

            info = "Звання: " + textBox2.Text + "\n";
            listBox1.Items.Add(info);

            info = "Дата призову: " + dateTimePicker2.Text + "\n";
            listBox1.Items.Add(info);

            info = "Дата занесення до запасу: " + dateTimePicker4.Text + "\n";
            listBox1.Items.Add(info);

            info = "Військова частина: " + textBox5.Text + "\n";
            listBox1.Items.Add(info);

            listBox1.Items.Add(" ");

            militaryCounter++;

            textBox1.Clear();
            textBox2.Clear();
            textBox5.Clear();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox10.Text) ||
                string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("Всі поля мають бути заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string info = "Військовозабов'язаний " + militaryCounter + "\n";

            listBox1.Items.Add(info);

            info = "ПІБ: " + textBox6.Text + "\n";
            listBox1.Items.Add(info);

            info = "Рік народження: " + textBox7.Text + "\n";
            listBox1.Items.Add(info);

            info = "Дата призову: " + dateTimePicker3.Text + "\n";
            listBox1.Items.Add(info);

            info = "Дата занесення до реєстру: " + dateTimePicker5.Text + "\n";
            listBox1.Items.Add(info);

            info = "Громадянство: " + textBox10.Text + "\n";
            listBox1.Items.Add(info);

            info = "Освіта, професія: " + textBox11.Text + "\n";
            listBox1.Items.Add(info);

            DateTime enlistmentDate = DateTime.Parse(dateTimePicker3.Text);
            DateTime endDate = enlistmentDate.AddYears(1).AddDays(-1);
            TimeSpan remainingServiceTime = endDate - DateTime.Today;

            if (remainingServiceTime.Days < 0)
            {
                info = "Ваша служба була завершена " + endDate.ToString("dd.MM.yyyy") + "\n";
            }
            else
            {
                info = "Залишилось до кінця служби: " + remainingServiceTime.Days + " днів(я), ваша служба завершиться " + endDate.ToString("dd.MM.yyyy") + "\n";
            }
            listBox1.Items.Add(info);

            listBox1.Items.Add(" ");

            militaryCounter++;

            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            textBox6.Clear();
            textBox7.Clear();
            textBox10.Clear();
            textBox11.Clear();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            XDocument doc = new XDocument();
            XElement rootElement = new XElement("Items");

            foreach (var item in listBox1.Items)
            {
                XElement itemElement = new XElement("Item", item.ToString());
                rootElement.Add(itemElement);
            }

            doc.Add(rootElement);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                doc.Save(saveFileDialog.FileName);
                MessageBox.Show("Файл успішно збережено!", "Збереження", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XDocument doc = XDocument.Load(openFileDialog.FileName);

                    listBox1.Items.Clear();
                    int highestCounter = 0;

                    foreach (XElement itemElement in doc.Descendants("Item"))
                    {
                        listBox1.Items.Add(itemElement.Value);
                        string itemValue = itemElement.Value;
                        if (itemValue.StartsWith("Військовозабов'язаний "))
                        {
                            int counter;
                            if (int.TryParse(itemValue.Split(' ')[1], out counter))
                            {
                                if (counter > highestCounter)
                                {
                                    highestCounter = counter;
                                }
                            }
                        }
                    }
                    militaryCounter = highestCounter + 1;

                    MessageBox.Show("Файл успішно завантажений! Не забудьте зберегти його після редагування.", "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
