using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace HqWeb
{
 namespace Forums
 {
  public abstract class clsBB
  {

   public clsBB()
   {

   }

   public static string FormatContents(string pContent)
   {
    string strReturn = pContent;

    Regex regPao;
    regPao = new Regex(@"\[name=([^\]]+)\]([^\]]+)\[\/name\]");
    strReturn = regPao.Replace(strReturn, "<a name=\"$1\">$2</a>");
    regPao = new Regex(@"\[url\]([^\]]+)\[\/url\]");
    strReturn = regPao.Replace(strReturn, "<a href=\"$1\">$1</a>");
    regPao = new Regex(@"\[url=([^\]]+)\]([^\]]+)\[\/url\]");
    strReturn = regPao.Replace(strReturn, "<a href=\"$1\">$2</a>");
    regPao = new Regex(@"\[img\]([^\]]+)\[\/img\]");
    strReturn = regPao.Replace(strReturn, "<img src=\"$1\" />");
    regPao = new Regex(@"\[b\](.+?)\[\/b\]");
    strReturn = regPao.Replace(strReturn, "<b>$1</b>");
    regPao = new Regex(@"\[i\](.+?)\[\/i\]");
    strReturn = regPao.Replace(strReturn, "<i>$1</i>");
    regPao = new Regex(@"\[u\](.+?)\[\/u\]");
    strReturn = regPao.Replace(strReturn, "<u>$1</u>");
    regPao = new Regex(@"\[size=([^\]]+)\]([^\]]+)\[\/size\]");
    strReturn = regPao.Replace(strReturn, "<span style=\"font-size: $1px\">$2</span>");
    regPao = new Regex(@"\[color=([^\]]+)\]([^\]]+)\[\/color\]");
    strReturn = regPao.Replace(strReturn, "<span style=\"color: $1\">$2</span>");
    regPao = new Regex(@"\[quote=([^\]]+)\]([^\]]+)\[\/quote\]");
    strReturn = regPao.Replace(strReturn, "<blockquote style='border-right: green 1px solid; border-top: green 1px solid; border-left: green 1px solid; border-bottom: green 1px solid; background-color: #F0FFF0; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; font-size: small; border-color: #009933;'>Quote by <a href='http://hq.sti.edu/Userpage/UserPage.aspx?username=$1'>$1</a><br><br>$2<br></blockquote>");
    regPao = new Regex(@"\[quote=([^\]]+)\]([^\]]+)\[\/quote\]");
    strReturn = regPao.Replace(strReturn, "<blockquote style='border-right: green 1px solid; border-top: green 1px solid; border-left: green 1px solid; border-bottom: green 1px solid; background-color: #F0FFF0; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; font-size: small; border-color: #009933;'>Quote by <a href='http://hq.sti.edu/Userpage/UserPage.aspx?username=$1'>$1</a><br><br>$2<br></blockquote>");
    regPao = new Regex(@"\[quote=([^\]]+)\]([^\]]+)\[\/quote\]");
    strReturn = regPao.Replace(strReturn, "<blockquote style='border-right: green 1px solid; border-top: green 1px solid; border-left: green 1px solid; border-bottom: green 1px solid; background-color: #F0FFF0; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; font-size: small; border-color: #009933;'>Quote by <a href='http://hq.sti.edu/Userpage/UserPage.aspx?username=$1'>$1</a><br><br>$2<br></blockquote>");
    regPao = new Regex(@"\[quote=([^\]]+)\]([^\]]+)\[\/quote\]");
    strReturn = regPao.Replace(strReturn, "<blockquote style='border-right: green 1px solid; border-top: green 1px solid; border-left: green 1px solid; border-bottom: green 1px solid; background-color: #F0FFF0; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; font-size: small; border-color: #009933;'>Quote by <a href='http://hq.sti.edu/Userpage/UserPage.aspx?username=$1'>$1</a><br><br>$2<br></blockquote>");
    regPao = new Regex(@"\[quote=([^\]]+)\]([^\]]+)\[\/quote\]");
    strReturn = regPao.Replace(strReturn, "<blockquote style='border-right: green 1px solid; border-top: green 1px solid; border-left: green 1px solid; border-bottom: green 1px solid; background-color: #F0FFF0; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; font-size: small; border-color: #009933;'>Quote by <a href='http://hq.sti.edu/Userpage/UserPage.aspx?username=$1'>$1</a><br><br>$2<br></blockquote>");
    //strReturn = regPao.Replace(strReturn, "<blockquote>Quote by <a href='http://hq.sti.edu/Userpage/UserPage.aspx?username=$1'>$1</a><br><br>$2<br></blockquote>");
    //strReturn = regPao.Replace(strReturn, "<blockquote style='border-right: green 1px solid; border-top: green 1px solid; border-left: green 1px solid; border-bottom: green 1px solid; background-color: #F0FFF0; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; font-size: small; border-color: #009933;'>Quote by <a href='http://hq.sti.edu/Userpage/UserPage.aspx?username=$1'>$1</a><br><br>$2<br></blockquote>");

    // Smileys
    strReturn = Regex.Replace(strReturn, "\n", "<br>");
    strReturn = Regex.Replace(strReturn, ":sad:", "<img src='../Support/Smiles/sad.gif'>");
    strReturn = Regex.Replace(strReturn, ":biggrin:", "<img src='../Support/Smiles/biggrin.gif'>");
    strReturn = Regex.Replace(strReturn, ":laugh:", "<img src='../Support/Smiles/laugh.gif'>");
    strReturn = Regex.Replace(strReturn, ":cool:", "<img src='../Support/Smiles/cool.gif'>");
    strReturn = Regex.Replace(strReturn, ":angry:", "<img src='../Support/Smiles/mad.gif'>");
    strReturn = Regex.Replace(strReturn, ":rock:", "<img src='../Support/Smiles/rock.gif'>");
    strReturn = Regex.Replace(strReturn, ":tounge:", "<img src='../Support/Smiles/tounge.gif'>");
    strReturn = Regex.Replace(strReturn, ":wink:", "<img src='../Support/Smiles/wink.gif'>");
    strReturn = Regex.Replace(strReturn, ":smile:", "<img src='../Support/Smiles/smile.gif'>");
    strReturn = Regex.Replace(strReturn, ":wow:", "<img src='../Support/Smiles/wow.gif'>");
    strReturn = Regex.Replace(strReturn, ":no:", "<img src='../Support/Smiles/no.gif'>");
    strReturn = Regex.Replace(strReturn, ":batwood:", "<img src='../Support/Smiles/batwood.gif'>");
    strReturn = Regex.Replace(strReturn, ":drunk:", "<img src='../Support/Smiles/drunk.gif'>");
    strReturn = Regex.Replace(strReturn, ":friendhug:", "<img src='../Support/Smiles/friendhug.gif'>");
    strReturn = Regex.Replace(strReturn, ":grouphug:", "<img src='../Support/Smiles/grouphug.gif'>");
    strReturn = Regex.Replace(strReturn, ":wave:", "<img src='../Support/Smiles/wave.gif'>");
    strReturn = Regex.Replace(strReturn, ":sos:", "<img src='../Support/Smiles/sos.gif'>");
    strReturn = Regex.Replace(strReturn, ":party:", "<img src='../Support/Smiles/party.gif'>");
    strReturn = Regex.Replace(strReturn, ":sleep:", "<img src='../Support/Smiles/sleep.gif'>");
    strReturn = Regex.Replace(strReturn, ":hooray:", "<img src='../Support/Smiles/hooray.gif'>");
    strReturn = Regex.Replace(strReturn, ":cry:", "<img src='../Support/Smiles/cry.gif'>");
    strReturn = Regex.Replace(strReturn, ":roll:", "<img src='../Support/Smiles/roll.gif'>");
    strReturn = Regex.Replace(strReturn, ":thumb:", "<img src='../Support/Smiles/thumb.gif'>");
    strReturn = Regex.Replace(strReturn, ":giggle:", "<img src='../Support/Smiles/giggle.gif'>");
    strReturn = Regex.Replace(strReturn, ":whistle:", "<img src='../Support/Smiles/whistle.gif'>");
    strReturn = Regex.Replace(strReturn, ":ohmy:", "<img src='../Support/Smiles/ohmy.gif'>");
    strReturn = Regex.Replace(strReturn, ":comfort:", "<img src='../Support/Smiles/emo-comfort.gif'>");
    strReturn = Regex.Replace(strReturn, ":firegun:", "<img src='../Support/Smiles/emo-firegun.gif'>");
    strReturn = Regex.Replace(strReturn, ":gun:", "<img src='../Support/Smiles/emo-gun.gif'>");
    strReturn = Regex.Replace(strReturn, ":gossip:", "<img src='../Support/Smiles/emo-gossip.gif'>");
    strReturn = Regex.Replace(strReturn, ":award:", "<img src='../Support/Smiles/emo-award.gif'>");
    strReturn = Regex.Replace(strReturn, ":laughat:", "<img src='../Support/Smiles/emo-laughat.gif'>");
    strReturn = Regex.Replace(strReturn, ":rolleyes:", "<img src='../Support/Smiles/rolleyes.gif'>");
    strReturn = Regex.Replace(strReturn, ":thumbdown:", "<img src='../Support/Smiles/emo-thumbdown.gif'>");
    strReturn = Regex.Replace(strReturn, ":confused:", "<img src='../Support/Smiles/emo-confused.gif'>");
    strReturn = Regex.Replace(strReturn, ":desserted:", "<img src='../Support/Smiles/emo-desserted.gif'>");
    strReturn = Regex.Replace(strReturn, ":eat:", "<img src='../Support/Smiles/emo-eat.gif'>");
    strReturn = Regex.Replace(strReturn, ":evil:", "<img src='../Support/Smiles/emo-evil.gif'>");
    strReturn = Regex.Replace(strReturn, ":funnyface:", "<img src='../Support/Smiles/emo-funnyface.gif'>");
    strReturn = Regex.Replace(strReturn, ":giveup:", "<img src='../Support/Smiles/emo-giveup.gif'>");
    strReturn = Regex.Replace(strReturn, ":groinkick:", "<img src='../Support/Smiles/emo-groinkick.gif'>");
    strReturn = Regex.Replace(strReturn, ":nod:", "<img src='../Support/Smiles/emo-nod.gif'>");
    strReturn = Regex.Replace(strReturn, ":huh:", "<img src='../Support/Smiles/emo-huh.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-axe:", "<img src='../Support/Smiles/544.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-weight:", "<img src='../Support/Smiles/1617.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-bow:", "<img src='../Support/Smiles/bow.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-celebrate:", "<img src='../Support/Smiles/emo-celebrate.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-dj:", "<img src='../Support/Smiles/emo-dj.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-guitar:", "<img src='../Support/Smiles/emo-guitar.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-lsaber:", "<img src='../Support/Smiles/emo-lsaber.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-offerrose:", "<img src='../Support/Smiles/emo-offerrose.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-pirate:", "<img src='../Support/Smiles/emo-pirate.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-solodance:", "<img src='../Support/Smiles/emo-solodance.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-toothbrush:", "<img src='../Support/Smiles/emo-toothbrush.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-eyebag:", "<img src='../Support/Smiles/eyebag.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-gba:", "<img src='../Support/Smiles/gba.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-hate:", "<img src='../Support/Smiles/hate2.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-lecture:", "<img src='../Support/Smiles/lecture.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-nyanya:", "<img src='../Support/Smiles/nyanya.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-rofl:", "<img src='../Support/Smiles/rofl.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-yay:", "<img src='../Support/Smiles/yay.gif'>");

    strReturn = Regex.Replace(strReturn, ":emo-bike:", "<img src='../Support/Smiles/emo-bike.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-bombing:", "<img src='../Support/Smiles/emo-bombing.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-cryeyes:", "<img src='../Support/Smiles/emo-cryeyes.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-disappear:", "<img src='../Support/Smiles/emo-disappear.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-fish:", "<img src='../Support/Smiles/emo-fish.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-harp:", "<img src='../Support/Smiles/emo-harp.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-invincible:", "<img src='../Support/Smiles/emo-invincible.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-beg:", "<img src='../Support/Smiles/emo-beg.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-headcut:", "<img src='../Support/Smiles/emo-headcut.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-boxing:", "<img src='../Support/Smiles/emo-boxing.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-redmad:", "<img src='../Support/Smiles/emo-redmad.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-rainmad:", "<img src='../Support/Smiles/emo-rainmad.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-shocked:", "<img src='../Support/Smiles/emo-shocked.gif'>");

    return strReturn;
   }

   public static string FormatContentsMaster(string pContent)
   {
    string strReturn = pContent;

    Regex regPao;
    regPao = new Regex(@"\[name=([^\]]+)\]([^\]]+)\[\/name\]");
    strReturn = regPao.Replace(strReturn, "<a name=\"$1\">$2</a>");
    regPao = new Regex(@"\[url\]([^\]]+)\[\/url\]");
    strReturn = regPao.Replace(strReturn, "<a href=\"$1\">$1</a>");
    regPao = new Regex(@"\[url=([^\]]+)\]([^\]]+)\[\/url\]");
    strReturn = regPao.Replace(strReturn, "<a href=\"$2\">$1</a>");
    regPao = new Regex(@"\[img\]([^\]]+)\[\/img\]");
    strReturn = regPao.Replace(strReturn, "<img src=\"$1\" />");
    regPao = new Regex(@"\[b\](.+?)\[\/b\]");
    strReturn = regPao.Replace(strReturn, "<b>$1</b>");
    regPao = new Regex(@"\[i\](.+?)\[\/i\]");
    strReturn = regPao.Replace(strReturn, "<i>$1</i>");
    regPao = new Regex(@"\[u\](.+?)\[\/u\]");
    strReturn = regPao.Replace(strReturn, "<u>$1</u>");
    regPao = new Regex(@"\[size=([^\]]+)\]([^\]]+)\[\/size\]");
    strReturn = regPao.Replace(strReturn, "<span style=\"font-size: $1px\">$2</span>");
    regPao = new Regex(@"\[color=([^\]]+)\]([^\]]+)\[\/color\]");
    strReturn = regPao.Replace(strReturn, "<span style=\"color: $1\">$2</span>");
    regPao = new Regex(@"\[color=([^\]]+)\]([^\]]+)\[\/color\]");
    strReturn = regPao.Replace(strReturn, "<div style='border-right: green 1px solid; border-top: green 1px solid; border-left: green 1px solid; border-bottom: green 1px solid; background-color: #F0FFF0; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; font-size: small; border-color: #009933;'>Quote by <a href='http://hq.sti.edu/Userpage/UserPage.aspx?username=$1'>$1</a><br><br>$2</div>");
    regPao = new Regex(@"\[quote=([^\]]+)\]([^\]]+)\[\/quote\]");
    strReturn = regPao.Replace(strReturn, "<div style='border-right: green 1px solid; border-top: green 1px solid; border-left: green 1px solid; border-bottom: green 1px solid; background-color: #F0FFF0; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; font-size: small; border-color: #009933;'>Quote by <a href='http://hq.sti.edu/Userpage/UserPage.aspx?username=$1'>$1</a><br><br>$2</div>");
    regPao = new Regex(@"\[quote=([^\]]+)\]([^\]]+)\[\/quote\]");
    strReturn = regPao.Replace(strReturn, "<div style='border-right: green 1px solid; border-top: green 1px solid; border-left: green 1px solid; border-bottom: green 1px solid; background-color: #F0FFF0; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; font-size: small; border-color: #009933;'>Quote by <a href='http://hq.sti.edu/Userpage/UserPage.aspx?username=$1'>$1</a><br><br>$2</div>");
    regPao = new Regex(@"\[quote=([^\]]+)\]([^\]]+)\[\/quote\]");
    strReturn = regPao.Replace(strReturn, "<div style='border-right: green 1px solid; border-top: green 1px solid; border-left: green 1px solid; border-bottom: green 1px solid; background-color: #F0FFF0; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px; font-size: small; border-color: #009933;'>Quote by <a href='http://hq.sti.edu/Userpage/UserPage.aspx?username=$1'>$1</a><br><br>$2</div>");

    // Smileys
    strReturn = Regex.Replace(strReturn, "\n", "<br>");
    strReturn = Regex.Replace(strReturn, ":sad:", "<img src='http://hq.sti.edu/Support/Smiles/sad.gif'>");
    strReturn = Regex.Replace(strReturn, ":biggrin:", "<img src='http://hq.sti.edu/Support/Smiles/biggrin.gif'>");
    strReturn = Regex.Replace(strReturn, ":laugh:", "<img src='http://hq.sti.edu/Support/Smiles/laugh.gif'>");
    strReturn = Regex.Replace(strReturn, ":cool:", "<img src='http://hq.sti.edu/Support/Smiles/cool.gif'>");
    strReturn = Regex.Replace(strReturn, ":angry:", "<img src='http://hq.sti.edu/Support/Smiles/mad.gif'>");
    strReturn = Regex.Replace(strReturn, ":rock:", "<img src='http://hq.sti.edu/Support/Smiles/rock.gif'>");
    strReturn = Regex.Replace(strReturn, ":tounge:", "<img src='http://hq.sti.edu/Support/Smiles/tounge.gif'>");
    strReturn = Regex.Replace(strReturn, ":wink:", "<img src='http://hq.sti.edu/Support/Smiles/wink.gif'>");
    strReturn = Regex.Replace(strReturn, ":smile:", "<img src='http://hq.sti.edu/Support/Smiles/smile.gif'>");
    strReturn = Regex.Replace(strReturn, ":wow:", "<img src='http://hq.sti.edu/Support/Smiles/wow.gif'>");
    strReturn = Regex.Replace(strReturn, ":no:", "<img src='http://hq.sti.edu/Support/Smiles/no.gif'>");
    strReturn = Regex.Replace(strReturn, ":batwood:", "<img src='http://hq.sti.edu/Support/Smiles/batwood.gif'>");
    strReturn = Regex.Replace(strReturn, ":drunk:", "<img src='http://hq.sti.edu/Support/Smiles/drunk.gif'>");
    strReturn = Regex.Replace(strReturn, ":friendhug:", "<img src='http://hq.sti.edu/Support/Smiles/friendhug.gif'>");
    strReturn = Regex.Replace(strReturn, ":grouphug:", "<img src='http://hq.sti.edu/Support/Smiles/grouphug.gif'>");
    strReturn = Regex.Replace(strReturn, ":wave:", "<img src='http://hq.sti.edu/Support/Smiles/wave.gif'>");
    strReturn = Regex.Replace(strReturn, ":sos:", "<img src='http://hq.sti.edu/Support/Smiles/sos.gif'>");
    strReturn = Regex.Replace(strReturn, ":party:", "<img src='http://hq.sti.edu/Support/Smiles/party.gif'>");
    strReturn = Regex.Replace(strReturn, ":sleep:", "<img src='http://hq.sti.edu/Support/Smiles/sleep.gif'>");
    strReturn = Regex.Replace(strReturn, ":hooray:", "<img src='http://hq.sti.edu/Support/Smiles/hooray.gif'>");
    strReturn = Regex.Replace(strReturn, ":cry:", "<img src='http://hq.sti.edu/Support/Smiles/cry.gif'>");
    strReturn = Regex.Replace(strReturn, ":roll:", "<img src='http://hq.sti.edu/Support/Smiles/roll.gif'>");
    strReturn = Regex.Replace(strReturn, ":thumb:", "<img src='http://hq.sti.edu/Support/Smiles/thumb.gif'>");
    strReturn = Regex.Replace(strReturn, ":giggle:", "<img src='http://hq.sti.edu/Support/Smiles/giggle.gif'>");
    strReturn = Regex.Replace(strReturn, ":whistle:", "<img src='http://hq.sti.edu/Support/Smiles/whistle.gif'>");
    strReturn = Regex.Replace(strReturn, ":ohmy:", "<img src='http://hq.sti.edu/Support/Smiles/ohmy.gif'>");
    strReturn = Regex.Replace(strReturn, ":comfort:", "<img src='http://hq.sti.edu/Support/Smiles/emo-comfort.gif'>");
    strReturn = Regex.Replace(strReturn, ":firegun:", "<img src='http://hq.sti.edu/Support/Smiles/emo-firegun.gif'>");
    strReturn = Regex.Replace(strReturn, ":gun:", "<img src='http://hq.sti.edu/Support/emo-gun.gif'>");
    strReturn = Regex.Replace(strReturn, ":gossip:", "<img src='http://hq.sti.edu/Support/Smiles/emo-gossip.gif'>");
    strReturn = Regex.Replace(strReturn, ":award:", "<img src='http://hq.sti.edu/Support/Smiles/emo-award.gif'>");
    strReturn = Regex.Replace(strReturn, ":laughat:", "<img src='http://hq.sti.edu/Support/Smiles/emo-laughat.gif'>");
    strReturn = Regex.Replace(strReturn, ":rolleyes:", "<img src='http://hq.sti.edu/Support/Smiles/rolleyes.gif'>");
    strReturn = Regex.Replace(strReturn, ":thumbdown:", "<img src='http://hq.sti.edu/Support/Smiles/emo-thumbdown.gif'>");
    strReturn = Regex.Replace(strReturn, ":confused:", "<img src='http://hq.sti.edu/Support/Smiles/emo-confused.gif'>");
    strReturn = Regex.Replace(strReturn, ":desserted:", "<img src='http://hq.sti.edu/Support/Smiles/emo-desserted.gif'>");
    strReturn = Regex.Replace(strReturn, ":eat:", "<img src='http://hq.sti.edu/Support/Smiles/emo-eat.gif'>");
    strReturn = Regex.Replace(strReturn, ":evil:", "<img src='http://hq.sti.edu/Support/Smiles/emo-evil.gif'>");
    strReturn = Regex.Replace(strReturn, ":funnyface:", "<img src='http://hq.sti.edu/Support/Smiles/emo-funnyface.gif'>");
    strReturn = Regex.Replace(strReturn, ":giveup:", "<img src='http://hq.sti.edu/Support/Smiles/emo-giveup.gif'>");
    strReturn = Regex.Replace(strReturn, ":groinkick:", "<img src='http://hq.sti.edu/Support/Smiles/emo-groinkick.gif'>");
    strReturn = Regex.Replace(strReturn, ":nod:", "<img src='http://hq.sti.edu/Support/Smiles/emo-nod.gif'>");
    strReturn = Regex.Replace(strReturn, ":huh:", "<img src='http://hq.sti.edu/Support/Smiles/emo-huh.gif'>");

    strReturn = Regex.Replace(strReturn, ":emo-axe:", "<img src='http://hq.sti.edu/Support/Smiles/544.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-weight:", "<img src='http://hq.sti.edu/Support/Smiles/1617.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-bow:", "<img src='http://hq.sti.edu/Support/Smiles/bow.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-celebrate:", "<img src='http://hq.sti.edu/Support/Smiles/emo-celebrate.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-dj:", "<img src='http://hq.sti.edu/Support/Smiles/emo-dj.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-guitar:", "<img src='http://hq.sti.edu/Support/Smiles/emo-guitar.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-lsaber:", "<img src='http://hq.sti.edu/Support/Smiles/emo-lsaber.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-offerrose:", "<img src='http://hq.sti.edu/Support/Smiles/emo-offerrose.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-pirate:", "<img src='http://hq.sti.edu/Support/Smiles/emo-pirate.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-solodance:", "<img src='http://hq.sti.edu/Support/Smiles/emo-solodance.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-toothbrush:", "<img src='http://hq.sti.edu/Support/Smiles/emo-toothbrush.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-eyebag:", "<img src='http://hq.sti.edu/Support/Smiles/eyebag.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-gba:", "<img src='http://hq.sti.edu/Support/Smiles/gba.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-hate:", "<img src='http://hq.sti.edu/Support/Smiles/hate2.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-lecture:", "<img src='http://hq.sti.edu/Support/Smiles/lecture.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-nyanya:", "<img src='http://hq.sti.edu/Support/Smiles/nyanya.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-rofl:", "<img src='http://hq.sti.edu/Support/Smiles/rofl.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-yay:", "<img src='http://hq.sti.edu/Support/Smiles/yay.gif'>");

    strReturn = Regex.Replace(strReturn, ":emo-bike:", "<img src='http://hq.sti.edu/Support/Smiles/emo-bike.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-bombing:", "<img src='http://hq.sti.edu/Support/Smiles/emo-bombing.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-cryeyes:", "<img src='http://hq.sti.edu/Support/Smiles/emo-cryeyes.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-disappear:", "<img src='http://hq.sti.edu/Support/Smiles/emo-disappear.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-fish:", "<img src='http://hq.sti.edu/Support/Smiles/emo-fish.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-harp:", "<img src='http://hq.sti.edu/Support/Smiles/emo-harp.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-invincible:", "<img src='http://hq.sti.edu/Support/Smiles/emo-invincible.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-beg:", "<img src='http://hq.sti.edu/Support/Smiles/emo-beg.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-headcut:", "<img src='http://hq.sti.edu/Support/Smiles/emo-headcut.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-boxing:", "<img src='http://hq.sti.edu/Support/Smiles/emo-boxing.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-redmad:", "<img src='http://hq.sti.edu/Support/Smiles/emo-redmad.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-rainmad:", "<img src='http://hq.sti.edu/Support/Smiles/emo-rainmad.gif'>");
    strReturn = Regex.Replace(strReturn, ":emo-shocked:", "<img src='http://hq.sti.edu/Support/Smiles/emo-shocked.gif'>");
    return strReturn;
   }
  }
 }
}