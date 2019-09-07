using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace UIL
{
    public partial class AttendanceManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GridView1.Attributes.Add("SortExpression", "UserID");
                this.GridView1.Attributes.Add("SortDirection", "ASC");
                this.GetDataBind();
            }
        }

        public void GetDataBind()
        {
            Model.UserInfo u = (Model.UserInfo)Session["user"];
            int DeptID = u.DeptID;
            DataTable dt = BLL.UserInfo.GetUserInfo(" and UserInfo.DeptID='" + DeptID + "'");
            // 获取GridView排序数据列及排序方向
            string sortExpression = this.GridView1.Attributes["SortExpression"];
            string sortDirection = this.GridView1.Attributes["SortDirection"];
            // 调用业务数据获取方法
            DataTable dtBind = dt;
            // 根据GridView排序数据列及排序方向设置显示的默认数据视图
            if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
            {
                dtBind.DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
            }
            // GridView绑定并显示数据       
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
        

        int i = 1;
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

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression.ToString();
            string sortDirection = "ASC";
            if (sortExpression == this.GridView1.Attributes["SortExpression"])
            {
                //获得下一次的排序状态
                sortDirection = (this.GridView1.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
            }
            // 重新设定GridView排序数据列及排序方向
            this.GridView1.Attributes["SortExpression"] = sortExpression;
            this.GridView1.Attributes["SortDirection"] = sortDirection;
            GetDataBind();
        }
    }
}