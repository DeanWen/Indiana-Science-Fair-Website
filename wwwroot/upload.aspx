<%@ Page Title="upload" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="upload" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <!--br />
    <br />
    <div>  
        <asp:Label runat="server">Judge Information Upload</asp:Label>
        <asp:FileUpload ID="fuload" runat="server" />  
        <asp:Button ID="btnFileUpload" runat="server"   
        OnClick="btnFileUpload_Click" Text="Upload" />  
        <asp:Label ID="lbmsg" runat="server"></asp:Label>  
    </div> -->

    <br />
    <br />
    <div> 
        <asp:Label ID="Label1" runat="server">Judge Information Upload</asp:Label> 
        <asp:FileUpload ID="JudgeUpLoad" runat="server" />  
        <asp:Button ID="Judgebtn" runat="server"   
        OnClick="JudgeBtn_Click" Text="Upload" />  
        <asp:Label ID="JudgeStatus" runat="server"></asp:Label>  
    </div> 
     
    <br />
    <br />
    <div>  
        <asp:Label runat="server">Project Information Upload</asp:Label>
        <asp:FileUpload ID="ProjUpLoad" runat="server" />  
        <asp:Button ID="Projbtn" runat="server"   
        OnClick="btnFileUpload_Click" Text="Upload" />  
        <asp:Label ID="ProjStatus" runat="server"></asp:Label>  
    </div> 

    <br />
    <br />
    <div>  
        <asp:Label runat="server">Student Information Upload</asp:Label>
        <asp:FileUpload ID="StuUpLoad" runat="server" />  
        <asp:Button ID="Stubtn" runat="server"   
        OnClick="StuBtn_Click" Text="Upload" />  
        <asp:Label ID="StuStatus" runat="server"></asp:Label>  
    </div>  

</asp:Content>