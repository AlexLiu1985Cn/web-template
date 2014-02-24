<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateImport.aspx.cs" Inherits="CmsApp20.CmsBack.TemplateImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>创都启航网站管理系统</title>
    <style >
    .pack{margin-left:270px;width:100px;}
     .Emp{display:none ;}
   </style>
 <link rel="stylesheet" href="css/cdqh.css" type="text/css" />
</head>
<script>
var $ = function (id) {	return "string" == typeof id ? document.getElementById(id) : id;};
function load(){
//alert ($("Continue").value);
if($("Continue").value=="1")
$("BtnEmp").click();
else if($("Continue").value=="2")
$("BtnDel").click();
}
function CheckName(id){
var vfName=$(id).value;
vfName=vfName.substring(vfName.lastIndexOf('.')+1).toLowerCase();
if(vfName!="cdqh")
{
alert("上传文件必须为.cdqh文件。");
return false ;
}
return true ;
}
</script>
<body onload=load() >
    <form id="form1" runat="server">
    <div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：模板管理 >> <a href="TemplateImport.aspx">导入模板</a></div>
<div class="rightmain1">二级位置：导入模板 - <a href="TemplateImportFTP.aspx">FTP上传模板导入</a></div>
<div class="rightmain1"> </div>
</div>
</div>

    <div style ="margin:200px 10px 10px 200px;">
        <br />
    请选择下载的模板文件：<input id="File1" type="file"  style="width:470px;" runat=server /><br /><br /><br />
        <asp:Button ID="BtnImport" runat="server" Text="将模板导入" CssClass =pack onclick="BtnImport_Click" OnClientClick ="return CheckName('File1');"
             /><br /><br />
         <asp:Button ID="BtnEmp" runat="server" Text="Button" onclick="BtnEmp_Click"  CssClass=Emp />
        <asp:Button  ID="BtnDel" runat="server" Text="Button" onclick="BtnDel_Click"  CssClass="Emp" /><asp:HiddenField
            ID="Continue" runat="server"  Value=0 />
    </div>
    </form>
</body>

</html>
