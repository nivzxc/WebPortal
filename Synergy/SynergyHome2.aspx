<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true"
    CodeFile="SynergyHome2.aspx.cs" Inherits="Synergy_SynergyHome2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div id="container">
        <div id="header">
            <table width="100%" cellspacing="0" cellpadding="0" style="color: White; font-family: Arial">
                <tr>
                    <td style="width: 500px; height: 175px; background-color: Black">
                        <img src="../Support/SportsFestBanner.png" alt="" />
                    </td>
                    <td style="background-color: Black">
                        &nbsp;
                    </td>
                    <td style="width: 230px; background-color: Black">
                        <asp:Literal runat="server" ID="litLatestEvent"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div id="leftcolumn">
            <%--Team Roster--%>
            <%--Header--%>
            <div style="background-color: #8A0829; font-size: large; padding: 5px; padding-left:10px; color: White;
                font-family: Arial; font-weight: bold" align="left">
                <asp:Literal runat="server" ID="litActiveEventTeamRoster"></asp:Literal></div>
            <%--Content--%>
            <div style="padding:5px">
                <asp:Literal runat="server" ID="litActiveTeamRoster"></asp:Literal>
            </div>
            <%--Event Information--%>
            <%--Header--%>
            <div style="background-color: #8A0829; font-size: large; padding: 5px; padding-left:10px;color: White;
                font-family: Arial; font-weight: bold" align="left">
                EVENT INFORMATION</div>
            <%--Content--%>
            <div style="padding:8px">
                <%--Active Team Standing--%>
                <table style="width: 100%;" cellspacing="0" cellpadding="0">
                    <%--Team Standing--%>
                    <tr>
                        <td style="width: 50%; text-align: left; font-size: small; color: black; font-family: Arial;
                            font-weight: bold">
                            <b>
                                <asp:Label ID="lblTeamLineUp" runat="server" Text="Team"></asp:Label></b>
                        </td>
                        <td style="width: 25%; text-align: center; font-size: small; color: black; font-family: Arial;
                            font-weight: bold">
                            <b>Win</b>
                        </td>
                        <td style="width: 25%; text-align: center; font-size: small; color: black; font-family: Arial;
                            font-weight: bold">
                            <b>Loss</b>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="litLineup"></asp:Literal>
                    <tr>
                        <td style="height: 20px;" colspan="3">
                            <hr style="width: 100%" />
                        </td>
                    </tr>
                    <%--Upcoming Schedule--%>
                    <tr>
                        <td colspan="3" style="font-size: small; color: black; font-family: Arial; font-weight: bold">
                            <b>Upcoming Schedules</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;" colspan="3">
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="litEcheduledEvents"></asp:Literal>
                </table>
                <%--Upcoming Schedule--%>
            </div>
        </div>

        <div id="rightcolumn">
            <%--Guidlines and Mechanics--%>
            <%--Header--%>
            <div style="background-color: white; font-size: large; padding: 5px;padding-left:10px; color: black;
                font-family: Arial; font-weight: bold" align="left">
                GUIDELINES AND MECHANICS</div>
            <%--Content--%>
            <div style="padding: 8px;">
                <table width="100%">
                    <tr>
                        <td style="font-size: small; font-family: Arial">
                            <a href="../Support/SportFestImage/Sportsfest%202012-2013_General%20Rules%20and%20Guidelines_05.21.2012.pdf"
                                onclick="window.open('../Support/SportFestImage/Sportsfest%202012-2013_General%20Rules%20and%20Guidelines_05.21.2012.pdf','popup','width=800,height=600,scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">
                                <b>Sports Fest 2012-2013 General Rules and Guidelines</b></a>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: small; font-family: Arial">
                            <a href="../Support/SportFestImage/SPORTSFEST%202012-13%20COUNTER%20STRIKE.pdf" onclick="window.open('../Support/SportFestImage/SPORTSFEST%202012-13%20COUNTER%20STRIKE.pdf','popup','width=800,height=600,scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">
                                <b>Counter Strike General Mechanics</b></a>
                        </td>
                    </tr>
                </table>
            </div>

            <hr style="width:100%">
           <%--Downloadables--%>
       
            <%--Content--%>
            <div style="padding: 8px;">
                <table width="100%">
                    <tr>
                        <td><img src="../Support/SportFestImage/avatarsCS.png" alt="" height="95px" 
                                width="400px" /></td>
                    </tr>
                </table>
            </div>
            <%--Counter Strike 101 : WEAPONS--%>
            <%--Header--%>
            <div style="background-color: white; font-size: large; padding: 5px;padding-left:10px; color: black;
                font-family: Arial; font-weight: bold" align="left">
                Counter Strike 101 : WEAPONS</div>
            <%--Content--%>
            <div style="padding: 8px;">
            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="vertical-align: top">
                                        <table width="100%">
                                            <tr>
                                                <td style="font-size: medium; font-family: Arial">
                                                    <b>Pistols</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    The pistols are the cheapest guns in the game. Considered as secondary weapons and usually used in pistol-only rounds; at the start of the game when nobody can afford larger guns; or when ammunition supplies for the larger guns have been exhausted. These weapons are semi-automatic and lack power but can be quite effective if aimed carefully.</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 5px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <a href="../Support/SportFestImage/Pistol.pdf"  onclick="window.open('../Support/SportFestImage/Pistol.pdf','popup','width=800,height=600,scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">View all Pistols</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <hr style="width:100%">
                             <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    
                                    <td style="vertical-align: top">
                                        <table width="100%">
                                            <tr>
                                                <td style="font-size: medium; font-family: Arial">
                                                    <b>Shotguns & Sub-machine Guns</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>The shotguns are a special group of weapons. Most players can either use shotguns properly or they can't. Correct use of a shotgun entails precise timing (not necessarily good aim) on when to shoot (especially with the pump-action M3 super 90) as wrong timing of a shot can mean death due to pumping time. Shotguns each round is reloaded individually, thus you can fire to interrupt the reload. <br /><br /> The sub-machine guns are the next cheapest and packed with more firepower than the pistols. Many players prefer to use SMGs over full-blown rifles due to the weight advantage (SMGs weigh less that the player can move faster) and if they're aimed properly, they can kill just as effectively and quickly as a rifle or shotgun.</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 5px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <a href="../Support/SportFestImage/Shotgun_SMG.pdf" onclick="window.open('../Support/SportFestImage/Shotgun_SMG.pdf','popup','width=800,height=600,scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">View all Shotguns & Sub-machine Guns</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                          <hr style="width:100%">
                             <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="vertical-align: top">
                                        <table width="100%">
                                            <tr>
                                                <td style="font-size: medium; font-family: Arial">
                                                    <b>Rifles & Machine Guns</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>The rifles are the most powerful and commonly-used automatic weapons. Players need a fair amount of money to be able to buy one and typically appear only during the second round of a game after pistols have been used in the first round. Due to the firepower of any of these rifles, aim is not a requirement but their effectiveness can increase dramatically if the seconds are taken to crouch down and aim for a player's head. Doing so will mean two or three shots are fired instead of ten.<br /><br />There is only one machine gun: the M249. It is one of the most expensive weapons, but also has one of the highest firing rates and can be used very effectively as a spray gun. Contrary to popular belief, the machine gun is actually quite accurate when firing one round at a time and can be used to snipe in a similar manner as a rifle.</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 5px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <a href="../Support/SportFestImage/Rifle.pdf" onclick="window.open('../Support/SportFestImage/Rifle.pdf','popup','width=800,height=600,scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">View all Rifles & Machine Guns</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
            </div>
        </div>
    </div>
    <div id="divAdmin" runat="server" style="padding: 5px;" align="center">
        <asp:HyperLink runat="server" ID="HyperLink1" Font-Size="Small" Text="[Sport Fest Administration]"
            NavigateUrl="~/Synergy/Synergy.aspx"></asp:HyperLink>
    </div>
</asp:Content>
