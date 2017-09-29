<%@ Page Title="Setup Data for Questions File Upload" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QueQuestionUploadSetupValues.aspx.cs" Inherits="KMSABET.KMSPages.QueQuestionUploadSetupValues" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .Initial
        {
          display: block;
          padding: 4px 18px 4px 14px;
          float: left;
          background: url("../Images/InitialImage.png") no-repeat right top;
          color: Black;
          font-weight: bold;
          font-size: medium
        }
        .Initial:hover
        {
          color: White;
          background: url("../Images/SelectedButton.png") no-repeat right top;
        }
        .Clicked
        {
          float: left;
          display: block;
          background: url("../Images/SelectedButton.png") no-repeat right top;
          padding: 4px 18px 4px 14px;
          color: Black;
          font-weight: bold;
          color: White;
          font-size: medium
        }
    </style>
    <h3>Setup Data for Questions File Upload</h3>
    <a href="QueQuestionAddUpload.aspx" class="btn btn-default">Upload Questions in Bulk</a>
    <a href="QueQuestionList.aspx" class="btn btn-default">Return to Main Menu</a>
    <br/><br/>
    <asp:Button Text="Course Topic" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
        OnClick="Tab1_Click" />
    <asp:Button Text="CLO" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
        OnClick="Tab2_Click" />
    <asp:Button Text="SO" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
        OnClick="Tab3_Click" />
    <asp:Button Text="Question Attribute" BorderStyle="None" ID="Tab4" CssClass="Initial" runat="server"
        OnClick="Tab4_Click" />
          
    <asp:MultiView ID="MainView" runat="server">
        <asp:View ID="View1" runat="server">
            <asp:GridView ID="courseTopicListTag" runat="server"
            AutoGenerateColumns="False" Width="100%"
            AllowPaging="true" OnPageIndexChanging="grdData_PageIndexChanging"
            ItemType="KMSABET.MyPocos.AppCourseTopic"
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="id" ItemStyle-Width="100px" HeaderText="Topic ID"/>
                <asp:BoundField DataField="topic" HeaderText="Topic Statement"/>
                <asp:BoundField DataField="course.courseId" Visible="false" HeaderText="Course ID"/>
                <asp:BoundField DataField="course.courseName" HeaderText="Course Name"/>
                <asp:BoundField DataField="course.program.programId" Visible="false" HeaderText="Program ID"/>
                <asp:BoundField DataField="course.program.programName" HeaderText="Program Name"/>
                </Columns>
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
            </asp:GridView>
        </asp:View>

        <asp:View ID="View2" runat="server">
            <asp:GridView ID="CLOListGridView" runat="server"
            AutoGenerateColumns="False" Width="100%"
            AllowPaging="true" OnPageIndexChanging="CLOListGridView_PageIndexChanging"
            ItemType="KMSABET.MyPocos.AppCLO"
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="cloId" ItemStyle-Width="100px" HeaderText="CLO ID"/>
                <asp:BoundField DataField="cloStatement" HeaderText="CLO Statement"/>
                <asp:BoundField DataField="courseId" Visible="false" HeaderText="Course ID"/>
                <asp:BoundField DataField="courseName" HeaderText="Course Name"/>
                <asp:BoundField DataField="programId" Visible="false" HeaderText="Program ID"/>
                <asp:BoundField DataField="programName" HeaderText="Program Name"/>
                </Columns>
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
            </asp:GridView>
        </asp:View>

        <asp:View ID="View3" runat="server">
            <asp:GridView ID="SO_GridView1" runat="server"
            AutoGenerateColumns="False" Width="100%"
            AllowPaging="true" OnPageIndexChanging="SO_GridView1_PageIndexChanging"
            ItemType="KMSABET.MyPocos.AppSO"
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="id" ItemStyle-Width="100px" HeaderText="SO ID"/>
                <asp:BoundField DataField="statement" HeaderText="SO Statement"/>
                <asp:BoundField DataField="program.programId" Visible="false" HeaderText="Program ID"/>
                <asp:BoundField DataField="program.programName" HeaderText="Program Name"/>
                </Columns>
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
            </asp:GridView>
        </asp:View>

        <asp:View ID="View4" runat="server">
            <asp:GridView ID="AttributeOptionsGridView1" runat="server"
                AutoGenerateColumns="False" Width="100%"
                AllowPaging="true" OnPageIndexChanging="AttributeOptionsGridView1_PageIndexChanging"
                ItemType="KMSABET.MyPocos.QueAttributeOptions"
                CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="attributeOptionId" ItemStyle-Width="150px" HeaderText="Attribute Option ID"/>
                    <asp:BoundField DataField="optionStatement" HeaderText="Attribute Option Statement"/>
                    <asp:BoundField DataField="attributeId" Visible="false" HeaderText="Attribute ID"/>
                    <asp:BoundField DataField="attributeStatement" HeaderText="Attribute Statement"/>
                    </Columns>
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
            </asp:GridView>
        </asp:View>
    </asp:MultiView>
</asp:Content>
