
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace salary_sms
{
    class Database : System.Web.UI.Page
    {
        string EmployeeCode;
        string EmployeeUCode;
        //###################This Connection for ORACLE Database####################
        public string getConnectionOracle()
        {
            //string connectionstring = "Data Source=172.16.1.200:1521/PROD;Persist Security Info=True;User ID=apps;Password=Aktm_prod";

            //string connectionstring = "Data Source=172.16.0.56:1521/PROD;Persist Security Info=True;User ID=devapps;Password=devapps";

            string connectionstring = "Data Source=172.16.0.56:1521/PROD;Persist Security Info=True;User ID=apps;Password=blacksheep007";

            //string connectionstring = "Data Source=172.16.1.97:1521/PROD;Persist Security Info=True;User ID=apps;Password=apps";

            //DOTNET User Link

            // string connectionstring = "Data Source=172.16.0.56:1521/PROD;Persist Security Info=True;User ID=DOTNET;Password=dotnet##";
            return connectionstring;

        }

        //###################This Connection for ORACLE Database####################
        public string getConnectionOracleCustom()
        {
            //string connectionstring = "Data Source=172.16.1.205:1521/uat.alkaram.com;Persist Security Info=True;User ID=apps;Password=apps";
            string connectionstring = "Data Source=172.16.0.10:1521/APP.alkaram.com;Persist Security Info=True;User ID=apps;Password=blacksheep007";
            //string connectionstring = "Data Source=172.16.0.56:1521/PROD;Persist Security Info=True;User ID=apps;Password=blacksheep007";
            return connectionstring;
        }

        //###################This Connection for MSSQL Database####################
        public string getConnectionMsSql()
        {
            string connectionstring = "Data Source=SQLDATABASE0044\\MAINSQLDBSERVER;Initial Catalog=Ak_OnlineSupplier;Persist Security Info=True;User ID=sa;Password=Aktm12345";
            return connectionstring;
        }

        public string getEmpName(string query)
        {
            string result = string.Empty;
            try
            {
                string orcconstring = getConnectionOracleCustom();
                OracleConnection con = new OracleConnection(orcconstring);
                con.Open();
                OracleCommand cmd = new OracleCommand(query, con);
                OracleDataAdapter OraAdt = new OracleDataAdapter();
                OraAdt.SelectCommand = cmd;
                DataTable dt = new DataTable();
                OraAdt.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    string empName = dr["EMP_NAME"].ToString();
                    result = empName;
                }
                return result;

            }
            catch (Exception ex)
            {
                result = "Error";
            }

            return result;
        }


        public DataTable GetDataEbs(string msql)
        {
            string orcconstring = this.getConnectionOracle();
            OracleConnection orccon = new OracleConnection(orcconstring);
            orccon.Open();
            OracleCommand OraCmd = new OracleCommand(msql, orccon);
            OracleDataAdapter OraAdt = new OracleDataAdapter();
            DataTable dt = new DataTable();
            OraAdt.SelectCommand = OraCmd;
            OraAdt.Fill(dt);
            orccon.Close();
            return dt;
        }



        public DataTable GetData(string msql)
        {
            DataTable dt = new DataTable();
            try
            {

                string orcconstring = this.getConnectionOracleCustom();
                OracleConnection orccon = new OracleConnection(orcconstring);
                orccon.Open();
                OracleCommand OraCmd = new OracleCommand(msql, orccon);
                OracleDataAdapter OraAdt = new OracleDataAdapter();

                OraAdt.SelectCommand = OraCmd;
                OraAdt.Fill(dt);
                orccon.Close();

            }
            catch
            {
                Exception ex;
            }
            return dt;
        }

        //public int GetCrdCd(string msql)
        //{
        //    int cdrNo = 0;
        //    string orcconstring = this.getConnectionOracleCustom();
        //    OracleConnection orccon = new OracleConnection(orcconstring);
        //    orccon.Open();
        //    OracleCommand OraCmd = new OracleCommand(msql, orccon);
        //    OracleDataAdapter OraAdt = new OracleDataAdapter();
        //    DataTable dt = new DataTable();
        //    OraAdt.SelectCommand = OraCmd;
        //    OraAdt.Fill(dt);
        //    orccon.Close();
        //    if (dt.Rows.Count > 0)
        //    {
        //        DataRow dr = dt.Rows[0];
        //        cdrNo = Convert.ToInt32(dr[0]);
        //    }
        //    return cdrNo;
        //}

        public int GetCrdCd(string msql)
        {
            int cdrNo = 0;
            string orcconstring = this.getConnectionOracleCustom();

            using (OracleConnection orccon = new OracleConnection(orcconstring))
            {
                orccon.Open();
                using (OracleCommand OraCmd = new OracleCommand(msql, orccon))
                {
                    using (OracleDataAdapter OraAdt = new OracleDataAdapter(OraCmd))
                    {
                        DataTable dt = new DataTable();
                        OraAdt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];

                            if (dr[0] != DBNull.Value)
                            {
                                cdrNo = Convert.ToInt32(dr[0]);
                            }

                        }

                        return cdrNo;
                    }
                }
            }


        }


        public string PostData(string query)
        {
            string Retuen = string.Empty;

            try
            {
                string orcconstring = this.getConnectionOracleCustom();
                OracleConnection orccon = new OracleConnection(orcconstring);
                orccon.Open();
                OracleCommand orccmd = new OracleCommand(query, orccon);
                orccmd.ExecuteNonQuery();
                orccon.Close();

                Retuen = "Done";
            }
            catch (Exception ex)
            {
                Retuen = "Error";
            }

            return Retuen;
        }

        public string GetSingleValue(string query)
        {
            string result = "";
            try
            {
                string orcconstring = this.getConnectionOracleCustom();
                using (OracleConnection orccon = new OracleConnection(orcconstring))
                {
                    orccon.Open();
                    OracleCommand orccmd = new OracleCommand(query, orccon);
                    object queryResult = orccmd.ExecuteScalar();
                    if (queryResult != null)
                    {
                        result = queryResult.ToString();
                    }
                    orccon.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors
                result = "Error";
            }
            return result;
        }



        public string DatabaseConnectionCheck()
        {
            string Retuen = string.Empty;

            try
            {
                string orcconstring = this.getConnectionOracleCustom();
                OracleConnection orccon = new OracleConnection(orcconstring);
                orccon.Open();
                Retuen = "Done";
                orccon.Close();
            }
            catch (Exception ex)
            {
                Retuen = ex.ToString();
            }


            return Retuen;
        }

        public void getSession()
        {
            try
            {
                if (Session["EmployeeCode"] != null)
                {
                    EmployeeCode = Session["EmployeeCode"].ToString();
                    DataTable dt = new DataTable();
                    string query = "SELECT * from users where EMP_CD = '" + EmployeeCode + "'";
                    dt = GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        Session["EmployeeUserCode"] = dr["USR_CD"].ToString();
                        EmployeeUCode = Session["EmployeeUserCode"].ToString();
                    }
                }
                else
                {
                    Response.Redirect("LockScreen.aspx");
                }
            }
            catch (Exception ex)
            {
                //string script = "alert('Database error');";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertScript", script, true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "showErrorToast", "showDatabaseErrorToast();", true);
            }
        }
    }
}