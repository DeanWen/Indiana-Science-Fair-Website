<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" EnableSessionState="True"
    CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>
<html lang="en">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Indiana Science Fair</title>
<!--                       CSS                       -->
<!-- Reset Stylesheet -->
<link rel="stylesheet" href="resources/css/reset.css" type="text/css" media="screen" />
<!-- Main Stylesheet -->
<link rel="stylesheet" href="resources/css/login.css" type="text/css" media="screen" />
<!-- Invalid Stylesheet. This makes stuff look pretty. Remove it if you want the CSS completely valid -->
<link rel="stylesheet" href="resources/css/invalid.css" type="text/css" media="screen" />
<!--                       Javascripts                       -->
<!-- jQuery -->
<script type="text/javascript" src="resources/scripts/jquery-1.3.2.min.js"></script>
<!-- jQuery Configuration -->
<script type="text/javascript" src="resources/scripts/simpla.jquery.configuration.js"></script>
<!-- Facebox jQuery Plugin -->
<script type="text/javascript" src="resources/scripts/facebox.js"></script>
<!-- jQuery WYSIWYG Plugin -->
<script type="text/javascript" src="resources/scripts/jquery.wysiwyg.js"></script>
</head>

<body id="login">
    <div id="login-wrapper" class="png_bg">
        <div id="login-top">
            <!-- Logo (221px width) -->
            <h1><img id="logo" src="resources/images/logo.png" alt="Simpla Admin logo"/></h1>                    
        </div>
            <!-- End #logn-top -->
        <div id="login-content">
            <asp:Label  id="LblMsg" Height="60px" Font-Size="18px" ForeColor="Orange" 
                runat="server" Width="367px" />
            <form runat="server">
                <div id="login_form">
                    <table>
                        <tr>
                            <td>
			                    <asp:Label ID="LblEm" CssClass="form label" runat="server">Username: </asp:Label>
			                </td>
			                <td>
			                    <div class="text-input">
			                    <asp:TextBox ID="TxtEm" MaxLength = "50"  width="300" Height="20" Text="" runat="server"  Font-Size="12"></asp:TextBox>			                    
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label runat="server"></asp:Label></td>
                            <td>                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtEm" ErrorMessage="Required Field" Display="Dynamic">*Username Required</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="checkuname" runat="server" ErrorMessage="Incorrect, must be between 4-10 bits letters or numbers ." ControlToValidate="TxtEm" ValidationExpression="[a-zA-Z0-9]{4,10}" />                             
                            
                            </td>
                        </tr>
                        <tr>
			                <td>
			                    <asp:Label id="LblPwd" CssClass="form label" runat="server">Password: </asp:Label>			
			                </td>
			                <td>
			                    <div class="text-input">
			                    <asp:TextBox ID="TxtPwd" MaxLength = "50"  TextMode="Password" width="300" Height="20" Text="" runat="server"  Font-Size="12"></asp:TextBox>
                                </div>                
			                </td>                                
			            </tr>                 
                        <tr>
                            <td><asp:Label runat="server"></asp:Label></td>
                            <td>                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtPwd" ErrorMessage="Required Field" Display="Dynamic">*Password Required</asp:RequiredFieldValidator>                                                    
                                <asp:RegularExpressionValidator ID="checkPWD" runat="server"  ErrorMessage="Incorrect, must be between 6-10 bits letters or numbers ." ControlToValidate="TxtPwd" ValidationExpression="[a-zA-Z0-9]{6,10}" />                                                   
                            </td>
                        </tr>
                        <!-- 
                        <tr>
                            <td><asp:Label runat="server"></asp:Label></td>
                            <td>
                                <asp:CheckBox  runat="server" Text="Remember me" TextAlign=Right/> 
                            </td>
                        </tr>    -->
                         <tr>
                            <td><asp:Label runat="server"></asp:Label></td>
                            <td>                                  
                                <a href="forgetPwd.aspx">Forgot password?</a>
                            </td>
                         </tr>
                    </table>  			       
			    </div>
                <br />
                <div class="submit">
		            <asp:Button class="button"  Visible="true" Text="Submit" runat="server" onclick="BtnSubmit_Click" />
                </div>
            </form>
        </div>
    <!-- End #login-content -->
    </div>
<!-- End #login-wrapper -->
</body>
</html>
