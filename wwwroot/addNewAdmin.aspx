<%@ Page Title="addNewAdmin" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="addNewAdmin.aspx.cs" Inherits="newAdmin" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="tab-content" id="tab2" style= "padding-left: 20px;">
        <form action="#" method="post">
            <p style="line-height: 30px">
                <asp:Label ID="LblUser" runat="server" Width="80px">User name: </asp:Label>
                <asp:TextBox ID="username"  MaxLength=20 runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="username" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                <br />

                <asp:Label ID="Label1" runat="server" Width="80px">password: </asp:Label>
                <asp:TextBox ID="right"  MaxLength=20 runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="right" runat="server" ErrorMessage="Required Field">*right Required</asp:RequiredFieldValidator>                                                    
                <br />

            </p> 
                              
                <div class="submit">
		            <asp:Button ID="Button1" class="button"  Visible="true" Text="Add admin" runat="server" onclick="BtnSubmit_Click1" />
                </div>                                  
            <div class="clear"></div>
            <!-- End .clear -->
        </form>
    </div>
    <!-- End #tab2 -->
</asp:Content>
