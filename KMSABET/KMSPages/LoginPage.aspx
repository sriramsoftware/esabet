<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="KMSABET.KMSPages.LoginPage" %>

<!DOCTYPE html>
<html >
<head>
  <meta charset="UTF-8">
  <title>Login Page - Expert System for ABET </title>
  
  <link rel="stylesheet" href='/fonts/robot.css'>
  <link rel='stylesheet' href='/Content/bootstrap.min.css'>
  <link rel='stylesheet' href='/fonts/robotoandslab.css'>
  <link rel="stylesheet" href="/Content/simple-login-widget/css/style.css">
  <script src='/Scripts/jquery-1.12.4.min.js'></script>
  <script src="/Content/simple-login-widget/js/index.js"></script>
  
</head>

<body>
<div id="dialog" class="dialog dialog-effect-in">
  <div class="dialog-front">
    <div class="dialog-content">
        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="False">
            <p class="text-danger">
                <asp:Literal runat="server" ID="FailureText" Text="Username or password is wrong."/>
            </p>
        </asp:PlaceHolder>
      <form id="login_form1" onsubmit="return validate_login_form()" class="dialog-form" action="/KMSPages/PageRedirection" method="POST">
          <input name="userTypeId" value="2" type="hidden" />
          <input name="expTypeId" value="1" type="hidden" />
          <input name="uniId" value="1" type="hidden" />
          <input name="departId" value="1" type="hidden" />
          <input name="userId" value="1" type="hidden" />
        <fieldset>
          <legend>Log in</legend>
          <div class="form-group">
            <label for="username" class="control-label">Username:</label>
            <input type="text" id="username" class="form-control" name="username" autofocus/>
          </div>
          <div class="form-group">
            <label for="password" class="control-label">Password:</label>
            <input type="password" id="password" class="form-control" name="password"/>
          </div>
          <%--<div class="text-center pad-top-20">
            <p>Have you forgotten your<br><a href="#" class="link"><strong>username</strong></a> or <a href="#" class="link"><strong>password</strong></a>?</p>
          </div>--%>
          <div class="pad-top-20 pad-btm-20">
            <input type="submit" class="btn btn-default btn-block btn-lg" value="Continue">
          </div>
          <%--<div class="text-center">
            <p>Do you wish to register<br> for <a href="#" class="link user-actions"><strong>a new account</strong></a>?</p>
          </div>--%>
        </fieldset>
      </form>
    </div>
  </div>
</div>
</body>
</html>
