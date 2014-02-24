<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_DefaultTmMobile.aspx.cs"
    Inherits="CmsApp20.CmsBack.QH_DefaultTmMobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <style>
        #table
        {
            float: none;
            width: 620px;
            border-left: #999999 1px solid;
            border-top: #999999 1px solid;
        }
        #table li
        {
            float: left;
            width: 620px;
            list-style: none outside;
            border-bottom: #999999 1px solid;
            border-right: #999999 1px solid;
            text-align: center;
        }
        #table div
        {
            float: left;
        }
        .left1
        {
            width: 300px;
            border-right: #999999 1px solid;
            padding-bottom: 4px;
            padding-top: 4px;
        }
        .left
        {
            width: 190px;
            border-right: #999999 1px solid;
            padding-bottom: 4px;
            padding-top: 4px;
            text-align: right;
            padding-right: 110px;
        }
        .right
        {
            width: 290px;
            padding-bottom: 4px;
            padding-top: 4px;
            text-align: left;
            padding-left: 10px;
        }
        .right1
        {
            width: 300px;
            padding-bottom: 4px;
            padding-top: 4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <br />
        <br />
        <br />
        <br />
        <div id="table">
            <li>
                <div class="left1" style="padding-bottom: 10px; padding-top: 10px">
                    页面类型</div>
                <div class="right1" style="padding-bottom: 10px; padding-top: 10px">
                    默认模板文件名</div>
            </li>
            <li>
                <div class="left">
                    首页</div>
                <div class="right">
                    Mobile_home.aspx</div>
            </li>
            <li>
                <div class="left">
                    简介模块</div>
                <div class="right">
                    Mobile_JianJie.aspx</div>
            </li>
            <li>
                <div class="left">
                    新闻模块</div>
                <div class="right">
                    Mobile_News.aspx</div>
            </li>
            <li>
                <div class="left">
                    产品模块</div>
                <div class="right">
                    Mobile_Product.aspx</div>
            </li>
            <li>
                <div class="left">
                    图片模块</div>
                <div class="right">
                    Mobile_Picture.aspx</div>
            </li>
            <li>
                <div class="left">
                    留言模块</div>
                <div class="right">
                    Mobile_Message.aspx</div>
            </li>
            <li>
                <div class="left">
                    新闻内容页</div>
                <div class="right">
                    Mobile_NewsDetails.aspx</div>
            </li>
            <li>
                <div class="left">
                    产品内容页</div>
                <div class="right">
                    Mobile_ProductDetails.aspx</div>
            </li>
            <li>
                <div class="left">
                    图片内容页</div>
                <div class="right">
                    Mobile_PictureDetails.aspx</div>
            </li>
            <li>
                <div class="left">
                    地图页</div>
                <div class="right">
                    Mobile_Map.aspx</div>
            </li>
        </div>
    </div>
    </form>
</body>
</html>
