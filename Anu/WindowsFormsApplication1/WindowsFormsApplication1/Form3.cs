﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public SqlConnection sqlConnection = new SqlConnection(@"Data Source = ANUYUKSHA\SQLEXPRESS; Initial Catalog = anu; Integrated Security = True");
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int m,n, z;
            string a, b;
            a = textBox1.Text;
            b = textBox2.Text;
            m= int.Parse(a);
            n = int.Parse(b);
            CheckState state = checkBox1.CheckState;
            CheckState state2 = checkBox2.CheckState;
            if (m< 1944 )
            {
                MessageBox.Show("enter year greater than 1944", "Error");
            }
            else if (m > 2013)
            {
                MessageBox.Show("enter year less than  2013", "Error");
            }
            else
            {
                if (checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    MessageBox.Show("select male , female or both", "Error");
                }
                else
                {
                    
                    if (checkBox1.Checked == true && checkBox2.Checked == true)
                    {
                        SqlCommand sqlComman = new SqlCommand();
                        sqlComman.Connection = sqlConnection;
                        sqlConnection.Open();
                        sqlComman.CommandText = "SELECT count(*) FROM  merg where Year ='" + textBox1.Text + "'  ";
                        sqlComman.ExecuteNonQuery();
                        z = Convert.ToInt32(sqlComman.ExecuteScalar().ToString());
                        sqlConnection.Close();
                        if(n<=z)
                        {
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = sqlConnection;
                            sqlConnection.Open();
                            sqlCommand.CommandText =  "select top " + textBox2.Text + " [Given Name],Amount  from merg where Year='" + textBox1.Text + "' order by CAST(Amount as int) desc; ";
                            sqlCommand.ExecuteNonQuery();
                            sqlConnection.Close();
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("enter valid number in 'TOP' space  ", "Error");
                        }
                    }

                    else if (checkBox1.Checked)
                    {
                        SqlCommand sqlComman = new SqlCommand();
                        sqlComman.Connection = sqlConnection;
                        sqlConnection.Open();
                        sqlComman.CommandText = "SELECT count(*) FROM fem where Year ='" + textBox1.Text + "'  ";
                        sqlComman.ExecuteNonQuery();
                        z = Convert.ToInt32(sqlComman.ExecuteScalar().ToString());
                        sqlConnection.Close();
                        if(n<=z)
                        {
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = sqlConnection;
                            sqlConnection.Open();
                            sqlCommand.CommandText = "SELECT top " + textBox2.Text + " [Given Name],Amount  FROM malef where Year='" + textBox1.Text + "' order by CAST(Amount as int) desc;  ";
                            sqlCommand.ExecuteNonQuery();
                            sqlConnection.Close();
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("enter valid number in 'TOP' space  ", "Error");
                        }
                    }
                    else if (checkBox2.Checked)
                    {
                        SqlCommand sqlComman = new SqlCommand();
                        sqlComman.Connection = sqlConnection;
                        sqlConnection.Open();
                        sqlComman.CommandText = "SELECT count(*) FROM malef where Year ='" + textBox1.Text + "' ";
                        sqlComman.ExecuteNonQuery();
                        z = Convert.ToInt32(sqlComman.ExecuteScalar().ToString());
                        sqlConnection.Close();
                        if(n<=z)
                        {
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = sqlConnection;
                            sqlConnection.Open();
                            sqlCommand.CommandText = "SELECT top " + textBox2.Text + " [Given Name],Amount  FROM fem where Year='" + textBox1.Text + "' order by CAST(Amount as int) desc; ";
                            sqlCommand.ExecuteNonQuery();
                            sqlConnection.Close();
                            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("enter valid number in 'TOP' space  ", "Error");
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 b3 = new Form1();
            b3.ShowDialog();
        }
    }
}
