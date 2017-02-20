<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="judge-sign-up.aspx.cs" Inherits="HorsetraderVideoContest.judge_sign_up" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/user_controls/Header.ascx" %>
<%@ Register TagPrefix="uc" TagName="BannerAds" Src="~/user_controls/BannerAds.ascx" %>
<%@ Register TagPrefix="uc" TagName="Analytics" Src="~/user_controls/Analytics.ascx" %>
<%@ Register TagPrefix="uc" TagName="Footer" Src="~/user_controls/Footer.ascx" %>

<!DOCTYPE html>
<html lang="en"><head>
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
        $(function () { $("input").jqBootstrapValidation(); });
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
                        <h2><span>Your opinion counts!</span></h2>
                        <h2>Help decide the winner...</h2>
                        <h4>Sign up, choose the $500 People's Choice winner, and get a reward yourself!</h4>
                        <p>
                            When you sign up to become a horsetrader.com People's Choice Judge and choose your favorite,<br>
                            your vote will help determine the winner of the $500 People's Choice Award, to be given at the<br>
                            <span>Villas at Rancho Valencia World Cup Grand Prix of Del Mar on Oct. 25, 2014!</span>
                        </p>
                    </div>
                    <div id="errorMessage" runat="server" class="alert alert-error"></div>
                    <div class="input-form">
                        <fieldset>
                            <!-- email input-->
                            <div class="control-group">
                                <label class="control-label">Email</label>
                                <div class="controls">
                                    <asp:TextBox ID="tbxEmail" runat="server" name="email" placeholder="your@email.com"
                                        class="input-xlarge" MaxLength="100" type="email" required
                                        data-validation-email-message="Please enter a valid email address"></asp:TextBox>
                                    <p class="help-block"></p>
                                </div>
                            </div>
                            <!-- password input-->
                            <div class="control-group">
                                <label class="control-label">Password</label>
                                <div class="controls">
                                    <asp:TextBox ID="tbxPassword" runat="server" name="password" TextMode="Password"
                                        class="input-xlarge" MaxLength="25" type="password" required></asp:TextBox>
                                </div>
                            </div>
                            <!-- full-name input-->
                            <div class="control-group">
                                <label class="control-label">Full Name</label>
                                <div class="controls">
                                    <asp:TextBox ID="tbxFirstName" runat="server" name="first-name" placeholder="first name" 
                                        class="input-medium" MaxLength="50" required></asp:TextBox>
                                    <asp:TextBox ID="tbxLastName" runat="server" name="last-name" placeholder="last name"
                                        class="input-medium" MaxLength="50" required></asp:TextBox>
                                </div>
                            </div>
                            <!-- zipcode input-->
                            <div class="control-group">
                                <label class="control-label">Zipcode</label>
                                <div class="controls">
                                    <asp:TextBox ID="tbxZipcode" runat="server" name="zipcode" class="input-small"
                                        MaxLength="5"
                                        data-validation-regex-regex='^\d{5}(?:[-\s]\d{4})?$'
                                        data-validation-regex-message="Please enter a valid zip code"
                                        required>
                                    </asp:TextBox>
                                </div>
                            </div>
                            <!-- screen name input-->
                            <div class="control-group">
                                <label class="control-label">Judge Screenname</label>
                                <div class="controls">
                                    <asp:TextBox ID="tbxScreenName" runat="server" name="screenname" class="input-xlarge"
                                        MaxLength="25" required></asp:TextBox>
                                    <p class="help-block">Your screenname will be used so your comments can remain anonymous</p>
                                </div>
                            </div>
                        </fieldset>
                        <div id="disclaimer">
                            <p>
                                <input type="checkbox" name="terms-and-conditions" required 
                                    data-validation-required-message= "Please check the box to confirm that you agree with the contest rules" />
                                <span>I have read and I agree to the</span>
                                <a class="task-details" href="#disclaimerModal" data-toggle="modal">judge's guidelines</a>
                            </p>
                            <p class="help-block"></p>
                            <asp:Button ID="btnSignUp" runat="server" type="button" 
                                class="btn btn-large btn-success" Text="Judge Now!" 
                                onclick="btnSignUp_Click">
                            </asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <uc:Footer ID="ucFooter" runat="server" />
        </div>
        <!--Judge's guidelines modal-->
        <div id="disclaimerModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="disclaimerHeader" aria-hidden="true">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h3>Judge's Guidelines</h3>
            </div>
            <div class="modal-body form-horizontal">
                <p id="disclaimerText" runat="server"></p>
            </div>
            <div class="modal-footer">
                <button class="btn" data-dismiss="modal" aria-hidden="true">Close</button>
            </div>
        </div>
    </form>
</body>
</html>