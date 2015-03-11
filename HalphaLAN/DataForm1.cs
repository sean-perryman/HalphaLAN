using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HalphaLAN
{
    public partial class DataViewForm : Form
    {
        private BindingSource bindingSource;
        private Form aboutBox;
        private Form helpBox;

        public DataViewForm()
        {
            InitializeComponent();

            //About Box
            aboutBox = new AboutBox1();

            //Help Box
            helpBox = new HelpForm();

            bindingSource = new BindingSource();
            bindingSource.DataSource = dataGridView1.DataSource;
            
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
            bindingSource.Filter = searchFieldColumnName + " like '%" + searchValue + "%'";
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            //Remove filter from binding source to clear the search
            searchField.Text = "";
            bindingSource.RemoveFilter();
        }

        private void aboutHalphaLANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox.Show();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            helpBox.Show();
        }
    }
}
