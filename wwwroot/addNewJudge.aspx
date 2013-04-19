<%@ Page Title="Add Juge" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="addNewJudge.aspx.cs" Inherits="addNewJudge" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="tab-content" id="tab2" style= "padding-left: 20px;">
        <form action="#" method="post">
           <fieldset>
               <table style="width: 100%">
                <h1>General Information:</h1>
            <p style="line-height: 30px">
               
               <td style="width: 140px">Judge ID:</td>
               <td style="width: 206px">
               
                <asp:TextBox ID="username"  MaxLength=20 runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="username" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="checkuname" runat="server" ErrorMessage="Incorrect, must be between 4-10 bits numbers ." ControlToValidate="username" ValidationExpression="[0-9]{4,10}" />               
               </td>
                
            <tr>
               <td style="width: 140px">First Name:</td>
               <td style="width: 206px">
                <asp:TextBox ID="firstname"  MaxLength=20 runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="firstname" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="firstnameChecker" runat="server" ErrorMessage="Invaild input" ControlToValidate="firstname" ValidationExpression="^[a-zA-Z\s]+$" />
                </td>
            </tr>

            <tr>
               <td style="width: 140px">Last Name:</td>
               <td style="width: 206px">
                
                <asp:TextBox ID="lastname"  MaxLength=20 runat="server"></asp:TextBox>
                <!-- Classes for input-notification: success, error, information, attention -->
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="lastname" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="lastnameChecker" runat="server" ErrorMessage="Invaild input" ControlToValidate="lastname" ValidationExpression="^[a-zA-Z\s]+$" />
                </td>
            </tr>


            <tr>           
                 <td style="width: 140px">Category A:</td>
                          <td style="width: 206px">
                              <asp:DropDownList ID="CategoryA" runat="server" Font-Size="10pt" Height="20pt">
                                  <asp:ListItem Value="AS">AS</asp:ListItem>
                                  <asp:ListItem Value="CB">CB</asp:ListItem>
                                  <asp:ListItem Value="CS">CS</asp:ListItem>
                                  <asp:ListItem Value="EE">EE</asp:ListItem>
                                  <asp:ListItem Value="EN">EN</asp:ListItem>
                                  <asp:ListItem Value="EV">EV</asp:ListItem>
                                  <asp:ListItem Value="ME">ME</asp:ListItem>
                                  <asp:ListItem Value="PH">PH</asp:ListItem>
                                  <asp:ListItem Value="CH">CH</asp:ListItem>
                                  <asp:ListItem Value="EM">EM</asp:ListItem>
                                  <asp:ListItem Value="MA">MA</asp:ListItem>
                                  <asp:ListItem Value="MH">MH</asp:ListItem>
                                  <asp:ListItem Value="PS">PS</asp:ListItem>
                                  <asp:ListItem Value="ET">ET</asp:ListItem>
                                  <asp:ListItem Value="BE">BE</asp:ListItem>
                                  <asp:ListItem Value="BI">BI</asp:ListItem>
                                  <asp:ListItem Value="EA">EA</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td style="width: 140px">Category B:</td>
                          <td style="width: 206px">
                              <asp:DropDownList ID="CategoryB" runat="server" Font-Size="10pt" Height="20pt">
                                   <asp:ListItem Value="AS">AS</asp:ListItem>
                                  <asp:ListItem Value="CB">CB</asp:ListItem>
                                  <asp:ListItem Value="CS">CS</asp:ListItem>
                                  <asp:ListItem Value="EE">EE</asp:ListItem>
                                  <asp:ListItem Value="EN">EN</asp:ListItem>
                                  <asp:ListItem Value="EV">EV</asp:ListItem>
                                  <asp:ListItem Value="ME">ME</asp:ListItem>
                                  <asp:ListItem Value="PH">PH</asp:ListItem>
                                  <asp:ListItem Value="CH">CH</asp:ListItem>
                                  <asp:ListItem Value="EM">EM</asp:ListItem>
                                  <asp:ListItem Value="MA">MA</asp:ListItem>
                                  <asp:ListItem Value="MH">MH</asp:ListItem>
                                  <asp:ListItem Value="PS">PS</asp:ListItem>
                                  <asp:ListItem Value="ET">ET</asp:ListItem>
                                  <asp:ListItem Value="BE">BE</asp:ListItem>
                                  <asp:ListItem Value="BI">BI</asp:ListItem>
                                  <asp:ListItem Value="EA">EA</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td style="width: 140px">Category C:</td>
                          <td style="width: 206px">
                              <asp:DropDownList ID="CategoryC" runat="server" Font-Size="10pt" Height="20pt">
                                  <asp:ListItem Value="AS">AS</asp:ListItem>
                                  <asp:ListItem Value="CB">CB</asp:ListItem>
                                  <asp:ListItem Value="CS">CS</asp:ListItem>
                                  <asp:ListItem Value="EE">EE</asp:ListItem>
                                  <asp:ListItem Value="EN">EN</asp:ListItem>
                                  <asp:ListItem Value="EV">EV</asp:ListItem>
                                  <asp:ListItem Value="ME">ME</asp:ListItem>
                                  <asp:ListItem Value="PH">PH</asp:ListItem>
                                  <asp:ListItem Value="CH">CH</asp:ListItem>
                                  <asp:ListItem Value="EM">EM</asp:ListItem>
                                  <asp:ListItem Value="MA">MA</asp:ListItem>
                                  <asp:ListItem Value="MH">MH</asp:ListItem>
                                  <asp:ListItem Value="PS">PS</asp:ListItem>
                                  <asp:ListItem Value="ET">ET</asp:ListItem>
                                  <asp:ListItem Value="BE">BE</asp:ListItem>
                                  <asp:ListItem Value="BI">BI</asp:ListItem>
                                  <asp:ListItem Value="EA">EA</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td style="width: 140px">Category D:</td>
                          <td style="width: 206px">
                              <asp:DropDownList ID="CategoryD" runat="server" Font-Size="10pt" Height="20pt">
                                  <asp:ListItem Value="AS">AS</asp:ListItem>
                                  <asp:ListItem Value="CB">CB</asp:ListItem>
                                  <asp:ListItem Value="CS">CS</asp:ListItem>
                                  <asp:ListItem Value="EE">EE</asp:ListItem>
                                  <asp:ListItem Value="EN">EN</asp:ListItem>
                                  <asp:ListItem Value="EV">EV</asp:ListItem>
                                  <asp:ListItem Value="ME">ME</asp:ListItem>
                                  <asp:ListItem Value="PH">PH</asp:ListItem>
                                  <asp:ListItem Value="CH">CH</asp:ListItem>
                                  <asp:ListItem Value="EM">EM</asp:ListItem>
                                  <asp:ListItem Value="MA">MA</asp:ListItem>
                                  <asp:ListItem Value="MH">MH</asp:ListItem>
                                  <asp:ListItem Value="PS">PS</asp:ListItem>
                                  <asp:ListItem Value="ET">ET</asp:ListItem>
                                  <asp:ListItem Value="BE">BE</asp:ListItem>
                                  <asp:ListItem Value="BI">BI</asp:ListItem>
                                  <asp:ListItem Value="EA">EA</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td style="width: 140px">Division:</td>
                          <td style="width: 206px">
                              <asp:DropDownList ID="Division" runat="server" Font-Size="10pt" Height="20pt">
                                  <asp:ListItem Value="J">Junior</asp:ListItem>
                                  <asp:ListItem Value="S">Senior</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                      </tr>     
                 
            </p>
                </table>
               </fieldset>
            <br />
              <div class="submit">
		            <asp:Button ID="Button1" class="button"  Visible="true" Text="Add Judge" runat="server" onclick="BtnSubmit_Click" />
                    <asp:Button ID="Button2" class="button"  Visible="true" Text="Clear" runat="server" OnClientClick="this.form.reset();return false;" />
                </div>  
            <div class="clear"></div>
            <!-- End .clear -->
        </form>
    </div>
    <!-- End #tab2 -->
</asp:Content>
