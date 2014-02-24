$=function(id) {return document .getElementById (id);}
String.prototype.Trim = function()
{
	return this.replace( /(^\s*)|(\s*$)/g, '' ) ;
}
//var Kdalert=false;
function checklength(obj,max) {
        if(obj.value.length > max) {
            //Kdalert=true;
            //alert("请不要超过最大长度:" + max);
            obj.value=obj.value.substring(0,(max-3))+"...";
        }
    }
function maxtext(x,y){ 
  //if(Kdalert) {Kdalert=false; return  ;}
 tempstr = x.value;
     if(tempstr.length>y){ 
          //alert("请不要超过最大长度:" + y);
          x.value = tempstr.substring(0,y-3)+"...";
      } 
}
function Check(){
    CheckImg("upload1");
} 
function CheckImg(id){
    var file=$(id);
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
    if(exName=="JPG"||exName=="BMP"||exName=="GIF"||exName=="PNG")
    {
    }
    else
    {
      alert("请选择正确的图片文件.jpg .gif .png");
      file.outerHTML=file.outerHTML;
     return 1;
    }
     return 0;
}
function Checkf(){
    var file=$("FileAnnex");
    var fileName=file.value;
    if(fileName=="")
     return 1;
    if(fileName.indexOf(':')<1 )
    {
      alert("请选择正确的文件");
      file.outerHTML=file.outerHTML;
     return 1;
     }
     return 0;
} 
function CheckMdb(){
    var file=$("MdbFile");
    var fileName=file.value;
    if(fileName=="")
     return 1;
    if(fileName.indexOf(':')<1 )
    {
      alert("请选择正确的文件");
      file.outerHTML=file.outerHTML;
     return 1;
     }
      var exName=fileName.substr(fileName.lastIndexOf(".")+1).toUpperCase();
    if(!(exName=="MDB"))
    {
      alert("请选择正确的数据库文件.mdb");
      file.outerHTML=file.outerHTML;
     return 1;
    }
   return 0;
} 
function CheckFavicon(obj){
    var fileName=obj.value;
    if(fileName=="")
     return 1;
    if(fileName.indexOf(':')<1 )
    {
      alert("请选择正确的文件");
      obj.outerHTML=obj.outerHTML;
     return 1;
     }
      var exName=fileName.substr(fileName.lastIndexOf(".")+1).toUpperCase();
    if(!(exName=="ICO"))
    {
      alert("请选择正确的图标文件.ico");
      obj.outerHTML=obj.outerHTML;
     return 1;
    }
   return 0;
}
function CheckDateHits(){
var Ad=$("addtime").value.Trim();
var Md=$("updatetime").value.Trim();
var Reg=/^(?:(?!0000)[0-9]{4}([-]?)(?:(?:0?[1-9]|1[0-2])([-]?)(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])([-]?)(?:29|30)|(?:0?[13578]|1[02])([-]?)31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)([-]?)0?2([-]?)29) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$/;
if(Ad==""){alert("发布日期不能为空！");return false ;}
if(!Reg.test(Ad)){alert("发布日期格式不对！");return false ;}
if(Md==""){alert("更新日期不能为空！");return false ;}
if(!Reg.test(Md)){alert("更新日期格式不对！");return false ;}
var objHits=$("hits");
if(objHits.value.Trim()=="")objHits.value="0";
if(!/^\d+$/.test(objHits.value.Trim())){alert("点击次数应为数字！");return false ;}
objHits.value=objHits.value.Trim().replace(/^0+(\d+)$/,function ($0,$1){return $1;})
return true;
}
function SaveCheck(){
if($("title").value.Trim()==""){
alert("标题不能为空！");return false ;
}
if(!CheckDateHits())return false ;
return true;
}
function SaveCheckM(){
if($("title").value.Trim()==""){
alert("新闻标题不能为空！");return false ;
}
if(!CheckDateHits())return false ;
return true;
}
function selTags(){
var x=$('DDLTags');
var v=x.options[x.selectedIndex].text;
var TagsNews=$('Tags');
var TagsCtn=TagsNews.value;
if(TagsCtn.length==0){ TagsNews.value=v; return ;}
var vList=TagsCtn.split(',');
for(var i=0;i<vList.length;i++)
if(vList[i]==v ) return;
TagsCtn=TagsCtn.Trim();
if(TagsCtn.charAt(TagsCtn.length-1)==',')
TagsNews.value=TagsCtn+v;
else 
TagsNews.value=TagsCtn+","+v;
}
function CheckDatePara(){
if(!CheckDateHits())return false ;
var vWr=$("HdID").value.split('|');
for(var i=0;i<vWr.length;i++){
var vTemp=vWr[i].split(',');
switch (vWr[i].substring(0,1)){
case "S":
if($(vTemp[0]).value.Trim()=="") {alert(vTemp[1]+"必须填写。");return false;}; break;
case "X":
if($(vTemp[0]).selectedIndex==0) {alert(vTemp[1]+"必须选择，不能留白。");return false;}; break;
case "C":
var vSelC=0;
for(var j=0;j<vTemp[1];j++){if($(vTemp[0]+j).checked==true) vSelC=1; }
if(vSelC==0) {alert(vTemp[2]+"必须选择。");return false;}; break;
case "R":
var vSelR=0;
for(var j2=0;j2<vTemp[1];j2++){if($(vTemp[0]+j2).checked==true) vSelR=1; }
if(vSelR==0) {alert(vTemp[2]+"必须选择。");return false;}; break;
}
}
vWr=$("HdnPlist").value.split('|');
for(var i=0;i<vWr.length;i++){
 if(vWr[i].indexOf('S')>=0){
 var vid=vWr[i].replace("S","S_N");
 $(vid).value= $(vid).value.replace(/</g,'&lt;').replace(/>/g,'&gt;');
 }
}
return true;
}
function SaveCheckWr(){
if($("title").value.Trim()==""){
alert("产品标题不能为空！");return false ;
}
if(!CheckDatePara()) return false;
$("HdnValue").value=$("title").value+"`"+($("Fps_ok").checked?"1":"0")+"`"+($("top_ok").checked?"1":"0")+"`"+$("Author").value
          +"`"+$("hits").value+"`"+$("addtime").value+"`"+$("updatetime").value+"`"+$("access").value+"`"+$("columnimg").value+"`"+$("Annex").value+"`"+$("Thumbimg").value+"`"+($("New_ok").checked?"1":"0")+"`"+($("Hot_ok").checked?"1":"0")+"`"+($("Weixin_ok").checked?"1":"0");
var NumL=parseInt ( $("HdnUpNum").value);
var vImgStr="";
for(var n=1;n<=NumL;n++)
   vImgStr+="`"+$("dspName"+n).value.Trim()+"`"+$("ImgUrl"+n).value.Trim();
$("HdnImg").value=vImgStr==""?vImgStr:vImgStr.substring (1);
return true;
}
function SaveCheckWrDL(){
if($("title").value.Trim()==""){
alert("下载标题不能为空！");return false ;
}
if(!CheckDatePara()) return false;
$("HdnValue").value=$("title").value+"`"+($("Fps_ok").checked?"1":"0")+"`"+($("top_ok").checked?"1":"0")+"`"+$("Author").value
          +"`"+$("hits").value+"`"+$("addtime").value+"`"+$("updatetime").value+"`"+$("access").value+"`"+$("DLAccess").value+"`"+$("FSize").value+"`"+$("Dload").value;
return true;
}
function SaveCheckWrPic(){
if($("title").value.Trim()==""){
alert("图片标题不能为空！");return false ;
}
if(!CheckDatePara()) return false;
$("HdnValue").value=$("title").value+"`"+($("Fps_ok").checked?"1":"0")+"`"+($("top_ok").checked?"1":"0")+"`"+$("Author").value
          +"`"+$("hits").value+"`"+$("addtime").value+"`"+$("updatetime").value+"`"+$("access").value+"`"+$("columnimg").value+"`"+$("Thumbimg").value+"`"+($("Weixin_ok").checked?"1":"0");
var NumL=parseInt ( $("HdnUpNum").value);
var vImgStr="";
for(var n=1;n<=NumL;n++)
   vImgStr+="`"+$("dspName"+n).value.Trim()+"`"+$("ImgUrl"+n).value.Trim();
$("HdnImg").value=vImgStr==""?vImgStr:vImgStr.substring (1);
return true;
}
function CheckUp(file1,Num){
   if ( CheckImg(file1)==1) return;
  var vForm=$("FormUp");
$("UpWt"+Num).style.display="";
  vForm.action ="UpForm/UpImg.aspx?Num="+Num;
  vForm.submit();  
 //$("BtnUpImg").click();
}
function Check2Up(nFType)
{
var vR;
if(nFType==1) vR=CheckImg("upload1");
else if(nFType==11) vR=CheckImg("upload1");
else if(nFType==3) vR=CheckImg("Thumb1");
else if(nFType==13) vR=CheckImg("Thumb1");
else if(nFType==2) vR=Checkf();
else if(nFType==5) vR=CheckMdb();
if(vR==1) return;
$("Wait"+nFType).style.display="";
  var vForm=$("FormUp2");
  vForm.action ="UpForm/UpPdFile.aspx?Type="+nFType;
  vForm.submit();  
} 
function AddImg(){
var trUp=$("trUp");
var hdNum=$("HdnUpNum");
var Num=(parseInt ( hdNum.value)+1);
hdNum.value=Num;
var parent=trUp.parentNode;
var tr=document.createElement("tr");
tr.setAttribute("id","Up"+Num);
parent.insertBefore(tr,trUp);
td1 = document.createElement("td"); 
td2 = document.createElement("td"); 
//td1=tr.insertCell();
//td2=tr.insertCell();
td1.setAttribute("align","right");
td2.setAttribute("align","left");
td1.innerHTML="展示图片"+Num+"：<br>\
              <a href =javascript:CancelImg("+Num+") >删除</a>  ";
td2.innerHTML="<input name='dspName"+Num+"' id='dspName"+Num+"' type='text' /> \
                <span class='tips'>请输入名称，留空将采用默认名称</span> <br>\
                <input  id=\"ImgUrl"+Num+"\" type=\"text\" style=\"width: 200px;\" /> \
                <span class=\"upImg\"> \
                <input  type=\"file\" id=\"File"+Num+"\" name=\"File"+Num+"\" onchange=CheckUp('File"+Num+"',"+Num+") class=\"NB\" size=1 />\
                <a href=\"#\">上传图片</a> </span><img src=\"images/ld.gif\" class=\"Wt\" style=\"display:none;\" id=UpWt"+Num+" /><span id=UOk"+Num+" class=\"Wt\" ></span>";
tr.appendChild(td1); 
tr.appendChild(td2); 
}
function CancelImg(Num){
var vfile=$("ImgUrl"+Num).value.Trim();
var trUpC=$("Up"+Num);
var hdNum=$("HdnUpNum");
var NumL=parseInt ( hdNum.value);
var parent=trUpC.parentNode;
parent.removeChild($("Up"+Num));
for(var i=Num+1;i<=NumL;i++){
 var trN= $("Up"+i);
 trN.setAttribute("id","Up"+(i-1));
 trN.cells[0].innerHTML="展示图片"+(i-1)+"：<br>\
              <a href =javascript:CancelImg("+(i-1)+") >删除</a>  ";
 trN.cells[1].innerHTML="<input name='dspName"+(i-1)+"' id='dspName"+(i-1)+"' type='text' value='"+$("dspName"+i).value+"' /> \
                <span class='tips'>请输入名称，留空将采用默认名称</span> <br>\
                <input  id=\"ImgUrl"+(i-1)+"\" type=\"text\" style=\"width: 200px;\"  value='"+$("ImgUrl"+i).value+"' /> \
               <span class=\"upImg\"> \
              <input  type=\"file\" id=\"File"+(i-1)+"\" name=\"File"+(i-1)+"\" onchange=CheckUp('File"+(i-1)+"',"+(i-1)+") class=\"NB\" size=1 />\
                <a href=\"#\">上传图片</a> </span><img src=\"images/ld.gif\" class=\"Wt\" style=\"display:none;\" id=UpWt"+Num+" /><span id=UOk"+Num+" class=\"Wt\" >";
}
hdNum.value=(NumL-1);
if(vfile!="")
{
  var vForm=$("FormUp");
  vForm.action ="UpForm/DelImg.aspx?FName="+vfile;
  vForm.submit();  
}
}
function SaveCheckFL(){
if($("SiteName").value.Trim()==""){
alert("网站名称不能为空！");return false ;
}
if($("SiteUrl").value.Trim()==""){
alert("网站地址不能为空！");return false ;
}
return true ;
}
function SaveCheckPB(){
if($("Brand").value.Trim()==""){
alert("品牌名称不能为空！");return false ;
}
var vMemo=$("SiteIntro").value;
if(vMemo.indexOf('<')>=0||vMemo.indexOf('>')>=0){
alert("备注中不能包含html标签！");return false ;
}
if(vMemo.length>255){
alert("备注长度不能大于255个字！");return false ;
}
return true ;
}
function SaveCheckAdv(){
if(!/^\d+$/.test($("order").value.Trim())){
alert("序号必须为数字！");return false ;
}
if(!/^$|^\d+$/.test($("Width").value.Trim())){
alert("图片宽度必须为数字！");return false ;
}
if(!/^$|^\d+$/.test($("Height").value.Trim())){
alert("图片高度必须为数字！");return false ;
}
var JsCtn=$("JsCode");
JsCtn.value=JsCtn.value.replace(/</g,'&lt;').replace(/>/g,'&gt;');
JsCtn.style.color="#ffffff";
return true ;
}
function htmlEncode1(id){
var JsCtn=$(id);
JsCtn.value=JsCtn.value.replace(/</g,'&lt;').replace(/>/g,'&gt;');
JsCtn.style.color="#ffffff";
}

