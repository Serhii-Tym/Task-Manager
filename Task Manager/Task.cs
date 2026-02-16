using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager
{
    internal class Task
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string DueDate { get; set; }

        // Method to save in string format: "TaskName|TaskDescription|DueDate"
        public override string ToString()
        {
            return $"{TaskName}|{TaskDescription}|{DueDate}";
        }
    }
}