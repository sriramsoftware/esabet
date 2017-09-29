<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KMSABET._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Expert System for ABET</h1>
        <p class="lead">Expert System - ES for ABET helps the teacher to attain a certain level of satisfaction for the course. 
            Moreover this project comprises of expert system which will help the faculty members in designing the assessment.
        </p>
        <p><a href="KMSPages/AboutProject.aspx" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Improvement Plan</h2>
            <p>
                Processes to ensure continuous improvement of the quality of a system are called Continuous Quality Improvement (CQI) processes. 
                Academic programs require CQI to attain a certain level of satisfaction of the learning outcomes.
            </p>
            <p>
                <a class="btn btn-default" href="KMSPages/AboutImprovementPlan.aspx">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Question Bank</h2>
            <p>
                The idea of integrating expert system with the question bank is new. 
                The expert system developed in this project will help the faculty members in designing the assessments.
            </p>
            <p>
                <a class="btn btn-default" href="KMSPages/AboutQuestionBank.aspx">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Project Highlights</h2>
            <p>
                Experts train our system. Any University in the world can register here and then utilize our services for any of the courses which they are teaching in their university.
            </p>
            <p>
                <a class="btn btn-default" href="KMSPages/AboutProject.aspx">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
