using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Apply
    {
        public int ApplyID { get; set; }
        public int ApplyTypeID { get; set; }
        public DateTime? ApplyDate { get; set; }
        public int ApplyStatus { get; set; }
        public string UserID { get; set; }
        public string Approver { get; set; }
        public string Assigner { get; set; }
        public string Dealer { get; set; }
        public string Phone { get; set; }
        public string ApplyTitle { get; set; }
        public string ApplyReason { get; set; }
        public string Enclosure { get; set; }
        public string Remark { get; set; }
    }
}