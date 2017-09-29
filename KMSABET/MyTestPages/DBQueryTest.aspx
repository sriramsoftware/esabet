<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DBQueryTest.aspx.cs" Inherits="KMSABET.MyTestPages.DBQueryTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>DB Query Generator</h3>
    
    <h4>Select :</h4>
    <asp:Label runat="server" Font-Bold="true" Text="Alias : B1" />
    <asp:DropDownList ID="DropDownPKTb2" AutoPostBack="True"
          OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" runat="server" />
    <asp:DropDownList ID="DropDownSelectCol1" runat="server" />
    <asp:DropDownList ID="DropDownList4" runat="server" />
    <br /><br />
    <h4>Join :</h4> 
    <table border="1" style="width:100%">
        <tr>
            <th></th>
            <th>Primary Key Table Name</th>
            <th>Primary Key Column Name</th>
            <th>Foreign Key Table Alias</th>
            <th>Foreign Key Table Name</th>
            <th>Foreign Key Column Name</th>
        </tr>
        <tr>
            <th>Join One </th>
            <td><asp:Label runat="server" ID="selectTablePKName2" /></td>
            <td><asp:Label runat="server" ID="PKCol2" /></td>
            <td><asp:Label runat="server" ID="Label2" Font-Bold="true" Text="A1" /></td>
            <td><asp:DropDownList ID="DropDownFKTb2" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedFKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="FKCol2" /></td>
        </tr>
        <tr>
            <th>Join Two </th>
            <td><asp:DropDownList ID="DropDownPKTb3" AutoPostBack="True"
                    AppendDataBoundItems="true"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedPKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            <td><asp:Label runat="server" ID="PKCol3" /></td>
            <td><asp:Label runat="server" ID="Label3" Font-Bold="true" Text="A2" /></td>
            <td><asp:DropDownList ID="DropDownFKTb3" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedFKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="FKCol3" /></td>
        </tr>
        <tr>
            <th>Join Three </th>
            <td><asp:DropDownList ID="DropDownPKTb4" AutoPostBack="True"
                    AppendDataBoundItems="true"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedPKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            <td><asp:Label runat="server" ID="PKCol4" /></td>
            <td><asp:Label runat="server" ID="Label4" Font-Bold="true" Text="A3" /></td>
            <td><asp:DropDownList ID="DropDownFKTb4" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedFKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="FKCol4" /></td>
        </tr>
        <tr>
            <th>Join Four </th>
            <td><asp:DropDownList ID="DropDownPKTb5" AutoPostBack="True"
                    AppendDataBoundItems="true"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedPKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            <td><asp:Label runat="server" ID="PKCol5" /></td>
            <td><asp:Label runat="server" ID="Label5" Font-Bold="true" Text="A4" /></td>
            <td><asp:DropDownList ID="DropDownFKTb5" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedFKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="FKCol5" /></td>
        </tr>
        <tr>
            <th>Join Five </th>
            <td><asp:DropDownList ID="DropDownPKTb6" AutoPostBack="True"
                    AppendDataBoundItems="true"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedPKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            <td><asp:Label runat="server" ID="PKCol6" /></td>
            <td><asp:Label runat="server" ID="Label6" Font-Bold="true" Text="A5" /></td>
            <td><asp:DropDownList ID="DropDownFKTb6" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedFKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="FKCol6" /></td>
        </tr>
        <tr>
            <th>Join Six </th>
            <td>
                <asp:DropDownList ID="DropDownPKTb7" AutoPostBack="True"
                    AppendDataBoundItems="true"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedPKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="PKCol7" /></td>
            <td><asp:Label runat="server" ID="Label7" Font-Bold="true" Text="A5" /></td>
            <td><asp:DropDownList ID="DropDownFKTb7" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedFKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="FKCol7" /></td>
        </tr>
        <tr>
            <th>Join Seven </th>
            <td>
                <asp:DropDownList ID="DropDownPKTb8" AutoPostBack="True"
                    AppendDataBoundItems="true"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedPKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="PKCol8" /></td>
            <td><asp:Label runat="server" ID="Label8" Font-Bold="true" Text="A5" /></td>
            <td><asp:DropDownList ID="DropDownFKTb8" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedFKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="FKCol8" /></td>
        </tr>
        <tr>
            <th>Join Eight </th>
            <td>
                <asp:DropDownList ID="DropDownPKTb9" AutoPostBack="True"
                    AppendDataBoundItems="true"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedPKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="PKCol9" /></td>
            <td><asp:Label runat="server" ID="Label9" Font-Bold="true" Text="A5" /></td>
            <td><asp:DropDownList ID="DropDownFKTb9" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedFKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="FKCol9" /></td>
        </tr>
        <tr>
            <th>Join Nine </th>
            <td>
                <asp:DropDownList ID="DropDownPKTb10" AutoPostBack="True"
                    AppendDataBoundItems="true"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedPKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="PKCol10" /></td>
            <td><asp:Label runat="server" ID="Label10" Font-Bold="true" Text="A5" /></td>
            <td><asp:DropDownList ID="DropDownFKTb10" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedFKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="FKCol10" /></td>
        </tr>
        <tr>
            <th>Join Ten </th>
            <td>
                <asp:DropDownList ID="DropDownPKTb11" AutoPostBack="True"
                    AppendDataBoundItems="true"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedPKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="PKCol11" /></td>
            <td><asp:Label runat="server" ID="Label11" Font-Bold="true" Text="A5" /></td>
            <td><asp:DropDownList ID="DropDownFKTb11" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChangedFKTable" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:Label runat="server" ID="FKCol11" /></td>
        </tr>
    </table>

    <br /><br />
    <h4>Where :</h4> 
    <table border="1" style="width:100%">
        <tr>
            <th></th>
            <th>Table Name</th>
            <th>Column Name</th>
            <th>Ask User ?</th>
            <th>Column Values</th>
            <th>Column Row ID</th>
            <th>PK Column Name</th>
            <th>Heading When Askign User</th>
        </tr>
        <tr>
            <th>Where One</th>
            <td><asp:DropDownList ID="DropDownWhereTable1" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedIndexChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="DropDownWhereColumnList1" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="askUser1" AutoPostBack="true" runat="server" OnCheckedChanged="changeAskUserWhereClause"/>
            </td>
            <td><asp:DropDownList ID="DropDownColumnValues1" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnValueChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td><asp:Label runat="server" ID="SelectedColumnID1" /></td>
            <td><asp:Label runat="server" ID="pkColumnName1" /></td>
            <td><asp:TextBox runat="server" Enabled="false" ID="headingWhereTable1" /></td>
        </tr>
        <tr>
            <th>Where Two</th>
            <td><asp:DropDownList ID="DropDownWhereTable2" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedIndexChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="DropDownWhereColumnList2" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="askUser2" AutoPostBack="true" runat="server" OnCheckedChanged="changeAskUserWhereClause"/>
            </td>
            <td><asp:DropDownList ID="DropDownColumnValues2" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnValueChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td><asp:Label runat="server" ID="SelectedColumnID2" /></td>
            <td><asp:Label runat="server" ID="pkColumnName2" /></td>
            <td><asp:TextBox runat="server" Enabled="false" ID="headingWhereTable2" /></td>
        </tr>
        <tr>
            <th>Where Three</th>
            <td><asp:DropDownList ID="DropDownWhereTable3" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedIndexChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="DropDownWhereColumnList3" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="askUser3" AutoPostBack="true" runat="server" OnCheckedChanged="changeAskUserWhereClause"/>
            </td>
            <td><asp:DropDownList ID="DropDownColumnValues3" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnValueChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td><asp:Label runat="server" ID="SelectedColumnID3" /></td>
            <td><asp:Label runat="server" ID="pkColumnName3" /></td>
            <td><asp:TextBox runat="server" Enabled="false" ID="headingWhereTable3" /></td>
        </tr>
        <tr>
            <th>Where Four</th>
            <td><asp:DropDownList ID="DropDownWhereTable4" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedIndexChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="DropDownWhereColumnList4" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="askUser4" AutoPostBack="true" runat="server" OnCheckedChanged="changeAskUserWhereClause"/>
            </td>
            <td><asp:DropDownList ID="DropDownColumnValues4" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnValueChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td><asp:Label runat="server" ID="SelectedColumnID4" /></td>
            <td><asp:Label runat="server" ID="pkColumnName4" /></td>
            <td><asp:TextBox runat="server" Enabled="false" ID="headingWhereTable4" /></td>
        </tr>
        <tr>
            <th>Where Five</th>
            <td><asp:DropDownList ID="DropDownWhereTable5" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedIndexChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="DropDownWhereColumnList5" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="askUser5" AutoPostBack="true" runat="server" OnCheckedChanged="changeAskUserWhereClause"/>
            </td>
            <td><asp:DropDownList ID="DropDownColumnValues5" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnValueChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td><asp:Label runat="server" ID="SelectedColumnID5" /></td>
            <td><asp:Label runat="server" ID="pkColumnName5" /></td>
            <td><asp:TextBox runat="server" Enabled="false" ID="headingWhereTable5" /></td>
        </tr>
        <tr>
            <th>Where Six</th>
            <td><asp:DropDownList ID="DropDownWhereTable6" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedIndexChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="DropDownWhereColumnList6" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="askUser6" AutoPostBack="true" runat="server" OnCheckedChanged="changeAskUserWhereClause"/>
            </td>
            <td><asp:DropDownList ID="DropDownColumnValues6" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnValueChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td><asp:Label runat="server" ID="SelectedColumnID6" /></td>
            <td><asp:Label runat="server" ID="pkColumnName6" /></td>
            <td><asp:TextBox runat="server" Enabled="false" ID="headingWhereTable6" /></td>
        </tr>
        <tr>
            <th>Where Seven</th>
            <td><asp:DropDownList ID="DropDownWhereTable7" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedIndexChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="DropDownWhereColumnList7" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="askUser7" AutoPostBack="true" runat="server" OnCheckedChanged="changeAskUserWhereClause"/>
            </td>
            <td><asp:DropDownList ID="DropDownColumnValues7" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnValueChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td><asp:Label runat="server" ID="SelectedColumnID7" /></td>
            <td><asp:Label runat="server" ID="pkColumnName7" /></td>
            <td><asp:TextBox runat="server" Enabled="false" ID="headingWhereTable7" /></td>
        </tr>
        <tr>
            <th>Where Eight</th>
            <td><asp:DropDownList ID="DropDownWhereTable8" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedIndexChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="DropDownWhereColumnList8" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="askUser8" AutoPostBack="true" runat="server" OnCheckedChanged="changeAskUserWhereClause"/>
            </td>
            <td><asp:DropDownList ID="DropDownColumnValues8" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnValueChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td><asp:Label runat="server" ID="SelectedColumnID8" /></td>
            <td><asp:Label runat="server" ID="pkColumnName8" /></td>
            <td><asp:TextBox runat="server" Enabled="false" ID="headingWhereTable8" /></td>
        </tr>
        <tr>
            <th>Where Nine</th>
            <td><asp:DropDownList ID="DropDownWhereTable9" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedIndexChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="DropDownWhereColumnList9" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="askUser9" AutoPostBack="true" runat="server" OnCheckedChanged="changeAskUserWhereClause"/>
            </td>
            <td><asp:DropDownList ID="DropDownColumnValues9" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnValueChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td><asp:Label runat="server" ID="SelectedColumnID9" /></td>
            <td><asp:Label runat="server" ID="pkColumnName9" /></td>
            <td><asp:TextBox runat="server" Enabled="false" ID="headingWhereTable9" /></td>
        </tr>
        <tr>
            <th>Where Ten</th>
            <td><asp:DropDownList ID="DropDownWhereTable10" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedIndexChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList>
            </td>
            <td><asp:DropDownList ID="DropDownWhereColumnList10" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td>
                <asp:CheckBox ID="askUser10" AutoPostBack="true" runat="server" OnCheckedChanged="changeAskUserWhereClause"/>
            </td>
            <td><asp:DropDownList ID="DropDownColumnValues10" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlWhere_SelectedColumnValueChanged" runat="server" >
                    <asp:ListItem Value="" />
                </asp:DropDownList></td>
            <td><asp:Label runat="server" ID="SelectedColumnID10" /></td>
            <td><asp:Label runat="server" ID="pkColumnName10" /></td>
            <td><asp:TextBox runat="server" Enabled="false" ID="headingWhereTable10" /></td>
        </tr>
    </table>
    
    <br /><br />
    <asp:Button runat="server" Text="Generate Query" onclick="Button1_Click"/>
    <br />
    <asp:TextBox runat="server" ID="queryOutput"></asp:TextBox>
</asp:Content>
