<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomMessageManage.aspx.cs"
    Inherits="CmsApp20.CDQHCmsBack.CustomMessageManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <title>无标题页</title>
    <style type="text/css">
        body
        {
            font-family: Microsoft Yahei;
            font-size: 12px;
        }
        .add
        {
            margin-left: 20px;
            font-weight: bold;
        }
        .srch
        {
            width: 140px;
        }
        td
        {
            word-wrap: break-word;
            word-break: break-all;
        }
    </style>
</head>
<script type="text/jscript">
    $ = function (id) { return document.getElementById(id); }
    String.prototype.Trim = function () {
        return this.replace(/(^\s*)|(\s*$)/g, '');
    }
    function fDel() {
        return confirm("确定要删除？");
    }
    function comfirmClr() {
        return confirm("确定要清空" + $("DDLFps").options[$("DDLFps").selectedIndex].text + "？");
    }
</script>
<body>
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：留言管理 >> <a href="CustomMessageManage.aspx">自定义留言管理</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    <table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF">
        <tr>
            <td width="180" height="35">
                <a href="QH_CustomMessageSet.aspx?ColumnID=<%=strClmnID%><%=strBackUrl %>&BackPage="
                    class="add">+自定义留言系统设置</a>
            </td>
            <td width="640" height="35">
                <span>留言信息分类&nbsp;&nbsp;<asp:DropDownList ID="DDLFps" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="DDLFps_SelectedIndexChanged">
                    <asp:ListItem>所有信息</asp:ListItem>
                    <asp:ListItem>未回复信息</asp:ListItem>
                    <asp:ListItem>已回复信息</asp:ListItem>
                    <asp:ListItem>未审核信息</asp:ListItem>
                    <asp:ListItem>已审核信息</asp:ListItem>
                </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnClr" runat="server" Text="清空"
                        OnClick="BtnClr_Click" OnClientClick="return comfirmClr();" /></span>
            </td>
        </tr>
    </table>
    <table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF">
        <tr style="background: url(images/1.jpg) repeat-x; height: 30px; color: #FFFFFF;
            font-weight: bold;">
            <td width="30" height="30">
                <div align="center">
                    选择</div>
            </td>
            <td width="210" height="30">
                <div align="center">
                    留言标题</div>
            </td>
            <td width="110" height="30">
                <div align="center">
                    姓名</div>
            </td>
            <td width="90" height="30">
                <div align="center">
                    手机</div>
            </td>
            <td width="120" align="center">
                来源
            </td>
            <td width="40">
                <div align="center">
                    审核</div>
            </td>
            <td width="140">
                <div align="center">
                    提交时间</div>
            </td>
            <%--<td width="80" ><div align="center">权限</div></td>--%>
            <td width="100">
                <div align="center">
                    具体操作</div>
            </td>
        </tr>
        <asp:Repeater ID="RepeaterNews" runat="server">
            <ItemTemplate>
                <tr bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                    <td height="30">
                        <div align="center">
                            <input type="checkbox" name="checkbox" id="checkbox" value="checkbox" runat="server" /></div>
                    </td>
                    <input type="hidden" id="SelectedID" runat="server" value='<% # DataBinder.Eval(Container.DataItem, "id")%>' />
                    <td height="30">
                        <div align="left">
                            <%# Eval("title")%></div>
                    </td>
                    <td height="30">
                        <div align="left">
                            <%# Eval("Name")%></div>
                    </td>
                    <td height="30">
                        <div align="left">
                            <%# Eval("mobile")%></div>
                    </td>
                    <td height="30" width="120" align="center">
                        <%# Eval("source")%>
                    </td>
                    <td height="30">
                        <div align="center">
                            <%# Eval("readok")%></div>
                    </td>
                    <td height="30">
                        <div align="center">
                            <%#Eval("AddDate1")%></div>
                    </td>
                    <%--<%#((DateTime)Eval("AddDate")).ToString("yyyy-MM-dd  HH:mm:ss")%>--%>
                    <%--<td height="30"><div align="left" >
      <div align="center"><span ><%# Eval("Access")%></span></div>
    </div></td>--%>
                    <td height="30">
                        <div align="center">
                            <span><a href="QH_LookMessageCustom.aspx?id=<%# Eval("id") %>&ColumnID=<%=strClmnID %><%=strBackUrl %>&Page=<%=strPage %>&Sel=<%=strSel %>">
                                查看</a> &nbsp;&nbsp; |&nbsp;&nbsp; <a href="QH_DelMessageCustom.aspx?id=<%# Eval("id") %><%=strBackUrl %>&Page=<%=strPage %>&Sel=<%=strSel %>"
                                    onclick="return fDel();">删除</a></span></div>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                    <td height="30">
                        <div align="center">
                            <input type="checkbox" name="checkbox" id="checkbox" value="checkbox" runat="server" /></div>
                    </td>
                    <input type="hidden" id="SelectedID" runat="server" value='<% # DataBinder.Eval(Container.DataItem, "id")%>' />
                    <td height="30">
                        <div align="left">
                            <%# Eval("title")%></div>
                    </td>
                    <td height="30">
                        <div align="left">
                            <%# Eval("Name")%></div>
                    </td>
                    <td height="30">
                        <div align="left">
                            <%# Eval("mobile")%></div>
                    </td>
                    <td height="30" width="120" align="center">
                        <%# Eval("source")%>
                    </td>
                    <td height="30">
                        <div align="center">
                            <%# Eval("readok")%></div>
                    </td>
                    <td height="30">
                        <div align="center">
                            <%# Eval("AddDate1")%></div>
                    </td>
                    <%--<td height="30"><div align="left" >
      <div align="center"><span ><%# Eval("Access")%></span></div>
    </div></td>--%>
                    <td height="30">
                        <div align="center">
                            <span><a href="QH_LookMessageCustom.aspx?id=<%# Eval("id") %>&ColumnID=<%=strClmnID %><%=strBackUrl %>&Page=<%=strPage %>&Sel=<%=strSel %>">
                                查看</a> &nbsp;&nbsp; |&nbsp;&nbsp; <a href="QH_DelMessageCustom.aspx?id=<%# Eval("id") %><%=strBackUrl %>&Page=<%=strPage %>&Sel=<%=strSel %>"
                                    onclick="return fDel();">删除</a></span></div>
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
        <!-- 是否全选开始 -->
        <tr>
            <td height="30" colspan="6" bgcolor="" align="left">
                <asp:CheckBox Text="全选" ID="Checkbox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckboxslctallSV_CheckedChanged" />
                <span>
                    <asp:Button ID="Button1" runat="server" Text="删除选定的项目" Height="28px" OnClientClick="return confirm('确定要删除选定的项目吗？');"
                        OnClick="ButtondeleteSV_Clicked" /></span>
            </td>
            <td height="30" align="center" colspan="3">
                每页显示：<asp:DropDownList ID="DDLPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLPage_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <!-- 是否全选结束 -->
    </table>
    <table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF">
        <tr>
            <td height="30">
                共有<asp:Label ID="lblRecordCount" runat="server" ForeColor="red"></asp:Label>条记录
                当前为<asp:Label ID="lblCurrentPage" runat="server" ForeColor="red"></asp:Label>/<asp:Label
                    ID="lblPageCount" runat="server" ForeColor="red"></asp:Label>页
                <asp:LinkButton ID="lbnPrevPage" runat="server" CommandName="prev" OnCommand="Page_OnClick"
                    Text="上一页"></asp:LinkButton>
                <asp:LinkButton ID="lbnNextPage" runat="server" CommandName="next" OnCommand="Page_OnClick"
                    Text="下一页">下一页</asp:LinkButton>
                &nbsp;转到：<asp:DropDownList ID="DropDownListPage" runat="server" OnSelectedIndexChanged="DropDownListPage_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
