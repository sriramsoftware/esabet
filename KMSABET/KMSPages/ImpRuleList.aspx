<%@ Page Title="Rule List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImpRuleList.aspx.cs" Inherits="KMSABET.KMSPages.ImpRuleList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Rule List</h3>
    <a href="ImpRuleAdd.aspx" class="btn btn-default">Add Rule</a>
    <a href="ImpPlanMenu.aspx" class="btn btn-default">Return to Main Menu</a>
    <br/><br/>
    <asp:GridView ID="attributeListTag" runat="server" Width="100%"
        AutoGenerateColumns="False" 
        AllowPaging="true" OnPageIndexChanging="grdData_PageIndexChanging"
        ItemType="KMSABET.MyPocos.ImpRule"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Sr. No."> 
                <ItemTemplate>
                    <%#Container.DataItemIndex+1%>       
                </ItemTemplate>
                <ItemStyle Width="60px" />                                                          
            </asp:TemplateField>
            <asp:BoundField DataField="ruleStatemet" HeaderText="Rule Statement"/>
            <asp:BoundField DataField="cloData.cloStatement" HeaderText= "CLO"/>
            <asp:BoundField DataField="cloData.courseName" HeaderText="Course"/>
            <asp:BoundField DataField="cloData.programName" HeaderText="Program"/>
            <asp:TemplateField HeaderText="View Details"> 
                <ItemTemplate>
                    <a href="ImpRuleAddEdit.aspx?id=<%#: Item.ruleId %>&viewMode=true&editMode=false&deleteMode=false" > View </a>
                </ItemTemplate>
                <ItemStyle Width="100px" />                                                          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit"> 
                <ItemTemplate>
                    <a href="ImpRuleAddEdit.aspx?id=<%#: Item.ruleId %>&viewMode=false&editMode=true&ddeleteMode=false" > Edit </a>
                </ItemTemplate>
                <ItemStyle Width="100px" />                                                          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete"> 
                <ItemTemplate>
                    <a href="ImpRuleAddEdit.aspx?id=<%#: Item.ruleId %>&viewMode=false&editMode=false&deleteMode=true" > Delete </a>
                </ItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
</asp:Content>
