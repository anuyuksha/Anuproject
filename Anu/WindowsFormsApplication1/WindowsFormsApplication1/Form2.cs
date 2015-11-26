using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using System.Configuration;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public SqlConnection sqlConnection = new SqlConnection();
        public Form2()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 b3 = new Form1();
            b3.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = "select distinct [Given Name] from merg";
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            while (reader.Read())
            {
                MyCollection.Add(reader.GetString(0));
            }
            textBox1.AutoCompleteCustomSource = MyCollection;
            reader.Close();
            sqlConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();


            int x, y;

            string a;
            a = textBox2.Text;
            x = int.Parse(a);
            if (x < 1944 || x > 2013)
            {
                MessageBox.Show("please enter year between 1944 and 2013", "Error");
            }
            else
            {
                if (checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    MessageBox.Show("please select male , female or both", "Error");
                }
                else
                {
                    if (checkBox1.Checked == true && checkBox2.Checked == true)
                    {
                        for (y = x; y <= 2013; y++)
                        {
                            int q, p;
                            string l, n;
                            q = y;
                            l = y.ToString();
                            SqlCommand sqlComman = new SqlCommand();
                            sqlComman.Connection = sqlConnection;
                            sqlConnection.Open();
                            sqlComman.CommandText = "select sum(cast(Amount as int)) from merg where [Given Name] = '" + textBox1.Text + "' AND Year = '" + l + "'; ";

                            sqlComman.ExecuteNonQuery();

                            object d = sqlComman.ExecuteScalar();
                            if (d == null || d == DBNull.Value)
                            {
                                p = 0;
                            }
                            else
                            {


                                n = sqlComman.ExecuteScalar().ToString();
                                p = int.Parse(n);

                            }
                            sqlConnection.Close();
                            chart1.Series["Series1"].Points.AddXY(q, p);
                            chart1.Series["Series1"].ChartType = SeriesChartType.Column;

                        }
                    }
                    else if (checkBox1.Checked)
                    {
                        for (y = x; y <= 2013; y++)
                        {
                            int q, p;
                            string l, n;
                            q = y;
                            l = y.ToString();
                            SqlCommand sqlComman = new SqlCommand();
                            sqlComman.Connection = sqlConnection;
                            sqlConnection.Open();
                            sqlComman.CommandText = "SELECT amount FROM malef where [Given Name] = '" + textBox1.Text + "'  AND Year = '" + l + "';";

                            sqlComman.ExecuteNonQuery();

                            object d = sqlComman.ExecuteScalar();
                            if (d == null || d == DBNull.Value)
                            {
                                p = 0;
                            }
                            else
                            {


                                n = sqlComman.ExecuteScalar().ToString();
                                p = int.Parse(n);

                            }
                            sqlConnection.Close();
                            chart1.Series["Series1"].Points.AddXY(q, p);
                            chart1.Series["Series1"].ChartType = SeriesChartType.Column;
                        }
                    }

                    else if (checkBox2.Checked)
                    {
                        for (y = x; y <= 2013; y++)
                        {
                            int q, p;
                            string l, n;
                            q = y;
                            l = y.ToString();
                            SqlCommand sqlComman = new SqlCommand();
                            sqlComman.Connection = sqlConnection;
                            sqlConnection.Open();
                            sqlComman.CommandText = "SELECT amount FROM fem where [Given Name] = '" + textBox1.Text + "' AND Year = '" + l + "'; ";

                            sqlComman.ExecuteNonQuery();

                            object d = sqlComman.ExecuteScalar();
                            if (d == null || d == DBNull.Value)
                            {
                                p = 0;
                            }
                            else
                            {


                                n = sqlComman.ExecuteScalar().ToString();
                                p = int.Parse(n);

                            }
                            sqlConnection.Close();
                            chart1.Series["Series1"].Points.AddXY(q, p);
                            chart1.Series["Series1"].ChartType = SeriesChartType.Column;
                        }
                    }


                }
            }
        }
    }
}
