<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QueAttributeAddViewEdit.aspx.cs" Inherits="KMSABET.KMSPages.QueAttributeView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Attribute Details</h3>
    <table style="padding:4px;">
        <tr>
            <th><asp:Label ID="attributeStmt" Text="Attribute Type: " runat="server"></asp:Label></th>
            <td><asp:RadioButtonList id="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow" TextAlign="Right">
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadioButtonList1"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>   
            <th><asp:Label Text="Attribute Statement: " runat="server"></asp:Label></th>
            <td><asp:TextBox Columns="37" Rows="4" TextMode="MultiLine" ID="attStmt" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="attStmt"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>   
            <th><asp:Label Text="Attribute Weightage: " runat="server"></asp:Label></th>
            <td><asp:TextBox ID="attWeight" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="attStmt"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>   
            <th><asp:Label Text="Is Relevance Applicable: " runat="server"></asp:Label></th>
            <td><asp:CheckBox ID="CheckBoxRelevance" runat="server" Width="300px"></asp:CheckBox></td>
        </tr>
    </table>
    <br />
    <h4>Attribute Options</h4>
    <asp:GridView ID="AttributeOptions" runat="server"
        ShowFooter="true" AutoGenerateColumns="False"
        OnRowDeleting="AttributeOptions_RowDeleting"
        CellPadding="4">
        <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="Sr. No." />
            <asp:TemplateField HeaderText="Priority">
                <ItemTemplate>
                    <asp:TextBox ID="priorityNo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="priorityNo"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="ButtonAdd" CssClass="btn btn-default" CausesValidation="false"
                        runat="server" Text="Add Option" OnClick="ButtonAdd_Click" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Option Statement">
                <ItemTemplate>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                        ErrorMessage="*"></asp:RequiredFieldValidator>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField HeaderText="Action" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:Label runat="server" ID="cautionLbl" 
        Text="Caution: Updating the Attribute will delete all assignments of the attribute options to the question(s)."></asp:Label>
    <br />
    <asp:Button id="Button1" CssClass="btn btn-default" Text="Submit" onclick="Button1_Click" runat="server"/>
    <asp:Button runat="server" CssClass="btn btn-default" ID="CancelBtnId" Text="Cancel" CausesValidation="false" PostBackUrl="~/KMSPages/QueAttributeList.aspx"/>
</asp:Content>
