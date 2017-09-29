<%@ Page Title="Ask for Improvement" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImpAskSuggestion.aspx.cs" Inherits="KMSABET.KMSPages.ImpAskSuggestion1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Improvement Plan</h3>
    <table style="padding:4px;">
        <tr>   
            <th><asp:Label Text="Program: " runat="server"></asp:Label></th>
            <td><asp:Label ID="TextBox1" runat="server"></asp:Label></td>
        </tr>
        <tr>   
            <th><asp:Label Text="Course: " runat="server"></asp:Label></th>
            <td><asp:Label ID="TextBox2" runat="server"></asp:Label></td>
        </tr>
        <tr>   
            <th><asp:Label Text="CLO: " runat="server"></asp:Label></th>
            <td><asp:Label ID="TextBox3" runat="server"></asp:Label></td>
        </tr>
    </table>
    
    <br/>
    <asp:Label runat="server" Font-Size="Large" id="expertSugLbl">Expert's Suggestions</asp:Label>
    <asp:GridView ID="ruleListTag" runat="server"
        AutoGenerateColumns="False" Width="100%"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <EditRowStyle BackColor="#2461BF" />
        <EmptyDataTemplate>
            No Suggestion Available
        </EmptyDataTemplate> 
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Sr. No."> 
                    <ItemTemplate>
                    <%#Container.DataItemIndex+1%>       
                    </ItemTemplate>
                    <ItemStyle Width="60px" />
            </asp:TemplateField>
            <asp:BoundField DataField="ruleId" Visible="false"/>
            <asp:BoundField DataField="ruleStatemet" HeaderText="Suggestion"/>
        </Columns>
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
    
    <h4>Case Definition</h4>
    <asp:DataList ID="DataList1" runat="server" EnableViewState="False" onitemdatabound="DataList1_ItemDataBound">
        <ItemTemplate>
            <asp:Label Font-Bold="true" ID="ProductNameLabel2" runat="server"
                Text='<%# "Question : " + Eval("ruleQuestionStatemet") %>' />
            <asp:HiddenField ID="hiddenQuesType" runat="server" />
            <asp:HiddenField ID="hiddenQuesID" runat="server" />
            <asp:HiddenField ID="HiddenCalculatedValue" runat="server" />
            <asp:radiobuttonlist id="radlstPubs" runat="server" />
            <br/>
        </ItemTemplate>
        <FooterTemplate>
            <asp:Label Visible='<%#bool.Parse((DataList1.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="No Record Found!"></asp:Label>
        </FooterTemplate>
    </asp:DataList>
    <br/>
    <asp:Button runat="server" CssClass="btn btn-default" Text="Previous" ID="cancelBtn" CausesValidation="false" PostBackUrl="~/KMSPages/ImpAskSuggestionOne.aspx"/>
    <asp:Button id="Button1" CssClass="btn btn-default" Text="Formulate" onclick="Submit_Button_Click1" runat="server"/>
    <a href="ImpPlanMenu.aspx" class="btn btn-default">Close</a>
</asp:Content>
