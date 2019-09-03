<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AssignerSetting.aspx.cs" Inherits="Apply.AssignerSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        #table1 tr
        {
            height: 30px;
        }
        #table1 tr td
        {
            width: 80px;
        }
    </style>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
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
    <script src="Scripts/ToolKit.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //绑定数据
            BindApplyFlow();
            //绑定分配人下拉框
            $.ajax({
                type: "get",
                url: "/Handlers/ApplyFlowHandler.ashx",
                dataType: "json",
                data: {
                    Method: "BindAssigner"
                },
                success: function (data) {
                    var s = "<option value='0'>--请选择--</option>";
                    $.each(data, function (index, o) {
                        s += "<option value='" + o.UserID + "'>" + o.Name + "</option>";
                    });
                    $("#selAssigner").empty();
                    $("#selAssigner").append(s);
                }
            });
            //关闭窗口
            $("#btnEsc2").click(function () {
                $("#dlg1").dialog("close");
            });
           
        });

        function BindApplyFlow() {
            $.ajax({
                type: "get", //键值对  用来指定HTTP请求的方式
                url: "/Handlers/ApplyFlowHandler.ashx", //专门用来设置请求对应的路径 设置一般处理程序所对应的路径
                data: {                     //参数data的功能：就是用来指定浏览器向服务器传递的数据
                    Method: "BindApplyType"
                },
                dataType: "json",
                success: function (data) {//参数data专门用来接收服务器返回给浏览器的数据
                    var s = "";
                    $.each(data, function (index, o) {
                        s += "<tr>";
                        s += "<td>" + o.ApplyTypeID + "</td>";
                        s += "<td>" + o.ApplyTypeName + "</td>";
                        s += "<td>" + o.Assigner + "</td>";
                        s += "<td>";
                        s += "<img title='编辑' onclick='EditApplyFlow(" + o.ApplyTypeID + ")' src='image/common/edit.png' alt=''/>";
                        s += "</td>";
                        s += "</tr>";
                    });
                    $("#tableApplyFlow tr:gt(0)").remove(); //在页面上查找id="tableUserInfo"的元素，并且把下标大于0的tr都删掉
                    $("#tableApplyFlow").append(s); //把变量s追加到id="tableUserInfo"的里面去
                }
            });
        }
        function EditApplyFlow(ApplyTypeID) {
            $("#dlg1").dialog({ width: 400, height: 200 }).dialog("setTitle", "分配人设置").dialog("open");
            $("#txtApplyName").attr("readonly", true);
            
            //赋值
            $.ajax({
                type: "post",
                url: "/Handlers/ApplyFlowHandler.ashx",
                data: {
                    method: "GetSingleApplyFlow",
                    ID: ApplyTypeID
                },
                dataType: "json",
                success: function (data) {
                    $("#txtApplyName").val(data.ApplyTypeName),
                                 $("#selAssigner").val(data.Assigner)          
                }
            });
            //修改方法
            $("#btnSubmit2").click(function () {
                $.ajax({
                    type: "post",
                    url: "/Handlers/ApplyFlowHandler.ashx",
                    data: {
                        Method: "UpdateApplyFlow",
                        ID: ApplyTypeID,
                        Assigner: $("#selAssigner").val()
                    },
                    success: function (data) {
                        if (data == "1") {
                            //表示添加成功
                            //关闭窗口
                            $("#dlg1").dialog("close");
                            //即时刷新列表上面的数据
                            BindApplyFlow();
                            $.messager.alert('温馨提示', '恭喜您，修改成功！', 'info');
                        }
                        else {
                            //表示添加失败
                            $.messager.alert('温馨提示', '对不起，修改失败！', 'info');
                        }
                    }
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tableApplyFlow" class="grid" border="1">
        <tr>
            <th>
                序号
            </th>
            <th>
                申请单类型
            </th>
            <th>
                分配人
            </th>
            <th>
                管理
            </th>
        </tr>
    </table>
    <div id="dlg1" class="easyui-dialog" title="My Dialog" style="width: 400px; height: 300px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <table align="center" id="table1">
            <tr>
                <td>
                    <span style="color: Red">*</span>申请单名称:
                </td>
                <td>
                    <input type="text" id="txtApplyName" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    分 配 人：
                </td>
                <td>
                    <select id="selAssigner" name="sel1">
                    </select>
                </td>
            </tr>
            <tr align="center" style="height: 50px">
                <td colspan="2">
                    <input id="btnSubmit2" type="button" value="确  定" class="btn" style="width: 50px" />&nbsp;&nbsp;
                    <input id="btnEsc2" type="button" value="取  消" class="btn" style="width: 50px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
