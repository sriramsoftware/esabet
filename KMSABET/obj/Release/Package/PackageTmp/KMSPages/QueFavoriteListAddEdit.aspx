<%@ Page Title="Add Assessment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QueFavoriteListAddEdit.aspx.cs" Inherits="KMSABET.KMSPages.QueFavoriteListAddEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3><asp:Label runat="server" ID="label1"></asp:Label></h3>
    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" Height="186px" Width="100%"
        DisplaySideBar="false"
        OnNextButtonClick="Wizard1_NextButtonClick" OnPreviousButtonClick="Wizard1_PreviousButtonClick"
        OnFinishButtonClick="Wizard1_FinishButtonClick" CancelDestinationPageUrl="QueFavoriteListAddEdit.aspx">
        <StartNavigationTemplate>
            <asp:Button ID="StartNextButton" CssClass="btn btn-default" runat="server" CommandName="MoveNext" 
                Text="Add/Delete Questions" />
            <asp:Button ID="CancelButton" CssClass="btn btn-default" runat="server" CausesValidation="False" CommandName="Cancel" 
                Text="Cancel" />
        </StartNavigationTemplate>
        <StepNavigationTemplate>
            <asp:Button ID="StepPreviousButton" CssClass="btn btn-default" runat="server" CausesValidation="False" CommandName="MovePrevious"
                Text="Previous" />
            <asp:Button ID="StepNextButton" CssClass="btn btn-default" runat="server" CommandName="MoveNext" 
                Text="Search Questions" />
            <asp:Button ID="CancelButton" CssClass="btn btn-default" runat="server" CausesValidation="False" CommandName="Cancel" 
                Text="Cancel" />
        </StepNavigationTemplate>
        <FinishNavigationTemplate>
            <asp:Button ID="StepPreviousButton" CssClass="btn btn-default" runat="server" CausesValidation="False" CommandName="MovePrevious"
                Text="Pick Questions" />
            <asp:Button ID="Button1" CssClass="btn btn-default" runat="server" CommandName="MoveComplete" 
                Text="Add Assessment" />
            <asp:Button ID="CancelButton2" CssClass="btn btn-default" runat="server" CausesValidation="False" CommandName="Cancel" 
                Text="Cancel" />
        </FinishNavigationTemplate>
        <WizardSteps>
            <asp:WizardStep runat="server" Title="">
                <table style="padding: 4px;">
                    <tr>
                        <th>
                            <asp:Label runat="server" ID="favListNameLbl2" Text="Assessment Name: " /></th>
                        <td>
                            <asp:Label runat="server" ID="favListName2"></asp:Label>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="questionsListSelectedTag2" runat="server"
                    AutoGenerateColumns="False" Width="100%"
                    Enabled="true"
                    ItemType="KMSABET.MyPocos.QueQuestion"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>
                        No data was found
                    </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                            <ItemStyle Width="60px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="questionStatement" HeaderText="Question(s)" />
                        <asp:TemplateField HeaderText="View Details">
                            <ItemTemplate>
                                <asp:Button ID="test" runat="server" Text="View" CssClass="btn btn-default"
                                    CommandName="<%#: Item.questionStatement %>"
                                    CommandArgument="<%#: Item.questionId %>" OnClick="MyBtnHandlerEditView" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
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
                <br />
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="">
                <h4>Question Selection Criteria</h4>
                <asp:Label Text="CLO: " Font-Bold="true" runat="server"></asp:Label>
                <asp:RadioButtonList ID="DropDownList3" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlClo_SelectedIndexChanged" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DropDownList3"
                    ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <hr />
                <asp:Label Text="SO" Font-Bold="true" runat="server"></asp:Label>
                <asp:Label Text="(Only the SOs that are relevant to the selected CLO are displayed in the following)" Font-Size="Small" Font-Bold="true" runat="server"></asp:Label>
                <asp:Label Text=":" Font-Bold="true" runat="server"></asp:Label>
                <asp:RadioButtonList ID="DropDownList4" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DropDownList4"
                    ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <hr />
                <asp:Label Text="Course Topic"  Font-Bold="true" runat="server"></asp:Label>
                <asp:Label Text="(Only the Course Topics that are relevant to the selected CLO are displayed in the following)" Font-Size="Small" Font-Bold="true" runat="server"></asp:Label>
                <asp:Label Text=":" Font-Bold="true" runat="server"></asp:Label>
                <asp:RadioButtonList ID="DropDownList5" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DropDownList5"
                    ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <br />
                <asp:DataList Width="100%" ID="DataList12" runat="server" EnableViewState="False" OnItemDataBound="DataList1_ItemDataBound">
                    <ItemTemplate>
                        <hr />
                        <asp:Label Font-Bold="true" ID="ProductNameLabel2" runat="server"
                            Text='<%# Eval("attributeStatement") %>' />
                        <br />
                        <asp:RadioButtonList ID="radlstPubs" runat="server" />
                        <asp:HiddenField ID="attributeTypeHidden" runat="server" 
                            Value='<%# Eval("attributeTypeID") %>' />
                        <asp:HiddenField ID="attributeIDHidden" runat="server" 
                            Value='<%# Eval("attributeID") %>' />
                        <asp:Label Font-Bold="true" ID="fromLabel" Visible="false" runat="server" Text="From : " />
                        <asp:TextBox ID="fromText" Visible="false" runat="server" />
                        <asp:Label Font-Bold="true" ID="toLabel" Visible="false" runat="server" Text="To : " />
                        <asp:TextBox ID="toText" Visible="false" runat="server" />
                    </ItemTemplate>
                </asp:DataList>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="">
                <h4>Search Result</h4>
                <asp:GridView ID="questionListTag" runat="server"
                    AutoGenerateColumns="False" Width="100%"
                    ItemType="KMSABET.MyPocos.QueQuestion"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>
                        No data was found
                    </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="questionId" Visible="false" HeaderText="SNo" />
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="questionStatement" HeaderText="Question Statement" />
                        <asp:TemplateField HeaderText="Total Score">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="ShowTotalScoreLbl" Text="<%#: Item.sumScore %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View Details">
                            <ItemTemplate>
                                <asp:Button ID="test" runat="server" Text="View" CssClass="btn btn-default" 
                                    CommandName="<%#: Item.questionStatement %>"
                                    CommandArgument="<%#: Item.questionId %>" OnClick="MyBtnHandler" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
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

                <br />

            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="">
                <table style="padding: 4px;">
                    <tr>
                        <th>
                            <asp:Label runat="server" ID="favListNameLbl" Text="Assessment Name: " /></th>
                        <td>
                            <asp:TextBox runat="server" ID="favListName"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="favListName"
                                ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                    </tr>
                </table>
                <h4>Selected Questions List</h4>
                <asp:GridView ID="questionsListSelectedTag" runat="server"
                    AutoGenerateColumns="False" Width="100%"
                    ItemType="KMSABET.MyPocos.QueQuestion"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>
                        No data was found
                    </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                            <ItemStyle Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete Selection">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox123" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="questionStatement" HeaderText="Question Statement" />
                    </Columns>
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
                <asp:Button ID="Button2" CssClass="btn btn-default" CausesValidation="false" runat="server" Text="Delete Selected" OnClick="DeleteSelected" />
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Complete" Title="">
                <table>
                    <tr>
                        <th>
                            <asp:Label ID="Label3" runat="server" Text="Your favorite list has been submitted successfully."></asp:Label>
                        </th>
                    </tr>
                </table>
            </asp:WizardStep>
        </WizardSteps>

    </asp:Wizard>
    <br />

    <asp:Button ID="HiddenButtonPopUpMsgBox" runat="server" Text="Show Popup" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="MsgBoxModalPopupExtender" BehaviorID="MPE" runat="server" Drag="true" DropShadow="True"
        PopupDragHandleControlID="DragPanelMsgBox" BackgroundCssClass="modalBackground"
        CancelControlID="ButtonMsgBoxClose" PopupControlID="PanelMsgBox" TargetControlID="HiddenButtonPopUpMsgBox">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelMsgBox" BackColor="White" Width="800px" ForeColor="Black" BorderStyle="Double" runat="server" 
        CssClass="popUpPanel" Style="display: none; vertical-align:central;"
        DefaultButton="ButtonMsgBoxClose">
        <fieldset>
            <asp:Panel ID="DragPanelMsgBox" runat="server" BackColor="Black">
                
                <legend>
                    <span><label style="color:white" class="legendlist">Question Details</label></span>
                    <span style="float:right; padding-right:2px;">
                        <asp:Button ID="ButtonMsgBoxClose" runat="server" Font-Bold="true" Text="X" 
                            style="padding:4px 6px 4px 6px;" CssClass="btn btn-default" />
                    </span>
                </legend>
            </asp:Panel>
            <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"/>--%>
            <asp:UpdatePanel ID="UpdatePanelMsgBox" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel Width="790px" runat="server" Height="500px" ID="selectedQuestionPanel" 
                        style="overflow-x: hidden;" Visible="false" ScrollBars="Vertical">
                        <div>
                            <asp:Label Font-Bold="true" runat="server" ID="selectedQuestionLabel" Text="Selected Question:"></asp:Label>
                            <asp:Label runat="server" ID="selectedQuestionStatment"></asp:Label>
                        </div>
                        <div style="float:right">
                            <asp:Label Font-Bold="true" runat="server" Text="Sum of Total Score: " />
                            <asp:Label runat="server" ID="Label4" Text="0"></asp:Label>
                        </div>
                        <asp:GridView ID="selectedQuestionDetailsView" runat="server"
                            AutoGenerateColumns="False" Width="790px"
                            ItemType="KMSABET.MyPocos.QueQuestionScore"
                            CellPadding="4" ForeColor="#333333" GridLines="None">
                            <EditRowStyle BackColor="#2461BF" />
                            <EmptyDataTemplate>
                                No data was found
                            </EmptyDataTemplate>
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="attributeStatement" HeaderText="Attribute Statement" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="userSelectionStatement" HeaderText="Your Selection" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="questionAttributeOptStatement" HeaderText="Question Attribute" />
                                <asp:BoundField ItemStyle-Width="50px" DataField="scoreValue" HeaderText="Score" />
                                <asp:BoundField ItemStyle-Width="50px" DataField="weightValue" HeaderText="Weightage" />
                                <asp:BoundField ItemStyle-Width="50px" DataField="sumScoreValue" HeaderText="Total Score" />
                            </Columns>
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
                        <br />
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </asp:Panel>
</asp:Content>
