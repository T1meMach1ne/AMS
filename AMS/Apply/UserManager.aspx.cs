using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Apply
{
    public partial class UserManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = BLL.Department.SelectDept("");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlDept.Items.Add(new ListItem(dt.Rows[i]["DeptName"].ToString(), dt.Rows[i]["DeptID"].ToString()));
                }
            }
        }
    }
}