$=function(id) {return document .getElementById (id);}
function CheckUp(Num){
var file1="File"+Num ;
   if ( CheckImg(file1)==1) return;
  var vForm=$("FormUp"+Num);
$("UpWt"+Num).style.display="";
  vForm.action ="UpForm/UpWeixinImg.aspx?Num="+Num;
  vForm.submit();  
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
function Open(nKey,nBranchNum){
for(var i=1;i<=nBranchNum;i++){
var obj=$("tr"+nKey+"_"+i);
var obj1=$(""+nKey);
if(obj.style.display=="")
{obj.style.display="none";obj1.src="images/jia.jpg";}
else
{obj.style.display="";obj1.src="images/jian.gif";}
}
}
function AutoSave(){
if(!confirm("保存自动生成将删除原有的设置，是否继续？") )
return false ;
var objHdn=$("HdnAutoSave");
var vNum=objHdn.value;
objHdn.value="";
for(var i=1;i<=parseInt(vNum);i++){
objHdn.value+=$("key"+i).value+"|";
objHdn.value+=$("Title"+i).value+"|";
objHdn.value+=$("Descrpt"+i).value+"|";
objHdn.value+=$("PicUrl"+i).value+"|";
objHdn.value+=$("Url"+i).value+"|";
objHdn.value+=$("HdnCID"+i).value+"|";
}
return true ;
}
function SaveTuWen(i,vID){
var objHdn=$("HdnInfo");
objHdn.value="TW|"+vID;
objHdn.value+="|"+$("key"+i).value;
objHdn.value+="|"+$("Title"+i).value;
objHdn.value+="|"+$("Descrpt"+i).value;
objHdn.value+="|"+$("PicUrl"+i).value;
objHdn.value+="|"+$("Url"+i).value;
objHdn.value+="|"+($("Eff"+i).checked?"1":"0");
$("BtnSave1").click();
}
function SaveText(i,vID){
var objHdn=$("HdnInfo");
objHdn.value="Text|"+vID;
objHdn.value+="|"+$("key"+i).value;
objHdn.value+="|"+$("Text"+i).value;
objHdn.value+="|"+($("Eff"+i).checked?"1":"0");
$("BtnSave1").click();
}
function Addnews(){
var objHdn=$("HdnInfo");
objHdn.value="TW";
$("BtnAdd1").click();
}
function Addtext(){
var objHdn=$("HdnInfo");
objHdn.value="Text";
$("BtnAdd1").click();
}
function Cancel(){
var objHdn=$("HdnInfo");
objHdn.value="Cancel";
$("BtnAdd1").click();
}
function SaveTuWenNew(){
var objHdn=$("HdnInfo");
objHdn.value="TWNew";
objHdn.value+="|"+$("keyN").value;
objHdn.value+="|"+$("TitleN").value;
objHdn.value+="|"+$("DescrptN").value;
objHdn.value+="|"+$("PicUrlN").value;
objHdn.value+="|"+$("UrlN").value;
objHdn.value+="|"+($("EffN").checked?"1":"0");
$("BtnSave1").click();
}
function SaveTextNew(){
var objHdn=$("HdnInfo");
objHdn.value="TextNew";
objHdn.value+="|"+$("keyN").value;
objHdn.value+="|"+$("TextN").value;
objHdn.value+="|"+($("EffN").checked?"1":"0");
$("BtnSave1").click();
}
function Del(id){
if(!confirm("确实要删除此回复吗？") )
return ;
var objHdn=$("HdnInfo");
objHdn.value="Del|"+id;
$("BtnSave1").click();
}
function RefreshNews(){
return confirm("更新新闻将删除原有的新闻设置，是否继续？") ;
}
function RefreshProduct(){
return confirm("更新产品将删除原有的产品设置，是否继续？") ;
}
function RefreshPicture(){
return confirm("更新图片将删除原有的图片设置，是否继续？") ;
}
