<%@ Page Title="Course Topic Enrollment View" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseTopicEnrollView.aspx.cs" Inherits="KMSABET.AppPages.CourseTopicEnrollView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Course Topic Enrollment View</h2>

    <br />


    <table>

        <tr>
            <th><label>Program : </label></th>
            <td><asp:DropDownList ID="program" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="program_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>

        <tr>
            <th><label>Course : </label></th>
            <td><asp:DropDownList ID="course" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="course_SelectedIndexChanged"></asp:DropDownList></td>
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
    
    <asp:Label ID="Error" runat="server" ForeColor="Red" Width="100%" Text="*"/>
    <br />
      <asp:GridView ID="MainGrid" runat="server" AutoGenerateColumns="False" ItemType="KMSABET.AppPages.APP_CourseTopic" Width="100%" ForeColor="#333333" GridLines="None">
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
            
            
            <asp:BoundField DataField="TOPIC_STATEMENT" HeaderText="Course Topic Statement" ItemStyle-Width="55%"  SortExpression="TOPIC_STATEMENT" />
            
            <asp:BoundField DataField="Course_ID" HeaderText="Course Name" SortExpression="Course_ID" />

            

             <asp:TemplateField HeaderText="Sequence">
                <ItemTemplate>
                    <asp:DropDownList ID="drop" runat="server"></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        </asp:GridView>
    

    <br />

    <asp:Button iD="Submit" runat="server" Text="Submit" CssClass="btn btn-default" OnClick="Submit_Click"/>

</asp:Content>
