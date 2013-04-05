<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgetPwd.aspx.cs" Inherits="forgetPwd" %>

<!DOCTYPE html>
<html lang="en">

<head>
<meta http-equiv="Content-Type" content="text/html;charset=iso-8859-1" />
<meta http-equiv="Content-Style-Type" content="text/css" />

<title>
Forget Password
</title>

<!--link rel="stylesheet" href="resources/css/style.css" type="text/css" media="screen" /-->
<link rel="stylesheet" href="resources/css/forgetPwd.css" type="text/css" media="screen" />
</head>

<body>

<div id="container">
	<!-- This line tells where to insert the reusable code -->
	<div id="main_content">
		<!-- Start of forgetPwd -->
		<div id="forgetPwd">
			<form ID="Frm1" runat="server">			
			    <div id="forgetPwd_form">			        
			        <asp:Panel ID="Pnl1" runat="server" >
			        <div id="forgetPwd_info">                             
			            <h1>Forget Password?</h1>
                        <div id="not_forget">
                            <h2>Want to <a href="login.aspx">Log In</a></h2>
                        </div>
			            <h2>Enter the email you registered:</h2>			
			         </div>
			         <asp:Label ID="LblMsg1" Font-Size="16px" ForeColor="Orange" Height="20px" 
                            runat="server" ></asp:Label>		

			         <table>
			            <tr>
			                <td>
			                <asp:Label id="LblEm" CssClass="forgetPwd_label" runat="server">Email: </asp:Label>
			                </td>
			                <td>
			                    <div class="forgetPwd_input">
			                        <asp:TextBox ID="TxtEm" MaxLength = "50"  width="300" Height="20" Text="" runat="server"  Font-Size="12"></asp:TextBox>
			                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtEm" ErrorMessage="Required Field" Display="Dynamic">*Required</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        ErrorMessage="RegularExpressionValidator" BorderStyle="None" ControlToValidate="TxtEm">*Not an Email format</asp:RegularExpressionValidator>
			                    </div>
			                </td>
			            </tr>
			
			
			        </table>
			        <div id="Div1" style="margin-top: 20px; margin-left: 150px;" >
		                <asp:Button class="button" ForeColor="White" BackColor="#9e9ec3" Font-Size="12" Font-Bold="true" 
                            ID="BtnEnter" CausesValidation="true"  Visible="true" Text="Enter" 
                            runat="server" onclick="BtnEnter_Click" Width="100px" />		   
			        </div>			        
			        </asp:Panel> <!--End of the first panel -->
			        <br /><br />				
		    </div><!-- end of forgetPwd_form -->
	    </form>

      
		<div class="clearthis">&nbsp;</div>
		</div><!-- End of forgetPwd -->
        </div>
	</div><!-- End of Main Content Area -->


	<div class="clearthis">&nbsp;</div>


	<!-- This line tells where to insert the reusable general frame code -->

<!-- End of container -->

</body>
</html>

