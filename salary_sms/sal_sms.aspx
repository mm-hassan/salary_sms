<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sal_sms.aspx.cs" Inherits="salary_sms.sal_sms" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <title>Payslip Form</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet"/>
    <style>
        body, html {
            height: 100%;
            margin: 0;
        }

        .wrapper {
            display: flex;
            flex-direction: column;
            min-height: 100vh;
        }

        .content {
            flex: 1;
            padding-bottom: 60px; /* Height of the footer */
        }

        .container {
            max-width: 800px;
        }

        .navbar-brand img {
            height: 40px;
            margin-right: 10px;
        }

        .navbar-brand {
            display: flex;
            align-items: center;
            font-size: 24px;
            font-weight: bold;
        }

        .footer {
            background-color: #005450;
            color: #ffffff;
            text-align: center;
            padding: 10px 0;
            width: 100%;
            position: fixed;
            bottom: 0;
            left: 0;
        }

        .footer p {
            margin: 0;
        }

        .footer a {
            color: #fff;
            text-decoration: none;
            transition: color 0.3s;
        }

        .footer a:hover {
            color: #00ffff;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <!-- Navbar -->
        <nav class="navbar navbar-expand-lg navbar-dark" style="background-color:#005450">
            <a class="navbar-brand" href="#">
                <img src="images/logo.png" alt="Alkaram Logo"/>
                Alkaram Textile Mills Pvt. Ltd
            </a>
        </nav>  

        <div class="content">
            <form id="form1" class="pb-4" runat="server">
                <div class="container mt-4">
                    <div class="card">
                        <div class="card-header bg-primary text-white">
                            <b>PAY SLIP (SMS)</b>
                        </div>
                        <asp:ValidationSummary ValidationGroup="sub" ID="ValidationSummary1" runat="server" BackColor="#CCCCCC" Font-Size="Large" ForeColor="Red" />
                        <div class="card-body">
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="ddlPeriod">Period ID</label>
                                    <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged">
                                        <asp:ListItem Text="Select" Value="" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="ddlPeriod" ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Period field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="RegionList">Region</label>
                                    <asp:DropDownList ID="RegionList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="sub" ControlToValidate="RegionList" ID="RequiredFieldValidator" runat="server" Display="Dynamic" ErrorMessage="Region field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="txtStartDate">Start Date</label>
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="txtEndDate">End Date</label>
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="ddlDivision">Division</label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="ddlUnit">Unit</label>
                                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="ddlDepartment">Department</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="ddlSection">Section</label>
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="ddlCadre">Cadre</label>
                                    <asp:DropDownList ID="ddlCadre" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCadre_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="ddlDesignation">Designation</label>
                                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="ddlEmpHod">Emp. HOD</label>
                                    <asp:DropDownList ID="ddlEmpHod" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="txtEmployee">Employee</label>
                                    <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <asp:Button ID="btnSendPayslip" ValidationGroup="sub" runat="server" Text="Send" CssClass="btn btn-primary" OnClick="btnSendPayslip_Click" />
                        </div>
                    </div>
                </div>
            </form>
        </div>

        <!-- Footer -->
        <footer class="footer">
            <div class="container">
                <p>&copy; 2024 Alkaram Portal. All rights reserved. BT Dept</p>
            </div>
        </footer>
    </div>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
