<!-- Copyright by Indiana University Purdue University Indianapolis
  -- School of Computer & Informatic Science
  -- Dian Wen & Rui Wang
  -- 2013 Jan-May
-->

<%@ Page Title="Scheduling" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Scheduling.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="content-box-header">
        <table>
            <tr>
                <td>
                    <asp:Label ID="JidMsg" CssClass="form lable" runat="server">Enter Judge ID: </asp:Label>
                    <asp:TextBox ID="JidTxt" runat="server"></asp:TextBox>
                    <asp:Button ID="submit" class="button"  Visible="true" Text="Submit" runat="server" onclick="submitBtnClick"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="JidTxt" ErrorMessage="Required Field" Display="Dynamic">*ID Required</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="checkuname" runat="server" ErrorMessage="Incorrect, must be between 4-10 bits letters or numbers ." ControlToValidate="JidTxt" ValidationExpression="[a-zA-Z0-9]{4,10}" />
                </td>
                <td>
                <ul class="content-box-tabs">
                    <li><a href="#tab1" class="default-tab">All Matched Projects</a></li>
                    <!-- href must be unique and match the id of target div -->
                    <li><a href="#tab2">Recommendation</a></li>
                </ul>
                </td>
            </tr>
        </table>
        <div class="clear"></div>
    </div>

    <div class="content-box-content">
        <div class="tab-content default-tab" id="tab1">
            <asp:ObjectDataSource ID="pjList" runat="server" TypeName = "projDB" SelectMethod = "getAllProjByJid" >
            </asp:ObjectDataSource> 
            <asp:Panel ID="PnlTable" runat="server">
                <asp:GridView ID="ProjListGrid" runat="server" DataDourceID="pjList"
                    AutoGenerateColumns="False" ShowFooter="True" PageSize="20"
                    AllowPaging="True" OnPageIndexChanging="PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="PID" HeaderText="Project ID">
                        </asp:BoundField>
                        <asp:BoundField DataField="PName" HeaderText="Project Name">
                        </asp:BoundField>
                        <asp:BoundField DataField="CID" HeaderText="Category">                  
                        </asp:BoundField>      
                        <asp:BoundField DataField="FName" HeaderText="Student">
                        </asp:BoundField>
                        <asp:BoundField DataField="GID" HeaderText="Grade">
                        </asp:BoundField>
                        <asp:BoundField DataField="Division" HeaderText="Division">
                        </asp:BoundField>
                        <asp:BoundField DataField="Times" HeaderText="Times">
                        </asp:BoundField>
                    </Columns>

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
        </div>

        <div class="tab-content" id="tab2">
            <asp:ObjectDataSource ID="recommendList" runat="server" TypeName = "projDB" SelectMethod = "recommendProjById">
            </asp:ObjectDataSource>
            <asp:Panel ID="Panel1" runat="server">
                <asp:GridView ID="RecProjGrid" runat="server" 
                    AutoGenerateColumns="False" ShowFooter="True" PageSize="20"
                    AllowPaging="True" OnPageIndexChanging="PageIndexChanging" AutoGenerateSelectButton="true" OnSelectedIndexChanged="RecProj_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="PID" HeaderText="Project ID">
                        </asp:BoundField>
                        <asp:BoundField DataField="PName" HeaderText="Project Name">
                        </asp:BoundField>
                        <asp:BoundField DataField="CID" HeaderText="Category">                  
                        </asp:BoundField>      
                        <asp:BoundField DataField="FName" HeaderText="Student">
                        </asp:BoundField>
                        <asp:BoundField DataField="GID" HeaderText="Grade">
                        </asp:BoundField>
                        <asp:BoundField DataField="Division" HeaderText="Division">                        
                        </asp:BoundField>
                        <asp:BoundField DataField="Times" HeaderText="Times">
                        </asp:BoundField>
                        <asp:BoundField DataField="Weight" HeaderText="Weight">
                        </asp:BoundField>
                    </Columns>

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
        </div>
    </div>
</asp:Content>
