using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(-1);
        }



        private void save_button_Click(object sender, EventArgs e)
        {
            
            try
            {

                if (!Validateform())
                {
                    return;
                }



                //Append data into same Json file
                string filepath = GetFilePath();

                List<Info> info = new List<Info>();

                if (File.Exists(filePath))
                {
                    string existingData = File.ReadAllText(filePath);
                    info = JsonConvert.DeserializeObject<List<Info>>(existingData);
                  //info = JsonConvert.DeserializeObject<List<Info>>(existingData) ?? new List<Info>();
                }


                //Add new data
                info.Add(new Info()
                {
                    Name = Environment.UserName,
                    Date = dateTimePicker1.Text,
                    Team = comboBox1.Text,
                    Work_Details = textBox1.Text,
                });



                //Append data into Json file
                string json = JsonConvert.SerializeObject(info, Formatting.Indented);
                File.WriteAllText(filePath, json);
                MessageBox.Show("Timesheet Save Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Reset();
                Application.Exit();
            }


            catch
            {
                MessageBox.Show("An error occured!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }


        private bool Validateform()
        {

            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Please Select a Valid Date!", " Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            if(comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Team from Dropdown!", " Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please Enter Some Text!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

/*
            var storedDate = File.Exists(filePath) ? File.ReadAllText(filePath) : null;
            if (storedDate == DateTime.Today.ToShortDateString()) 
            {
                Application.Exit();
            }
            File.WriteAllText (filePath, DateTime.Today.ToShortDateString());
*/



            return true;

        }



        //Save Data into Json File on Windows Start (Combine the Path)
        private string GetFilePath()
        {

            string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(docpath, "TimesheetData.json");

        }





        private void Reset()
        {
            comboBox1.Text = null;
            textBox1.Text = null;
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Fill the Form!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }



    }
}
