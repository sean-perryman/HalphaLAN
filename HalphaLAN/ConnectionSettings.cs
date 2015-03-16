using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace HalphaLAN
{
    public partial class ConnectionSettings : Form
    {
        public ConnectionSettings()
        {
            InitializeComponent();
            SetTextBoxes();
        }

        private void UpdateConnectionString() {
            string connectionString = "server=" + serverBox.Text + ";" +
                                      "username=" + usernameBox.Text + ";" +
                                      "password=" + passwordBox.Text + ";" +
                                      "database=" + databaseBox.Text + ";";

            var settings = ConfigurationManager.ConnectionStrings["HalphaLAN.Properties.Settings.gseConnectionString"];
            var fi = typeof(ConfigurationElement).GetField("_bReadOnly", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            fi.SetValue(settings, false);
            settings.ConnectionString = connectionString;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            SetTextBoxes();
            this.Hide();
        }

        private void SetTextBoxes()
        {
            string t = ConfigurationManager.ConnectionStrings["HalphaLAN.Properties.Settings.gseConnectionString"].ConnectionString.ToString();
            if (t != null)
            {
                string[] tSplit = t.Split(';');

                int stage = 1;
                foreach (string setting in tSplit)
                {
                    string[] subString = setting.Split('=');

                    foreach (string value in subString)
                    {
                        switch (stage)
                        {
                            case 2:
                                serverBox.Text = value;
                                stage++;
                                break;                            
                            case 4:
                                usernameBox.Text = value;
                                stage++;
                                break;
                            case 6:
                                passwordBox.Text = value;
                                stage++;
                                break;
                            case 8:
                                databaseBox.Text = value;
                                stage++;
                                break;
                            default:
                                stage++;
                                break;
                        }

                    }
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //UpdateConnectionString();
            MessageBox.Show("This feature is not available yet.");
            this.Hide();
        }
    }
}
