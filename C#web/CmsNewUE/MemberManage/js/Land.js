$2=function(id) {return document .getElementById (id);}
function getCookie(name)   
{  
  var arr = document.cookie.match(new RegExp("(^| )"+name+"=([^;]*)(;|$)"));  
  if(arr != null) return decodeURIComponent(arr[2]); return null;  
    
} 
   function init(){
    var vUserName=getCookie("CmsUserName") ;
    var divLand2 = $2("cdqh_Toland");
    var divWelcome2 = $2("cdqh_Exitland")
    if(vUserName==null)
    {
        divWelcome2.style.display="none";
        divLand2.style.display="";
    }
    else
    {
        var divMemberName=$2("cdqh_MemberUser");
        divMemberName.innerHTML=vUserName;
        divLand2.style.display="none";
        divWelcome2.style.display="";
    }
}
function ExitLand()
{
//获取当前时间 
var date=new Date(); 
//将date设置为过去的时间 
date.setTime(date.getTime()-10000); 
//将userId这个cookie删除 
document.cookie="CmsUserName=; expires="+date.toGMTString()+"; path=/;";
//document.cookie="UserName="+vNameEncode+"; expires="+date.toGMTString()+"; path=/";
//location .reload();
location =document .URL ;
}
