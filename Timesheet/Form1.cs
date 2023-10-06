using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timesheet
{
    public partial class Form1 : Form
    {

        private string filePath;

        private bool isSaveClicked = false;            //for without saving close the form

        public Form1()
        {
            InitializeComponent();
            filePath = "C:\\Users\\SuhasC\\source\\repos\\Timesheet\\Timesheet\\bin\\Debug\\TimesheetData.json";    //full path


            //Open Form on Windows Start
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("Timesheet", Application.ExecutablePath);


            //Display the form over other application
            this.TopMost = true;
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(-1);
        }



        private void save_button_Click(object sender, EventArgs e)
        {
            
            try
            {

             
               if (!ValidateChildren(ValidationConstraints.Enabled))
               {
                    return;
               }



                isSaveClicked = true;                           //checked if save button is click



                //Append data into same Json file
                string filepath = GetFilePath();

                List<Info> info = new List<Info>();

                if (File.Exists(filePath))
                {
                    string existingData = File.ReadAllText(filePath);
                  //info = JsonConvert.DeserializeObject<List<Info>>(existingData);
                    info = JsonConvert.DeserializeObject<List<Info>>(existingData) ?? new List<Info>();
                }


/*
                //Save data at one Time in a Day
                if (info.Any(entry => entry.Date == dateTimePicker1.Text))
                {
                    MessageBox.Show("Data for the current day already exists!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Exit without saving
                }
*/


                //Add new data
                info.Add(new Info()
                {
                        
                   Name = Environment.UserName,
                   IP_Address = GetIPAddress(),
                   Date = dateTimePicker1.Text,
                   Team = comboBox1.Text,
                   Work_Details = textBox1.Text,
                    
                });

                

                

                //Append data into Json file
                string json = JsonConvert.SerializeObject(info, Formatting.Indented);
                File.WriteAllText(filePath, json);
                MessageBox.Show("Timesheet Save Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                
                Application.Exit();
            }


            catch
            {
                MessageBox.Show("An error occured!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        


        //Save Data into Json File on Windows Start (Combine the Path)
        private string GetFilePath()
        {

            string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(docpath, "TimesheetData.json");

        }





        //IP Address
        private string GetIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("IP Address not Found!");
        }





        //Form Validation

        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {

            if (dateTimePicker1.Value > DateTime.Now)
            {
                e.Cancel = true;
                dateTimePicker1.Focus();
                errorProvider.SetError(dateTimePicker1, "Please Select a Valid Date!");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(dateTimePicker1, null);
            }
        }


        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                e.Cancel = true;
                comboBox1.Focus();
                errorProvider.SetError(comboBox1, "Please Select Team From Dropdown!");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(comboBox1, null);
            }
        }


        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                //MessageBox.Show("Please Enter Some Text!", " Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                textBox1.Focus();
                errorProvider.SetError(textBox1, "Please Enter Some Text!");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textBox1, null);
            }

        }




        //Form Closing button event
        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if(comboBox1.SelectedIndex == -1 || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please Fill the Form!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else if (!isSaveClicked)
            {
                MessageBox.Show("Please Save the Timesheet before closing the form!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }




        //Space in Textbox
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as TextBox).SelectionStart == 0)
                e.Handled = (e.KeyChar == (char)Keys.Space);
            else
                e.Handled = false;
        }



    }
}
