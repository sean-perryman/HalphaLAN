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
        BindingSource bindingSource;

        public DataViewForm()
        {
            InitializeComponent();
            searchFieldComboBox.Items.Add("Patient ID");
            searchFieldComboBox.Items.Add("First Name");
            searchFieldComboBox.Items.Add("Last Name");
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
    }
}
