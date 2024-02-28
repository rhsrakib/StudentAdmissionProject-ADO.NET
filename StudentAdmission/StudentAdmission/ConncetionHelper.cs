using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAdmission
{
    internal class ConncetionHelper
    {
        public static string Student

        {
            get
            {
                string dbPath = Path.Combine(Path.GetFullPath(@"..\..\"), "StudentAdmissionDB.mdf");
                return $@"Data Source=(localdb)\mssqllocaldb;AttachDbFilename={dbPath};Initial Catalog=StudentAdmissionDB;Trusted_Connection=True";
            }
        }
    }
}
