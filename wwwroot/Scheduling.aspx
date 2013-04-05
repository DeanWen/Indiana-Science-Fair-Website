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
                    <asp:TextBox ID="JidTxt" OnTextChanged="getPjByJid" runat="server"></asp:TextBox>
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
            <asp:Panel ID="PnlTable" runat="server">
                <asp:GridView ID="ProjListGrid" runat="server"
                   AllowSorting="True" AutoGenerateColumns="False" ShowFooter="True" PageSize="20"
                   AllowPaging="True" OnPageIndexChanging="PageIndexChanging" AutoGenerateSelectButton="true" OnSelectedIndexChanged="ProjListGrid_SelectedIndexChanged">
                   <Columns>
                        <asp:BoundField DataField="pid" HeaderText="Project ID" SortExpression="pid">
                        </asp:BoundField>
                        <asp:BoundField DataField="pname" HeaderText="Project Name">
                        </asp:BoundField>
                        <asp:BoundField DataField="cid" HeaderText="Category">                  
                        </asp:BoundField>      
                        <asp:BoundField DataField="FName" HeaderText="Student">
                        </asp:BoundField>
                        <asp:BoundField DataField="gid" HeaderText="Grade">
                        </asp:BoundField>
                        <asp:BoundField DataField="division" HeaderText="division">
                        </asp:BoundField>
                    </Columns>
               </asp:GridView>
            </asp:Panel>
        </div>
        <div class="tab-content" id="tab2">
            <asp:Panel ID="Panel1" runat="server">
                <asp:GridView ID="RecProj" runat="server"
                   AllowSorting="True" AutoGenerateColumns="False" ShowFooter="True" PageSize="20"
                   AllowPaging="True" OnPageIndexChanging="PageIndexChanging" AutoGenerateSelectButton="true" OnSelectedIndexChanged="RecProj_SelectedIndexChanged">
                   <Columns>
                        <asp:BoundField DataField="pid" HeaderText="Project ID" SortExpression="pid">
                        </asp:BoundField>
                        <asp:BoundField DataField="pname" HeaderText="Project Name">
                        </asp:BoundField>
                        <asp:BoundField DataField="cid" HeaderText="Category">                  
                        </asp:BoundField>      
                        <asp:BoundField DataField="FName" HeaderText="Student">
                        </asp:BoundField>
                        <asp:BoundField DataField="gid" HeaderText="Grade">
                        </asp:BoundField>
                        <asp:BoundField DataField="division" HeaderText="Division">
                        </asp:BoundField>
                    </Columns>
               </asp:GridView>
            </asp:Panel>
        </div>
    </div>

<!--
    <table>
        <thead>
            <tr>
                <th>Projest ID</th>
                <th>Project Name</th>
                <th>Category</th>
                <th>Division</th>
                <th>Student</th>
                <th>Grade</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>100</td>
                <td>Space vs. No Space: Competition for Oviposition Sites in Bean </td>
                <td>AS</td>
                <td>J</td>
                <td>Adam Zaher</td>
                <td>6</td>
            </tr>
            <tr>
                <td>101</td>
                <td>What Effect Does Temperature Have on Yeast Rising?</td>
                <td>CB</td>
                <td>J</td>
                <td>Alainna Wright</td>
                <td>5</td>
            </tr>
            <tr>
                <td>102</td>
                <td>How Much Shake Will It Take?</td>
                <td>EN</td>
                <td>J</td>
                <td>Anthea Weng</td>
                <td>7</td>
            </tr>
            <tr>
                <td>103</td>
                <td>Does the color of the food affect the taste?</td>
                <td>EV</td>
                <td>J</td>
                <td>Anthony Weng</td>
                <td>6</td>
            </tr>
            <tr>
                <td>104</td>
                <td>Phalanges</td>
                <td>ME</td>
                <td>J</td>
                <td>Ashley Turner</td>
                <td>7</td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="6">
                    <div class="bulk-actions align-left">
                        <a class="button" href="#">Sort by</a>
                        <select name="dropdown">
                            <option value="option1">Category</option>
                            <option value="option2">Division</option>
                            <option value="option3">Project ID</option>
                        </select>                         
                    </div>
                    <div class="pagination"> <a href="#" title="First Page">&laquo; First</a><a href="#" title="Previous Page">&laquo; Previous</a> <a href="#" class="number current" title="1">1</a> <a href="#" class="number" title="2">2</a> <a href="#" class="number" title="3">3</a> <a href="#" class="number" title="4">4</a> <a href="#" title="Next Page">Next &raquo;</a><a href="#" title="Last Page">Last &raquo;</a> </div>
 -->                   <!-- End .pagination -->
<!--                    <div class="clear"></div>
                </td>
            </tr>
        </tfoot>
    </table>-->
</asp:Content>
