<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="HorseTraderWeb.user_controls.Header" %>
<div id="topnav">
    <div id="topnavbox">
        <div id="topnavleft">
            <a href="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>">Home</a>
            <a class="exampleyudu cboxElement" href="http://content.yudu.com.s3.amazonaws.com/Aoy9u/Horsetrader/resources/index.htm">Read Digital Edition</a>
            <a href="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/shows-and-events">Shows/Events</a> 
            <a href="http://news.horsetrader.com/">News</a> 
            <a href="http://directory.horsetrader.com/">Directory</a>
			<a href="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/images/2011_Display_Rates.pdf">Ad Rates</a>
            <a href="http://news.horsetrader.com/signup-form/">Ad Specials</a>
        </div>
		<div id="topnavsocial">
            <a href="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/info_help.asp"><img alt="" src="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/images/icons/silk/help.png" style="vertical-align:text-bottom;" /><span style="vertical-align:top; margin:5px;">Help</span></a>
			<a href="http://twitter.com/thehorsetrader"><img src="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/icons/twitterIcon.png" alt="twitter"/></a> 
            <a href="http://www.facebook.com/pages/California-Horsetrader/191721637197"><img src="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/icons/facebook_logo.gif" alt="facebook" /></a>
		</div>
        <div id="topnavright">
            Go to FastAd#:
            <asp:TextBox runat="server" ID="txtFastAd"></asp:TextBox>
            <asp:Button ID="btnGoAd" runat="server" class="sexybutton sexysimple sexysmall sexyred" OnClick="btnGoToFastAd_Click" Text="Go!" />
            <%--<button runat="server" id="btnGoToAd" class="sexybutton sexysimple sexysmall sexyred" onserverclick="btnGoToFastAd_Click">Go!</button>--%>
        </div>
    </div>
</div>