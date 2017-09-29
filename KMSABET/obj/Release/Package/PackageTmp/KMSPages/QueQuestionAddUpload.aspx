<%@ Page Title="Add Questions via File Upload" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QueQuestionAddUpload.aspx.cs" Inherits="KMSABET.KMSPages.QueQuestionAddUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Add Question via Excel File Upload</h3>
    <a href="QueGenerateQuestions.aspx" class="btn btn-default">Generate Dummy Questions</a>
    <a href="QueQuestionUploadSetupValues.aspx" class="btn btn-default">View Setup Data Values for File Upload</a>
    <br/><br/>
    <table> 
        <tr>
            <th>Select Program : </th>
            <td>
                <asp:DropDownList ID="program" runat="server" AutoPostBack="True" OnSelectedIndexChanged="program_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="program"
                        ErrorMessage="* Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr> 
        <tr>
            <th>Select Course : </th>
            <td>
                <asp:DropDownList ID="course" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="course"
                        ErrorMessage="* Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                Choose Excel File :
            </th>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <th></th>
            <td align="right">
                <asp:RegularExpressionValidator ID="uplValidator" ForeColor="Red" runat="server" ControlToValidate="FileUpload1"
                    ErrorMessage="Only .xls file format is allowed" 
                    ValidationExpression="(.+\.[Xx][Ll][Ss])"></asp:RegularExpressionValidator>
            <asp:Button class="btn btn-default" ID="btnUpload" runat="server" CausesValidation="false" OnClick="btnUpload_Click" Text="Upload" /></td>
        </tr>
        <tr>
            <th>Select Sheet # : </th>
            <td><asp:DropDownList ID="Sheetss" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Sheetss_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server" ControlToValidate="Sheetss"
                ErrorMessage="* Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                Select Question's Column # : 
            </th>
            <td>
                <asp:DropDownList runat="server" ID="Qcol"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" runat="server" ControlToValidate="Qcol"
                    ErrorMessage="* Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                Select Course Topic Column # : 
            </th>
            <td>
                <asp:DropDownList ID="Coursetopic" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" runat="server" ControlToValidate="Coursetopic"
                    ErrorMessage="* Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                Select CLO Column # : 
            </th>
            <td>
                <asp:DropDownList runat="server" ID="QCLO"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" runat="server" ControlToValidate="QCLO"
                    ErrorMessage="* Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                Select SO Column # : 
            </th>
            <td>
                <asp:DropDownList runat="server" ID="QSO"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" runat="server" ControlToValidate="QSO"
                    ErrorMessage="* Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                Enter Attribute Column # Splitted By "," : 
            </th>
            <td>
                <asp:TextBox runat="server" ID="QATT"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br/>
    <asp:Button ID="b" runat="server" OnClick="b_Click" class="btn btn-default" Text="Add Questions"/>
    <asp:Button runat="server" CssClass="btn btn-default" Text="Cancel" ID="cancelBtn" CausesValidation="false" PostBackUrl="~/KMSPages/QueQuestionList.aspx"/>

    <br />  
    <asp:GridView ID="gvExcelFile" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
        Visible="false">  
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />  
        <EditRowStyle BackColor="#999999" />  
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />  
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />  
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />  
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />  
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />  
        <SortedAscendingCellStyle BackColor="#E9E7E2" />  
        <SortedAscendingHeaderStyle BackColor="#506C8C" />  
        <SortedDescendingCellStyle BackColor="#FFFDF8" />  
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />  
    </asp:GridView>
    <br />  
</asp:Content>
