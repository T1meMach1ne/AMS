using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class UserEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlUserType.Items.Add(new ListItem("请选择", ""));
                ddlUserType.Items.Add(new ListItem("员工", "0"));
                ddlUserType.Items.Add(new ListItem("主管", "1"));
                DataTable dt = BLL.Department.GetDepartmentInfo("");
                ddlDeptName.Items.Add(new ListItem("请选择", ""));
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ddlDeptName.Items.Add(new ListItem(dt.Rows[i]["DeptName"].ToString(), dt.Rows[i]["DeptID"].ToString()));
                    }
                }
               
            }
            if (Request["UserID"] != null)
            {
                string where = "";
                where += " and UserID='" + Request["UserID"].ToString() + "'";
                DataTable dt1 = BLL.UserInfo.GetUserInfo(where);
                if (dt1.Rows.Count > 0)
                {
                    txtUserID.Text = dt1.Rows[0]["UserID"].ToString();
                    txtUserID.ReadOnly = true;
                    txtUserName.Text = dt1.Rows[0]["UserName"].ToString();
                    ddlDeptName.SelectedValue = dt1.Rows[0]["DeptID"].ToString();
                    ddlUserType.SelectedValue = dt1.Rows[0]["UserType"].ToString();
                    txtCellphone.Text = dt1.Rows[0]["Cellphone"].ToString();
                }
            }
        }
        public bool VaildateNull()
        {
            bool nu = true;
            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                lblUserID.Text = "用户ID不能为空！—_—";
                nu= false;
            }    
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                lblUserName.Text = "用户姓名不能为空！—_—";
                nu = false;
            }
            if (string.IsNullOrEmpty(ddlUserType.SelectedValue))
            {
                lblUserType.Text = "请选择用户类型！—_—";
                nu = false;
            }
            if (string.IsNullOrEmpty(ddlDeptName.SelectedValue))
            {
                lblDeptName.Text = "请选择所属部门！—_—";
            }
            return nu;
        }
        protected void btnInsert_Click1(object sender, EventArgs e)
        {
            if (Request["UserID"] == null)
            {
                if (VaildateNull())
                {
                    DataTable dt = BLL.UserInfo.GetUserInfo(" and UserID='" + txtUserID.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        lblUserID.Text = "该用户ID已经存在！—_—";
                        lblUserName.Text = "";
                        lblUserType.Text = "";
                        lblDeptName.Text = "";
                    }
                    else
                    {
                        Model.UserInfo u = new Model.UserInfo();
                        u.UserID = txtUserID.Text;
                        u.UserName = txtUserName.Text;
                        u.UserType = byte.Parse(ddlUserType.SelectedValue);                   
                        u.DeptID = int.Parse(ddlDeptName.SelectedValue);
                        u.Password = "1";
                        u.Cellphone = txtCellphone.Text;
                        if (BLL.UserInfo.AddUserInfo(u))
                        {
                            //表示添加成功
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功！');window.parent.location='/UserList.aspx';", true);
                        }
                        else
                        {
                            //表示添加失败
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加失败！');", true);
                        }
                    }
                }
            }
            else
            {
                if (VaildateNull())
                {

                    Model.UserInfo u = new Model.UserInfo();
                    u.UserID = txtUserID.Text;
                    u.UserName = txtUserName.Text;
                    u.UserType = byte.Parse(ddlUserType.SelectedValue);
                    u.DeptID = int.Parse(ddlDeptName.SelectedValue);
                    u.Cellphone = txtCellphone.Text;
                    if (BLL.UserInfo.UpdateUserInfo(u))
                    {
                        //表示修改成功
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功！');window.parent.location='/UserList.aspx';", true);
                    }
                    else
                    {
                        //表示修改失败
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改失败！');", true);
                    }

                }
            }
        }       
    }
}