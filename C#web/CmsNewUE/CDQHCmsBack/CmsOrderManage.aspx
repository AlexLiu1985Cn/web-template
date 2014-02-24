<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CmsOrderManage.aspx.cs"
    Inherits="CmsApp20.CDQHCmsBack.CmsOrderManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
<head id="Head1" runat="server">
    <title>全部定单</title>
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
        location = "CmsOrderDetails.aspx?sub_num=" + id;
    }
    function toName(Name) {
        location = "CmsOrderManage.aspx?sub_name=" + encodeURIComponent(Name);
    }
</script>
<body>
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：定单管理 >> <a href="CmsOrderManage.aspx">全部定单</a></div>
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
                <font color="#FF0000"><b>查询统计：</b></font>
            </td>
            <td>
                用户名：
            </td>
            <td>
                开始日期：
            </td>
            <td>
                结束日期：
            </td>
            <td>
                订单状态：
            </td>
            <td rowspan="2">
                <asp:Button ID="ButtonSelect" runat="server" Text="确定" Height="22px" OnClick="ButtonSelect_Click" />
            </td>
            <td rowspan="2" style="width: 400px; text-align: right;">
                （点击定单号查看详细信息）
            </td>
        </tr>
        <tr>
            <td>
                <input type="text" id="user_name" size="12" runat="server" class="input" value="">
            </td>
            <td>
                <input type="text" id="Begindate" size="12" runat="server" class="input" value="">
            </td>
            <td>
                <input type="text" id="Enddate" size="12" runat="server" class="input" value="">
            </td>
            <td>
                <select size="1" id="State" runat="server">
                    <option value="">全部订单</option>
                    <option value="0">未处理订单</option>
                    <option value="1">已完成订单</option>
                </select>
            </td>
        </tr>
    </table>
    <br />
    <div style="text-align: center">
        <table width="1040px" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF">
            <tr style="background: url(images/1.jpg) repeat-x; height: 30px; color: #FFFFFF;
                font-weight: bold;">
                <td width="60" height="30">
                    <div align="center">
                        ID</div>
                </td>
                <td width="120" height="30">
                    <div align="center">
                        定单号</div>
                </td>
                <td width="100">
                    <div align="center">
                        定单状态</div>
                </td>
                <td width="100">
                    <div align="center">
                        定货人</div>
                </td>
                <td width="240">
                    <div align="center">
                        定货地址</div>
                </td>
                <td width="80">
                    <div align="center">
                        联系人</div>
                </td>
                <td width="160">
                    <div align="center">
                        电话</div>
                </td>
                <td width="180">
                    <div align="center">
                        日期</div>
                </td>
            </tr>
            <asp:Repeater ID="RepeaterMember" runat="server">
                <ItemTemplate>
                    <tr bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                        <td height="30">
                            <a href="javascript:to(<%# Eval("sub_number")%>)">
                                <div align="left">
                                    <%# Eval("sub_id")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("sub_number")%>)">
                                <div align="left">
                                    <%# Eval("sub_number")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <div align="center">
                                <%# Eval("sub_State")%></div>
                        </td>
                        <td height="30">
                            <a href="javascript:toName('<%# Eval("sub_name")%>')">
                                <div align="center">
                                    <%# Eval("sub_name")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <div align="left">
                                <%# Eval("sub_toadds")%></div>
                        </td>
                        <td height="30">
                            <div align="left">
                                <%# Eval("sub_to")%></div>
                        </td>
                        <td height="30">
                            <div align="left">
                                <%# Eval("sub_totel")%></div>
                        </td>
                        <td height="30">
                            <div align="center">
                                <%# Eval("sub_date")%></div>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                        <td height="30">
                            <a href="javascript:to(<%# Eval("sub_number")%>)">
                                <div align="left">
                                    <%# Eval("sub_id")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <a href="javascript:to(<%# Eval("sub_number")%>)">
                                <div align="left">
                                    <%# Eval("sub_number")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <div align="center">
                                <%# Eval("sub_State")%></div>
                        </td>
                        <td height="30">
                            <a href="javascript:toName('<%# Eval("sub_name")%>')">
                                <div align="center">
                                    <%# Eval("sub_name")%></div>
                            </a>
                        </td>
                        <td height="30">
                            <div align="left">
                                <%# Eval("sub_toadds")%></div>
                        </td>
                        <td height="30">
                            <div align="left">
                                <%# Eval("sub_to")%></div>
                        </td>
                        <td height="30">
                            <div align="left">
                                <%# Eval("sub_totel")%></div>
                        </td>
                        <td height="30">
                            <div align="center">
                                <%# Eval("sub_date")%></div>
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
