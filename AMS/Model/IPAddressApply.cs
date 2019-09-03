using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class IPAddressApply
    {
        public int ApplyID { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        public string PortNumber { get; set; }
        public DateTime? TimeLimit { get; set; }
    }
}