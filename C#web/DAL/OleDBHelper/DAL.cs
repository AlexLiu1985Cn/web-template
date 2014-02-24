using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using _commen;

namespace _DAL.OleDBHelper
{
    public class DAL
    {
        public OleDbConnection MyConn;

        public DAL()
        {
            this.MyConn = this.CreateDB();
        }

        public DAL(int nDepth)
        {
            this.MyConn = this.CreateDB(nDepth);
        }

        public int AdpaterUpdate(string strSelect, string strUpdate, string[] astrField, List<List<string>> listData)
        {
            this.MyConn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(strSelect, this.MyConn)
            {
                UpdateCommand = new OleDbCommand(strUpdate, this.MyConn)
            };
            for (int i = 0; i < astrField.Length; i++)
            {
                switch (listData[0][i + 1])
                {
                    case "true":
                    case "false":
                        adapter.UpdateCommand.Parameters.Add("@" + astrField[i], OleDbType.Boolean, 1, astrField[i]);
                        break;

                    default:
                        adapter.UpdateCommand.Parameters.Add("@" + astrField[i], OleDbType.VarWChar, 0xfde8, astrField[i]);
                        break;
                }
            }
            OleDbParameter parameter = adapter.UpdateCommand.Parameters.Add("@ID", OleDbType.Integer);
            parameter.SourceColumn = "ID";
            parameter.SourceVersion = DataRowVersion.Original;
            DataTable dataTable = new DataTable();
            try
            {
                adapter.Fill(dataTable);
                for (int j = 0; j < dataTable.Rows.Count; j++)
                {
                    for (int k = 0; k < listData.Count; k++)
                    {
                        if (dataTable.Rows[j]["id"].ToString() == listData[k][0])
                        {
                            for (int m = 0; m < astrField.Length; m++)
                            {
                                string str2 = listData[k][m + 1];
                                if ((str2 == "true") || (str2 == "false"))
                                {
                                    dataTable.Rows[j][astrField[m]] = str2 == "true";
                                }
                                else
                                {
                                    dataTable.Rows[j][astrField[m]] = listData[k][m + 1];
                                }
                            }
                            break;
                        }
                    }
                }
                return adapter.Update(dataTable);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("adapter.update数据库记录错误：" + exception.ToString());
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return 0;
        }

        public int AdpaterUpdate(string strSelect, string strUpdate, string[] astrField, List<string[]> listData)
        {
            this.MyConn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(strSelect, this.MyConn)
            {
                UpdateCommand = new OleDbCommand(strUpdate, this.MyConn)
            };
            for (int i = 0; i < astrField.Length; i++)
            {
                switch (listData[0][i + 1])
                {
                    case "true":
                    case "false":
                        adapter.UpdateCommand.Parameters.Add("@" + astrField[i], OleDbType.Boolean, 1, astrField[i]);
                        break;

                    default:
                        adapter.UpdateCommand.Parameters.Add("@" + astrField[i], OleDbType.VarWChar, 0xfde8, astrField[i]);
                        break;
                }
            }
            OleDbParameter parameter = adapter.UpdateCommand.Parameters.Add("@ID", OleDbType.Integer);
            parameter.SourceColumn = "ID";
            parameter.SourceVersion = DataRowVersion.Original;
            DataTable dataTable = new DataTable();
            try
            {
                adapter.Fill(dataTable);
                for (int j = 0; j < dataTable.Rows.Count; j++)
                {
                    for (int k = 0; k < listData.Count; k++)
                    {
                        if (dataTable.Rows[j]["id"].ToString() == listData[k][0])
                        {
                            for (int m = 0; m < astrField.Length; m++)
                            {
                                string str2 = listData[k][m + 1];
                                if ((str2 == "true") || (str2 == "false"))
                                {
                                    dataTable.Rows[j][astrField[m]] = str2 == "true";
                                }
                                else
                                {
                                    dataTable.Rows[j][astrField[m]] = listData[k][m + 1];
                                }
                            }
                            break;
                        }
                    }
                }
                return adapter.Update(dataTable);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("adapter.update数据库记录错误：" + exception.ToString());
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return 0;
        }

        public int AdpaterUpdate(string strSelect, string strUpdate, string[] astrField, string[] astrFieldType, List<string[]> listData)
        {
            this.MyConn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(strSelect, this.MyConn)
            {
                UpdateCommand = new OleDbCommand(strUpdate, this.MyConn)
            };
            OleDbParameter parameter = null;
            for (int i = 0; i < astrField.Length; i++)
            {
                string str = astrFieldType[i];
                if (str != null)
                {
                    if (!(str == "int"))
                    {
                        if (str == "text")
                        {
                            goto Label_00CC;
                        }
                        if (str == "memo")
                        {
                            goto Label_00FC;
                        }
                        if (str == "bool")
                        {
                            goto Label_0129;
                        }
                        if (str == "date")
                        {
                            goto Label_014F;
                        }
                        if (str == "cur")
                        {
                            goto Label_0175;
                        }
                    }
                    else
                    {
                        parameter = adapter.UpdateCommand.Parameters.Add("@" + astrField[i], OleDbType.Integer, 4, astrField[i]);
                    }
                }
                goto Label_0199;
            Label_00CC:
                parameter = adapter.UpdateCommand.Parameters.Add("@" + astrField[i], OleDbType.VarChar, 0xff, astrField[i]);
                goto Label_0199;
            Label_00FC:
                parameter = adapter.UpdateCommand.Parameters.Add("@" + astrField[i], OleDbType.VarWChar, 0xfde8, astrField[i]);
                goto Label_0199;
            Label_0129:
                parameter = adapter.UpdateCommand.Parameters.Add("@" + astrField[i], OleDbType.Boolean, 1, astrField[i]);
                goto Label_0199;
            Label_014F:
                parameter = adapter.UpdateCommand.Parameters.Add("@" + astrField[i], OleDbType.Date, 10, astrField[i]);
                goto Label_0199;
            Label_0175:
                parameter = adapter.UpdateCommand.Parameters.Add("@" + astrField[i], OleDbType.Currency, 10, astrField[i]);
            Label_0199:
                if (i == (astrField.Length - 1))
                {
                    parameter.SourceVersion = DataRowVersion.Original;
                }
            }
            DataTable dataTable = new DataTable();
            try
            {
                adapter.Fill(dataTable);
                for (int j = 0; j < dataTable.Rows.Count; j++)
                {
                    for (int k = 0; k < listData.Count; k++)
                    {
                        if (dataTable.Rows[j][astrField[astrField.Length - 1]].ToString() == listData[k][astrField.Length - 1])
                        {
                            for (int m = 0; m < (astrField.Length - 1); m++)
                            {
                                string str2 = astrFieldType[m];
                                if (str2 != null)
                                {
                                    if (((!(str2 == "int") && !(str2 == "text")) && (!(str2 == "memo") && !(str2 == "date"))) && !(str2 == "cur"))
                                    {
                                        if (str2 == "bool")
                                        {
                                            goto Label_029D;
                                        }
                                    }
                                    else
                                    {
                                        dataTable.Rows[j][astrField[m]] = listData[k][m];
                                    }
                                }
                                continue;
                            Label_029D:
                                dataTable.Rows[j][astrField[m]] = listData[k][m] == "1";
                            }
                            break;
                        }
                    }
                }
                return adapter.Update(dataTable);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("adapter.update(new)数据库记录错误：" + exception.ToString());
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return 0;
        }

        public bool BatchInsert(string strTableSchema, string strInsert, string[] strCommen, string[] strCmnValue, string[] astrField, List<List<string>> listData, string[] astrFieldAll)
        {
            try
            {
                this.MyConn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(strTableSchema, this.MyConn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                adapter.InsertCommand = new OleDbCommand(strInsert, this.MyConn);
                for (int i = 0; i < astrFieldAll.Length; i++)
                {
                    adapter.InsertCommand.Parameters.Add("@" + astrFieldAll[i], OleDbType.VarWChar, 0xfde8, astrFieldAll[i]);
                }
                DataRow row = dataTable.NewRow();
                for (int j = 0; j < strCommen.Length; j++)
                {
                    row[strCommen[j]] = strCmnValue[j];
                }
                for (int k = 0; k < listData.Count; k++)
                {
                    DataRow row2 = dataTable.NewRow();
                    row2.ItemArray = row.ItemArray;
                    for (int m = 0; m < astrField.Length; m++)
                    {
                        row2[astrField[m]] = listData[k][m];
                    }
                    dataTable.Rows.Add(row2);
                }
                adapter.Update(dataTable);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("批量更新数据库错误：" + exception.ToString());
                return false;
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return true;
        }

        public bool BatchInsert(string strTableSchema, string strInsert, string[] strCommen, string[] strCmnValue, string[] astrField, List<string[]> listData, string[] astrFieldAll)
        {
            try
            {
                this.MyConn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(strTableSchema, this.MyConn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                adapter.InsertCommand = new OleDbCommand(strInsert, this.MyConn);
                for (int i = 0; i < astrFieldAll.Length; i++)
                {
                    adapter.InsertCommand.Parameters.Add("@" + astrFieldAll[i], OleDbType.VarWChar, 0xfde8, astrFieldAll[i]);
                }
                DataRow row = dataTable.NewRow();
                for (int j = 0; j < strCommen.Length; j++)
                {
                    row[strCommen[j]] = strCmnValue[j];
                }
                for (int k = 0; k < listData.Count; k++)
                {
                    DataRow row2 = dataTable.NewRow();
                    row2.ItemArray = row.ItemArray;
                    for (int m = 0; m < astrField.Length; m++)
                    {
                        row2[astrField[m]] = listData[k][m];
                    }
                    dataTable.Rows.Add(row2);
                }
                adapter.Update(dataTable);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("批量更新数据库错误：" + exception.ToString());
                return false;
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return true;
        }

        public bool BatchInsert(string strTableSchema, string strInsert, string[] strCommen, string[] strCommenType, string[] strCmnValue, string[] astrField, string[] astrFieldType, List<string[]> listData, string[] astrFieldAll, string[] astrFieldAllType)
        {
            try
            {
                this.MyConn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(strTableSchema, this.MyConn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                adapter.InsertCommand = new OleDbCommand(strInsert, this.MyConn);
                for (int i = 0; i < astrFieldAll.Length; i++)
                {
                    string str = astrFieldAllType[i];
                    if (str != null)
                    {
                        if (!(str == "int"))
                        {
                            if (str == "text")
                            {
                                goto Label_00DA;
                            }
                            if (str == "memo")
                            {
                                goto Label_010C;
                            }
                            if (str == "bool")
                            {
                                goto Label_013B;
                            }
                            if (str == "date")
                            {
                                goto Label_0163;
                            }
                            if (str == "cur")
                            {
                                goto Label_018B;
                            }
                        }
                        else
                        {
                            adapter.InsertCommand.Parameters.Add("@" + astrFieldAll[i], OleDbType.Integer, 4, astrFieldAll[i]);
                        }
                    }
                    continue;
                Label_00DA:
                    adapter.InsertCommand.Parameters.Add("@" + astrFieldAll[i], OleDbType.VarChar, 0xff, astrFieldAll[i]);
                    continue;
                Label_010C:
                    adapter.InsertCommand.Parameters.Add("@" + astrFieldAll[i], OleDbType.VarWChar, 0xfde8, astrFieldAll[i]);
                    continue;
                Label_013B:
                    adapter.InsertCommand.Parameters.Add("@" + astrFieldAll[i], OleDbType.Boolean, 1, astrFieldAll[i]);
                    continue;
                Label_0163:
                    adapter.InsertCommand.Parameters.Add("@" + astrFieldAll[i], OleDbType.Date, 10, astrFieldAll[i]);
                    continue;
                Label_018B:
                    adapter.InsertCommand.Parameters.Add("@" + astrFieldAll[i], OleDbType.Currency, 10, astrFieldAll[i]);
                }
                DataRow row = dataTable.NewRow();
                for (int j = 0; j < strCommen.Length; j++)
                {
                    string str2 = strCommenType[j];
                    if (str2 != null)
                    {
                        if (((!(str2 == "int") && !(str2 == "text")) && (!(str2 == "memo") && !(str2 == "date"))) && !(str2 == "cur"))
                        {
                            if (str2 == "bool")
                            {
                                goto Label_0242;
                            }
                        }
                        else
                        {
                            row[strCommen[j]] = strCmnValue[j];
                        }
                    }
                    continue;
                Label_0242:
                    row[strCommen[j]] = strCmnValue[j] == "1";
                }
                for (int k = 0; k < listData.Count; k++)
                {
                    DataRow row2 = dataTable.NewRow();
                    row2.ItemArray = row.ItemArray;
                    for (int m = 0; m < astrField.Length; m++)
                    {
                        string str3 = astrFieldType[m];
                        if (str3 != null)
                        {
                            if (((!(str3 == "int") && !(str3 == "text")) && (!(str3 == "memo") && !(str3 == "date"))) && !(str3 == "cur"))
                            {
                                if (str3 == "bool")
                                {
                                    goto Label_0318;
                                }
                            }
                            else
                            {
                                row2[astrField[m]] = listData[k][m];
                            }
                        }
                        continue;
                    Label_0318:
                        row2[astrField[m]] = listData[k][m] == "1";
                    }
                    dataTable.Rows.Add(row2);
                }
                adapter.Update(dataTable);
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("批量更新数据库(new)错误：" + exception.ToString());
                return false;
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return true;
        }

        public OleDbConnection CreateDB()
        {
            OleDbConnection connection = null;
            try
            {
//               String conStr = ConfigurationManager.AppSettings["OLEDBCONNECTIONSTRING"].ToString() + HttpContext.Current.Server.MapPath("../App_Data/" + ConfigurationManager.AppSettings["dbPath"]);
                connection = new OleDbConnection(ConfigurationManager.AppSettings["OLEDBCONNECTIONSTRING"].ToString() + HttpContext.Current.Server.MapPath("../App_Data/" + ConfigurationManager.AppSettings["dbPath"]));
                connection.Open();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("连接数据库错误： " + exception.ToString());
                connection = null;
            }
            if (connection != null)
            {
                connection.Close();
            }
            return connection;
        }

        public OleDbConnection CreateDB(int nDepth)
        {
            OleDbConnection connection = null;
            try
            {
                if (nDepth == 0)
                {
                    connection = new OleDbConnection(ConfigurationManager.AppSettings["OLEDBCONNECTIONSTRING"].ToString() + HttpContext.Current.Server.MapPath("App_Data/" + ConfigurationManager.AppSettings["dbPath"]));
                }
                else if (nDepth == 2)
                {
                    connection = new OleDbConnection(ConfigurationManager.AppSettings["OLEDBCONNECTIONSTRING"].ToString() + HttpContext.Current.Server.MapPath("../../App_Data/" + ConfigurationManager.AppSettings["dbPath"]));
                }
                connection.Open();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("连接数据库(0,2)错误： " + exception.ToString());
                connection = null;
            }
            if (connection != null)
            {
                connection.Close();
            }
            return connection;
        }

        public int ExecuteNonQuery(string strQuery)
        {
            try
            {
                this.MyConn.Open();
                OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
                return command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("操作数据库(无参数)错误： " + exception.ToString() + strQuery);         
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
                
            }
            return 0;       
        }

        public int ExecuteNonQuery(string strQuery, OleDbParameter[] OlePara)
        {
            try
            {
                this.MyConn.Open();
                OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
                command.Parameters.AddRange(OlePara);
                return command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("操作数据库错误： " + exception.ToString() + strQuery);
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return 0;
        }

        public int ExecuteReader(string strQuery)
        {
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    return int.Parse(reader["co"].ToString());
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("ExecuteReader读数据库记录数错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return 0;
        }

        public object ExecuteScalar(string strQuery)
        {
            try
            {
                this.MyConn.Open();
                OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
                return command.ExecuteScalar();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("操作数据库(无参数)错误： " + exception.ToString() + strQuery);
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return 0;
        }

        public object ExecuteScalar(string strQuery, OleDbParameter[] OlePara)
        {
            try
            {
                this.MyConn.Open();
                OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
                command.Parameters.AddRange(OlePara);
                return command.ExecuteScalar();
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("ExecuteScalar操作数据库(有参数)错误： " + exception.ToString() + strQuery);
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return 0;
        }

        public DataSet GetDataSet(string strQuery)
        {
            try
            {
                this.MyConn.Open();
                OleDbCommand selectCommand = new OleDbCommand(strQuery, this.MyConn);
                DataSet dataSet = new DataSet();
                new OleDbDataAdapter(selectCommand).Fill(dataSet, "tb1");
                return dataSet;
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("获取DataSet错误： " + exception.ToString() + strQuery);
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return null;
        }

        public DataSet GetDataSet(string strQuery, OleDbParameter[] OlePara)
        {
            try
            {
                this.MyConn.Open();
                OleDbCommand selectCommand = new OleDbCommand(strQuery, this.MyConn);
                selectCommand.Parameters.AddRange(OlePara);
                DataSet dataSet = new DataSet();
                new OleDbDataAdapter(selectCommand).Fill(dataSet, "tb1");
                return dataSet;
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("获取DataSet OledbPara错误： " + exception.ToString() + strQuery);
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return null;
        }

        public DataSet GetDataTablePage(int nStart, int nSize, string strQuery)
        {
            try
            {
                this.MyConn.Open();
                OleDbCommand selectCommand = new OleDbCommand(strQuery, this.MyConn);
                DataSet dataSet = new DataSet();
                new OleDbDataAdapter(selectCommand).Fill(dataSet, nStart, nSize, "tb1");
                return dataSet;
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("获取DataSet Page错误： " + exception.ToString() + strQuery);
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return null;
        }

        public DataSet GetDataTablePage(int nStart, int nSize, string strQuery, OleDbParameter[] OlePara)
        {
            try
            {
                this.MyConn.Open();
                OleDbCommand selectCommand = new OleDbCommand(strQuery, this.MyConn);
                selectCommand.Parameters.AddRange(OlePara);
                DataSet dataSet = new DataSet();
                new OleDbDataAdapter(selectCommand).Fill(dataSet, nStart, nSize, "tb1");
                return dataSet;
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("获取DataSet Page错误： " + exception.ToString() + strQuery);
            }
            finally
            {
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return null;
        }

        public string GetDelIDINString(string strQuery)
        {
            StringBuilder builder = new StringBuilder();
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    builder.Append("," + reader[0].ToString());
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            if (builder.Length > 0)
            {
                return builder.ToString().Substring(1);
            }
            return "";
        }

        public string GetINString(string strQuery)
        {
            StringBuilder builder = new StringBuilder();
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    builder.Append(",'" + reader[0].ToString() + "'");
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            if (builder.Length > 0)
            {
                return builder.ToString().Substring(1);
            }
            return "";
        }

        public string GetJsonString(string strQuery, string strKey, string strValue)
        {
            StringBuilder builder = new StringBuilder();
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    builder.Append(",{\"" + strKey + "\":\"" + reader[0].ToString() + "\",\"" + strValue + "\":\"" + reader[1].ToString() + "\"}");
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("JsonDataReader读数据库记录错误：" + exception.ToString());
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            if (builder.Length > 0)
            {
                return ("[" + builder.ToString().Substring(1) + "]");
            }
            return "[]";
        }

        public string ReadDataReader(string strQuery)
        {
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    return reader[0].ToString();
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return "<NULL>";
        }

        public string ReadDataReader(string strQuery, OleDbParameter[] OlePara)
        {
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            command.Parameters.AddRange(OlePara);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    return reader[0].ToString();
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return "<NULL>";
        }

        public void ReadDataReader(string strQuery, ref string[] strValue, string[] strField)
        {
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    for (int i = 0; i < strValue.Length; i++)
                    {
                        strValue[i] = reader[strField[i]].ToString();
                    }
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
        }

        public void ReadDataReader(string strQuery, ref string[] strValue, int nNum)
        {
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    for (int i = 0; i < nNum; i++)
                    {
                        strValue[i] = reader[i].ToString();
                    }
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
        }

        public void ReadDataReader(string strQuery, object obj2Bind, string strBindField)
        {
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (obj2Bind is DropDownList)
                {
                    DropDownList list = (DropDownList)obj2Bind;
                    list.DataSource = reader;
                    list.DataTextField = strBindField;
                    list.DataBind();
                }
                else if (obj2Bind is Repeater)
                {
                    Repeater repeater = (Repeater)obj2Bind;
                    repeater.DataSource = reader;
                    repeater.DataBind();
                }
                else if (obj2Bind is DataGrid)
                {
                    DataGrid grid = (DataGrid)obj2Bind;
                    grid.DataSource = reader;
                    grid.DataBind();
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
        }

        public void ReadDataReader(string strQuery, ref string[] strValue, string[] strField, OleDbParameter[] OlePara)
        {
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            command.Parameters.AddRange(OlePara);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    for (int i = 0; i < strValue.Length; i++)
                    {
                        strValue[i] = reader[strField[i]].ToString();
                    }
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
        }

        public string[,] ReadDataReader2DStr(string strQuery, int nRow, int nNum)
        {
            string[,] strArray = new string[nRow, nNum];
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                for (int i = 0; i < nRow; i++)
                {
                    if (reader.Read())
                    {
                        for (int j = 0; j < nNum; j++)
                        {
                            strArray[i, j] = reader[j].ToString();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader 2dimensionArrayString读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return strArray;
        }

        public ArrayList ReadDataReaderAL(string strQuery)
        {
            ArrayList list = new ArrayList();
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                }
            }
            catch (Exception exception)
            {
                list = null;
                SystemError.CreateErrorLog("DataReader ArrayList读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return list;
        }

        public string[][] ReadDataReaderJagged2DStr(string strQuery, int nRow, int nNum)
        {
            string[][] strArray = new string[nRow][];
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                for (int i = 0; i < nRow; i++)
                {
                    if (reader.Read())
                    {
                        strArray[i] = new string[nNum];
                        for (int j = 0; j < nNum; j++)
                        {
                            strArray[i][j] = reader[j].ToString();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader Jagged2ArrayString读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return strArray;
        }

        public List<List<string>> ReadDataReaderList(string strQuery, int nNum)
        {
            List<List<string>> list = new List<List<string>>();
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    List<string> item = new List<string>();
                    for (int i = 0; i < nNum; i++)
                    {
                        item.Add(reader[i].ToString());
                    }
                    list.Add(item);
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader ArrayList读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return list;
        }

        public List<string[]> ReadDataReaderListStr(string strQuery, int nNum)
        {
            List<string[]> list = new List<string[]>();
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    string[] item = new string[nNum];
                    for (int i = 0; i < nNum; i++)
                    {
                        item[i] = reader[i].ToString();
                    }
                    list.Add(item);
                }
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader ArrayList读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return list;
        }

        public string[] ReadDataReaderStringArray(string strQuery, int nNum)
        {
            string[] strArray = new string[nNum];
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    for (int i = 0; i < nNum; i++)
                    {
                        strArray[i] = reader[i].ToString();
                    }
                }
                return strArray;
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return strArray;
        }

        public string[] ReadDataReaderStringArray(string strQuery, int nNum, OleDbParameter[] OlePara)
        {
            string[] strArray = new string[nNum];
            OleDbDataReader reader = null;
            this.MyConn.Open();
            OleDbCommand command = new OleDbCommand(strQuery, this.MyConn);
            command.Parameters.AddRange(OlePara);
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                {
                    for (int i = 0; i < nNum; i++)
                    {
                        strArray[i] = reader[i].ToString();
                    }
                }
                return strArray;
            }
            catch (Exception exception)
            {
                SystemError.CreateErrorLog("DataReader读数据库记录错误：" + exception.ToString() + strQuery);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (this.MyConn != null)
                {
                    this.MyConn.Close();
                }
            }
            return strArray;
        }
    }
}
