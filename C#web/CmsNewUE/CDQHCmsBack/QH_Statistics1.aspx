<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_Statistics1.aspx.cs" Inherits="CmsApp20.CmsBack.QH_Statistics1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
   <script type ="text/javascript"  src ="js/NavFrame.js"></script> 
 <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
   <style type="text/css"  >
        body
        {
            font-family: Microsoft Yahei;
            font-size: 12px;
        }
.ttl {
	float:left;
	padding-left:12px;
}
.more {
	float:right;
	font-size:12px;
	font-weight:normal;
	padding-right:10px;
}
h4{margin-left :160px;background:#F2F2F2; width:840px;}
table.data03 {
	margin:10px 0px;
	font-family:Arial;
	font-size:15px;
}

table.data03 th {
	font-size:14px;
	line-height:24px;
	text-align:left;
}

table.data03 td {
	font-size:14px;
	line-height:24px;
	text-align:left;
	padding-bottom:5px;
}

table.data03 td.itm {
	padding-right:30px;
	text-align:right;
	width:100px;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
<%--    <div >
					<h3>综合报告：</h3>
				</div>
--%>
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：访问统计 >> <a href="QH_Statistics1.aspx">综合报告</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>
    </div>
   </div>

				<h4>
					最近30天流量趋势
				</h4>
      <div style="text-align:center; margin-top:10px">
           <asp:Literal ID="FCLiteral" runat="server" EnableViewState=false ></asp:Literal>
    </div>
				<h4>
					<span class="ttl">在线人数</span>
					<span class="more">5分钟在线人数：<strong><%=str5m %> </strong></span>
					<span class="more">10分钟在线人数：<strong><%=str10m %> </strong></span>
					<span class="more">15分钟在线人数：<strong><%=str15m %> </strong></span>
				</h4>
       <div style=" clear:both; text-align:center; margin-top:10px"><div style="margin-left:-560px; color:#ff6600;" id=mUV runat =server enableviewstate=false >━ 每分钟在线人数趋势(UV)</div>
           <asp:Literal ID="FCLiteral1" runat="server" EnableViewState=false ></asp:Literal>
    </div>
 				<h4>
					网站流量
				</h4>
       <div style="margin:0 auto; width:600px; margin-top:10px" >
       <%=strClose %>
				<table width="600" align="center" cellspacing="0" class="data03" id="stat" style="width:600px" runat =server enableviewstate =false >
	  <tr>
						<th></th><th width="150">PV</th><th width="150">UV</th><th width="112">IP</th>
					</tr>
					<tr>
						<td class="itm">今日：</td><td><%=strTDPV %></td><td><%=strTDUV %></td><td><%=strTDIP %></td>
					</tr>
					<tr>
						<td class="itm">昨日：</td><td><%=strYDPV %></td><td><%=strYDUV %></td><td><%=strYDIP %></td>
					</tr>
					<tr>
						<td class="itm">平均每日：</td><td><%=strAVDPV %></td><td><%=strAVDUV%></td><td><%=strAVDIP%></td>
					</tr>
					<tr>
						<td class="itm">近30天合计：</td><td><%=strMPV %></td><td><%=strMUV%></td><td><%=strMIP%></td>
					</tr>
					<tr>
						<td class="itm">总量：</td><td><%=strTLPV %></td><td><%=strTLUV%></td><td><%=strTLIP%></td>
					</tr>
					<tr>
						<td height="10"></td>
					</tr>
					<tr>
						<td class="itm"><strong>最高记录：</strong></td><td><strong><%=strMaxPV %></strong></td><td><strong><%=strMaxUV%></strong></td><td><strong><%=strMaxIP%></strong></td>
					</tr>
					<tr>
						<td>&nbsp;</td><td><span class="f12">发生在:<%=strTMPV %></span></td><td><span class="f12">发生在:<%=strTMUV%></span></td><td><span class="f12">发生在:<%=strTMIP%></span></td>
					</tr>
				</table>	
 
           
      </div> 

    </form>
</body>
</html>
