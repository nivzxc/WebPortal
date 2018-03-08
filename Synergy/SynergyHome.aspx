<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/Default.master" AutoEventWireup="true"
    CodeFile="SynergyHome.aspx.cs" Inherits="Synergy_SynergyHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
   
    <style type="text/css">
        .style3
        {
            width: 280px;
            height: 15px;
        }
        .style5
        {
            height: 22px;
        }
        .style6
        {
            width: 374px;
        }
        .style7
        {
            font-size: x-small;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
  
    <div id="SContainer"class="ContainerSportsFest">
        <div id="header" class="HeaderSportsfest">
            <img src="../Support/SportsFestBanner.png" alt="" class="BannerSportsFest" width="175px" />
        </div>
        <div id="leftcolumn" class="LeftColumn" align="left">
            <%--Header--%>            <%--Content--%>
            <div align="left" class="Literal">
                <asp:Literal runat="server" ID="litActiveEventTeamRoster"></asp:Literal></div>
            <%--Guidlines and Mechanics--%>
            <div>
                <asp:Literal runat="server" ID="litActiveTeamRoster"></asp:Literal>
            </div>
           
        </div>

        <div id="rightcolumn" class="RightColumn">
            <%--Header--%>            <%--Content--%>
            <div 
                style="background-color: white; font-size: large; padding-left:10px; color: black;
                font-family: Arial; font-weight: bold; float: right; width: 280px; padding-top: 5px; padding-bottom: 5px;" 
                align="left">
                GUIDELINES AND MECHANICS</div>
            <div style="float: right; width: 280px;">
                
                &nbsp;<table frame="void" style="border-color: #FFFFFF">
                    <tr>
                        <td style="border-color: #FFFFFF; font-size:x-small; font-family: Arial" 
                            class="style5">
                            <a href="../Support/SportFestImage/Sportsfest%202012-2013_General%20Rules%20and%20Guidelines_05.21.2012.pdf"
                                onclick="window.open('../Support/SportFestImage/Sportsfest%202012-2013_General%20Rules%20and%20Guidelines_05.21.2012.pdf','popup','width=800,height=600,scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">
                                <b style="border-color: #FFFFFF">Sports Fest 2012-2013 General Rules and Guidelines</b></a>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td style="border-color: #FFFFFF; font-size: x-small; font-family: Arial">
                            <a href="../Support/SportFestImage/Sportsfest_Table Tennis Mechanics_07.31.2012.docx" >
                                <b>Table Tennis General Mechanics</b></a>
                        </td>
                    </tr>--%>
                </table>
               <hr style="width:91%">
            </div>

            <%--Event Information--%>
             <%--Header--%>            <%--Content--%>
            <div 
                style="background-color: white; font-size: large; padding-left:10px; color: black;
                font-family: Arial; font-weight: bold; float: right; width: 280px; padding-top: 5px; padding-bottom: 5px;" 
                align="left">
                EVENT INFORMATION</div>
            <%--Active Team Standing--%>
            <br />
            <div style="float: right; width: 280px; font-size: small;">
                <%--Team Standing--%>
                <table 
                    class="TablesSportsfest" frame="void" style="border-color: #FFFFFF">
                    <%--Upcoming Schedule--%>
                    <%--<tr style="border-color: #FFFFFF;">
                        <td style="border-color: #FFFFFF;" class="style6">
                            <b style="border-color: #FFFFFF">
                                <asp:Label ID="lblTeamLineUp" runat="server" Text="Team" 
                                style="font-size: x-small"></asp:Label></b>
                        </td>
                        <td style="border-color: #FFFFFF; text-align: center; font-size: small; color: black; font-family: Arial;
                            font-weight: bold" class="style3">
                            <b style="font-family: Helvetica;" class="style7">Total Score</b>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="litLineup"></asp:Literal>
                    <tr style="border-color: #FFFFFF">
                        <td style="border-color: #FFFFFF;" colspan="3">
                            <hr style="width: 101%" />
                        </td>
                    </tr>--%>
                    <%--Upcoming Schedule--%>
                    <tr style="border-color: #FFFFFF">
                        <td colspan="3" 
                            style="border-color: #FFFFFF;" 
                            class="style5">
                            <b style="font-size: xx-small">Upcoming Schedules</b>
                           <%-- <div 
                style="background-color: white; font-size: large; padding-left:10px; color: black;
                font-family: Arial; font-weight: bold; float: right; width: 280px; padding-top: 5px; padding-bottom: 5px;" 
                align="left">
                Upcoming Schedules</div>--%>
                        </td>
                    </tr>
                    <asp:Literal runat="server" ID="litEcheduledEvents"></asp:Literal>
                </table>
                <%--Downloadables--%>
            </div>

           <%--Content--%>            <%--<div style="padding: 8px;">
                <table width="100%">
                    <tr>
                        <td><img src="../Support/SportFestImage/csdownloadnow.png" alt=""  /></td>
                    </tr>
                    <tr>
                        <td>
                        Wallpaper: <a href="../Support/SportFestImage/wallpaper.jpg" onclick="window.open('../Support/SportFestImage/wallpaper.jpg','popup','width=1024,height=768,scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">Wallpaper</a><br />
                        Team Avatars: <a href="../Support/SportFestImage/Armed n Loaded_big.png" onclick="window.open('../Support/SportFestImage/ArmednLoadedbig.png','popup','width=200,height=270,scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">Armed N Loaded</a> | <a href="../Support/SportFestImage/charliesangelsbig.png" onclick="window.open('../Support/SportFestImage/charliesangelsbig.png','popup','width=200,height=270,scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">Charlie’s Angels</a> | <a href="../Support/SportFestImage/TASKbig.png" onclick="window.open('../Support/SportFestImage/TASKbig.png','popup','width=200,height=270,scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">TASK</a> | <a href="../Support/SportFestImage/BadAssbig.png" onclick="window.open('../Support/SportFestImage/BadAssbig.png','popup','width=200,height=270,scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">Bad Ass.</a>
                        </td>
                    </tr>
                </table>
            </div>--%>            <%--Counter Strike 101 : WEAPONS--%>            <%--Header--%>            <%--<div style="background-color: white; font-size: large; padding: 5px;padding-left:10px; color: black;
                font-family: Arial; font-weight: bold" align="left">
                Counter Strike 101 : WEAPONS</div>--%>            <%--Content--%>            <%--<div style="padding: 8px;">
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
            </div>--%>
            <%--<div style="padding: 8px;">
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
            </div>--%>
        </div>
    </div>
    
    </br>
    </asp:Content>

