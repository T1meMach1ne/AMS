using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserInfo
    {
        public string UserID { get; set; }//自动实现的属性:简化程序员封装属性的代码。
        public string UserName { get; set; }
        public int DeptID { get; set; }
        public string Password { get; set; }
        public string Cellphone { get; set; }
        public byte UserType { get; set; }
    }
}
