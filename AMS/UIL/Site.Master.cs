using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace UIL
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //页面第一次加载
                Model.UserInfo u = (Model.UserInfo)Session["User"];
                lblUserName.Text = u.UserName;
                if (u.UserType == 0)
                {
                    lblUserType.Text = "员工";
                }
                else if (u.UserType == 1)
                {
                    lblUserType.Text = "主管";
                }
                else
                {
                    lblUserType.Text = "系统管理员";
                }
            }
        }

        protected void lbExit_Click(object sender, EventArgs e)
        {
            //清空Session里面的数据
            Session.Clear();
            //清空cookie里面的数据
            FormsAuthentication.SignOut();
            //实现页面的跳转
            Response.Redirect("~/Login.aspx");
        }
    }
}