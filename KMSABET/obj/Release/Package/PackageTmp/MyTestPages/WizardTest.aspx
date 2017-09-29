<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WizardTest.aspx.cs" Inherits="KMSABET.MyTestPages.WizardTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" Height="186px" Width="330px"
        OnNextButtonClick="Wizard1_NextButtonClick">
        
        <StepNavigationTemplate>
            <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                Text="Previous" />
            <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Next" />
        </StepNavigationTemplate>

        <WizardSteps>
            <asp:WizardStep runat="server" title="">
                Name:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </asp:WizardStep>
            <asp:WizardStep runat="server" title="">
                Age:<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </asp:WizardStep>
            <asp:WizardStep runat="server" title="">
                Email:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Complete" Title="">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
</asp:Content>