<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list-entries.aspx.cs" Inherits="HorsetraderVideoContest.list_entries" %>
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
    <link rel="icon" href="http://www.horsetrader.com/favicon.ico" type="image/x-icon">
    <!--Scripts-->
    <script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/page.js"></script>
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
                    <h3 class="text-center" style="color:Gray;">Choose your favorite!</h3>
                    <h3 class="text-center">Remember: You can change your vote until the deadline!</h3>
                    <div class="row">
                        <div class="span6 text-left">
                            <ul class="nav nav-pills float-left">
                                <li class="active">
                                    <a href="index.aspx"><i class="icon-chevron-left"></i> Back to main page</a>
                                </li>
                            </ul>
                        </div>
                        <div class="span6 text-right">
                            <ul id="voteButtons" runat="server" class="nav nav-pills float-right">
                                <li class="active">
                                    <a style="background-color: #51a351" href="judge-sign-up.aspx">Not voted? It's time!</a>
                                </li>
                                <li class="active">
                                    <a style="background-color: #51a351" href="judge-login.aspx">Change your vote</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div id="entries-list" class="row">
                        <asp:Repeater ID="repContestEntries" runat="server" onitemdatabound="repContestEntries_ItemDataBound">
                            <ItemTemplate>
                                <div class="span4">
                                    <div class='<%# SetSelectedClass(int.Parse(Eval("ContestEntryID").ToString())) %>'>
                                        <span class="label label-info votes"><%# Eval("PeoplesChoiceScore") %> votes</span>
                                        <h4><%# Eval("Title") %></h4>
                                        <a class="show-video" video-url='<%# Eval("EmbedVideoURL") %>' video-title='<%# Eval("Title") %>'
                                            href="#videoModal" data-toggle="modal">
                                            <div class="video">
                                                <div class="medium-player"></div>
                                                <asp:Image ID="imgFrontRunnerEntry" runat="server" ImageUrl='<%# GetYoutubeThumbnailUrl(Eval("VideoURL").ToString()) %>'
                                                    class="medium-thumbnail"/>
                                            </div>
                                        </a>
                                        <div class="row">
                                            <div class="entry-info span4">
                                                <p>Submitted by <%# Eval("FirstName") %></p>
                                                <p style="color: Gray"><%# Eval("Description") %></p>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="span4" style="text-align: center;">
                                                <asp:ImageButton ID="ibtnVote" runat="server" OnClick="ibtnVote_Click" CommandArgument='<%# Eval("ContestEntryID") %>'
                                                    ImageUrl='<%# SetVoteButton(int.Parse(Eval("ContestEntryID").ToString())) %>' />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="span4" style="text-align: center;">
                                                <br>
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
    </form>
</body>
</html>