using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.M_Models
{
     public class M_Sales
    {
        public int SaleID { get; set; }
        public Nullable<System.DateTime> SaleDate { get; set; }
        public Nullable<int> SellerID { get; set; }
        public Nullable<int> BuyerID { get; set; }
        public Nullable<int> ProductID { get; set; }

    }
}
