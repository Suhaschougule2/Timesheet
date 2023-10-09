using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Timesheet
{
    public partial class Records : Form
    {
        public Records()
        {
            InitializeComponent();
        }

        private void Records_Load(object sender, EventArgs e)
        {
            LoadJsonData();
        }


        private void LoadJsonData()
        {
            try
            {
                string filePath = "C:\\Users\\SuhasC\\source\\repos\\Timesheet\\Timesheet\\bin\\Debug\\TimesheetData.json";

                if (System.IO.File.Exists(filePath))
                {
                    // Read JSON data from the file
                    string jsonData = System.IO.File.ReadAllText(filePath);

                    // Deserialize JSON into a list of Info objects
                    List<Info> infoList = JsonConvert.DeserializeObject<List<Info>>(jsonData);

                    // Populate TreeView
                    PopulateTreeView(infoList);
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


    }
}
