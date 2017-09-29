<%@ Page Title="Course Topic Enrollment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseTopicEnrollAdd.aspx.cs" Inherits="KMSABET.AppPages.CourseTopicEnrollAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Course Topic Enrollment</h2>

    <br />


    <table>

        <tr>
            <th><label>Program : </label></th>
            <td><asp:DropDownList ID="program" runat="server" AutoPostBack="true" OnSelectedIndexChanged="program_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>

        <tr>
            <th><label>Course : </label></th>
            <td><asp:DropDownList ID="course" runat="server" AutoPostBack="true" OnSelectedIndexChanged="course_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>

         <tr>
            <th><label>Acadmic Year : </label></th>
            <td><asp:DropDownList ID="Acadmicyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Acadmicyear_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>

        <tr>
            <th><label>Semester : </label></th>
            <td><asp:DropDownList ID="semster" runat="server" AutoPostBack="true" OnSelectedIndexChanged="semster_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>

    </table>


    <hr />


    <asp:GridView ID="MainGrid" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="MainGrid_PageIndexChanging" ItemType="KMSABET.AppPages.APP_CourseTopic" Width="100%" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
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
        <Columns>
            
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:CheckBox id="ch" runat="server"/>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="TOPIC_STATEMENT" HeaderText="Course Topic Statement" ItemStyle-Width="55%"  SortExpression="TOPIC_STATEMENT" />
            
            <asp:BoundField DataField="CLO" HeaderText="CLO Name" SortExpression="CLO" />

        </Columns>
        </asp:GridView>

    <br />

    <asp:Button CssClass="btn btn-default" runat="server" ID="Submit" Text="Submit" OnClick="Submit_Click" />

    <asp:Button CssClass="btn btn-default" runat="server" ID="Button1" Text="Close" PostBackUrl="~/AppPages/UniversityPortalMenu.aspx" />

</asp:Content>
