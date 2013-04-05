﻿<%@ Page Title="Assignment" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Assignment.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="SearchMsg" CssClass="form lable" runat="server">Search by: </asp:Label>
    <asp:DropDownList ID="SearchArea"  runat="server">
        <asp:ListItem Value="JID">JudgeID</asp:ListItem>
        <asp:ListItem Value="PID">ProjectId</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="SearchTxt" OnTextChanged="getAssignment" runat="server"></asp:TextBox>
    <asp:Button ID="search" class="button"  Visible="true" Text="Search" runat="server" onclick="getAssignment" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="SearchTxt" ErrorMessage="Required Field" Display="Dynamic">*ID Required</asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="checkuname" runat="server" ErrorMessage="Incorrect, must be between 1-10 bits letters or numbers ." ControlToValidate="SearchTxt" ValidationExpression="[a-zA-Z0-9]{1,10}" />

       <asp:Panel ID="PnlTable" runat="server">
       <asp:GridView ID="Grid1" runat="server"  AutoGenerateColumns="False" AllowPaging="true" 
           OnPageIndexChanging="PageIndexChanging" AutoGenerateDeleteButton="true"
            AutoGenerateEditButton="true" OnRowCancelingEdit="Grid1_RowCancelingEdit" 
           OnRowDeleting="Grid1_RowDeleting" OnRowEditing="Grid1_RowEditing"
            OnRowUpdating="Grid1_RowUpdating" DataKeyNames="AID">
            <columns>
            
            <asp:templatefield headertext="assignment ID">
                <itemtemplate>
                    <asp:label id="aid" runat='server' Text='<%#Eval("Aid") %>'/>                  
                </itemtemplate>
            </asp:templatefield>
            
            <asp:templatefield headertext="Score"><itemtemplate> <%#Eval("score") %></itemtemplate> 
                <edititemtemplate>
                    <asp:textbox id="textbox1" runat='server' text='<%#Eval("score") %>'></asp:textbox>
                </edititemtemplate>
            </asp:templatefield>
            
            <asp:templatefield headertext="Judge"><itemtemplate><%#Eval("jid") %> </itemtemplate>
           <edititemtemplate>
           <asp:textbox id="textbox2" runat='server' text='<%#Eval("jid") %>'></asp:textbox>
           </edititemtemplate>
            </asp:templatefield>
            
            <asp:templatefield headertext="Project"><itemtemplate><%#Eval("pid") %> </itemtemplate>
           <edititemtemplate>
           <asp:textbox id="textbox3" runat='server' Text='<%#Eval("pid") %>'></asp:textbox>
           </edititemtemplate>
            </asp:templatefield>

            <asp:templatefield headertext="Period "><itemtemplate><%#Eval("periodID") %> </itemtemplate>
           <edititemtemplate>
           <asp:textbox id="textbox4" runat='server' text='<%#Eval("periodID") %>'></asp:textbox>
           </edititemtemplate>
            </asp:templatefield>

            </columns>

           <PagerTemplate>
                            <table align="right" bgcolor="#e9e9e9" width="100%">
                                <tr>
                                    <td style="text-align: right">
                                        Page <b><asp:Label ID="lblPageIndex" runat="server" Text="<%#((GridView)Container.Parent.Parent).PageIndex + 1 %>"></asp:Label></b>
                                        of <b><asp:Label ID="lblPageCount" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageCount %>"></asp:Label></b>
                                        <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First"
                                            CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>"
                                            Text="First  "></asp:LinkButton>
                                        <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev"
                                            CommandName="Page" Enabled=" <%# ((GridView)Container.NamingContainer).PageIndex != 0 %>"
                                            Text="<< Previous  "></asp:LinkButton>
                                        <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next"
                                            CommandName="Page" Enabled=" <%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>"
                                            Text="  Next >>"></asp:LinkButton>
                                        <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last"
                                            CommandName="Page" Enabled=" <%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>"
                                            Text="  Last"></asp:LinkButton>
                                        <asp:TextBox ID="txtNewPageIndex" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageIndex + 1%>"
                                            Width="20px"></asp:TextBox>
                                        <asp:LinkButton ID="btnGo" runat="server" CausesValidation="false" CommandArgument="-1"
                                            CommandName="Page" Text="GO"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </PagerTemplate>
       </asp:GridView>

   </asp:Panel>
</asp:Content>