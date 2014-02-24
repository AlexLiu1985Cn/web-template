<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapSet.aspx.cs" Inherits="CmsApp20.CDQHCmsBack.MapSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置地图</title>
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
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
        #allmap
        {
            width: 600px;
            height: 300px;
            overflow: hidden;
            margin: 0 auto;
        }
    </style>
</head>
<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=PYAQmsU6cHwnEdBax3c8BupF"></script>
<body>
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：地图设置 >> <a href="MapSet.aspx">设置动态地图</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
            <form id="form1" runat="server">
            <div style="text-align: center;">
                <input id="Address" type="text" maxlength="255" runat="server" size="40" />
                <asp:Button ID="BtnSearch" runat="server" Text="搜索地址" OnClick="BtnSearch_Click" /><span
                    style="margin-left: 30px;">城市（可选）：<input id="City" type="text" maxlength="20" runat="server"
                        size="10" /></span>
                <div style="line-height: 13px;">
                    &nbsp;</div>
                <div id="allmap">
                </div>
                <div style="line-height: 13px;">
                    &nbsp;</div>
                标注点显示信息：<textarea name="info" id="info" cols="40" rows="4" runat="server"></textarea>
                <div style="line-height: 13px;">
                    &nbsp;</div>
                <asp:Button ID="BtnSave" runat="server" Text=" 保存 " OnClick="BtnSave_Click" />
                <div style="line-height: 24px;">
                    &nbsp;</div>
                <div id="Msg" runat="server">
                </div>
                <asp:Button ID="BtnCreate" runat="server" Text=" 生成动态地图页 " OnClick="BtnCreate_Click" />
            </div>
            <script type="text/javascript">
                function GenerateMap(lng, lat, info) {
                    var map = new BMap.Map("allmap");
                    map.centerAndZoom(new BMap.Point(lng, lat), 18);
                    var marker1 = new BMap.Marker(new BMap.Point(lng, lat));  // 创建标注
                    map.addOverlay(marker1);              // 将标注添加到地图中
                    //marker1.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
                    map.enableScrollWheelZoom();
                    //创建信息窗口
                    var infoWindow1 = new BMap.InfoWindow(info);
                    marker1.addEventListener("click", function () { this.openInfoWindow(infoWindow1); });
                }
            </script>
            <div>
            </div>
            </form>
</body>
</html>
