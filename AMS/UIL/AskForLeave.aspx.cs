using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL
{
    public partial class AskForLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                this.GvAskForLeave.Attributes.Add("SortExpression", "UserName");
                this.GvAskForLeave.Attributes.Add("SortDirection", "ASC");
                GetDataBind();
            }
        }
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

            DataTable dt = BLL.Approve.GetApproveInfo(where);
            // 获取GridView排序数据列及排序方向
            string sortExpression = this.GvAskForLeave.Attributes["SortExpression"];
            string sortDirection = this.GvAskForLeave.Attributes["SortDirection"];
            // 调用业务数据获取方法
            DataTable dtBind = dt;
            // 根据GridView排序数据列及排序方向设置显示的默认数据视图
            if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
            {
                dtBind.DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
            }
            // GridView绑定并显示数据       
            this.GvAskForLeave.DataSource = dtBind;
            this.GvAskForLeave.DataBind();
        }

        protected void btn_select_Click(object sender, EventArgs e)
        {
            GetDataBind();      
        }

        protected void GvAskForLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //实现删除编辑按钮的显示和影藏
                //先找到删除控件
                Image imgDelete = (Image)e.Row.FindControl("imgDelete");
                Image imgUpdate = (Image)e.Row.FindControl("Image1");
                ;
                DataRowView drv = (DataRowView)e.Row.DataItem;
                if (BLL.Approve.SelectApproveState(Convert.ToInt32(drv["ApproveID"])))
                {
                    //不显示
                    imgDelete.Visible = true;
                    imgUpdate.Visible = true;
                    e.Row.FindControl("aShow").Visible = false;
              
                }
                else
                {
                    //删除编辑按钮的显示
                    imgDelete.Visible = false;
                    imgUpdate.Visible = false;
                    e.Row.FindControl("aShow").Visible = true;
                }
            }
        }

        protected void GvAskForLeave_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteApproveInfo")
            {
                Model.Approve a = new Model.Approve();
                a.ApproveID =int.Parse( e.CommandArgument.ToString());
                if (BLL.Approve.DelApproveInfo(a))
                {
                    GetDataBind();
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除成功！');", true);

                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除失败！');", true);
                }
            }
        }

        protected void GvAskForLeave_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression.ToString();
            string sortDirection = "ASC";
            if (sortExpression == this.GvAskForLeave.Attributes["SortExpression"])
            {
                //获得下一次的排序状态
                sortDirection = (this.GvAskForLeave.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
            }
            // 重新设定GridView排序数据列及排序方向
            this.GvAskForLeave.Attributes["SortExpression"] = sortExpression;
            this.GvAskForLeave.Attributes["SortDirection"] = sortDirection;
            GetDataBind();
        }

        protected void GvAskForLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvAskForLeave.PageIndex = e.NewPageIndex;
            GetDataBind();
        }
    }
}