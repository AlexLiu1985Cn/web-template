<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainFrameNew.aspx.cs" Inherits="CmsApp20.CalliBack.MainFrameNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="images/favicon.ico" mce_href="images/favicon.ico" type="image/x-icon">
    <title>创都启航企业网站管理系统</title>
    <!-- Power by 创都启航企业网站管理系统 http://www.95c.com.cn -->
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
</head>
<frameset rows="55,*,5" cols="*" frameborder="no" border="0" framespacing="0">
  <frame src="top.aspx" name="topFrame" border="0" frameborder="no" scrolling="No" noresize="noresize" id="topFrame" title="topFrame" runat=server />
  <frameset cols="230,*,5" frameborder="no" border="0" framespacing="0" >
    <frame src="Left_SystemSet.aspx" name="menu-frame" scrolling="No" noresize="noresize" id="menu-frame" title="leftFrame" />
    <frameset rows ="0,*" frameborder="no" border="0" framespacing="0" >
        <frame src="" name="Nav-frame" id="Nav-frame" title="NavFrame"  noresize="noresize"  />
        <frame src="QH_SystemInfoNew.aspx" name="main-frame" id="main-frame" title="mainFrame"  />
    </frameset>
    <frame src="QH_right.html" name="right-frame" id="right-frame" title="rightFrame"    />
    </frameset>
    <frame src="QH_footframe.html" name="footFrame" border="0" frameborder="no" scrolling="No" noresize="noresize" id="footFrame" title="footFrame" />
</frameset>
<noframes>
    <body>
    </body>
</noframes>
</html>
