using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace salary_sms
{
    public partial class sal_sms : System.Web.UI.Page
    {
        Database db = new Database();
        DBHelper dbHelper = new DBHelper();

         protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPeriods();
                
                LoadCadres();
                LoadRegions();
            }
        }


        private void LoadRegions()
        {
            string query = "SELECT R.REG_CD, R.REG_NAME FROM REGIONS R ORDER BY 1";
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                RegionList.DataSource = dt;
                RegionList.DataTextField = "REG_NAME";
                RegionList.DataValueField = "REG_CD";
                RegionList.DataBind();
                // Add "Select" option at the top
                RegionList.Items.Insert(0, new ListItem("Select", ""));
            }
        }

        private void LoadPeriods()
        {
            string query = "SELECT PERIOD_ID, TO_CHAR(TRUNC(START_DATE), 'DD-MON-YYYY') AS START_DATE, TO_CHAR(TRUNC(END_DATE), 'DD-MON-YYYY') AS END_DATE, MONTH_YEAR FROM HRM_PERIOD ORDER BY 1 DESC";
            DataTable dt = db.GetData(query);

            if (dt.Rows.Count > 0)
            {
                // Set the DataSource
                ddlPeriod.DataSource = dt;

                // Set the DataTextField to a concatenation of multiple columns
                ddlPeriod.DataTextField = "DisplayText";
                ddlPeriod.DataValueField = "PERIOD_ID";

                // Create a new column in the DataTable for the concatenated text
                dt.Columns.Add("DisplayText", typeof(string), "PERIOD_ID + ' - ' + START_DATE + ' to ' + END_DATE");
                
                // Bind the data to the DropDownList
                ddlPeriod.DataBind();
                // Add the "Select" item at the top
                ddlPeriod.Items.Insert(0, new ListItem("Select Period", ""));
            }
        }


        private void LoadDivisions()
        {
            string query = "select d.division_cd, d.division_name from hrm_division d where d.reg_cd = '" + RegionList.SelectedValue + "'";
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataTextField = "division_name";
                ddlDivision.DataValueField = "division_cd";
                ddlDivision.DataBind();

                // Add "Select" option at the top
                ddlDivision.Items.Insert(0, new ListItem("Select Division", ""));
            }
        }

        private void LoadUnits()
        {
            string query = "select u.unit_cd, u.unit_name from hrm_unit u where u.reg_cd = '" + RegionList.SelectedValue + "' and u.division_cd = '" + ddlDivision.SelectedValue + "'";
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                ddlUnit.DataSource = dt;
                ddlUnit.DataTextField = "unit_name";
                ddlUnit.DataValueField = "unit_cd";
                ddlUnit.DataBind();
                // Add "Select" option at the top
                ddlUnit.Items.Insert(0, new ListItem("Select Unit", ""));
            }
        }

        private void LoadDepartments()
        {
            string query = "select d.department_cd, d.department_name from hrm_department d where d.reg_cd = '" + RegionList.SelectedValue + "' and d.unit_cd = '" + ddlUnit.SelectedValue + "' and d.division_cd = '" + ddlDivision.SelectedValue + "'";
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataTextField = "department_name";
                ddlDepartment.DataValueField = "department_cd";
                ddlDepartment.DataBind();

                // Add "Select" option at the top
                ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));
            }
        }

        private void LoadSections()
        {
            string query = "select s.section_cd, s.section_name from hrm_section s where s.reg_cd = '" + RegionList.SelectedValue + "' and s.division_cd = '" + ddlDivision.SelectedValue + "' and s.unit_cd = '" + ddlUnit.SelectedValue + "' and s.department_cd = '" + ddlDepartment.SelectedValue + "'";
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                ddlSection.DataSource = dt;
                ddlSection.DataTextField = "section_name";
                ddlSection.DataValueField = "section_cd";
                ddlSection.DataBind();
                // Add "Select" option at the top
                ddlSection.Items.Insert(0, new ListItem("Select Section", ""));
            }
        }

        private void LoadCadres()
        {
            string query = "select m.cadre_cd, m.cadre_name from hrm_cadre_mst m order by 1";
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                ddlCadre.DataSource = dt;
                ddlCadre.DataTextField = "cadre_name";
                ddlCadre.DataValueField = "cadre_cd";
                ddlCadre.DataBind();
                // Add "Select" option at the top
                ddlCadre.Items.Insert(0, new ListItem("Select Cadre", ""));
            }
        }

        private void LoadDesignations()
        {
            string query = "select d.designation_cd, d.designation_name from hrm_designation d where d.reg_cd = '" + RegionList.SelectedValue + "' and d.cadre_cd = '" + ddlCadre.SelectedValue + "'";
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                ddlDesignation.DataSource = dt;
                ddlDesignation.DataTextField = "designation_name";
                ddlDesignation.DataValueField = "designation_cd";
                ddlDesignation.DataBind();
                // Add "Select" option at the top
                ddlDesignation.Items.Insert(0, new ListItem("Select Designation", ""));
            }
        }

        private void LoadEmpHods()
        {
            string query = "select d.emp_hod13 emp_cd, e.emp_name from hrm_department d, hrm_employee e where d.emp_hod13 = e.emp_cd and d.reg_cd = '" + RegionList.SelectedValue + "' and e.emp_status in ('A', 'S')";
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                ddlEmpHod.DataSource = dt;
                ddlEmpHod.DataTextField = "emp_name";
                ddlEmpHod.DataValueField = "emp_cd";
                ddlEmpHod.DataBind();
                // Add "Select" option at the top
    ddlEmpHod.Items.Insert(0, new ListItem("Select HOD", ""));
            }
        }

        protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPeriodId = ddlPeriod.SelectedValue;
            string query = "SELECT START_DATE, END_DATE FROM HRM_PERIOD WHERE PERIOD_ID = '"+selectedPeriodId+"'";
            DataTable dt = db.GetData(query);
            if (dt.Rows.Count > 0)
            {
                txtStartDate.Text = Convert.ToDateTime(dt.Rows[0]["START_DATE"]).ToString("yyyy-MM-dd");
                txtEndDate.Text = Convert.ToDateTime(dt.Rows[0]["END_DATE"]).ToString("yyyy-MM-dd");
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = RegionList.SelectedValue;
            LoadDivisions();
            LoadEmpHods();
        }


        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUnits();
        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSections();
        }

        protected void ddlCadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDesignations();
        }

        //protected void btnSendPayslip_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Extract form values
        //        string selectedRegion = RegionList.SelectedValue;
        //        string selectedDivision = ddlDivision.SelectedValue;
        //        string selectedUnit = ddlUnit.SelectedValue;
        //        string selectedDepartment = ddlDepartment.SelectedValue;
        //        string selectedSection = ddlSection.SelectedValue;
        //        string selectedCadre = ddlCadre.SelectedValue;
        //        string selectedDesignation = ddlDesignation.SelectedValue;
        //        string selectedEmpHod = ddlEmpHod.SelectedValue;
        //        string employeeCode = txtEmployee.Text; // Assuming you enter employee code here
        //        string gender = "M";
        //        string body = "";
        //        string emp_cd = "";
        //        // Fetch employee name based on the employee code
        //        string employeeCellNumber = GetEmployeeCell(employeeCode);
        //        string startingdate = txtStartDate.Text;
        //        string endingdate = txtEndDate.Text;


        //        // Parse the input date
        //        DateTime date = DateTime.ParseExact(startingdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //        DateTime date2 = DateTime.ParseExact(endingdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        //        // Convert to desired format
        //        string StartDate = date.ToString("dd-MMM-yyyy").ToUpper();
        //        string EndDate = date2.ToString("dd-MMM-yyyy").ToUpper();



        //        string query2 = "select e.emp_cd from hrm_emp_payroll p, hrm_employee e where e.emp_cd = p.emp_cd and  e.reg_cd = p.reg_cd and p.fromdate='" + StartDate + "' and p.todate='" + EndDate + "' and p.serial_no is not null and p.reg_cd='" + selectedRegion + "' and e.hrm_division_cd= nvl('" + selectedDivision + "',e.hrm_division_cd) and e.hrm_unit_cd= nvl('" + selectedUnit + "',e.hrm_unit_cd) and e.hrm_department_cd= nvl('" + selectedDepartment + "',e.hrm_department_cd) and e.hrm_section_cd = nvl ('" + selectedSection + "',e.hrm_section_cd) and e.hrm_cadre_cd = nvl('" + selectedCadre + "', e.hrm_cadre_cd)";
        //        DataTable dtt = db.GetData(query2);
        //        if (dtt.Rows.Count > 0)
        //        {
        //            foreach (DataRow drr in dtt.Rows)
        //            {
        //                emp_cd = drr["emp_cd"].ToString();
        //            }
        //        }

        //        string query = "SELECT PAY_LIVE.FUN_SAL_SMS_STRING_PAY ('" + selectedRegion + "', '" + StartDate + "','" + EndDate + "','" + employeeCode + "','" + gender + "') AS SMS_BODY FROM DUAL";
        //        DataTable dt = db.GetData(query);
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                body = dr["SMS_BODY"].ToString();
        //            }
        //            // Send the SMS
        //            SendResponse sendResponse = new SendResponse();
        //            sendResponse.SendSmS(employeeCellNumber, body);
        //            ScriptManager.RegisterStartupScript(this, GetType(), "MessageAlert", "alert('Message sent successfully.');", true);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions or log them as necessary
        //        // Example: Log the exception or show a user-friendly message
        //        // LogException(ex);
        //    }
        //}

        private string GetEmployeeCell(string empCode)
        {
            string employeeCell = string.Empty;
            try
            {
                string query = "SELECT e.emp_name, e.sms_cell_no FROM hrm_employee e WHERE emp_cd = '" + empCode + "'";
                DataTable dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    employeeCell = dt.Rows[0]["sms_cell_no"].ToString();
                 }
            }
            catch (Exception ex)
            {
                // Handle exception
                // LogException(ex);
            }
            return employeeCell;
        }




        protected void btnSendPayslip_Click(object sender, EventArgs e)
        {
            try
            {
                // Extract form values
                string employeeCode = txtEmployee.Text;
                string selectedRegion = RegionList.SelectedValue;
                string selectedDivision = ddlDivision.SelectedValue;
                string selectedUnit = ddlUnit.SelectedValue;
                string selectedDepartment = ddlDepartment.SelectedValue;
                string selectedSection = ddlSection.SelectedValue;
                string selectedCadre = ddlCadre.SelectedValue;
                string selectedDesignation = ddlDesignation.SelectedValue;
                string selectedEmpHod = ddlEmpHod.SelectedValue;
                string gender = "M";
                string emp_cd = "";
                string body = "";
                string query2 = string.Empty;

                // Fetch employee name based on the employee code
                string startingdate = txtStartDate.Text;
                string endingdate = txtEndDate.Text;

                // Parse the input date
                DateTime date = DateTime.ParseExact(startingdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime date2 = DateTime.ParseExact(endingdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // Convert to desired format
                string StartDate = date.ToString("dd-MMM-yyyy").ToUpper();
                string EndDate = date2.ToString("dd-MMM-yyyy").ToUpper();

                //// Query to get multiple employee codes
                //string query2 = "select e.emp_cd from hrm_emp_payroll p, hrm_employee e where e.emp_cd = p.emp_cd and e.reg_cd = p.reg_cd and p.fromdate='" + StartDate + "' and p.todate='" + EndDate + "' and p.serial_no is not null and p.reg_cd='" + selectedRegion + "' and e.hrm_division_cd= nvl('" + selectedDivision + "',e.hrm_division_cd) and e.hrm_unit_cd= nvl('" + selectedUnit + "',e.hrm_unit_cd) and e.hrm_department_cd= nvl('" + selectedDepartment + "',e.hrm_department_cd) and e.hrm_section_cd = nvl ('" + selectedSection + "',e.hrm_section_cd) and e.hrm_cadre_cd = nvl('" + selectedCadre + "', e.hrm_cadre_cd) and e.emp_cd= nvl('" + employeeCode + "', e.emp_cd)";

                if (!string.IsNullOrEmpty(employeeCode))
                {
                    query2 = "SELECT e.emp_cd " +
                             "FROM hrm_emp_payroll p, hrm_employee e " +
                             "WHERE e.emp_cd = p.emp_cd " +
                             "AND e.reg_cd = p.reg_cd " +
                             "AND p.fromdate = '" + StartDate + "' " +
                             "AND p.todate = '" + EndDate + "' " +
                             "AND p.serial_no IS NOT NULL " +
                             "AND p.reg_cd = '" + selectedRegion + "' " +
                             "AND e.emp_cd = '" + employeeCode + "'";
                }
                else
                {
                    query2 = "SELECT e.emp_cd " +
                             "FROM hrm_emp_payroll p, hrm_employee e " +
                             "WHERE e.emp_cd = p.emp_cd " +
                             "AND e.reg_cd = p.reg_cd " +
                             "AND p.fromdate = '" + StartDate + "' " +
                             "AND p.todate = '" + EndDate + "' " +
                             "AND p.serial_no IS NOT NULL " +
                             "AND p.reg_cd = '" + selectedRegion + "' " +
                             "AND e.hrm_division_cd = NVL('" + selectedDivision + "', e.hrm_division_cd) " +
                             "AND e.hrm_unit_cd = NVL('" + selectedUnit + "', e.hrm_unit_cd) " +
                             "AND e.hrm_department_cd = NVL('" + selectedDepartment + "', e.hrm_department_cd) " +
                             "AND e.hrm_section_cd = NVL('" + selectedSection + "', e.hrm_section_cd) " +
                             "AND e.hrm_cadre_cd = NVL('" + selectedCadre + "', e.hrm_cadre_cd)";
                }
                
                DataTable dtt = db.GetData(query2);

                if (dtt.Rows.Count > 0)
                {
                    foreach (DataRow drr in dtt.Rows)
                    {
                        emp_cd = drr["emp_cd"].ToString();
                        string employeeCellNumber = GetEmployeeCell(emp_cd);

                        if (!string.IsNullOrEmpty(employeeCellNumber))
                        {
                            // Query to get the SMS body for the current employee
                            string query = "SELECT PAY_LIVE.FUN_SAL_SMS_STRING_PAY ('" + selectedRegion + "', '" + StartDate + "','" + EndDate + "','" + emp_cd + "','" + gender + "') AS SMS_BODY FROM DUAL";
                            DataTable dt = db.GetData(query);

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    body = dr["SMS_BODY"].ToString();
                                }

                                // Send the SMS
                                SendResponse sendResponse = new SendResponse();
                                sendResponse.SendSmS(employeeCellNumber, body);
                            }
                        }
                    }

                    // Show success message after sending all SMS messages
                    ScriptManager.RegisterStartupScript(this, GetType(), "MessageAlert", "alert('Messages sent successfully.');", true);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log them as necessary
                // Example: Log the exception or show a user-friendly message
                // LogException(ex);
            }
        }






    }
}