<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AttendanceManage.aspx.cs" Inherits="UIL.AttendanceManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="shortcut icon" href="image/login/logo.ico"/>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
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
    <script type="text/javascript">
        $(function () {
            $("#btnImport").click(function () {
                $("#dlg2").dialog({ width: 550, height: 100 }).dialog("setTitle", "考勤导入").dialog("open");
                $("#frm2").attr("src", "ImportExcel.aspx");
            });
        });
        function LookMyAttendance(ID) {
            $("#dlg2").dialog("setTitle", "我的考勤").dialog("open");
            $("#frm2").attr("src", "AttendanceShow.aspx?ID=" + ID);
        }
    </script>
    <style type="text/css">
        .div1
        {
             margin-left:150px; 	
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<p>考勤管理</p>
    <div class="div1">
           <input id="btnImport" class="btn" type="button" value="导入考勤数据" /> 
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CssClass="grid" OnRowDataBound="GvUserInfo_RowDataBound" AllowSorting="True" 
        onsorting="GridView1_Sorting">
        <Columns>
            <asp:TemplateField HeaderText="序号" InsertVisible="False">
                <ItemTemplate>
                    <asp:Label ID="lblNO" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="用户ID" DataField="UserID" SortExpression="UserID" />
            <asp:BoundField HeaderText="用户名" DataField="UserName" SortExpression="UserName" />
            <asp:BoundField HeaderText="部门"   DataField="DeptName" SortExpression="DeptName" />
            <asp:TemplateField HeaderText="查看考勤">
                <ItemTemplate>
                    <a id="a1" runat="server" onclick='<%# "LookMyAttendance("+Eval("UserID")+")"%>'>查看</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div id="dlg2" class="easyui-dialog" title="My Dialog" style="width: 900px; height: 600px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <iframe id="frm2" width='99%' height='99%' frameborder='0'></iframe>
    </div>
</asp:Content>
