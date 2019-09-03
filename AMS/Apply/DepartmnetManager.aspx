<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DepartmnetManager.aspx.cs" Inherits="Apply.DepartmnetManager" %>

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
            //绑定部门信息表
            BindDepartment();
            //绑定部门负责人下拉框

            $("#btnSelect").click(function () {
                BindDepartment();
            });
            //点击弹出选择申请单类型的窗口
            $("#btnInsertDept").click(function () {
                $("#dlg1").dialog({ width: 400, height: 200 }).dialog("setTitle", "新建部门信息").dialog("open");
            });
            //关闭窗口
            $("#btnEsc1").click(function () {
                $("#dlg1").dialog("close");
            });
            //关闭窗口
            $("#btnEsc2").click(function () {
                $("#dlg2").dialog("close");
            });
            //新增部门信息
            $("#btnSubmit1").click(function () {
                $.ajax({
                    type: "post", //键值对  用来指定HTTP请求的方式
                    url: "/Handlers/DepartmentHandler.ashx", //专门用来设置请求对应的路径 设置一般处理程序所对应的路径
                    data: {                     //参数data的功能：就是用来指定浏览器向服务器传递的数据
                        Method: "InsertDepartment",
                        DeptName: $("#txtDeptName1").val(),
                        DeptInfo: $("#txtRemark1").val()
                    },
                    success: function (data) {//参数data专门用来接收服务器返回给浏览器的数据
                        if (data == "2") {
                            //表示添加失败
                            $.messager.alert('温馨提示', '该部门名称已经存在！', 'info');
                        }
                        else {
                            if (data == "1") {
                                //表示新增成功
                                $("#dlg1").dialog("close");
                                $.messager.alert('温馨提示', '新增成功！', 'info');
                                BindDepartment();
                            }
                            else {
                                $.messager.alert('温馨提示', "新增失败！", 'info');
                            }
                        }
                    }
                });
            });

        });
        function BindDepartment() {
            //通过AJAX实现无刷新效果
            $.ajax({
                type: "get", //键值对  用来指定HTTP请求的方式
                url: "/Handlers/DepartmentHandler.ashx", //专门用来设置请求对应的路径 设置一般处理程序所对应的路径
                data: {                     //参数data的功能：就是用来指定浏览器向服务器传递的数据
                    Method: "GetPagedDepartment",
                    DeptName: $("#txtDeptName").val()
                },
                dataType: "json",
                success: function (data) {//参数data专门用来接收服务器返回给浏览器的数据
                    var s = "";
                    $.each(data, function (index, o) {
                        s += "<tr>";
                        s += "<td>" + o.Department.DeptID + "</td>";
                        s += "<td>" + o.Department.DeptName + "</td>";
                        s += "<td>" + o.UserInfo.Name + "</td>";
                        s += "<td>" + o.Department.DeptInfo + "</td>";
                        s += "<td>";
                        s += "<img title='编辑' onclick='EditDepartment(" + o.Department.DeptID + ")' src='image/common/edit.png' alt=''/>";
                        if (o.Department.DeptID != 1 && o.Department.DeptID != 2) {
                            s += "<img title='删除' onclick='DeleteDepartment(" + o.Department.DeptID + ")' src='image/common/delete.png' alt=''/>";
                        }
                        s += "</td>";
                        s += "</tr>";
                    });
                    $("#tableDepartment tr:gt(0)").remove(); //在页面上查找id="tableUserInfo"的元素，并且把下标大于0的tr都删掉
                    $("#tableDepartment").append(s); //把变量s追加到id="tableUserInfo"的里面去
                }
            });
        }

        function EditDepartment(DeptID) {
            $("#dlg2").dialog({ width: 400, height: 250 }).dialog("setTitle", "修改部门信息").dialog("open");
            $("#txtDeptName2").attr("readonly", true);
            $.ajax({
                type: "get",
                url: "/Handlers/DepartmentHandler.ashx",
                dataType: "json",
                data: {
                    Method: "BindDeptManager",
                    ID: DeptID,
                    r: Math.random()
                },
                success: function (data) {
                    var s = "";
                    $.each(data, function (index, o) {
                        s += "<option value='" + o.UserID + "'>" + o.Name + "</option>";
                    });
                    $("#DeptManager").empty();
                    $("#DeptManager").append(s);
                }
            });
            $.ajax({
                type: "post",
                url: "/Handlers/DepartmentHandler.ashx",
                data: {
                    method: "GetSingleDept",
                    ID: DeptID,
                    r: Math.random()
                },
                dataType: "json",
                success: function (data) {
                    $("#txtDeptName2").val(data.DeptName),
                         $("#DeptManager").val(data.Manager),
                         $("#txtRemark2").val(data.DeptInfo)
                }
            });
            //修改部门信息
            $("#btnSubmit2").click(function () {
                $.ajax({
                    type: "post",
                    url: "/Handlers/DepartmentHandler.ashx",
                    data: {
                        Method: "UpdateDepartment",
                        ID: DeptID,
                        DeptName: $("#txtDeptName2").val(),
                        DeptManager: $("#DeptManager").val(),
                        DeptInfo: $("#txtRemark2").val()
                    },
                    success: function (data) {
                        if (data == "1") {
                            //表示添加成功
                            //关闭窗口
                            $("#dlg2").dialog("close");
                            //即时刷新列表上面的数据
                            BindDepartment();
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
        // 删除方法
        function DeleteDepartment(DeptID) {
            if (confirm("确定要删除该部门吗？")) {
                $.ajax({
                    type: "post",
                    url: "/Handlers/DepartmentHandler.ashx",
                    data: {
                        method: "DeleteDepartment",
                        ID: DeptID
                    },
                    success: function (data) {
                        if (data == "2") {
                            //即时刷新列表上面的数据
                            $.messager.alert('温馨提示', "该部门下存在用户，请首先调整用户所属部门或删除用户！", 'info');

                        } else {
                            if (data == "1") {
                                //即时刷新列表上面的数据
                                BindDepartment();
                                $.messager.alert('温馨提示', "恭喜您，删除成功！", 'info');

                            }
                            else {
                                //表示添加失败
                                $.messager.alert('温馨提示', "对不起，删除失败！", 'info');
                            }
                        }
                    }
                });
            }
        }
        function validatesInsert() {
            if ($("#txtDeptName1").val() == "") {
                alert("部门名称不能为空！")
                return false;
            }
            else {
                return true;
            }
        }
        function validatesUpdate() {
            if ($("#txtDeptName2").val() == "") {
                alert("部门名称不能为空！")
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table align="center">
        <tr>
            <td colspan="4">
                <input id="btnInsertDept" class="btn" type="button" value="新  建" />&nbsp;&nbsp;
            </td>
            <td>
                部门名称：<input type="text" id="txtDeptName" />
            </td>
            <td>
                <input id="btnSelect" class="btn" type="button" value="查询" />
            </td>
        </tr>
    </table>
    <table id="tableDepartment" class="grid" border="1">
        <tr>
            <th>
                序号
            </th>
            <th>
                部门名称
            </th>
            <th>
                部门负责人
            </th>
            <th>
                备注信息
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
                    <span style="color: Red">*</span>部门名称：
                </td>
                <td>
                    <input type="text" id="txtDeptName1" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    备注说明：
                </td>
                <td>
                    <textarea id="txtRemark1" rows="5" cols="100" style="width: 250px; height: 50px;
                        resize: none;"></textarea>
                </td>
            </tr>
            <tr align="center" style="height: 50px">
                <td colspan="2">
                    <input id="btnSubmit1" type="button" value="提交" onclick="return validatesInsert()"
                        class="btn" style="width: 50px" />&nbsp;&nbsp;
                    <input id="btnEsc1" type="button" value="取消" class="btn" style="width: 50px" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dlg2" class="easyui-dialog" title="My Dialog" style="width: 400px; height: 300px;"
        data-options="iconCls:'icon-save',resizable:true,modal:true,closed:true">
        <table align="center" id="table2">
            <tr>
                <td>
                    <span style="color: Red">*</span>部门名称：
                </td>
                <td>
                    <input type="text" id="txtDeptName2" style="width: 250px" />
                </td>
            </tr>
            <tr>
                <td>
                    部门负责人：
                </td>
                <td>
                    <select id="DeptManager" name="sel">
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    备注说明：
                </td>
                <td>
                    <textarea id="txtRemark2" rows="5" cols="100" style="width: 250px; height: 50px;
                        resize: none;"></textarea>
                </td>
            </tr>
            <tr align="center" style="height: 50px">
                <td colspan="2">
                    <input id="btnSubmit2" type="button" value="提交" onclick="return validatesUpdate()"
                        class="btn" style="width: 50px" />&nbsp;&nbsp;
                    <input id="btnEsc2" type="button" value="取消" class="btn" style="width: 50px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
