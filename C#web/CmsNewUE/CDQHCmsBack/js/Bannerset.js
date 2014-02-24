$=function(id) {return document .getElementById (id);}
String.prototype.Trim = function()
{
	return this.replace( /(^\s*)|(\s*$)/g, '' ) ;
}
function CheckOr(){
if(!/^[0-9]+$/.test($("no_order").value.Trim()))
{
alert("排序必须为数字！");return false;
}
var ANotChk=true;
var input = document.getElementsByTagName("input");
 for(var i = 0 ; i < input.length; i++){
  if(input[i].type=="checkbox"&&input[i].id!="all"&&input[i].checked==true ){ ANotChk=false; break;}; 
}
if(ANotChk){alert("请至少选择一个所属栏目。");return false; }
//if($("BnrMode").selectedIndex==0){
//if($("Img").value==""){alert("请选择上传图片！");return false; }
//}
//else if($("Flash").value==""){alert("请选择上传Flash！");return false; }
return true;
}
function CheckOrMdf(){
if(!/^[0-9]+$/.test($("no_order").value.Trim()))
{
alert("排序必须为数字！");return false;
}
var ANotChk=true;
var input = document.getElementsByTagName("input");
 for(var i = 0 ; i < input.length; i++){
  if(input[i].type=="checkbox"&&input[i].id!="all"&&input[i].checked==true ){ ANotChk=false; break;}; 
}
if(ANotChk){alert("请至少选择一个所属栏目。");return false; }
return true;
}
function CheckFlash(){
    var file=$("Flash");
    var fileName=file.value;
    if(fileName=="")
     return 1;
    if(fileName.indexOf(':')<1 )
    {
      alert("请选择正确的图片文件。");
      file.outerHTML=file.outerHTML;
     return 1;
     }
    var exName=fileName.substr(fileName.lastIndexOf(".")+1).toUpperCase();
    if(exName=="SWF")
    {
    }
    else
    {
      alert("请选择正确的Flash文件.swf");
      file.outerHTML=file.outerHTML;
     return 1;
    }
     return 0;
}
function ModeChange(){
var sel=$("BnrMode").selectedIndex;
if(sel==0){
$("title").style.display="";
$("Img1").style.display="";
$("LinkUrl").style.display="";
$("Flsh").style.display="none";
}
else{
$("title").style.display="none";
$("Img1").style.display="none";
$("LinkUrl").style.display="none";
$("Flsh").style.display="";
}
loadXMLDoc();
BannerClmnDsp(sel);
}
function AllSel(){
var ChBox=document.getElementsByTagName("input");
var chk=$("all").checked;
for(var i=0;i<ChBox.length;i++)
  if(ChBox[i].type=="checkbox") ChBox[i].checked=chk;
$("default").checked=false ; 
}
function BindCheckbox(){
 var vDefault=$("default");
var input = document.getElementsByTagName("input");
 for(var i = 0 ; i < input.length; i++){
  if(input[i].type=="checkbox"&&input[i].id!="all"&&input[i].id!="default") 
  (function(index){
    input[index].onclick = function(){
      if(this.checked) vDefault.checked=false ;
      if(!this.checked) $("all").checked=false ;
      else if(AllCheck()) $("all").checked=true;
      } 
   })(i)
 }
 vDefault.onclick = function(){
      if(this.checked) { for(var i = 0 ; i < input.length; i++)
        if(input[i].type=="checkbox"&&input[i].id!="default") 
        input[i].checked=false ;  
      }
   } 
}
function AllCheck(){
var AChk=true;
var input = document.getElementsByTagName("input");
 for(var i = 0 ; i < input.length; i++){
  if(input[i].type=="checkbox"&&input[i].id!="all"&&input[i].id!="default"&&input[i].checked==false ){ AChk=false; break;}; 
}
return AChk ;
}
window.onload = function(){	BindCheckbox();}
function BannerClmnDsp(Mode){
if (xmlhttp!=null){
          xmlhttp.onreadystatechange=state_ChangeBnr;
          xmlhttp.open("GET","../Ajax/AjaxBannerClmn.aspx?Mode="+Mode+"&Tm="+Math.random(),true);
          xmlhttp.send(null);
}
}
function state_ChangeBnr()
{
    if (xmlhttp.readyState==4)
    {
      if (xmlhttp.status==200)
        {
           //alert(xmlhttp.responseText);
           AddBannerClmn();
        }
      else
        {
            alert("wrong!");
        }
    }
}
function AddBannerClmn(){
var strHtml="";
var rows=xmlhttp.responseText.split('|');
var strTitle="<div class=\"Column\" > \
         <div class =\"Ctitle\" ><input name=\"all\" id=\"all\" type=\"checkbox\" class=\"checkbox\" value=\"a\" onclick=AllSel() > 所有栏目</div> \
         <div class=\"list\"> \
         <p><input name=\"default\" id=\"default\" type=\"checkbox\" class=\"checkbox\" value=\"D\"> 默认栏目</p>";
var r0Clmn= rows[0].split(',');        
if(r0Clmn[0]=="1")
 strHtml=strTitle +"<p><input name=\"Home\" type=\"checkbox\" class=\"checkbox\" value=\"0\"> 网站首页</p>";
var reg=/[13]/; 
if(strHtml==""&&rows.length>1)
 strHtml=strTitle ;
if(r0Clmn[1]=="1") reg=/2/; 
var nRNum=0;//{id,ColumnName,BnrMode,margin| }
for(var i=1;i<rows.length;i++)
{
  var rClmn=rows[i].split(',');
  if (!reg.test(rClmn[2]))
    strHtml+="<p style=\"margin-left :" + ( parseInt(rClmn[3])+24)  + "px\"> " + rClmn[1] + "</p>";
  else
    strHtml+="<p style=\"margin-left :" + rClmn[3] + "px\"><input name=\"Chk_" + nRNum + "\" id=\"Chk_" + nRNum++ + "\" type=\"checkbox\" class=\"checkbox\" value=\"" + rClmn[0] + "\"> " + rClmn[1] + "</p>";
}
$("HdnNum").value=nRNum;
var vNo="<font color=red>暂时没有设置为{0}的栏目，请设置后再编辑</font>";
if(strHtml=="")
 if(r0Clmn[0]=="0") strHtml=vNo.replace("{0}","图片");
 else strHtml=vNo.replace("{0}","Flash");
else strHtml+="</div></div>";
$("clmnDsp").innerHTML=strHtml;
BindCheckbox();
}