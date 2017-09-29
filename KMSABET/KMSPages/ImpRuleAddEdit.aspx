<%@ Page Title="Add Rule" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImpRuleAddEdit.aspx.cs" Inherits="KMSABET.KMSPages.ImpRuleAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Rule Details</h3>
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
        <tr>   
            <th><asp:Label Text="Rule Statement: " runat="server"></asp:Label></th>
            <td><asp:TextBox ID="ruleStmt" TextMode="MultiLine" Columns="50" Rows="5" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ruleStmt"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
        </tr>
    </table>

    <br />
    <h4>Rule Case Definition</h4>
    <asp:DataList ID="DataList1" runat="server" EnableViewState="False" onitemdatabound="DataList1_ItemDataBound">
        <ItemTemplate>
            <table>
                <tr>
                    <th colspan="2">
                        <asp:HiddenField runat="server" ID="ruleQuestionIDHidden" />
                        <asp:HiddenField runat="server" ID="ruleQuestionTypeIDHidden" />
                        <asp:Label runat="server" ID="ruleQuestionLabel" Font-Bold="true" Text='<%# "Question : " + Eval("ruleQuestionStatemet") %>' />
                    </th>
                </tr>
                <asp:Panel runat="server" ID="QueryPanelOne">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="dbQueryValueLabel" Text="<b>Comparison Value (x) : </b><br/><p style='font-size:0.8em'>(Where y will be calculated by system)</p>" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="dbQueryValue" />
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <asp:radiobuttonlist id="radlstPubs" runat="server" />
            <br />
        </ItemTemplate>
    </asp:DataList>
    <br/>
    <asp:Button id="Button1" CssClass="btn btn-default" Text="Submit" onclick="Submit_Button_Click1" runat="server"/>
    <asp:Button runat="server" CssClass="btn btn-default" Text="Cancel" ID="cancelBtn" CausesValidation="false" PostBackUrl="~/KMSPages/ImpRuleList.aspx"/>
</asp:Content>
