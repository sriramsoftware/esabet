<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PopupModal.aspx.cs" Inherits="KMSABET.MyTestPages.PopupModal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
     <asp:Button ID="ClientButton" runat="server" Text="Launch Modal Popup (Client)" />
     <asp:Panel ID="ModalPanel" runat="server" Width="500px">
         ASP.NET AJAX is a free framework for quickly creating a new generation of more 
         efficient, more interactive and highly-personalized Web experiences that work 
         across all the most popular browsers.<br />
         <asp:Button ID="OKButton" runat="server" Text="Close" />
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" TargetControlId="ClientButton" 
        PopupControlID="ModalPanel" OkControlID="OKButton" />
    
    <asp:Button ID="ServerButton" runat="server" Text="Launch Modal 
Popup (Server)" OnClick="ServerButton_Click" /> 
    
    <script type="text/javascript">
        var launch = false;
        function launchModal() {
            launch = true;
        }
        function pageLoad() {
            if (launch) {
                $find("<%=mpe.ClientID%>").show();
                //$find("mpe").show();
            }
        }
    </script> 





    <asp:Button ID="btnNewWindow" Text="Open new Window" runat="server" OnClick="OpenWindow" />






    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "jQuery Dialog Popup",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };
    </script>
    <div id="dialog" style="display: none">
        <asp:Label runat="server" id="hello" Text="Hi I am Haris" />
    </div>
    <asp:Button ID="btnShowPopup" runat="server" Text="Show Popup" OnClick="btnShowPopup_Click" />





    
    <asp:Button ID="Button1" runat="server" Text="Show Popup" CommandArgument="MyVal1"
                    CommandName="ThisBtnClick"  OnClick="btnShowPopup_Click1" />
    <asp:Button ID="HiddenButtonPopUpMsgBox" runat="server" Text="Show Popup" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="MsgBoxModalPopupExtender" runat="server" DropShadow="True"
        PopupDragHandleControlID="DragPanelMsgBox" BackgroundCssClass="popUpbackground"
        CancelControlID="ButtonMsgBoxClose" PopupControlID="PanelMsgBox" TargetControlID="HiddenButtonPopUpMsgBox">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelMsgBox" runat="server" CssClass="popUpPanel" Style="display: none"
        DefaultButton="ButtonMsgBoxClose">
        
        <fieldset>
            <asp:Panel ID="DragPanelMsgBox" runat="server">
                <legend class="legendlist">Confirmation</legend>
            </asp:Panel>
            <asp:UpdatePanel ID="UpdatePanelMsgBox" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PanelMsgScroll" runat="server" CssClass="PopUpPanelMsgPanel">
                        <asp:Label ID="LabelMsgBox" runat="server" Text="-" CssClass="MsgBoxLabel"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div align="center">
                <asp:Button ID="Button2" runat="server" Text="OK" OnClick="Button2_Click"
                    CssClass="button" />
                <asp:Button ID="ButtonMsgBoxClose" runat="server" Text="Cancel"
                    CssClass="button" />
            </div>
        </fieldset>
    </asp:Panel>

</asp:Content>


