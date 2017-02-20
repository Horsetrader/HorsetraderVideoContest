<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view-comments.aspx.cs" Inherits="HorsetraderVideoContest.view_comments" %>
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
    <link rel="stylesheet" type="text/css" href="assets/css/page.css" media="screen">
    <link rel="stylesheet" type="text/css" href="assets/css/jquery.countdown.css" media="screen">
    <link rel="icon" href="http://www.horsetrader.com/favicon.ico" type="image/x-icon">
    <script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
    <script src="assets/js/jqBootstrapValidation.js" type="text/javascript"></script>
    <script src="assets/js/jquery.limit-1.2.source.js"></script>
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
    <!--Facebook-->
    <meta property="og:site_name" content="Horsetrader.com Video Contest" />
    <meta property="fb:admins" content="1411365756,776784509,1055847817" />
    <meta property="fb:app_id" content="115642931869437" />
    <meta property="og:type" content="Company" />
    <meta property="og:url" content="<%= pageUrl %>" />
    <meta property="og:title" content="<%= pageTitle %>" />
    <meta property="og:image" content="<%= pageThumbnail %>" />
    <meta property="og:description" content="<%= pageDescription %>" />
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
        <uc:Header ID="ucHeader" runat="server" />
        <div class="container">
            <uc:BannerAds ID="ucBannerAds" runat="server" />
            <div class="row">
                <div class="span12">
                    <br>
                    <div class="row">
                        <div class="span6 text-left">
                            <ul class="nav nav-pills float-left">
                                <li class="active">
                                    <a href="list-entries.aspx"><i class="icon-chevron-left"></i> All Videos!</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="row">
                        <div class="span12">
                            <div class="generic-container">
                                <div class="row">
                                    <div class="span6 float-left">
                                        <div style="max-width: 56px; float: left;">
                                            <span id="frontrunnerVotes" class="label label-info" runat="server"></span>
                                        </div>
                                    </div>
                                    <div class="span5 float-right" style="text-align: right">
                                        <script type="text/javascript">
                                            function fbs_clickAdv() {
                                                u = "<%= pageUrl %>";
                                                t = "<%= pageTitle %>";
                                                window.open('http://www.facebook.com/sharer.php?u=' + encodeURIComponent(u) + '&t=' + encodeURIComponent(t),
                                                'sharer', 'toolbar=0,status=0,width=626,height=436');
                                                return false;
                                            }
                                        </script>
                                        <a rel="nofollow" href="http://www.facebook.com/share.php?u=<;url>&t=<%= pageTitle %>"
                                            onclick="return fbs_clickAdv()" target="_blank">
                                            <img src="assets/img/share_icon.gif" alt="Share on Facebook" />
                                        </a>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="span12">
                                        <h4 id="frontrunnerTitle" runat="server"></h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="span6 offset2">
                                        <iframe id="videoFrame" runat="server" width="420" height="315" frameborder="0" allowfullscreen></iframe>
                                    </div>
                                    <div class="span3">
                                        <p id="frontrunnerContestant" runat="server"></p>
                                        <p id="frontrunnerDescription" class="entry-description" runat="server"></p>
                                        <asp:ImageButton ID="ibtnVote" runat="server" OnClick="ibtnVote_Click" />
                                    </div>
                                </div>
                                <br>
                                <div class="row">
                                    <div class="span8 offset2">
                                        <div id="signInPostComment" runat="server" style="background: #eee; border: 1px solid #ccc; width: 617-x; height: 60px; padding: 3px">
                                            <a href="judge-login.aspx?from=view-comments.aspx?id=<%= id %>">Sign in</a> to post a comment
                                        </div>
                                        <div id="postComment" runat="server">
                                        <asp:TextBox ID="tbxJudgeComment" runat="server" TextMode="MultiLine" name="comment"
                                            class="input-xxlarge" Rows="4" style="width: 605px; margin-bottom:10px;" placeholder="Leave a comment">
                                        </asp:TextBox>
                                        <p class="help-block" style="float:left;">
                                            You have <span id="charsLeft"></span> characters left
                                        </p>
                                        <asp:Button ID="btnSaveComment" runat="server" Text="Post" style="float:right;" 
                                            class="btn btn-success" onclick="btnSaveComment_Click" />
                                        </div>
                                        <div style="clear: both; padding-top: 20px;">
                                        <asp:Repeater ID="repComments" runat="server" onitemdatabound="repComments_ItemDataBound">
                                            <ItemTemplate>
                                                <p style="background-color: #F5F5F5; padding: 12px;">
                                                    <b><%# Eval("ScreenName") %> says:</b>
                                                    <br>
                                                    <%# Eval("Comment") %>
                                                </p>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <p style="padding: 12px;">
                                                    <b><%# Eval("ScreenName") %> says:</b>
                                                    <br>
                                                    <%# Eval("Comment") %>
                                                </p>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align:center;">
                                                    <asp:Label ID="lblNoComments" runat="server" Visible="false" style="margin-top:15px;"></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="span6 text-left">
                            <ul class="nav nav-pills float-left">
                                <li class="active">
                                    <a href="list-entries.aspx"><i class="icon-chevron-left"></i> All Videos!</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <uc:Footer ID="ucFooter" runat="server" />
        </div>
    </form>
</body>
</html>