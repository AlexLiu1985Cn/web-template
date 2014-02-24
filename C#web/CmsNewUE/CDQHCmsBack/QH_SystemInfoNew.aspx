<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_SystemInfoNew.aspx.cs"
    Inherits="CmsApp20.CmsBack.QH_SystemInfoNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <title>创都启航企业网站管理系统</title>
    <!-- Power by 创都启航企业网站管理系统 http://www.95c.com.cn -->
    <script type="text/javascript" src="js/NavFrame.js"></script>
</head>
<body>
    <div class="systeminfo">
        <div class="systeminfoleft">
            <div class="systeminfoleft1">
                <div class="systeminfotitle">
                    &nbsp;&nbsp;欢迎使用创都启航创都启航企业网站管理系统</div>
                <div class="systeminfocontent">
                    <ul>
                        <li>当前版本号：</li>
                        <li class="red">
                            <%=strVersion %></li>
                        <li>最新版本：</li>
                        <li class="red">
                            <%=strHighestVersion %></li>
                    </ul>
                </div>
                <div class="systeminfotitle">
                    &nbsp;&nbsp;系统统计信息</div>
                <div class="systeminfocontent">
                    <ul>
                        <li>新闻统计：</li>
                        <li class="red">
                            <%=strNews %></li>
                        <li>产品统计：</li>
                        <li class="red">
                            <%=strProduct %></li>
                        <li>留言统计：</li>
                        <li class="red">
                            <%=strMessage %></li>
                        <li>下载统计：</li>
                        <li class="red">
                            <%=strDownload%></li>
                    </ul>
                </div>
                <div class="systeminfotitle">
                    &nbsp;&nbsp;服务器端信息</div>
                <div class="systeminfocontent">
                    <ul>
                        <li>ip地址：</li>
                        <li>
                            <%=strServerIP%></li>
                        <li>操作系统：</li>
                        <li>
                            <%=strServerOS%></li>
                        <li>NET CLR 版本：</li>
                        <li>
                            <%=strServerNetVer%></li>
                        <li>&nbsp;</li>
                        <li>&nbsp;</li>
                        <li>创建目录权限：</li>
                        <li>
                            <%=strDirRight%></li>
                        <li>水印权限：</li>
                        <li>√</li>
                    </ul>
                </div>
                <div class="systeminfotitle">
                    &nbsp;&nbsp;客户端信息</div>
                <div class="systeminfocontent">
                    <ul>
                        <li>ip地址：</li>
                        <li>
                            <%=strIP%></li>
                        <li>操作系统：</li>
                        <li>
                            <%=strOPSys%></li>
                        <li>浏览器</li>
                        <li>
                            <%=strBrowser%></li>
                    </ul>
                </div>
                <div class="systeminfotitle">
                    &nbsp;&nbsp;系统登录日志信息
                </div>
                <div class="systeminfocontent">
                    <ul>
                        <asp:Repeater ID="RP_Log" runat="server" OnItemDataBound="RP_Log_ItemDataBound">
                            <ItemTemplate>
                                <li>
                                    <%# Eval("Name")%></li>
                                <li>
                                    <%# Eval("ClientIP")%></li>
                                <li>
                                    <%# Eval("LoginTime")%></li>
                                <li><span id="State" runat="server"><font color="green">
                                    <%# Eval("State")%></font></span></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="systeminforight">
            <iframe src="ClientNewsFrom95c.aspx" frameborder="0" scrolling="no" height="260">
            </iframe>
            <div class="systeminforight1">
                <div class="systeminfotitle">
                    &nbsp;&nbsp;服务与技术支持</div>
                <div class="sservice">
                    <p>
                        欢迎使用创都启航创都启航企业网站管理系统！</p>
                    <p>
                        &nbsp;&nbsp;<a href="http://www.95c.com.cn" target="_blank">创都启航企业管理系统官方网站</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a
                            href="http://bbs.95c.com.cn" target="_blank">技术论坛</a></p>
                    <p>
                        &nbsp;&nbsp;<a href="http://www.95c.com.cn/HelpCenter/" target="_blank">系统帮助说明</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a
                            href="http://www.95c.com.cn/templateWeb/ChargeStyle/" target="_blank">在线模板中心</a></p>
                    <p>
                        &nbsp;&nbsp;<a href="http://vip.95c.com.cn" target="_blank">VIP服务平台</a>&nbsp;&nbsp;&nbsp;&nbsp;<a
                            href="http://www.95c.com.cn/Services/Authorize/" target="_blank">商业授权</a>&nbsp;&nbsp;&nbsp;&nbsp;<a
                                href="http://demo.95c.com.cn/" target="_blank">演示网址</a></p>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
</body>
</html>
