<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="HorseTraderWeb.user_controls.Footer" %>
<div id="footer" class="row">
    <div class="span12">
        <!-- Horsetrader Trademark -->
        <script type="text/javascript">
            var year = new Date();
            document.write(year.getFullYear());
        </script>
        California Horsetrader, Inc. |
        <!-- Privacy Policy -->
        <a href="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/info_privacy.asp" class="example5 cboxElement">Privacy Policy</a> |
        <!-- Refund Policy  -->
        <a href="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/info_refund.asp" class="popupRefundInfo cboxElement">Refund Policy</a>

        <div style="float:right;">
            <a href="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>">Home</a>
			<a href="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/shows-and-events">Shows &amp; Events</a>
			<a href="http://news.horsetrader.com/">News</a>
			<a href="http://directory.horsetrader.com/">Directory</a>
			<a href="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/info_help.asp">Help</a>
        </div>
    </div>
</div>