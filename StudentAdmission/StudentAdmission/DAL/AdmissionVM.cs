using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAdmission.DAL
{
    public class AdmissionVM
    {
        public int AdmissionID { get; set; }
        public int? StudentMasterID { get; set; }
        public int? SubjectID { get; set; }
        public decimal? Credits { get; set; }
        public decimal? Semester { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal? TotalFee { get; set; }
        public string SubjectName { get; set; }
    }
}
