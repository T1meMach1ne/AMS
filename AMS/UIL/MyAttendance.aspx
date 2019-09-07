<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyAttendance.aspx.cs" Inherits="UIL.MyAttendance" %>
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
    <style type="text/css">
       .table1
        { 
             text-align:center;
        }
        .tr1
        {
             height:30px;
             width:100px;    
             font-weight:bold;
             font-size:18px;
        }
       
        .td2
        {
              height:100px;	
              width:100px;
         }
         .div1
         {
             height:100px;
             width:30px;
             float:left;   
                    
         }
        .div2
        {
            height:100px;
            width:70px;
            text-align:center;
            font-size:15px;
            font-weight:bold;       
        }
        .lbl1
        {
             font-size:20px;
             font-weight:bold;
             padding-top:30px;
             line-height:100px;
        }
        .AM
        {
             height:50px;
             line-height:50px;  
               
        }
        .PM
        {
             height:50px;   
             line-height:50px; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<p>我的考勤</p>
    <div>
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
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem Text="列表显示" Value="1"></asp:ListItem>
                        <asp:ListItem Text="日历显示" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSee" runat="server" CssClass="btn" Text="查看" OnClick="btnSee_Click" />
                </td>
            </tr>
        </table>   
        </div>     
        <asp:GridView ID="gvAttendanceInfo" runat="server" 
        AutoGenerateColumns="False" CssClass="grid" 
        onrowdatabound="gvAttendanceInfo_RowDataBound1">
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
        <table class="table1" align="center" border="1">
            <tr class="tr1">
                <td class="td1">一</td>
                <td class="td1">二</td>
                <td class="td1">三</td>
                <td class="td1">四</td>
                <td class="td1">五</td>
                <td class="td1">六</td>
                <td class="td1">日</td>
            </tr>
            <tr>
                <td class="td2"><div class="div1"><label class="lbl1" id="lblNum">1</label></div><div class="div2"><div class="AM"><label id="lblAM">请假</label></div><div class="PM"><label id="lblPM">迟到</label></div></div></td>
                <td class="td2"><div class="div1"><label class="lbl1" id="Label1">1</label></div><div class="div2"><div class="AM"><label id="lblAM1">请假</label></div><div class="PM"><label id="Label2">迟到</label></div></div></td>
            </tr>
        <%--    <%=TableDataBind()%>--%>
        </table>
    <div id="dlg" class="easyui-dialog" title="My Dialog" style="width:450px;height:300px;"   
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        </div>
        <iframe id="frm1" width='99%' height='99%' frameborder='0'></iframe>
</asp:Content>
