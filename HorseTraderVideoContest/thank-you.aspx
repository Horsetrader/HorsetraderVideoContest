<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="thank-you.aspx.cs" Inherits="HorsetraderVideoContest.thank_you" %>
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
    <link rel="stylesheet" type="text/css" href="assets/css/page.css" media="screen">
    <link rel="stylesheet" type="text/css" href="assets/css/print.css" media="print">
    <link rel="icon" href="http://www.horsetrader.com/favicon.ico" type="image/x-icon">
    <script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
    <script src="assets/js/jqBootstrapValidation.js" type="text/javascript"></script>
    <script src="assets/js/jquery.limit-1.2.source.js"></script>
    <script src="assets/js/page.js"></script>
    <script>
        $(function () {
            if ($('#tbxJudgeComment').length > 0) {
                $('input').jqBootstrapValidation();
                $('#tbxJudgeComment').limit('250', '#charsLeft');
            }
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
                        <h2><span>Thank you!</span></h2>
                        <div id="submitComment" runat="server">
                            <h4>One last step to claim your prize</h4>
                            <p>
                                What did you think of the video?<br>Leave your comment below and get our special
                                thank you reward for being a judge.
                            </p>
                            <asp:TextBox ID="tbxJudgeComment" runat="server" TextMode="MultiLine" name="comment"
                                class="input-xxlarge" Rows="4" required>
                            </asp:TextBox>
                            <p class="help-block">
                                You have <span id="charsLeft"></span> characters left
                            </p>
                            <br>
                            <asp:Button ID="btnSaveComment" runat="server" Text="Submit" 
                                class="btn btn-success btn-large" onclick="btnSaveComment_Click" />
                        </div>
                    </div>
                    <div id="printCoupon" runat="server"></div>
                </div>
            </div>
            <uc:Footer ID="ucFooter" runat="server" />
        </div>
    </form>
</body>
</html>