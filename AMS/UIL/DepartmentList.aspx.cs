using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class DepartmentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {          
                //绑定主管下拉框
                DataTable dt = BLL.Department.GetAllManager();
                ddl_Manager.Items.Add(new ListItem("请选择", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddl_Manager.Items.Add(new ListItem(dt.Rows[i]["UserName"].ToString(), dt.Rows[i]["UserID"].ToString()));
                    }
                }
                GetDataBind();      //调用绑定数据方法
            }
        }
        public void GetDataBind()
        {
            string where = "";
            if (txt_DepartmentName.Text != "")
            {
                where += " and DeptName like '%" + txt_DepartmentName.Text + "%'";
            }
            
            if (int.Parse(ddl_Manager.SelectedValue) != 0)
            {
                where += " and UserInfo.UserID='" + Convert.ToInt32(ddl_Manager.SelectedValue) + "'";
            }

            GvDepartmentInfo.DataSource = BLL.Department.GetDepartmentInfo(where);
            GvDepartmentInfo.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDepartmentInfo.PageIndex = e.NewPageIndex;
            GetDataBind();
        }
        int i = 1;
        protected void GvDepartmentInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //先把控件lblNO找到
                Label lblNO = (Label)e.Row.FindControl("lblNO");
                lblNO.Text = i.ToString();
                i++;
                //实现部门删除按钮的显示和影藏
                //先找到删除控件
                Image imgDelete = (Image)e.Row.FindControl("imgDelete");
                DataRowView drv = (DataRowView)e.Row.DataItem;
                if (BLL.Department.HaveUserInfo(Convert.ToInt32(drv["DeptID"])))
                {
                    //部门存在用户
                    imgDelete.Visible = false;
                }
                else
                {
                    //部门无用户
                    imgDelete.Visible = true;
                }


            }
        }

        protected void btn_select_Click(object sender, EventArgs e)
        {
            GetDataBind();
        }

        protected void GvDepartmentInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUserInfo")
            {
                Model.Department u = new Model.Department();
                u.DeptID =int.Parse( e.CommandArgument.ToString());
                if (BLL.Department.DelDepartmentInfo(u))
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除成功！');window.parent.location='/DepartmentList.aspx';", true);
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除失败！');", true);
                }
            }
        }
    }
}