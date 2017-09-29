<%@ Page Title="Ask for Improvement" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImpAskSuggestionTwo.aspx.cs" Inherits="KMSABET.KMSPages.ImpAskSuggestionTwo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Improvement Plan</h3>
    <asp:DataList ID="DataList1" runat="server" EnableViewState="False" onitemdatabound="DataList1_ItemDataBound">
        <ItemTemplate>
            <table>
                <tr>
                    <th>
                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text='<%# "Question : " %>' />
                    </th>
                    <td>
                        <asp:Label runat="server" ID="Label2" Text='<%# Eval("questionStatement") %>' />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Label runat="server" ID="dbQuestionDDLLabel" Font-Bold="true" Text='<%# Eval("headingLabel") + " : " %>' />
                    </th>
                    <td>
                        <asp:HiddenField ID="questionIDHidden" runat="server" Value='<%# Eval("questionID") %>' />
                        <asp:HiddenField ID="jsonStringHidden" runat="server" Value='<%# Eval("whereAskUserToReplace") %>' />
                        <asp:DropDownList id="ddlItemTemp" runat="server" />
                    </td>
                </tr>
            </table>
            <br/>
        </ItemTemplate>
    </asp:DataList>
    <asp:Button id="Button1" CssClass="btn btn-default" Text="Next" onclick="Button1_Click" runat="server"/>
    <a href="ImpAskSuggestionOne.aspx" class="btn btn-default">Back</a>
</asp:Content>
