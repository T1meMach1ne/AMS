using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class DepartmentEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定主管下拉框
                DataTable dt1 = BLL.Department.GetAllManager();
                if (dt1.Rows.Count > 0)
                {
                    ddlManager.Items.Add(new ListItem("请选择", ""));
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        ddlManager.Items.Add(new ListItem(dt1.Rows[i]["UserName"].ToString(), dt1.Rows[i]["UserID"].ToString()));
                    }
                }
            }
            if (Request["DeptID"] != null)
            {
                string where = "";
                where += " and Department.DeptID=" + Request["DeptID"].ToString() + "";
                DataTable dt = BLL.Department.GetDepartmentInfo(where);
                txtDepartmentName.Text = dt.Rows[0]["DeptName"].ToString();
                txtDepartmentInfo.Text = dt.Rows[0]["DeptInfo"].ToString();
                ddlManager.SelectedValue = dt.Rows[0]["UserID"].ToString();

            }
        }
        /// <summary>
        /// 非空验证
        /// </summary>
        /// <returns></returns>
        public bool VaildateNull()
        {
            bool nu = true;
            if (string.IsNullOrEmpty(txtDepartmentName.Text))
            {
                lblDeptName.Text = "请填写部门名称！—_—";
                nu = false;
            }
           
            if (string.IsNullOrEmpty(ddlManager.SelectedValue))
            {
                lblManager.Text = "请选择主管！—_—";
                nu = false;
            }
            return nu;
        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (Request["DeptID"] == null)
            {
                if (VaildateNull())
                {
                    DataTable dt = BLL.Department.GetDepartmentInfo(" and DeptName='" + txtDepartmentName.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        lblDeptName.Text = "部门名称已存在！—_—";
                        lblManager.Text = "";
                    }
                    else
                    {
                        Model.Department d = new Model.Department();
                        d.DeptName = txtDepartmentName.Text;
                        d.ManagerID = ddlManager.SelectedValue;
                        d.DeptInfo = txtDepartmentInfo.Text;
                        if (BLL.Department.AddDepartmentInfo(d))
                        {
                            //表示添加成功
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功！');window.parent.location='/DepartmentList.aspx';", true);
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
                    Model.Department d = new Model.Department();
                    d.DeptID = int.Parse(Request["DeptID"].ToString());
                    d.DeptName = txtDepartmentName.Text;
                    d.ManagerID = ddlManager.SelectedValue;
                    d.DeptInfo = txtDepartmentInfo.Text;
                    if (BLL.Department.UpdateDepartmentInfo(d))
                    {
                        //表示修改成功
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功！');window.parent.location='/DepartmentList.aspx';", true);
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