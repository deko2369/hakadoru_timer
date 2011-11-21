using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hakadoru
{
    public partial class Form1 : Form
    {
        private DateTime dt1, dt2, stop;
        private double hours;
        private bool start = false, oncerun = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || e.KeyChar > '9')
            {
                if (e.KeyChar == '.' &&
                    textBox1.Text.IndexOf('.') >= 0)
                {
                    e.Handled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!start)
            {
                if (!oncerun)
                {
                    hours = double.Parse(textBox1.Text);
                    dt1 = DateTime.Now.AddHours(hours);
                    oncerun = true;
                }
                else
                {
                    TimeSpan ts = DateTime.Now - stop;
                    dt1 = dt1.Add(ts);
                }
                button2.Enabled = false;
            }
            else
            {
                stop = DateTime.Now;
                button2.Enabled = true;
            }

            start = !start;
            timer1.Enabled = start;
            textBox1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan diff;

            dt2 = DateTime.Now;

            diff = dt1 - dt2;
            if (diff.TotalHours <= 0)
            {
                start = oncerun = timer1.Enabled = false;
                button2.Enabled = true;
                button2_Click(sender, e);
                MessageBox.Show("done");
                return;
            }

            label2.Text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                diff.Hours + diff.Days * 24, diff.Minutes, diff.Seconds);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            oncerun = false;
            label2.Text = "00:00:00";
            textBox1.Enabled = true;
        }
    }
}
