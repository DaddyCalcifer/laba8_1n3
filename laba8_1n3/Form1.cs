using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba8_1n3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> data = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            int count = (int)numericUpDown1.Value;
            data.Clear();
            listBox1.Items.Clear();
            var rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                data.Add(rnd.Next(100).ToString());
                listBox1.Items.Add(data[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый файл (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            string filename = saveFileDialog.FileName;
            System.IO.File.WriteAllLines(filename, data);
            MessageBox.Show("Файл успешно записан.", "Запись");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовый файл (*.txt)|*.txt"; ;
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;
            string filename = openFileDialog.FileName;
            var tempo = System.IO.File.ReadAllLines(filename);
            int last;
            for(int i = tempo.Length-1; i > 0; i--)
            {
                try
                {
                    last = Convert.ToInt32(tempo[i]);
                    MessageBox.Show("Последнее число в файле: " + tempo[i], "Результат");
                    break;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                    return;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый файл (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            string filename = saveFileDialog.FileName;
            if(textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Введите текст!", "Ошибка!");
                return;
            }
            //
            var coding = Encoding.UTF8;
            if (comboBox2.SelectedIndex == 0)
                coding = Encoding.UTF8;
            if (comboBox2.SelectedIndex == 1)
                coding = Encoding.Unicode;
            if (comboBox2.SelectedIndex == 2)
                coding = Encoding.ASCII;
            //
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    System.IO.File.WriteAllText(filename,textBox1.Text.Trim(),coding);
                    break;
                case 1:
                    System.IO.File.WriteAllLines(filename, textBox1.Text.Split(new char[] {'\n','\t' }, StringSplitOptions.RemoveEmptyEntries), coding);
                    break;
                case 2:
                    System.IO.FileStream file1 = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(file1);
                    writer.Write(textBox1.Text.Trim(), coding);
                    writer.Close();
                    break;
                default:
                    MessageBox.Show("Не выбран метод записи!", "Ошибка!");
                    return;
            }
            MessageBox.Show("Файл успешно записан.", "Запись");
        }
    }
}
