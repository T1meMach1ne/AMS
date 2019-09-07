<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceShow.aspx.cs"
    Inherits="UIL.AttendanceShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <!--第一步：引入Easy UI所需的样式表文件-->
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" /> 
    <script src="Scripts/easyui/jquery-1.11.3.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div algin="center">
        <table align="center">
            <tr>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server">
                    </asp:DropDownList>
                    年
                </td>
                <td>
                    <asp:DropDownList ID="ddlMonth" runat="server">
                    </asp:DropDownList>
                    月
                </td>
                <td>
                    <asp:Button ID="btnSee" runat="server" CssClass="btn" Text="查看" OnClick="btnSee_Click" />
                </td>
            </tr>
        </table>
         </div>
         <div align="center">
        <asp:GridView ID="gvAttendanceInfo" runat="server" AutoGenerateColumns="False" CssClass="grid"
            OnRowDataBound="gvAttendanceInfo_RowDataBound" >
            <Columns>
                <asp:TemplateField HeaderText="日期">
                    <ItemTemplate>
                        <%#((DateTime)Eval("Date")).ToShortDateString()%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="星期">
                    <ItemTemplate>
                        <%#((DateTime)Eval("Date")).ToString("ddd") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="首次打卡时间">
                    <ItemTemplate>
                        <asp:Label ID="lblFirstTime" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="最后打卡时间">
                    <ItemTemplate>
                        <asp:Label ID="lblLastTime" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="考勤状态">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
    </form>
</body>
</html>
