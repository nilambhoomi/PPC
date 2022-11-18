<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="PatientIntakeList.aspx.cs" Inherits="PatientIntakeList" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <%--<script src="Scripts/jquery-1.8.2.min.js"></script>
    <script src="js/images/bootstrap.min.js"></script>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.11.4/jquery-ui.css" rel="stylesheet" />
    <script src="https://cdn.rawgit.com/igorescobar/jQuery-Mask-Plugin/master/src/jquery.mask.js"></script>
    <script src="js/jquery-mask-1.14.8.min.js"></script>
    <script src="js/jquery.maskedinput.js"></script>
    <script src="https://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>--%>
    
    
    <script src="Scripts/jquery-1.8.2.min.js"></script>

    <script type="text/javascript">
        $(function () {

            $('[id*=txtDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                language: "tr"
            });
            $('[id*=txtDOB]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                language: "tr"
            });
        });


    </script>


    <style>
        /*a.btn {
            text-decoration: none;
        }

        table {
            text-align: center;
        }

        .main-container {
            min-height: 900px;
        }*/

        .pager::before {
            display: none;
        }

        .pager table {
            margin: 0 auto;
        }

            .pager table tbody tr td a,
            .pager table tbody tr td span {
                position: relative;
                float: left;
                padding: 6px 12px;
                margin-left: -1px;
                line-height: 1.42857143;
                color: #337ab7;
                text-decoration: none;
                background-color: #fff;
                border: 1px solid #ddd;
            }

            .pager table > tbody > tr > td > span {
                z-index: 3;
                color: #fff;
                cursor: default;
                background-color: #337ab7;
                border-color: #337ab7;
            }

            .pager table > tbody > tr > td:first-child > a,
            .pager table > tbody > tr > td:first-child > span {
                margin-left: 0;
                border-top-left-radius: 4px;
                border-bottom-left-radius: 4px;
            }

            .pager table > tbody > tr > td:last-child > a,
            .pager table > tbody > tr > td:last-child > span {
                border-top-right-radius: 4px;
                border-bottom-right-radius: 4px;
            }

            .pager table > tbody > tr > td > a:hover,
            .pager table > tbody > tr > td > span:hover,
            .pager table > tbody > tr > td > a:focus,
            .pager table > tbody > tr > td > span:focus {
                z-index: 2;
                color: #23527c;
                background-color: #eee;
                border-color: #ddd;
            }

        .modal {
            width: 100%;
        }

        .modal-dialog {
            width: 1000px;
            overflow-y: initial !important;
        }

        .modal-body {
            width: 1000px;
            height: 650px;
            overflow-y: auto;
        }

        .chkChoice input {
            margin-right: 8px;
        }

        .chkChoice td {
            padding-left: 5px;
        }
        /*input[type="radio"] {*/
        /*-webkit-appearance: checkbox;*/ /* Chrome, Safari, Opera */
        /*-moz-appearance: checkbox;*/ /* Firefox */
        /*-ms-appearance: checkbox;*/ /* not currently supported */
        /*}*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpTitle" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpMain" runat="Server">
    <!-- Modal -->

    <asp:HiddenField ID="hdnieid" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdniefuid" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdniefutype" runat="server"></asp:HiddenField>



    <div class="main-content-inner">
        <div class="page-content">
            <div class="page-header">
                <h1>
                    <small>Patient Details								
									<i class="ace-icon fa fa-angle-double-right"></i>

                    </small>
                </h1>
            </div>


            <div class="">

                <div class="row">
                    <div class="col-xs-12">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSearch" CssClass="form-control" placeholder="Search" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnRefresh" runat="server" CssClass="btn btn-success" Text="Refresh" OnClick="btnRefresh_Click" />
                                        <asp:HiddenField ID="hfPatientId" runat="server"></asp:HiddenField>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="space"></div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvPatientDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" DataKeyNames="PatientIE_ID" OnRowDataBound="OnRowDataBound" AllowPaging="True" OnPageIndexChanging="gvPatientDetails_PageIndexChanging1" PagerStyle-CssClass="pager">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <img alt="" title='<%# Eval("PatientIE_ID") %>' style="cursor: pointer" src="img/plus.png" />
                                                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                        <asp:GridView ID="gvPatientFUDetails" BorderStyle="None" CssClass="table table-bordered" Width="100%" runat="server" AllowPaging="True" OnPageIndexChanging="gvPatientFUDetails_PageIndexChanging" AutoGenerateColumns="False" EmptyDataText="No Records Found" PagerStyle-CssClass="pager">
                                                            <Columns>
                                                                <asp:BoundField DataField="DOE" HeaderText="DOE" DataFormatString="{0:d}" />
                                                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                                                <asp:BoundField DataField="MAProviders" HeaderText="MA & Providers" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <%--<asp:HyperLink runat="server" CssClass="btn btn-info" ID="hlAddFU" NavigateUrl='<%# "~/TimeSheet.aspx?PId="+Eval("PatientIEId")+"&FID="+Eval("PatientFUId")  %>' Text="Procedure Details"></asp:HyperLink>--%>
                                                                        <asp:HyperLink runat="server" CssClass="btn btn-info" ID="HyperLink1" NavigateUrl='<%# "~/EditFU.aspx?FUID="+Eval("PatientFUId") %>' Text="Edit"></asp:HyperLink>
                                                                        |
                                                                        <asp:HyperLink runat="server" CssClass="btn btn-link PrintClick" data-id='<%# Eval("PatientFUId") %>' data-FUIE="FU" ID="lkbtnReprint" Text="Print"></asp:HyperLink>
                                                                        | 
                                                                        <asp:HyperLink runat="server" CssClass="btn btn-link PrintClick" data-id='<%# Eval("PatientFUId") %>' data-FUIE="FU" ID="HyperLink2" Text='<%# Eval("PrintStatus").ToString() %>'></asp:HyperLink>


                                                                        |


                                                                        <asp:HyperLink runat="server" CssClass="btn btn-link" data-id='<%# Eval("PatientIEId") %>' data-idfu='<%# Eval("PatientFUId")%>' data-backdrop="false" data-toggle="modal" data-target="#exampleModalLong" onclick="modalopen(this)" data-FUIE="FU" ID="HyperLink3" Text="EX"></asp:HyperLink>
                                                                        <asp:HyperLink runat="server" style="display:none" CssClass="btn btn-link" data-backdrop="false" data-toggle="modal" data-target="#exampleModalLong" Text="EX2"></asp:HyperLink>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>

                                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Sex" HeaderText="Title" />
                                            <asp:BoundField DataField="lastname" HeaderText="LastName" />
                                            <asp:BoundField DataField="firstname" HeaderText="FirstName" />
                                            <asp:BoundField DataField="DOB" HeaderText="DOB" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="DOA" HeaderText="DOA" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="DOE" HeaderText="DOE" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="Compensation" HeaderText="Case Type" />
                                            <asp:BoundField DataField="location" HeaderText="Location" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" CssClass="btn btn-link" ID="hlEdit" NavigateUrl='<%# "~/Page1.aspx?id="+Eval("PatientIE_ID")+"&PID="+Eval("PatientId") %>' Text="Edit IE">
                                      
                                                    </asp:HyperLink>
                                                    | 
              <asp:HyperLink runat="server" CssClass="btn btn-link" ID="hlAddFU" NavigateUrl='<%# "~/AddFU.aspx?PID="+Eval("PatientIE_ID") %>' Text="AddFU"></asp:HyperLink>
                                                    |<asp:HyperLink runat="server" CssClass="btn btn-link PrintClick" data-id='<%# Eval("PatientIE_ID") %>' data-FUIE="IE" ID="lkbtnReprint" Text="Print"></asp:HyperLink>
                                                    |
                                                <asp:HyperLink runat="server" CssClass="btn btn-link PrintClick" data-id='<%# Eval("PatientIE_ID") %>' data-FUIE="IE" ID="HyperLink2" Text='<%# Eval("PrintStatus").ToString() %>'></asp:HyperLink>
                                                    |
                                                    <asp:LinkButton runat="server" ID="btnex" CssClass="btn btn-link" CommandArgument='<%# Eval("PatientIE_ID") %>' OnClick="btnex_Click" Text="Ex"></asp:LinkButton>
                                                    <asp:HyperLink runat="server" ID="lnkhyper" CssClass="btn btn-link" NavigateUrl='<%# "~/patientdocuments.aspx?PatientIE_ID="+Eval("PatientIE_ID") %>'>| Documents</asp:HyperLink>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings PageButtonCount="5" />

                                        <PagerStyle CssClass="pager"></PagerStyle>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfCurrentlyOpened" runat="server"></asp:HiddenField>
                    </div>
                </div>
                <script src="Scripts/jquery-1.8.2.js"></script>
                <script src="Scripts/jquery-ui-1.8.24.js"></script>
                <link href="Style/jquery-ui.css" rel="stylesheet" />
                <script type="text/javascript">
				var downloadPath = '<%=ConfigurationSettings.AppSettings["downloadpath"]%>';
                    $(document).ready(function () {
                        if ($('[title="' + $("#<%=hfCurrentlyOpened.ClientID %>").val() + '"]')) {
                            $('[title="' + $("#<%=hfCurrentlyOpened.ClientID %>").val() + '"]').closest("tr").after("<tr><td></td><td colspan = '999'>" + $('[title="' + $("#<%=hfCurrentlyOpened.ClientID %>").val() + '"]').next().html() + "</td></tr>");
                            $('[title="' + $("#<%=hfCurrentlyOpened.ClientID %>").val() + '"]').attr("src", "img/minus.png");
                        }
                        $("[src*=plus]").live("click", function () {
                            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                            $(this).attr("src", "img/minus.png");
                        });

                        $("[src*=minus]").live("click", function () {
                            $(this).attr("src", "img/plus.png");
                            $(this).closest("tr").next().remove();
                        });

                        $("#<%=txtSearch.ClientID %>").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: 'Search.aspx/GetPatients',
                                    data: "{ 'prefix': '" + request.term + "'}",
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        response($.map(data.d, function (item) {
                                            return {
                                                label: item.split('_')[0],
                                                val: item.split('_')[1]
                                            }
                                        }))
                                    },
                                    error: function (response) {
                                        alert(response.responseText);
                                    },
                                    failure: function (response) {
                                        alert(response.responseText);
                                    }
                                });
                            },
                            select: function (e, i) {
                                $("#<%=hfPatientId.ClientID %>").val(i.item.val);
                        $('#<%= txtSearch.ClientID %>').val(i.item.label);
                        $('#<%= btnSearch.ClientID %>').click();
                    },
                            minLength: 1
                        });



                        $(document).on("click", ".PrintClick", function () {
                            var currentID = this.id;
                            var obj = $(this);
                            var flag = $(this).attr("data-FUIE");
                            var id = $(this).attr("data-id");
                            var isdownload = 0;
                            if ($(this).html().toLowerCase() == "print") {
                                isdownload = 0;
                            }
                            else if ($(this).html().toLowerCase() == "print requested") {
                                alert("You already given print request.");
                                return false;
                            }
                            else if ($(this).html().toLowerCase() == "download" || $(this).html().toLowerCase() == "downloaded") {
                                isdownload = 1;
                            }
                            if (isdownload == 0) {
                                $.ajax({
                                    url: 'PatientIntakeList.aspx/UpdatePrintStatus',
                                    data: '{"flag": "' + flag + '", "id": ' + id + '}',
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        if (currentID.indexOf("lkbtnReprint") != -1) {
                                            alert("Print Request received.")
                                            location.reload();
                                        }
                                        else {
                                            $(obj).html("Print Requested");
                                        }
                                    },
                                    error: function (response) {
                                        alert(response.responseText);
                                    },
                                    failure: function (response) {
                                        alert(response.responseText);
                                    }
                                });
                            }
                            if (isdownload == 1) {
                                $.ajax({
                                    url: 'PatientIntakeList.aspx/CheckDownload',
                                    data: '{"flag": "' + flag + '", "id": ' + id + '}',
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        if (data.d == "") {
                                            alert("No files found to download.");
                                            return false;
                                        }
                                        var link = document.createElement("a");
                                        link.download = data.d;
                                        link.href = downloadPath + "/" + data.d + ".zip";
                                        link.click();
                                    },
                                    error: function (response) {
                                        alert(response.responseText);
                                    },
                                    failure: function (response) {
                                        alert(response.responseText);
                                    }
                                });
                            }
                        })

                        function modalopen(arg) {
                            $("#" + arg).click();
                        }

                        $(document).on("click", ".ExitClick", function () {
                            debugger;
                            var currentID = this.id;
                            var obj = $(this);

                            $("#<%=hdnieid.ClientID %>").val("")
                            $("#<%=hdniefuid.ClientID %>").val("")
                            $("#<%=hdniefutype.ClientID %>").val("")

                            var ieid = $(this).attr("data-id");
                            var fuid = $(this).attr('data-idfu');
                            var iefutype = $(this).attr("data-fuie");

                            $("#<%=hdnieid.ClientID %>").val(ieid)
                            $("#<%=hdniefuid.ClientID %>").val(fuid)
                            $("#<%=hdniefutype.ClientID %>").val(iefutype)



                            $.ajax({
                                // url: 'PatientIntakeList.aspx/BindExitForm',
                                data: '{"patientIeId": "' + ieid + '"}',
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {

                                    alert("Print Request received.")


                                },
                                error: function (response) {
                                    alert(response.responseText);
                                },
                                failure: function (response) {
                                    alert(response.responseText);
                                }
                            });


                        })

                    });


                </script>
            </div>
        </div>
    </div>
     <div id="printThis">
    <div class="modal fade" id="exampleModalLong" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-scrollable" role="document">
            <div class="modal-content" style="width: 100%;">
                <div id="divPrint" runat="server" name="divPrint" style="display:block;">
                    <p style="page-break-before: always"></p>
                    <div class="modal-header">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="lblCopay" runat="server" Text="Copay:" Font-Bold="true"></asp:Label>
                                    <asp:TextBox ID="txtCopay" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td style="width: 10%">
                                    <asp:Label ID="lblVisitEndForm" runat="server" Text="Visit End Form" Font-Bold="true"></asp:Label>

                                </td>
                                <td style="width: 10%">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </td>
                            </tr>
                        </table>



                    </div>
                    <div class="modal-body">
                        <div style="border: 2px solid !important; background-color: #d9d6d6">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 30%">
                                        <asp:Label ID="lblLoc" runat="server" Text="Location:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtLoc" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:HiddenField ID="hdnIfpp" runat="server" />
                                        <asp:CheckBoxList ID="chkIFPP" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Initial">Initial</asp:ListItem>
                                            <asp:ListItem Value="FU">F/U</asp:ListItem>
                                            <asp:ListItem Value="Proc">Proc</asp:ListItem>
                                            <asp:ListItem Value="ProcFU">Proc&F/U</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                    <td style="width: 30%; border-left: 2px solid black; border-bottom: 2px solid black;">
                                        <asp:Label ID="lblDate" runat="server" Text="Date:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 33%">
                                        <asp:Label ID="lblPatientName" runat="server" Text="PatientName:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtPatientName" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                    <td align="center" style="width: 40%">
                                        <asp:Label ID="lblDOB" runat="server" Text="DOB:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtDOB" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                    </td>
                                    <td style="width: 30%; border-left: 2px solid black;">
                                        <table>
                                            <tr>
                                                <td style="width: 2%">
                                                    <asp:Label ID="lblCase" runat="server" Text="Case:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 28%">
                                                    <asp:RadioButtonList ID="rdlCase" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <%--<asp:CheckBoxList ID="chkCase" runat="server" RepeatDirection="Horizontal">
                                                        
                                                    </asp:CheckBoxList>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="border-top: 2px solid !important;">
                                    <td colspan="3" style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 10%">
                                                    <asp:Label ID="lblInsurance" runat="server" Text="Insurance" Font-Bold="true"></asp:Label></td>

                                                <td style="width: 90%">
                                                    <asp:HiddenField ID="hdnInsurance" runat="server" />
                                                    <asp:CheckBoxList ID="chkInsurance" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="PLIGA">PLIGA</asp:ListItem>
                                                        <asp:ListItem Value="Mcare">Mcare</asp:ListItem>
                                                        <asp:ListItem Value="MVA">MVA</asp:ListItem>
                                                        <asp:ListItem Value="LOP">LOP</asp:ListItem>
                                                        <asp:ListItem Value="OON">OON</asp:ListItem>
                                                        <asp:ListItem Value="INN">INN</asp:ListItem>
                                                        <asp:ListItem Value="WC">WC</asp:ListItem>
                                                        <asp:ListItem Value="Self">Self</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                                <tr style="border-bottom: 2px solid !important;">
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblVerified" runat="server" Text="Verified:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdlVerified" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <%-- <asp:CheckBoxList ID="chkVerified" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                    </asp:CheckBoxList>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="border-bottom: 2px solid !important;">
                                    <td colspan="3" style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 10%">
                                                    <asp:Label ID="lblfuon" runat="server" Text="F/u on:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 90%">
                                                    <asp:HiddenField ID="hdnFUON" runat="server" />
                                                    <asp:CheckBoxList ID="chkfuon" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="LMRI">LMRI</asp:ListItem>
                                                        <asp:ListItem Value="TMRI">TMRI</asp:ListItem>
                                                        <asp:ListItem Value="CMRI">CMRI</asp:ListItem>
                                                        <asp:ListItem Value="CTLSP">CTLSP</asp:ListItem>
                                                        <asp:ListItem Value="CTTSP">CTTSP</asp:ListItem>
                                                        <asp:ListItem Value="CTCTSP">CTCTSP</asp:ListItem>
                                                        <asp:ListItem Value="XR LSP">XR LSP</asp:ListItem>
                                                        <asp:ListItem Value="XR TSP">XR TSP</asp:ListItem>
                                                        <asp:ListItem Value="XR CSP">XR CSP</asp:ListItem>


                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <asp:Label ID="lblOtherImaging" runat="server" Text="Other Imaging:" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width: 80%" colspan="2">
                                        <asp:HiddenField ID="hdnOtherImaging" runat="server" />
                                        <asp:CheckBoxList ID="chkOtherImaging" runat="server" RepeatDirection="Horizontal" Width="100%">
                                            <asp:ListItem Value="Chiropractic Therapy">Chiropractic Therapy</asp:ListItem>
                                            <asp:ListItem Value="Physical Therapy">Physical Therapy</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:HiddenField ID="hdnEMGU" runat="server" />
                                        <asp:CheckBoxList ID="chkEMGU" runat="server" RepeatDirection="Horizontal" Width="100%">
                                            <asp:ListItem Value="EMG UE">EMG UE</asp:ListItem>
                                            <asp:ListItem Value="EMG LE">EMG LE</asp:ListItem>
                                            <asp:ListItem Value="CTPI">CTPI</asp:ListItem>
                                            <asp:ListItem Value="TTPI">TTPI</asp:ListItem>
                                            <asp:ListItem Value="LTPI">LTPI</asp:ListItem>
                                            <asp:ListItem Value="LKIA">LKIA</asp:ListItem>
                                            <asp:ListItem Value="RKIA">RKIA</asp:ListItem>
                                            <asp:ListItem Value="RSIA">RSIA</asp:ListItem>
                                            <asp:ListItem Value="LSIA">LSIA</asp:ListItem>
                                        </asp:CheckBoxList>

                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblCESI" runat="server" Text="CESI:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtCESI" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="90px"></asp:TextBox>
                                        <asp:Label ID="lblTESI" runat="server" Text="TESI:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtTESI" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="90px"></asp:TextBox>
                                        <asp:Label ID="lblLESI" runat="server" Text="LESI:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtLESI" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="90px"></asp:TextBox>
                                        <asp:Label ID="lblLTFE" runat="server" Text="LTFE:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtLTFE" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="90px"></asp:TextBox>
                                        <asp:Label ID="lblCarpTunInj" runat="server" Text="CarpTun Inj:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtCarpTunInj" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="90px"></asp:TextBox>




                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblTRFA" runat="server" Text="TRFA:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtTRFA" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>
                                        <asp:Label ID="lblLRFA" runat="server" Text="LRFA:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtLRFA" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>
                                        <asp:Label ID="lblCRFA" runat="server" Text="CRFA:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtCRFA" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>
                                        <asp:Label ID="lblKneeGelInj" runat="server" Text="Knee Gel Inj:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtKneeGelInj" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>
                                        <asp:Label ID="lblSCSTrail" runat="server" Text="SCS Trail:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtSCSTrail" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>




                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="3" align="right">
                                        <asp:Label ID="lblLMBB" runat="server" Text="LMBB:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtLMBB" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>
                                        <asp:Label ID="lblTMBB" runat="server" Text="TMBB:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtTMBB" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>
                                        <asp:Label ID="lblCMBB" runat="server" Text="CMBB:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtCMBB" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>





                                    </td>

                                </tr>
                                <tr style="border-bottom: 2px solid black;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblOther1" runat="server" Text="Other:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtOther1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="200px"></asp:TextBox>
                                                </td>
                                                <td align="center">
                                                    <asp:HiddenField ID="hdnOther1" runat="server" />
                                                    <asp:CheckBoxList ID="chkOther1" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="Meds">Meds</asp:ListItem>
                                                        <asp:ListItem Value="Utox">Utox</asp:ListItem>
                                                        <asp:ListItem Value="NJPMP">NJPMP</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>

                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblRequestImaging" runat="server" Text="Request Imaging:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:HiddenField ID="hdnRequestImaging" runat="server" />
                                                    <asp:CheckBoxList ID="chkRequestImaging" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="LMRI">LMRI</asp:ListItem>
                                                        <asp:ListItem Value="TMRI">TMRI</asp:ListItem>
                                                        <asp:ListItem Value="CMRI">CMRI</asp:ListItem>
                                                        <asp:ListItem Value="CTLSP">CTLSP</asp:ListItem>
                                                        <asp:ListItem Value="CTTSP">CTTSP</asp:ListItem>
                                                        <asp:ListItem Value="CTCTSP">CTCTSP</asp:ListItem>
                                                        <asp:ListItem Value="Knee MRI">Knee MRI</asp:ListItem>


                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="border-bottom: 2px solid black;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblOtherImaging1" runat="server" Text="Other Imaging:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtOtherImaging1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="200px"></asp:TextBox>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblImagingLoc" runat="server" Text="Imaging Loc:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtImagingLoc" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>

                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblReqProc" runat="server" Text="Req Proc:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:HiddenField ID="hdnchkReqProc" runat="server" />
                                                    <asp:CheckBoxList ID="chkReqProc" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="EMG UE">EMG UE</asp:ListItem>
                                                        <asp:ListItem Value="EMG LE">EMG LE</asp:ListItem>
                                                        <asp:ListItem Value="CTPI">CTPI</asp:ListItem>
                                                        <asp:ListItem Value="TTPI">TTPI</asp:ListItem>
                                                        <asp:ListItem Value="LTPI">LTPI</asp:ListItem>
                                                        <asp:ListItem Value="LKIA">LKIA</asp:ListItem>
                                                        <asp:ListItem Value="RKIA">RKIA</asp:ListItem>
                                                        <asp:ListItem Value="RSIA">RSIA</asp:ListItem>
                                                        <asp:ListItem Value="LSIA">LSIA</asp:ListItem>
                                                    </asp:CheckBoxList>



                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblCESI1" runat="server" Text="CESI:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtCESI1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="90px"></asp:TextBox>
                                        <asp:Label ID="lblTESI1" runat="server" Text="TESI:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtTESI1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="90px"></asp:TextBox>
                                        <asp:Label ID="lblLESI1" runat="server" Text="LESI:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtLESI1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="90px"></asp:TextBox>
                                        <asp:Label ID="lblLTFE1" runat="server" Text="LTFE:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtLTFE1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="90px"></asp:TextBox>
                                        <asp:Label ID="lblMBB" runat="server" Text="MBB:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtMBB" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="90px"></asp:TextBox>




                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblTRFA1" runat="server" Text="TRFA:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtTRFA1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>
                                        <asp:Label ID="lblLRFA1" runat="server" Text="LRFA:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtLRFA1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>
                                        <asp:Label ID="lblCRFA1" runat="server" Text="CRFA:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtCRFA1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>
                                        <asp:Label ID="lblKneeGelInj1" runat="server" Text="Knee Gel Inj:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtKneeGelInj1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="85px"></asp:TextBox>





                                    </td>

                                </tr>
                                <tr style="border-bottom: 2px solid !important;">
                                    <td colspan="3" style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblOther2" runat="server" Text="Other:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtOther2" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="100px"></asp:TextBox>
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:HiddenField ID="hdnchkOther2" runat="server" />
                                                    <asp:CheckBoxList ID="chkOther2" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="RequestChiropractic Therapy">Request Chiropractic Therapy</asp:ListItem>
                                                        <asp:ListItem Value="RequestPhysical Therapy">Request Physical Therapy</asp:ListItem>
                                                    </asp:CheckBoxList>



                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblReqBrace" runat="server" Text="Request Brace:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:HiddenField ID="hdnchkReqBrace" runat="server" />
                                                    <asp:CheckBoxList ID="chkReqBrace" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="Lumbar">Lumbar</asp:ListItem>
                                                        <asp:ListItem Value="Thoracolumbar">Thoracolumbar</asp:ListItem>
                                                        <asp:ListItem Value="RKnee">RKnee</asp:ListItem>
                                                        <asp:ListItem Value="Lknee">Lknee</asp:ListItem>
                                                        <asp:ListItem Value="Wrist w/o Thumb">Wrist w/o Thumb</asp:ListItem>

                                                    </asp:CheckBoxList>



                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr style="border-bottom: 2px solid black;">
                                    <td colspan="3" style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblSeeForm" runat="server" Text="See Form:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:HiddenField ID="hdnchkSeeForm" runat="server" />
                                                    <asp:CheckBoxList ID="chkSeeForm" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="Cervicothoracic">Cervicothoracic</asp:ListItem>
                                                        <asp:ListItem Value="Lankle">Lankle</asp:ListItem>
                                                        <asp:ListItem Value="Rankle">Rankle</asp:ListItem>
                                                        <asp:ListItem Value="Lknee">Lknee</asp:ListItem>
                                                        <asp:ListItem Value="Wrist w/ Thumb">Wrist w/ Thumb</asp:ListItem>

                                                    </asp:CheckBoxList>



                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="border-bottom: 2px solid black;">
                                    <td colspan="2">
                                        <asp:Label ID="lblInHouseProcPerformed" runat="server" Text="In House Proc Performed:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtInHouseProcPerformed" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="border-left: 2px solid black;"></td>

                                </tr>
                                <tr style="background-color: white;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblProcedureDate" runat="server" Text="Procedure Date:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:HiddenField ID="hdnchkTBD" runat="server" />
                                                    <asp:CheckBoxList ID="chkTBD" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="TBD">TBD</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:Label ID="lblProcedure" runat="server" Text="Procedure:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtProcedure" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr style="background-color: white;">
                                    <td colspan="3" style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblLocationNew" runat="server" Text="Location:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:HiddenField ID="hdnchkLocationNew" runat="server" />
                                                    <asp:CheckBoxList ID="chkLocationNew" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="Manalapan">Manalapan</asp:ListItem>
                                                        <asp:ListItem Value="Jerseycity">Jersey city</asp:ListItem>
                                                        <asp:ListItem Value="Rahway">Rahway</asp:ListItem>
                                                        <asp:ListItem Value="Westorange">West orange</asp:ListItem>


                                                    </asp:CheckBoxList>



                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>



                                <tr style="border-bottom: 2px solid black; background-color: white;">
                                    <td colspan="3" style="width: 100%">
                                        <asp:HiddenField ID="hdnchkTLC" runat="server" />
                                        <asp:CheckBoxList ID="chkTLC" runat="server" RepeatDirection="Horizontal" Width="100%">
                                            <asp:ListItem Value="TBD">TBD</asp:ListItem>
                                            <asp:ListItem Value="LakeWood">Lake Wood</asp:ListItem>
                                            <asp:ListItem Value="Carteret">Carteret</asp:ListItem>


                                        </asp:CheckBoxList>



                                    </td>
                                </tr>
                                <tr style="background-color: white;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblProcDateNew1" runat="server" Text="Procedure Date:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:HiddenField ID="hdnchkTBDNew1" runat="server" />
                                                    <asp:CheckBoxList ID="chkTBDNew1" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="TBD">TBD</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:Label ID="lblProcNew1" runat="server" Text="Procedure:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtProcNew1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr style="background-color: white;">
                                    <td colspan="3" style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblLocNew1" runat="server" Text="Location:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 80%">
                                                    <asp:HiddenField ID="hdnchkLOcNew1" runat="server" />
                                                    <asp:CheckBoxList ID="chkLOcNew1" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="Manalapan">Manalapan</asp:ListItem>
                                                        <asp:ListItem Value="Jerseycity">Jersey city</asp:ListItem>
                                                        <asp:ListItem Value="Rahway">Rahway</asp:ListItem>
                                                        <asp:ListItem Value="Westorange">West orange</asp:ListItem>


                                                    </asp:CheckBoxList>



                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>



                                <tr style="border-bottom: 2px solid black; background-color: white;">
                                    <td colspan="3" style="width: 100%">
                                        <asp:HiddenField ID="hdnchkTLCNEW1" runat="server" />
                                        <asp:CheckBoxList ID="chkTLCNEW1" runat="server" RepeatDirection="Horizontal" Width="100%">
                                            <asp:ListItem Value="TBD">TBD</asp:ListItem>
                                            <asp:ListItem Value="LakeWood">Lake Wood</asp:ListItem>
                                            <asp:ListItem Value="Carteret">Carteret</asp:ListItem>


                                        </asp:CheckBoxList>



                                    </td>
                                </tr>
                                <tr style="border-bottom: 2px solid black; background-color: white;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:Label ID="lblHouseProcDate" runat="server" Text="In House Proc Date/Loc:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtHouseProcDate" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:Label ID="lblInHouseProc" runat="server" Text="In House Procedure:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtInHouseProc" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                                </td>
                                            </tr>

                                        </table>

                                    </td>
                                </tr>
                                <tr style="border-bottom: 2px solid black; background-color: white;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:Label ID="lblHouseProcDate1" runat="server" Text="In House Proc Date/Loc:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtHouseProcDate1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:Label ID="lblInHouseProc1" runat="server" Text="In House Procedure:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtInHouseProc1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                                </td>
                                            </tr>

                                        </table>

                                    </td>
                                </tr>
                                <tr style="border-bottom: 2px solid black; background-color: white;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:Label ID="lblHouseProcDate2" runat="server" Text="In House Proc Date/Loc:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtHouseProcDate2" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:Label ID="lblInHouseProc2" runat="server" Text="In House Procedure:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtInHouseProc2" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                                </td>
                                            </tr>

                                        </table>

                                    </td>
                                </tr>
                                <tr style="border-bottom: 2px solid black; background-color: white;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:HiddenField ID="hdnchkOnAc" runat="server" />
                                                    <asp:CheckBoxList ID="chkOnAc" runat="server" RepeatDirection="Horizontal" Width="40%">
                                                        <asp:ListItem Value="OnAc">On A/C</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:HiddenField ID="hdnchkPthxDM" runat="server" />
                                                    <asp:CheckBoxList ID="chkPthxDM" runat="server" RepeatDirection="Horizontal" Width="40%">

                                                        <asp:ListItem Value="PthxDM">Pt hx DM</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>

                                    </td>

                                </tr>







                            </table>
                        </div>
                    </div>
                        <p style="page-break-before: always"></p>
                    <div class="modal-header">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="lblCopay1" runat="server" Text="Copay:" Font-Bold="true"></asp:Label>
                                    <asp:TextBox ID="txtCopay1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td style="width: 50%" align="left">
                                    <asp:Label ID="lblVisitEndForm1" runat="server" Text="Visit End Form" Font-Bold="true"></asp:Label>

                                </td>

                            </tr>
                        </table>



                    </div>
                    <div class="modal-body">
                        <div style="border: 2px solid !important;">
                            <table style="width: 100%">
                                <tr style="border-bottom: 2px solid !important;">
                                    <td colspan="3" style="width: 30%;">
                                        <table>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblHasApptPleaseRemind" runat="server" Text="Has F/u Appt Please Remind:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:HiddenField ID="hdnchkHasApptPleaseRemind" runat="server" />
                                                    <asp:CheckBoxList ID="chkHasApptPleaseRemind" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="PRN">PRN</asp:ListItem>
                                                        <asp:ListItem Value="Discharge">Discharge</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 30%;">
                                        <table>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblReturnVisit1" runat="server" Text="Return visit #1:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:HiddenField ID="hdnchkReturnVisit1" runat="server" />
                                                    <asp:CheckBoxList ID="chkReturnVisit1" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="1week">1 week</asp:ListItem>
                                                        <asp:ListItem Value="2week">2 week</asp:ListItem>
                                                        <asp:ListItem Value="3week">3 week</asp:ListItem>
                                                        <asp:ListItem Value="4week">4 week</asp:ListItem>
                                                        <asp:ListItem Value="5week">5 week</asp:ListItem>
                                                        <asp:ListItem Value="6week">6 week</asp:ListItem>
                                                        <asp:ListItem Value="7week">7 week</asp:ListItem>
                                                        <asp:ListItem Value="8week">8 week</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right">
                                        <asp:Label ID="lblOtherreview1" runat="server" Text="Other:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtOtherreview1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <asp:Label ID="lblDateReview1" runat="server" Text="Date:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtDateReview1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <asp:Label ID="lblTimeReview1" runat="server" Text="Time:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtTimeReview1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <asp:Label ID="lblLocReview1" runat="server" Text="Loc:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtLocReview1" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 30%;">
                                        <table>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblVisitType" runat="server" Text="Visit Type:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:HiddenField ID="hdnchkVisitType" runat="server" />
                                                    <asp:CheckBoxList ID="chkVisitType" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="MedReview">Med Review</asp:ListItem>
                                                        <asp:ListItem Value="MedRefill">Med Refill</asp:ListItem>
                                                        <asp:ListItem Value="TherapyReview">Therapy Review</asp:ListItem>
                                                        <asp:ListItem Value="ImagingReview">Imaging Review</asp:ListItem>

                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="border-bottom: 2px solid black;">
                                    <td>
                                        <asp:Label ID="lblOrProc" runat="server" Text="Or Proc:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtOrproc" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInhouse" runat="server" Text="In House Proc:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtInhouse" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmgreview" runat="server" Text="EMG Review:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtEmgreview" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 30%;">
                                        <table>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblReturnVisit2" runat="server" Text="Return visit #2:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:HiddenField ID="hdnchkReturnVisit2" runat="server" />
                                                    <asp:CheckBoxList ID="chkReturnVisit2" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="1week">1 week</asp:ListItem>
                                                        <asp:ListItem Value="2week">2 week</asp:ListItem>
                                                        <asp:ListItem Value="3week">3 week</asp:ListItem>
                                                        <asp:ListItem Value="4week">4 week</asp:ListItem>
                                                        <asp:ListItem Value="5week">5 week</asp:ListItem>
                                                        <asp:ListItem Value="6week">6 week</asp:ListItem>
                                                        <asp:ListItem Value="7week">7 week</asp:ListItem>
                                                        <asp:ListItem Value="8week">8 week</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right">
                                        <asp:Label ID="lblOtherreview2" runat="server" Text="Other:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtOtherreview2" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <asp:Label ID="lblDateReview2" runat="server" Text="Date:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtDateReview2" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <asp:Label ID="lblTimeReview2" runat="server" Text="Time:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtTimeReview2" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <asp:Label ID="lblLocReview2" runat="server" Text="Loc:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtLocReview2" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 30%;">
                                        <table>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblVisitType2" runat="server" Text="Visit Type:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:HiddenField ID="hdnchkVisitType2" runat="server" />
                                                    <asp:CheckBoxList ID="chkVisitType2" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="MedReview">Med Review</asp:ListItem>
                                                        <asp:ListItem Value="MedRefill">Med Refill</asp:ListItem>
                                                        <asp:ListItem Value="TherapyReview">Therapy Review</asp:ListItem>
                                                        <asp:ListItem Value="ImagingReview">Imaging Review</asp:ListItem>

                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="border-bottom: 2px solid black;">
                                    <td>
                                        <asp:Label ID="lblOrProc2" runat="server" Text="Or Proc:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtOrproc2" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInhouse2" runat="server" Text="In House Proc:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtInhouse2" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmgreview2" runat="server" Text="EMG Review:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtEmgreview2" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 30%;">
                                        <table>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblReturnVisit3" runat="server" Text="Return visit #3:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:HiddenField ID="hdnchkReturnVisit3" runat="server" />
                                                    <asp:CheckBoxList ID="chkReturnVisit3" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="1week">1 week</asp:ListItem>
                                                        <asp:ListItem Value="2week">2 week</asp:ListItem>
                                                        <asp:ListItem Value="3week">3 week</asp:ListItem>
                                                        <asp:ListItem Value="4week">4 week</asp:ListItem>
                                                        <asp:ListItem Value="5week">5 week</asp:ListItem>
                                                        <asp:ListItem Value="6week">6 week</asp:ListItem>
                                                        <asp:ListItem Value="7week">7 week</asp:ListItem>
                                                        <asp:ListItem Value="8week">8 week</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right">
                                        <asp:Label ID="lblOtherreview3" runat="server" Text="Other:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtOtherreview3" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <asp:Label ID="lblDateReview3" runat="server" Text="Date:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtDateReview3" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <asp:Label ID="lblTimeReview3" runat="server" Text="Time:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtTimeReview3" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                        <asp:Label ID="lblLocReview3" runat="server" Text="Loc:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtLocReview3" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 30%;">
                                        <table>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblVisitType3" runat="server" Text="Visit Type:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:HiddenField ID="hdnchkVisitType3" runat="server" />
                                                    <asp:CheckBoxList ID="chkVisitType3" runat="server" RepeatDirection="Horizontal" Width="100%">
                                                        <asp:ListItem Value="MedReview">Med Review</asp:ListItem>
                                                        <asp:ListItem Value="MedRefill">Med Refill</asp:ListItem>
                                                        <asp:ListItem Value="TherapyReview">Therapy Review</asp:ListItem>
                                                        <asp:ListItem Value="ImagingReview">Imaging Review</asp:ListItem>

                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="border-bottom: 2px solid black;">
                                    <td>
                                        <asp:Label ID="lblOrProc3" runat="server" Text="Or Proc:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtOrproc3" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInhouse3" runat="server" Text="In House Proc:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtInhouse3" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmgreview3" runat="server" Text="EMG Review:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtEmgreview3" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr style="border-bottom: 2px solid black;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblUtox1" runat="server" Text="Utox:" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlUtox" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <%--<asp:CheckBoxList ID="chkUtox1" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:CheckBoxList>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 50%; border-left: 2px solid black;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblScriptsGiven" runat="server" Text="Scripts Given:" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlScriptsGiven" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <%-- <asp:CheckBoxList ID="chkScriptsGiven" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:CheckBoxList>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>



                                    </td>
                                </tr>


                                <tr style="border-bottom: 2px solid black;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>

                                                <td>
                                                    <asp:Label ID="lblRecordRequest" runat="server" Text="Records Request:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtRecordRequest" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="80px"></asp:TextBox>
                                                </td>
                                                <td style="width: 40%; background-color: #d9d6d6;">
                                                    <table>
                                                        <tr>
                                                            <td>

                                                                <asp:Label ID="lblFormCompleted" runat="server" Text="Form Completed:" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlFormCompleted" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <%--<asp:CheckBoxList ID="chkFormCompleted" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:CheckBoxList>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr style="border-bottom: 2px solid black; background-color: #d9d6d6;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSendLegalUpdate" runat="server" Text="Send Legal Update:" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlSendLegalUpdate" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <%--<asp:CheckBoxList ID="chkSendLegalUpdate" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:CheckBoxList>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 50%; border-left: 2px solid black;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblScriptscanned" runat="server" Text="Script scanned:" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlScriptscanned" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <%--  <asp:CheckBoxList ID="chkScriptscanned" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                                </asp:CheckBoxList>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>



                                    </td>
                                </tr>
                                <tr style="background-color: #d9d6d6;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    <asp:Label ID="lblCompletedBy" runat="server" Text="Completed By:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtCompletedBy" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="180px"></asp:TextBox>
                                                </td>
                                                <td style="width: 50%; border-left: 2px solid black;">
                                                    <asp:Label ID="lblCollected" runat="server" Text="Collected:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtCollected" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                            </table>
                        </div>
                        <br />

                        <div style="border: 2px solid !important;">
                            <table style="width: 100%">
                                <tr style="border-bottom: 2px solid !important;">
                                    <td colspan="3">
                                        <table width="100%">
                                            <tr style="border-bottom: 1px solid !important;">
                                                <td style="width: 50%">New Pt</td>
                                                <td style="border-left: 2px solid black; width: 50%;">Surgical Referral</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblReferredBy" runat="server" Text="Referred By:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtReferredBy" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="180px"></asp:TextBox>
                                                </td>
                                                <td style="border-left: 2px solid black;">
                                                    <asp:Label ID="lblOrthopedics" runat="server" Text="Orthopedics:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtOrthopedics" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTherapyReferral" runat="server" Text="Therapy Referral:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtTherapyReferral" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="180px"></asp:TextBox>
                                                </td>
                                                <td style="border-left: 2px solid black;">
                                                    <asp:Label ID="lblSpine" runat="server" Text="Spine:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtSpine" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblLegalReferral" runat="server" Text="Legal Referral:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtLegalReferral" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="180px"></asp:TextBox>
                                                </td>
                                                <td style="border-left: 2px solid black; border-bottom: 2px solid black;">
                                                    <asp:Label ID="lblPodiatry" runat="server" Text="Podiatry:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtPodiatry" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblImagingReferral" runat="server" Text="Imaging Referral:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtImagingReferral" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="180px"></asp:TextBox>
                                                </td>
                                                <td style="border-left: 2px solid black;">
                                                    <asp:Label ID="lblEmgReferral" runat="server" Text="Emg Referral:" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtEmgReferral" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblComments" runat="server" Text="Comments:" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                    </td>


                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtComments" runat="server" BorderColor="Black" BorderStyle="Ridge" BorderWidth="1px" Width="50%" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>




                        </div>
                    </div>
                    <div class="modal-footer">

                        <%--<asp:Button ID="SaveExitForm" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="SaveExitForm_Click" />--%>
                        <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                        <asp:Button ID="SaveExitForm" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="SaveExitForm_Click" />
                     <asp:Button ID="UpdateExitForm" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="UpdateExitForm_Click"  Visible="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
         </div>
    <script type="text/javascript">
        $.noConflict();
        function openModel() {
            $('#exampleModalLong').modal('show');

           
        }
        function openModal1()
        {
            $('#exampleModalLong').modal('show');

           
            var divContents = $("#printThis").html();

                var printWindow = window.open('', '', 'height=400,width=800');
                printWindow.document.write('<html><head><title>DIV Contents</title>');
                printWindow.document.write('</head><body >');
                printWindow.document.write(divContents);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                printWindow.print();  
                $('#exampleModalLong').modal('hide');
            }
        function CloseModel() {
            $('#exampleModalLong').modal('hide');
        }
        function printDiv(divName) {
            debugger;
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
</asp:Content>

