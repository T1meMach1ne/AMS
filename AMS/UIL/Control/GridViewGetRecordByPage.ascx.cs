using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace UIL.Control
{
    public partial class GridViewGetRecordByPage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }

        #region 自定义属性

        /// <summary>
        /// 每页显示多少条数据
        /// </summary>
        public int PageCount
        {
            get;
            set;
        }
        /// <summary>
        /// 每页显示多少条数据
        /// </summary>
        public int PageCounts;

        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortID
        {
            get;
            set;
        }

        /// <summary>
        /// 要显示的列，用“|”分隔列名和要显示的列名，用“，”分隔列名        /// </summary>
        public string Columns
        {
            get;
            set;
        }
        /// <summary>
        /// 要查询的sql语句
        /// </summary>
        public string SqlString
        {
            get;
            set;
        }
        #endregion
 

        
    }
}