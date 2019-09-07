using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Approve
    {
        public int ApproveID { get; set; }
        public string ApplyUser { get; set; }
        public string Title { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string ApproveUser { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime ApproveDate { get; set; }
        public byte Status { get; set; }
        public byte Result { get; set; }
        public string Remark { get; set; }
    }
}
