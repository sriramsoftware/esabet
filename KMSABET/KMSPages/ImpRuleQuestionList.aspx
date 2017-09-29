<%@ Page Title="Rule Question List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImpRuleQuestionList.aspx.cs" Inherits="KMSABET.KMSPages.ImpRuleQuestionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Rule Question List</h3>
    <a href="ImpRuleQuestionAddEditView.aspx" class="btn btn-default">Add Rule Question</a>
    <a href="ImpPlanMenu.aspx" class="btn btn-default">Return to Main Menu</a>
    <br/><br/>
    <asp:GridView ID="attributeListTag" runat="server" Width="100%"
        AutoGenerateColumns="False" 
        AllowPaging="True" OnPageIndexChanging="grdData_PageIndexChanging"
        ItemType="KMSABET.MyPocos.ImpRuleQuestion"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Sr. No."> 
                <ItemTemplate>
                    <%#Container.DataItemIndex+1%>       
                </ItemTemplate>
                <ItemStyle Width="60px" />                                                          
            </asp:TemplateField>
            <asp:BoundField DataField="ruleQuestionStatemet" HeaderText="Question Statement"/>
            <asp:BoundField DataField="ruleQuesType" HeaderText="Type"/>
            <asp:BoundField DataField="cloData.cloStatement" HeaderText="CLO"/>
            <asp:BoundField DataField="cloData.courseName" HeaderText="Course"/>
            <asp:BoundField DataField="cloData.programName" HeaderText="Program"/>
            <asp:TemplateField HeaderText="View Details"> 
                <ItemTemplate>
                    <a href="ImpRuleQuestionAddEditView.aspx?id=<%#: Item.ruleQuesId %>&viewMode=true&editMode=false&delete=false&duplicate=false" > View </a>
                </ItemTemplate>
                <ItemStyle Width="100px" />                                                          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Duplicate"> 
                <ItemTemplate>
                    <a href="ImpRuleQuestionAddEditView.aspx?id=<%#: Item.ruleQuesId %>&viewMode=false&editMode=false&delete=false&duplicate=true" > Copy </a>
                </ItemTemplate>
                <ItemStyle Width="100px" />                                                          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit"> 
                <ItemTemplate>
                    <a href="ImpRuleQuestionAddEditView.aspx?id=<%#: Item.ruleQuesId %>&viewMode=false&editMode=true&delete=false&duplicate=false" > Edit </a>
                </ItemTemplate>
                <ItemStyle Width="100px" />                                                          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete"> 
                <ItemTemplate>
                    <a href="ImpRuleQuestionAddEditView.aspx?id=<%#: Item.ruleQuesId %>&viewMode=false&editMode=false&delete=true&duplicate=false" > Delete </a>
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
