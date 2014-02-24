<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CmsMemberManage.aspx.cs"
    Inherits="CmsApp20.CDQHCmsBack.CmsMemberManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
<head id="Head1" runat="server">
    <title>管理会员</title>
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
            word-break: break-all;
        }
        td a
        {
            color: #333333;
        }
    </style>
</head>
<script type="text/javascript">
    function to(id) {
        location = "CmsMemDetail.aspx?ID=" + id;
    }
</script>
<body>
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：会员管理 >> <a href="CmsMemberManage.aspx">管理会员</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    <table border="0" cellpadding="2" cellspacing="0" id="table3" style="margin-left: 140px;">
        <tr>
            <td rowspan="2">
                用户名：<input type="text" id="user_name" size="18" runat="server" class="input" value="">
            </td>
            <td rowspan="2">
                <asp:Button ID="ButtonSelect" runat="server" Text="搜索用户" Height="22px" CssClass="btnSel"
                    OnClick="BUserNameSrch_Click" />
            </td>
            <td rowspan="2" style="width: 400px; text-align: right;">
                （点击会员名查看详细信息）
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <div style="text-align: center">
        <table width="940px" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF">
            <tr style="background: url(images/1.jpg) repeat-x; height: 30px; color: #FFFFFF;
                font-weight: bold;">
                <td width="60" height="30">
                    <div align="center">
                        编号</div>
                </td>
                <td width="120" height="30">
                    <div align="center">
                        会员名</div>
                </td>
                <td width="40">
                    <div align="center">
                        性别</div>
                </td>
                <td width="100">
                    <div align="center">
                        联系电话</div>
                </td>
                <td width="100">
                    <div align="center">
                        手机</div>
                </td>
                <td width="140">
                    <div align="center">
                        E-Mail</div>
                </td>
                <td width="160">
                    <div align="center">
                        公司名称</div>
                </td>
                <td width="60">
                    <div align="center">
                        登录次数</div>
                </td>
                <td width="80">
                    <div align="center">
                        收货人</div>
                </td>
                <td width="80">
                    <div align="center">
                        操作</div>
                </td>
            </tr>
            <asp:Repeater ID="RepeaterMember" runat="server" OnItemCommand="RepeaterMember_ItemCommand">
                <ItemTemplate>
                    <tr bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="left" id="UserID" runat="server">
                                    <%# Eval("UserID")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="left">
                                    <%# Eval("UserName")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="center">
                                    <%# Eval("Sex")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="center">
                                    <%# Eval("Phone")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="center">
                                    <%# Eval("Mobile")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="left">
                                    <%# Eval("Email")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="left">
                                    <%# Eval("CompanyName")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="left">
                                    <%# Eval("logins")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="center">
                                    <%# Eval("Receiver")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <div align="center">
                                <asp:LinkButton ID="BtnDelete" CommandName="Del" runat="server" OnClientClick="return confirm('你确定要删除吗')">删除</asp:LinkButton></div>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="left" id="UserID" runat="server">
                                    <%# Eval("UserID")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="left">
                                    <%# Eval("UserName")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="center">
                                    <%# Eval("Sex")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="center">
                                    <%# Eval("Phone")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="center">
                                    <%# Eval("Mobile")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="left">
                                    <%# Eval("Email")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="left">
                                    <%# Eval("CompanyName")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="left">
                                    <%# Eval("logins")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("UserID")%>)">
                                <div align="center">
                                    <%# Eval("Receiver")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <div align="center">
                                <asp:LinkButton ID="BtnDelete" CommandName="Del" runat="server" OnClientClick="return confirm('你确定要删除吗')">删除</asp:LinkButton></div>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <!-- 是否全选开始 -->
            <tr>
                <td height="30" colspan="4" bgcolor="" align="left">
                </td>
                <td height="30" align="center" colspan="2">
                    每页显示：<asp:DropDownList ID="DDLPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLPage_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <!-- 是否全选结束 -->
        </table>
        <table width="840px" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF">
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
    </div>
    </form>
</body>
</html>
