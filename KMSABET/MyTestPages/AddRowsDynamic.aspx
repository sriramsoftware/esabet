﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRowsDynamic.aspx.cs" Inherits="KMSABET.AddRowsDynamic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
            <asp:GridView ID="grvStudentDetails" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting"
                Width="97%" Style="text-align: left">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="SNo" />
                    <asp:TemplateField HeaderText="Student Name">
                        <ItemTemplate>
                            <asp:TextBox ID="txtName" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Age">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAge" runat="server" MaxLength="3" Width="66px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAge"
                                ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Address">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAddress" runat="server" Height="55px" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress"
                                ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gender">
                        <ItemTemplate>
                            <asp:RadioButtonList ID="RBLGender" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="RBLGender"
                                ErrorMessage="*"></asp:RequiredFieldValidator>&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="My Decision">
                        <ItemTemplate>
                            <asp:RadioButtonList ID="RBLGender1" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="M">Approved</asp:ListItem>
                                <asp:ListItem Value="F">Reject</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="RBLGender1"
                                ErrorMessage="*"></asp:RequiredFieldValidator>&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qualification">
                        <ItemTemplate>
                            <asp:DropDownList ID="drpQualification" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem Value="G">Graduate</asp:ListItem>
                                <asp:ListItem Value="P">Post Graduate</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpQualification"
                                ErrorMessage="*" InitialValue="Select"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
                <asp:Button ID="btnSave" runat="server" Text="Save Data" OnClick="btnSave_Click" />
        </div>
</asp:Content>
