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
        // Публичный метод для установки данных listBox1 из второй формы
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
                    // Загрузка XML-файла
                    XDocument xmlDoc = XDocument.Load(openFileDialog1.FileName);

                    // Очистка listBox2 перед загрузкой данных
                    listBox2.Items.Clear();

                    // Чтение данных из XML-файла и добавление их в listBox2, пропуская корневой элемент
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

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
