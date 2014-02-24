$1=function(id) {return document .getElementById (id);}
String.prototype.Trim = function()
{
	return this.replace( /(^\s*)|(\s*$)/g, '' ) ;
}
var xmlhttp;
if (window.XMLHttpRequest)
      xmlhttp=new XMLHttpRequest();
else
      xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
if (xmlhttp===undefined)
{
  alert("不支持XMLHTTP.");
}
var AddHref;
function AddMenu1(order){
xmlhttp.onreadystatechange=null;
xmlhttp.open("GET","WeixinMenuAjax.aspx?Type=ClearTemp&Tm="+Math.random(),true);
xmlhttp.send(null);
AddHref= $1("Add1").href;
$1("Add1").disabled = true;
$1("Add1").removeAttribute('href');   
var oDiv=$1("Menu");
//var oAllP=oDiv.getElementsByTagName("p");
//var oPLast=oAllP[oAllP.length-1];
var p=document.createElement("p");
p.setAttribute("id","NewMenu1");
p.style.backgroundColor="#9ec0fe";
p.style.height="30px";
p.style.lineHeight="30px";
oDiv.appendChild(p);
p.innerHTML="<input name =\"TOrN\" id =\"TOrN\" type=\"text\" value="+order+" class=or  maxlength=3 />&nbsp;&nbsp;<input name=\"NTitle\" id=\"NTitle\" type=\"text\" class=or1 /><span  style=\"margin-left:40px;\"><a href =javascript:SaveN1()  >保存</a></span><span  style=\"margin-left:40px;\"><a href =javascript:CancelN1() >取消</a></span>";
}
function SetAutoMenu(title,link){
var oNew=$1("NTitle");
if(!oNew) return ;
oNew.value=title;
$1("pMenuURl").style.display="";
$1("SaveUrl").style.display="none";
$1("MenuURl").style.marginBottom="100px";
$1("pMenuText").style.display="none";
$1("pTuwen").style.display="none";
$1("MenuURl").value=link;
}
function CancelN1(){
var p=$1("NewMenu1");
if(p!=null ){
var parent=p.parentNode;//得到父节点
parent.removeChild (p);
$1("Add1").disabled = false;
$1("Add1").href=AddHref ;
$1("pMenuURl").style.display="none";
$1("pMenuText").style.display="none";
$1("pTuwen").style.display="none";
$1("MenuURl").value="";
$1("MenuText").value="";
$1("TuwenList").innerHTML="";
}
}
function SaveN1(){
var vOrder=$1("TOrN").value.Trim();
if(!/^\d+$/.test(vOrder)){alert("排序必须为数字。");return ;}
var vTitle=$1("NTitle").value.Trim();
if(vTitle==""){alert("菜单名称必须填写。");return ;}
var vtype=0;
var vVaule="";
var vMenuURL=$1("MenuURl").value.Trim();
var vMenuText=$1("MenuText").value.Trim();
var vMenuTuwen=$1("TuwenList").innerHTML.Trim();
if($1("pMenuURl").style.display!="none"&&vMenuURL!=""){vtype=1;vVaule=vMenuURL;}
if($1("pMenuText").style.display!="none"&&vMenuText!=""){vtype=2;vVaule=vMenuText;}
if($1("pTuwen").style.display!="none"&&vMenuTuwen!=""){vtype=3;}
xmlhttp.onreadystatechange=SetMenu;
xmlhttp.open("POST","WeixinMenuAjax.aspx?Type=SaveNewMenu&Tm="+Math.random(),true);
xmlhttp.setRequestHeader("Content-type","application/x-www-form-urlencoded;charset=utf-8");
var vPost="class=1&Order="+vOrder+"&Title="+encodeURIComponent(vTitle)+"&Type="+vtype+"&Content="+encodeURIComponent(vVaule);
xmlhttp.send(vPost);
}
function SetMenu(){
if (xmlhttp.readyState==4 && xmlhttp.status==200){
alert(xmlhttp.responseText);
}
}
function Jump(){
var p=$1("NewMenu1");
if(p!=null ){
$1("pMenuURl").style.display="";
$1("SaveUrl").style.display="none";
$1("MenuURl").style.marginBottom="100px";
$1("pMenuText").style.display="none";
$1("pTuwen").style.display="none";
}
}
function Text(){
var p=$1("NewMenu1");
if(p!=null ){
$1("pMenuURl").style.display="none";
$1("pMenuText").style.display="";
$1("SaveText").style.display="none";
$1("MenuText").style.marginBottom="100px";
$1("pTuwen").style.display="none";
}
}
function Tuwen(){
var p=$1("NewMenu1");
if(p!=null ){
$1("pMenuURl").style.display="none";
$1("pMenuText").style.display="none";
$1("pTuwen").style.display="";
}
}
function SetTuwen(){
$1("OrderN").value="";
$1("TitleN").value="";
$1("DescrptN").value="";
$1("NewTuwenSet").style.display="";
$1("NewTuwen").style.display="none";
var Spans=$1("TuwenList").getElementsByTagName("span");
for(var i=0;i<Spans.length;i++){
Spans[i].setAttribute("onclick","");
}
}
function SaveNewTW(){
var vOrder=$1("OrderN").value.Trim();
if(!/^\d+$/.test(vOrder)){alert("排序必须为数字。");return ;}
var vTitle=$1("TitleN").value.Trim();
if(vTitle==""){alert("题目必须填写。");return ;}
xmlhttp.onreadystatechange=SetNewTuwen;
xmlhttp.open("POST","WeixinMenuAjax.aspx?Type=SaveTemp&Tm="+Math.random(),true);
xmlhttp.setRequestHeader("Content-type","application/x-www-form-urlencoded;charset=utf-8");
var vPost="Order="+vOrder+"&Title="+encodeURIComponent(vTitle)+"&Descrpt="+encodeURIComponent($1("DescrptN").value.Trim())+"&PicUrl="+encodeURIComponent($1("PicUrlN").value.Trim())+"&Url="+encodeURIComponent($1("UrlN").value.Trim());
xmlhttp.send(vPost);
}
var vSpanId="";
var vAllTemp=[];
function SetNewTuwen(){
if (xmlhttp.readyState==4 && xmlhttp.status==200){
//alert(xmlhttp.responseText);
var vRTitle=xmlhttp.responseText;
if(vRTitle=="Wrong"){alert("保存错误！");return ;}
var aRtitle=vRTitle.split("|");
var titleHtml="";vSpanId="";
vAllTemp=aRtitle;
var vNum=(aRtitle.length-1)/6;
for(var i=0;i<vNum;i++){
var n=i*6;
titleHtml+="<span  id=\"SpanTW"+aRtitle[n]+"\" onclick=TWModify('"+aRtitle[n]+"',"+i+") >回复"+(i+1)+"："+aRtitle[n+2]+"<br></span>";
vSpanId+=aRtitle[n]+"|";
}
$1("TuwenList").innerHTML=titleHtml;
if(vNum<10) $1("NewTuwen").style.display="";
$1("NewTuwenSet").style.display="none";
}
}
function TWModify(id,i){
var objSpan=$1("SpanTW"+id);
var parent=objSpan.parentNode;//得到父节点
var span=document.createElement("span");
span.setAttribute("id","ModiTuwen"+id);
parent.insertBefore(span,objSpan);
objSpan.style.display="none";
//span.innerHTML="<div style=\"margin-left:-80px;\">"+$1("NewTuwenSet").innerHTML.replace("SaveNewTW()","ModifyNewTW('"+id+"')").replace("CancelNewTW()","CancelModifyTW('"+id+"')").replace("(id=",id+"\"").replace("CheckUp('N')","CheckUp('"+id+"')")+"</div>";
span.innerHTML="<div style=\"margin-left:-80px;\">"+vModifyTWHtml.replace(/\{id\}/g,id)+"</div>";
var n=i*6;
$1("Order"+id).value=vAllTemp[n+1];
$1("Title"+id).value=vAllTemp[n+2];
$1("Descrpt"+id).value=vAllTemp[n+3];
$1("PicUrl"+id).value=vAllTemp[n+4];
$1("Url"+id).value=vAllTemp[n+5];
$1("NewTuwen").style.display="none";
var Spans=parent.getElementsByTagName("span");
for(var i=0;i<Spans.length;i++){
Spans[i].setAttribute("onclick","");
}
}
function CancelNewTW(){
$1("NewTuwenSet").style.display="none";
$1("NewTuwen").style.display="";
var Spans=$1("TuwenList").getElementsByTagName("span");
var vID=vSpanId.split ('|');
for(var i=0;i<Spans.length;i++){
Spans[i].onclick=new Function ("TWModify('"+vID[i]+"',"+i+");");
}
}
function CancelModifyTW(id){
var objSpan=$1("SpanTW"+id);
var parent=objSpan.parentNode;//得到父节点
var objModifySpan=$1("ModiTuwen"+id);
parent.removeChild(objModifySpan);
objSpan.style.display="";
$1("NewTuwen").style.display="";
var Spans=parent.getElementsByTagName("span");
var vID=vSpanId.split ('|');
for(var i=0;i<Spans.length;i++){
Spans[i].onclick=new Function ("TWModify('"+vID[i]+"',"+i+");");
}
}
function ModifyNewTW(id){
var vOrder=$1("Order"+id).value.Trim();
if(!/^\d+$/.test(vOrder)){alert("排序必须为数字。");return ;}
var vTitle=$1("Title"+id).value.Trim();
if(vTitle==""){alert("题目必须填写。");return ;}
xmlhttp.onreadystatechange=SetNewTuwen;
xmlhttp.open("POST","WeixinMenuAjax.aspx?Type=ModifyTemp&id="+id+"&Tm="+Math.random(),true);
xmlhttp.setRequestHeader("Content-type","application/x-www-form-urlencoded;charset=utf-8");
var vPost="Order="+vOrder+"&Title="+encodeURIComponent(vTitle)+"&Descrpt="+encodeURIComponent($1("Descrpt"+id).value.Trim())+"&PicUrl="+encodeURIComponent($1("PicUrl"+id).value.Trim())+"&Url="+encodeURIComponent($1("Url"+id).value.Trim());
xmlhttp.send(vPost);
}

