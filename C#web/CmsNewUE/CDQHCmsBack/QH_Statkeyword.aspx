<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QH_Statkeyword.aspx.cs"
    Inherits="CmsApp20.CmsBack.QH_Statkeyword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
    <style type="text/css">
        body
        {
            font-family: Microsoft Yahei;
            font-size: 12px;
        }
        h4
        {
            margin-left: 160px;
            background: #F2F2F2;
            width: 840px;
        }
        .form
        {
            border-top: 1px solid #ccc;
            height: 24px;
            background-color: #f4f4f4;
            line-height: 20px;
            margin-left: 10px;
        }
    </style>
</head>
<script type="text/javascript">
    $ = function (id) { return document.getElementById(id); }
    function StatDate(obj) {
        $("HdnV").value = obj.value;
        //alert($("HdnV").value);
        $("Btndate").click();
    }
    function SetDisp(vDate) {
        var vHdn = $("HdnV").value;
        if (vHdn.length > 1 || vHdn == "8")
            for (var i = 1; i < 8; i++)
                $("d" + i).checked = false;
        if (vHdn.length == 1) {
            $("DS_Date").value = vDate;
            $("d" + vHdn).checked = true;
        }
        else $("DS_Date").value = vHdn;
    }
    function CheckDate() {
        var vIndate = $('DS_Date').value;
        var reg = /^(?:(?!0000)[0-9]{4}([-]?)(?:(?:0?[1-9]|1[0-2])([-]?)(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])([-]?)(?:29|30)|(?:0?[13578]|1[02])([-]?)31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)([-]?)0?2([-]?)29)$/;
        if (!reg.test(vIndate)) { alert("日期格式不对！"); return; }
        $("HdnV").value = vIndate;
        $("Btndate").click();
    }
</script>
<body>
    <form id="form1" runat="server">
    <%--       <div >
					<h3>关键词分析：</h3>
				</div>
    --%>
    <div class="glrihgt">
        <div class="glrihgtnei">
            <div class="rightmain">
                当前位置：访问统计 >> <a href="QH_Statkeyword.aspx">关键词分析</a></div>
            <div class="rightmain1">
            </div>
            <div class="rightmain1">
            </div>
        </div>
    </div>
    <table width="840px" align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
        <tr>
            <td height="30" bgcolor="#F2F2F2" style="z-index: 5;">
                <input type="hidden" name="unit_id" value="3038204">
                <input type="hidden" name="type" value="day">
                <strong>选择时段 </strong>
                <script type="text/javascript" src="js/DateSel.js"></script>
                <input type="radio" name="rapid" checked onclick="StatDate(this)" value="1" id="d1" />今天
                <input type="radio" name="rapid" onclick="StatDate(this)" value="2" id="d2">昨天
                <input type="radio" name="rapid" onclick="StatDate(this)" value="3" id="d3" />前天
                <%--                              <script type="text/javascript" language=javascript>
                                   var myDate1=new dateSelector();
                                  myDate1.inputName='Date';  //注意这里设置输入框的name，同一页中日期输入框，不能出现重复的name。
                                myDate1.display();
                            </script>--%>
                <input style='text-align: center; width: 80px;' id='DS_Date' name='Date' value='<%=DateTime.Now.ToString("yyyy-MM-dd") %>'>
                <button style='width: 60px; height: 18px; font-size: 12px; margin: 1px; border: 1px solid #A4B3C8;
                    background-color: #DFE7EF;' type="button" onclick="CheckDate()">
                    选择日期</button>
                <input type="radio" name="rapid" onclick="StatDate(this)" value="4" id="d4" />本周
                <input type="radio" name="rapid" onclick="StatDate(this)" value="5" id="d5" />上周
                <input type="radio" name="rapid" onclick="StatDate(this)" value="6" id="d6" />本月
                <input type="radio" name="rapid" onclick="StatDate(this)" value="7" id="d7" />最近30天
                任意月
                <asp:DropDownList ID="year" runat="server" AutoPostBack="true" OnSelectedIndexChanged="year_SelectedIndexChanged">
                </asp:DropDownList>
                <select name="month" id="month" runat="server">
                </select>
                <asp:Button ID="BtnOK" runat="server" Text="确认" OnClick="BtnOK_Click" OnClientClick="$('HdnV').value='8';" /><asp:HiddenField
                    ID="HdnV" runat="server" />
            </td>
        </tr>
    </table>
    <table width="840px" align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
        <tr>
            <td height="30" style="z-index: 4">
                <strong>时段：</strong><%=strBegin %>
                -
                <%=strEnd %>
            </td>
        </tr>
    </table>
    <table width="840px" align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
        <tr>
            <td style="position: relative; z-index: 0; text-align: center;">
                <asp:Literal ID="FCLiteral" runat="server" EnableViewState="false"></asp:Literal>
            </td>
        </tr>
    </table>
    <div style="text-align: center;" id="table1" runat="server">
        <table width="800" border="0" align="center" cellpadding="2" cellspacing="2" bordercolor="#FFFFFF">
            <thead>
                <tr style="background: url(images/1.jpg) repeat-x; height: 30px; color: #FFFFFF;
                    font-weight: bold;">
                    <th>
                        关键词
                    </th>
                    <th>
                        入口PV
                    </th>
                    <th>
                        百分比
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="REList" runat="server" EnableViewState="false">
                    <ItemTemplate>
                        <tr bgcolor="#e6e6e6" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                            <td>
                                <%#((string[])Container.DataItem)[0]%>
                            </td>
                            <td>
                                <%#((string[])Container.DataItem)[1]%>
                            </td>
                            <td>
                                <%#((string[])Container.DataItem)[2]%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr bgcolor="#f9f9f9" onmouseover="this.style.backgroundColor='#9ec0fe'" onmouseout="this.style.backgroundColor=''">
                            <td>
                                <%#((string[])Container.DataItem)[0]%>
                            </td>
                            <td>
                                <%#((string[])Container.DataItem)[1]%>
                            </td>
                            <td>
                                <%#((string[])Container.DataItem)[2]%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div style="display: none;">
        <asp:Button ID="Btndate" runat="server" Text="" OnClick="Btndate_Click" /></div>
    </form>
</body>
</html>
