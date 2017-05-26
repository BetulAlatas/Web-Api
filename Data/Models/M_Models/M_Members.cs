using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.M_Models
{
    public class M_Members
    {
        public int MemberID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> RecordDate { get; set; }
        public string City { get; set; }
        public Nullable<double> PointScore { get; set; }
        public Nullable<int> Photo { get; set; }
        public Nullable<int> MemberType { get; set; }
        public string FacebookPhotoURL { get; set; }
        public Nullable<bool> MemberStatus { get; set; }
        public string FireBaseToken { get; set; }
    }
}
