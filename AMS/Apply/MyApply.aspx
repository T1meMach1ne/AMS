﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MyApply.aspx.cs" Inherits="Apply.MyApply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>我的申请单</title>
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
            BindApply();    //加载的时候绑定列表
            //绑定下拉框
            $.ajax({
                type: "get",
                url: "/Handlers/ApplyFlowHandler.ashx",
                data: {
                    Method: "BindApplyType"
                },
                dataType: "json",
                success: function (data) {
                    var s = "";
                    $.each(data, function (index, o) {
                        s += "<option value='" + o.ApplyTypeID + "'>" + o.ApplyTypeName + "</option>";
                    });
                    $("#ddlApplyType,#ddlApplyType2").empty();
                    $("#ddlApplyType,#ddlApplyType2").append(s);
                }
            });
            $("#btn_select").click(function () {
                BindApply();
            });
            //点击弹出选择申请单类型的窗口
            $("#btnApplyType").click(function () {
                $("#dlg").dialog({ width: 400, height: 200 }).dialog("setTitle", "选择申请单类型").dialog("open");
            });
            $("#btnInsertApply").click(function () {
                var type = $("#ddlApplyType2").val();
                if (type == 0) {
                    $.messager.alert('温馨提示', '请选择申请单类型！', 'error');
                }
                if (type == 1) {
                    $("#dlg1").dialog({ width: 600, height: 450 }).dialog("setTitle", "IP地址申请单").dialog("open");
                    $("#txtApplyTitle1").val("IP地址申请")
                    $("#IPAddress").show();
                    $("#OfficeEmail").hide();
                    $("#StorageSpace").hide();
                }
                if (type == 2) {
                    $("#dlg1").dialog({ width: 600, height: 450 }).dialog("setTitle", "办公邮箱申请单").dialog("open");
                    $("#txtApplyTitle1").val("办公邮箱申请")
                    $("#OfficeEmail").show();
                    $("#IPAddress").hide();
                    $("#StorageSpace").hide();
                }
                if (type == 3) {
                    $("#dlg1").dialog({ width: 600, height: 450 }).dialog("setTitle", "存储空间申请单").dialog("open");
                    $("#txtApplyTitle1").val("存储空间申请")
                    $("#StorageSpace").show();
                    $("#OfficeEmail").hide();
                    $("#IPAddress").hide();
                }
            });
            $("#btnEsc").click(function () {
                $("#dlg").dialog("close");
            });
            $("#btnEsc1").click(function () {
                $("#dlg1").dialog("close");
            });
            //添加的方法
            $("#btnSubmit1").click(function () {
                $.ajax({
                    type: "post",
                    url: "/Handlers/ApplyHandler.ashx",
                    data: {
                        Method: "InsertApply",
                        ApplyTypeID: $("#ddlApplyType2").val(),
                        UserID: $("#ContentPlaceHolder2_lblUserID1").text(),
                        ApplyName: $("#ContentPlaceHolder2_lblApplyName1").text(),
                        ApplyDept: $("#ContentPlaceHolder2_lblApplyDept1").text(),
                        Telphone: $("#ContentPlaceHolder2_txtTelphone1").val(),
                        ApplyTitle: $("#txtApplyTitle1").val(),
                        ApplyReason: $("#txtApplyReason1").val(),
                        Quantity1: $("#txtQuantity1").val(),
                        Address1: $("#txtAddress1").val(),
                        PortNumber1: $("#txtPortNumber1").val(),
                        TimeLimit1: $("#txtTimeLimit1").val(),
                        Zone2: $("#txtZone2").val(),
                        OfficePlace2: $("#txtOfficePlace2").val(),
                        UserName2: $("#txtUserName2").val(),
                        FullName2: $("#txtFullName2").val(),
                        Zone3: $("#txtUseZone3").val(),
                        InterfaceMan3: $("#txtInterfaceMan3").val(),
                        Right3: $("#txtRight3").val(),
                        TimeLimit3: $("#txtTimeLimit3").val(),
                        Remark: $("#txtRemark1").val(),
                        file: $("#file1").val()
                    },
                    success: function (data) {
                        if (data == "0") {
                            //表示添加失败
                            $.messager.alert('温馨提示', '申请单创建失败，请重试！', 'info');
                        }
                        else {
                            $.messager.alert('温馨提示', "单号为" + data + "的申请单创建成功！", 'info');
                            $("#dlg1").dialog("close");  //表示添加成功   //关闭窗口 
                            $("#dlg").dialog("close");
                            BindApply();   //即时刷新列表上面的数据
                        }
                    }
                });
            });
        });
        //绑定数据的方法
        function BindApply() {
            //通过AJAX实现无刷新效果
            $.ajax({
                type: "get", //键值对  用来指定HTTP请求的方式
                url: "/Handlers/ApplyHandler.ashx", //专门用来设置请求对应的路径 设置一般处理程序所对应的路径
                data: {                     //参数data的功能：就是用来指定浏览器向服务器传递的数据
                    Method: "GetPagedApply",
                    ApplyID: $("#ContentPlaceHolder2_txtApplyID").val(),
                    ApplyType: $("#ddlApplyType").val(),
                    ApplyTitle: $("#ContentPlaceHolder2_txtApplyTitle").val(),
                    ApplyStatus: $("#ContentPlaceHolder2_ddlApplyStatus").val(),
                    r: Math.random()
                },
                dataType: "json",
                success: function (data) {//参数data专门用来接收服务器返回给浏览器的数据
                    var s = "";
                    $.each(data, function (index, o) {
                        s += "<tr>";
                        s += "<td>" + (index + 1) + " </td>";
                        s += "<td>" + o.Apply.ApplyID + "</td>";
                        s += "<td>" + o.ApplyFlow.ApplyTypeName + "</td>";
                        s += "<td>" + o.Apply.ApplyTitle + "</td>";
                        s += "<td>" + o.Apply.Approver + "</td>";
                        s += "<td>" + o.Apply.Assigner + "</td>";
                        s += "<td>" + o.Apply.Dealer + "</td>";
                        var msg = "";
                        if (o.Apply.ApplyStatus == 1) {
                            msg = "待审批";
                        }
                        if (o.Apply.ApplyStatus == 2) {
                            msg = "待分配";
                        }
                        if (o.Apply.ApplyStatus == 3) {
                            msg = "待处理";
                        }
                        if (o.Apply.ApplyStatus == 4) {
                            msg = "归档";
                        }
                        if (o.Apply.ApplyStatus == -1) {
                            msg = "审批否决";
                        }
                        s += "<td>" + msg + "</td>";
                        s += "<td>" + FormatJsonTime(o.Apply.ApplyDate) + "</td>";
                        s += "<td>";
                        s += "<img title='查看' onclick='ShowApply(" + o.Apply.ApplyID + "," + o.Apply.ApplyTypeID + ")' src='image/common/search.png' alt=''/>";
                        if (o.Apply.ApplyStatus == 1) {
                            s += "<img title='删除' onclick='DeleteApply(" + o.Apply.ApplyID + "," + o.Apply.ApplyTypeID + ")' src='image/common/delete.png' alt=''/>";
                        }
                        s += "</td>";
                        s += "</tr>";
                    });
                    $("#tableApply tr:gt(0)").remove(); //在页面上查找id="tableUserInfo"的元素，并且把下标大于0的tr都删掉
                    $("#tableApply").append(s); //把变量s追加到id="tableUserInfo"的里面去
                }
            });
        }

        //查看单条的申请单信息
        function ShowApply(ApplyID, ApplyTypeID) {
            $("#dlg1").dialog({ width: 600, height: 400 }).dialog("setTitle", "查看申请单信息").dialog("open");
            $("#table1 :input").attr("readonly", true);
            $("#btnSubmit1").hide();
            $("#btnEsc1").val("关闭");
            $.ajax({
                type: "post",
                url: "/Handlers/ApplyHandler.ashx",
                data: {
                    method: "GetSingleApply",
                    ID: ApplyID
                },
                dataType: "json",
                success: function (data) {
                    $.each(data, function (index, o) {
                         $("#txtTelphone1").val(o.Apply.Phone),
                         $("#txtApplyTitle1").val(o.Apply.ApplyTitle),
                         $("#txtApplyReason1").val(o.Apply.ApplyReason),
                         $("#ContentPlaceHolder2_lblUserID1").text(o.Apply.UserID),
                         $("#ContentPlaceHolder2_lblApplyName1").text(o.UserInfo.Name),
                         $("#ContentPlaceHolder2_lblApplyDept1").text(o.Department.DeptName),
                         $("#txtRemark1").val(o.Apply.Remark),
                         $("#file1").val(o.Apply.Enclosure)
                    });
                }
            });
            if (ApplyTypeID == 1) {
                $("#IPAddress").show();
                $("#OfficeEmail").hide();
                $("#StorageSpace").hide();
                $.ajax({
                    type: "post",
                    url: "/Handlers/IPAddressApplyHandler.ashx",
                    data: {
                        method: "GetSingleIPAddressApply",
                        ID: ApplyID
                    },
                    dataType: "json",
                    success: function (data) {
                        $("#txtQuantity1").val(data.Quantity),
                            $("#txtAddress1").val(data.Address),
                            $("#txtPortNumber1").val(data.PortNumber),
                            $("#txtTimeLimit1").val(FormatJsonDate(data.TimeLimit))
                    }
                });
            }
            if (ApplyTypeID == 2) {
                $("#OfficeEmail").show();
                $("#IPAddress").hide();
                $("#StorageSpace").hide();
                $.ajax({
                    type: "post",
                    url: "/Handlers/OfficeEmailApplyHandler.ashx",
                    data: {
                        method: "GetSingleOfficeEmailApply",
                        ID: ApplyID
                    },
                    dataType: "json",
                    success: function (data) {
                        $("#txtZone2").val(data.Zone),
                            $("#txtOfficePlace2").val(data.OfficePlace),
                            $("#txtUserName2").val(data.UserName),
                            $("#txtFullName2").val(data.FullName)
                    }
                });
            }
            if (ApplyTypeID == 3) {
                $("#StorageSpace").show();
                $("#OfficeEmail").hide();
                $("#IPAddress").hide();
                $.ajax({
                    type: "post",
                    url: "/Handlers/StorageSpaceApplyHandler.ashx",
                    data: {
                        method: "GetSingleStorageSpaceApply",
                        ID: ApplyID
                    },
                    dataType: "json",
                    success: function (data) {
                        $("#txtZone3").val(data.Zone),
                            $("#txtInterfaceMan3").val(data.InterfaceMan),
                            $("#txtRight3").val(data.Right),
                            $("#txtTimeLimit3").val(FormatJsonDate(data.TimeLimit))
                    }
                });
            }
        }
        // 删除方法
        function DeleteApply(ApplyID, ApplyTpeID) {
            if (confirm("是否要删除申请单号为" + ApplyID + "的申请单？")) {
                $.ajax({
                    type: "post",
                    url: "/Handlers/ApplyHandler.ashx",
                    data: {
                        method: "DeleteApply",
                        TypeID: ApplyTpeID,
                        ID: ApplyID
                    },
                    success: function (data) {
                        if (data == "1") {
                            //即时刷新列表上面的数据
                            BindApply();
                            $.messager.alert('温馨提示', "恭喜您，删除成功！", 'info');
                        }
                        else {
                            //表示添加失败
                            $.messager.alert('温馨提示', "对不起，删除失败！", 'info');
                        }
                    }
                });
            }
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>
                    <input id="btnApplyType" class="btn" type="button" value="创建申请单" />&nbsp;&nbsp;
                    &nbsp;&nbsp;
                </td>
                <td>
                    申请单号:<asp:TextBox ID="txtApplyID" runat="server"></asp:TextBox>
                </td>
                <td>
                    申请单类型:<select id="ddlApplyType" name="d1"><option value="0">--请选择--</option>
                    </select>
                </td>
                <td>
                    申请单标题:<asp:TextBox ID="txtApplyTitle" runat="server"></asp:TextBox>
                </td>
                <td>
                    申请单状态：<asp:DropDownList ID="ddlApplyStatus" runat="server">
                        <asp:ListItem Value="0" Text="--请选择--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="待审批"></asp:ListItem>
                        <asp:ListItem Value="2" Text="待分配"></asp:ListItem>
                        <asp:ListItem Value="3" Text="归档"></asp:ListItem>
                        <asp:ListItem Value="4" Text="审批否决"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <input type="button" id="btn_select" value="查询" class="btn" />
                </td>
            </tr>
        </table>
        <table id="tableApply" class="grid" border="1">
            <tr>
                <th>
                    序号
                </th>
                <th>
                    申请单号
                </th>
                <th>
                    申请类型
                </th>
                <th>
                    标题
                </th>
                <th>
                    审批人
                </th>
                <th>
                    分配人
                </th>
                <th>
                    处理人
                </th>
                <th>
                    申请状态
                </th>
                <th>
                    申请日期
                </th>
                <th>
                    管理
                </th>
            </tr>
        </table>
    </div>
    <div id="dlg" class="easyui-dialog" title="My Dialog" style="width: 400px; height: 300px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <table align="center" class="grid">
            <tr style="height: 50px">
                <td>
                    选择申请单类型：<select id="ddlApplyType2" name="d2">
                        <option value="0">--请选择--</option>
                    </select>
                </td>
            </tr>
            <tr style="height: 50px">
                <td>
                    <input id="btnInsertApply" type="button" class="btn" value="添加" />
                    <input id="btnEsc" type="button" class="btn" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dlg1" class="easyui-dialog" title="My Dialog" style="width: 400px; height: 300px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <table align="center" id="table1">
            <tr>
                <td>
                    申请人ID：
                </td>
                <td>
                    <label id="lblUserID1" runat="server">
                    </label>
                </td>
                <td>
                    申请部门：
                </td>
                <td>
                    <label id="lblApplyDept1" runat="server">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    申请人姓名：
                </td>
                <td>
                    <label id="lblApplyName1" runat="server">
                    </label>
                </td>
                <td>
                    联系电话：
                </td>
                <td>
                    <input id="txtTelphone1" runat="server" type="text" style="width: 100px" />
                </td>
            </tr>
            <tr>
                <td>
                    申请主题：
                </td>
                <td colspan="3">
                    <input id="txtApplyTitle1" type="text" style="width: 350px" />
                </td>
            </tr>
            <tr>
                <td>
                    申请原因：
                </td>
                <td colspan="3">
                    <input id="txtApplyReason1" type="text" style="width: 350px" />
                </td>
            </tr>
            <tr id="IPAddress">
                <td>
                    基本内容：
                </td>
                <td colspan="3">
                    申请数量：<input id="txtQuantity1" type="text" style="width: 110px" />
                    使用地点：<input id="txtAddress1" type="text" style="width: 110px" />
                    <br />
                    <br />
                    网口号码：<input id="txtPortNumber1" type="text" style="width: 110px" />
                    使用时间：<input id="txtTimeLimit1" type="text" style="width: 110px" />
                </td>
            </tr>
            <tr id="OfficeEmail">
                <td>
                    基本内容：
                </td>
                <td colspan="3">
                    空 间(兆) ：<input id="txtZone2" type="text" style="width: 105px" />
                    使用地点：<input id="txtOfficePlace2" type="text" style="width: 105px" />
                    <br />
                    <br />
                    使用者姓名：<input id="txtUserName2" type="text" style="width: 105px" />
                    姓名全拼：<input id="txtFullName2" type="text" style="width: 105px" />
                </td>
            </tr>
            <tr id="StorageSpace">
                <td>
                    基本内容：
                </td>
                <td colspan="3">
                    空间(兆)：<input id="txtZone3" type="text" style="width: 110px" />
                    接 口 人：<input id="txtInterfaceMan3" type="text" style="width: 110px" />
                    <br />
                    <br />
                    权 &nbsp; 限：<input id="txtRight3" type="text" style="width: 110px" />
                    使用期限：<input id="txtTimeLimit3" type="text" style="width: 110px" />
                </td>
            </tr>
            <tr>
                <td>
                    备注：
                </td>
                <td colspan="3">
                    <textarea id="txtRemark1" rows="5" cols="100" style="width: 350px; height: 50px;
                        resize: none;"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    附件：
                </td>
                <td colspan="3">
                    <input id="file1" type="file" value="浏览" style="width: 350px;" />
                </td>
            </tr>
            <tr align="center" style="height: 50px">
                <td colspan="4">
                    <input id="btnSubmit1" type="button" value="提交" class="btn" style="width: 80px" />&nbsp;&nbsp;
                    <input id="btnEsc1" type="button" value="取消" class="btn" style="width: 80px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>