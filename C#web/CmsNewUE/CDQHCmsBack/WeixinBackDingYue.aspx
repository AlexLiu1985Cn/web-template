<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeixinBackDingYue.aspx.cs"
    Inherits="CmsApp20.CDQHCmsBack.WeixinBackDingYue" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>自动回复设置</title>
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <script type="text/javascript" src="js/Weixin.js"></script>
    <style type="text/css">
        body
        {
            font-family: Microsoft Yahei;
            font-size: 12px;
        }
        .tips
        {
            color: #999999;
        }
        .ret
        {
            margin-left: 160px;
        }
        .td60
        {
            width: 36px;
        }
        .td80
        {
            width: 76px;
        }
        .td100
        {
            width: 96px;
        }
        .td240
        {
            width: 206px;
        }
        .td220
        {
            width: 206px;
        }
        .NB
        {
            position: absolute;
            z-index: 100;
            margin-left: -80px;
            font-size: 18px;
            opacity: 0;
            filter: alpha(opacity=0);
            margin-top: -5px;
            cursor: hand;
        }
        .BtnHide
        {
            display: none;
        }
    </style>
    <script type="text/javascript" src="js/NavFrame.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：微信管理 >> <a href="WeixinBackDingYue.aspx">自动回复设置</a></div>
            <div class="rightmain1">
                <div class="rightmain1">
                </div>
            </div>
        </div>
    </div>
    <div style="text-align: center; line-height: 30px;">
        <table cellpadding="2" cellspacing="6" width="840px" align="center">
            <tr>
                <td colspan="3">
                    <asp:Button ID="BtnAuto" runat="server" Text=" 自动生成 " OnClick="BtnAuto_Click" />
                    <asp:Button ID="BtnCancel" runat="server" Text="取消自动生成" CssClass="ret" OnClick="BtnCancel_Click" />
                    <asp:Button ID="BtnsaveAuto" runat="server" Text="保存自动生成" CssClass="ret" OnClick="BtnsaveAuto_Click"
                        OnClientClick="return AutoSave();" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td width="100px" align="right">
                    关注欢迎词：
                </td>
                <td align="left">
                    <textarea name="eventWelcom" cols="70" rows="4"><%=strEventWelcom%></textarea>
                </td>
                <td align="left">
                    <asp:Button ID="BtnWelcomSave" runat="server" Text=" 保 存 " OnClick="BtnWelcomSave_Click" />
                </td>
            </tr>
            <tr>
                <td width="100px" align="right">
                    缺省自动回复：
                </td>
                <td align="left">
                    <textarea name="DefaultBack" cols="70" rows="4"><%=strDefaultBack%></textarea>
                </td>
                <td align="left">
                    <asp:Button ID="BtnDefault" runat="server" Text=" 保 存 " OnClick="BtnDefault_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="BtnNews" runat="server" Text=" 更新新闻 " OnClick="BtnNews_Click" OnClientClick="return RefreshNews();" />
                    <asp:Button ID="BtnProduct" runat="server" Text=" 更新产品 " CssClass="ret" OnClick="BtnProduct_Click"
                        OnClientClick="return RefreshProduct();" />
                    <asp:Button ID="BtnPicture" runat="server" Text=" 更新图片 " OnClick="BtnPicture_Click"
                        OnClientClick="return RefreshPicture();" CssClass="ret" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HdnAutoSave" runat="server" />
    <asp:HiddenField ID="HdnInfo" runat="server" />
    <asp:Button ID="BtnSave1" runat="server" Text="Button" CssClass="BtnHide" OnClick="BtnSave1_Click" />
    <asp:Button ID="BtnAdd1" runat="server" Text="Button" CssClass="BtnHide" OnClick="BtnAdd1_Click" />
    </form>
    <table width="860px" border="0" align="center" cellpadding="0" cellspacing="2" bordercolor="#FFFFFF"
        id="table1">
        <thead style="background: #ECECEC; font-weight: bold; text-align: left; height: 23px;">
            <tr style="background: url(images/1.jpg) repeat-x; height: 30px; color: #FFFFFF;
                font-weight: bold;">
                <td width="20" style="background: White;">
                </td>
                <td width="38" align="center">
                    有效
                </td>
                <td width="40">
                    关键字
                </td>
                <td width="80">
                    题目
                </td>
                <td width="100">
                    简短描述
                </td>
                <td width="282">
                    图片地址
                </td>
                <td width="220">
                    链接地址
                </td>
                <td width="80">
                    操作
                </td>
            </tr>
        </thead>
        <%--   <form action="" enctype ="multipart/form-data" method =post target=UpFrame id=FormUp0 >
     <tr bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
        <td><input  name ="key0" id="key0" type="text"  class ="td60" /></td>
        <td><input  name ="Title0" id="Title0" type="text" class ="td80" /></td>
        <td><input  name ="Descrpt0" id="Descrpt0" type="text" class ="td100"  /></td>
		<td><input  name ="PicUrl0" id="PicUrl0" type="text" class ="td240"  />
		<input  type="file" id="File0" name="File0" onchange=CheckUp(0) class="NB" size=1 /><a href="#">浏览上传</a>
		<img src="images/ld.gif" class="Wt" style="display:none;" id=UpWt0 /></td>
		<td><input  name ="Url0" id="Url0" type="text" class ="td220"  /></td>
        <td><a href =javascript:Save(0)>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:Del(0)>删除</a></td>
	</tr>
    </form>
     <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
        <td><input  name ="key1" id="key1" type="text"  class ="td60" /></td>
        <td colspan =4><textarea name="Text1" id="Text1" cols="70" rows="3" ></textarea></td>
        <td><a href =javascript:Save(0)>保存</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:Del(0)>删除</a></td>
	 </tr>
        --%>
        <%=strTr %>
        <tr id="Add">
            <td>
            </td>
            <td colspan="3" align="left">
                <a href="javascript:Addnews();">+添加图文回复</a>
            </td>
            <td colspan="3" align="left">
                <a href="javascript:Addtext();">+添加文本回复</a>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <%=strJsHide %>
    <iframe name="UpFrame" style="display: none;"></iframe>
</body>
</html>
