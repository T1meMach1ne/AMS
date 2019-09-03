using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace Apply
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Model.UserInfo u = (Model.UserInfo)Session["User"];
                UserID.Value = u.UserID.ToString().Trim();
                txtUserID.Text = u.UserID.ToString().Trim();
                txtPhone.Text = u.Telephone;
                txtName.Text = u.Name;
                txtMail.Text = u.Email;
                DataTable dt = BLL.Department.SelectDept("");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlDept.Items.Add(new ListItem(dt.Rows[i]["DeptName"].ToString(), dt.Rows[i]["DeptID"].ToString()));
                }
                ddlDept.SelectedValue = u.DeptID.ToString();
                //页面第一次加载
                lblUserName.Text = u.Name;
                if (u.Type == 1)
                {
                    lblUserType.Text = "普通用户";
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