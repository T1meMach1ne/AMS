using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class OperateLog
    {
        public int ID { get; set; }
        public int ApplyID { get; set; }
        public string OperateType { get; set; }
        public DateTime? OperateDate { get; set; }
        public string UserID { get; set; }
        public string Describe { get; set; }
        public string Result { get; set; }
    }
}