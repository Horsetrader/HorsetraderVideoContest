<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="judge-login.aspx.cs" Inherits="HorsetraderVideoContest.judge_login" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/user_controls/Header.ascx" %>
<%@ Register TagPrefix="uc" TagName="BannerAds" Src="~/user_controls/BannerAds.ascx" %>
<%@ Register TagPrefix="uc" TagName="Analytics" Src="~/user_controls/Analytics.ascx" %>
<%@ Register TagPrefix="uc" TagName="Footer" Src="~/user_controls/Footer.ascx" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Horsetrader.com Video Contest</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="Submit your entry for the Horsetrader video contest!">
    <meta name="keywords" content="california horse trader, horsetrader video contest">
    <!--Page stylesheets, etc-->
    <link rel="stylesheet" type="text/css" href="assets/css/bootstrap.min.css" media="screen">
    <link rel="stylesheet" type="text/css" href="assets/css/bootstrap-responsive.min.css" media="screen">
    <link rel="stylesheet" type="text/css" href="assets/css/page.css" media="screen">
    <link rel="icon" href="http://www.horsetrader.com/favicon.ico" type="image/x-icon">
    <!--Scripts-->
    <script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/jqBootstrapValidation.js" type="text/javascript"></script>
    <script>
        $(function () {
            $("input").jqBootstrapValidation();

            //If user presses 'Enter' while on Login
            //inputs, then fire btnLogin's click event
            $('.login-input').keydown(function (e) {
                if (e.keyCode == 13) {
                    $('input[id$=btnLogin]').click();
                    return false;
                }
            });
        });
    </script>
    <!--Analytics-->
    <uc:Analytics ID="ucAnalytics" runat="server" />
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
        <uc:Header ID="ucHeader" runat="server" />
        <div class="container">
            <uc:BannerAds ID="ucBannerAds" runat="server" />
            <div class="row">
                <div class="span12">
                    <div class="form-text">
                        <h2>Help decide the winner...</h2>
                        <h4>Sign up, choose the $500 People's Choice winner, and get a reward yourself!</h4>
                        <p>
                            When you sign up to become a horsetrader.com People's Choice Judge and choose your favorite,<br>
                            your vote will help determine the winner of the $500 People's Choice Award, to be given at the<br>
                            <span>Villas at Rancho Valencia World Cup Grand Prix of Del Mar on Oct. 25, 2014!</span>
                        </p>
                    </div>
                    <div id="errorMessage" runat="server" class="alert alert-error text-center"></div>
                    <div id="infoMessage" runat="server" class="alert alert-info"></div>
                    <div id="login" class="input-form">
                        <fieldset>
                            <!-- email input-->
                            <div class="control-group">
                                <label class="control-label">Email</label>
                                <div class="controls">
                                    <asp:TextBox ID="tbxEmail" runat="server" name="email" placeholder="your@email.com"
                                        class="input-xlarge login-input" MaxLength="100" type="email" required
                                        data-validation-email-message="Please enter a valid email address"></asp:TextBox>
                                    <p class="help-block"></p>
                                </div>
                            </div>
                            <!-- password input-->
                            <div class="control-group">
                                <label class="control-label">Password</label>
                                <div class="controls">
                                    <asp:TextBox ID="tbxPassword" runat="server" name="password" TextMode="Password"
                                        class="input-xlarge login-input" MaxLength="25" type="password" required></asp:TextBox>
                                </div>
                            </div>
                            <!-- remember me input-->
                            <%--<div class="control-group">
                                <label class="control-label"></label>
                                <div class="controls">
                                    <asp:CheckBox ID="cbxRememberMe" runat="server" />
                                    Remember me
                                </div>
                            </div>--%>
                        </fieldset>
                        <div id="disclaimer">
                            <asp:Button ID="btnLogin" runat="server" type="button" 
                                class="btn btn-large btn-success" Text="Update Vote!" 
                                onclick="btnLogin_Click">
                            </asp:Button>
                        </div>
                        <div class="text-center bottom-text">
                            <span>Don't have an account yet?</span>
                            <a href="judge-sign-up.aspx">Create one here</a>
                        </div>
                    </div>
                    
                </div>
            </div>
            <uc:Footer ID="ucFooter" runat="server" />
        </div>
    </form>
</body>
</html>