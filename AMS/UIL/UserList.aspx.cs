using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class UserList : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 

                string where="";
                DataTable dt = BLL.Department.GetDepartmentInfo(where);
                ddlDepartment.Items.Add(new ListItem("请选择", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlDepartment.Items.Add(new ListItem(dt.Rows[i]["DeptName"].ToString(), dt.Rows[i]["DeptID"].ToString()));
                }
                this.GvUserInfo.Attributes.Add("SortExpression", "UserID");
                this.GvUserInfo.Attributes.Add("SortDirection", "ASC");
                GetDataBind();
            }
        }
        /// <summary>
        /// 分页属性
        /// </summary>
        #region
        private int _pageSize = 1000;
        /// <summary>
        /// 每页显示的数据量
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                GetPageCount();
            }
        }

        private int _nMax = 0;
        /// <summary>
        /// 总行数
        /// </summary>
        public int NMax
        {
            get { return _nMax; }
            set
            {
                _nMax = value;
                GetPageCount();
            }
        }

        private int _pageCurrent;
        /// <summary>
        /// 当前页号
        /// </summary>
        public int PageCurrent
        {
            get { return _pageCurrent; }
            set { _pageCurrent = value; }
        }

        private int _pageCount;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        /// <summary>
        /// 计算总页数
        /// </summary>
        public void GetPageCount()
        {
            if (NMax > 0)
            {
                PageCount = int.Parse(Math.Ceiling(double.Parse(NMax.ToString()) / double.Parse(PageSize.ToString())).ToString());
            }
            else
            {
                PageCount = 0;
            }
        }
        #endregion

        public void GetDataBind()
        {
            string where = "";
            if (txtID.Text != "")
            {
                where += " and UserID like '%" + txtID.Text + "%'";
            }
            if (txtName.Text != "")
            {
                where += " and UserName like '%" + txtName.Text + "%'";
            }
            if (int.Parse(ddlDepartment.SelectedValue) != 0)
            {
                where += " and UserInfo.DeptID='" + Convert.ToInt32(ddlDepartment.SelectedValue) + "'";
            }
            DataTable dt = BLL.UserInfo.GetUserInfo(where);
            // 获取GridView排序数据列及排序方向
            string sortExpression = this.GvUserInfo.Attributes["SortExpression"];
            string sortDirection = this.GvUserInfo.Attributes["SortDirection"];
            // 调用业务数据获取方法
            DataTable dtBind = dt;
            // 根据GridView排序数据列及排序方向设置显示的默认数据视图
            if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
            {
                dtBind.DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
            }
            // GridView绑定并显示数据       
            this.GvUserInfo.DataSource = dtBind;
            this.GvUserInfo.DataBind();
            
        }

        protected void btn_select_Click(object sender, EventArgs e)
        {
            GetDataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserEdit.aspx");
        }
        int i=1;
        protected void GvUserInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //先把控件lblNO找到
                Label lblNO = (Label)e.Row.FindControl("lblNO");
                lblNO.Text = i.ToString();
                i++;
            }
        }
      
        protected void GvUserInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUserInfo")
            { 
                Model.UserInfo u=new Model.UserInfo();
                u.UserID = e.CommandArgument.ToString();
                if (BLL.UserInfo.DelUserInfo(u))
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除成功！');window.parent.location='/UserList.aspx';", true);                
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(),"","alert('删除失败！');",true);
                }
            }
        }
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string str = "";
            Model.UserInfo u = new Model.UserInfo();
            for (int i = 0; i < GvUserInfo.Rows.Count; i++)
            {
                CheckBox Cb = (CheckBox)GvUserInfo.Rows[i].FindControl("ck1");
                if (Cb.Checked)
                {

                    str += "'" + GvUserInfo.Rows[i].Cells[2].Text + "'" + ",";
                }
            }
            u.UserID = str.TrimEnd(',');
            if (BLL.UserInfo.DelUserInfo1(u))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除成功！');window.parent.location='/UserList.aspx';", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除失败！');", true);
            }
        }

        protected void GvUserInfo_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression.ToString();
            string sortDirection = "ASC";
            if (sortExpression == this.GvUserInfo.Attributes["SortExpression"])
            {
                //获得下一次的排序状态
                sortDirection = (this.GvUserInfo.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
            }
            // 重新设定GridView排序数据列及排序方向
            this.GvUserInfo.Attributes["SortExpression"] = sortExpression;
            this.GvUserInfo.Attributes["SortDirection"] = sortDirection;
            GetDataBind();
        }

        protected void GvUserInfo_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            GvUserInfo.PageIndex = e.NewPageIndex;
            GetDataBind();
        }
    }
}