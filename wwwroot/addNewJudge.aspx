<%@ Page Title="Setting" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="addNewJudge.aspx.cs" Inherits="addNewJudge" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="tab-content" id="tab2" style= "padding-left: 20px;">
        <form action="#" method="post">
            <p style="line-height: 30px">
                <asp:Label ID="LblUser" runat="server" Width="80px">Judge ID: </asp:Label>
                <asp:TextBox ID="username"  MaxLength=20 runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="username" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                <br />

<!--
                <asp:Label ID="LblPwd" runat="server" Width="80px">password: </asp:Label>
                <asp:TextBox ID="pwd"  MaxLength=20 runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="pwd" runat="server" ErrorMessage="Required Field">*Password Required</asp:RequiredFieldValidator>                                                    
                <asp:RegularExpressionValidator ID="checkPWD" runat="server"  ErrorMessage="Incorrect, must be between 6-10 bits letters or numbers ." ControlToValidate="pwd" ValidationExpression="[a-zA-Z0-9]{6,10}" />  
                <br />
-->


                <asp:Label ID="LblFirstName" runat="server" Width="80px">First name: </asp:Label>
                <asp:TextBox ID="firstname"  MaxLength=20 runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="firstname" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                <br />


                <asp:Label ID="LblLastName" runat="server" Width="80px">Last name: </asp:Label>
                <asp:TextBox ID="lastname"  MaxLength=20 runat="server"></asp:TextBox>
                <!-- Classes for input-notification: success, error, information, attention -->
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="lastname" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                <br />


            </p>                
                <asp:Label ID="Category" runat="server"> Category </asp:Label>            
                <ul>
                    <li> <asp:Label ID="Cat1" runat="server"> Category 1 </asp:Label>
                        <select id="Select1" name="D1">
                            <option>BI</option>
                            <option>CB</option>
                            <option>EA</option>
                            <option>EV</option>
                            <option>ME</option>
                            <option>MI</option>
                        </select>
                    </li>
                    <li> <asp:Label ID="Cat2" runat="server"> Category 2 </asp:Label>
                        <select id="Select2" name="D1">
                            <option></option>
                            <option>BI</option>
                            <option>CB</option>
                            <option>EA</option>
                            <option>EV</option>
                            <option>ME</option>
                            <option>MI</option>
                        </select>
                    </li>
                    <li> <asp:Label ID="Cat3" runat="server"> Category 3 </asp:Label>
                        <select id="Select3" name="D1">
                            <option></option>
                            <option>BI</option>
                            <option>CB</option>
                            <option>EA</option>
                            <option>EV</option>
                            <option>ME</option>
                            <option>MI</option>
                        </select>
                    </li>
                    <li> <asp:Label ID="Cat4" runat="server"> Category 4 </asp:Label>
                        <select id="Select4" name="D1">
                            <option></option>
                            <option>BI</option>
                            <option>CB</option>
                            <option>EA</option>
                            <option>EV</option>
                            <option>ME</option>
                            <option>MI</option>
                        </select>
                    </li>
                </ul>
                <div class="submit">
		            <asp:Button ID="Button1" class="button"  Visible="true" Text="Add Judge" runat="server" onclick="BtnSubmit_Click" />
                </div>       
                 
            
            <div class="clear"></div>
            <!-- End .clear -->
        </form>
    </div>
    <!-- End #tab2 -->
</asp:Content>
