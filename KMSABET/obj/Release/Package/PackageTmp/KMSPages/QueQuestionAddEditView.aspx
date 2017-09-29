<%@ Page Title="Add Question" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QueQuestionAddEditView.aspx.cs" Inherits="KMSABET.KMSPages.CQIQuestionAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .rbl input[type="radio"]
        {
            margin-left: 10px;
            margin-right: 1px;
        }
    </style>
    <h3>Question Details</h3>
            <asp:Label Text="Question Statement: " Font-Bold="true" runat="server"></asp:Label>
            <br />
            <asp:TextBox Columns="90" Rows="4" TextMode="MultiLine" ID="quesStmt" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="quesStmt"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <br />
            <hr />
            <h4>Assign Attributes to Question</h4>
            <hr />
            <asp:Label Text="CLO: " Font-Bold="true" runat="server"></asp:Label>
            <br />
            <asp:RadioButtonList ID="DropDownList3" runat="server" AutoPostBack="True"
                OnSelectedIndexChanged="ddlClo_SelectedIndexChanged" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DropDownList3"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <div style="float:left">
                <asp:Label Font-Bold="true" ID="Label2" runat="server"
                Text='Relevance of the question to the selected CLO : ' />
            </div>
            <div style="float:left">
                <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="rbl" runat="server" ID="TextBox1"/>
            </div>
            <br />
            <hr />
            <asp:Label Text="SO: " Font-Bold="true" runat="server"></asp:Label>
            <asp:Label Text="(Only the SOs that are relevant to the selected CLO are displayed in the following)" Font-Size="Small" Font-Bold="true" runat="server"></asp:Label>
            <asp:Label Text=":" Font-Bold="true" runat="server"></asp:Label>
            <br />
            <asp:RadioButtonList ID="DropDownList4" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DropDownList4"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <div style="float:left">        
                <asp:Label Font-Bold="true" ID="Label3" runat="server"
                    Text='Relevance of the question to the selected SO : ' />
            </div>
            <div style="float:left">
                <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="rbl" runat="server" ID="TextBox2"/>
            </div>
            <hr />
            
            <asp:Label Text="Course Topic: " Font-Bold="true" runat="server"></asp:Label>
            <asp:Label Text="(Only the Course Topics that are relevant to the selected CLO are displayed in the following)" Font-Size="Small" Font-Bold="true" runat="server"></asp:Label>
            <asp:Label Text=":" Font-Bold="true" runat="server"></asp:Label>
            <br />
            <asp:RadioButtonList ID="DropDownList5" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DropDownList5"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <div style="float:left">
                <asp:Label Font-Bold="true" ID="Label11" runat="server"
                            Text='Relevance of the question to the selected Course Topic : ' />
            </div>
            <div style="float:left">
                <asp:RadioButtonList RepeatDirection="Horizontal" CssClass="rbl" runat="server" ID="scoreTxtBx1"/>
            </div>
            
    <br />
    <asp:DataList ID="DataList12" Width="100%" runat="server" EnableViewState="False" onitemdatabound="DataList1_ItemDataBound">
        <ItemTemplate>
            <hr />
            <asp:Label Font-Bold="true" ID="ProductNameLabel2" runat="server"
                Text='<%# Eval("attributeStatement") %>' />
            <asp:radiobuttonlist id="radlstPubs" runat="server" />
            <asp:CheckBoxList id="chkBxList1" runat="server" />
            <asp:HiddenField ID="attributeID" runat="server"/>
            <div style="float:left">
                <asp:Label Font-Bold="true" ID="Label1" runat="server"
                    Text='Relevance Score : ' />
            </div>
            <div style="float:left">
                <asp:radiobuttonlist  RepeatDirection="Horizontal" CssClass="rbl" runat="server" ID="scoreTxtBx"/>
            </div>
            <br />
        </ItemTemplate>
    </asp:DataList>
    <br/>
    <asp:Button id="Button1" CssClass="btn btn-default" Text="Submit" onclick="Submit_Button_Click1" runat="server"/>
    <asp:Button runat="server" CssClass="btn btn-default" Text="Cancel" ID="cancelBtn" CausesValidation="false" PostBackUrl="~/KMSPages/PageRedirection"/>
</asp:Content>
