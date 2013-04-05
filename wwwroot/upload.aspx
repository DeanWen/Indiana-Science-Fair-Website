<%@ Page Title="upload" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="upload" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <div>  
        <asp:FileUpload ID="fuload" runat="server" />  
        <asp:Button ID="btnFileUpload" runat="server"   
        OnClick="btnFileUpload_Click" Text="Upload" />  
        <asp:Label ID="lbmsg" runat="server"></asp:Label>  
    </div>  
 

</asp:Content>