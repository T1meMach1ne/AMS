using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UIL
{
    public partial class PersonalInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Model.UserInfo u = (Model.UserInfo)Session["User"];
            if (!string.IsNullOrEmpty(u.UserID))
            {
                string where = "";
                where = " and UserID='" + u.UserID + "'";
                txtCellPhone.Text = BLL.UserInfo.GetUserInfo(where).Rows[0]["Cellphone"].ToString();
            }
        }     
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string where = "";
            Model.UserInfo u = (Model.UserInfo)Session["User"];
            Model.UserInfo model = new Model.UserInfo();
            model.UserID = u.UserID;
            model.Password = txtPwd.Text;
            model.Cellphone = txtCellPhone.Text;
            if (string.IsNullOrEmpty(txtPwd.Text) && string.IsNullOrEmpty(txtPwdTwo.Text))
            {
                where = " and UserID='" + u.UserID + "'";
                model.Password = BLL.UserInfo.GetUserInfo(where).Rows[0]["Password"].ToString();
                if (BLL.UserInfo.UpdateUserInfo1(model))
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功！');window.parent.location='/Login.aspx'", true);
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改失败！');", true);
                }
            }
            else if (string.IsNullOrEmpty(txtPwd.Text) && !string.IsNullOrEmpty(txtPwdTwo.Text))
            {
                lblPwd.Text = "请填写新密码！—_—";
            }
            else if (!string.IsNullOrEmpty(txtPwd.Text) && string.IsNullOrEmpty(txtPwdTwo.Text))
            {
                lblPwdTwo.Text = "请确认密码！—_—";
            }
            else if (!string.IsNullOrEmpty(txtPwd.Text) && !string.IsNullOrEmpty(txtPwdTwo.Text))
            {
                if (txtPwd.Text == txtPwdTwo.Text)
                {
                    if (BLL.UserInfo.UpdateUserInfo1(model))
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功！');window.parent.location='/Login.aspx';", true);
                    }
                    else
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改失败！');", true);
                    }
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('两次输入密码不一致！');", true);
                }
            }          
        }
    }
}