using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class Login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 

            }
        }
        public bool VaildateNull()
        {
            bool nu = true;
            if (string.IsNullOrEmpty(txtID.Text))
            {
                lblUserID.Text = "请填写用户名！—_—";
                nu = false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblPwd.Text = "请填写密码！—_—";
                nu = false;
            }
            return nu;
        }
        protected void ibLogin_Click(object sender, ImageClickEventArgs e)
        {
            if (VaildateNull())
            {
                Model.UserInfo u = BLL.UserInfo.UserLogin(txtID.Text, txtPassword.Text);
                if (u != null)
                {
                    Session["User"] = u;
                    if (u.UserType == 0)
                    {
                        Response.Redirect("MyAttendance.aspx");
                    }
                    else if (u.UserType == 1)
                    {
                        Response.Redirect("AttendanceManage.aspx");
                    }
                    else if (u.UserType == 2)
                    {
                        Response.Redirect("AttendanceSetting.aspx");
                    }
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('用户名或密码有误，请重新填写！');", true);
                }
            }                      
        }
    }
}