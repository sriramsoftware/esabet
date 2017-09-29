<%@ Page Title="Question Bank" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AboutQuestionBank.aspx.cs" Inherits="KMSABET.KMSPages.AboutQuestionBank" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Question Bank</h3>
    <p>The idea of integrating expert system with the question bank is new. The expert system developed in this project will help the faculty members in designing assessments.</p>
    <p>in this project we would develop an expert system for helping the instructors in selecting the appropriate questions for any type of assessment (in the form of /quizzes/ home works/Midterm/ Final Exams/ term projects etc) of the course. Presently there is no systematic or consistent way for manually designing the type of assessment for which the proposed expert system is required. For the proposed expert system, we are talking about the assessment of learning outcomes. Mostly instructors don&rsquo;t design assessments for learning outcomes. Usually the manual design of assessments focuses on the assessment of the topics or subtopics covered in a course. The idea of an expert system for assessment of learning outcomes of a course which is the root of the CQI and a necessary requirement of ABET Accreditation, is new.</p>
    <p>While manually designing the assessment, the instructor thinks and based on his own judgment, selects a question from the set of questions available to him for the course, or he makes a new question and adds it to the list of questions. The expert system we are proposing will do the inferencing based upon the following:</p>
    <p>A set of technical questions for each subtopic (developed initially and then improved gradually in quantity and quality based on the data gathered from the use of the expert system )</p>
    <p>Each question will have a set of attributes as follows:</p>
    <ol>
    <li>Learning outcome IDs (may be more than 1) that it can test</li>
    <li>Bloom&rsquo;s Level</li>
    <li>Time needed to solve</li>
    <li>How strong is the question in assessing the learning outcomes in (i) above e.g. 10%, 50% or 100% etc</li>
    <li>Topic/Subtopic it addresses</li>
    <li>A set of questions that the expert system interface will ask the instructor</li>
    <li>Possible answers of each question in ( c )</li>
    </ol>
    <p>A set of If-then-else rules to select the right technical question from the set of questions in (a).</p>
    <p>An expert system is a computer system that emulates the decision-making ability of a human expert. Expert systems are designed to solve complex problems by reasoning about knowledge, represented primarily as if&ndash;then rules rather than through conventional procedural code. An expert system is divided into two sub-systems: the inference engine and the knowledge base. The knowledge base represents facts and rules. The inference engine applies the rules to the known facts to deduce new facts.</p>
    <p>The CQI and accreditation processes of engineering education could have potential applications of knowledge-based expert systems. One such area is the &ldquo;assessment design&rdquo; for a specific learning outcome. In this case, the instructor, in a course he/she is teaching, can select the most suitable question(s) for assessing a global learning outcome (e.g., student outcome in ABET terminology) or one of the course learning outcomes. A knowledge base may be built having a variety of questions for each topic or subtopic of the course. Each question can have tags indicating the learning outcome it addresses, time needed to solve the question, level of difficulty, and Bloom&rsquo;s Level etc. A set of if/then/else rules may also be developed and incorporated in the knowledge base. The inference engine will interact with the knowledge base and respond to the user by suggesting appropriate questions for the assessment</p>
</asp:Content>
