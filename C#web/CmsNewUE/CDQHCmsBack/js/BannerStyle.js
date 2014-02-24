$=function(id) {return document .getElementById (id);}
function BnrSel(obj,Num){
var parent=obj.parentNode;//得到父节点
old=$("SelS"+Num);
old1=$("SpanTE"+Num);
if(obj.selectedIndex!=1){
if(old!=null)
parent.removeChild(old);
if(old1!=null)
parent.removeChild(old1);
return ;
}
AddSelS(parent,Num,0);
}
function AddSelS(parent,Num,Sel){
var Input=document.createElement("Select");
Input.setAttribute("id","SelS"+Num);
Input.setAttribute("name","SelS"+Num);
Input.onchange=function(){ClearTM(Num);};
for(var i=1;i<=5;i++){
if(i==4||i==5){
Input.options.add(new Option("样式"+i+"(全屏)",i));continue ;}
Input.options.add(new Option("样式"+i,i));
}
Input.selectedIndex=Sel-1; 
parent.appendChild(Input);

var vSpan=document.createElement("Span");
vSpan.setAttribute("id","SpanTE"+Num);
vSpan.innerHTML="&nbsp;&nbsp;切换时间：<input name =\"TM"+Num+"\" id =\"TM"+Num+"\" type=\"text\" class=orTM  maxlength=3 />秒 \
                 &nbsp;&nbsp;效果编号：<input name =\"EF"+Num+"\" id =\"EF"+Num+"\" type=\"text\" class=orTM  maxlength=3 />";
parent.appendChild(vSpan);
}
function CkAll(obj){
var nN=$("HdSaveID").value.split('|');
for(var i=1;i<=nN.length;i++)
$("Ck"+i).checked=obj.checked;
}
function CheckWH(){
var reg1=/^[0-9]+$/;
var reg2=/^$|^[0-9]+$/;
var nN=$("HdSaveID").value.split('|');
if(!(reg1.test($("Width0").value))||!(reg1.test($("Height0").value))){
alert("默认栏目Banner的宽高必须为数字！");return false;
}
if(!(reg1.test($("Width1").value))||!(reg1.test($("Height1").value))){
alert("网站首页Banner的宽高必须为数字！");return false;
}
if(!(reg2.test($("TM0").value))||!(reg2.test($("EF0").value))){
alert("默认栏目Banner的切换时间和效果编号必须为数字！");return false;
}
if(!(reg2.test($("TM1").value))||!(reg2.test($("EF1").value))){
alert("网站首页Banner的切换时间和效果编号必须为数字！");return false;
}
for(var i=2;i<=nN.length+1;i++)
if($("Ck"+i).checked==false){
 if(!(reg1.test($("Width"+i).value))||!(reg1.test($("Height"+i).value))){
alert("Banner的宽高必须为数字！");return false;}
 if(!(reg2.test($("TM"+i).value))||!(reg2.test($("EF"+i).value))){
alert("Banner的切换时间和效果编号必须为数字！");return false;}
}
return true;
}
function ClearTM(Num){
$("TM"+Num).value="";
$("EF"+Num).value="";
}
