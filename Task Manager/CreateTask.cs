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

namespace Task_Manager
{
    public partial class CreateTask : Form
    {
        string dbPath = "Tasks.txt";

        public CreateTask()
        {
            InitializeComponent();
        }

        private void SaveTask_Click(object sender, EventArgs e)
        {
            string taskName = textBox1.Text;
            string taskDescription = textBox2.Text;
            string dueDate = textBox3.Text;

            if (string.IsNullOrWhiteSpace(taskName) || string.IsNullOrWhiteSpace(taskDescription) || string.IsNullOrWhiteSpace(dueDate))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to save this task?\n\nName: {taskName}\nDescription: {taskDescription}\nDue Date: {dueDate}",
                                                  "Confirm Save",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(dbPath))
                    {
                        sw.WriteLine($"{taskName}|{taskDescription}|{dueDate}");
                    }

                    MessageBox.Show("Task saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving data: " + ex.Message);
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel? Unsaved data will be lost.",
                                                  "Confirm Cancel",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}