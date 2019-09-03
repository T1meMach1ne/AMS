using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Apply
{
    public partial class MyApply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                Model.UserInfo u = (Model.UserInfo)Session["User"];
                lblApplyDept1.InnerText = BLL.Department.GetSingleDept(u.DeptID).DeptName;
                lblUserID1.InnerText = u.UserID;
                lblApplyName1.InnerText = u.Name;
                txtTelphone1.Value = u.Telephone;
            }
        }
    }
}