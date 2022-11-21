using FitnessWpfApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessWpfApp.Model
{
    public  partial class CourseRegistration
    {
        public string IsDoneString => isDone ? "Завершен" : "В процессе";
    }
}
