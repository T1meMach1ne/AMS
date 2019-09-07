<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewGetRecordByPage.ascx.cs" Inherits="UIL.Control.GridViewGetRecordByPage" %>
<style type="text/css">
    .tb1
    {
      width:100%;
      border:1px solid #000;
      border-collapse:collapse;   
     }
    .tb1 th
     {
         height:35px;
         width:15%;
         border:1px solid #000;
         background:#8F8F8F;
      }
       .tb1 td
     {
         height:30px;
         border:1px solid #000;
         text-align:center;
      }
      table
      {
          width:80%;
          height:70%;  
      }
</style>
<script src="../Scripts/jquery-1.4.1-vsdoc.js"></script>
<script src="../Scripts/jquery-1.4.1.js"></script>
<script>
    function CheckAll() {
        $("#ContentPlaceHolder2_DataGridView1_GridView1 tr").each(function () {
            $(this).find("td").eq(0).find("input[type=checkbox]").attr("checked", $("#checkAll").attr("checked"));
            ToggerColor();
        })
        function ToggerColor() {
            $("#ContentPlaceHolder2_DataGridView1_GridView1 tr").each(function () {
                if ($(this).find("td").eq(0).find("input[type=checkbox]").attr("checked")) {
                    $(this).addClass("back");
                } else {
                    $(this).removeClass("back");
                }

            });
        }
    }

</script>
<table>
    <tr>
        
        <td>
            <asp:GridView ID="GridView1"  CssClass="tb1" runat="server" 
                AutoGenerateColumns="false">
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td style="text-align:center">
           <asp:ImageButton ID="btn_Begin" runat="server" ToolTip="首页" 
                ImageUrl="~/image/DataPager/begin.gif" onclick="btn_Begin_Click" 
                style="width: 18px" />
           <asp:ImageButton ID="btn_Last" runat="server" ToolTip="上一页" 
                ImageUrl="~/image/DataPager/last.gif" onclick="btn_Last_Click" 
                style="width: 18px" />
           <asp:DropDownList ID="dropPageIndex" runat="server" AutoPostBack="true" 
                onselectedindexchanged="dropPageIndex_SelectedIndexChanged"></asp:DropDownList>
           <asp:ImageButton ID="btn_Next" runat="server" ToolTip="下一页" 
                ImageUrl="~/image/DataPager/next.gif" onclick="btn_Next_Click"/>
           <asp:ImageButton ID="btn_End" runat="server" ToolTip="尾页" 
                ImageUrl="~/image/DataPager/end.gif" onclick="btn_End_Click" />
           <asp:Label ID="Label1" runat="server" Text="总记录数："></asp:Label>
            <asp:Label ID="labPageCount" runat="server"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="总页数："></asp:Label>
            <asp:Label ID="labPageIndex" runat="server" Text="1"></asp:Label>
            
        </td>
    </tr>
</table>