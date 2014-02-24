function  AddColumn(){
//var table1=document.getElementById("table1");
var table1=document.getElementsByTagName("table")[0];
var trs=table1.getElementsByTagName("tr")
var first=trs[trs.length-1];
//var first=table1.getElementsByTagName("tr")[0];
//var topid=first.getAttribute("id")+1;
var parent=first.parentNode;//得到父节点
var tr=document.createElement("tr");
//var td=document.createElement("td");
//var txt="<input name=\"Ck2\" type=\"checkbox\" />";//document.getElementById("txt").value;
//var add_txt=document.createTextNode(txt);
//td.appendChild(add_txt);
parent.insertBefore(tr,first);
td=tr.insertCell();
td2=tr.insertCell();
td.innerHTML="<input name=\"Ck2\" type=\"checkbox\" checked />";
td2.innerHTML="<input  name =\"TOrn\" type=\"text\" class=or  maxlength=3 />";
//tr.appendChild(td);
//tr.setAttribute("id",topid);

//tr.innerHtml="<td>cccc</td>";

}
function  AddColumn1(){
var table1=document.getElementsByTagName("table")[0];
var newTr = table1.insertRow();

//添加两列

var newTd0 = newTr.insertCell();

var newTd1 = newTr.insertCell();

//设置列内容和属性

newTd0.innerHTML = '<input type=checkbox id="box4">'; 

newTd2.innerText= '新加行';

}

function  AddColumn(Num,Depth){
CancelN();

var table1=document.getElementsByTagName("table")[0];
var trs=table1.getElementsByTagName("tr")
var first=trs[trs.length-1];
var parent=first.parentNode;//得到父节点
var tr=document.createElement("tr");
tr.setAttribute("id","NewR");
parent.insertBefore(tr,first);
td1=tr.insertCell();
td2=tr.insertCell();
td3=tr.insertCell();
td4=tr.insertCell();
td5=tr.insertCell();
td6=tr.insertCell();
td7=tr.insertCell();
td7.setAttribute("align","left");
td1.innerHTML="<input name=\"CkN\" type=\"checkbox\" checked />";
td2.innerHTML="<input  name =\"TOrN\" type=\"text\" class=or  maxlength=3 />";
td3.innerHTML="<input name=\"TNmN\" type=\"text\" class=or1 />";
td4.innerHTML="<select name=\"SelN1\">\
            <option  value=0 >不显示</option>\
            <option  value=1 >头部主导航条</option>\
            <option  value=2 >尾部导航条</option>\
            <option  value=3 >都显示</option>\
       </select>";
td5.innerHTML="<select name=\"SelN2\">\
            <option  value=1 >简介模块</option>\
            <option  value=2 >新闻模块</option>\
            <option  value=3 >产品模块</option>\
            <option  value=4 >下载模块</option>\
            <option  value=5 >图片模块</option>\
            <option  value=6 >反馈系统</option>\
            <option  value=7 >外部模块</option>\
       </select>";
td6.innerHTML="<input name=\"TDir\" type=\"text\"  class=or2 />";
td7.innerHTML="<a href =javascript:SaveN( )  >保存</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href =javascript:CancelN()  >取消</a>";
$("Add1").style.display="none";
}
function checklength(obj) {
        var max = obj.maxlength; 
        if(max == null || max == "" || max == undefined) {
            return;
        }
        if(obj.value.length > max) {
            alert("请不要超过最大长度:" + max);
            obj.value=obj.value.substring(0,(max-1));
            return;
        }
    }
  $("upload1").select(); 
document.execCommand("delete"); 
      //$("upload1").select ();
      //document.selection.clear();
      //form1 .reset ();
        document.all.FILE1.focus();
alert(document.all.FILE1);
var WshShell=new ActiveXObject("WScript.Shell");
alert(WshShell);
WshShell.sendKeys("c:\\temp\\date.rar");
 
 for(var i=Num;i<=NumL;i++)
parent.removeChild($("Up"+i));
hdNum.value=(Num-1);
  
  function CancelImg(Num){
var trUpC=$("Up"+Num);
var hdNum=$("HdnUpNum");
var NumL=parseInt ( hdNum.value);
var parent=trUpC.parentNode;
parent.removeChild($("Up"+Num));
hdNum.value=(NumL-1);
}
 td2.innerHTML="<input name='dspName"+Num+"' id='dspName"+Num+"' type='text' /> \
                <span class='tips'>请输入名称，留空将采用默认名称</span> <br>\
                <input  id=\"ImgUrl"+Num+"\" type=\"text\" style=\"width: 200px;\" /> \
                <input  type=\"file\" id=\"File"+Num+"\" name=\"File"+Num+"\" onchange=CheckUp('File"+Num+"',"+Num+") class=\"NB\" />";
                
 trN.cells[1].innerHTML="<input name='dspName"+(i-1)+"' id='dspName"+(i-1)+"' type='text' value='"+$("dspName"+i).value+"' /> \
                <span class='tips'>请输入名称，留空将采用默认名称</span> <br>\
                <input  id=\"ImgUrl"+(i-1)+"\" type=\"text\" style=\"width: 200px;\"  value='"+$("ImgUrl"+i).value+"' /> \
                <input  type=\"file\" id=\"File"+(i-1)+"\" name=\"File"+(i-1)+"\" onchange=CheckUp('File"+(i-1)+"',"+(i-1)+") class=\"NB\" />";
  FCKConfig.ToolbarSets["Custom"] = [
	['Source'],
	['Cut','Copy','Paste','PasteText','PasteWord'],
	['Undo','Redo','-','Find','Replace','-','SelectAll','RemoveFormat'],
	['Table','Rule','SpecialChar','-','FitWindow','ShowBlocks'],
	'/',
	['Bold','Italic','Underline','StrikeThrough','-','Subscript','Superscript'],
	['OrderedList','UnorderedList','-','Outdent','Indent','Blockquote','CreateDiv'],
	['Image','JustifyLeft','JustifyCenter','JustifyRight','JustifyFull'],
    ['Style','FontFormat','FontName','FontSize']
			// No comma for the last row.
] ;
