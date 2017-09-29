<%@ Page Title="View Assessment Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QueFavoriteListView.aspx.cs" Inherits="KMSABET.KMSPages.QueFavoriteListView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h3>Assessment Details</h3>
    <table>
        <tr>   
            <th><asp:Label runat="server" ID="favListNameLbl" Text="Assessment Name: " /></th>
            <td><asp:Label Enabled="false" runat="server" ID="favListName1"></asp:Label>
        </tr>
    </table>
    <br />
    
    <asp:GridView ID="questionsListSelectedTag" runat="server"
        AutoGenerateColumns="False" Enabled="true"
        ItemType="KMSABET.MyPocos.QueQuestion" Width="100%"
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
            <asp:BoundField DataField="questionStatement" HeaderText="Selected Question(s)"/>
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
                            ItemType="KMSABET.MyPocos.FavListQueScore"
                            CellPadding="4" ForeColor="#333333" GridLines="None">
                            <EditRowStyle BackColor="#2461BF" />
                            <EmptyDataTemplate>
                                No data was found
                            </EmptyDataTemplate>
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="attributeStatement" HeaderText="Attribute Statement" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="userSelectionStatement" HeaderText="User Selection" />
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
