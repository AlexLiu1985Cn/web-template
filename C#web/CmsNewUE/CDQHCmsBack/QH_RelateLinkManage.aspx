<%@ Page Language="C#" AutoEventWireup="true" Inherits="QH_RelateLinkManage" CodeBehind="QH_RelateLinkManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>创都启航网站管理系统</title>
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <style type="text/css">
        BODY
        {
            margin-top: 0px;
            font-size: 12px;
        }
    </style>
</head>
<script language="JavaScript" type="text/JavaScript">
/*
function checkBig()
{
  if (document.form1.BigClassName.value=="")
  {
    alert("大类名称不能为空！");
    document.form1.BigClassName.focus();
    return false;
  }
}
function checkSmall()
{
  if (document.form2.BigClassName.value=="")
  {
    alert("请先添加大类名称！");
	document.form1.BigClassName.focus();
	return false;
  }

  if (document.form2.SmallClassName.value=="")
  {
    alert("小类名称不能为空！");
	document.form2.SmallClassName.focus();
	return false;
  }
}
*/
function ConfirmDelBig()
{
   if(confirm("确定要删除此相关链接吗？"))//删除此大类同时将删除所包含的小类和该类下的所有标签，并且不能恢复！
     return true;
   else
     return false;
	 
}
}
</script>
<body>
    <form id="form1" runat="server">
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：其它设置 >> <a href="QH_RelateLinkManage.aspx">热点关键词管理</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
        </div>
    </div>
    <div>
        <!-- <div style="text-align:center"><a href="QH_News_AddBig.aspx" ><strong><font color="#FF0000"><u>添加标签类别</u></font></strong></a></div> -->
        <br />
        <div style="text-align: center" id="CategoryTable" runat="server">
        </div>
    </div>
    <div style="text-align: center" id="NewsTypeTable" runat="server">
        <!--
        <table width="600" border="0" cellpadding="0" cellspacing="1" bgcolor="#000000">
        <tr bgcolor="#A4B6D7"> 
        <td width="30%" height="30" align="center" bgcolor="#A4B6D7"><strong>标签类别名称</strong></td>
        <td width="30%" height="30" align="center"><strong>操作选项</strong></td>
        <td width="30%" height="30" align="center"><strong>标签选项</strong></td>
        </tr>
    
        <tr bgcolor="#F2F8FF" class="tdbg"> 
        <td align=left width="233" height="22"><img src="../Images/tree_folder4.gif" width="15" height="15">标签报道</td>
        <td align="right" style="padding-right:10"><a href="QH_DY_CtAddSmallSingle.aspx?FLName=" + Server.UrlEncode(strCtName) + ""><font color="#FF0000">添加二级分类</font></a> 
          | <a href="QH_DY_CategoryModifyBig.aspx?ID=" + strID + "">修改</a> 
          | <a href="QH_DY_CategoryDelBig.aspx?ID=" + strID + "" onClick="return ConfirmDelBig();">删除</a> &nbsp;</td>
        <td align="right" style="padding-right:10"> <a href="QH_DY_CategoryDelBig.aspx?ID=" + strID + "" onClick="return ConfirmDelBig();">列表</a> &nbsp;</td>
        </tr>

        <tr bgcolor="#EAEAEA" class="tdbg">
          <td align=left width="233" height="22">&nbsp;&nbsp;<img src="../Images/tree_folder3.gif" width="15" height="15">标签报道A1</td>
          <td align="right" style="padding-right:10"><a href="QH_CtModifySmallNews.aspx?ID=" + ds_small.Tables[0].Rows[j]["BigClassID"] + "">修改</a> 
            | <a href="QH_CtDelSmallNews.aspx?ID=" + ds_small.Tables[0].Rows[j]["BigClassID"] + "" onClick="return ConfirmDelSmall();">删除</a> &nbsp;</td>
         <td align="right" style="padding-right:10"> <a href="QH_DY_CategoryModifyBig.aspx?ID=" + strID + "">添加</a> 
          | <a href="QH_DY_CategoryDelBig.aspx?ID=" + strID + "" onClick="return ConfirmDelBig();">列表</a> &nbsp;</td>
        </tr>
   -->
    </div>
    <br />
    <br />
    <div style="text-align: center">
        <a href="QH_RelateLinkAdd.aspx"><strong><font color="#FF0000"><u>添加热点关键词</u></font></strong></a></div>
    </form>
</body>
</html>
