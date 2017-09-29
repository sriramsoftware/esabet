<%@ Page Title="Course Enrollment Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/AppPages/CourseViewData.aspx.cs" Inherits="KMSABET.AppPages.CourseViewData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Course Enrollment Information</h2>

                <h4 style="width: 429px">Instructor's Name</h4>
                <asp:TextBox ID="TextBox1" runat="server" Width="423px" Enabled="false"></asp:TextBox>
                
                
                <h4>Program</h4>
                <asp:DropDownList ID="program" AutoPostBack="true" runat="server" Width="201px" OnSelectedIndexChanged="program_SelectedIndexChanged">
                    
                </asp:DropDownList>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KMSABETDBConnectionString %>" SelectCommand="SELECT [program_name] FROM [App_Program]"></asp:SqlDataSource>

                <h4 style="width: 429px">Course Number and Name</h4>

                <asp:DropDownList ID="DropDownList1" runat="server" style="margin-top: 0px" Width="431px">
                    <asp:ListItem>Select Course Name</asp:ListItem>
                    </asp:DropDownList>
                
                <h4 style="width: 139px">Acadmic Year</h4>

                <asp:DropDownList ID="acadmicyear" runat="server" OnLoad="acadmicyear_Load" style="margin-top: 0px" Width="141px">
                    
                    </asp:DropDownList>
                <br />

                <h4>Semester</h4>
                <asp:DropDownList ID="Semester" runat="server" Width="141px" />

                <br />

    
            <h4>University</h4>
            
                <asp:DropDownList ID="unis" runat="server" DataTextField="UNI_NAME"></asp:DropDownList>
            <br /><br />
                <br />
                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-default" OnClick="Button1_Click" Width="130px" />

                
    <asp:Button ID="Cancel" runat="server" CssClass="btn btn-default" Text="Cancel" PostBackUrl="~/AppPages/Course.aspx"/>

                <br />
                <br />
                    

    <br />
    <br />      

</asp:Content>
