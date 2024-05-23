using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Linq;

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
            this.StartPosition = FormStartPosition.CenterScreen;
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
            List<(DateTime, string, string)> militaryAndDates = new List<(DateTime, string, string)>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string itemText = listBox1.Items[i].ToString();
                if (itemText.Contains("Дата занесення до запасу:"))
                {
                    int index = Math.Max(0, i - 4);
                    string previousItemText = listBox1.Items[index].ToString();
                    int startIndex = itemText.IndexOf(":") + 1;
                    string dateString = itemText.Substring(startIndex).Trim();
                    DateTime date;
                    if (DateTime.TryParse(dateString, out date))
                    {
                        militaryAndDates.Add((date, previousItemText, itemText));
                    }
                }
            }
            militaryAndDates.Sort((x, y) => DateTime.Compare(y.Item1, x.Item1));

            Form infoForm = new Form
            {
                Text = "Інформація про Військовозабов'язаних і дати занесення до запасу",
                StartPosition = FormStartPosition.CenterScreen
            };

            TextBox textBox = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill
            };

            foreach (var item in militaryAndDates)
            {
                string info = $"{item.Item2}, {item.Item3}";
                textBox.AppendText(info + Environment.NewLine);
            }

            infoForm.Controls.Add(textBox);
            infoForm.Size = new Size(500, 400);
            infoForm.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
