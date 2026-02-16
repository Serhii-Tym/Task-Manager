using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Expenses_Tracker
{
    public partial class ManageTasks : Form
    {
        string dbPath = "tasks.txt";

        private DataGridView dgvExpenses;
        private Button btnDelete;
        private Button btnExit;
        private Button btnReturn;

        public ManageTasks()
        {
            InitializeComponent();
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            this.Size = new Size(700, 500);
            this.Text = "Manage Tasks";


            dgvExpenses = new DataGridView();
            dgvExpenses.Location = new Point(20, 20);
            dgvExpenses.Size = new Size(640, 350);
            dgvExpenses.ColumnCount = 3;
            dgvExpenses.Columns[0].Name = "Task Name";
            dgvExpenses.Columns[1].Name = "Description";
            dgvExpenses.Columns[2].Name = "Due Date";
            dgvExpenses.AllowUserToAddRows = false;
            dgvExpenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpenses.MultiSelect = false;
            this.Controls.Add(dgvExpenses);

            btnDelete = new Button();
            btnDelete.Text = "Delete Task";
            btnDelete.Location = new Point(20, 390);
            btnDelete.Size = new Size(150, 40);
            btnDelete.Click += BtnDelete_Click;
            this.Controls.Add(btnDelete);

            btnReturn = new Button();
            btnReturn.Text = "Return to Menu";
            btnReturn.Location = new Point(265, 390);
            btnReturn.Size = new Size(150, 40);
            btnReturn.Click += BtnReturn_Click;
            this.Controls.Add(btnReturn);

            btnExit = new Button();
            btnExit.Text = "Exit Program";
            btnExit.Location = new Point(510, 390);
            btnExit.Size = new Size(150, 40);
            btnExit.Click += BtnExit_Click;
            this.Controls.Add(btnExit);
        }

        private void LoadData()
        {
            dgvExpenses.Rows.Clear();

            if (File.Exists(dbPath))
            {
                string[] lines = File.ReadAllLines(dbPath);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 3)
                        {
                            string taskName = parts[0];
                            string taskDescription = parts[1];
                            string dueDate = parts[2];
                            dgvExpenses.Rows.Add(taskName, taskDescription, dueDate);
                        }
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this task?",
                                                      "Confirm Delete",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    int selectedIndex = dgvExpenses.SelectedRows[0].Index;

                    dgvExpenses.Rows.RemoveAt(selectedIndex);

                    RewriteFile();
                }
            }
            else
            {
                MessageBox.Show("Please select a task to delete.");
            }
        }

        private void RewriteFile()
        {
            using (StreamWriter sw = new StreamWriter(dbPath))
            {
                foreach (DataGridViewRow row in dgvExpenses.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        string taskName = row.Cells[0].Value.ToString();
                        string taskDescription = row.Cells[1].Value.ToString();
                        string dueDate = row.Cells[2].Value.ToString();
                        sw.WriteLine($"{taskName}|{taskDescription}|{dueDate}");
                    }
                }
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit the program?",
                                                  "Confirm Exit",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}