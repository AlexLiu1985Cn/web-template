<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateShow.aspx.cs" Inherits="CmsApp20.CmsBack.TemplateShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>创都启航网站管理系统</title>
<style >
html,body{ font:12px/1.8 Tahoma,"微软雅黑",'Simsun'; color:#333; background:#ffffff;/*#197bd9;*/ padding-top:10px; }
a{ color:#0066CC; text-decoration:none; }
a:hover{ color:#777;}
.clear{ clear:none}
body, div,form{ margin: 0; padding: 0; }

#tempshow{ width:100%;  padding:0px 0px 5px 0px; text-align:left; float:left;  }
#tempbiao { background:url(images/1.jpg) repeat-x; text-align:left; padding-left:10px; height:25px; color:#ffffff; font-weight:bold;  }
.temdan { margin:5px 5px 5px 5px; padding:10px 0px 5px 5px;float:left;width:210px; border:1px dotted #91ADBF; white-space: nowrap; text-overflow:ellipsis; overflow: hidden; }
.temdan img { border:1px solid #91ADBF }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div  id="tempshow">
<div id="tempbiao">本地已安装模板</div>
        <asp:Repeater ID="RD" runat="server" OnItemCommand="R1_ItemCommand" OnItemDataBound="R1_ItemDataBound" >
          <ItemTemplate>
<div class="temdan" id=deflst runat=server >
<a href="TemplateDetail.aspx?ID=<%# Eval("id")%>&UsedID=<%# Eval("UsedID")%>" title='<%# Eval("TemplateName")%>' >
<img src="<%# Eval("ThumbImag")%>" width="200" height="150" title="点击查看详细信息！" /></a>
<br />
<b>名称</b>：<%# Eval("TemplateName")%><br />
<b>作者</b>：<%# Eval("Designer")%><br /><input type="hidden" id="SelectedID" runat="server" value='<%# Eval("id")%>'/>
<b>发布</b>：<%# Eval("AddDate1")%><br />
<b>描述</b>：<%# Eval("Abstract1")%><br />
<b>状态</b>：<b><asp:LinkButton ID="LinkBttnUse" runat="server">使用此模板</asp:LinkButton></b>
</div>
   </ItemTemplate>
  </asp:Repeater>
 <div class="clear"></div>
</div>
   </form>
<IFRAME  src="http://www.95c.com.cn/TemplateWebsite.aspx?TM=<%=strTMShowType %>" frameBorder=0 width=100% height=350 scrolling="no"></IFRAME><%--localhost:134--%>
</body>
</html>
