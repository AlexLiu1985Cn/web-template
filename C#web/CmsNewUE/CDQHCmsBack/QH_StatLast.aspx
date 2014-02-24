<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_StatLast.aspx.cs" Inherits="CmsApp20.CmsBack.QH_StatLast" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
 <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <style type="text/css"  >
        body
        {
            font-family: Microsoft Yahei;
            font-size: 13px;
        }
h4{ padding-left :10px;background:#F2F2F2; width:1000px;}
</style>
</head>
<body>
    <form id="form1" runat="server">
<%--        <div >
					<h3>最近访客：</h3>
				</div>
--%>
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：访问统计 >> <a href="QH_StatLast.aspx">最近访客</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>
    </div>
    </div>

				<table width="840px"  align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF" >
<tr style=" text-align:center;  height:30px; color:#000000; font-weight:bold;"><td>
					最近访客报表
				</td></tr></table>
				
	<table width=840 border="0" align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF" style="text-align:left;" >
	<thead>
		<tr  style="background:url(images/1.jpg) repeat-x; height:25px; color:#FFFFFF; font-weight:bold;">
			<th width="20%" style="">时间</th>
			<th width="14%">IP地址</th>
			<th width="33%">来源网址</th>
			<th width="33%">访问网址</th>
		</tr>
	</thead>
	<tbody>	

<asp:Repeater ID="RStatList" runat="server" EnableViewState =false >
<ItemTemplate >
<tr bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
<td><%#((string[])Container.DataItem)[3]%></td>
<td><a target="_blank" href="http://ip.taobao.com/ipSearch.php?ipAddr=<%#((string[])Container.DataItem)[0]%>"><%#((string[])Container.DataItem)[0]%></a></td>
<td style="font-size:12px; font-family:Times New Roman;" ><a target=_blank title='<%#((string[])Container.DataItem)[2]%>' href='<%#((string[])Container.DataItem)[2]%>'><%#SetLength(((string[])Container.DataItem)[2],40)%></a></td>
<td style="font-size:12px; font-family:Times New Roman;" ><a target=_blank title='<%#((string[])Container.DataItem)[1]%>' href='<%#((string[])Container.DataItem)[1]%>'><%#SetLastLength(((string[])Container.DataItem)[1],40)%></a></td>
</tr>
</ItemTemplate>
<AlternatingItemTemplate >
  <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
<td><%#((string[])Container.DataItem)[3]%></td>
<td><a target="_blank" href="http://ip.taobao.com/ipSearch.php?ipAddr=<%#((string[])Container.DataItem)[0]%>"><%#((string[])Container.DataItem)[0]%></a></td>
<td style="font-size:12px; font-family:Times New Roman; "><a target=_blank title='<%#((string[])Container.DataItem)[2]%>' href='<%#((string[])Container.DataItem)[2]%>'><%#SetLength(((string[])Container.DataItem)[2],40)%></a></td>
<td style="font-size:12px; font-family:Times New Roman;" ><a target=_blank title='<%#((string[])Container.DataItem)[1]%>' href='<%#((string[])Container.DataItem)[1]%>'><%#SetLastLength(((string[])Container.DataItem)[1],40)%></a></td>
</tr>
</AlternatingItemTemplate>
</asp:Repeater>
	</tbody>
	</table> 
 
    </form>
</body>
</html>
