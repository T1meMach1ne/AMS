using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class AskForLeaveApprovalEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GvAskForLeaveApproval.Attributes.Add("SortExpression", "UserID");
                this.GvAskForLeaveApproval.Attributes.Add("SortDirection", "ASC");
                GetDataBind();
            }
        }

        protected void btn_select_Click(object sender, EventArgs e)
        {
            GetDataBind();
        }
        /// <summary>
        /// 绑定GridView数据
        /// </summary>
        public void GetDataBind()
        {
            string where = "";
            if (txtTitle.Text != "")
            {
                where += " and Title like '%" + txtTitle.Text + "%'";
            }
            if (txtBeginTime.Text != "")
            {
                where += " and BeginDate>='" + txtBeginTime.Text + "'";
            }
            if (txtEndTime.Text != "")
            {
                where += " and EndDate<='" + txtEndTime.Text + "'";
            }
            if (rblStatus.SelectedValue != "")
            {
                if (rblStatus.SelectedValue == "2")
                {
                    where += " and Status in (0,1)";
                }
                else
                {
                    where += " and Status='" + rblStatus.SelectedValue + "'";
                }
            }
            string sortExpression = this.GvAskForLeaveApproval.Attributes["SortExpression"];
            string sortDirection = this.GvAskForLeaveApproval.Attributes["SortDirection"];
            // 调用业务数据获取方法
            DataTable dtBind = BLL.Approve.GetApproveInfo(where); ;
            // 根据GridView排序数据列及排序方向设置显示的默认数据视图
            if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
            {
                dtBind.DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
            }
            // GridView绑定并显示数据       
            this.GvAskForLeaveApproval.DataSource = dtBind;
            this.GvAskForLeaveApproval.DataBind();
        }

        protected void GvAskForLeaveApproval_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression.ToString();
            string sortDirection = "ASC";
            if (sortExpression == this.GvAskForLeaveApproval.Attributes["SortExpression"])
            {
                //获得下一次的排序状态
                sortDirection = (this.GvAskForLeaveApproval.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
            }
            // 重新设定GridView排序数据列及排序方向
            this.GvAskForLeaveApproval.Attributes["SortExpression"] = sortExpression;
            this.GvAskForLeaveApproval.Attributes["SortDirection"] = sortDirection;
            GetDataBind();
        }

        protected void GvAskForLeaveApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvAskForLeaveApproval.PageIndex = e.NewPageIndex;
            GetDataBind();
        }
    }
}