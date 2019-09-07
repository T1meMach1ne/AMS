using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class AttendanceSetting
    {
        public int SettingID { get; set; }
        public DateTime Date { get; set; }
        public byte Status { get; set; }
    }
}
