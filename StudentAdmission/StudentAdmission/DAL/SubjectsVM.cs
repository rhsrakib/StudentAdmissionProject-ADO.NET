using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAdmission.DAL
{
    public class SubjectsVM
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public decimal? Credits { get; set; }
        public decimal? TotalCredits { get; set; }
    }
}
