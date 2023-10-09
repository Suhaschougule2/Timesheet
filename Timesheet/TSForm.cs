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
    public partial class Timesheet_Form : Form
    {

        private List<Info> info = new List<Info>();

        private string filePath;

        private bool isSaveClicked = false;            //for without saving close the form

       

        public Timesheet_Form()
        {
            InitializeComponent();
            filePath = "C:\\Users\\SuhasC\\source\\repos\\Timesheet\\Timesheet\\bin\\Debug\\TimesheetData.json";    //full path


            //Open Form on Windows Start
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("Timesheet", Application.ExecutablePath);


            //Display the form over other application
            this.TopMost = true;
            

        }

        private void TSForm_Load(object sender, EventArgs e)
        {
            datePicker.Value = DateTime.Now.AddDays(-1);
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



                //Save data at one Time in a Day
                if (info.Any(entry => entry.Date == datePicker.Text))
                {
                    MessageBox.Show("Data for the current day already exists!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Exit without saving
                }



                //Add new data
                info.Add(new Info()
                {
                        
                   Name = Environment.UserName,
                   IP_Address = GetIPAddress(),
                   Date = datePicker.Text,
                   Team = comboBox.Text,
                   Work_Details = textBox.Text,
                    
                });

                

                

                //Append data into Json file
                string json = JsonConvert.SerializeObject(info, Formatting.Indented);
                File.WriteAllText(filePath, json);
                MessageBox.Show("Timesheet Save Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                
               
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
        private void datePicker_Validating(object sender, CancelEventArgs e)
        {
            if (datePicker.Value > DateTime.Now)
            {
                e.Cancel = true;
                datePicker.Focus();
                errorProvider.SetError(datePicker, "Please Select a Valid Date!");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(datePicker, null);
            }
        }

        private void comboBox_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox.SelectedIndex == -1)
            {
                e.Cancel = true;
                comboBox.Focus();
                errorProvider.SetError(comboBox, "Please Select Team From Dropdown!");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(comboBox, null);
            }
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                //MessageBox.Show("Please Enter Some Text!", " Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                textBox.Focus();
                errorProvider.SetError(textBox, "Please Enter Some Details of your Work!");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textBox, null);
            }
        }




        //Form Closing button event
        private void TSForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (comboBox.SelectedIndex == -1 || string.IsNullOrWhiteSpace(textBox.Text))
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
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((sender as TextBox).SelectionStart == 0)
                e.Handled = (e.KeyChar == (char)Keys.Space);
            else
                e.Handled = false;
        }



        //Update Data in TextBox
        private void update_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateChildren(ValidationConstraints.Enabled))
                {
                    return;
                }



                if (File.Exists(filePath))
                {
                    string existingData = File.ReadAllText(filePath);
                    info = JsonConvert.DeserializeObject<List<Info>>(existingData) ?? new List<Info>();
                }


                // Find the entry to update based on the selected date
                Info entryToUpdate = info.Find(entry => entry.Date == datePicker.Text);


                if (entryToUpdate != null)
                {
                    // Update the relevant information
                    entryToUpdate.Team = comboBox.Text;
                    entryToUpdate.Work_Details = textBox.Text;



                    string json = JsonConvert.SerializeObject(info, Formatting.Indented);
                    File.WriteAllText(filePath, json);

                    MessageBox.Show("Timesheet Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show("No Entry found for the selected date!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            catch
            {
                MessageBox.Show("An error occured!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
         
        //Delete button to Delete the Record from Json File
        private void delete_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string existingData = File.ReadAllText(filePath);
                    info = JsonConvert.DeserializeObject<List<Info>>(existingData) ?? new List<Info>();
                }

                //to delete based on the selected date
                Info entryToDelete = info.Find(entry => entry.Date == datePicker.Text);

                if (entryToDelete != null)
                {
                    // Remove the entry
                    info.Remove(entryToDelete);

                    string json = JsonConvert.SerializeObject(info, Formatting.Indented);
                    File.WriteAllText(filePath, json);

                    MessageBox.Show("Timesheet Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No entry found for the selected date!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


       
        private void showTreeView_button_Click_1(object sender, EventArgs e)
        {
            // Open the RecordsForm
            using (Records tsrecords = new Records())
            {
                tsrecords.ShowDialog();
            }
        }
    }
}
