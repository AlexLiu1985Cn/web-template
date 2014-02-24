<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_AdvertiseManage.aspx.cs"
    Inherits="CmsApp20.CmsBack.QH_AdvertiseManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>������</title>
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                ��ǰλ�ã������� >> <a href="QH_AdvertiseManage.aspx">������</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
        </div>
    </div>
    <table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF">
        <tr>
            <td height="24">
                <a href="QH_AddAdvertise.aspx" class="add">+��ӹ������</a>
            </td>
        </tr>
    </table>
    <table width="840px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF">
        <tr style="background: url(images/1.jpg) repeat-x; height: 30px; color: #FFFFFF;
            font-weight: bold;">
            <td width="120" align="center">
                ���
            </td>
            <td width="180" align="center">
                ����
            </td>
            <td width="180" align="center">
                ͼƬ��ַ
            </td>
            <td width="140" align="center">
                ���ӵ�ַ
            </td>
            <td width="100" align="center">
                ��������
            </td>
            <td width="120" align="center">
                ����
            </td>
        </tr>
        <asp:Repeater ID="RepeaterFriendLink" runat="server">
            <ItemTemplate>
                <tr bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                    <td width="122" height="28">
                        <%# Eval("order")%>
                    </td>
                    <td width="214">
                        <%# Eval("title")%>
                    </td>
                    <td width="220">
                        <%# Eval("ImageUrl")%>
                    </td>
                    <td width="220">
                        <a href="<%# Eval("linkUrl")%>" target='blank' title="<%# Eval("linkUrl")%>">
                            <%# Eval("linkUrl")%></a>
                    </td>
                    <td width="60">
                        <%# Eval("LinkType")%>
                    </td>
                    <td align="center">
                        <a href='QH_AdvertiseModify.aspx?ID=<%# Eval("id")%>'>�޸�</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp<a
                            href='QH_AdvertiseDel.aspx?ID=<%# Eval("id")%>' onclick='return ConfirmDel();'>ɾ��</a>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                    <td width="122" height="28">
                        <%# Eval("order")%>
                    </td>
                    <td width="214">
                        <%# Eval("title")%>
                    </td>
                    <td width="220">
                        <%# Eval("ImageUrl")%>
                    </td>
                    <td width="220">
                        <a href="<%# Eval("linkUrl")%>" target='blank' title="<%# Eval("linkUrl")%>">
                            <%# Eval("linkUrl")%></a>
                    </td>
                    <td width="60">
                        <%# Eval("LinkType")%>
                    </td>
                    <td align="center">
                        <a href='QH_AdvertiseModify.aspx?ID=<%# Eval("id")%>'>�޸�</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp<a
                            href='QH_AdvertiseDel.aspx?ID=<%# Eval("id")%>' onclick='return ConfirmDel();'>ɾ��</a>
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </table>
    <div align="right">
        <table align='center'>
            <tr>
                <td>
                    ����<asp:Label ID="lblRecordCount" runat="server" ForeColor="red"></asp:Label>����¼
                    ��ǰΪ<asp:Label ID="lblCurrentPage" runat="server" ForeColor="red"></asp:Label>/<asp:Label
                        ID="lblPageCount" runat="server" ForeColor="red"></asp:Label>ҳ
                    <asp:LinkButton ID="lbnPrevPage" runat="server" CommandName="prev" OnCommand="Page_OnClick"
                        Text="��һҳ"></asp:LinkButton>
                    <asp:LinkButton ID="lbnNextPage" runat="server" CommandName="next" OnCommand="Page_OnClick"
                        Text="��һҳ">��һҳ</asp:LinkButton>
                    &nbsp;ת����<asp:DropDownList ID="DropDownListPage" runat="server" OnSelectedIndexChanged="DropDownListPage_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
