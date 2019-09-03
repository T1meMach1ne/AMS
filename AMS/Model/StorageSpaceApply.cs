using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class StorageSpaceApply
    {
        public int ApplyID { get; set; }
        public int Zone { get; set; }
        public string InterfaceMan { get; set; }
        public string Right { get; set; }
        public DateTime? TimeLimit { get; set; }
    }
}