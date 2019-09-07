<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AttendanceSetting.aspx.cs" Inherits="UIL.AttendanceSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="shortcut icon" href="image/login/logo.ico"/>
    <!--第一步：引入Easy UI所需的样式表文件-->
    <link href="css/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <!--第二步：引入Easy UI所需的图标样式表文件-->
    <link href="css/themes/icon.css" rel="stylesheet" type="text/css" />
    <!--第三步：引入jQuery所需的js文件-->
    <script src="Scripts/easyui/jquery-1.11.3.js" type="text/javascript"></script>
    <!--第四步：引入Easy UI所需的js文件-->
    <script src="Scripts/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <!--第五步：引入Easy UI所需的汉化包js文件-->
    <script src="Scripts/easyui/easyui-lang-zh_CN.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<p>考勤设置</p>
    <table align="center">
        <tr>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server">
                </asp:DropDownList>
                年</td>
            <td>
                <asp:DropDownList ID="ddlMonth" runat="server">
                </asp:DropDownList>
                月</td>
            <td>
                <asp:Button ID="btnShow" runat="server" CssClass="btn" Text="显示" 
                    onclick="btnShow_Click" />
&nbsp;<asp:Button ID="btnSave" runat="server" CssClass="btn" Text="保存设置" 
                    onclick="btnSave_Click" />
            </td>
        </tr>
    </table>

<asp:GridView ID="gvAttendanceSetting" runat="server" CssClass="grid" 
        AutoGenerateColumns="False" 
        onrowdatabound="gvAttendanceSetting_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="日期">
            <ItemTemplate>
                <asp:Label ID="lblDate" runat="server" Text='<%#((DateTime)Eval("Date")).ToShortDateString() %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="星期">
            <ItemTemplate>
                <%#((DateTime)Eval("Date")).ToString("dddd") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="状态">
            <ItemTemplate>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="0" style="color:Black;">默认</asp:ListItem>
                    <asp:ListItem Value="1" style="color:Green;">上班</asp:ListItem>
                    <asp:ListItem Value="2" style="color:Red;">休假</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</asp:Content>