var vModifyTWHtml="\
<form action=\"\" enctype =\"multipart/form-data\" method =post target=UpFrame id=\"FormUp{id}\" > \
<table width =96% border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"2\"  >\
<tr><td style=\"width :20%;\" align=right>排序：</td>\
<td style=\"width :80%;\" align=left><input  name =\"Order{id}\" id=\"Order{id}\" type=\"text\" class =\"td40\" /></td>\
</tr><tr><tr><td align=right>题目：</td>\
<td align=left><input  name =\"Title{id}\" id=\"Title{id}\" type=\"text\" class =\"td80\" /></td>\
</tr><tr><td align=right>简短描述：</td>\
<td align=left><input  name =\"Descrpt{id}\" id=\"Descrpt{id}\" type=\"text\" class =\"td80\" /></td>\
</tr><tr><td align=right>图片地址：</td>\
<td align=left><input  name =\"PicUrl{id}\" id=\"PicUrl{id}\" type=\"text\" class =\"td240\"  />\
<input  type=\"file\" id=\"File{id}\" name=\"File{id}\" onchange=\"CheckUp('{id}')\" class=\"NB\" size=1 /><a href=\"#\">浏览上传</a>\
		<img src=\"images/ld.gif\"  style=\"display:none; width :22px;\" id=UpWt{id} />\
</td></tr><tr><td align=right>链接地址：</td>\
<td align=left><input  name =\"Url{id}\" id=\"Url{id}\" type=\"text\" class =\"td240\"  /></td>\
</tr><tr><td align=right>操作：</td>\
<td align=left><span style =\"margin-left :40px;\"><a href =javascript:ModifyNewTW('{id}')>保存</a></span><span style =\"margin-left :60px;\"><a href =javascript:CancelModifyTW('{id}')>取消</a></span></td>\
</tr></table>";