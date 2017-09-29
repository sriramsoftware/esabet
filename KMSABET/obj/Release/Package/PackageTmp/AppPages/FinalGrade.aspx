<%@ Page Title="Score Distrubution" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinalGrade.aspx.cs" Inherits="KMSABET.AppPages.FinalGrade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Score Distrubution</h2>
    

    <hr style="margin-top:-7px;" />

    <table style="margin-top:30px;">
        
            <tr>
                <th><label>Program</label></th>
                <td><asp:DropDownList ID="programs" AutoPostBack="true" runat="server" OnSelectedIndexChanged="programs_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>

            <tr>
                <th><label>Course</label></th>
                <td><asp:DropDownList ID="course" runat="server" AutoPostBack="True" OnSelectedIndexChanged="course_SelectedIndexChanged"/></td>

            </tr>
            
        <tr>
            <th><label>Acadmic Year</label></th>
            <td><asp:DropDownList ID="Years" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Years_SelectedIndexChanged" /></td>
        </tr>
            
        <tr>
            <th><label>Semester</label></th>
            <td><asp:DropDownList ID="Semester" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Semester_SelectedIndexChanged"/></td>
        </tr>
        <tr>
            <th><label>University</label></th>
            <td><asp:DropDownList ID="University" runat="server" AutoPostBack="True"/></td>
        </tr>

        </table>    
        
    <br />
    <hr />


    <table>
       <tr>
            <td><label >Quizzes</label></td>
            <td style="width:100px;"></td><td>                
                <asp:TextBox ID="TextBox1" TextMode="Number"  runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>                
            </td>
       </tr>

        <tr>
            <td><label >Homework Assignments</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox ID="TextBox2" TextMode="Number" runat="server" OnTextChanged="TextBox1_TextChanged" />
                    
                
            </td>
        </tr>

        <tr>
            <td><label >Team-Project</label></td><td style="width:100px;"></td><td>
                <asp:TextBox ID="TextBox3"   TextMode="Number"  runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><label >Attendance</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox  ID="TextBox4"   TextMode="Number"  runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td><label >Presentation</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox TextMode="Number"   ID="TextBox5"   runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td><label >life-Long Learning Assignment</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox TextMode="Number" ID="TextBox6"    runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td><label >Contemporay Issues Knowlege Test</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox TextMode="Number"   ID="TextBox7" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><label >Lab. Reports</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox TextMode="Number"     ID="TextBox8"   runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><label >Lab. Examination</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox TextMode="Number"     ID="TextBox9"  runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><label >Mid-Term Examination</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox TextMode="Number"      ID="TextBox10"  runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><label >Exam 1</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox TextMode="Number"      ID="TextBox11"  runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><label >Exam 2</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox TextMode="Number"     runat="server"  ID="TextBox12" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><label >Final Examination</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox TextMode="Number"     runat="server"  ID="TextBox13"  OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><label >Other Assessments</label></td>
            <td style="width:100px;"></td><td>
                <asp:TextBox TextMode="Number"     runat="server"  ID="TextBox14"  OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><label >Total</label></td>
            <td style="width:100px;"></td><td>                <asp:Button ID="add" CssClass="btn btn-default" runat="server" Text="+" OnClick="add_Click"  />
                <asp:Label ID="Label1" runat="server" Text="0" style="text-align:right;"></asp:Label>
            </td>
        </tr>

        <tr>
            <td></td>
            <td style="width:100px;"><asp:Button CssClass="btn btn-default" ID="Clear" runat="server" Text="Cancel" OnClick="Clear_Click" /></td><td>
                &nbsp;<asp:Button ID="BtnSave" CssClass="btn btn-default" runat="server" Text="Submit" OnClick="OnSaveBtn"/>
            </td>
        </tr>

        

    </table>


    

</asp:Content>
