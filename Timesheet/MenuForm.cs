using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timesheet
{
    public partial class MenuForm : Form
    {
        //Timesheet Panel


        private List<Info> info = new List<Info>();

        private string filePath;

        public MenuForm()
        {
            InitializeComponent();

            filePath = GetFilePath();

            //Open Form on Windows Start
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("Timesheet", Application.ExecutablePath);



            //Display Data after selecting Node from Records treeview
            treeView.AfterSelect += treeView_AfterSelect;
        }

        
        private void MenuForm_Load(object sender, EventArgs e)
        {
            datePicker.Value = DateTime.Now.AddDays(-1);
       
            if (datePicker.Value.DayOfWeek == DayOfWeek.Saturday || datePicker.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                datePicker.Value = DateTime.Now.AddDays(-3);
            }
           

        }



        private void tsOpenButton_Click(object sender, EventArgs e)
        {
            rsPanel.SendToBack();
        }

        private void recordsOpenButton_Click(object sender, EventArgs e)
        {
            rsPanel.BringToFront();
        }



        private void tsPanel_Paint(object sender, PaintEventArgs e)
        {

        }


        //Dynamic filePath
         private string GetFilePath()
         {
             string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
             string appName = "Timesheet"; 
             string fileName = "TimesheetData.json";
             string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
             return Path.Combine(appDataPath, appName, fileName, fullPath);
         }



        private void save_button_Click(object sender, EventArgs e)
        {

            try
            {


                if (!ValidateChildren(ValidationConstraints.Enabled))
                {
                    return;
                }


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
                File.WriteAllText(filepath, json);
                MessageBox.Show("Timesheet Save Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch
            {
                MessageBox.Show("An error occured!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }



        //IP Address
        private string GetIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("IP Address not Found!");
        }



        //Space block
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as TextBox).SelectionStart == 0)
                e.Handled = (e.KeyChar == (char)Keys.Space);
            else
                e.Handled = false;
        }


        //Validations
        private void datePicker_Validating(object sender, CancelEventArgs e)
        {
            // Check if the selected date is a Saturday or Sunday
            if (datePicker.Value.DayOfWeek == DayOfWeek.Saturday || datePicker.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                e.Cancel = true;
                datePicker.Focus();
                errorProvider.SetError(datePicker, "Please choose a weekday (Monday to Friday)");
            }
            else if (datePicker.Value > DateTime.Now)
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



        //Update
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

                    MessageBox.Show("Record Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        //Delete
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

                    MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        




//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




        //Records Panel

        private void rsPanel_Paint(object sender, PaintEventArgs e)
        {
            LoadJsonData();
        }

        private void LoadJsonData()
        {
            try
            {
                filePath = GetFilePath();

                if (System.IO.File.Exists(filePath))
                {
                    string existingData = System.IO.File.ReadAllText(filePath);

                    List<Info> infoList = JsonConvert.DeserializeObject<List<Info>>(existingData) ?? new List<Info>();

                    // Populate TreeView
                    PopulateTreeView(infoList);
                }

                else if(!System.IO.File.Exists(filePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    File.WriteAllText(filePath, "[]");
                }

                else
                {
                    MessageBox.Show("JSON file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void PopulateTreeView(List<Info> infoList)
        {
            treeView.Nodes.Clear();

            //by month
            var entriesByMonth = infoList.GroupBy(info => DateTime.Parse(info.Date).ToString("MMMM yyyy"));

            foreach (var monthGroup in entriesByMonth)
            {
                TreeNode monthNode = new TreeNode(monthGroup.Key);

                foreach (Info info in monthGroup)
                {
                    TreeNode dayNode = new TreeNode($"{info.Date}");

                    dayNode.Nodes.Add($"Name: {info.Name}");
                    dayNode.Nodes.Add($"IP Address: {info.IP_Address}");
                    dayNode.Nodes.Add($"Team: {info.Team}");
                    dayNode.Nodes.Add($"Work Details: {info.Work_Details}");

                    monthNode.Nodes.Add(dayNode);
                }

                treeView.Nodes.Add(monthNode);
            }
        }



        private void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //treview
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (treeView.SelectedNode != null && e.Node.Parent != null && treeView.SelectedNode.Nodes.Count > 0)
                {

                    tsPanel.BringToFront();
         
                    rsPanel.SendToBack();

                    string selectedDate = e.Node.Text;
                    string selectedTeam = e.Node.Nodes[2].Text.Replace("Team: ", "");
                    string selectedWorkDetails = e.Node.Nodes[3].Text.Replace("Work Details: ", "");

                    datePicker.Text = selectedDate;
                    comboBox.Text = selectedTeam;
                    textBox.Text = selectedWorkDetails;
                }
            }
            catch
            {

            }
            
        }





    }
}
