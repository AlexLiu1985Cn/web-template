<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductPriceInterval.aspx.cs" Inherits="CmsApp20.CmsBack.ProductPriceInterval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>产品价格区间</title>
    	<STYLE type=text/css>BODY {
	MARGIN-TOP: 0px;  FONT-SIZE: 12px;  }
.header1{background:url(images/1.jpg) repeat-x; height:30px; color:#FFFFFF; font-weight:bold;}
.item1{height:28px; background-color :#e6e6e6;}
.AltItem1{height:28px; background-color :#f9f9f9;}
</STYLE>
<link rel="stylesheet" href="css/cdqh.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
<div class="glrihgt">
<div class="glrihgtnei">
<div class="rightmain">当前位置：其它设置 >> <a href="ProductPriceInterval.aspx">产品价格区间</a></div>
<div class="rightmain1"> </div>
<div class="rightmain1"> </div>
</div> 
</div> 
         <table cellspacing="2" cellpadding="0" align="center" border="0"  bordercolor="#FFFFFF" >
          <tbody>
            <tr>
              <td height="28"><asp:DataGrid ID=DataGrid1 runat=server AllowPaging="True"  OnItemCreated="dgBoxList_ItemCreated"  BorderWidth=0 
              AutoGenerateColumns="False" OnCancelCommand="DataGrid1_CancelCommand"   CellPadding ="0" CellSpacing="2" GridLines=None
              OnDeleteCommand="DataGrid1_DeleteCommand" OnEditCommand="DataGrid1_EditCommand" OnItemDataBound="DataGrid1_ItemDataBound"  OnItemCommand="DataGrid1_ItemCommand"
               OnPageIndexChanged="DataGrid1_PageIndexChanged" OnUpdateCommand="DataGrid1_UpdateCommand"  PageSize="18" >
                  <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height=30px />
                              <EditItemStyle  Width=300px  Wrap=true  />
                           <HeaderStyle />
                           <ItemStyle CssClass="item1" /> 
                           <AlternatingItemStyle CssClass ="AltItem1" />  
    
             
        <Columns>

            <asp:BoundColumn DataField="order" HeaderText="排序"  ></asp:BoundColumn>
            <asp:BoundColumn DataField="Start" HeaderText="起始价格（元）"   ></asp:BoundColumn>
            <asp:BoundColumn DataField="End" HeaderText="终止价格（元）"   ></asp:BoundColumn>
            <asp:BoundColumn DataField="Title" HeaderText="显示标签"   ></asp:BoundColumn>
            
            <asp:TemplateColumn HeaderText="编辑">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CausesValidation="false" CommandName="Edit" Text="编辑" ID="editLButton"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="link1" runat="server" CommandName="Update" Text="更新" CausesValidation="False"></asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Cancel" Text="取消"></asp:LinkButton>
                </EditItemTemplate>
         <FooterTemplate >
          <asp:LinkButton id=lbAddNewRow runat="server" CommandName="AddNewRow"  Enabled="<%# IsEnableAddNewRow() %>">添加新价格区间...</asp:LinkButton>
      </FooterTemplate>
         </asp:TemplateColumn>
            
            
           <asp:ButtonColumn CommandName="Delete" Text="删除"  runat="server"  CausesValidation="false" HeaderText="删除" ></asp:ButtonColumn>
        </Columns>
        
        
                  <SelectedItemStyle BackColor="#FF0066" />
                  <PagerStyle NextPageText="下一页" PrevPageText="上一页"  />
    </asp:DataGrid></td>
            </tr>
          </tbody>
        </table>
   

    </form>
</body>
</html>
