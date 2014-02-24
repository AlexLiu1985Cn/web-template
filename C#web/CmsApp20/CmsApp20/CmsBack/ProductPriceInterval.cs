namespace CmsApp20.CmsBack
{
    using _BLL;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ProductPriceInterval : Page
    {
        private BLL Bll1 = new BLL();
        protected DataGrid DataGrid1;
        protected HtmlForm form1;

        private void BindToDataGrid()
        {
            DataSet dataSet = this.Bll1.DAL1.GetDataSet("select id, [Order],Start,[End],title from QH_PriceInterval order by CLNG([order]) asc");
            if (dataSet != null)
            {
                this.DataGrid1.DataKeyField = "id";
                this.DataGrid1.DataSource = dataSet.Tables[0];
                this.DataGrid1.DataBind();
                this.DataGrid1.ShowFooter = true;
            }
        }

        protected void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            this.DataGrid1.EditItemIndex = -1;
            this.BindToDataGrid();
        }

        protected void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            this.Bll1.DAL1.ExecuteNonQuery("delete from QH_PriceInterval where id=" + this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString());
            this.DataGrid1.CurrentPageIndex = 0;
            this.DataGrid1.EditItemIndex = -1;
            this.BindToDataGrid();
        }

        protected void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
        {
            this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
            this.BindToDataGrid();
        }

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "AddNewRow")
            {
                DataSet dataSet = this.Bll1.DAL1.GetDataSet("select id, [Order],Start,[End],title from QH_PriceInterval order by CLNG([order]) asc");
                DataColumn column = new DataColumn("OrderInt")
                {
                    DataType = Type.GetType("System.Int32"),
                    Expression = "Convert(Order, 'System.Int32')"
                };
                dataSet.Tables[0].Columns.Add(column);
                object obj2 = dataSet.Tables[0].Compute("Max(OrderInt)", "");
                string str = "1";
                if (!Convert.IsDBNull(obj2))
                {
                    str = (Convert.ToInt32(obj2) + 1).ToString();
                }
                DataRow row = dataSet.Tables[0].NewRow();
                row["Order"] = str;
                dataSet.Tables[0].Rows.Add(row);
                this.DataGrid1.DataKeyField = "id";
                this.DataGrid1.DataSource = dataSet;
                int count = this.DataGrid1.Items.Count;
                if (count >= this.DataGrid1.PageSize)
                {
                    this.DataGrid1.CurrentPageIndex++;
                    count = 0;
                }
                this.DataGrid1.EditItemIndex = count;
                this.DataGrid1.DataBind();
            }
        }

        protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#9ec0fe'");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                ((LinkButton)e.Item.Cells[5].Controls[0]).Attributes.Add("onclick", "return confirm('确认删除?')");
                ((LinkButton)e.Item.Cells[4].FindControl("editLButton")).Style.Add("color", "#333333");
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                if (((DataRowView)e.Item.DataItem)["id"].ToString() == "")
                {
                    ((TextBox)e.Item.Cells[3].Controls[0]).Style.Add("display", "none");
                }
                ((LinkButton)e.Item.Cells[5].Controls[0]).Style.Add("display", "none");
                this.DataGrid1.ShowFooter = false;
                ((TextBox)e.Item.Cells[0].Controls[0]).Style.Add("Width", "40px");
                ((TextBox)e.Item.Cells[1].Controls[0]).Style.Add("Width", "80px");
                ((TextBox)e.Item.Cells[2].Controls[0]).Style.Add("Width", "80px");
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((LinkButton)e.Item.Cells[3].FindControl("lbAddNewRow")).Style.Add("color", "#ffffff");
            }
            else if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.Style.Add("background", "url(images/1.jpg) repeat-x");
                e.Item.Style.Add("height", "30px");
                e.Item.Style.Add("color", "#FFFFFF");
            }
        }

        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
            this.BindToDataGrid();
        }

        protected void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
        {
            string input = ((TextBox)e.Item.Cells[0].Controls[0]).Text.Trim();
            string str2 = ((TextBox)e.Item.Cells[1].Controls[0]).Text.Trim();
            string str3 = ((TextBox)e.Item.Cells[2].Controls[0]).Text.Trim();
            if (!Regex.IsMatch(input, @"^\d+$"))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "Order", "<script>alert('排序必须为数字！');</script>");
            }
            else if (!Regex.IsMatch(str2, @"^\d+(\.\d*)?$"))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "Start", "<script>alert('起始价格必须为数字！');</script>");
            }
            else if (!Regex.IsMatch(str3, @"^$|^\d+(\.\d*)?$"))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "End", "<script>alert('终止价格必须为数字或为空！');</script>");
            }
            else
            {
                string str4;
                OleDbParameter[] parameterArray;
                string str5;
                if (((TextBox)e.Item.Cells[3].Controls[0]).Style["display"] == "none")
                {
                    if ((str3 == "") && (float.Parse(str2) == 0f))
                    {
                        str5 = "全部价格";
                    }
                    else if (str3 == "")
                    {
                        str5 = str2 + "元以上";
                    }
                    else
                    {
                        str5 = str2 + "元～" + str3 + "元";
                    }
                    str4 = "Insert into QH_PriceInterval( [Order],Start,[End],title) values(@Order,@Start,@End,@title) ";
                    parameterArray = new OleDbParameter[] { new OleDbParameter("@Order", input), new OleDbParameter("@Start", str2), new OleDbParameter("@End", str3), new OleDbParameter("@title", str5) };
                }
                else
                {
                    str5 = ((TextBox)e.Item.Cells[3].Controls[0]).Text.Trim();
                    string str6 = this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
                    str4 = "UPDATE QH_PriceInterval SET [Order]=@Order,Start=@Start,[End]=@End,title=@title WHERE ID=@id";
                    parameterArray = new OleDbParameter[] { new OleDbParameter("@Order", input), new OleDbParameter("@Start", str2), new OleDbParameter("@End", str3), new OleDbParameter("@title", str5), new OleDbParameter("@id", str6) };
                }
                this.Bll1.DAL1.ExecuteNonQuery(str4, parameterArray);
                this.DataGrid1.EditItemIndex = -1;
                this.BindToDataGrid();
            }
        }

        protected void dgBoxList_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton button = (LinkButton)e.Item.Cells[5].Controls[0];
                button.CssClass = "buttoncss";
            }
            if ((e.Item.ItemType == ListItemType.Pager) && (this.DataGrid1.EditItemIndex == 0))
            {
                e.Item.Cells[0].Attributes.Add("colspan", "6");
            }
            this.DataGrid1.Attributes.Add("bordercolor", "#FFFFFF");
            this.DataGrid1.Attributes.Add("Width", "840px");
            this.DataGrid1.Attributes.Add("align", "center");
        }

        protected bool IsEnableAddNewRow()
        {
            return ((this.DataGrid1.CurrentPageIndex == (this.DataGrid1.PageCount - 1)) && (this.DataGrid1.EditItemIndex == -1));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Commen1.JudgeLogin(this.Page) && !this.Page.IsPostBack)
            {
                this.BindToDataGrid();
            }
        }
    }
}