using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Common;

namespace HalphaLAN
{
    public partial class DataViewForm : Form
    {
        private Form aboutBox;
        private Form helpBox;
        private BindingSource bSource;

        public DataViewForm()
        {
            InitializeComponent();

            //About Box
            aboutBox = new AboutBox1();

            //Help Box
            helpBox = new HelpForm();

            //Populate dataGridView1
            bSource = new BindingSource();
            MySQL_ToDatagridview();

            // Populate field search box and set the default to last name
            searchFieldComboBox.Items.Add("Patient ID");
            searchFieldComboBox.Items.Add("First Name");
            searchFieldComboBox.Items.Add("Last Name");
            searchFieldComboBox.SelectedIndex = 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'gseDataSet.patient' table. You can move, or remove it, as needed.            
            this.patientTableAdapter.Fill(this.gseDataSet.patient);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchFieldColumnName;

            //Pull info from ComboBox
            switch (searchFieldComboBox.Text)
            {
                case "Patient ID":
                    searchFieldColumnName = "patientID";
                    break;
                case "First Name":
                    searchFieldColumnName = "first_name";
                    break;
                case "Last Name":
                    searchFieldColumnName = "last_name";
                    break;
                default:
                    return;
            }
            
            //Apply Search
            string searchValue = searchField.Text;
            
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            bSource.Filter = searchFieldColumnName + " like '%" + searchValue + "%'";
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            //Remove filter from binding source to clear the search
            searchField.Text = "";
            bSource.RemoveFilter();
        }

        private void aboutHalphaLANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox.Show();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            helpBox.Show();
        }

        private void MySQL_ToDatagridview()
        {
            string connString = "server=192.168.1.89;" +
                                "uid=gse;" +
                                "pwd=gsepassword;" +
                                "database=gse_pt;";
            MySqlConnection conn;
           
            try {
                conn = new MySqlConnection();
                conn.ConnectionString = connString;
                conn.Open();
                
                MySqlDataAdapter da = new MySqlDataAdapter();
                string sqlQuery = "SELECT * FROM patient";
                da.SelectCommand = new MySqlCommand(sqlQuery, conn);

                DataTable table = new DataTable();
                da.Fill(table);

                bSource.DataSource = table;
                
                dataGridView1.DataSource = bSource;

                conn.Close();
            } catch (MySqlException e) {
                MessageBox.Show(e.Message);
            }

           
        }
    }
}
