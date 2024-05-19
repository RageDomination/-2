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
            form3.Show();
            this.Hide();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            // строка для хранения информации о военнообязанных
            string info = "Військовообов'язаний " + militaryCounter + "\n";

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
        }
    }
}
