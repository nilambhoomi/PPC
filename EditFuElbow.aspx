﻿<%@ Page Title="" Language="C#" MasterPageFile="~/FollowUpMaster.master" AutoEventWireup="true" CodeFile="EditFuElbow.aspx.cs" Inherits="EditFuElbow" %>
<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        /*.table{
    display:table;
    width:100%;
    table-layout:fixed;
}*/
        .table_cell {
            /*display:table-cell;*/
            width: 100px;
            /*border:solid black 1px;*/
        }
    </style>
    <script type="text/javascript">
        function Confirmbox(e, page) {
            e.preventDefault();
            var answer = confirm('Do you want to save the data?');
            if (answer) {
                //var currentURL = window.location.href;
                document.getElementById('<%=pageHDN.ClientID%>').value = $('#ctl00_' + page).attr('href');
                document.getElementById('<%= btnSave.ClientID %>').click();
            }
            else {
                window.location.href = $('#ctl00_' + page).attr('href');
            }
        }
        function saveall() {
            document.getElementById('<%= btnSave.ClientID %>').click();
        }
    </script>
    <asp:HiddenField ID="pageHDN" runat="server" />
    <div id="mymodelmessage" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Message</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="upMessage" UpdateMode="Conditional">
                        <ContentTemplate>
                            <label runat="server" id="lblMessage"></label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
        <!-- start: Header -->
        <%--    <asp:UpdatePanel runat="server" ID="upMain">
            <ContentTemplate>--%>
    <div class="container">
        <div class="row">
            <div class="col-lg-10" id="content">
                    <%--    <ul class="breadcrumb">
                                <li>
                                    <i class="icon-home"></i>
                                    <a href="Page1.aspx"><span class="label">Page1</span></a>
                                </li>
                                <li id="lipage2">
                                    <i class="icon-edit"></i>
                                    <a href="Page2.aspx"><span class="label label-success">Page2</span></a>
                                </li>
                                <li id="li1" runat="server" enable="false">
                                    <i class="icon-edit"></i>
                                    <a href="Page3.aspx"><span class="label">Page3</span></a>
                                </li>
                                <li id="li2" runat="server" enable="false">
                                    <i class="icon-edit"></i>
                                    <a href="Page4.aspx"><span class="label">Page4</span></a>
                                </li>
                            </ul>--%>
                <div class="row">
                    <div class="col-md-3">
                         <label class="control-label"><b><u>CHIEF COMPLAINT</u></b></label>
                    </div>
                    <div class="col-md-9" style="margin-top: 5px">
                                <div id="WrapLeft" runat="server">
                                    <label  class=control-label> The patient complains of left elbow pain that is&nbsp;</label>
                                    <asp:TextBox  ID="txtPainScaleLeft" Width="40px" runat="server"></asp:TextBox>
                                    <label class=control-label>/10 and </label>
                                    <asp:CheckBox  ID="chkContentLeft" runat="server" Text="constant" />
                                    <asp:CheckBox  ID="chkIntermittentLeft" runat="server" Text="intermittent." />
                                    <asp:CheckBox  ID="chksharpLeft" runat="server" Text="sharp, " />
                                    <asp:CheckBox  ID="chkelectricLeft" runat="server" Text="electric, " />
                                    <asp:CheckBox  ID="chkshootingLeft" runat="server" Text="shooting, " />
                                    <asp:CheckBox  ID="chkthrobbingLeft" runat="server" Text="throbbing, " />
                                    <asp:CheckBox  ID="chkpulsatingLeft" runat="server" Text="pulsating, " />   
                                    <asp:CheckBox  ID="chkdullLeft" runat="server" Text="dull, " />   
                                    <asp:CheckBox  ID="chkachyinnatureLeft" runat="server" Text="achy in nature." />
                                    <label class=control-label>The patient complains of pain specifically at the &nbsp; </Label>
                                    <asp:CheckBox  ID="chkMedEpicondyleLeft" runat="server" Text="medial epicondyle,  " />
                                    <asp:CheckBox  ID="chkLatEpicondyleLeft" runat="server" Text="lateral epicondyle,  " />
                                    <asp:CheckBox  ID="chkOlecranonLeft"  runat="server" Text="olecranon aspect of the elbow. " />
                                    <label class=control-label>The elbow pain is worsened with </Label>
                                    <asp:CheckBox  ID="chkmovementLeft"  runat="server" Text="movement,  " />
                                    <asp:CheckBox  ID="chkraisingthearmLeft" runat="server" Text="raising the arm,  " />
                                    <asp:CheckBox  ID="chkliftingobjectsLeft"  runat="server" Text="lifting objects, " />
                                    <asp:CheckBox  ID="chkrotationLeft"  runat="server" Text="rotation, " />
                                    <asp:CheckBox  ID="chkworkingLeft"  runat="server" Text="working, " />
                                    <asp:TextBox   ID="txtNoteLeft"  runat="server" ></asp:TextBox>
                                </div><br />
                                <div id="wrpRight" runat="server">
                                    <label class=control-label>The patient complains of right  elbow pain that is&nbsp;</Label>
                                    <asp:TextBox   ID="txtPainScaleRight" Width="40px" runat="server"></asp:TextBox>
                                    <label class=control-label>/10 and </Label>
                                    <asp:CheckBox  ID="chkContentRight"  runat="server" Text="constant" />
                                    <asp:CheckBox  ID="chkIntermittentRight" runat="server" Text="intermittent." />
                                    <asp:CheckBox  ID="chksharpRight" runat="server" Text="sharp, " />
                                    <asp:CheckBox  ID="chkelectricRight" runat="server" Text="electric, " />
                                    <asp:CheckBox  ID="chkshootingRight" runat="server" Text="shooting, " />
                                    <asp:CheckBox  ID="chkthrobbingRight" runat="server" Text="throbbing, " />
                                    <asp:CheckBox  ID="chkpulsatingRight" runat="server" Text="pulsating, " />  
                                    <asp:CheckBox  ID="chkdullRight" runat="server" Text="dull, " />   
                                    <asp:CheckBox  ID="chkachyinnatureRight" runat="server" Text="achy in nature." /> 
                                    <label class=control-label>The patient complains of pain specifically at the  &nbsp;</Label>
                                    <asp:CheckBox  ID="chkMedEpicondyleRight" runat="server" Text="medial epicondyle,  " />
                                    <asp:CheckBox  ID="chkLatEpicondyRight" runat="server" Text="lateral epicondyle,  " />
                                    <asp:CheckBox  ID="chkOlecranonRight"  runat="server" Text="olecranon aspect of the elbow. " />
                                    <label class=control-label>The elbow pain is worsened with </Label>
                                    <asp:CheckBox  ID="chkmovementRight" runat="server" Text="movement,  " />
                                    <asp:CheckBox  ID="chkraisingthearmRight" runat="server" Text="raising the arm,  " />
                                    <asp:CheckBox  ID="chkliftingobjectsRight" runat="server" Text="lifting objects, " />
                                    <asp:CheckBox  ID="chkrotationRight" runat="server" Text="rotation, " />
                                    <asp:CheckBox  ID="chkworkingRight" runat="server" Text="working, " />
                                    <asp:TextBox   ID="txtNoteRight" runat="server" ></asp:TextBox>
                                </div>
                        </div>
                    </div>
                
                <div class="row">
                    <div class="col-md-3">
                        <label class="control-label">Notes</label>
                    </div>
                    <div class="col-md-9" style="margin-top: 5px">
                                <asp:TextBox runat="server" ID="txtFreeFormCC"  TextMode="MultiLine" Width="700px" Height="100px"></asp:TextBox>
                         <button type="button" id="start_button1" onclick="startButton1(event)">
                                <img src="images/mic.gif" alt="start" /></button>
                            <div style="display: none"><span class="final" id="final_span1"></span><span class="interim" id="interim_span1"></span></div>
                            </div>
                        </div>
                 <div class="row">
                    <div class="col-md-3">
                         <label class="control-label"><b><u>PHYSICAL EXAM:</u></b></label>
                    </div>
                    <div class="col-md-9" style="margin-top: 5px">
                                <table style="width: 500px;display:none" >
                                    <tr>
                                        <td  colspan="2">Upper Extremities</td>
                                        <td colspan="3">ROM</td>
                                    </tr>
                                    <tr>
                                        <td  colspan="2"></td>
                                        <td >Left</td>
                                        <td >Right</td>
                                        <td >Normal</td>
                                    </tr>
                                    <tr>
                                        <td >Elbow </td>
                                        <td >Extension</td>
                                        <td><asp:TextBox ID="txtExtension1" runat="server" style="width:50px;"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtExtension2" runat="server" style="width:50px;"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtExtension3" ReadOnly="true" runat="server" style="width:50px;" Text="0-150"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td >Flexion</td>
                                        <td><asp:TextBox ID="txtFlex1" runat="server" style="width:50px;"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtFlex2" runat="server" style="width:50px;"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtFlex3" runat="server" ReadOnly="true"  style="width:50px;" Text="0-150"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td >Supination</td>
                                        <td><asp:TextBox ID="txtSupination1" runat="server" style="width:50px;"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtSupination2" runat="server" style="width:50px;"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtSupination3" runat="server" ReadOnly="true" style="width:50px;" Text="0-90"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td >Pronation</td>
                                        <td><asp:TextBox ID="txtPronation1" runat="server" style="width:50px;"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtPronation2" runat="server" style="width:50px;"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtPronation3" runat="server" ReadOnly="true" style="width:50px;" Text="0-90"></asp:TextBox></td>
                                    </tr>
                                </table>
                              
                            </div>
                        </div>
                <div class="row">
                    <div class="col-md-3">
                        <label class="control-label"></label>
                    </div>
                    <div class="col-md-9" style="margin-top: 5px">

                                <div id="wrpLeft2" runat="server">
                                    
                                    <asp:Label   Font-Bold="false"  runat="server"  Text="There is tenderness upon palpation of the Left elbow. Range of motion is &nbsp;"></asp:Label>
                                    <asp:DropDownList ID="cboRangeOfMotionLeft" DataSourceID="RangeOfMotionLeft" DataTextField="name"   runat="server"></asp:DropDownList>
                                    <asp:XmlDataSource ID="RangeOfMotionLeft" runat="server" DataFile="~/xml/HSMData.xml" XPath="HSM/ROMs/ROM" />
                                    <asp:Label   Font-Bold="false"  runat="server"   Text="&nbsp; decreased. Tinel is &nbsp;"></asp:Label>
                                    <asp:DropDownList ID="cboTinelLeft" DataSourceID="TinelLeft" DataTextField="name"   runat="server"></asp:DropDownList>
                                    <asp:XmlDataSource ID="TinelLeft" runat="server" DataFile="~/xml/HSMData.xml" XPath="HSM/Results/Result" />
                                </div><br />
                                <div id="wrpRight2" runat="server">
                                    <asp:Label   Font-Bold="false" runat="server"  Text="There is tenderness upon palpation of the Right elbow. Range of motion is &nbsp;"></asp:Label>
                                    <asp:DropDownList ID="cboRangeOfMotionRight" DataSourceID="RangeOfMotionRight" DataTextField="name"  runat="server"></asp:DropDownList>
                                    <asp:XmlDataSource ID="RangeOfMotionRight" runat="server" DataFile="~/xml/HSMData.xml" XPath="HSM/ROMs/ROM" />
                                    <asp:Label runat="server"   Font-Bold="false"   Text="&nbsp; decreased. Tinel is &nbsp;"></asp:Label>
                                    <asp:DropDownList ID="cboTinelRight" DataSourceID="TinelRight" DataTextField="name"  runat="server"></asp:DropDownList>
                                    <asp:XmlDataSource ID="TinelRight" runat="server" DataFile="~/xml/HSMData.xml" XPath="HSM/Results/Result" />
                                </div>
                            </div>
                        </div>
                <div class="row">
                    <div class="col-md-3">
                        <label class="control-label">Notes</label>
                    </div>
                    <div class="col-md-9" style="margin-top: 5px">

                                <asp:TextBox runat="server"  ID="txtFreeForm" TextMode="MultiLine" Width="700px" Height="100px"></asp:TextBox>
                        <button type="button" id="start_button" onclick="startButton(event)">
                            <img src="images/mic.gif" alt="start" /></button>
                        <div style="display: none"><span class="final" id="final_span"></span><span class="interim" id="interim_span"></span></div>
                            </div>
                        </div>
                <%--<tr>
                 <th style="width: 10%;"><label class="control-label">ASSESSMENT/DIAGNOSIS</label></th>
                 <th style="width: 90%; margin-left: 40px;">--%>
                 <div class="row">
                    <div class="col-md-3">
                        <label class="control-label"><b><u>ASSESSMENT/DIAGNOSIS:</u></b></label>
                    </div>
                    <div class="col-md-9" style="margin-top: 5px">

                                <%--<asp:CheckBox ID="chkSprainStrain" Style=";" runat="server" Text="Cervical muscle sprain/strain." Checked="true" /><br />
                                <asp:CheckBox ID="chkHerniation" Style="; margin-left: -18.5%" runat="server" Text="Possible cervical disc herniation." Checked="true" /><br />--%>
                                <%-- <asp:CheckBox ID="chkSyndrome" runat="server"  Text="Possible cervical radiculopathy vs. plexopathy vs. entrapment syndrome." Checked="true" />
                                --%>
                            </div>
                        </div>

                 <div class="row">
                    <div class="col-md-3">
                        <label class="control-label">Notes:</label>
                    </div>
                    <div class="col-md-9" style="margin-top: 5px">

                                <asp:TextBox runat="server" Style=";" ID="txtFreeFormA" TextMode="MultiLine" Width="700px" Height="100px"></asp:TextBox>
                                <asp:ImageButton ID="AddDiag" Style="text-align: left;" ImageUrl="~/img/a1.png" Height="50px" Width="50px" runat="server" OnClientClick="basicPopup();" OnClick="AddDiag_Click" />
                            </div>
                        </div>

                 <div class="row">
                    <div class="col-md-3">
                        <label class="control-label"></label>
                    </div>
                    <div class="col-md-9" style="margin-top: 5px">

                                <asp:GridView ID="dgvDiagCodes" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="DiagCode" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtcc" ReadOnly="true" runat="server" Text='<%# Eval("DiagCode") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="450">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtpe" ReadOnly="true" runat="server" Width="400" Text='<%# Eval("Description") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                 <div class="row">
                    <div class="col-md-3">
                        <label class="control-label"><b><u>PLAN:</u></b></label>
                    </div>
                    <div class="col-md-9" style="margin-top: 5px">

                                <%--  <asp:CheckBox ID="chkCervicalSpine" Style=";" Text="MRI" runat="server" />
                                <asp:DropDownList ID="cboScanType" Style="; height: 25px;" runat="server"></asp:DropDownList>
                                <asp:Label ID="Label7" Style=";" Text=" of the cervical spine " runat="server"></asp:Label>
                                <asp:TextBox ID="txtToRuleOut" runat="server" Style="; " Text="to rule out herniated nucleus pulposus/soft tissue injury " Width="299px"></asp:TextBox>--%>
                                <%--OnClick="AddStd_Click"--%>
                            </div>
                        </div>
                 <div class="row">
                    <div class="col-md-3">
                        <label class="control-label">Notes:</label>
                    </div>
                    <div class="col-md-9" style="margin-top: 5px">

                                <asp:TextBox runat="server" ID="txtFreeFormP" Style=";" TextMode="MultiLine" Width="700px" Height="100px"></asp:TextBox>
                                <asp:ImageButton ID="AddStd" Style="display:none" runat="server" Height="50px" Width="50px" ImageUrl="~/img/a1.png" PostBackUrl="~/AddStandards.aspx" OnClientClick="basicPopup();return false;" />
                            </div>
                        </div>

                 <div class="row">
                    <div class="col-md-3">
                        <label class="control-label"></label>
                    </div>
                    <div class="col-md-9" style="margin-top: 5px">
                                <asp:GridView ID="dgvStandards" runat="server" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfFname" runat="server" Value='<%# Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Heading" ItemStyle-Width="450">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtHeading" runat="server" CssClass="form-control" Width="400px" TextMode="MultiLine" Text='<%# Eval("Heading") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PDesc" ItemStyle-Width="600">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPDesc" runat="server" CssClass="form-control" Width="600px" TextMode="MultiLine" Text='<%# Eval("PDesc") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="IsChkd">

                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" value='<%# Convert.ToBoolean(Eval("IsChkd")) %>' AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <%-- <asp:TemplateField HeaderText="MCODE" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:Label ID="mcode" runat="server" Text='<%# Eval("MCODE") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <%-- <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfFname" runat="server" Value='<%# Eval("ProcedureDetail_ID") %>' />
                    </ItemTemplate>
                                      </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderText="BodyPart" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("BodyPart") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                               <%--<asp:TemplateField HeaderText="Heading" ItemStyle-Width="450">
                                    <ItemTemplate>--%>
                                        <%--<asp:Label ID="lblheading" runat="server" Text='<%# Eval("Heading") %>'></asp:Label>--%>
                                       <%-- <asp:TextBox ID="txtHeading" runat="server" CssClass="form-control" Width="400px"  TextMode="MultiLine" Text='<%# Eval("Heading") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                               <%-- <asp:TemplateField HeaderText="CC" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtcc" Width="48" ReadOnly="true" runat="server" Text='<%# Eval("CCDesc") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PE" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtpe" Width="48" ReadOnly="true" runat="server" Text='<%# Eval("PEDesc") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AD" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtadesc" Width="48" ReadOnly="true" runat="server" Text='<%# Eval("ADesc") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PD" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtpdesc" Width="95" ReadOnly="true" runat="server" Text='<%# Eval("PDesc") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <%-- <asp:TemplateField HeaderText="PN" ItemStyle-Width="20">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox3" Enabled="false" runat="server" value='<%# Eval("PN") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                <%--<asp:TemplateField HeaderText="IsChkd">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox4" Enabled="false" runat="server" value='<%# Eval("PN") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                            </div>
                        </div>
             <div class="row"></div>
                 <div class="row" style="margin-top: 15px">
                    <div class="col-md-3"></div>
                    <div class="col-md-9" style="margin-top: 5px">

                               <%-- <asp:ImageButton ID="LoadDV" Style=";" runat="server" OnClick="LoadDV_Click" ImageUrl="~/img/edit.gif" />--%>
                                <div style="display:none"><asp:Button ID="btnSave"  runat="server" Text="Save" CssClass="btn blue"  OnClick="btnSave_Click" /></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <%--   </ContentTemplate>
        </asp:UpdatePanel>--%>
    <script type="text/javascript">
        function OnSuccess(response) {
            //debugger;
            popupWindow = window.open("AddStandards.aspx", 'popUpWindow', 'height=500,width=1200,left=100,top=30,resizable=No,scrollbars=Yes,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
        function OnSuccess_q(response) {
            popupWindow = window.open("AddDiagnosis.aspx", 'popUpWindow', 'height=500,width=1200,left=100,top=30,resizable=No,scrollbars=Yes,toolbar=no,menubar=no,location=no,directories=no, status=No');

        }
        function basicPopup() {
            document.forms[0].target = "_blank";
        };
    </script>
    <script>
        $(document).ready(function () {
            $('#rbl_in_past input').change(function () {
                if ($(this).val() == '0') {
                    $("#txt_injur_past_bp").prop('disabled', true);
                    $("#txt_injur_past_how").prop('disabled', true);
                }
                else {
                    $("#txt_injur_past_bp").prop('disabled', false);
                    $("#txt_injur_past_how").prop('disabled', false);
                }
            });
        });

        $(document).ready(function () {
            $('#rbl_seen_injury input').change(function () {
                if ($(this).val() == 'False') {
                    $("#txt_docname").prop('disabled', true);
                }
                else {
                    $("#txt_docname").prop('disabled', false);
                }
            });
        });

        $(document).ready(function () {
            $('#rep_wenttohospital input').change(function () {
                if ($(this).val() == '0') {
                    $("#txt_day").prop('disabled', true);
                    $("#txt_day").prop('value', "0");
                }
                else {
                    $("#txt_day").prop('disabled', false);
                    $("#txt_day").select();
                    $("#txt_day").focus();
                }
            });
        });

        $(document).ready(function () {
            $('#rep_hospitalized input').change(function () {
                if ($(this).val() == '0') {
                    $("#txt_hospital").prop('disabled', true);
                    $("#txt_day").prop('disabled', true);
                    $("#chk_mri").prop('disabled', true);
                    $("#txt_mri").prop('disabled', true);
                    $("#chk_CT").prop('disabled', true);
                    $("#txt_CT").prop('disabled', true);
                    $("#chk_xray").prop('disabled', true);
                    $("#txt_x_ray").prop('disabled', true);
                    $("#txt_prescription").prop('disabled', true);
                    $("#txt_which_what").prop('disabled', true);
                }
                else {
                    $("#txt_hospital").prop('disabled', false);
                    $("#ddl_via").prop('disabled', false);
                    $("#txt_day").prop('disabled', false);
                    $("#chk_mri").prop('disabled', false);
                    $("#txt_mri").prop('disabled', false);
                    $("#chk_CT").prop('disabled', false);
                    $("#txt_CT").prop('disabled', false);
                    $("#chk_xray").prop('disabled', false);
                    $("#txt_x_ray").prop('disabled', false);
                    $("#txt_prescription").prop('disabled', false);
                    $("#txt_which_what").prop('disabled', false);
                }
            });
        });
    </script>
     <script>
         var controlname = null;
         var final_transcript = '';
         var recognizing = false;
         var ignore_onend;
         var start_timestamp;

         if (!('webkitSpeechRecognition' in window)) {
             // upgrade();
         } else {
             start_button.style.display = 'inline-block';
             var recognition = new webkitSpeechRecognition();
             recognition.continuous = true;
             recognition.interimResults = true;

             recognition.onstart = function () {
                 recognizing = true;
             };

             recognition.onerror = function (event) {
                 if (event.error == 'no-speech') {
                     ignore_onend = true;
                 }
                 if (event.error == 'audio-capture') {
                     //showInfo('info_no_microphone');
                     ignore_onend = true;
                 }
                 if (event.error == 'not-allowed') {
                     if (event.timeStamp - start_timestamp < 100) {
                         //showInfo('info_blocked');
                     } else {
                         //showInfo('info_denied');
                     }
                     ignore_onend = true;
                 }
             };

             recognition.onend = function () {
                 recognizing = false;
                 if (ignore_onend) {
                     return;
                 }
                 if (!final_transcript) {
                     //showInfo('info_start');
                     return;
                 }
                 if (!final_transcript1) {
                     //showInfo('info_start');
                     return;
                 }

             };

             recognition.onresult = function (event) {
                 var interim_transcript = '';
                 if (typeof (event.results) == 'undefined') {
                     recognition.onend = null;
                     recognition.stop();
                     //upgrade();
                     return;
                 }
                 for (var i = event.resultIndex; i < event.results.length; ++i) {
                     if (event.results[i].isFinal) {
                         final_transcript += event.results[i][0].transcript;
                     } else {
                         interim_transcript += event.results[i][0].transcript;
                     }
                 }
                 final_transcript = capitalize(final_transcript);
                 //finalrecord = linebreak(final_transcript);
                 //$('#ctl00_ContentPlaceHolder1_txtFreeForm').text(linebreak(final_transcript));
                 $(controlname).text(linebreak(final_transcript));
                 interim_span.innerHTML = linebreak(interim_transcript);
             };
         }



         var two_line = /\n\n/g;
         var one_line = /\n/g;
         function linebreak(s) {
             return s.replace(two_line, '<p></p>').replace(one_line, '<br>');
         }

         var first_char = /\S/;
         function capitalize(s) {
             return s.replace(first_char, function (m) { return m.toUpperCase(); });
         }

         function startButton(event) {
             controlname = "#ctl00_ContentPlaceHolder1_txtFreeForm";
             if (recognizing) {
                 recognition.stop();
                 return;
             }
             final_transcript = '';
             recognition.lang = 'en';
             recognition.start();
             ignore_onend = false;
             final_span.innerHTML = '';
             interim_span.innerHTML = '';
             //showInfo('info_allow');
             //showButtons('none');
             start_timestamp = event.timeStamp;
         }

         function startButton1(event) {
             controlname = "#ctl00_ContentPlaceHolder1_txtFreeFormCC";
             if (recognizing) {
                 recognition.stop();
                 return;
             }
             final_transcript = '';
             recognition.lang = 'en';
             recognition.start();
             ignore_onend = false;
             final_span1.innerHTML = '';
             interim_span1.innerHTML = '';
             //showInfo('info_allow');
             //showButtons('none');
             start_timestamp = event.timeStamp;
         }
    </script>
</asp:Content>
