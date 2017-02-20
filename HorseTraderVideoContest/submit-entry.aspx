<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="submit-entry.aspx.cs" Inherits="HorsetraderVideoContest.submit_entry" %>
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
    <script src="assets/js/jquery.limit-1.2.source.js"></script>
    <script>
        $(function () {
            $('input').jqBootstrapValidation();
            $('#tbxDescription').limit('140', '#charsLeft');
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
                    <h3 id="firstPrize" runat="server"></h3>
                    <div id="secondaryBanner" runat="server"></div>
                    <div id="errorMessage" runat="server" class="alert alert-error"></div>
                    <div id="infoMessage" runat="server" class="alert alert-info"></div>
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
                            <!-- full-name input-->
                            <div class="control-group">
                                <label class="control-label">Your name</label>
                                <div class="controls">
                                    <asp:TextBox ID="tbxFirstName" runat="server" name="first-name" placeholder="first name" 
                                        class="input-medium" MaxLength="50" required></asp:TextBox>
                                    <asp:TextBox ID="tbxLastName" runat="server" name="last-name" placeholder="last name"
                                        class="input-medium" MaxLength="50" required></asp:TextBox>
                                </div>
                            </div>
                            <!-- title input-->
                            <div class="control-group">
                                <label class="control-label">Your video title</label>
                                <div class="controls">
                                    <asp:TextBox ID="tbxTitle" runat="server" name="title" placeholder="what shall we call your horse video?"
                                        class="input-xlarge" MaxLength="20" required></asp:TextBox>
                                </div>
                            </div>
                            <!-- description input-->
                            <div class="control-group">
                                <label class="control-label">Description</label>
                                <div class="controls">
                                    <asp:TextBox ID="tbxDescription" runat="server" TextMode="MultiLine" name="description" 
                                        placeholder="tell us what our audience will see in this funny horse video"
                                        class="input-xlarge" Rows="4" required>
                                    </asp:TextBox>
                                    <p class="help-block">
                                        You have <span id="charsLeft"></span> characters left
                                    </p>
                                </div>
                            </div>
                            <!-- video-url input-->
                            <div class="control-group">
                                <div style="max-width: 180px; float: left; text-align: right;">
                                    <label class="control-label">Submit Video</label>
                                    <img src="assets/img/youtube-logo.jpg" alt="Submit your YouTube&reg; video!" style="margin-right: 29px;" />
                                </div>
                                <div class="controls">
                                    <asp:TextBox ID="tbxVideoUrl" runat="server" name="video-url" 
                                        placeholder="paste your YouTube&reg; link here"
                                        class="input-xlarge" MaxLength="250"
                                        required>
                                    </asp:TextBox>
                                    <!--
                                        data-validation-regex-regex='http://(?:www\.)?youtu(?:be\.com/watch\?v=|\.be/)([\w\-]+)(&(amp;)?[\w\?=]*)?'
                                        data-validation-regex-message="Please enter a valid YouTube&reg; URL<br>Example: http://www.youtube.com/watch?v=1234"
                                    -->
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
                        </fieldset>
                        <div id="disclaimer">
                            <p>
                                <input type="checkbox" name="terms-and-conditions" required 
                                    data-validation-required-message= "Please check the box to confirm that you agree with the contest rules" />
                                <span>I have read and I agree to the</span>
                                <a class="task-details" href="#disclaimerModal" data-toggle="modal">contest rules</a>
                            </p>
                            <p class="help-block"></p>
                            <asp:Button ID="btnSubmitVideo" runat="server" type="button" 
                                class="btn btn-large btn-success" Text="Submit" 
                                onclick="btnSubmitVideo_Click">
                            </asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <uc:Footer ID="ucFooter" runat="server" />
        </div>
        <!-- Contest rules modal -->
        <div id="disclaimerModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="disclaimerHeader" aria-hidden="true">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h3>Contest Rules</h3>
            </div>
            <div class="modal-body form-horizontal">
                <p id="disclaimerText" runat="server"></p>
            </div>
            <div class="modal-footer">
                <button class="btn" data-dismiss="modal" aria-hidden="true">Close</button>
            </div>
        </div>
        <!-- Confirmation modal -->
        <div id="confirmationModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="confirmationHeader" aria-hidden="true">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h3>Congratulations!</h3>
            </div>
            <div class="modal-body form-horizontal">
                <p id="confirmationText" runat="server"></p>
            </div>
            <div class="modal-footer">
                <button class="btn" data-dismiss="modal" aria-hidden="true">Close</button>
            </div>
        </div>
    </form>
</body>
</html>