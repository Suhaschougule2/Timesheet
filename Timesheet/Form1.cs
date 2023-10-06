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



                //Append data into same Json file
                string filepath = GetFilePath();

                List<Info> info = new List<Info>();

                if (File.Exists(filePath))
                {
                    string existingData = File.ReadAllText(filePath);
                  //info = JsonConvert.DeserializeObject<List<Info>>(existingData);
                    info = JsonConvert.DeserializeObject<List<Info>>(existingData) ?? new List<Info>();
                }

                

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





        //TextBox AutoSize 
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AutoSizeTextBox(textBox1);
            textBox1.Multiline = true;
        }


        private void AutoSizeTextBox(TextBox textBox)
        {
            int border = 2; // Adjust this value based on your TextBox border
            int padding = textBox.Height - textBox.ClientRectangle.Height;


            TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
            int newHeight = TextRenderer.MeasureText(textBox.Text, textBox.Font, new Size(textBox.Width, int.MaxValue), flags).Height + padding + border;


            if (newHeight > textBox.Height)
            {
                textBox.Height = newHeight;
            }

            else if (textBox.Lines.Length <= 1)
            {
                // Reset the height to the default if the text is empty or a single line
                textBox.Height = textBox.Font.Height + padding + border;
            }
        }







        //Form validation

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

        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please Fill the Form!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else 
            {
                MessageBox.Show("Please Save the Form!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
