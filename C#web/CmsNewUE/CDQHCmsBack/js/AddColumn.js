$=function(id) {return document .getElementById (id);}
String.prototype.Trim = function()
{
	return this.replace( /(^\s*)|(\s*$)/g, '' ) ;
}
var DepthN,idN,pIDN,ChNumN;
function  AddColumn(Num,ChNum,Depth,id,pID,IDM,Msg,Zhp,Mdl,DirInfo){
CancelN();
DepthN=Depth;idN=id;pIDN=pID;ChNumN=ChNum;
var str="";
if(Depth==1) str="&nbsp;&nbsp;└";
else if(Depth==2) str="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;└";
var table1=document.getElementsByTagName("table")[0];
var trs=table1.getElementsByTagName("tr");
var first=Num==-1?trs[trs.length-1]:trs[Num+ChNum+2];
var order=ChNum;
if(Depth<2){
var arrDirInfo=eval(DirInfo);
order =0;
for(var i=0;i<8;i++) order +=arrDirInfo[i];
}
var parent=first.parentNode;//得到父节点
var tr=document.createElement("tr");
tr.setAttribute("id","NewR");
tr.style.backgroundColor="#9ec0fe";
var c;
tr.onmouseover=function(){
c=this.style.backgroundColor;this.style.backgroundColor="#9ec0fe";};
tr.onmouseout=function(){
this.style.backgroundColor=c;
};
parent.insertBefore(tr,first);
td1 = document.createElement("td"); 
td2 = document.createElement("td"); 
td3 = document.createElement("td"); 
td4 = document.createElement("td"); 
td5 = document.createElement("td"); 
td6 = document.createElement("td"); 
td7 = document.createElement("td"); 
td8 = document.createElement("td"); 
//td1=tr.insertCell();
//td2=tr.insertCell();
//td3=tr.insertCell();
//td4=tr.insertCell();
//td5=tr.insertCell();
//td6=tr.insertCell();
//td7=tr.insertCell();
//td8=tr.insertCell();
td4.setAttribute("align","left");
//td7.setAttribute("align","center");
td1.innerHTML="<input name=\"CkN\" id=\"CkN\" type=\"checkbox\" checked />";
td2.innerHTML="<input name =\"IDMN\" id =\"IDMN\" type=\"text\" value="+IDM+" class=or  maxlength=3 />";
td3.innerHTML="<input name =\"TOrN\" id =\"TOrN\" type=\"text\" value="+order+" class=or  maxlength=3 />";
td4.innerHTML=str+"<input name=\"TNmN\" id=\"TNmN\" type=\"text\" class=or1 />";
var vSel=Depth==0?"selected":"";
td5.innerHTML="<select name=\"SelN1\" id=\"SelN1\">\
            <option  value=0 >不显示</option>\
            <option  value=1 >头部主导航条</option>\
            <option  value=2 >尾部导航条</option>\
            <option  value=3 "+vSel+" >都显示</option>\
       </select>";
       
var vDir=GetDirName(DirInfo,Mdl);

if(Mdl>=6) Mdl=1;
Mdl-=1;
var vMdlSel=new Array (5);
vMdlSel[0]=["selected","","","",""];       
vMdlSel[1]=["","selected","","",""];       
vMdlSel[2]=["","","selected","",""];       
vMdlSel[3]=["","","","selected",""];       
vMdlSel[4]=["","","","","selected"];       
var Module="<select name=\"SelN2\" id=\"SelN2\" onchange=\"MdlChanged("+DirInfo+");\"  >\
            <option  value=1 "+vMdlSel[Mdl][0]+" >简介模块</option>\
            <option  value=2 "+vMdlSel[Mdl][1]+" >新闻模块</option>\
            <option  value=3 "+vMdlSel[Mdl][2]+" >产品模块</option>\
            <option  value=4 "+vMdlSel[Mdl][3]+" >下载模块</option>\
            <option  value=5 "+vMdlSel[Mdl][4]+" >图片模块</option>\
            {0}\
            <option  value=7 >外部模块</option>\
            {1}\
       </select>";
var LY6=Msg==0?"<option  value=6 >留言反馈</option>":"";              
var ZP8=Zhp==0?"<option  value=8 >招聘模块</option>":""; 
td6.innerHTML=Module.replace("{0}",LY6).replace("{1}",ZP8);              
//td6.innerHTML="aa";              
//td6.style.width="90px";
//td6.setAttribute("width","90px");
td7.innerHTML="<input name=\"TDir\" id=\"TDir\" type=\"text\"  class=or2 value=\""+vDir+"\" />";
//td7.innerHTML="aaaa";
//td7.style.width="130px";
//td7.setAttribute("width","130px");
td8.innerHTML="<a href =javascript:SaveN("+Depth+","+id+","+pID +","+ChNum+")  >保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:CancelN()  >取消</a>";
//$("Add1").style.display="none";
tr.appendChild(td1); 
tr.appendChild(td2); 
tr.appendChild(td3); 
tr.appendChild(td4);
tr.appendChild(td5); 
tr.appendChild(td6); 
tr.appendChild(td7); 
tr.appendChild(td8);
}
function CancelN(){
var tr=$ ("NewR");
if(tr!=null ){
var parent=tr.parentNode;//得到父节点
parent.removeChild (tr);
$("Add1").style.display="";
}
}
function Save(Num,id ){
if(!CheckNumRow(Num)) return ;
$("HdSave").value=Num+"|"+id;
$("BSave1").click();
}
function SaveN(Depth,id ,pID,ChNum){
if(!CheckNewRow()) return ;
$("HdSaveN").value=Depth +"|"+id+"|"+pID+"|"+ChNum;
loadXMLDoc();
//$("BSaveN").click();
if(Depth==0){
var strDomian = document .URL ;
var nPos = strDomian.lastIndexOf ('/');
var nPos1 = strDomian.lastIndexOf ('/',nPos-1);
var strAdminDir = strDomian.substr(nPos1+1, nPos - nPos1-1 );
var strSystemDir = ",bin,app_data,ajax,upload,DBBAK,UpFile,include,FusionCharts,images,install,template,m,cdqhUpGradeTemp,NewsDetails,ProductDetails,DownloadDetails,PictureDetails,TagsAndSearch," + strAdminDir+",WebApp,MemberManage,";
if (strSystemDir.toLowerCase().indexOf("," + $("TDir").value.toLowerCase() + ",")>=0){
alert("新添栏目目录名称不能与系统目录"+$("TDir").value+"相同");
return ;
}
}
CheckIDMarkDir(id);
}
function CheckIDMarkDir(id){
if (xmlhttp!=null){
          xmlhttp.onreadystatechange=state_ChangeNew;
          xmlhttp.open("GET","../Ajax/AjaxCheckIDMarkDir.aspx?IDM="+$("IDMN").value+"&TDir="+encodeURIComponent($("TDir").value)+"&pID="+id+"&Tm="+Math.random(),true);
          xmlhttp.send(null);
}
}
function state_ChangeNew()
{
    if (xmlhttp.readyState==4)
    {
      if (xmlhttp.status==200)
        {
            //alert(xmlhttp.responseText);
          var vMD=xmlhttp.responseText.split('|');
          if(vMD[0]=="0"){alert('其它栏目已有此新添栏目标识，栏目标识不能相同，标识0除外！'); return ;}
          if(vMD[1]=="0"){alert('新添栏目在同级栏目目录名称不能相同！'); return ;}
          //if(vMD[1]=="2"){alert('新添目录名称不能为m（m为手机网站目录）！'); return ;}
          $("BSaveN").click();
        }
      else
        {
            alert("wrong!");
        }
    }
}
function Del(Depth,id,pID,ChNum,Mdl,folder){
var vTemp="删除此栏目将删除此栏目下所有内容。";
if(Mdl=="3") vTemp="\n删除此产品栏目将删除此栏目产品及此栏目备用字段！"; 
if(confirm ("确定要删除此栏目吗？"+vTemp) ){
if(ChNum!=0) {alert("请先将此栏目下的子栏目删干净。");return ; }
$("HdDel").value=Depth +"|"+id+"|"+pID+"|"+ChNum+"|"+Mdl+"|"+folder;
$("BDel").click();
}
}
function CheckNumRow(Num){
 var regD = /^[0-9]{1,3}$/;
if(!regD.test ($("IDM"+Num).value.Trim())){
alert ("标识必须为数字！");return false  ;}
if(!regD.test ($("TOr"+Num).value.Trim())){
alert ("顺序号必须为数字！");return false  ;}
if($("TNm"+Num).value.Trim()==""){
alert ("名称不能为空！");return  false ;}
return true;
}
function CheckNumRowA(Num){
 var regD = /^[0-9]{1,3}$/;
if(!regD.test ($("IDM"+Num).value.Trim())){
alert ("第"+Num+"行标识必须为数字！");return false  ;}
if(!regD.test ($("TOr"+Num).value.Trim())){
alert ("第"+Num+"行顺序号必须为数字！");return false  ;}
if($("TNm"+Num).value.Trim()==""){
alert ("第"+Num+"行名称不能为空！");return  false ;}
return true;
}
function CheckNewRow(){
 var regD = /^[0-9]{1,3}$/;
if(!regD.test ($("IDMN").value.Trim())){
alert (" 新添栏目标识必须为数字！");return false ;}
if(!regD.test ($("TOrN").value.Trim())){
alert (" 新添栏目顺序号必须为数字！");return false ;}
if($("TNmN").value.Trim()==""){
alert ("新添栏目名称不能为空！");return  false ;}
if($("TDir")!=null )
if($("TDir").value.Trim()==""){
alert ("新添栏目目录名称不能为空！");return  false ;}
return true;
}
function SaveAll(){
var ckNum=0;
if($("CkN")!=null&&$("CkN").checked==true ){
    if(!CheckNewRow()) return false ;
    else {
    ckNum++;
    $("HdSaveN").value=DepthN +"|"+idN+"|"+pIDN+"|"+ChNumN;
    }
}
var table1=document.getElementsByTagName("table")[0];
var trs=table1.getElementsByTagName("tr");
for(var i=1;i<trs.length;i++){
    if($("Ck"+i)!=null&&$("Ck"+i).checked==true ){
    ckNum++;
    if(!CheckNumRowA(i)) return false;
    }
}
if(ckNum==0){alert("您没有选择栏目，请先选择要保存的栏目。");return false;}
return true ;
}
function SetMsgDir(obj){
var tr=$ ("NewR");
if(obj.selectedIndex==5)
tr.cells[5].innerHTML="Message";
else 
tr.cells[5].innerHTML="<input name=\"TDir\" id=\"TDir\" type=\"text\"  class=or2 />";
}
var xmlhttp;
function loadXMLDoc()
{
   if (window.XMLHttpRequest)
          xmlhttp=new XMLHttpRequest();
    else
          xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
    if (xmlhttp===undefined)
    {
      alert("不支持XMLHTTP.");
    }
}
function GetClmnName(Mdl){
if (xmlhttp!=null){
          xmlhttp.onreadystatechange=state_Change;
          xmlhttp.open("GET","../Ajax/AjaxColumn.aspx?Mdl="+Mdl+"&Tm="+Math.random(),true);
          xmlhttp.send(null);
}
}
function state_Change()
{
    if (xmlhttp.readyState==4)
    {
      if (xmlhttp.status==200)
        {
           // alert(xmlhttp.responseText);
          AddRows();
        }
      else
        {
            alert("wrong!");
        }
    }
}
function AddField(Mdl){
if($ ("NewR")!=null ) return;
loadXMLDoc();
GetClmnName(Mdl);
}
function AddRows(){
var Clmn=xmlhttp.responseText.split(',');
var table1=document.getElementsByTagName("table")[0];
var trs=table1.getElementsByTagName("tr");
var first=trs[trs.length-1];
var order=trs.length-1;
var parent=first.parentNode;//得到父节点
var tr=document.createElement("tr");
tr.setAttribute("id","NewR");
var c;
tr.onmouseover=function(){
c=this.style.backgroundColor;this.style.backgroundColor="#eeeeee";};
tr.onmouseout=function(){
this.style.backgroundColor=c;
};
parent.insertBefore(tr,first);
td1 = document.createElement("td"); 
td2 = document.createElement("td"); 
td3 = document.createElement("td"); 
td4 = document.createElement("td"); 
td5 = document.createElement("td"); 
td6 = document.createElement("td"); 
td7 = document.createElement("td"); 
td8 = document.createElement("td"); 
td9 = document.createElement("td"); 
//td1=tr.insertCell();
//td2=tr.insertCell();
//td3=tr.insertCell();
//td4=tr.insertCell();
//td5=tr.insertCell();
//td6=tr.insertCell();
//td7=tr.insertCell();
//td8=tr.insertCell();
//td8.setAttribute("align","left");
td1.innerHTML="<input name=\"CkN\" id=\"CkN\" type=\"checkbox\" checked />";
td9.innerHTML="<input name =\"IDMN\" id =\"IDMN\" type=\"text\" value="+order+" class=or  maxlength=3 />";
td2.innerHTML="<input  name =\"TOrN\"  id =\"TOrN\" type=\"text\" value="+order+" class=or  maxlength=3 />";
td3.innerHTML="<input name=\"TNmN\" id=\"TNmN\" type=\"text\" class=or1 />";
var vSel="<select name=\"SelN\"><option  value=0 >所有栏目</option>";
for(var i=0;i<Clmn.length/2;i++)
vSel+="<option  value="+Clmn[i*2]+" >"+Clmn[i*2+1]+"</option>";
td4.innerHTML=vSel+"</select>";
td5.innerHTML="<select name=\"accessN\">\
            <option  value=0 >不限    </option>\
            <option  value=1 >普通会员</option>\
            <option  value=2 >高级会员</option>\
            <option  value=3 >管理员  </option>\
       </select>";
td6.innerHTML="<select name=\"TypeN\">\
            <option  value=0 >简短</option>\
            <option  value=1 >下拉</option>\
            <option  value=2 >文本</option>\
            <option  value=3 >多选</option>\
            <option  value=4 >单选</option>\
       </select>";
td7.innerHTML="<input type=\"checkbox\" name=wr_ok_N value=1 />";
td8.innerHTML="<a href =javascript:FSaveN()  >保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:CancelN()  >取消</a>";
tr.appendChild(td1) 
tr.appendChild(td9) 
tr.appendChild(td2) 
tr.appendChild(td3) 
tr.appendChild(td4)
tr.appendChild(td5) 
tr.appendChild(td6) 
tr.appendChild(td7) 
tr.appendChild(td8)
}
function FSaveN(){
if(!CheckNewRowF()) return ;
$("BSaveN").click();
}
function CheckNewRowF(){
 var regD = /^[0-9]{1,3}$/;
//if(!regD.test ($("IDMN").value.Trim())){
//alert (" 新添字段标识必须为数字！");return false ;}
if(!regD.test ($("TOrN").value.Trim())){
alert (" 新添字段顺序号必须为数字！");return false ;}
if($("TNmN").value.Trim()==""){
alert ("新添字段名称不能为空！");return  false ;}
return true;
}
function DelF(id){
if(confirm ("删除此栏目将删除此栏目下所有内容。确定要删除此项吗？") ){
$("HdDel").value=id;
$("BDel").click();
}
}
function FSaveAll(){
var ckNum=0;
if($("CkN")!=null&&$("CkN").checked==true ){
    if(!CheckNewRowF()) return false ;
    else {
    ckNum++;
    }
}
var table1=document.getElementsByTagName("table")[0];
var trs=table1.getElementsByTagName("tr");
for(var i=1;i<trs.length;i++){
    if($("Ck"+i)!=null&&$("Ck"+i).checked==true ){
    ckNum++;
    if(!CheckNumRowA(i)) return false;
    }
}
if(ckNum==0){alert("您没有选择栏目，请先选择要保存的栏目。");return false;}
return true ;
}
function AddFieldL(){
if($ ("NewR")!=null ) return;
var table1=document.getElementsByTagName("table")[0];
var trs=table1.getElementsByTagName("tr");
var first=trs[trs.length-1];
var order=trs.length-1;
var parent=first.parentNode;//得到父节点
var tr=document.createElement("tr");
tr.setAttribute("id","NewR");
var c;
tr.onmouseover=function(){
c=this.style.backgroundColor;this.style.backgroundColor="#eeeeee";};
tr.onmouseout=function(){
this.style.backgroundColor=c;
};
parent.insertBefore(tr,first);
td1=tr.insertCell();
td2=tr.insertCell();
td3=tr.insertCell();
td4=tr.insertCell();
td3.setAttribute("align","left");
td4.setAttribute("align","left");
td1.innerHTML="<input name=\"CkN\" id=\"CkN\" type=\"checkbox\" checked />";
td2.innerHTML="<input  name =\"TOrN\" id =\"TOrN\" type=\"text\" value="+order+" class=or  maxlength=3 />";
td3.innerHTML="<input name=\"TNmN\" id=\"TNmN\" type=\"text\" class=or1 />";
td4.innerHTML="<a href =javascript:FSaveN()  >保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:CancelN()  >取消</a>";
}
var vMdlDir=["","JianJie","News","Product","Download","Picture","Message","OutLink","ZhaoPin"];
function MdlChanged(DirInfo){
var vIndex=$("SelN2");
vIndex=vIndex.options[vIndex.selectedIndex].value;
$("TDir").value= GetDirName(DirInfo,vIndex);
}

function GetDirName(DirInfo,Mdl){
var arrDirInfo=eval(DirInfo);
var vDir=vMdlDir[Mdl];
if(Mdl!==6&&Mdl!==8){
var vMdlIndex=Mdl==7?5:(Mdl-1);
vDir+=arrDirInfo[vMdlIndex]==0?"":arrDirInfo[vMdlIndex];
}
return vDir;
}