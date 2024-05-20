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
    public partial class Form3 : Form
    {
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

        }
    }
}
