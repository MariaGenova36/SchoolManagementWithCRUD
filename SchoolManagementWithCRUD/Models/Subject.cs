using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWithCRUD.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Teacher { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}

