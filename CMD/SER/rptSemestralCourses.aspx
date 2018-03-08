<%@ Page Language="C#" MasterPageFile="~/App_Master/TwoPanel.master" AutoEventWireup="true" CodeFile="rptSemestralCourses.aspx.cs" Inherits="CMD_SER_rptSemestralCourses" %>

<asp:Content ID="con" ContentPlaceHolderID="cphBody" Runat="Server" Visible="true">
 <table width="100%" cellpadding="0" cellspacing="0">

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">
     <a href="../../../Default.aspx" class="SiteMap">Home</a> » 
     <a href="#" class="SiteMap">CMD</a> » 
     <a href="SERMain.aspx" class="SiteMap">SER</a> » 
     <a href="rptSemestralCourses.aspx" class="SiteMap">Semestral Courses Report</a>
    </div>        
   </td>
  </tr>
      
  <tr><td style="height:9px;"></td></tr>

  <tr>
   <td>
    <div class="border" style="padding-top:10px;padding-left:10px;padding-right:10px;padding-bottom:10px;">    
     <b><span class="HeaderText">Semestral Courses Report</span></b>
     <br />
     <br />
     
     <script type="text/javascript" src="../../Visifire2.js"></script>
     <div id="VisifireChart">
     <script language="javascript" type="text/javascript">
      var chartXmlString = ''
                           + '<vc:Chart xmlns:vc="clr-namespace:Visifire.Charts;assembly=SLVisifire.Charts" Width="500" Height="400" BorderThickness="0" Theme="Theme1" ColorSet="Visifire1" >'
                           + '<vc:Chart.Titles><vc:Title Text="OGS per Region" /></vc:Chart.Titles>'
                           + '<vc:Chart.AxesX>'
                           + '<vc:Axis Title="Region" TitleFontSize="20" ><vc:Axis.AxisLabels><vc:AxisLabels Enabled="True" /></vc:Axis.AxisLabels></vc:Axis>'
                           + '</vc:Chart.AxesX>'
                           + '<vc:Chart.AxesY>'
                           + '<vc:Axis Title="OGS" TitleFontSize="20" AxisType="Primary" >'
                           + '<vc:Axis.AxisLabels>'
                           + '<vc:AxisLabels Enabled="True" />'
                           + '</vc:Axis.AxisLabels>'
                           + '</vc:Axis>'
                           + '</vc:Chart.AxesY>'
                           + '<vc:Chart.Series>'
                           + '<vc:DataSeries RenderAs="Pie" AxisYType="Primary" >'
                           + '<vc:DataSeries.DataPoints>'
                           + '<vc:DataPoint AxisXLabel="Metro Manila" YValue="19825" />'
                           + '<vc:DataPoint AxisXLabel="Northern Luzon" YValue="10284" />'
                           + '<vc:DataPoint AxisXLabel="Southern Luzon" YValue="13693" />'
                           + '<vc:DataPoint AxisXLabel="Visayas" YValue="5847" />'
                           + '<vc:DataPoint AxisXLabel="Mindanao" YValue="10428" />'
                           + '</vc:DataSeries.DataPoints>'
                           + '</vc:DataSeries>'
                           + '</vc:Chart.Series>'
                           +'</vc:Chart>';
      var vChart = new Visifire2("../../SL.Visifire.Charts.xap ", 500, 400);
        vChart.setDataXml(chartXmlString);
        vChart.render("VisifireChart");
    </script>
    </div>

<br /><br />     
     <div class="GridBorder">
      <table cellpadding="5" cellspacing="1" width="2500px;">
       <tr>
        <td class="GridText" rowspan="3" style="width:10%;">&nbsp;</td>
        <td class="GridText" rowspan="3" style="font-size:x-small; text-align:center; width:5%;"><b>NS</b></td>
        <td class="GridText" rowspan="3" style="font-size:x-small; text-align:center; width:5%;"><b>OS</b></td>
        <td class="GridText" rowspan="3" style="font-size:x-small; text-align:center; width:5%;"><b>Total OGS</b></td>
        <td class="GridColumns" colspan="26"><b>CHED Programs</b></td>
        <td class="GridColumns" colspan="38"><b>TESDA Programs</b></td>
       </tr>
       <tr>
        <td class="GridColumns" colspan="2"><b>BSCOE</b></td>
        <td class="GridColumns" colspan="2"><b>BSECE</b></td>
        <td class="GridColumns" colspan="2"><b>BSIT</b></td>
        <td class="GridColumns" colspan="2"><b>BSCS</b></td>
        <td class="GridColumns" colspan="2"><b>ACT</b></td>
        <td class="GridColumns" colspan="2"><b>BSHRM</b></td>
        <td class="GridColumns" colspan="2"><b>BSBA</b></td>
        <td class="GridColumns" colspan="2"><b>BSENTREP</b></td>
        <td class="GridColumns" colspan="2"><b>BSOA</b></td>
        <td class="GridColumns" colspan="2"><b>AOM</b></td>
        <td class="GridColumns" colspan="2"><b>BSED</b></td>
        <td class="GridColumns" colspan="2"><b>BSN</b></td>
        <td class="GridColumns" colspan="2"><b>MIT</b></td>
        <td class="GridColumns" colspan="2"><b>DCET</b></td>        
        <td class="GridColumns" colspan="2"><b>DIT</b></td>
        <td class="GridColumns" colspan="2"><b>DMA</b></td>
        <td class="GridColumns" colspan="2"><b>HRA</b></td>
        <td class="GridColumns" colspan="2"><b>HRS</b></td>
        <td class="GridColumns" colspan="2"><b>DENTREP</b></td>
        <td class="GridColumns" colspan="2"><b>DOSM</b></td>
        <td class="GridColumns" colspan="2"><b>PNP</b></td>
        <td class="GridColumns" colspan="2"><b>CNAP</b></td>
        <td class="GridColumns" colspan="2"><b>DPN</b></td>
        <td class="GridColumns" colspan="2"><b>IHC</b></td>
        <td class="GridColumns" colspan="2"><b>DHCS</b></td>
        <td class="GridColumns" colspan="2"><b>DHNA</b></td>
        <td class="GridColumns" colspan="2"><b>DAIT</b></td>
        <td class="GridColumns" colspan="2"><b>DCBB</b></td>        
        <td class="GridColumns" colspan="2"><b>CYPROG</b></td>
        <td class="GridColumns" colspan="2"><b>DEP</b></td>
        <td class="GridColumns" colspan="2"><b>HCS</b></td>
        <td class="GridColumns" colspan="2"><b>BASS</b></td>        
       </tr>       
       <tr>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>        
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td> 
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>        
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>        
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>        
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>               
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>    
        <td class="GridColumns" style="width:1%;"><b>S1</b></td>
        <td class="GridColumns" style="width:1%;"><b>S2</b></td>            
       </tr>
       <% LoadRecords(); %>
      </table>
     </div>     
    </div>     
    </td>
   </tr>
     
 </table>  
</asp:Content>