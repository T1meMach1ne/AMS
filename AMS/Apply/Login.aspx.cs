using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Apply
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ibLogin_Click1(object sender, ImageClickEventArgs e)
        {
            Model.UserInfo u = BLL.UserInfo.UserLogin(txtID.Text, txtPassword.Text);
            if (u != null)
            {
                Session["User"] = u;
                if (u.Type == 1)
                {
                    Response.Redirect("MyApply.aspx");
                }
                else if (u.Type == 2)
                {
                    Response.Redirect("DepartmnetManager.aspx");
                }
            }
            else
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('用户名或密码无效！');", true);
            }
        }
       
    }
}
