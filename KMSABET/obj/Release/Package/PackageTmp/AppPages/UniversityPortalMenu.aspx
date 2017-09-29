<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UniversityPortalMenu.aspx.cs" Inherits="KMSABET.KMSPages.UniversityPortalMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="Instructor" runat="server" Text="Instructor Name : "></asp:Label>

    <h3>University Portal Menu</h3>
    <ul>

        <% if (Session["userTypeId"] != null)
           {
               if (Session["userTypeId"].ToString() == "1")
               { %>

        <li><a href="/AppPages/ProgramView.aspx">Program </a></li>
        <li><a href="/AppPages/CoursesViews.aspx">Course </a></li>
        <li><a href="University.aspx">University </a></li>
        <li><a href="/AppPages/Instructor.aspx">Instructor </a></li>
        <li><a href="CourseTopic.aspx">Course Topic </a></li>


        <%     }
           } if (Session["userTypeId"] != null)
           {
               if (Session["userTypeId"].ToString() == "2")
               { %>


        <li><a href="/AppPages/Course.aspx">Course Enrollment </a></li>
        
        <li><a href="CourseTopicEnrollAdd.aspx">Course Topic Enrollment</a></li>
        <li><a href="insmedview.aspx">Instructor Method</a></li>



        <li><a href="FinalGradeView.aspx">Score Distribution </a></li>

        <li><a href="/AppPages/ScoreDesignView.aspx">Score Design </a></li>
        
        <%}
           } %>
    </ul>

</asp:Content>
