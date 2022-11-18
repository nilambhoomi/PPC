using IntakeSheet.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;

using System.IO.Compression;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;




public partial class PatientIntakeList : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["uname"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            Session["patientFUId"] = "";
           // bindLocation();
            BindPatientIEDetails();
        }
        // BindExitForm();


    }

    protected void UpdateExitForm_Click(object sender, EventArgs e)
    {
        string ieid = hdnieid.Value;
        string iefuid = hdniefuid.Value;
        string iefutype = hdniefutype.Value;

        # region pg1
        string selectedchkIFPP = String.Join(",", chkIFPP.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnIfpp.Value = selectedchkIFPP;
        string selectedchkInsurance = String.Join(",", chkInsurance.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnInsurance.Value = selectedchkInsurance;
        string selectedchkfuon = String.Join(",", chkfuon.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnFUON.Value = selectedchkfuon;
        string selectedchkOtherImaging = String.Join(",", chkOtherImaging.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnOtherImaging.Value = selectedchkOtherImaging;
        string selectedchkEMGU = String.Join(",", chkEMGU.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnEMGU.Value = selectedchkEMGU;
        string selectedchkOther1 = String.Join(",", chkOther1.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnOther1.Value = selectedchkOther1;
        string selectedchkRequestImaging = String.Join(",", chkRequestImaging.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnRequestImaging.Value = selectedchkRequestImaging;
        string selectedchkReqProc = String.Join(",", chkReqProc.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkReqProc.Value = selectedchkReqProc;
        string selectedchkOther2 = String.Join(",", chkOther2.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkOther2.Value = selectedchkOther2;
        string selectedchkReqBrace = String.Join(",", chkReqBrace.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkReqBrace.Value = selectedchkReqBrace;
        string selectedchkSeeForm = String.Join(",", chkSeeForm.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkSeeForm.Value = selectedchkSeeForm;
        string selectedchkTBD = String.Join(",", chkTBD.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkTBD.Value = selectedchkTBD;
        string selectedchkLocationNew = String.Join(",", chkLocationNew.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkLocationNew.Value = selectedchkLocationNew;
        string selectedchkTLC = String.Join(",", chkTLC.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkTLC.Value = selectedchkTLC;

        string selectedchkTBDNew1 = String.Join(",", chkTBDNew1.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkTBDNew1.Value = selectedchkTBDNew1;
        string selectedchkLOcNew1 = String.Join(",", chkLOcNew1.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkLOcNew1.Value = selectedchkLOcNew1;
        string selectedchkTLCNEW1 = String.Join(",", chkTLCNEW1.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkTLCNEW1.Value = selectedchkTLCNEW1;

        string selectedchkOnAc = String.Join(",", chkOnAc.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkOnAc.Value = selectedchkOnAc;
        string selectedchkPthxDM = String.Join(",", chkPthxDM.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkPthxDM.Value = selectedchkPthxDM;
        #endregion
        # region pg2
        string selectedchkHasApptPleaseRemind = String.Join(",", chkHasApptPleaseRemind.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkHasApptPleaseRemind.Value = selectedchkHasApptPleaseRemind;
        string selectedchkReturnVisit1 = String.Join(",", chkReturnVisit1.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkReturnVisit1.Value = selectedchkReturnVisit1;
        string selectedchkVisitType = String.Join(",", chkVisitType.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkVisitType.Value = selectedchkVisitType;

        string selectedchkReturnVisit2 = String.Join(",", chkReturnVisit2.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkReturnVisit2.Value = selectedchkReturnVisit2;
        string selectedchkVisitType2 = String.Join(",", chkVisitType2.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkVisitType2.Value = selectedchkVisitType2;

        string selectedchkReturnVisit3 = String.Join(",", chkReturnVisit3.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkReturnVisit3.Value = selectedchkReturnVisit3;
        string selectedchkVisitType3 = String.Join(",", chkVisitType3.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkVisitType3.Value = selectedchkVisitType3;
        # endregion

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connString_NV_Jo"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_updateExitForm", con);

            if (!string.IsNullOrEmpty(ieid))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                #region pageone
                cmd.Parameters.AddWithValue("@PatientIE_ID", ieid);
                cmd.Parameters.AddWithValue("Copay", Convert.ToString(txtCopay.Text));
                cmd.Parameters.AddWithValue("Location", Convert.ToString(txtLoc.Text));
                cmd.Parameters.AddWithValue("IFPP", Convert.ToString(hdnIfpp.Value));
                cmd.Parameters.AddWithValue("ExitDate", Convert.ToString(txtDate.Text));
                cmd.Parameters.AddWithValue("PatientName", Convert.ToString(txtPatientName.Text));
                cmd.Parameters.AddWithValue("DOB", Convert.ToString(txtDOB.Text));


                if (rdlCase.SelectedIndex == -1)
                {

                    cmd.Parameters.AddWithValue("@ExitCase", Convert.ToString(rdlCase.SelectedValue = "none"));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ExitCase", Convert.ToString(rdlCase.SelectedItem.Text));
                }

                cmd.Parameters.AddWithValue("@Insurance", Convert.ToString(hdnInsurance.Value));
                if (rdlVerified.SelectedIndex == -1)
                {

                    cmd.Parameters.AddWithValue("@Verified", Convert.ToString(rdlVerified.SelectedValue = "none"));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Verified", Convert.ToString(rdlVerified.SelectedItem.Text));
                }

                cmd.Parameters.AddWithValue("@Fuon", Convert.ToString(hdnFUON.Value));
                cmd.Parameters.AddWithValue("@OtherImaging", Convert.ToString(hdnOtherImaging.Value));
                cmd.Parameters.AddWithValue("@EECTLR", Convert.ToString(hdnEMGU.Value));
                cmd.Parameters.AddWithValue("@CESI", Convert.ToString(txtCESI.Text));
                cmd.Parameters.AddWithValue("@TESI", Convert.ToString(txtTESI.Text));
                cmd.Parameters.AddWithValue("@LESI", Convert.ToString(txtLESI.Text));
                cmd.Parameters.AddWithValue("@LTFE", Convert.ToString(txtLTFE.Text));
                cmd.Parameters.AddWithValue("@CarpTun", Convert.ToString(txtCarpTunInj.Text));
                cmd.Parameters.AddWithValue("@TRFA", Convert.ToString(txtTRFA.Text));
                cmd.Parameters.AddWithValue("@LRFA", Convert.ToString(txtLRFA.Text));
                cmd.Parameters.AddWithValue("@CRFA", Convert.ToString(txtCRFA.Text));
                cmd.Parameters.AddWithValue("@KneeJelInj", Convert.ToString(txtKneeGelInj.Text));
                cmd.Parameters.AddWithValue("@SCSTrail", Convert.ToString(txtSCSTrail.Text));
                cmd.Parameters.AddWithValue("@LMBB", Convert.ToString(txtLMBB.Text));
                cmd.Parameters.AddWithValue("@TMBB", Convert.ToString(txtTMBB.Text));
                cmd.Parameters.AddWithValue("@CMBB", Convert.ToString(txtCMBB.Text));
                cmd.Parameters.AddWithValue("@Other", Convert.ToString(txtOther1.Text));
                cmd.Parameters.AddWithValue("@MUN", Convert.ToString(hdnOther1.Value));
                cmd.Parameters.AddWithValue("@RequestImaging", Convert.ToString(hdnRequestImaging.Value));
                cmd.Parameters.AddWithValue("@ReqOtherImaging", Convert.ToString(txtOtherImaging1.Text));
                cmd.Parameters.AddWithValue("@ImagingLoc", Convert.ToString(txtImagingLoc.Text));
                cmd.Parameters.AddWithValue("@ReqProc", Convert.ToString(hdnchkReqProc.Value));

                cmd.Parameters.AddWithValue("@ReqProcCESI", Convert.ToString(txtCESI1.Text));
                cmd.Parameters.AddWithValue("@ReqProcTESI", Convert.ToString(txtTESI1.Text));
                cmd.Parameters.AddWithValue("@ReqProcLESI", Convert.ToString(txtLESI1.Text));
                cmd.Parameters.AddWithValue("@ReqProcLTFE", Convert.ToString(txtLTFE1.Text));
                cmd.Parameters.AddWithValue("@ReqProcMBB", Convert.ToString(txtMBB.Text));
                cmd.Parameters.AddWithValue("@ReqProcTRFA", Convert.ToString(txtTRFA1.Text));
                cmd.Parameters.AddWithValue("@ReqProcLRFA", Convert.ToString(txtLRFA1.Text));
                cmd.Parameters.AddWithValue("@ReqProcCRFA", Convert.ToString(txtCRFA1.Text));
                cmd.Parameters.AddWithValue("@ReqProcKneeJelInj", Convert.ToString(txtKneeGelInj1.Text));
                cmd.Parameters.AddWithValue("@ReqProcOther", Convert.ToString(txtOther2.Text));
                cmd.Parameters.AddWithValue("@ReqTherapy", Convert.ToString(hdnchkOther2.Value));
                cmd.Parameters.AddWithValue("@RequestBrace", Convert.ToString(hdnchkReqBrace.Value));
                cmd.Parameters.AddWithValue("@SeeForm", Convert.ToString(hdnchkSeeForm.Value));
                cmd.Parameters.AddWithValue("@InHouseholdPerformed", Convert.ToString(txtInHouseProcPerformed.Text));
                cmd.Parameters.AddWithValue("@ProcedureDate", Convert.ToString(hdnchkTBD.Value));
                cmd.Parameters.AddWithValue("@ReqProcedure", Convert.ToString(txtProcedure.Text));
                cmd.Parameters.AddWithValue("@ReqLocation", Convert.ToString(hdnchkLocationNew.Value));
                cmd.Parameters.AddWithValue("@TLC", Convert.ToString(hdnchkTLC.Value));
                cmd.Parameters.AddWithValue("@ProcedureDate1", Convert.ToString(hdnchkTBDNew1.Value));
                cmd.Parameters.AddWithValue("@ReqProcedure1", Convert.ToString(txtProcNew1.Text));
                cmd.Parameters.AddWithValue("@ReqLocation1", Convert.ToString(hdnchkLOcNew1.Value));
                cmd.Parameters.AddWithValue("@TLC1", Convert.ToString(hdnchkTLCNEW1.Value));

                cmd.Parameters.AddWithValue("@InHouseProcDateLoc", Convert.ToString(txtHouseProcDate.Text));
                cmd.Parameters.AddWithValue("@InHouseProcDateLoc1", Convert.ToString(txtHouseProcDate1.Text));
                cmd.Parameters.AddWithValue("@InHouseProcDateLoc2", Convert.ToString(txtHouseProcDate2.Text));
                cmd.Parameters.AddWithValue("@InHouseProcedure", Convert.ToString(txtInHouseProc.Text));
                cmd.Parameters.AddWithValue("@InHouseProcedure1", Convert.ToString(txtInHouseProc1.Text));
                cmd.Parameters.AddWithValue("@InHouseProcedure2", Convert.ToString(txtInHouseProc2.Text));

                cmd.Parameters.AddWithValue("@OnAC", Convert.ToString(hdnchkOnAc.Value));
                cmd.Parameters.AddWithValue("@PthxDM", Convert.ToString(hdnchkPthxDM.Value));

                #endregion

                #region review
                cmd.Parameters.AddWithValue("@Copay1", Convert.ToString(txtCopay1.Text));
                cmd.Parameters.AddWithValue("@HasFUApptPlsRem", Convert.ToString(hdnchkHasApptPleaseRemind.Value));
                cmd.Parameters.AddWithValue("@ReturnVisit1", Convert.ToString(hdnchkReturnVisit1.Value));
                cmd.Parameters.AddWithValue("@ReturnVisitOther1", Convert.ToString(txtOtherreview1.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitDate1", Convert.ToString(txtDateReview1.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitTime1", Convert.ToString(txtTimeReview1.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitLoc1", Convert.ToString(txtLocReview1.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitType1", Convert.ToString(hdnchkVisitType.Value));
                cmd.Parameters.AddWithValue("@ReturnVisitORProc1", Convert.ToString(txtOrproc.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitINnHouseProc1", Convert.ToString(txtInhouse.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitEMGReview1", Convert.ToString(txtEmgreview.Text));

                cmd.Parameters.AddWithValue("@ReturnVisit2", Convert.ToString(hdnchkReturnVisit2.Value));
                cmd.Parameters.AddWithValue("@ReturnVisitOther2", Convert.ToString(txtOtherreview2.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitDate2", Convert.ToString(txtDateReview2.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitTime2", Convert.ToString(txtTimeReview2.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitLoc2", Convert.ToString(txtLocReview2.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitType2", Convert.ToString(hdnchkVisitType2.Value));
                cmd.Parameters.AddWithValue("@ReturnVisitORProc2", Convert.ToString(txtOrproc2.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitINnHouseProc2", Convert.ToString(txtInhouse2.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitEMGReview2", Convert.ToString(txtEmgreview2.Text));

                cmd.Parameters.AddWithValue("@ReturnVisit3", Convert.ToString(hdnchkReturnVisit3.Value));
                cmd.Parameters.AddWithValue("@ReturnVisitOther3", Convert.ToString(txtOtherreview3.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitDate3", Convert.ToString(txtDateReview3.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitTime3", Convert.ToString(txtTimeReview3.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitLoc3", Convert.ToString(txtLocReview3.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitType3", Convert.ToString(hdnchkVisitType3.Value));
                cmd.Parameters.AddWithValue("@ReturnVisitORProc3", Convert.ToString(txtOrproc3.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitINnHouseProc3", Convert.ToString(txtInhouse3.Text));
                cmd.Parameters.AddWithValue("@ReturnVisitEMGReview3", Convert.ToString(txtEmgreview3.Text));
                #endregion
                #region  page2comp
                if (rdlUtox.SelectedIndex == -1)
                {

                    cmd.Parameters.AddWithValue("@UTOX", Convert.ToString(rdlUtox.SelectedValue = "none"));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@UTOX", Convert.ToString(rdlUtox.SelectedItem.Text));
                }


                if (rdlScriptsGiven.SelectedIndex == -1)
                {

                    cmd.Parameters.AddWithValue("@ScriptsGiven", Convert.ToString(rdlScriptsGiven.SelectedValue = "none"));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ScriptsGiven", Convert.ToString(rdlScriptsGiven.SelectedItem.Text));
                }


                cmd.Parameters.AddWithValue("@RecordsRequest", Convert.ToString(txtRecordRequest.Text));
                if (rdlFormCompleted.SelectedIndex == -1)
                {

                    cmd.Parameters.AddWithValue("@FormCompleted", Convert.ToString(rdlFormCompleted.SelectedValue = "none"));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FormCompleted", Convert.ToString(rdlFormCompleted.SelectedItem.Text));
                }


                if (rdlSendLegalUpdate.SelectedIndex == -1)
                {

                    cmd.Parameters.AddWithValue("@SendLegalUpdate", Convert.ToString(rdlSendLegalUpdate.SelectedValue = "none"));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SendLegalUpdate", Convert.ToString(rdlSendLegalUpdate.SelectedItem.Text));
                }



                if (rdlScriptscanned.SelectedIndex == -1)
                {

                    cmd.Parameters.AddWithValue("@ScriptsScanned", Convert.ToString(rdlScriptscanned.SelectedValue = "none"));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ScriptsScanned", Convert.ToString(rdlScriptscanned.SelectedItem.Text));
                }


                cmd.Parameters.AddWithValue("@CompltedBy", Convert.ToString(txtCompletedBy.Text));
                cmd.Parameters.AddWithValue("@Collected", Convert.ToString(txtCollected.Text));
                #endregion
                cmd.Parameters.AddWithValue("@ReferredBy", Convert.ToString(txtReferredBy.Text));
                cmd.Parameters.AddWithValue("@TherapyReferral", Convert.ToString(txtTherapyReferral.Text));
                cmd.Parameters.AddWithValue("@LegalReferral", Convert.ToString(txtLegalReferral.Text));
                cmd.Parameters.AddWithValue("@ImagingReferral", Convert.ToString(txtImagingReferral.Text));
                cmd.Parameters.AddWithValue("@Orthopedics", Convert.ToString(txtOrthopedics.Text));
                cmd.Parameters.AddWithValue("@Spine", Convert.ToString(txtSpine.Text));
                cmd.Parameters.AddWithValue("@Podiatry", Convert.ToString(txtPodiatry.Text));
                cmd.Parameters.AddWithValue("@EmgReferral", Convert.ToString(txtEmgReferral.Text));
                cmd.Parameters.AddWithValue("@Comments", Convert.ToString(txtComments.Text));


                int i = cmd.ExecuteNonQuery();
                con.Close();
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModal1()", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "printElement()", true);
            }
        }

    }
    protected void SaveExitForm_Click(object sender, EventArgs e)
    {
        string ieid = hdnieid.Value;
        string iefuid = hdniefuid.Value;
        string iefutype = hdniefutype.Value;

        # region pg1
        string selectedchkIFPP = String.Join(",", chkIFPP.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnIfpp.Value = selectedchkIFPP;
        string selectedchkInsurance = String.Join(",", chkInsurance.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnInsurance.Value = selectedchkInsurance;
        string selectedchkfuon = String.Join(",", chkfuon.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnFUON.Value = selectedchkfuon;
        string selectedchkOtherImaging = String.Join(",", chkOtherImaging.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnOtherImaging.Value = selectedchkOtherImaging;
        string selectedchkEMGU = String.Join(",", chkEMGU.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnEMGU.Value = selectedchkEMGU;
        string selectedchkOther1 = String.Join(",", chkOther1.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnOther1.Value = selectedchkOther1;
        string selectedchkRequestImaging = String.Join(",", chkRequestImaging.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnRequestImaging.Value = selectedchkRequestImaging;
        string selectedchkReqProc = String.Join(",", chkReqProc.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkReqProc.Value = selectedchkReqProc;
        string selectedchkOther2 = String.Join(",", chkOther2.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkOther2.Value = selectedchkOther2;
        string selectedchkReqBrace = String.Join(",", chkReqBrace.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkReqBrace.Value = selectedchkReqBrace;
        string selectedchkSeeForm = String.Join(",", chkSeeForm.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkSeeForm.Value = selectedchkSeeForm;
        string selectedchkTBD = String.Join(",", chkTBD.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkTBD.Value = selectedchkTBD;
        string selectedchkLocationNew = String.Join(",", chkLocationNew.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkLocationNew.Value = selectedchkLocationNew;
        string selectedchkTLC = String.Join(",", chkTLC.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkTLC.Value = selectedchkTLC;

        string selectedchkTBDNew1 = String.Join(",", chkTBDNew1.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkTBDNew1.Value = selectedchkTBDNew1;
        string selectedchkLOcNew1 = String.Join(",", chkLOcNew1.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkLOcNew1.Value = selectedchkLOcNew1;
        string selectedchkTLCNEW1 = String.Join(",", chkTLCNEW1.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkTLCNEW1.Value = selectedchkTLCNEW1;

        string selectedchkOnAc = String.Join(",", chkOnAc.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkOnAc.Value = selectedchkOnAc;
        string selectedchkPthxDM = String.Join(",", chkPthxDM.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkPthxDM.Value = selectedchkPthxDM;
        #endregion
        # region pg2
        string selectedchkHasApptPleaseRemind = String.Join(",", chkHasApptPleaseRemind.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkHasApptPleaseRemind.Value = selectedchkHasApptPleaseRemind;
        string selectedchkReturnVisit1 = String.Join(",", chkReturnVisit1.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkReturnVisit1.Value = selectedchkReturnVisit1;
        string selectedchkVisitType = String.Join(",", chkVisitType.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkVisitType.Value = selectedchkVisitType;

        string selectedchkReturnVisit2 = String.Join(",", chkReturnVisit2.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkReturnVisit2.Value = selectedchkReturnVisit2;
        string selectedchkVisitType2 = String.Join(",", chkVisitType2.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkVisitType2.Value = selectedchkVisitType2;

        string selectedchkReturnVisit3 = String.Join(",", chkReturnVisit3.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkReturnVisit3.Value = selectedchkReturnVisit3;
        string selectedchkVisitType3 = String.Join(",", chkVisitType3.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(r => r.Selected).Select(r => r.Text));
        hdnchkVisitType3.Value = selectedchkVisitType3;
        # endregion

        #region sql
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=dbPainTrax_PPC_Live;uid=sa;pwd=Annie123;");
        SqlCommand cmd = new SqlCommand
            ("insert into tblexitsheet(Copay,Location,IFPP,ExitDate,PatientName,DOB,ExitCase,Insurance,Verified,Fuon," +
  "OtherImaging,EECTLR,CESI,TESI,LESI,LTFE,CarpTun,TRFA,LRFA,CRFA,KneeJelInj,SCSTrail,LMBB,TMBB,CMBB,Other," +
  "MUN,RequestImaging,ReqOtherImaging,ImagingLoc,ReqProc,ReqProcCESI,ReqProcTESI,ReqProcLESI,ReqProcLTFE," +
  "ReqProcMBB,ReqProcTRFA,ReqProcLRFA,ReqProcCRFA,ReqProcKneeJelInj,ReqProcOther,ReqTherapy,RequestBrace,SeeForm,InHouseholdPerformed," +
  "ProcedureDate,ReqProcedure,ReqLocation,ProcedureDate1,ReqProcedure1,ReqLocation1,TLC1,InHouseProcDateLoc,InHouseProcDateLoc1,InHouseProcDateLoc2," +
  "InHouseProcedure,InHouseProcedure1,InHouseProcedure2,OnAC,PthxDM,Copay1,HasFUApptPlsRem,ReturnVisit1," +
  "ReturnVisitOther1,ReturnVisitDate1,ReturnVisitTime1,ReturnVisitLoc1,ReturnVisitType1,ReturnVisitORProc1,ReturnVisitINnHouseProc1,ReturnVisitEMGReview1," +
  "ReturnVisit2,ReturnVisitOther2,ReturnVisitDate2,ReturnVisitTime2,ReturnVisitLoc2,ReturnVisitType2,ReturnVisitORProc2,ReturnVisitINnHouseProc2,ReturnVisitEMGReview2," +
  "ReturnVisit3,ReturnVisitOther3,ReturnVisitDate3,ReturnVisitTime3,ReturnVisitLoc3,ReturnVisitType3,ReturnVisitORProc3,ReturnVisitINnHouseProc3," +
  "ReturnVisitEMGReview3,UTOX,ScriptsGiven,RecordsRequest,FormCompleted,SendLegalUpdate,ScriptsScanned,CompltedBy,Collected," +
  "ReferredBy,TherapyReferral,LegalReferral,ImagingReferral,Orthopedics,Spine,Podiatry,EmgReferral,Comments,PatientIE_ID,PatientFU_ID,PrintStatus)values(@Copay," +
  "@Location,@IFPP,@ExitDate,@PatientName,@DOB," +
  "@ExitCase,@Insurance,@Verified,@Fuon,@OtherImaging,@EECTLR,@CESI,@TESI,@LESI,@LTFE,@CarpTun,@TRFA,@LRFA,@CRFA,@KneeJelInj,@SCSTrail," +
  "@LMBB,@TMBB,@CMBB,@Other,@MUN,@RequestImaging,@ReqOtherImaging,@ImagingLoc,@ReqProc,@ReqProcCESI,@ReqProcTESI,@ReqProcLESI,@ReqProcLTFE,@ReqProcMBB," +
  "@ReqProcTRFA,@ReqProcLRFA,@ReqProcCRFA,@ReqProcKneeJelInj,@ReqProcOther,@ReqTherapy,@RequestBrace,@SeeForm,@InHouseholdPerformed," +
  "@ProcedureDate,@ReqProcedure,@ReqLocation,@ProcedureDate1,@ReqProcedure1,@ReqLocation1,@TLC1,@InHouseProcDateLoc,@InHouseProcDateLoc1," +
  "@InHouseProcDateLoc2,@InHouseProcedure,@InHouseProcedure1,@InHouseProcedure2,@OnAC,@PthxDM,@Copay1,@HasFUApptPlsRem,@ReturnVisit1," +
  "@ReturnVisitOther1,@ReturnVisitDate1,@ReturnVisitTime1,@ReturnVisitLoc1,@ReturnVisitType1,@ReturnVisitORProc1,@ReturnVisitINnHouseProc1,@ReturnVisitEMGReview1," +
  "@ReturnVisit2,@ReturnVisitOther2,@ReturnVisitDate2,@ReturnVisitTime2,@ReturnVisitLoc2,@ReturnVisitType2,@ReturnVisitORProc2,@ReturnVisitINnHouseProc2," +
  "@ReturnVisitEMGReview2,@ReturnVisit3,@ReturnVisitOther3,@ReturnVisitDate3,@ReturnVisitTime3,@ReturnVisitLoc3,@ReturnVisitType3,@ReturnVisitORProc3," +
  "@ReturnVisitINnHouseProc3,@ReturnVisitEMGReview3,@UTOX,@ScriptsGiven,@RecordsRequest,@FormCompleted,@SendLegalUpdate,@ScriptsScanned,@CompltedBy,@Collected," +
  "@ReferredBy,@TherapyReferral,@LegalReferral,@ImagingReferral,@Orthopedics,@Spine,@Podiatry,@EmgReferral,@Comments,@PatientIE_ID,@PatientFU_ID,@PrintStatus)", con);
        #endregion
        # region pageone

        cmd.Parameters.AddWithValue("Copay", Convert.ToString(txtCopay.Text));
        cmd.Parameters.AddWithValue("Location", Convert.ToString(txtLoc.Text));
        cmd.Parameters.AddWithValue("IFPP", Convert.ToString(hdnIfpp.Value));

        //DateTime myDate = DateTime.Parse(txtDate.Text);

        //cmd.Parameters.AddWithValue("ExitDate", myDate);
        cmd.Parameters.AddWithValue("ExitDate", Convert.ToString(txtDate.Text));

        cmd.Parameters.AddWithValue("PatientName", Convert.ToString(txtPatientName.Text));
        //DateTime dob = DateTime.Parse(txtDOB.Text);
        //cmd.Parameters.AddWithValue("DOB", dob);
        cmd.Parameters.AddWithValue("DOB", Convert.ToString(txtDOB.Text));
        if (rdlCase.SelectedIndex == -1)
        {

            cmd.Parameters.AddWithValue("ExitCase", Convert.ToString(rdlCase.SelectedValue = "none"));
        }
        else
        {
            cmd.Parameters.AddWithValue("ExitCase", Convert.ToString(rdlCase.SelectedItem.Text));
        }

        cmd.Parameters.AddWithValue("Insurance", Convert.ToString(hdnInsurance.Value));
        if (rdlVerified.SelectedIndex == -1)
        {

            cmd.Parameters.AddWithValue("Verified", Convert.ToString(rdlVerified.SelectedValue = "none"));
        }
        else
        {
            cmd.Parameters.AddWithValue("Verified", Convert.ToString(rdlVerified.SelectedItem.Text));
        }

        cmd.Parameters.AddWithValue("Fuon", Convert.ToString(hdnFUON.Value));
        cmd.Parameters.AddWithValue("OtherImaging", Convert.ToString(hdnOtherImaging.Value));
        cmd.Parameters.AddWithValue("EECTLR", Convert.ToString(hdnEMGU.Value));
        cmd.Parameters.AddWithValue("CESI", Convert.ToString(txtCESI.Text));
        cmd.Parameters.AddWithValue("TESI", Convert.ToString(txtTESI.Text));
        cmd.Parameters.AddWithValue("LESI", Convert.ToString(txtLESI.Text));
        cmd.Parameters.AddWithValue("LTFE", Convert.ToString(txtLTFE.Text));
        cmd.Parameters.AddWithValue("CarpTun", Convert.ToString(txtCarpTunInj.Text));
        cmd.Parameters.AddWithValue("TRFA", Convert.ToString(txtTRFA.Text));
        cmd.Parameters.AddWithValue("LRFA", Convert.ToString(txtLRFA.Text));
        cmd.Parameters.AddWithValue("CRFA", Convert.ToString(txtCRFA.Text));
        cmd.Parameters.AddWithValue("KneeJelInj", Convert.ToString(txtKneeGelInj.Text));
        cmd.Parameters.AddWithValue("SCSTrail", Convert.ToString(txtSCSTrail.Text));
        cmd.Parameters.AddWithValue("LMBB", Convert.ToString(txtLMBB.Text));
        cmd.Parameters.AddWithValue("TMBB", Convert.ToString(txtTMBB.Text));
        cmd.Parameters.AddWithValue("CMBB", Convert.ToString(txtCMBB.Text));
        cmd.Parameters.AddWithValue("Other", Convert.ToString(txtOther1.Text));
        cmd.Parameters.AddWithValue("MUN", Convert.ToString(hdnOther1.Value));
        cmd.Parameters.AddWithValue("RequestImaging", Convert.ToString(hdnRequestImaging.Value));
        cmd.Parameters.AddWithValue("ReqOtherImaging", Convert.ToString(txtOtherImaging1.Text));
        cmd.Parameters.AddWithValue("ImagingLoc", Convert.ToString(txtImagingLoc.Text));
        cmd.Parameters.AddWithValue("ReqProc", Convert.ToString(hdnchkReqProc.Value));

        cmd.Parameters.AddWithValue("ReqProcCESI", Convert.ToString(txtCESI1.Text));
        cmd.Parameters.AddWithValue("ReqProcTESI", Convert.ToString(txtTESI1.Text));
        cmd.Parameters.AddWithValue("ReqProcLESI", Convert.ToString(txtLESI1.Text));
        cmd.Parameters.AddWithValue("ReqProcLTFE", Convert.ToString(txtLTFE1.Text));
        cmd.Parameters.AddWithValue("ReqProcMBB", Convert.ToString(txtMBB.Text));
        cmd.Parameters.AddWithValue("ReqProcTRFA", Convert.ToString(txtTRFA1.Text));
        cmd.Parameters.AddWithValue("ReqProcLRFA", Convert.ToString(txtLRFA1.Text));
        cmd.Parameters.AddWithValue("ReqProcCRFA", Convert.ToString(txtCRFA1.Text));
        cmd.Parameters.AddWithValue("ReqProcKneeJelInj", Convert.ToString(txtKneeGelInj1.Text));
        cmd.Parameters.AddWithValue("ReqProcOther", Convert.ToString(txtOther2.Text));
        cmd.Parameters.AddWithValue("ReqTherapy", Convert.ToString(hdnchkOther2.Value));
        cmd.Parameters.AddWithValue("RequestBrace", Convert.ToString(hdnchkReqBrace.Value));
        cmd.Parameters.AddWithValue("SeeForm", Convert.ToString(hdnchkSeeForm.Value));
        cmd.Parameters.AddWithValue("InHouseholdPerformed", Convert.ToString(txtInHouseProcPerformed.Text));
        cmd.Parameters.AddWithValue("ProcedureDate", Convert.ToString(hdnchkTBD.Value));
        cmd.Parameters.AddWithValue("ReqProcedure", Convert.ToString(txtProcedure.Text));
        cmd.Parameters.AddWithValue("ReqLocation", Convert.ToString(hdnchkLocationNew.Value));
        cmd.Parameters.AddWithValue("TLC", Convert.ToString(hdnchkTLC.Value));
        cmd.Parameters.AddWithValue("ProcedureDate1", Convert.ToString(hdnchkTBDNew1.Value));
        cmd.Parameters.AddWithValue("ReqProcedure1", Convert.ToString(txtProcNew1.Text));
        cmd.Parameters.AddWithValue("ReqLocation1", Convert.ToString(hdnchkLOcNew1.Value));
        cmd.Parameters.AddWithValue("TLC1", Convert.ToString(hdnchkTLCNEW1.Value));

        cmd.Parameters.AddWithValue("InHouseProcDateLoc", Convert.ToString(txtHouseProcDate.Text));
        cmd.Parameters.AddWithValue("InHouseProcDateLoc1", Convert.ToString(txtHouseProcDate1.Text));
        cmd.Parameters.AddWithValue("InHouseProcDateLoc2", Convert.ToString(txtHouseProcDate2.Text));
        cmd.Parameters.AddWithValue("InHouseProcedure", Convert.ToString(txtInHouseProc.Text));
        cmd.Parameters.AddWithValue("InHouseProcedure1", Convert.ToString(txtInHouseProc1.Text));
        cmd.Parameters.AddWithValue("InHouseProcedure2", Convert.ToString(txtInHouseProc2.Text));

        cmd.Parameters.AddWithValue("OnAC", Convert.ToString(hdnchkOnAc.Value));
        cmd.Parameters.AddWithValue("PthxDM", Convert.ToString(hdnchkPthxDM.Value));

        #endregion
        # region page2
        #region review
        cmd.Parameters.AddWithValue("Copay1", Convert.ToString(txtCopay1.Text));
        cmd.Parameters.AddWithValue("HasFUApptPlsRem", Convert.ToString(hdnchkHasApptPleaseRemind.Value));
        cmd.Parameters.AddWithValue("ReturnVisit1", Convert.ToString(hdnchkReturnVisit1.Value));
        cmd.Parameters.AddWithValue("ReturnVisitOther1", Convert.ToString(txtOtherreview1.Text));
        cmd.Parameters.AddWithValue("ReturnVisitDate1", Convert.ToString(txtDateReview1.Text));
        cmd.Parameters.AddWithValue("ReturnVisitTime1", Convert.ToString(txtTimeReview1.Text));
        cmd.Parameters.AddWithValue("ReturnVisitLoc1", Convert.ToString(txtLocReview1.Text));
        cmd.Parameters.AddWithValue("ReturnVisitType1", Convert.ToString(hdnchkVisitType.Value));
        cmd.Parameters.AddWithValue("ReturnVisitORProc1", Convert.ToString(txtOrproc.Text));
        cmd.Parameters.AddWithValue("ReturnVisitINnHouseProc1", Convert.ToString(txtInhouse.Text));
        cmd.Parameters.AddWithValue("ReturnVisitEMGReview1", Convert.ToString(txtEmgreview.Text));

        cmd.Parameters.AddWithValue("ReturnVisit2", Convert.ToString(hdnchkReturnVisit2.Value));
        cmd.Parameters.AddWithValue("ReturnVisitOther2", Convert.ToString(txtOtherreview2.Text));
        cmd.Parameters.AddWithValue("ReturnVisitDate2", Convert.ToString(txtDateReview2.Text));
        cmd.Parameters.AddWithValue("ReturnVisitTime2", Convert.ToString(txtTimeReview2.Text));
        cmd.Parameters.AddWithValue("ReturnVisitLoc2", Convert.ToString(txtLocReview2.Text));
        cmd.Parameters.AddWithValue("ReturnVisitType2", Convert.ToString(hdnchkVisitType2.Value));
        cmd.Parameters.AddWithValue("ReturnVisitORProc2", Convert.ToString(txtOrproc2.Text));
        cmd.Parameters.AddWithValue("ReturnVisitINnHouseProc2", Convert.ToString(txtInhouse2.Text));
        cmd.Parameters.AddWithValue("ReturnVisitEMGReview2", Convert.ToString(txtEmgreview2.Text));

        cmd.Parameters.AddWithValue("ReturnVisit3", Convert.ToString(hdnchkReturnVisit3.Value));
        cmd.Parameters.AddWithValue("ReturnVisitOther3", Convert.ToString(txtOtherreview3.Text));
        cmd.Parameters.AddWithValue("ReturnVisitDate3", Convert.ToString(txtDateReview3.Text));
        cmd.Parameters.AddWithValue("ReturnVisitTime3", Convert.ToString(txtTimeReview3.Text));
        cmd.Parameters.AddWithValue("ReturnVisitLoc3", Convert.ToString(txtLocReview3.Text));
        cmd.Parameters.AddWithValue("ReturnVisitType3", Convert.ToString(hdnchkVisitType3.Value));
        cmd.Parameters.AddWithValue("ReturnVisitORProc3", Convert.ToString(txtOrproc3.Text));
        cmd.Parameters.AddWithValue("ReturnVisitINnHouseProc3", Convert.ToString(txtInhouse3.Text));
        cmd.Parameters.AddWithValue("ReturnVisitEMGReview3", Convert.ToString(txtEmgreview3.Text));
        #endregion
        #region  page2comp
        if (rdlUtox.SelectedIndex == -1)
        {

            cmd.Parameters.AddWithValue("UTOX", Convert.ToString(rdlUtox.SelectedValue = "none"));
        }
        else
        {
            cmd.Parameters.AddWithValue("UTOX", Convert.ToString(rdlUtox.SelectedItem.Text));
        }


        if (rdlScriptsGiven.SelectedIndex == -1)
        {

            cmd.Parameters.AddWithValue("ScriptsGiven", Convert.ToString(rdlScriptsGiven.SelectedValue = "none"));
        }
        else
        {
            cmd.Parameters.AddWithValue("ScriptsGiven", Convert.ToString(rdlScriptsGiven.SelectedItem.Text));
        }


        cmd.Parameters.AddWithValue("RecordsRequest", Convert.ToString(txtRecordRequest.Text));
        if (rdlFormCompleted.SelectedIndex == -1)
        {

            cmd.Parameters.AddWithValue("FormCompleted", Convert.ToString(rdlFormCompleted.SelectedValue = "none"));
        }
        else
        {
            cmd.Parameters.AddWithValue("FormCompleted", Convert.ToString(rdlFormCompleted.SelectedItem.Text));
        }


        if (rdlSendLegalUpdate.SelectedIndex == -1)
        {

            cmd.Parameters.AddWithValue("SendLegalUpdate", Convert.ToString(rdlSendLegalUpdate.SelectedValue = "none"));
        }
        else
        {
            cmd.Parameters.AddWithValue("SendLegalUpdate", Convert.ToString(rdlSendLegalUpdate.SelectedItem.Text));
        }



        if (rdlScriptscanned.SelectedIndex == -1)
        {

            cmd.Parameters.AddWithValue("ScriptsScanned", Convert.ToString(rdlScriptscanned.SelectedValue = "none"));
        }
        else
        {
            cmd.Parameters.AddWithValue("ScriptsScanned", Convert.ToString(rdlScriptscanned.SelectedItem.Text));
        }


        cmd.Parameters.AddWithValue("CompltedBy", Convert.ToString(txtCompletedBy.Text));
        cmd.Parameters.AddWithValue("Collected", Convert.ToString(txtCollected.Text));
        #endregion
        cmd.Parameters.AddWithValue("ReferredBy", Convert.ToString(txtReferredBy.Text));
        cmd.Parameters.AddWithValue("TherapyReferral", Convert.ToString(txtTherapyReferral.Text));
        cmd.Parameters.AddWithValue("LegalReferral", Convert.ToString(txtLegalReferral.Text));
        cmd.Parameters.AddWithValue("ImagingReferral", Convert.ToString(txtImagingReferral.Text));
        cmd.Parameters.AddWithValue("Orthopedics", Convert.ToString(txtOrthopedics.Text));
        cmd.Parameters.AddWithValue("Spine", Convert.ToString(txtSpine.Text));
        cmd.Parameters.AddWithValue("Podiatry", Convert.ToString(txtPodiatry.Text));
        cmd.Parameters.AddWithValue("EmgReferral", Convert.ToString(txtEmgReferral.Text));
        cmd.Parameters.AddWithValue("Comments", Convert.ToString(txtComments.Text));
        cmd.Parameters.AddWithValue("PatientIE_ID", Convert.ToString(ieid));
        cmd.Parameters.AddWithValue("PatientFU_ID", Convert.ToString(iefuid));
        cmd.Parameters.AddWithValue("PrintStatus", "");
        # endregion

        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();

        ////using (SqlConnection myConnection = new SqlConnection(@"Data Source=104.171.116.58,49170;Initial Catalog=dbPainTrax_JL_NV;uid=sa;pwd=Annie123;")) ;
        ////{
        ////    using (SqlCommand comnd = new SqlCommand("sp_getPatientDetails_ExitForm", myConnection))
        ////    {

        //    }
        //}


        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            Document document = new Document(PageSize.A4, 10, 10, 10, 10);

            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            PdfPTable table1 = new PdfPTable(3);
            #region row1
            PdfPCell cells = new PdfPCell(new Phrase("Copay:" + txtCopay.Text + ""));
            cells.Colspan = 1;
            cells.BorderWidthTop = 0;
            cells.BorderWidthRight = 0;
            cells.BorderWidthLeft = 0;
            cells.BorderWidthBottom = 0;
            cells.HorizontalAlignment = Element.ALIGN_LEFT;
            table1.AddCell(cells);

            cells = new PdfPCell(new Phrase("Visit End Form"));
            cells.Colspan = 2;
            cells.BorderWidthTop = 0;
            cells.BorderWidthRight = 0;
            cells.BorderWidthLeft = 0;
            cells.BorderWidthBottom = 0;
            cells.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cells);
            #endregion
            # region row2
            cells = new PdfPCell(new Phrase("Location:" + txtLoc.Text + ""));
            cells.BorderWidthRight = 0;
            cells.BorderWidthBottom = 0;
            table1.AddCell(cells);
            cells = new PdfPCell();
            cells.BorderWidthLeft = 0;
            Font zapfdingbats = new Font(Font.ZAPFDINGBATS);
            Phrase pharsenew = new Phrase();

            for (int j = 0; j < chkIFPP.Items.Count; j++)
            {
                if (chkIFPP.Items[j].Selected == true)
                {
                    pharsenew.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew.Add(" " + chkIFPP.Items[j].Text + "   ");

                }
                else
                {
                    pharsenew.Add("X" + " ");
                    //pharsenew.Add(new Chunk("X", zapfdingbats));
                    pharsenew.Add(chkIFPP.Items[j].Text + "   ");
                }
            }
            cells.Phrase = pharsenew;
            cells.BorderWidthBottom = 0;
            table1.AddCell(cells);

            cells = new PdfPCell(new Phrase("Date:" + txtDate.Text));
            cells.HorizontalAlignment = Element.ALIGN_RIGHT;
            table1.AddCell(cells);
            #endregion

            #region row3
            cells = new PdfPCell(new Phrase("PatientName:" + txtPatientName.Text));
            cells.BorderWidthTop = 0;
            cells.BorderWidthRight = 0;
            table1.AddCell(cells);
            cells = new PdfPCell(new Phrase("DOB:" + txtDOB.Text));
            cells.BorderWidthTop = 0;
            cells.BorderWidthLeft = 0;
            table1.AddCell(cells);
            cells = new PdfPCell();
            Phrase pharsenew1 = new Phrase();
            if (rdlCase.SelectedItem != null && rdlCase.SelectedItem.Value.Contains("Yes"))
            {
                pharsenew1.Add(" Case:");
                pharsenew1.Add(" Yes");
            }
            else if (rdlCase.SelectedItem == null)
            {
                pharsenew1.Add(" Case:");
                pharsenew1.Add("");
            }
            else
            {
                pharsenew1.Add(" Case:");
                pharsenew1.Add("No");
            }

            cells.Phrase = pharsenew1;
            cells.HorizontalAlignment = Element.ALIGN_RIGHT;
            table1.AddCell(cells);
            #endregion
            #region row4
            cells = new PdfPCell();
            cells.Colspan = 3;
            //cells.BorderWidthRight = 1;
            //cells.BorderWidthLeft = 1;
            // cells.Border = 0;
            //Font zapfdingbats1 = new Font(Font.ZAPFDINGBATS);
            Phrase pharsenew2 = new Phrase();
            pharsenew2.Add("Insurance:");
            for (int k = 0; k < chkInsurance.Items.Count; k++)
            {
                if (chkInsurance.Items[k].Selected == true)
                {
                    pharsenew2.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew2.Add(chkInsurance.Items[k].Text + "    ");
                }
                else
                {
                    pharsenew2.Add("X" + " ");
                    // pharsenew2.Add(new Chunk("X", zapfdingbats));
                    pharsenew2.Add(chkInsurance.Items[k].Text + "    ");
                }
            }
            cells.Phrase = pharsenew2;
            cells.BorderWidthBottom = 0;
            table1.AddCell(cells);
            #endregion
            #region row5
            cells = new PdfPCell();
            cells.Colspan = 3;
            cells.HorizontalAlignment = Element.ALIGN_LEFT;
            Phrase pharsenew3 = new Phrase();
            if (rdlVerified.SelectedItem != null && rdlVerified.SelectedItem.Value.Contains("Yes"))
            {
                pharsenew3.Add(" Verified:");
                pharsenew3.Add(" Yes");
            }
            else if (rdlVerified.SelectedItem == null)
            {
                pharsenew3.Add(" Verified:");
                pharsenew3.Add("");
            }
            else
            {
                pharsenew3.Add(" Verified:");
                pharsenew3.Add("No");
            }
            cells.BorderWidthTop = 0;
            cells.Phrase = pharsenew3;

            table1.AddCell(cells);
            #endregion
            #region row31
            cells = new PdfPCell();
            cells.Colspan = 3;

            //Font zapfdingbats1 = new Font(Font.ZAPFDINGBATS);
            Phrase pharsenew4 = new Phrase();
            pharsenew4.Add("F/u on:");
            for (int l = 0; l < chkfuon.Items.Count; l++)
            {
                if (chkfuon.Items[l].Selected == true)
                {
                    pharsenew4.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew4.Add(chkfuon.Items[l].Text + "    ");
                }
                else
                {
                    pharsenew4.Add("X" + " ");
                    //pharsenew4.Add(new Chunk("X", zapfdingbats));
                    pharsenew4.Add(chkfuon.Items[l].Text + "    ");
                }
            }
            cells.Phrase = pharsenew4;
            cells.BorderWidthBottom = 1;
            table1.AddCell(cells);
            #endregion
            #region row6
            cells = new PdfPCell(new Phrase("Other Imaging:"));
            cells.BorderWidthBottom = 0;
            cells.BorderWidthRight = 0;
            cells.BorderWidthTop = 0;
            cells.HorizontalAlignment = Element.ALIGN_LEFT;
            table1.AddCell(cells);
            cells = new PdfPCell();
            cells.Colspan = 2;
            Phrase pharsenew5 = new Phrase();

            for (int m = 0; m < chkOtherImaging.Items.Count; m++)
            {
                if (chkOtherImaging.Items[m].Selected == true)
                {
                    pharsenew5.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew5.Add(chkOtherImaging.Items[m].Text + "    ");

                }
                else
                {
                    pharsenew5.Add("X" + " ");
                    //pharsenew5.Add(new Chunk("X", zapfdingbats));
                    pharsenew5.Add(chkOtherImaging.Items[m].Text + "    ");
                }
            }
            cells.Phrase = pharsenew5;
            cells.BorderWidthLeft = 0;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);
            #endregion
            #region row7
            cells = new PdfPCell();
            cells.Colspan = 3;
            Phrase pharsenew7 = new Phrase();

            for (int n = 0; n < chkEMGU.Items.Count; n++)
            {
                if (chkEMGU.Items[n].Selected == true)
                {
                    pharsenew7.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew7.Add(chkEMGU.Items[n].Text + "    ");

                }
                else
                {
                    pharsenew7.Add("X" + " ");
                    //pharsenew7.Add(new Chunk("X", zapfdingbats));
                    pharsenew7.Add(chkEMGU.Items[n].Text + "    ");

                }
            }
            cells.Phrase = pharsenew7;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);
            #endregion

            #region row8
            cells = new PdfPCell();
            cells.Colspan = 3;
            Phrase phrasenew8 = new Phrase("CESI: " + txtCESI.Text + "  ");
            //cells.HorizontalAlignment = Element.ALIGN_LEFT;

            phrasenew8.Add("TESI: " + txtTESI.Text + "    ");
            phrasenew8.Add("LESI: " + txtLESI.Text + "    ");
            phrasenew8.Add("LTFE: " + txtLTFE.Text + "    ");
            phrasenew8.Add("CarpTun Inj: " + txtCarpTunInj.Text + "    ");
            cells.Phrase = phrasenew8;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);
            #endregion

            #region row9
            cells = new PdfPCell();
            cells.Colspan = 3;
            Phrase phrasenew9 = new Phrase("TRFA: " + txtTRFA.Text + "  ");
            //cells.HorizontalAlignment = Element.ALIGN_LEFT;

            phrasenew9.Add("LRFA: " + txtLRFA.Text + "    ");
            phrasenew9.Add("CRFA: " + txtCRFA.Text + "    ");
            phrasenew9.Add("Knee Gel Inj: " + txtKneeGelInj.Text + "    ");
            phrasenew9.Add("SCS Trail: " + txtSCSTrail.Text + "    ");
            cells.Phrase = phrasenew9;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);

            #endregion
            #region row10


            cells = new PdfPCell();
            cells.Colspan = 3;
            cells.HorizontalAlignment = Element.ALIGN_RIGHT;
            Phrase phrasenew10 = new Phrase("LMBB: " + txtLMBB.Text + "  ");


            phrasenew10.Add("TMBB: " + txtTMBB.Text + "    ");
            phrasenew10.Add("CMBB: " + txtCMBB.Text + "    ");
            cells.Phrase = phrasenew10;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);

            #endregion
            #region row11
            cells = new PdfPCell(new Phrase("Other: " + txtOther1.Text + "  "));
            cells.Colspan = 1;
            cells.BorderWidthLeft = 1;
            cells.BorderWidthRight = 0;
            cells.BorderWidthBottom = 1;
            cells.BorderWidthTop = 0;
            cells.HorizontalAlignment = Element.ALIGN_LEFT;
            table1.AddCell(cells);

            cells = new PdfPCell();
            cells.Colspan = 2;
            Phrase pharsenew11 = new Phrase();

            for (int n = 0; n < chkOther1.Items.Count; n++)
            {
                if (chkOther1.Items[n].Selected == true)
                {
                    pharsenew11.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew11.Add(chkOther1.Items[n].Text + "    ");

                }
                else
                {
                    pharsenew11.Add("X" + " ");
                    //pharsenew11.Add(new Chunk("X", zapfdingbats));
                    pharsenew11.Add(chkOther1.Items[n].Text + "    ");

                }
            }
            cells.Phrase = pharsenew11;
            cells.BorderWidthLeft = 0;
            cells.BorderWidthBottom = 1;
            cells.BorderWidthTop = 0;
            cells.HorizontalAlignment = Element.ALIGN_RIGHT;
            table1.AddCell(cells);

            #endregion
            #region row12
            cells = new PdfPCell();
            cells.Colspan = 3;
            Phrase pharsenew12 = new Phrase();
            pharsenew12.Add("Request Imaging:");
            for (int o = 0; o < chkRequestImaging.Items.Count; o++)
            {
                if (chkRequestImaging.Items[o].Selected == true)
                {
                    pharsenew12.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew12.Add(chkRequestImaging.Items[o].Text + "    ");
                }
                else
                {
                    pharsenew12.Add("X" + " ");
                    //pharsenew12.Add(new Chunk("X", zapfdingbats));
                    pharsenew12.Add(chkRequestImaging.Items[o].Text + "    ");
                }
            }
            cells.Phrase = pharsenew12;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);
            #endregion

            #region row13
            cells = new PdfPCell(new Phrase("Other Imaging: " + txtOtherImaging1.Text + "  "));
            cells.Colspan = 1;

            cells.BorderWidthRight = 0;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            cells.HorizontalAlignment = Element.ALIGN_LEFT;
            table1.AddCell(cells);

            cells = new PdfPCell(new Phrase("Imaging Loc: " + txtImagingLoc.Text + "  "));
            cells.Colspan = 2;
            cells.BorderWidthLeft = 0;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            cells.HorizontalAlignment = Element.ALIGN_RIGHT;
            table1.AddCell(cells);

            #endregion
            #region row14
            cells = new PdfPCell();
            cells.Colspan = 3;
            Phrase pharsenew14 = new Phrase();
            pharsenew14.Add("Req Proc:");
            for (int p = 0; p < chkReqProc.Items.Count; p++)
            {
                if (chkReqProc.Items[p].Selected == true)
                {
                    pharsenew14.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew14.Add(chkReqProc.Items[p].Text + "    ");
                }
                else
                {
                    pharsenew14.Add("X" + " ");
                    // pharsenew14.Add(new Chunk("X", zapfdingbats));
                    pharsenew14.Add(chkReqProc.Items[p].Text + "    ");
                }
            }
            cells.Phrase = pharsenew14;
            cells.BorderWidthBottom = 0;
            //cells.BorderWidthTop = 0;
            table1.AddCell(cells);
            #endregion
            #region row15
            cells = new PdfPCell();
            cells.Colspan = 3;
            Phrase phrasenew15 = new Phrase("CESI: " + txtCESI1.Text + "  ");
            //cells.HorizontalAlignment = Element.ALIGN_LEFT;

            phrasenew15.Add("TESI: " + txtTESI1.Text + "    ");
            phrasenew15.Add("LESI: " + txtLESI1.Text + "    ");
            phrasenew15.Add("LTFE: " + txtLTFE1.Text + "    ");
            phrasenew15.Add("MBB: " + txtMBB.Text + "    ");
            cells.Phrase = phrasenew15;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);
            #endregion

            #region row16
            cells = new PdfPCell();
            cells.Colspan = 3;
            Phrase phrasenew16 = new Phrase("TRFA: " + txtTRFA1.Text + "  ");
            //cells.HorizontalAlignment = Element.ALIGN_LEFT;

            phrasenew16.Add("LRFA: " + txtLRFA1.Text + "    ");
            phrasenew16.Add("CRFA: " + txtCRFA1.Text + "    ");
            phrasenew16.Add("Knee Gel Inj: " + txtKneeGelInj1.Text + "    ");
            cells.Phrase = phrasenew16;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);
            #endregion
            #region row17
            cells = new PdfPCell(new Phrase("Other: " + txtOther2.Text + "  "));
            cells.Colspan = 1;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            cells.BorderWidthRight = 0;
            cells.HorizontalAlignment = Element.ALIGN_LEFT;
            table1.AddCell(cells);
            cells = new PdfPCell();
            cells.Colspan = 2;
            Phrase pharsenew17 = new Phrase();

            for (int a = 0; a < chkOther2.Items.Count; a++)
            {
                if (chkOther2.Items[a].Selected == true)
                {
                    pharsenew17.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew17.Add(chkOther2.Items[a].Text + "    ");

                }
                else
                {
                    pharsenew17.Add("X" + " ");
                    //pharsenew17.Add(new Chunk("X", zapfdingbats));
                    pharsenew17.Add(chkOther2.Items[a].Text + "    ");
                }
            }
            cells.Phrase = pharsenew17;
            cells.BorderWidthLeft = 0;
            cells.BorderWidthTop = 0;
            cells.BorderWidthBottom = 0;
            table1.AddCell(cells);
            #endregion
            #region row18
            cells = new PdfPCell();
            cells.Colspan = 3;


            Phrase pharsenew18 = new Phrase();
            pharsenew18.Add("Request Brace:" + "    ");
            for (int b = 0; b < chkReqBrace.Items.Count; b++)
            {
                if (chkReqBrace.Items[b].Selected == true)
                {
                    pharsenew18.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew18.Add(chkReqBrace.Items[b].Text + "    ");
                }
                else
                {
                    pharsenew18.Add("X" + " ");
                    //pharsenew18.Add(new Chunk("X", zapfdingbats));
                    pharsenew18.Add(chkReqBrace.Items[b].Text + "    ");
                }
            }
            cells.Phrase = pharsenew18;
            //cells.BorderWidthBottom = 1;
            table1.AddCell(cells);
            #endregion

            #region row19
            cells = new PdfPCell();
            cells.Colspan = 3;


            Phrase pharsenew19 = new Phrase();
            pharsenew19.Add("See Form:" + "    ");
            for (int b = 0; b < chkSeeForm.Items.Count; b++)
            {
                if (chkSeeForm.Items[b].Selected == true)
                {
                    pharsenew19.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew19.Add(chkSeeForm.Items[b].Text + "    ");
                }
                else
                {
                    pharsenew19.Add("X" + " ");
                    //pharsenew19.Add(new Chunk("X", zapfdingbats));
                    pharsenew19.Add(chkSeeForm.Items[b].Text + "    ");
                }
            }
            cells.Phrase = pharsenew19;
            cells.BorderWidthBottom = 1;
            table1.AddCell(cells);
            #endregion

            #region row20
            cells = new PdfPCell(new Phrase("In House Proc Performed: " + txtInHouseProcPerformed.Text + "   "));
            cells.Colspan = 3;
            cells.HorizontalAlignment = Element.ALIGN_LEFT;
            cells.BorderWidthBottom = 1;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);
            #endregion

            #region row21
            cells = new PdfPCell(new Phrase("Procedure Date: " + "    "));
            cells.HorizontalAlignment = Element.ALIGN_LEFT;
            cells.BorderWidthTop = 0;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthRight = 0;
            table1.AddCell(cells);
            cells = new PdfPCell();
            cells.BorderWidthTop = 0;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthRight = 0;
            cells.BorderWidthLeft = 0;
            Phrase pharsenew20 = new Phrase();
            if (chkTBD.SelectedItem != null && chkTBD.SelectedItem.Value.Contains("TBD"))
            {
                pharsenew20.Add(new Chunk("\u0033", zapfdingbats));
                pharsenew20.Add("TBD" + "    ");
            }
            else
            {
                pharsenew20.Add("X" + " ");
                // pharsenew20.Add(new Chunk("X", zapfdingbats));
                pharsenew20.Add("TBD" + "    ");
            }
            cells.Phrase = pharsenew20;
            table1.AddCell(cells);
            cells = new PdfPCell(new Phrase("Procedure:" + txtProcedure.Text + "     "));

            cells.BorderWidthLeft = 0;
            cells.BorderWidthTop = 0;
            cells.BorderWidthBottom = 0;
            table1.AddCell(cells);
            #endregion
            #region row22
            cells = new PdfPCell();
            cells.Colspan = 3;


            Phrase pharsenew21 = new Phrase();
            pharsenew21.Add("Location:" + "    ");
            for (int c = 0; c < chkLocationNew.Items.Count; c++)
            {
                if (chkLocationNew.Items[c].Selected == true)
                {
                    pharsenew21.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew21.Add(chkLocationNew.Items[c].Text + "    ");
                }
                else
                {
                    pharsenew21.Add("X" + " ");
                    //pharsenew21.Add(new Chunk("X", zapfdingbats));
                    pharsenew21.Add(chkLocationNew.Items[c].Text + "    ");
                }
            }
            cells.Phrase = pharsenew21;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);
            #endregion

            #region row23
            cells = new PdfPCell();
            cells.Colspan = 3;


            Phrase pharsenew22 = new Phrase();
            for (int d = 0; d < chkTLC.Items.Count; d++)
            {
                if (chkTLC.Items[d].Selected == true)
                {
                    pharsenew22.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew22.Add(chkTLC.Items[d].Text + "    ");
                }
                else
                {
                    pharsenew22.Add("X" + " ");
                    // pharsenew22.Add(new Chunk("X", zapfdingbats));
                    pharsenew22.Add(chkTLC.Items[d].Text + "    ");
                }
            }
            cells.Phrase = pharsenew22;
            cells.BorderWidthTop = 0;
            cells.BorderWidthBottom = 1;
            table1.AddCell(cells);
            #endregion

            #region row24

            cells = new PdfPCell(new Phrase("Procedure Date: " + "    "));
            cells.HorizontalAlignment = Element.ALIGN_LEFT;
            cells.BorderWidthTop = 0;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthRight = 0;
            table1.AddCell(cells);
            cells = new PdfPCell();
            cells.BorderWidthTop = 0;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthRight = 0;
            cells.BorderWidthLeft = 0;
            Phrase pharsenew23 = new Phrase();
            if (chkTBDNew1.SelectedItem != null && chkTBDNew1.SelectedItem.Value.Contains("TBD"))
            {
                pharsenew23.Add(new Chunk("\u0033", zapfdingbats));
                pharsenew23.Add("TBD" + "    ");
            }
            else
            {
                pharsenew23.Add("X" + " ");
                //pharsenew23.Add(new Chunk("X", zapfdingbats));
                pharsenew23.Add("TBD" + "    ");
            }

            cells.Phrase = pharsenew23;
            table1.AddCell(cells);
            cells = new PdfPCell(new Phrase("Procedure:" + txtProcNew1.Text + "     "));

            cells.BorderWidthLeft = 0;
            cells.BorderWidthTop = 0;
            cells.BorderWidthBottom = 0;
            table1.AddCell(cells);
            #endregion
            #region row25
            cells = new PdfPCell();
            cells.Colspan = 3;


            Phrase pharsenew24 = new Phrase();
            pharsenew24.Add("Location:" + "    ");
            for (int f = 0; f < chkLOcNew1.Items.Count; f++)
            {
                if (chkLOcNew1.Items[f].Selected == true)
                {
                    pharsenew24.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew24.Add(chkLOcNew1.Items[f].Text + "    ");
                }
                else
                {
                    pharsenew24.Add("X" + " ");
                    // pharsenew24.Add(new Chunk("X", zapfdingbats));
                    pharsenew24.Add(chkLOcNew1.Items[f].Text + "    ");
                }
            }
            cells.Phrase = pharsenew24;
            cells.BorderWidthBottom = 0;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);
            #endregion
            #region row26
            cells = new PdfPCell();
            cells.Colspan = 3;


            Phrase pharsenew25 = new Phrase();
            for (int g = 0; g < chkTLCNEW1.Items.Count; g++)
            {
                if (chkTLCNEW1.Items[g].Selected == true)
                {
                    pharsenew25.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew25.Add(chkTLCNEW1.Items[g].Text + "    ");
                }
                else
                {
                    pharsenew25.Add("X" + " ");
                    // pharsenew25.Add(new Chunk("X", zapfdingbats));
                    pharsenew25.Add(chkTLCNEW1.Items[g].Text + "    ");
                }
            }
            cells.Phrase = pharsenew25;
            cells.BorderWidthBottom = 1;
            cells.BorderWidthTop = 0;
            table1.AddCell(cells);
            #endregion

            #region row27
            cells = new PdfPCell();
            cells.Colspan = 3;
            cells.HorizontalAlignment = Element.ALIGN_CENTER;
            cells.BorderWidthTop = 0;
            cells.BorderWidthBottom = 1;
            Phrase pharsenew30 = new Phrase("In House Proc Date/Loc: " + txtHouseProcDate.Text + "           ");
            pharsenew30.Add("In House Procedure: " + txtInHouseProc.Text + "          ");
            cells.Phrase = pharsenew30;
            table1.AddCell(cells);


            #endregion

            #region row28
            cells = new PdfPCell();
            cells.Colspan = 3;
            cells.HorizontalAlignment = Element.ALIGN_CENTER;
            cells.BorderWidthTop = 0;
            cells.BorderWidthBottom = 1;
            Phrase pharsenew29 = new Phrase("In House Proc Date/Loc: " + txtHouseProcDate1.Text + "           ");
            pharsenew29.Add("In House Procedure: " + txtInHouseProc1.Text + "          ");
            cells.Phrase = pharsenew29;
            table1.AddCell(cells);
            #endregion

            #region row29
            cells = new PdfPCell();
            cells.Colspan = 3;
            cells.HorizontalAlignment = Element.ALIGN_CENTER;
            cells.BorderWidthTop = 0;
            cells.BorderWidthBottom = 1;
            Phrase pharsenew28 = new Phrase("In House Proc Date/Loc: " + txtHouseProcDate2.Text + "           ");
            pharsenew28.Add("In House Procedure: " + txtInHouseProc2.Text + "          ");
            cells.Phrase = pharsenew28;
            table1.AddCell(cells);
            #endregion
            #region row30
            cells = new PdfPCell();
            cells.Colspan = 1;
            Phrase pharsenew26 = new Phrase();
            if (chkOnAc.SelectedItem != null && chkOnAc.SelectedItem.Value.Contains("OnAc"))
            {
                pharsenew26.Add(new Chunk("\u0033", zapfdingbats));
                pharsenew26.Add("On A/C" + "    ");
            }
            else
            {
                pharsenew26.Add("X" + " ");
                //pharsenew26.Add(new Chunk("X", zapfdingbats));
                pharsenew26.Add("On A/C" + "    ");
            }
            cells.Phrase = pharsenew26;
            table1.AddCell(cells);
            cells = new PdfPCell();
            cells.Colspan = 2;
            Phrase pharsenew27 = new Phrase();
            if (chkPthxDM.SelectedItem != null && chkPthxDM.SelectedItem.Value.Contains("PthxDM"))
            {
                pharsenew27.Add(new Chunk("\u0033", zapfdingbats));
                pharsenew27.Add("Pt hx DM" + "    ");
            }
            else
            {
                pharsenew27.Add("X" + " ");
                // pharsenew27.Add(new Chunk("X", zapfdingbats));
                pharsenew27.Add("Pt hx DM" + "    ");
            }
            cells.Phrase = pharsenew27;
            table1.AddCell(cells);
            #endregion
            document.Add(table1);

            PdfPTable table2 = new PdfPTable(3);
            #region row1
            PdfPCell cell1 = new PdfPCell(new Phrase("Copay:" + txtCopay1.Text + ""));
            cell1.Colspan = 1;
            cell1.BorderWidthTop = 0;
            cell1.BorderWidthRight = 0;
            cell1.BorderWidthLeft = 0;
            cell1.BorderWidthBottom = 0;
            cell1.HorizontalAlignment = Element.ALIGN_LEFT;
            table2.AddCell(cell1);

            cell1 = new PdfPCell(new Phrase("Visit End Form"));
            cell1.Colspan = 2;
            cell1.BorderWidthTop = 0;
            cell1.BorderWidthRight = 0;
            cell1.BorderWidthLeft = 0;
            cell1.BorderWidthBottom = 0;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            table2.AddCell(cell1);
            #endregion
            #region row2
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            //cells.BorderWidthRight = 1;
            //cells.BorderWidthLeft = 1;
            // cells.Border = 0;
            //Font zapfdingbats1 = new Font(Font.ZAPFDINGBATS);
            Phrase pharsenew32 = new Phrase();
            pharsenew32.Add("Has F/u Appt Please Remind:");
            for (int k = 0; k < chkHasApptPleaseRemind.Items.Count; k++)
            {
                if (chkHasApptPleaseRemind.Items[k].Selected == true)
                {
                    pharsenew32.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew32.Add(chkHasApptPleaseRemind.Items[k].Text + "    ");
                }
                else
                {
                    pharsenew32.Add("X" + " ");
                    //pharsenew32.Add(new Chunk("X", zapfdingbats));
                    pharsenew32.Add(chkHasApptPleaseRemind.Items[k].Text + "    ");
                }
            }
            cell1.Phrase = pharsenew32;
            cell1.BorderWidthBottom = 1;
            table2.AddCell(cell1);
            #endregion
            #region row2
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            Phrase pharsenew33 = new Phrase();
            pharsenew33.Add("Return visit #1:");
            for (int k = 0; k < chkReturnVisit1.Items.Count; k++)
            {
                if (chkReturnVisit1.Items[k].Selected == true)
                {
                    pharsenew33.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew33.Add(chkReturnVisit1.Items[k].Text + "    ");
                }
                else
                {
                    pharsenew33.Add("X" + " ");
                    //pharsenew33.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew33.Add(chkReturnVisit1.Items[k].Text + "    ");
                }
            }
            cell1.Phrase = pharsenew33;
            cell1.BorderWidthBottom = 0;
            table2.AddCell(cell1);
            #endregion
            #region row3
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            Phrase phrasenew35 = new Phrase("Other: " + txtOtherreview1.Text + "  ");
            phrasenew35.Add("Date: " + txtDateReview1.Text + "    ");
            phrasenew35.Add("Time: " + txtTimeReview1.Text + "    ");
            phrasenew35.Add("Loc: " + txtLocReview1.Text + "    ");
            cell1.Phrase = phrasenew35;
            cell1.BorderWidthBottom = 0;
            cell1.BorderWidthTop = 0;
            table2.AddCell(cell1);
            #endregion
            #region row4
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            Phrase pharsenew34 = new Phrase();
            pharsenew34.Add("Visit Type:");
            for (int k = 0; k < chkVisitType.Items.Count; k++)
            {
                if (chkVisitType.Items[k].Selected == true)
                {
                    pharsenew34.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew34.Add(chkVisitType.Items[k].Text + "    ");
                }
                else
                {
                    pharsenew34.Add("X" + " ");
                    // pharsenew34.Add(new Chunk("X", zapfdingbats));
                    pharsenew34.Add(chkVisitType.Items[k].Text + "    ");
                }
            }
            cell1.Phrase = pharsenew34;
            cell1.BorderWidthTop = 0;
            cell1.BorderWidthBottom = 0;
            table2.AddCell(cell1);
            #endregion
            #region row5
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            Phrase phrasenew36 = new Phrase("Or Proc: " + txtOrproc.Text + "  ");

            phrasenew36.Add("In House Proc: " + txtInhouse.Text + "    ");
            phrasenew36.Add("EMG Review: " + txtEmgreview.Text + "    ");
            cell1.Phrase = phrasenew36;
            cell1.BorderWidthBottom = 1;
            cell1.BorderWidthTop = 0;
            table2.AddCell(cell1);
            #endregion
            #region row6
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            Phrase pharsenew37 = new Phrase();
            pharsenew37.Add("Return visit #2:");
            for (int k = 0; k < chkReturnVisit2.Items.Count; k++)
            {
                if (chkReturnVisit2.Items[k].Selected == true)
                {
                    pharsenew37.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew37.Add(chkReturnVisit2.Items[k].Text + "    ");
                }
                else
                {
                    pharsenew37.Add("X" + " ");
                    // pharsenew37.Add(new Chunk("X", zapfdingbats));
                    pharsenew37.Add(chkReturnVisit2.Items[k].Text + "    ");
                }
            }
            cell1.Phrase = pharsenew37;
            cell1.BorderWidthBottom = 0;
            table2.AddCell(cell1);
            #endregion
            #region row7
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            Phrase phrasenew38 = new Phrase("Other: " + txtOtherreview2.Text + "  ");
            phrasenew38.Add("Date: " + txtDateReview2.Text + "    ");
            phrasenew38.Add("Time: " + txtTimeReview2.Text + "    ");
            phrasenew38.Add("Loc: " + txtLocReview2.Text + "    ");
            cell1.Phrase = phrasenew38;
            cell1.BorderWidthBottom = 0;
            cell1.BorderWidthTop = 0;
            table2.AddCell(cell1);
            #endregion
            #region row8
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            Phrase pharsenew39 = new Phrase();
            pharsenew39.Add("Visit Type:");
            for (int k = 0; k < chkVisitType2.Items.Count; k++)
            {
                if (chkVisitType2.Items[k].Selected == true)
                {
                    pharsenew39.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew39.Add(chkVisitType2.Items[k].Text + "    ");
                }
                else
                {
                    pharsenew39.Add("X" + " ");
                    //pharsenew39.Add(new Chunk("X", zapfdingbats));
                    pharsenew39.Add(chkVisitType2.Items[k].Text + "    ");
                }
            }
            cell1.Phrase = pharsenew39;
            cell1.BorderWidthTop = 0;
            cell1.BorderWidthBottom = 0;
            table2.AddCell(cell1);
            #endregion
            #region row9
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            Phrase phrasenew40 = new Phrase("Or Proc: " + txtOrproc2.Text + "  ");

            phrasenew40.Add("In House Proc: " + txtInhouse2.Text + "    ");
            phrasenew40.Add("EMG Review: " + txtEmgreview2.Text + "    ");
            cell1.Phrase = phrasenew40;
            cell1.BorderWidthBottom = 1;
            cell1.BorderWidthTop = 0;
            table2.AddCell(cell1);
            #endregion
            #region row10
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            Phrase pharsenew41 = new Phrase();
            pharsenew41.Add("Return visit #3:");
            for (int k = 0; k < chkReturnVisit3.Items.Count; k++)
            {
                if (chkReturnVisit3.Items[k].Selected == true)
                {
                    pharsenew41.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew41.Add(chkReturnVisit3.Items[k].Text + "    ");
                }
                else
                {
                    pharsenew41.Add("X" + " ");
                    //pharsenew41.Add(new Chunk("X", zapfdingbats));
                    pharsenew41.Add(chkReturnVisit3.Items[k].Text + "    ");
                }
            }
            cell1.Phrase = pharsenew41;
            cell1.BorderWidthBottom = 0;
            table2.AddCell(cell1);
            #endregion
            #region row11
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            Phrase phrasenew42 = new Phrase("Other: " + txtOtherreview3.Text + "  ");
            phrasenew42.Add("Date: " + txtDateReview3.Text + "    ");
            phrasenew42.Add("Time: " + txtTimeReview3.Text + "    ");
            phrasenew42.Add("Loc: " + txtLocReview3.Text + "    ");
            cell1.Phrase = phrasenew42;
            cell1.BorderWidthBottom = 0;
            cell1.BorderWidthTop = 0;
            table2.AddCell(cell1);
            #endregion
            #region row12
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            Phrase pharsenew42 = new Phrase();
            pharsenew42.Add("Visit Type:");
            for (int k = 0; k < chkVisitType3.Items.Count; k++)
            {
                if (chkVisitType3.Items[k].Selected == true)
                {
                    pharsenew42.Add(new Chunk("\u0033", zapfdingbats));
                    pharsenew42.Add(chkVisitType3.Items[k].Text + "    ");
                }
                else
                {
                    pharsenew42.Add("X" + " ");
                    //pharsenew42.Add(new Chunk("X", zapfdingbats));
                    pharsenew42.Add(chkVisitType3.Items[k].Text + "    ");
                }
            }
            cell1.Phrase = pharsenew42;
            cell1.BorderWidthTop = 0;
            cell1.BorderWidthBottom = 0;
            table2.AddCell(cell1);
            #endregion
            #region row13
            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            Phrase phrasenew43 = new Phrase("Or Proc: " + txtOrproc3.Text + "  ");

            phrasenew43.Add("In House Proc: " + txtInhouse3.Text + "    ");
            phrasenew43.Add("EMG Review: " + txtEmgreview3.Text + "    ");
            cell1.Phrase = phrasenew43;
            cell1.BorderWidthBottom = 1;
            cell1.BorderWidthTop = 0;
            table2.AddCell(cell1);
            #endregion
            #region row14
            cell1 = new PdfPCell();
            cell1.Colspan = 1;
            Phrase pharsenew44 = new Phrase();
            if (rdlUtox.SelectedItem != null && rdlUtox.SelectedItem.Value.Contains("Yes"))
            {
                pharsenew44.Add(" Utox:");
                pharsenew44.Add(" Yes");
            }
            else if (rdlUtox.SelectedItem == null)
            {
                pharsenew44.Add(" Utox:");
                pharsenew44.Add("");
            }
            else
            {
                pharsenew44.Add(" Utox:");
                pharsenew44.Add("No");
            }

            cell1.Phrase = pharsenew44;
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            table2.AddCell(cell1);


            cell1 = new PdfPCell();
            cell1.Colspan = 2;
            Phrase pharsenew45 = new Phrase();
            if (rdlScriptsGiven.SelectedItem != null && rdlScriptsGiven.SelectedItem.Value.Contains("Yes"))
            {
                pharsenew45.Add(" Scripts Given:");
                pharsenew45.Add(" Yes");
            }
            else if (rdlScriptsGiven.SelectedItem == null)
            {
                pharsenew45.Add(" Scripts Given:");
                pharsenew45.Add("");
            }
            else
            {
                pharsenew45.Add(" Scripts Given:");
                pharsenew45.Add("No");
            }

            cell1.Phrase = pharsenew45;
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            table2.AddCell(cell1);
            #endregion
            #region row15

            cell1 = new PdfPCell();
            cell1.Colspan = 1;
            Phrase pharsenew47 = new Phrase("Records Request: " + txtRecordRequest.Text + "  ");
            cell1.Phrase = pharsenew47;
            cell1.BorderWidthTop = 0;

            cell1.BorderWidthBottom = 0;
            table2.AddCell(cell1);


            cell1 = new PdfPCell();
            cell1.Colspan = 2;
            Phrase pharsenew46 = new Phrase();
            if (rdlFormCompleted.SelectedItem != null && rdlFormCompleted.SelectedItem.Value.Contains("Yes"))
            {
                pharsenew46.Add(" Form Completed:");
                pharsenew46.Add(" Yes");
            }
            else if (rdlFormCompleted.SelectedItem == null)
            {
                pharsenew46.Add(" Form Completed:");
                pharsenew46.Add("");
            }
            else
            {
                pharsenew46.Add(" Form Completed:");
                pharsenew46.Add("No");
            }

            cell1.Phrase = pharsenew46;
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            table2.AddCell(cell1);

            #endregion
            #region row16
            cell1 = new PdfPCell();
            cell1.Colspan = 1;
            Phrase pharsenew48 = new Phrase();
            if (rdlSendLegalUpdate.SelectedItem != null && rdlSendLegalUpdate.SelectedItem.Value.Contains("Yes"))
            {
                pharsenew48.Add(" Send Legal Update:");
                pharsenew48.Add(" Yes");
            }
            else if (rdlSendLegalUpdate.SelectedItem == null)
            {
                pharsenew48.Add(" Send Legal Update:");
                pharsenew48.Add("");
            }
            else
            {
                pharsenew48.Add(" Send Legal Update:");
                pharsenew48.Add("No");
            }

            cell1.Phrase = pharsenew48;
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            table2.AddCell(cell1);


            cell1 = new PdfPCell();
            cell1.Colspan = 2;
            Phrase pharsenew49 = new Phrase();
            if (rdlScriptscanned.SelectedItem != null && rdlScriptscanned.SelectedItem.Value.Contains("Yes"))
            {
                pharsenew49.Add(" Script scanned:");
                pharsenew49.Add(" Yes");
            }
            else if (rdlScriptscanned.SelectedItem == null)
            {
                pharsenew49.Add(" Script scanned:");
                pharsenew49.Add("");
            }
            else
            {
                pharsenew49.Add(" Script scanned:");
                pharsenew49.Add("No");
            }

            cell1.Phrase = pharsenew49;
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            table2.AddCell(cell1);
            #endregion
            #region row17

            cell1 = new PdfPCell();
            cell1.Colspan = 1;
            Phrase pharsenew50 = new Phrase("Completed By: " + txtCompletedBy.Text + "  ");

            cell1.Phrase = pharsenew50;
            table2.AddCell(cell1);


            cell1 = new PdfPCell();
            cell1.Colspan = 2;
            Phrase pharsenew51 = new Phrase("Collected: " + txtCollected.Text + "  ");
            cell1.Phrase = pharsenew51;

            table2.AddCell(cell1);

            #endregion
            #region heading

            cell1 = new PdfPCell();
            cell1.Colspan = 1;
            Phrase pharsenew52 = new Phrase("New Pt" + "      ");
            cell1.Phrase = pharsenew52;

            table2.AddCell(cell1);


            cell1 = new PdfPCell();
            cell1.Colspan = 2;
            Phrase pharsenew53 = new Phrase("Surgical Referral" + "      ");
            cell1.Phrase = pharsenew53;

            table2.AddCell(cell1);

            #endregion
            #region row18

            cell1 = new PdfPCell();
            cell1.Colspan = 1;
            Phrase pharsenew61 = new Phrase("Referred By: " + txtReferredBy.Text + "  ");
            cell1.Phrase = pharsenew61;

            table2.AddCell(cell1);


            cell1 = new PdfPCell();
            cell1.Colspan = 2;
            Phrase pharsenew62 = new Phrase("Orthopedics: " + txtOrthopedics.Text + "  ");
            cell1.Phrase = pharsenew62;

            table2.AddCell(cell1);

            #endregion
            #region row19

            cell1 = new PdfPCell();
            cell1.Colspan = 1;
            Phrase pharsenew54 = new Phrase("Therapy Referral: " + txtTherapyReferral.Text + "  ");
            cell1.Phrase = pharsenew54;

            table2.AddCell(cell1);


            cell1 = new PdfPCell();
            cell1.Colspan = 2;
            Phrase pharsenew55 = new Phrase("Spine: " + txtSpine.Text + "  ");
            cell1.Phrase = pharsenew55;

            table2.AddCell(cell1);

            #endregion
            #region row20

            cell1 = new PdfPCell();
            cell1.Colspan = 1;
            Phrase pharsenew56 = new Phrase("Legal Referral: " + txtLegalReferral.Text + "  ");
            cell1.Phrase = pharsenew56;

            table2.AddCell(cell1);


            cell1 = new PdfPCell();
            cell1.Colspan = 2;
            Phrase pharsenew57 = new Phrase("Podiatry: " + txtPodiatry.Text + "  ");
            cell1.Phrase = pharsenew57;

            table2.AddCell(cell1);

            #endregion
            #region row22

            cell1 = new PdfPCell();
            cell1.Colspan = 1;
            Phrase pharsenew58 = new Phrase("Imaging Referral: " + txtImagingReferral.Text + "  ");
            cell1.Phrase = pharsenew58;

            table2.AddCell(cell1);


            cell1 = new PdfPCell();
            cell1.Colspan = 2;
            Phrase pharsenew59 = new Phrase("Emg Referral: " + txtEmgReferral.Text + "  ");
            cell1.Phrase = pharsenew59;

            table2.AddCell(cell1);

            #endregion
            #region row21

            cell1 = new PdfPCell();
            cell1.Colspan = 3;
            Phrase pharsenew60 = new Phrase("Comments: " + txtComments.Text + "  ");
            cell1.Phrase = pharsenew60;

            table2.AddCell(cell1);




            #endregion
            document.Add(table2);


            document.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";

            string pdfName = "User";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + pdfName + ".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }


    }
    public class DynamicCheckbox : IPdfPCellEvent
    {
        private string fieldname;

        public DynamicCheckbox(string name)
        {
            fieldname = name;
        }

        public void CellLayout(PdfPCell cell, Rectangle rectangle, PdfContentByte[] canvases)
        {
            PdfWriter writer = canvases[0].PdfWriter;
            RadioCheckField ckbx = new RadioCheckField(writer, rectangle, fieldname, "Yes");
            ckbx.CheckType = RadioCheckField.TYPE_CHECK;
            ckbx.BackgroundColor = Color.ORANGE;
            ckbx.FontSize = 6;
            ckbx.TextColor = Color.WHITE;
            PdfFormField field = ckbx.CheckField;
            writer.AddAnnotation(field);
        }
    }

    protected void BindPatientIEDetails(string patientId = null, string searchText = null)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connString_NV_Jo"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("nusp_GetPatientIEDetails", con);

            if (!string.IsNullOrEmpty(patientId))
            {
                cmd.Parameters.AddWithValue("@Patient_Id", hfPatientId.Value);
            }
            else if (!string.IsNullOrEmpty(searchText) && string.IsNullOrEmpty(patientId))
            {
                string keyword = searchText.TrimStart(("Mrs. ").ToCharArray());
                cmd.Parameters.AddWithValue("@SearchText", keyword);
            }
            else
            {
                if (Session["Location"] != null)
                {
                    cmd.Parameters.AddWithValue("@LocationId", Convert.ToString(Session["Location"]));
                }
            }

            //if (!string.IsNullOrEmpty(txtFromDate.Text))
            //    cmd.Parameters.AddWithValue("@SDate", txtFromDate.Text);

            //if (!string.IsNullOrEmpty(txtEndDate.Text))
            //    cmd.Parameters.AddWithValue("@EDate", txtEndDate.Text);

            //if (ddl_location.SelectedIndex > 0)
            //{
            //    cmd.Parameters.AddWithValue("@LocationId", ddl_location.SelectedItem.Value);
            //}


            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            con.Close();
            gvPatientDetails.DataSource = dt;
            gvPatientDetails.DataBind();
            hfPatientId.Value = null;
        }

    }

    protected void gvPatientDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPatientDetails.PageIndex = e.NewPageIndex;
        BindPatientIEDetails();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindPatientIEDetails(hfPatientId.Value, txtSearch.Text.Trim());
    }
    protected void gvPatientFUDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvPatientFUDetails = (sender as GridView);
        hfCurrentlyOpened.Value = gvPatientFUDetails.ToolTip;
        gvPatientFUDetails.PageIndex = e.NewPageIndex;
        bindFUDetails(gvPatientFUDetails);
    }

    protected void bindFUDetails(GridView gvPatientFUDetails)
    {
        BusinessLogic bl = new BusinessLogic();
        gvPatientFUDetails.DataSource = bl.GetFUDetails(Convert.ToInt32(gvPatientFUDetails.ToolTip));
        gvPatientFUDetails.DataBind();
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string patientIEId = gvPatientDetails.DataKeys[e.Row.RowIndex].Value.ToString();
            BusinessLogic bl = new BusinessLogic();
            GridView gvPatientFUDetails = e.Row.FindControl("gvPatientFUDetails") as GridView;
            gvPatientFUDetails.ToolTip = patientIEId;
            gvPatientFUDetails.DataSource = bl.GetFUDetails(Convert.ToInt32(patientIEId));
            gvPatientFUDetails.DataBind();
        }

    }


    protected void gvPatientDetails_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gvPatientDetails.PageIndex = e.NewPageIndex;
        BindPatientIEDetails();
    }

    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Login.aspx");
    }


    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Session["PatientIE_ID"] = null;
        Response.Redirect("Page1.aspx");
    }
    protected void lnk_openIE_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        Response.Redirect("Page1.aspx?id=" + btn.CommandArgument);
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PatientIntakeList.aspx");
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdatePrintStatus(string flag, Int64 id)
    {
        string tempFileName = DateTime.Now.ToString("yyyyMMdd_") + flag + "_" + id;
        string tempFilePath = ConfigurationSettings.AppSettings["downloadpath"].ToString();
        string fileGetPath = ConfigurationSettings.AppSettings["fileGetPath"].ToString();
        string zipCreatePath = System.Web.Hosting.HostingEnvironment.MapPath(tempFilePath + "/" + tempFileName + ".zip");
        string[] filePaths = Directory.GetFiles(HttpContext.Current.Server.MapPath(fileGetPath), "*_" + id + "_*.*");

        if (File.Exists(zipCreatePath))
        {
            File.Delete(zipCreatePath);
            if (filePaths.Count() > 0)
            {
                foreach (var item in filePaths)
                {
                    File.Delete(item);
                }
            }
        }

        //if (filePaths.Length <= 0)
        //    return "";
        //using (ZipArchive archive = ZipFile.Open(zipCreatePath, ZipArchiveMode.Create))
        //{
        //    foreach (string filePath in filePaths)
        //    {
        //        string filename = filePath.Substring(filePath.LastIndexOf("\\") + 1);
        //        archive.CreateEntryFromFile(filePath, filename);
        //    }
        //}

        List<string> _patients = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["connString_NV_Jo"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "nusp_UpdatePrintStatus";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", flag);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        _patients.Add(sdr["RESULT"].ToString());
                    }
                }
                conn.Close();
            }
            return "";
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CheckDownload(string flag, Int64 id)
    {
        string tempFileName = DateTime.Now.ToString("yyyyMMdd_") + flag + "_" + id;
        string tempFilePath = ConfigurationSettings.AppSettings["downloadpath"].ToString();
        string fileGetPath = ConfigurationSettings.AppSettings["fileGetPath"].ToString();
        string zipCreatePath = System.Web.Hosting.HostingEnvironment.MapPath(tempFilePath + "/" + tempFileName + ".zip");
        string[] filePaths = Directory.GetFiles(HttpContext.Current.Server.MapPath(fileGetPath), "*_" + id + "_*.*");
        if (File.Exists(zipCreatePath))
        {
            File.Delete(zipCreatePath);
        }
        if (filePaths.Length <= 0)
            return "";
        using (ZipArchive archive = ZipFile.Open(zipCreatePath, ZipArchiveMode.Create))
        {
            foreach (string filePath in filePaths)
            {
                string filename = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                archive.CreateEntryFromFile(filePath, filename);
            }
        }


        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["connString_NV_Jo"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "nusp_UpdatePrintStatus";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", flag);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Isdownload", "1");
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        //_patients.Add(sdr["RESULT"].ToString());
                    }
                }
                conn.Close();
            }
        }
        return tempFileName;
    }



    public override void VerifyRenderingInServerForm(Control control) { }

    protected void btnex_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string PatientIeID = btn.CommandArgument;
        string patieID, printStatus, patfiID;
        string sql = "SELECT c.PatientIE_ID, c.PrintStatus, o.PatientFU_ID";
        sql += " FROM tblPatientIE c INNER JOIN tblFUPatient o on c.PatientIE_ID = o.PatientIE_ID";
        sql += " WHERE c.PatientIE_ID=" + PatientIeID;

        string constr = ConfigurationManager.ConnectionStrings["connString_NV_Jo"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    cmd.Connection = con;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        patieID = dt.Rows[0]["PatientIE_ID"].ToString();
                        printStatus = dt.Rows[0]["PrintStatus"].ToString();
                        patfiID = dt.Rows[0]["PatientFU_ID"].ToString();
                        hdnieid.Value = patieID;
                        hdniefuid.Value = patfiID;

                    }

                }
            }
        }
        string ieid = hdnieid.Value;
        string iefuid = hdniefuid.Value;
        string iefutype = hdniefutype.Value;
        try
        {
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            string firstName = row.Cells[2].Text; // here we are
            string lastName = row.Cells[3].Text;
            string Name = String.Concat(firstName, " ", lastName);
            string dob = row.Cells[4].Text;
            string location = row.Cells[8].Text;


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connString_NV_Jo"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SelectPatientDetails_ExitForm", con);

                if (!string.IsNullOrEmpty(PatientIeID))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlParameter Patient_ID = cmd.Parameters.AddWithValue("@PatientIE_ID", PatientIeID);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        #region ifvalue
                        if (dr.Read())
                        {
                            txtCopay.Text = Convert.ToString(dr["copay"]);
                            txtLoc.Text = Convert.ToString(dr["Location"]);
                            string IFPP;
                            IFPP = Convert.ToString(dr["IFPP"]);
                            string[] k = IFPP.Split(',');
                            for (int m = 0; m <= k.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkIFPP.Items.Count - 1; i++)
                                {
                                    if (chkIFPP.Items[i].Value == k[m])
                                    {
                                        chkIFPP.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtDate.Text = Convert.ToString(dr["ExitDate"]);
                            txtPatientName.Text = Convert.ToString(dr["PatientName"]);
                            txtDOB.Text = Convert.ToString(dr["DOB"]);

                            if (!string.IsNullOrEmpty(dr["ExitCase"].ToString()))
                            {
                                string strCase = Convert.ToString(dr["ExitCase"]);
                                if (strCase == "Yes")
                                {
                                    rdlCase.Items.FindByValue("Yes").Selected = true;
                                }
                                else if (strCase == "No")
                                {
                                    rdlCase.Items.FindByValue("No").Selected = true;
                                }
                                else
                                {
                                    rdlCase.SelectedIndex = -1;
                                }

                            }

                            string Insurance;
                            Insurance = Convert.ToString(dr["Insurance"]);
                            string[] l = Insurance.Split(',');
                            for (int m = 0; m <= l.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkInsurance.Items.Count - 1; i++)
                                {
                                    if (chkInsurance.Items[i].Value == l[m])
                                    {
                                        chkInsurance.Items[m].Selected = true;
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(dr["Verified"].ToString()))
                            {
                                string Verified = Convert.ToString(dr["Verified"]);
                                if (Verified == "Yes")
                                {
                                    rdlVerified.Items.FindByValue("Yes").Selected = true;
                                }
                                else if (Verified == "No")
                                {
                                    rdlVerified.Items.FindByValue("No").Selected = true;
                                }
                                else
                                {
                                    rdlVerified.SelectedIndex = -1;
                                }

                            }
                            string fuon;
                            fuon = Convert.ToString(dr["Fuon"]);
                            string[] n = fuon.Split(',');
                            for (int m = 0; m <= n.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkfuon.Items.Count - 1; i++)
                                {
                                    if (chkfuon.Items[i].Value == n[m])
                                    {
                                        chkfuon.Items[m].Selected = true;
                                    }
                                }
                            }
                            string OtherImaging;
                            OtherImaging = Convert.ToString(dr["OtherImaging"]);
                            string[] p = OtherImaging.Split(',');
                            for (int m = 0; m <= p.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkOtherImaging.Items.Count - 1; i++)
                                {
                                    if (chkOtherImaging.Items[i].Value == p[m])
                                    {
                                        chkOtherImaging.Items[m].Selected = true;
                                    }
                                }
                            }
                            string EECTLR;
                            EECTLR = Convert.ToString(dr["EECTLR"]);
                            string[] q = EECTLR.Split(',');
                            for (int m = 0; m <= q.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkEMGU.Items.Count - 1; i++)
                                {
                                    if (chkEMGU.Items[i].Value == q[m])
                                    {
                                        chkEMGU.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtCESI.Text = Convert.ToString(dr["CESI"]);
                            txtTESI.Text = Convert.ToString(dr["TESI"]);
                            txtLESI.Text = Convert.ToString(dr["LESI"]);
                            txtLTFE.Text = Convert.ToString(dr["LTFE"]);
                            txtCarpTunInj.Text = Convert.ToString(dr["CarpTun"]);

                            txtTRFA.Text = Convert.ToString(dr["TRFA"]);
                            txtLRFA.Text = Convert.ToString(dr["LRFA"]);
                            txtCRFA.Text = Convert.ToString(dr["CRFA"]);
                            txtKneeGelInj.Text = Convert.ToString(dr["KneeJelInj"]);
                            txtSCSTrail.Text = Convert.ToString(dr["SCSTrail"]);

                            txtLMBB.Text = Convert.ToString(dr["LMBB"]);
                            txtTMBB.Text = Convert.ToString(dr["TMBB"]);
                            txtCMBB.Text = Convert.ToString(dr["CMBB"]);

                            txtOther1.Text = Convert.ToString(dr["Other"]);
                            string MUN;
                            MUN = Convert.ToString(dr["MUN"]);
                            string[] r = MUN.Split(',');
                            for (int m = 0; m <= r.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkOther1.Items.Count - 1; i++)
                                {
                                    if (chkOther1.Items[i].Value == r[m])
                                    {
                                        chkOther1.Items[m].Selected = true;
                                    }
                                }
                            }
                            string RequestImaging;
                            RequestImaging = Convert.ToString(dr["RequestImaging"]);
                            string[] s = RequestImaging.Split(',');
                            for (int m = 0; m <= s.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkRequestImaging.Items.Count - 1; i++)
                                {
                                    if (chkRequestImaging.Items[i].Value == s[m])
                                    {
                                        chkRequestImaging.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtOtherImaging1.Text = Convert.ToString(dr["ReqOtherImaging"]);
                            txtImagingLoc.Text = Convert.ToString(dr["ImagingLoc"]);

                            string ReqProc;
                            ReqProc = Convert.ToString(dr["ReqProc"]);
                            string[] t = ReqProc.Split(',');
                            for (int m = 0; m <= t.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkReqProc.Items.Count - 1; i++)
                                {
                                    if (chkReqProc.Items[i].Value == t[m])
                                    {
                                        chkReqProc.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtCESI1.Text = Convert.ToString(dr["ReqProcCESI"]);
                            txtTESI1.Text = Convert.ToString(dr["ReqProcTESI"]);
                            txtLESI1.Text = Convert.ToString(dr["ReqProcLESI"]);
                            txtLTFE1.Text = Convert.ToString(dr["ReqProcLTFE"]);
                            txtMBB.Text = Convert.ToString(dr["ReqProcMBB"]);

                            txtTRFA1.Text = Convert.ToString(dr["ReqProcTRFA"]);
                            txtLRFA1.Text = Convert.ToString(dr["ReqProcLRFA"]);
                            txtCRFA1.Text = Convert.ToString(dr["ReqProcCRFA"]);
                            txtKneeGelInj1.Text = Convert.ToString(dr["ReqProcKneeJelInj"]);
                            txtOther2.Text = Convert.ToString(dr["ReqProcOther"]);
                            string ReqTherapy;
                            ReqTherapy = Convert.ToString(dr["ReqTherapy"]);
                            string[] u = ReqTherapy.Split(',');
                            for (int m = 0; m <= u.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkOther2.Items.Count - 1; i++)
                                {
                                    if (chkOther2.Items[i].Value == u[m])
                                    {
                                        chkOther2.Items[m].Selected = true;
                                    }
                                }
                            }
                            string RequestBrace;
                            RequestBrace = Convert.ToString(dr["RequestBrace"]);
                            string[] v = RequestBrace.Split(',');
                            for (int m = 0; m <= v.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkReqBrace.Items.Count - 1; i++)
                                {
                                    if (chkReqBrace.Items[i].Value == v[m])
                                    {
                                        chkReqBrace.Items[m].Selected = true;
                                    }
                                }
                            }

                            string SeeForm;
                            SeeForm = Convert.ToString(dr["SeeForm"]);
                            string[] w = SeeForm.Split(',');
                            for (int m = 0; m <= w.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkSeeForm.Items.Count - 1; i++)
                                {
                                    if (chkSeeForm.Items[i].Value == w[m])
                                    {
                                        chkSeeForm.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtInHouseProcPerformed.Text = Convert.ToString(dr["InHouseholdPerformed"]);
                            string ProcedureDate;
                            ProcedureDate = Convert.ToString(dr["ProcedureDate"]);
                            string[] x = ProcedureDate.Split(',');
                            for (int m = 0; m <= x.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkTBD.Items.Count - 1; i++)
                                {
                                    if (chkTBD.Items[i].Value == x[m])
                                    {
                                        chkTBD.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtProcedure.Text = Convert.ToString(dr["ReqProcedure"]);
                            string ReqLocation;
                            ReqLocation = Convert.ToString(dr["ReqLocation"]);
                            string[] y = ReqLocation.Split(',');
                            for (int m = 0; m <= y.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkLocationNew.Items.Count - 1; i++)
                                {
                                    if (chkLocationNew.Items[i].Value == y[m])
                                    {
                                        chkLocationNew.Items[m].Selected = true;
                                    }
                                }
                            }
                            string TLC;
                            TLC = Convert.ToString(dr["TLC"]);
                            string[] z = ReqLocation.Split(',');
                            for (int m = 0; m <= z.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkTLC.Items.Count - 1; i++)
                                {
                                    if (chkTLC.Items[i].Value == z[m])
                                    {
                                        chkTLC.Items[m].Selected = true;
                                    }
                                }
                            }

                            string ProcedureDate1;
                            ProcedureDate1 = Convert.ToString(dr["ProcedureDate1"]);
                            string[] a = ProcedureDate1.Split(',');
                            for (int m = 0; m <= a.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkTBDNew1.Items.Count - 1; i++)
                                {
                                    if (chkTBDNew1.Items[i].Value == a[m])
                                    {
                                        chkTBDNew1.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtProcNew1.Text = Convert.ToString(dr["ReqProcedure1"]);
                            string ReqLocation1;
                            ReqLocation1 = Convert.ToString(dr["ReqLocation1"]);
                            string[] b = ProcedureDate1.Split(',');
                            for (int m = 0; m <= b.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkLOcNew1.Items.Count - 1; i++)
                                {
                                    if (chkLOcNew1.Items[i].Value == b[m])
                                    {
                                        chkLOcNew1.Items[m].Selected = true;
                                    }
                                }
                            }
                            string TLC1;
                            TLC1 = Convert.ToString(dr["TLC1"]);
                            string[] c = TLC1.Split(',');
                            for (int m = 0; m <= c.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkTLCNEW1.Items.Count - 1; i++)
                                {
                                    if (chkTLCNEW1.Items[i].Value == c[m])
                                    {
                                        chkTLCNEW1.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtHouseProcDate.Text = Convert.ToString(dr["InHouseProcDateLoc"]);
                            txtInHouseProc.Text = Convert.ToString(dr["InHouseProcedure"]);

                            txtHouseProcDate1.Text = Convert.ToString(dr["InHouseProcDateLoc1"]);
                            txtInHouseProc1.Text = Convert.ToString(dr["InHouseProcedure1"]);

                            txtHouseProcDate2.Text = Convert.ToString(dr["InHouseProcDateLoc2"]);
                            txtInHouseProc2.Text = Convert.ToString(dr["InHouseProcedure2"]);

                            string OnAC;
                            OnAC = Convert.ToString(dr["OnAC"]);
                            string[] d = OnAC.Split(',');
                            for (int m = 0; m <= d.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkOnAc.Items.Count - 1; i++)
                                {
                                    if (chkOnAc.Items[i].Value == d[m])
                                    {
                                        chkOnAc.Items[m].Selected = true;
                                    }
                                }
                            }

                            string PthxDM;
                            PthxDM = Convert.ToString(dr["PthxDM"]);
                            string[] f = PthxDM.Split(',');
                            for (int m = 0; m <= f.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkPthxDM.Items.Count - 1; i++)
                                {
                                    if (chkPthxDM.Items[i].Value == f[m])
                                    {
                                        chkPthxDM.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtCopay1.Text = Convert.ToString(dr["Copay1"]);
                            string HasFUApptPlsRem;
                            HasFUApptPlsRem = Convert.ToString(dr["HasFUApptPlsRem"]);
                            string[] g = HasFUApptPlsRem.Split(',');
                            for (int m = 0; m <= g.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkHasApptPleaseRemind.Items.Count - 1; i++)
                                {
                                    if (chkHasApptPleaseRemind.Items[i].Value == g[m])
                                    {
                                        chkHasApptPleaseRemind.Items[m].Selected = true;
                                    }
                                }
                            }

                            string ReturnVisit1;
                            ReturnVisit1 = Convert.ToString(dr["ReturnVisit1"]);
                            string[] h = ReturnVisit1.Split(',');
                            for (int m = 0; m <= h.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkReturnVisit1.Items.Count - 1; i++)
                                {
                                    if (chkReturnVisit1.Items[i].Text == h[m])
                                    {
                                        chkReturnVisit1.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtOtherreview1.Text = Convert.ToString(dr["ReturnVisitOther1"]);
                            txtDateReview1.Text = Convert.ToString(dr["ReturnVisitDate1"]);
                            txtTimeReview1.Text = Convert.ToString(dr["ReturnVisitTime1"]);
                            txtLocReview1.Text = Convert.ToString(dr["ReturnVisitLoc1"]);

                            string ReturnVisitType1;
                            ReturnVisitType1 = Convert.ToString(dr["ReturnVisitType1"]);
                            string[] a1 = ReturnVisitType1.Split(',');
                            for (int m = 0; m <= a1.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkVisitType.Items.Count - 1; i++)
                                {
                                    if (chkVisitType.Items[i].Value == a1[m])
                                    {
                                        chkVisitType.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtOrproc.Text = Convert.ToString(dr["ReturnVisitORProc1"]);
                            txtInhouse.Text = Convert.ToString(dr["ReturnVisitINnHouseProc1"]);
                            txtEmgreview.Text = Convert.ToString(dr["ReturnVisitEMGReview1"]);

                            string ReturnVisit2;
                            ReturnVisit2 = Convert.ToString(dr["ReturnVisit2"]);
                            string[] a2 = ReturnVisit2.Split(',');
                            for (int m = 0; m <= a2.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkReturnVisit2.Items.Count - 1; i++)
                                {
                                    if (chkReturnVisit2.Items[i].Text == a2[m])
                                    {
                                        chkReturnVisit2.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtOtherreview2.Text = Convert.ToString(dr["ReturnVisitOther2"]);
                            txtDateReview2.Text = Convert.ToString(dr["ReturnVisitDate2"]);
                            txtTimeReview2.Text = Convert.ToString(dr["ReturnVisitTime2"]);
                            txtLocReview2.Text = Convert.ToString(dr["ReturnVisitLoc2"]);

                            string ReturnVisitType2;
                            ReturnVisitType2 = Convert.ToString(dr["ReturnVisitType2"]);
                            string[] a3 = ReturnVisitType2.Split(',');
                            for (int m = 0; m <= a3.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkVisitType2.Items.Count - 1; i++)
                                {
                                    if (chkVisitType2.Items[i].Value == a3[m])
                                    {
                                        chkVisitType2.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtOrproc2.Text = Convert.ToString(dr["ReturnVisitORProc2"]);
                            txtInhouse2.Text = Convert.ToString(dr["ReturnVisitINnHouseProc2"]);
                            txtEmgreview2.Text = Convert.ToString(dr["ReturnVisitEMGReview2"]);

                            string ReturnVisit3;
                            ReturnVisit3 = Convert.ToString(dr["ReturnVisit3"]);
                            string[] a4 = ReturnVisit3.Split(',');
                            for (int m = 0; m <= a4.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkReturnVisit3.Items.Count - 1; i++)
                                {
                                    if (chkReturnVisit3.Items[i].Text == a4[m])
                                    {
                                        chkReturnVisit3.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtOtherreview3.Text = Convert.ToString(dr["ReturnVisitOther3"]);
                            txtDateReview3.Text = Convert.ToString(dr["ReturnVisitDate3"]);
                            txtTimeReview3.Text = Convert.ToString(dr["ReturnVisitTime3"]);
                            txtLocReview3.Text = Convert.ToString(dr["ReturnVisitLoc3"]);

                            string ReturnVisitType3;
                            ReturnVisitType3 = Convert.ToString(dr["ReturnVisitType3"]);
                            string[] a5 = ReturnVisitType3.Split(',');
                            for (int m = 0; m <= a5.Length - 1; m++)
                            {
                                for (int i = 0; i <= chkVisitType3.Items.Count - 1; i++)
                                {
                                    if (chkVisitType3.Items[i].Text == a5[m])
                                    {
                                        chkVisitType3.Items[m].Selected = true;
                                    }
                                }
                            }
                            txtOrproc3.Text = Convert.ToString(dr["ReturnVisitORProc3"]);
                            txtInhouse3.Text = Convert.ToString(dr["ReturnVisitINnHouseProc3"]);
                            txtEmgreview3.Text = Convert.ToString(dr["ReturnVisitEMGReview3"]);
                            if (!string.IsNullOrEmpty(dr["UTOX"].ToString()))
                            {
                                string UTOX = Convert.ToString(dr["UTOX"]);
                                if (UTOX == "Yes")
                                {
                                    rdlUtox.Items.FindByValue("Yes").Selected = true;
                                }
                                else if (UTOX == "No")
                                {
                                    rdlUtox.Items.FindByValue("No").Selected = true;
                                }
                                else
                                {
                                    rdlUtox.SelectedIndex = -1;
                                }

                            }
                            if (!string.IsNullOrEmpty(dr["ScriptsGiven"].ToString()))
                            {
                                string ScriptsGiven = Convert.ToString(dr["ScriptsGiven"]);
                                if (ScriptsGiven == "Yes")
                                {
                                    rdlScriptsGiven.Items.FindByValue("Yes").Selected = true;
                                }
                                else if (ScriptsGiven == "No")
                                {
                                    rdlScriptsGiven.Items.FindByValue("No").Selected = true;
                                }
                                else
                                {
                                    rdlScriptsGiven.SelectedIndex = -1;
                                }

                            }
                            txtRecordRequest.Text = Convert.ToString(dr["RecordsRequest"]);
                            if (!string.IsNullOrEmpty(dr["FormCompleted"].ToString()))
                            {
                                string FormCompleted = Convert.ToString(dr["FormCompleted"]);
                                if (FormCompleted == "Yes")
                                {
                                    rdlFormCompleted.Items.FindByValue("Yes").Selected = true;
                                }
                                else if (FormCompleted == "No")
                                {
                                    rdlFormCompleted.Items.FindByValue("No").Selected = true;
                                }
                                else
                                {
                                    rdlFormCompleted.SelectedIndex = -1;
                                }

                            }

                            if (!string.IsNullOrEmpty(dr["SendLegalUpdate"].ToString()))
                            {
                                string SendLegalUpdate = Convert.ToString(dr["SendLegalUpdate"]);
                                if (SendLegalUpdate == "Yes")
                                {
                                    rdlSendLegalUpdate.Items.FindByValue("Yes").Selected = true;
                                }
                                else if (SendLegalUpdate == "No")
                                {
                                    rdlSendLegalUpdate.Items.FindByValue("No").Selected = true;
                                }
                                else
                                {
                                    rdlSendLegalUpdate.SelectedIndex = -1;
                                }

                            }
                            if (!string.IsNullOrEmpty(dr["ScriptsScanned"].ToString()))
                            {
                                string ScriptsScanned = Convert.ToString(dr["ScriptsScanned"]);
                                if (ScriptsScanned == "Yes")
                                {
                                    rdlScriptscanned.Items.FindByValue("Yes").Selected = true;
                                }
                                else if (ScriptsScanned == "No")
                                {
                                    rdlScriptscanned.Items.FindByValue("No").Selected = true;
                                }
                                else
                                {
                                    rdlScriptscanned.SelectedIndex = -1;
                                }

                            }
                            txtCompletedBy.Text = Convert.ToString(dr["CompltedBy"]);
                            txtCollected.Text = Convert.ToString(dr["Collected"]);

                            txtReferredBy.Text = Convert.ToString(dr["ReferredBy"]);
                            txtOrthopedics.Text = Convert.ToString(dr["Orthopedics"]);
                            txtTherapyReferral.Text = Convert.ToString(dr["TherapyReferral"]);
                            txtSpine.Text = Convert.ToString(dr["Spine"]);
                            txtLegalReferral.Text = Convert.ToString(dr["LegalReferral"]);
                            txtPodiatry.Text = Convert.ToString(dr["Podiatry"]);
                            txtImagingReferral.Text = Convert.ToString(dr["ImagingReferral"]);
                            txtEmgReferral.Text = Convert.ToString(dr["EmgReferral"]);
                            txtComments.Text = Convert.ToString(dr["Comments"]);
                            SaveExitForm.Visible = false;
                            UpdateExitForm.Visible = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModel()", true);

                        }
                        #endregion
                        else
                        {

                            txtPatientName.Text = Name;
                            txtDOB.Text = dob;
                            txtLoc.Text = location;
                            txtDate.Text = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
                            txtDateReview1.Text = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
                            txtDateReview2.Text = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
                            txtDateReview3.Text = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
                            SaveExitForm.Visible = true;
                            UpdateExitForm.Visible = false;
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModel()", true);
                        }


                    }
                }


                con.Close();
            }
            // add your code here for whaterever edit 
            // in btn.command argument you will get the patient ie id.
            // if get record in the your table bind here else bind the default value. on close this below line will open the popup

            //  ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModel()", true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //private void bindLocation()
    //{
    //    DataSet ds = new DataSet();
    //    DBHelperClass db = new DBHelperClass();
    //    string query = "select Location,Location_ID from tblLocations ";
    //    if (!string.IsNullOrEmpty(Session["Locations"].ToString()))
    //    {
    //        query = query + " where Location_ID in (" + Session["Locations"] + ")";
    //    }
    //    query = query + " Order By Location";

    //    ds = db.selectData(query);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddl_location.DataValueField = "Location_ID";
    //        ddl_location.DataTextField = "Location";

    //        ddl_location.DataSource = ds;
    //        ddl_location.DataBind();

    //        ddl_location.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- All --", "0"));


    //    }

    //}
}