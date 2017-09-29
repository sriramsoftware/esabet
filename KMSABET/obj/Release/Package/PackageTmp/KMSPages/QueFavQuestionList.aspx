﻿<%@ Page Title="Assessment List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QueFavQuestionList.aspx.cs" Inherits="KMSABET.KMSPages.QueFavQuestionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Assessment List</h3>
    <asp:GridView ID="attributeListTag" runat="server"
        AutoGenerateColumns="False" Width="100%"
        AllowPaging="true" OnPageIndexChanging="grdData_PageIndexChanging"
        ItemType="KMSABET.MyPocos.QueFavQuestionList"
        CellPadding="4" ForeColor="#333333" GridLines="None"
        OnRowCreated="attributeListTag_RowCreated">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Sr. No."> 
                <ItemTemplate>
                    <%#Container.DataItemIndex+1%>       
                </ItemTemplate>
                <ItemStyle Width="60px" />
            </asp:TemplateField>
            <asp:BoundField DataField="favQuestionName" HeaderText="Assessment Name"/>
            <asp:BoundField DataField="instructorName" HeaderText="Instructor Name"/>
            <asp:TemplateField HeaderText="View Details"> 
                <ItemTemplate>
                    <a href="QueFavoriteListView.aspx?id=<%#: Item.favQuestionId %>&viewMode=true&delete=false" > View </a>
                </ItemTemplate>
                <ItemStyle Width="100px" />                                                          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit"> 
                <ItemTemplate>
                    <a href="QueFavoriteListAddEdit.aspx?id=<%#: Item.favQuestionId %>&viewMode=false&editMode=true&delete=false" > Edit </a>
                </ItemTemplate>
                <ItemStyle Width="100px" />                                                          
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete"> 
                <ItemTemplate>
                    <asp:Button ID="deleteBtn" runat="server" CssClass="linktobutton" 
                        CommandArgument=<%# Item.favQuestionId %>
                        OnClick="deleteBtn_Click" Text="Delete"></asp:Button>
                </ItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
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

    <%--For confirmation popup--%>
    <asp:Button ID="HiddenButtonPopUpMsgBox" runat="server" Text="Show Popup" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="MsgBoxModalPopupExtender" BehaviorID="MPE" runat="server" Drag="true" DropShadow="True"
        PopupDragHandleControlID="DragPanelMsgBox" BackgroundCssClass="modalBackground"
        CancelControlID="ButtonMsgBoxClose" PopupControlID="PanelMsgBox" TargetControlID="HiddenButtonPopUpMsgBox">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelMsgBox" BackColor="White" ForeColor="Black" BorderStyle="Solid" runat="server" 
        CssClass="popUpPanel" Style="display: none; vertical-align:central; padding-bottom:10px;"
        DefaultButton="ButtonMsgBoxClose">
        <fieldset>
            <asp:Panel ID="DragPanelMsgBox" runat="server" BackColor="Black">
                <legend style="color:white; padding-left:10px;" class="legendlist">Confirmation</legend>
            </asp:Panel>
            <asp:UpdatePanel ID="UpdatePanelMsgBox" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PanelMsgScroll" runat="server" CssClass="PopUpPanelMsgPanel" Style="padding: 0px 10px 10px 10px;">
                        <asp:Label ID="LabelMsgBox" runat="server" Text="-" CssClass="MsgBoxLabel"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="text-align:center">
                <asp:Button ID="Button2" runat="server" Text="OK"
                    CssClass="btn btn-default" />
                <asp:Button ID="ButtonMsgBoxClose" runat="server" Text="Cancel"
                    CssClass="btn btn-default" />
            </div>
        </fieldset>
    </asp:Panel>

</asp:Content>

