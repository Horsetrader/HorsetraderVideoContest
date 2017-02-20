<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="HorseTraderWeb.user_controls.Search" %>
<div id="header" class="logo topSpace">
    <div class="searchBoxContainer">
        <div style="float:right;">
            <asp:Button runat="server" ID="btnSearch" CssClass="searchButton" Text="Search" 
                style="width:80px;" onclick="btnSearch_Click" />
            <div class="searchInnerContainer">
                <asp:TextBox runat="server" ID="tbxSearch" CssClass="searchBox watermark" style="border:0; -webkit-box-shadow: none; box-shadow: none; font-size:12px; height: 12px;"></asp:TextBox>
                <asp:DropDownList runat="server" ID="ddlSearchCategories" CssClass="searchCategories">
                    <asp:ListItem Value="All">All Sections</asp:ListItem>
                    <asp:ListItem Value="HorsesForSale">Horses For Sale</asp:ListItem>
                    <asp:ListItem Value="TackAndMore">Tack & Products</asp:ListItem>
                    <asp:ListItem Value="Training">Trainers</asp:ListItem>
                    <asp:ListItem Value="Boarding">Boarding</asp:ListItem>
                    <asp:ListItem Value="Stallions">Stallions</asp:ListItem>
                    <asp:ListItem Value="RealEstate">Real Estate</asp:ListItem>
                    <asp:ListItem Value="Trailers">Trailers</asp:ListItem>
                    <asp:ListItem Value="Trucks">Trucks</asp:ListItem>
                    <asp:ListItem Value="Other">Services & More</asp:ListItem>
                    <asp:ListItem Value="Showdates">Shows& Events</asp:ListItem>
                </asp:DropDownList>
                <img alt="" src="<%= ConfigurationManager.AppSettings["WebsiteURL"] %>/images/buttons/searchboxSeparator.png" class="searchBoxSeparator" />
            </div>
        </div>
        <div class="headerLinks">
            <a href="http://www.horsetrader.com/media/WhatsNew.pdf" class="exampleyudu cboxElement blueLink">Check out what's new!</a>
        </div>
        <%--<div class="headerLinks">
            <a href="https://order.horsetrader.com/login.aspx?pub_number=101" class="examplelogin cboxElement blueLink">Place an Ad</a>
            <span>or</span>
            <a class="blueLink">Login</a>
        </div>--%>
        <div style="clear:both;"></div>
    </div>
</div>