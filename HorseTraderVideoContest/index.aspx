<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="HorsetraderVideoContest.index" %>
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
    <!--Scripts-->
    <script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
    <script src="assets/js/jquery.countdown.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/page.js"></script>
    <script>
        $(function () {
            var contestDeadline = new Date();
            contestDeadline = new Date($('#deadlineDate').val());
            $('#countdown').countdown({ until: contestDeadline,
                layout: '{dn} {dl}, {hn} {hl}, {mn} {ml} and {sn} {sl}'
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
                    <div class="row" style="margin-top:10px;">
                        <div class="span5">
                            <div style="text-align: left;font-size: 28px;font-weight: bold; color: Gray;">Top People's Choice videos</div>
                            <div style="color:#d31; font-weight:bold; margin-top: 5px;">COUNTDOWN TO VOTING DEADLINE:</div>
                            <div id="countdown"></div>
                        </div>
                        <div class="span7">
                            <div id="secondaryBanner" runat="server"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="span12 text-right">
                            <ul class="nav nav-pills float-right">
                                <li class="active">
                                    <a href="list-entries.aspx">See all & vote! <i class="icon-chevron-right"></i></a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="row">
                        <div class="span12">
                            <div id="frontrunner" runat="server" class="generic-container">
                                <div class="row">
                                    <div class="span12">
                                        <div style="max-width: 56px; float: left;">
                                            <img class="top-entries-place" src="assets/img/1.jpg" />
                                            <span id="frontrunnerVotes" class="label label-info" runat="server"></span>
                                        </div>
                                        <h4 id="frontrunnerTitle" runat="server"></h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="span6 offset2">
                                        <a id="frontrunnerEntry" runat="server" class="show-video" href="#videoModal" data-toggle="modal">
                                            <div class="video">
                                                <div class="large-player"></div>
                                                <asp:Image ID="imgFrontRunnerEntry" runat="server" class="large-thumbnail" />
                                            </div>
                                        </a>
                                    </div>
                                    <div class="span3">
                                        <p id="frontrunnerContestant" runat="server"></p>
                                        <p id="frontrunnerDescription" class="entry-description" runat="server"></p>
                                        <p id="frontrunnerComments" runat="server"></p>
                                        <a id="frontrunnerViewComments" runat="server" href="#">View all comments</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <asp:Repeater ID="repContestEntries" runat="server" onitemdatabound="repContestEntries_ItemDataBound">
                            <ItemTemplate>
                                <div class="span3">
                                    <div class="generic-container" style="padding: 15px 5px;">
                                        <div class="row">
                                            <div class="span3">
                                                <div style="max-width: 56px; float: left;">
                                                    <img class="top-entries-place" src='assets/img/<%# Eval("Place") %>.jpg' />
                                                    <span class="label label-info votes"><%# Eval("PeoplesChoiceScore") %> votes</span>
                                                </div>
                                                <h5><%# Eval("Title") %></h5>
                                            </div>
                                        </div>
                                        <br>
                                        <div class="row">
                                            <div class="span3">
                                                <a class="show-video" video-url='<%# Eval("EmbedVideoURL") %>' video-title='<%# Eval("Title") %>'
                                                    href="#videoModal" data-toggle="modal">
                                                    <div class="video">
                                                        <div class="small-player"></div>
                                                        <asp:Image ID="imgFrontRunnerEntry" runat="server" ImageUrl='<%# GetYoutubeThumbnailUrl(Eval("VideoURL").ToString()) %>'
                                                            class="small-thumbnail" />
                                                    </div>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="span3">
                                                <p>Submitted by <%# Eval("FirstName") %> on <%# DateTime.Parse(Eval("CreatedDate").ToString()).ToShortDateString() %></p>
                                                <p class="entry-description"><%# Eval("Description") %></p>
                                                <%# GetViewCommentsLink(Eval("ContestEntryID").ToString(), Eval("NumberOfComments").ToString()) %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align:center;">
                                    <asp:Label ID="lblNoEntries" runat="server" Visible="false" style="margin-top:15px;"></asp:Label>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <br>
                    <div class="row">
                        <div class="span12 text-right">
                            <ul class="nav nav-pills float-right">
                                <li class="active">
                                    <a href="list-entries.aspx">See all & vote! <i class="icon-chevron-right"></i></a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <uc:Footer ID="ucFooter" runat="server" />
        </div>
        <!-- Video display modal -->
        <div id="videoModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="videoHeader" aria-hidden="true">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h3 id="videoTitle"></h3>
            </div>
            <div class="modal-body form-horizontal" style="text-align: center;">
                <iframe id="videoFrame" width="420" height="315" frameborder="0" allowfullscreen></iframe>
            </div>
            <div class="modal-footer">
                <button class="btn" data-dismiss="modal" aria-hidden="true">Close</button>
            </div>
        </div>
        <input id="deadlineDate" runat="server" type="hidden" />
    </form>
</body>
</html>