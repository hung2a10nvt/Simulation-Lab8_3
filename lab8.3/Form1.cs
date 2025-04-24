using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8._3
{
    public partial class Form1 : Form
    {
        private int GenerateEvent()
        {
            double a = rnd.NextDouble();
            double A = a;
            int k = 1;

            while (k <= probT.Length)
            {
                A -= probT[k - 1];
                if (A <= 0)
                {
                    return k - 1;
                }
                k++;
            }

            return probT.Length - 1; 
        }
        public Form1()
        {
            InitializeComponent();
        }
        double[] probT, probEx;
        int N;
        int[] stat;
        Random rnd = new Random();
        double a;
        private void button1_Click(object sender, EventArgs e)
        {
            probT = new double[5];
            probEx = new double[5];
            stat = new int[5];
            N = int.Parse(textBox6.Text);
            if (!double.TryParse(textBox1.Text, out probT[0]) ||
                !double.TryParse(textBox2.Text, out probT[1]) ||
                !double.TryParse(textBox3.Text, out probT[2]) ||
                !double.TryParse(textBox4.Text, out probT[3]))
            {
                MessageBox.Show("Пожалуйста, введите корректные вероятности.");
                return;
            }
            probT[4] = 1 - (probT[0] + probT[1] + probT[2] + probT[3]);
            if (probT[4] < 0)
            {
                MessageBox.Show("Сумма вероятностей > 1.");
                return;
            }
            textBox5.Text = probT[4].ToString("F3");

            for (int i = 0; i < N; i++)
            {
                int eventIndex = GenerateEvent();
                stat[eventIndex]++;
            }

            for (int i = 0; i<5; i++)
            {
                probEx[i] = stat[i] / (double)N;
            }

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Name = "Эмпирические";
            if (chart1.Series.Count == 1)
            {
                chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Теоретические"));
                chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                chart1.Series[1].Color = Color.Red;
            }
            chart1.Series[1].Points.Clear();

            for (int i = 0; i < 5; i++)
            {
                chart1.Series[0].Points.AddXY(i + 1, probEx[i]);
                chart1.Series[0].Points[i].Label = probEx[i].ToString("F3");
                chart1.Series[1].Points.AddXY(i + 1, probT[i]);
                chart1.Series[1].Points[i].Label = probT[i].ToString("F3");
            }
        }
    }
}
