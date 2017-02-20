<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="email-unsubscribe.aspx.cs" Inherits="HorsetraderVideoContest.email_unsubscribe" %>
<%@ Register TagPrefix="uc" TagName="Analytics" Src="~/user_controls/Analytics.ascx" %>

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
    <link rel="stylesheet" type="text/css" href="assets/css/page.css" media="screen">
    <link rel="icon" href="http://www.horsetrader.com/favicon.ico" type="image/x-icon">
    <!--Analytics-->
    <uc:Analytics ID="ucAnalytics" runat="server" />
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
        <div class="container">
            <div class="row">
                <div class="span12">
                    <div class="row">
                        <div class="span6 offset3">
                            <div id="unsubscribeBox" runat="server" class="generic-container" style="text-align:center">
                                <div id="errorMessage" runat="server" class="alert alert-error"></div>
                                <h2>Update your subscription</h2>
                                <div style="text-align: left; padding: 10px 0 20px 20px;">
                                    <asp:RadioButton ID="rbReceiveAlerts" runat="server" GroupName="subscription"
                                        Checked="true" />
                                    <span>Continue receiving alerts with new entries</span>
                                    <br><br>
                                    <asp:RadioButton ID="rbWeeklyAlerts" runat="server" GroupName="subscription" />
                                    <span>Receive a weekly summary with links to new entries</span>
                                    <br><br>
                                    <asp:RadioButton ID="rbUnsubscribe" runat="server" GroupName="subscription" />
                                    <span>Unsubscribe from the contest (NOTE: We’ll miss your vote!)</span>
                                    <br><br>
                                </div>
                                <p>Please confirm your email to update your settings:</p>
                                <asp:TextBox ID="tbxEmail" runat="server" name="email"
                                    class="input-xlarge" MaxLength="100" type="email" required
                                    data-validation-email-message="Please enter a valid email address">
                                </asp:TextBox>
                                <br><br>
                                <asp:Button ID="btnUnsubscribe" runat="server" type="button" 
                                    class="btn btn-success" Text="Confirm" onclick="btnUnsubscribe_Click">
                                </asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>