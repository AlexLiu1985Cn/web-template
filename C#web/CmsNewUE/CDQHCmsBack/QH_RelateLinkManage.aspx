<%@ Page Language="C#" AutoEventWireup="true" Inherits="QH_RelateLinkManage" CodeBehind="QH_RelateLinkManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>����������վ����ϵͳ</title>
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
    alert("�������Ʋ���Ϊ�գ�");
    document.form1.BigClassName.focus();
    return false;
  }
}
function checkSmall()
{
  if (document.form2.BigClassName.value=="")
  {
    alert("������Ӵ������ƣ�");
	document.form1.BigClassName.focus();
	return false;
  }

  if (document.form2.SmallClassName.value=="")
  {
    alert("С�����Ʋ���Ϊ�գ�");
	document.form2.SmallClassName.focus();
	return false;
  }
}
*/
function ConfirmDelBig()
{
   if(confirm("ȷ��Ҫɾ�������������"))//ɾ���˴���ͬʱ��ɾ����������С��͸����µ����б�ǩ�����Ҳ��ָܻ���
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
                ��ǰλ�ã��������� >> <a href="QH_RelateLinkManage.aspx">�ȵ�ؼ��ʹ���</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
        </div>
    </div>
    <div>
        <!-- <div style="text-align:center"><a href="QH_News_AddBig.aspx" ><strong><font color="#FF0000"><u>��ӱ�ǩ���</u></font></strong></a></div> -->
        <br />
        <div style="text-align: center" id="CategoryTable" runat="server">
        </div>
    </div>
    <div style="text-align: center" id="NewsTypeTable" runat="server">
        <!--
        <table width="600" border="0" cellpadding="0" cellspacing="1" bgcolor="#000000">
        <tr bgcolor="#A4B6D7"> 
        <td width="30%" height="30" align="center" bgcolor="#A4B6D7"><strong>��ǩ�������</strong></td>
        <td width="30%" height="30" align="center"><strong>����ѡ��</strong></td>
        <td width="30%" height="30" align="center"><strong>��ǩѡ��</strong></td>
        </tr>
    
        <tr bgcolor="#F2F8FF" class="tdbg"> 
        <td align=left width="233" height="22"><img src="../Images/tree_folder4.gif" width="15" height="15">��ǩ����</td>
        <td align="right" style="padding-right:10"><a href="QH_DY_CtAddSmallSingle.aspx?FLName=" + Server.UrlEncode(strCtName) + ""><font color="#FF0000">��Ӷ�������</font></a> 
          | <a href="QH_DY_CategoryModifyBig.aspx?ID=" + strID + "">�޸�</a> 
          | <a href="QH_DY_CategoryDelBig.aspx?ID=" + strID + "" onClick="return ConfirmDelBig();">ɾ��</a> &nbsp;</td>
        <td align="right" style="padding-right:10"> <a href="QH_DY_CategoryDelBig.aspx?ID=" + strID + "" onClick="return ConfirmDelBig();">�б�</a> &nbsp;</td>
        </tr>

        <tr bgcolor="#EAEAEA" class="tdbg">
          <td align=left width="233" height="22">&nbsp;&nbsp;<img src="../Images/tree_folder3.gif" width="15" height="15">��ǩ����A1</td>
          <td align="right" style="padding-right:10"><a href="QH_CtModifySmallNews.aspx?ID=" + ds_small.Tables[0].Rows[j]["BigClassID"] + "">�޸�</a> 
            | <a href="QH_CtDelSmallNews.aspx?ID=" + ds_small.Tables[0].Rows[j]["BigClassID"] + "" onClick="return ConfirmDelSmall();">ɾ��</a> &nbsp;</td>
         <td align="right" style="padding-right:10"> <a href="QH_DY_CategoryModifyBig.aspx?ID=" + strID + "">���</a> 
          | <a href="QH_DY_CategoryDelBig.aspx?ID=" + strID + "" onClick="return ConfirmDelBig();">�б�</a> &nbsp;</td>
        </tr>
   -->
    </div>
    <br />
    <br />
    <div style="text-align: center">
        <a href="QH_RelateLinkAdd.aspx"><strong><font color="#FF0000"><u>����ȵ�ؼ���</u></font></strong></a></div>
    </form>
</body>
</html>
