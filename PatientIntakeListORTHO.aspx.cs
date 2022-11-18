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
using IntakeSheet.DAL;
//using System.Drawing;
//using Xceed.Document.NET;

public partial class PatientIntakeListORTHO : System.Web.UI.Page
{
    DBHelperClass db = new DBHelperClass();
    string selectedbodypart = string.Empty;
    DBHelperClass gDbhelperobj = new DBHelperClass();

    int age = 0;
    string doa = string.Empty;
    string gender = string.Empty;
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

        #region pg1
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
        #region pg2
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
        #endregion

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

        #region pg1
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
        #region pg2
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
        #endregion

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
        #region pageone

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
        #region page2
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
        #endregion

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
            #region row2
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
            SqlCommand cmd = new SqlCommand("nusp_GetPatientIEDetailsORTHOPC", con);

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
            Session["iedata"] = dt;
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
        // BusinessLogic bl = new BusinessLogic();
        gvPatientFUDetails.DataSource = GetFUDetailsORTHOPC(Convert.ToInt32(gvPatientFUDetails.ToolTip));
        gvPatientFUDetails.DataBind();
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataRowView drv = e.Row.DataItem as DataRowView;

            if (!string.IsNullOrEmpty(Convert.ToString(drv["soap"])))
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }


            string patientIEId = gvPatientDetails.DataKeys[e.Row.RowIndex].Value.ToString();
            BusinessLogic bl = new BusinessLogic();
            GridView gvPatientFUDetails = e.Row.FindControl("gvPatientFUDetails") as GridView;
            gvPatientFUDetails.ToolTip = patientIEId;
            gvPatientFUDetails.DataSource = GetFUDetailsORTHOPC(Convert.ToInt32(patientIEId));
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
        Response.Redirect("~/PatientIntakeListORTHO.aspx");
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

    protected void lnkiesoap_Click1(object sender, EventArgs e)
    {
        try
        {
            //  btnCreatnewFu.Visible = false;
            btnCreatnew.Visible = true;
            clearsoap();
            LinkButton btn = (LinkButton)(sender);
            btnCreatnew.CommandArgument = Convert.ToString(btn.CommandArgument) + "|" + "0";
            //check for the value available or not in the soap table.
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connString_NV_Jo"].ConnectionString);
            DBHelperClass db = new DBHelperClass();
            string query = "select LEFT(DOS,10) AS DOS,* from tblSoap where PatientIE_ID = " + btn.CommandArgument;
            SqlCommand cm = new SqlCommand(query, cn);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            cn.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvEditSoap.DataSource = ds;
                gvEditSoap.DataBind();
                gvEditSoap.Visible = true;
                lblRecordnotfound.Visible = false;
            }
            else
            {
                gvEditSoap.Visible = false;
                lblRecordnotfound.Visible = true;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoapEditSoap();", true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnsavesoap_Click(object sender, EventArgs e)
    {

        string SP = "";
        SqlParameter[] param = null;

        //starting the insertion in database

        DBHelperClass db = new DBHelperClass();
        param = new SqlParameter[56];
        SP = "saveupdatesoap";

        param[0] = new SqlParameter("@PatientIE_ID", hdnrodieid.Value);
        param[1] = new SqlParameter("@PatientFU_ID", hdnrodfuid.Value);
        param[2] = new SqlParameter("@Patient_Name", lblName.Text);
        param[3] = new SqlParameter("@DOB", txtdobsoap.Text);
        param[4] = new SqlParameter("@DOS", txtCreateSoapDate.Text);
        param[5] = new SqlParameter("@chkLeftShoulder", chkLeftShoulder.Checked);
        param[6] = new SqlParameter("@chkRightShoulder", chkRightShoulder.Checked);
        param[7] = new SqlParameter("@chkLeftHip", chkLeftHip.Checked);
        param[8] = new SqlParameter("@chkRightHip", chkRightHip.Checked);
        param[9] = new SqlParameter("@chkLeftKnee", chkLeftKnee.Checked);
        param[10] = new SqlParameter("@chkRightKnee", chkRightKnee.Checked);
        param[11] = new SqlParameter("@chkLeftAnkleFoot", chkLeftAnkleFoot.Checked);
        param[12] = new SqlParameter("@chkRightAnkleFoot", chkRightAnkleFoot.Checked);
        param[13] = new SqlParameter("@txtHistoryPresentillness", txtHistoryPresentillness.Text);
        param[14] = new SqlParameter("@chkWC", chkWC.Checked);
        param[15] = new SqlParameter("@txtwccheck", txtwccheck.Text);
        param[16] = new SqlParameter("@bpLShoulder", bpLShoulder.Text);
        param[17] = new SqlParameter("@bpRShoulder", bpRShoulder.Text);
        param[18] = new SqlParameter("@bpLHip", bpLHip.Text);
        param[19] = new SqlParameter("@bpRHip", bpRHip.Text);
        param[20] = new SqlParameter("@bpLKnee", bpLKnee.Text);
        param[21] = new SqlParameter("@bpRKnee", bpRKnee.Text);
        param[22] = new SqlParameter("@bpLAnkleFoot", bpLAnkleFoot.Text);
        param[23] = new SqlParameter("@bpRAnkleFoot", bpRAnkleFoot.Text);
        param[24] = new SqlParameter("@txtPastMedicalHistory", txtPastMedicalHistory.Text);
        param[25] = new SqlParameter("@txtpastsurgicalhistory", txtpastsurgicalhistory.Text);
        param[26] = new SqlParameter("@txtpastaccideninjuries", txtpastaccideninjuries.Text);
        param[27] = new SqlParameter("@txtdailyMedications", txtdailyMedications.Text);
        param[28] = new SqlParameter("@txtAllergies", txtAllergies.Text);
        param[29] = new SqlParameter("@txtSocialHistory", txtSocialHistory.Text);
        param[30] = new SqlParameter("@txtPhysicalExamination", txtPhysicalExamination.Text);
        param[31] = new SqlParameter("@chkStextbox", chkStextbox.Checked);
        param[32] = new SqlParameter("@txtStext", txtStext.Text);
        param[33] = new SqlParameter("@chkAtextbox", chkAtextbox.Checked);
        param[34] = new SqlParameter("@txtAtextbox", txtAtextbox.Text);
        param[35] = new SqlParameter("@chkHtextbox", chkHtextbox.Checked);
        param[36] = new SqlParameter("@txtHtextbox", txtHtextbox.Text);
        param[37] = new SqlParameter("@chkKtextbox", chkKtextbox.Checked);
        param[38] = new SqlParameter("@txtKtextbox", txtKtextbox.Text);
        param[39] = new SqlParameter("@txtDiagnosticImaging", txtDiagnosticImaging.Text);
        param[40] = new SqlParameter("@txtAssestmentplan", txtAssestmentplan.Text);
        param[41] = new SqlParameter("@chkAshoulder", chkAshoulder.Checked);
        param[42] = new SqlParameter("@txtchkAshoulder", txtchkAshoulder.Text);
        param[43] = new SqlParameter("@chkAOther", chkAOther.Checked);
        param[44] = new SqlParameter("@txtchkAOther", txtchkAOther.Text);

        param[45] = new SqlParameter("@txtExaminedResult", txtExaminedResult.Text);
        param[46] = new SqlParameter("@txtDefault", txtDefault.Text);
        param[47] = new SqlParameter("@SoapID", Convert.ToInt32("0"));
        param[48] = new SqlParameter("@chkAKnee", chkAKnee.Checked);
        param[49] = new SqlParameter("@txtchkAKnee", txtchkAKnee.Text);

        param[50] = new SqlParameter("@chkLeftOther", chkLeftOther.Checked);
        //  param[51] = new SqlParameter("@chkRightOther", chkRightOther.Checked);
        param[51] = new SqlParameter("@chkRightOther", false);

        param[52] = new SqlParameter("@bpLOtherFoot", bpLOtherFoot.Text);
        //  param[53] = new SqlParameter("@bpROtherFoot", bpROtherFoot.Text);
        param[53] = new SqlParameter("@bpROtherFoot", null);

        param[54] = new SqlParameter("@chkOtextbox", chkOtextbox.Checked);
        param[55] = new SqlParameter("@txtOtextbox", txtOtextbox.Text);


        //Insert values in the db.
        int val = db.executeSP(SP, param);


    }
    protected void btnCreatnew_Click(object sender, EventArgs e)
    {
        try
        {
            // clearsoap();
            LinkButton btncreate = (LinkButton)(sender);
            LinkButton btn = new LinkButton();
            btn.CommandArgument = btncreate.CommandArgument;
            if (btn.CommandArgument.Split(',').Count() > 1)
            {
                lnkFUsoap_Click1(btn, e);
            }
            else
            {
                lnkiesoap_Click(btn, e);
            }

        }
        catch (Exception ex)
        {
            db.LogError(ex);
            throw;
        }
    }
    public string selectedbodypartsoap()
    {
        selectedbodypart = "";

        //body part selected string. 
        if (chkLeftShoulder.Checked)
        { selectedbodypart += chkLeftShoulder.Text + ", "; }
        else
        { selectedbodypart.Replace(chkLeftShoulder.Text + ", ", ""); }

        if (chkRightShoulder.Checked)
        { selectedbodypart += chkRightShoulder.Text + ", "; }
        else
        { selectedbodypart.Replace(chkRightShoulder.Text + ", ", ""); }

        if (chkLeftHip.Checked)
        { selectedbodypart += chkLeftHip.Text + ", "; }
        else
        { selectedbodypart.Replace(chkLeftHip.Text + ", ", ""); }
        if (chkRightHip.Checked)
        { selectedbodypart += chkRightHip.Text + ", "; }
        else
        { selectedbodypart.Replace(chkRightHip.Text + ", ", ""); }


        if (chkLeftKnee.Checked)
        { selectedbodypart += chkLeftKnee.Text + ", "; }
        else
        { selectedbodypart.Replace(chkLeftKnee.Text + ", ", ""); }
        if (chkRightKnee.Checked)
        { selectedbodypart += chkRightKnee.Text + ", "; }
        else
        { selectedbodypart.Replace(chkRightKnee.Text + ", ", ""); }


        if (chkLeftAnkleFoot.Checked)
        { selectedbodypart += chkLeftAnkleFoot.Text + ", "; }
        else
        { selectedbodypart.Replace(chkLeftAnkleFoot.Text + ", ", ""); }
        if (chkRightAnkleFoot.Checked)
        { selectedbodypart += chkRightAnkleFoot.Text + ", "; }
        else
        { selectedbodypart.Replace(chkRightAnkleFoot.Text + ", ", ""); }



        if (chkLeftOther.Checked)
        { selectedbodypart += chkLeftOther.Text + ", "; }
        else
        { selectedbodypart.Replace(chkLeftOther.Text + ", ", ""); }

        //if (chkRightOther.Checked)
        //{ selectedbodypart += chkRightOther.Text + ", "; }
        //else
        //{ selectedbodypart.Replace(chkRightOther.Text + ", ", ""); }


        return selectedbodypart.TrimEnd(' ').TrimEnd(',');


    }
    protected void lnkiesoap_Click(object sender, EventArgs e)
    {
        try
        {
            hdnrodeditedfuid.Value = string.Empty;
            hdnrodeditedfuieid.Value = string.Empty;
            hdnSoapId.Value = string.Empty;
            btnsavesoap.Visible = true;
            btnupdaterecords.Visible = false;
            // end body parts. 
            btnupdaterecords.Visible = false;
            btnsavesoap.Visible = true;

            string SoapId = hdnrodieid.Value = hdnSoapId.Value = string.Empty;
            LinkButton btn = (LinkButton)(sender);
            DataTable dt = (DataTable)(Session["iedata"]);

            if (btn.CommandArgument.Split('|').Count() > 1)
            {
                hdnrodieid.Value = btn.CommandArgument.Split('|')[0];
                hdnSoapId.Value = btn.CommandArgument.Split('|')[1];
            }

            DataView dv = new DataView(dt);
            dv.RowFilter = "PatientIE_ID=" + Convert.ToInt32(hdnrodieid.Value); // query example = "id = 10"

            Session["ieid"] = btn.CommandArgument;

            //check for the value available or not in the soap table.

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connString_NV_Jo"].ConnectionString);
            DBHelperClass db = new DBHelperClass();
            string query = ("select * from tblSoap where PatientIE_ID= " + hdnrodieid.Value + " and PatientFU_ID is null and ID =" + hdnSoapId.Value);
            SqlCommand cm = new SqlCommand(query, cn);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            cn.Open();

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                if (dv != null)
                {
                    lblName.Text = Convert.ToString(dv[0].Row.ItemArray[2]) + ", " + Convert.ToString(dv[0].Row.ItemArray[3]);//Last name +First Name;
                    txtdobsoap.Text = !String.IsNullOrEmpty(Convert.ToString(dv[0].Row.ItemArray[4])) ? Convert.ToDateTime(dv[0].Row.ItemArray[4]).ToString("MM/dd/yyyy") : string.Empty;//DOB
                    personage.Value = Convert.ToString(!String.IsNullOrEmpty(Convert.ToString(dv[0].Row.ItemArray[4])) ? get_age(Convert.ToDateTime(dv[0].Row.ItemArray[4])) : 0);//DOB);
                    doa = !String.IsNullOrEmpty(Convert.ToString(dv[0].Row.ItemArray[5])) ? Convert.ToDateTime(dv[0].Row.ItemArray[5]).ToString("MM/dd/yyyy") : string.Empty;//DOA
                    persongender.Value = Convert.ToString(dv[0].Row.ItemArray[1]) == "Ms." ? "female" : "male";
                }
                txtCreateSoapDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                chkWC.Checked = true;
                CheckBox chk = new CheckBox();
                chk.Checked = true;
                chkWC_CheckedChanged(chk, e);
            }
            string socialhistorty = string.Empty;
            string query2 = "select PMH,PSH,Medications,Allergies,FamilyHistory,DeniesSmoking,DeniesDrinking,DeniesDrugs,DeniesSocialDrinking,Vitals from tblPatientIEDetailPage2 where PatientIE_ID=" + hdnrodieid.Value;
            DataSet ds1 = gDbhelperobj.selectData(query2);

            if (ds1.Tables[0].Rows.Count > 0)
            {
                txtPastMedicalHistory.Text = ds1.Tables[0].Rows[0]["PMH"].ToString();
                txtpastsurgicalhistory.Text = ds1.Tables[0].Rows[0]["PSH"].ToString();
                txtdailyMedications.Text = ds1.Tables[0].Rows[0]["Medications"].ToString();
                txtAllergies.Text = ds1.Tables[0].Rows[0]["Allergies"].ToString();

                if (ds1.Tables[0].Rows[0]["DeniesSmoking"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(ds1.Tables[0].Rows[0]["DeniesSmoking"].ToString()))
                    { socialhistorty += " Somking, "; }
                }
                if (ds1.Tables[0].Rows[0]["DeniesDrinking"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(ds1.Tables[0].Rows[0]["DeniesDrinking"].ToString()))
                    { socialhistorty += " drinking, "; }
                }
                if (ds1.Tables[0].Rows[0]["DeniesDrugs"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(ds1.Tables[0].Rows[0]["DeniesDrugs"].ToString()))
                    { socialhistorty += "  drugs, "; }
                }
                if (ds1.Tables[0].Rows[0]["DeniesSocialDrinking"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(ds1.Tables[0].Rows[0]["DeniesSocialDrinking"].ToString()))
                    { socialhistorty += " social drinking."; }
                }
                txtSocialHistory.Text = socialhistorty;
            }

            string query1 = "select * from tblPatientIEDetailPage1 where PatientIE_ID=" + hdnrodieid.Value;

            DataSet ds2 = gDbhelperobj.selectData(query1);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                txtpastaccideninjuries.Text = ds2.Tables[0].Rows[0]["AccidentDetail"].ToString();
            }
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
        }
        catch (Exception ex)
        {
            db.LogError(ex);
            throw;
        }
    }
    public void binddynamicdataonchanges()
    {
        if (personage.Value.Equals("0"))
        {
            personage.Value = "__";
        }

        txtHistoryPresentillness.Text = "This is a " + personage.Value + " year-old right_left hand dominant " + persongender.Value + " who was involved in a motor vehicle /work related accident on " + doa + ". " +
                                                   "Accident description.  Patient injured " + selectedbodypartsoap() + " in the accident. " +
                                                  "The patient is here today for orthopedic evaluation. Patient has tried _____ months of PT.";

        txtPhysicalExamination.Text = "Vitals: On physical examination, the patient is 5__feet 1__ inches tall weighs ___ pounds \n" +
                                               "General Appearance: Patient is a well-developed, well-nourished " + persongender.Value + " in no acute distress. Awake, alert, \n" +
                                                "and oriented x 3. Mood and affect are normal.\n" +
                                                "Gait and Station: Gait is normal ";

        txtExaminedResult.Text = "The patient’s  " + selectedbodypartsoap() + " was/were examined" +
                                "MRI of the " + selectedbodypartsoap() + " was / were reviewed" +
                                "The patient at the present time is advised to __________________________" +
                                "Patient is to return to the office ____________";
        string bodyparttextselected = (Convert.ToString(selectedbodypartsoap()).Split(',').Count() > 1 ? selectedbodypartsoap() + " were " : selectedbodypartsoap() + " was ");
        txtAssestmentplan.Text = "Diagnosis: 1._______ \n " +
                                 "                  2._______\n" +
                                 "Recommend __________";

        txtExaminedResult.Text = "The patient’s  " + bodyparttextselected + " examined \n" +
                                 "MRI of the " + bodyparttextselected + " reviewed. \n" +
                                 "The patient at the present time is advised to ______." +
                                 "Patient is to return to the office ____________";

    }
    public int get_age(DateTime dob)
    {
        int age = 0;
        age = DateTime.Now.AddYears(-dob.Year).Year;
        return age;
    }

    protected void soapdelete_Click(object sender, EventArgs e)
    {
        try
        {
            string ieid = string.Empty;
            string fuid = string.Empty;
            string soapid = string.Empty;
            LinkButton btn = (LinkButton)(sender);
            string type = btn.CommandArgument.Split('-')[0];
            if (type.Equals("IE"))
            {
                ieid = btn.CommandArgument.Split('-')[1].Split('|')[0];
                soapid = btn.CommandArgument.Split('-')[1].Split('|')[1];
            }
            else if (type.Equals("FU"))
            {
                ieid = btn.CommandArgument.Split('-')[1].Split('|')[0];
                fuid = btn.CommandArgument.Split('-')[1].Split('|')[1];
                soapid = btn.CommandArgument.Split('-')[1].Split('|')[2];
            }



            DBHelperClass dB = new DBHelperClass();
            //string ieid = string.Empty, soapid = string.Empty;

            int val = dB.executeQuery("delete from tblsoap where id=" + soapid);


            if (type.Equals("IE"))
            {
                LinkButton btn1 = new LinkButton();
                btn1.CommandArgument = ieid;
                lnkiesoap_Click1(btn1, e);
            }
            else if (type.Equals("FU"))
            {
                LinkButton btn1 = new LinkButton();
                btn1.CommandArgument = fuid + "|" + ieid;
                //  lnkfusoap_Click1(btn1, e);
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void chkLeftShoulder_CheckedChanged(object sender, EventArgs e)
    {
        selectedbodypartsoap();
        binddynamicdataonchanges();
        if (chkLeftShoulder.Checked)
        {
            bpLShoulder.Visible = true;
            bpLShoulder.Text = "Left Shoulder";
            chkStextbox.Text = "Left Shoulder";
            chkStextbox.Checked = true;
            if (chkRightShoulder.Checked && chkLeftShoulder.Checked)
            {
                chkStextbox.Text = "Right_Left Sholder";
            }

            CheckBox btn = new CheckBox();
            btn.Checked = true;
            chkStextbox_CheckedChanged(btn, e);
        }
        else
        {
            bpLShoulder.Visible = false;
            bpLShoulder.Text = string.Empty;
        }

        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }


    protected void chkRightShoulder_CheckedChanged(object sender, EventArgs e)
    {
        selectedbodypartsoap();
        binddynamicdataonchanges();
        if (chkRightShoulder.Checked)
        {
            bpRShoulder.Visible = true;
            bpRShoulder.Text = "Right Shoulder";
            chkStextbox.Text = "Right Sholder";
            chkStextbox.Checked = true;
            if (chkRightShoulder.Checked && chkLeftShoulder.Checked)
            {
                chkStextbox.Text = "Right_Left Sholder";
            }

            CheckBox btn = new CheckBox();
            btn.Checked = true;
            chkStextbox_CheckedChanged(btn, e);
        }
        else
        {
            bpRShoulder.Visible = false;
            bpRShoulder.Text = string.Empty;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkLeftHip_CheckedChanged(object sender, EventArgs e)
    {
        selectedbodypartsoap();
        binddynamicdataonchanges();
        if (chkLeftHip.Checked)
        {
            bpLHip.Visible = true;
            bpLHip.Text = "Left Hip";
            chkHtextbox.Text = "Left Hip";
            chkHtextbox.Checked = true;
            if (chkRightHip.Checked && chkLeftHip.Checked)
            {
                chkHtextbox.Text = "Right_Left Hip";
            }
            CheckBox btn = new CheckBox();
            btn.Checked = true;
            chkHtextbox_CheckedChanged(btn, e);
        }
        else
        {
            bpLHip.Visible = false;
            bpLHip.Text = string.Empty;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkRightHip_CheckedChanged(object sender, EventArgs e)
    {
        selectedbodypartsoap();
        binddynamicdataonchanges();
        if (chkRightHip.Checked)
        {
            bpRHip.Visible = true;
            bpRHip.Text = "Right Hip";
            chkHtextbox.Text = "Right Hip";
            chkHtextbox.Checked = true;
            if (chkRightHip.Checked && chkLeftHip.Checked)
            {
                chkHtextbox.Text = "Right_Left Hip";
            }

            CheckBox btn = new CheckBox();
            btn.Checked = true;
            chkHtextbox_CheckedChanged(btn, e);
        }
        else
        {
            bpRHip.Visible = false;
            bpRHip.Text = string.Empty;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkLeftKnee_CheckedChanged(object sender, EventArgs e)
    {
        selectedbodypartsoap();
        binddynamicdataonchanges();
        if (chkLeftKnee.Checked)
        {
            bpLKnee.Visible = true;
            bpLKnee.Text = "Left Knee";
            chkKtextbox.Text = "Left Knee";
            chkKtextbox.Checked = true;
            if (chkRightKnee.Checked && chkLeftKnee.Checked)
            {
                chkHtextbox.Text = "Right_Left Knee";
            }
            CheckBox btn = new CheckBox();
            btn.Checked = true;
            chkKtextbox_CheckedChanged(btn, e);
        }
        else
        {
            bpLKnee.Visible = false;
            bpLKnee.Text = string.Empty;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkRightKnee_CheckedChanged(object sender, EventArgs e)
    {
        selectedbodypartsoap();
        binddynamicdataonchanges();
        if (chkRightKnee.Checked)
        {
            bpRKnee.Visible = true;
            bpRKnee.Text = "Right Knee";
            chkKtextbox.Text = "Right Knee";
            chkKtextbox.Checked = true;
            if (chkRightKnee.Checked && chkLeftKnee.Checked)
            {
                chkKtextbox.Text = "Right_Left Knee";
            }
            CheckBox btn = new CheckBox();
            btn.Checked = true;
            chkKtextbox_CheckedChanged(btn, e);
        }
        else
        {
            bpRKnee.Visible = false;
            bpRKnee.Text = string.Empty;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkLeftAnkleFoot_CheckedChanged(object sender, EventArgs e)
    {
        selectedbodypartsoap();
        binddynamicdataonchanges();
        if (chkLeftAnkleFoot.Checked)
        {
            bpLAnkleFoot.Visible = true;
            bpLAnkleFoot.Text = "Left Ankle/Foot";

            chkAtextbox.Text = "Left Ankle/Foot";
            chkAtextbox.Checked = true;
            if (chkLeftAnkleFoot.Checked && chkRightAnkleFoot.Checked)
            {
                chkAtextbox.Text = "Right_Left Ankle/Foot";
            }

            CheckBox btn = new CheckBox();
            btn.Checked = true;
            chkAtextbox_CheckedChanged(btn, e);
        }
        else
        {
            bpLAnkleFoot.Visible = false;
            bpLAnkleFoot.Text = string.Empty;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkRightAnkleFoot_CheckedChanged(object sender, EventArgs e)
    {
        selectedbodypartsoap();
        binddynamicdataonchanges();
        if (chkRightAnkleFoot.Checked)
        {
            bpRAnkleFoot.Visible = true;
            bpRAnkleFoot.Text = "Right Ankle/Foot";

            chkAtextbox.Text = "Right Ankle/Foot";
            chkAtextbox.Checked = true;
            if (chkLeftAnkleFoot.Checked && chkRightAnkleFoot.Checked)
            {
                chkAtextbox.Text = "Right_Left Ankle/Foot";
            }
            CheckBox btn = new CheckBox();
            btn.Checked = true;
            chkAtextbox_CheckedChanged(btn, e);
        }
        else
        {
            bpRAnkleFoot.Visible = false;
            bpRAnkleFoot.Text = string.Empty;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkWC_CheckedChanged(object sender, EventArgs e)
    {
        if (chkWC.Checked)
        {
            txtwccheck.Visible = true;
            txtwccheck.Text = "WC injury details _______ (Mechanism of injury to involved body parts / Patient is ___not working)";
        }
        else
        {
            txtwccheck.Text = string.Empty;
            txtwccheck.Visible = false;

        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkStextbox_CheckedChanged(object sender, EventArgs e)
    {
        if (chkStextbox.Checked)
        {
            string selectedpart = string.Empty;

            if (chkLeftShoulder.Checked)
            { selectedpart = " Left Sholuder "; }
            if (chkRightShoulder.Checked)
            { selectedpart = " Right Shoulder "; }

            if (chkLeftShoulder.Checked && chkRightShoulder.Checked)
            { selectedpart = " Left and Right Sholuder "; }


            txtStext.Visible = true;
            txtStext.Text = "Examination of the shoulder revealed no tenderness to palpation. There was no effusion. No crepitus was present. No atrophy was present. Hawkins, drop arm, and apprehension tests were negative.  Range of motion Abduction __ degrees(180 degrees normal )  Forward flexion __ degrees(180 degrees normal )  Internal rotation __ degrees (80 degrees normal )  External rotation __ degrees(90 degrees normal ) ";
        }
        else
        {
            txtStext.Text = string.Empty;
            txtStext.Visible = false;

        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkAtextbox_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAtextbox.Checked)
        {
            txtAtextbox.Visible = true;
            txtAtextbox.Text = "Examination reveals a _____. There is no heat, swelling, effusion, erythema, crepitus, instability, or atrophy appreciated. Range of motion reveals dorsiflexion at __ degrees (20 degrees normal), plantar flexion at __ degrees (40 degrees normal), sub inversion at __ degrees (30 degrees normal), and sub eversion at __ degrees (20 degrees normal).  Drawer – negative. ";
        }
        else
        {
            txtAtextbox.Text = string.Empty;
            txtAtextbox.Visible = false;

        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkHtextbox_CheckedChanged(object sender, EventArgs e)
    {
        if (chkHtextbox.Checked)
        {
            txtHtextbox.Visible = true;
            txtHtextbox.Text = "Examination observation and palpation of the hip is positive for pain-limited range of motion, tenderness with muscle spasm and atrophy noted at lower extremity. Range of motion reveals  flexion __ (100  degrees normal)with pain at end range of motion;  extension __ (30 degrees normal) with pain at end range of motion ;abduction __ (40  degrees normal) with pain at end range of motion; adduction __ (20  degrees normal) with pain at end range of motion;  internal rotation __ (50  degrees normal)with pain at end range of motion; external rotation __ ( 40  degrees normal) with pain at end range of motion. Muscle strength is __/5.";
        }
        else
        {
            txtHtextbox.Text = string.Empty;
            txtHtextbox.Visible = false;

        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkKtextbox_CheckedChanged(object sender, EventArgs e)
    {
        if (chkKtextbox.Checked)
        {
            txtKtextbox.Visible = true;
            txtKtextbox.Text = "Examination of the knee revealed no tenderness on palpation. There was no effusion. There was no atrophy of the quadriceps noted. Lachman’s test was negative. Anterior drawer sign and Posterior drawer sign were each negative. Patellofemoral crepitus was not present. Valgus & Varus stress test was stable. Range of motion Flexion __ degrees(150 degrees normal ) Extension __ degrees(0 degrees normal ) The calf touches the back of the thigh at __ degrees of flexion (normal for the patient). ";
        }
        else
        {
            txtKtextbox.Text = string.Empty;
            txtKtextbox.Visible = false;

        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkAKnee_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAKnee.Checked)
        {
            string selectedpart = string.Empty;

            if (chkLeftKnee.Checked)
            { selectedpart = "Left"; }
            if (chkRightKnee.Checked)
            { selectedpart = "Right"; }

            if (chkLeftKnee.Checked && chkRightKnee.Checked)
            { selectedpart = "Left and Right"; }


            txtchkAKnee.Visible = true;
            txtchkAKnee.Text = "The patient has failed conservative management which has included physical therapy, oral medications.  The MRI was reviewed with the patient as well as the clinical examination findings.  I have gone over all treatment options with the patient.  At this time, I have discussed the benefits and risks of " + selectedpart + " knee arthroscopy, chondroplasty, synovectomy, partial vs total meniscectomy and all other related procedures with the patient.  I answered all their questions in regards to the procedure. The patient verbally consents to the procedure and will be scheduled on _______.";
        }
        else
        {
            txtchkAKnee.Text = string.Empty;
            txtchkAKnee.Visible = false;

        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkAshoulder_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAshoulder.Checked)
        {
            string selectedpart = string.Empty;

            if (chkLeftShoulder.Checked)
            { selectedpart = "Left"; }
            if (chkRightShoulder.Checked)
            { selectedpart = "Right"; }

            if (chkLeftShoulder.Checked && chkRightShoulder.Checked)
            { selectedpart = "Left and Right"; }

            txtchkAshoulder.Visible = true;
            txtchkAshoulder.Text = "The patient has failed conservative management which has included physical therapy, oral medications, and injections.  The MRI was reviewed with the patient as well as the clinical examination findings.  I have gone over all treatment options with the patient.  At this time, I have discussed the benefits and risks of " + selectedpart + " shoulder arthroscopy, acromioplasty, subacromial decompression, debridement of rotator cuff versus possible rotator cuff repair, biceps tenotomy versus tenodesis and all other related procedures with the patient.  I answered all their questions in regards to the procedure. The patient verbally consents to the procedure and will be scheduled on ______.";
        }
        else
        {
            txtchkAshoulder.Text = string.Empty;
            txtchkAshoulder.Visible = false;

        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void chkAOther_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAOther.Checked)
        {
            txtchkAOther.Visible = true;
            txtchkAOther.Text = "The patient has failed conservative management which has included physical therapy, oral medications, and injections.  The MRI was reviewed with the patient as well as the clinical examination findings.  I have gone over all treatment options with the patient.  At this time, I have discussed the benefits and risks of _____ shoulder arthroscopy, acromioplasty, subacromial decompression, debridement of rotator cuff versus possible rotator cuff repair, biceps tenotomy versus tenodesis and all other related procedures with the patient.  I answered all their questions in regards to the procedure. The patient verbally consents to the procedure and will be scheduled on ______.";
        }
        else
        {
            txtchkAOther.Text = string.Empty;
            txtchkAOther.Visible = false;

        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void btnupdaterecords_Click(object sender, EventArgs e)
    {

        //hdneditedsoapid.Value
        string SP = "";
        SqlParameter[] param = null;

        //starting the updatating the data. 

        DBHelperClass db = new DBHelperClass();

        param = new SqlParameter[56];
        SP = "saveupdatesoap";

        param[0] = new SqlParameter("@PatientIE_ID", hdnrodieid.Value);
        param[1] = new SqlParameter("@PatientFU_ID", hdnrodfuid.Value);
        param[2] = new SqlParameter("@Patient_Name", lblName.Text);
        param[3] = new SqlParameter("@DOB", txtdobsoap.Text);
        param[4] = new SqlParameter("@DOS", txtCreateSoapDate.Text);
        param[5] = new SqlParameter("@chkLeftShoulder", chkLeftShoulder.Checked);
        param[6] = new SqlParameter("@chkRightShoulder", chkRightShoulder.Checked);
        param[7] = new SqlParameter("@chkLeftHip", chkLeftHip.Checked);
        param[8] = new SqlParameter("@chkRightHip", chkRightHip.Checked);
        param[9] = new SqlParameter("@chkLeftKnee", chkLeftKnee.Checked);
        param[10] = new SqlParameter("@chkRightKnee", chkRightKnee.Checked);
        param[11] = new SqlParameter("@chkLeftAnkleFoot", chkLeftAnkleFoot.Checked);
        param[12] = new SqlParameter("@chkRightAnkleFoot", chkRightAnkleFoot.Checked);
        param[13] = new SqlParameter("@txtHistoryPresentillness", txtHistoryPresentillness.Text);
        param[14] = new SqlParameter("@chkWC", chkWC.Checked);
        param[15] = new SqlParameter("@txtwccheck", txtwccheck.Text);
        param[16] = new SqlParameter("@bpLShoulder", bpLShoulder.Text);
        param[17] = new SqlParameter("@bpRShoulder", bpRShoulder.Text);
        param[18] = new SqlParameter("@bpLHip", bpLHip.Text);
        param[19] = new SqlParameter("@bpRHip", bpRHip.Text);
        param[20] = new SqlParameter("@bpLKnee", bpLKnee.Text);
        param[21] = new SqlParameter("@bpRKnee", bpRKnee.Text);
        param[22] = new SqlParameter("@bpLAnkleFoot", bpLAnkleFoot.Text);
        param[23] = new SqlParameter("@bpRAnkleFoot", bpRAnkleFoot.Text);
        param[24] = new SqlParameter("@txtPastMedicalHistory", txtPastMedicalHistory.Text);
        param[25] = new SqlParameter("@txtpastsurgicalhistory", txtpastsurgicalhistory.Text);
        param[26] = new SqlParameter("@txtpastaccideninjuries", txtpastaccideninjuries.Text);
        param[27] = new SqlParameter("@txtdailyMedications", txtdailyMedications.Text);
        param[28] = new SqlParameter("@txtAllergies", txtAllergies.Text);
        param[29] = new SqlParameter("@txtSocialHistory", txtSocialHistory.Text);
        param[30] = new SqlParameter("@txtPhysicalExamination", txtPhysicalExamination.Text);
        param[31] = new SqlParameter("@chkStextbox", chkStextbox.Checked);
        param[32] = new SqlParameter("@txtStext", txtStext.Text);
        param[33] = new SqlParameter("@chkAtextbox", chkAtextbox.Checked);
        param[34] = new SqlParameter("@txtAtextbox", txtAtextbox.Text);
        param[35] = new SqlParameter("@chkHtextbox", chkHtextbox.Checked);
        param[36] = new SqlParameter("@txtHtextbox", txtHtextbox.Text);
        param[37] = new SqlParameter("@chkKtextbox", chkKtextbox.Checked);
        param[38] = new SqlParameter("@txtKtextbox", txtKtextbox.Text);
        param[39] = new SqlParameter("@txtDiagnosticImaging", txtDiagnosticImaging.Text);
        param[40] = new SqlParameter("@txtAssestmentplan", txtAssestmentplan.Text);
        param[41] = new SqlParameter("@chkAshoulder", chkAshoulder.Checked);
        param[42] = new SqlParameter("@txtchkAshoulder", txtchkAshoulder.Text);
        param[43] = new SqlParameter("@chkAOther", chkAOther.Checked);
        param[44] = new SqlParameter("@txtchkAOther", txtchkAOther.Text);
        param[45] = new SqlParameter("@txtExaminedResult", txtExaminedResult.Text);
        param[46] = new SqlParameter("@txtDefault", txtDefault.Text);
        param[47] = new SqlParameter("@SoapID", hdneditedsoapid.Value);
        param[48] = new SqlParameter("@chkAKnee", chkAKnee.Checked);
        param[49] = new SqlParameter("@txtchkAKnee", txtchkAKnee.Text);

        param[50] = new SqlParameter("@chkLeftOther", chkLeftOther.Checked);
        //  param[51] = new SqlParameter("@chkRightOther", chkRightOther.Checked);
        param[51] = new SqlParameter("@chkRightOther", false);

        param[52] = new SqlParameter("@bpLOtherFoot", bpLOtherFoot.Text);
        //  param[53] = new SqlParameter("@bpROtherFoot", bpROtherFoot.Text);
        param[53] = new SqlParameter("@bpROtherFoot", null);

        param[54] = new SqlParameter("@chkOtextbox", chkOtextbox.Checked);
        param[55] = new SqlParameter("@txtOtextbox", txtOtextbox.Text);

        //Insert values in the db.
        int val = db.executeSP(SP, param);

    }

    protected void lnkiesoapedit_Click(object sender, EventArgs e)
    {
        LinkButton btncreate = (LinkButton)(sender);
        LinkButton btn = new LinkButton();
        btn.CommandArgument = btncreate.CommandArgument;
        clearsoap();
        hdneditedsoapid.Value = btn.CommandArgument;
        string query2 = "select * from tblsoap where ID=" + hdneditedsoapid.Value;

        DataSet ds1 = gDbhelperobj.selectData(query2);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            hdnrodieid.Value = Convert.ToString(ds1.Tables[0].Rows[0]["PatientIE_ID"]);
            hdnrodfuid.Value = Convert.ToString(ds1.Tables[0].Rows[0]["PatientFU_ID"]);
            lblName.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Patient_Name"]);
            txtdobsoap.Text = Convert.ToString(ds1.Tables[0].Rows[0]["DOB"]);
            txtCreateSoapDate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["DOS"]);
            chkLeftShoulder.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkLeftShoulder"]);
            chkRightShoulder.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkRightShoulder"]);
            chkLeftHip.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkLeftHip"]);
            chkRightHip.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkRightHip"]);
            chkLeftKnee.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkLeftKnee"]);
            chkRightKnee.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkRightKnee"]);
            chkLeftAnkleFoot.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkLeftAnkleFoot"]);
            chkRightAnkleFoot.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkRightAnkleFoot"]);
            txtHistoryPresentillness.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtHistoryPresentillness"]);
            chkWC.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkWC"]);
            txtwccheck.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtwccheck"]);

            if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpLShoulder"])))
            {
                bpLShoulder.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpLShoulder"]);
                bpLShoulder.Visible = true;
            }

            if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpRShoulder"])))
            {
                bpRShoulder.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpRShoulder"]);
                bpRShoulder.Visible = true;
            }

            if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpLHip"])))
            {
                bpLHip.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpLHip"]);
                bpLHip.Visible = true;
            }

            if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpRHip"])))
            {
                bpRHip.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpRHip"]);
                bpRHip.Visible = true;
            }

            if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpLKnee"])))
            {
                bpLKnee.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpLKnee"]);
                bpLKnee.Visible = true;
            }

            if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpRKnee"])))
            {
                bpRKnee.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpRKnee"]);
                bpRKnee.Visible = true;
            }

            if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpLAnkleFoot"])))
            {
                bpLAnkleFoot.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpLAnkleFoot"]);
                bpLAnkleFoot.Visible = true;
            }

            if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpRAnkleFoot"])))
            {
                bpRAnkleFoot.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpRAnkleFoot"]);
                bpRAnkleFoot.Visible = true;
            }


            txtPastMedicalHistory.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtPastMedicalHistory"]);
            txtpastsurgicalhistory.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtpastsurgicalhistory"]);
            txtpastaccideninjuries.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtpastaccideninjuries"]);
            txtdailyMedications.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtdailyMedications"]);
            txtAllergies.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtAllergies"]);
            txtSocialHistory.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtSocialHistory"]);
            txtPhysicalExamination.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtPhysicalExamination"]);
            chkStextbox.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkStextbox"]);
            txtStext.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtStext"]);
            chkAtextbox.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkAtextbox"]);
            txtAtextbox.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtAtextbox"]);
            chkHtextbox.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkHtextbox"]);
            txtHtextbox.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtHtextbox"]);
            chkKtextbox.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkKtextbox"]);
            txtKtextbox.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtKtextbox"]);
            txtDiagnosticImaging.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtDiagnosticImaging"]);
            txtAssestmentplan.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtAssestmentplan"]);
            chkAshoulder.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkAshoulder"]);
            txtchkAshoulder.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtchkAshoulder"]);
            chkAOther.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkAOther"]);
            txtchkAOther.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtchkAOther"]);
            txtExaminedResult.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtExaminedResult"]);
            chkAKnee.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkAKnee"]);
            txtchkAKnee.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtchkAKnee"]);

            chkLeftOther.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkLeftOther"]);

            if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpLOtherFoot"])))
            {
                bpLOtherFoot.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpLOtherFoot"]);
                bpLOtherFoot.Visible = true;
            }

            chkOtextbox.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkOtextbox"]);
            txtOtextbox.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtOtextbox"]);

            btnsavesoap.Visible = false;
            btnupdaterecords.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
        }

    }
    public void clearsoap()
    {
        personage.Value = string.Empty;
        persongender.Value = string.Empty;
        hdneditedsoapid.Value = string.Empty;
        hdnrodieid.Value = string.Empty;
        //lnkiesoapedit.Visible = false;
        lblName.Text = string.Empty;
        txtdobsoap.Text = string.Empty;
        txtCreateSoapDate.Text = string.Empty;
        chkLeftShoulder.Checked = false;
        chkRightShoulder.Checked = false;
        chkLeftHip.Checked = false;
        chkRightHip.Checked = false;
        chkLeftKnee.Checked = false;
        chkRightKnee.Checked = false;
        chkLeftAnkleFoot.Checked = false;
        chkRightAnkleFoot.Checked = false;
        txtHistoryPresentillness.Text = string.Empty;
        chkWC.Checked = false;
        txtwccheck.Text = string.Empty;
        bpLShoulder.Text = string.Empty;
        bpRShoulder.Text = string.Empty;
        bpLHip.Text = string.Empty;
        bpRHip.Text = string.Empty;
        bpLKnee.Text = string.Empty;
        bpRKnee.Text = string.Empty;
        bpLAnkleFoot.Text = string.Empty;
        bpRAnkleFoot.Text = string.Empty;
        txtPastMedicalHistory.Text = string.Empty;
        txtpastsurgicalhistory.Text = string.Empty;
        txtpastaccideninjuries.Text = string.Empty;
        txtdailyMedications.Text = string.Empty;
        txtAllergies.Text = string.Empty;
        txtSocialHistory.Text = string.Empty;
        txtPhysicalExamination.Text = string.Empty;
        chkStextbox.Checked = false;
        txtStext.Text = string.Empty;
        chkAtextbox.Checked = false;
        txtAtextbox.Text = string.Empty;
        chkHtextbox.Checked = false;
        txtHtextbox.Text = string.Empty;
        chkKtextbox.Checked = false;
        txtKtextbox.Text = string.Empty;
        txtDiagnosticImaging.Text = string.Empty;
        txtAssestmentplan.Text = string.Empty;
        chkAshoulder.Checked = false;
        txtchkAshoulder.Text = string.Empty;
        chkAOther.Checked = false;
        txtchkAOther.Text = string.Empty;
        txtExaminedResult.Text = string.Empty;
        chkAKnee.Checked = false;
        txtchkAKnee.Text = string.Empty;


        chkLeftOther.Checked = false;
        bpLOtherFoot.Text = string.Empty;
        bpLOtherFoot.Visible = false;
        chkOtextbox.Checked = false;
        txtOtextbox.Text = string.Empty;


    }

    protected void lnkprintsoap_Click(object sender, EventArgs e)
    {

        LinkButton btnsoapprint = (LinkButton)(sender);
        LinkButton btn = new LinkButton();
        btn.CommandArgument = btnsoapprint.CommandArgument;

        string query = string.Empty;
        int id = Convert.ToInt32(btn.CommandArgument.Split(',')[0]);
        int srno = Convert.ToInt32(btn.CommandArgument.Split(',')[1]);
        query = "select s.* from tblsoap s where s.ID =  " + id;

        DataTable dt = null;

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connString_NV_Jo"].ConnectionString))
        {

            SqlCommand cm = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
        }

        #region For print single report
        string PatientName = string.Empty;
        string DOS = string.Empty;
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTable tblFiltered = null;
            EnumerableRowCollection<DataRow> query1 = from row in dt.AsEnumerable()
                                                      orderby DateTime.Parse(row.Field<string>("DOS")) ascending
                                                      select row;
            tblFiltered = query1.AsDataView().ToTable();
            PatientName = Convert.ToString(tblFiltered.Rows[0].ItemArray[4]);
            DOS = Convert.ToString(tblFiltered.Rows[0].ItemArray[6]);
            string optype = string.Empty;
            if (srno > 1)
            {
                optype = "OFU";
            }
            else { optype = "OC"; }


            string fileName = Server.MapPath("~/document/SoapReport/" + PatientName + optype + DOS.Replace("/","") + ".docx");
            string bodypart = string.Empty;
            // Create the proper formats first

            using (Xceed.Words.NET.DocX doc = Xceed.Words.NET.DocX.Create(fileName))
            {
                foreach (DataRow r in tblFiltered.Rows)
                {
                    //var firstSpaceIndex = Convert.ToString(r["Patient_Name"]).IndexOf("");
                    //PatientName = Convert.ToString(r["Patient_Name"]).Insert(firstSpaceIndex, ",");
                    PatientName = Convert.ToString(r["Patient_Name"]);
                    //doc.InsertParagraph("SOAP").Bold().FontSize(10d).SpacingAfter(10d).Font("Times New Roman").Alignment = Xceed.Document.NET.Alignment.center;
                    doc.InsertParagraph("Patient Name: ").Bold().FontSize(12d).Font("Times New Roman").Append(PatientName.ToUpper()).FontSize(12d).Font("Times New Roman");
                    doc.InsertParagraph("Date of Birth: ").Bold().FontSize(12d).Font("Times New Roman").Append(Convert.ToString(r["DOB"])).FontSize(12d).Font("Times New Roman");
                    if (!string.IsNullOrEmpty(Convert.ToString(r["DOS"])))
                    {
                        doc.InsertParagraph("Date of Service: ").Bold().FontSize(12d).Font("Times New Roman").Append(Convert.ToString(r["DOS"])).FontSize(10d).SpacingAfter(12d).Font("Times New Roman");
                    }




                    //body part selected string. 
                    if (Convert.ToBoolean(r["chkLeftShoulder"]))
                    { bodypart += chkLeftShoulder.Text + ", "; }
                    else
                    { bodypart.Replace(chkLeftShoulder.Text + ", ", ""); }

                    if (Convert.ToBoolean(r["chkRightShoulder"]))
                    { bodypart += chkRightShoulder.Text + ", "; }
                    else
                    { bodypart.Replace(chkRightShoulder.Text + ", ", ""); }

                    if (Convert.ToBoolean(r["chkLeftHip"]))
                    { bodypart += chkLeftHip.Text + ", "; }
                    else
                    { bodypart.Replace(chkLeftHip.Text + ", ", ""); }

                    if (Convert.ToBoolean(r["chkRightHip"]))
                    { bodypart += chkRightHip.Text + ", "; }
                    else
                    { bodypart.Replace(chkRightHip.Text + ", ", ""); }

                    if (Convert.ToBoolean(r["chkLeftKnee"]))
                    { bodypart += chkLeftKnee.Text + ", "; }
                    else
                    { bodypart.Replace(chkLeftKnee.Text + ", ", ""); }

                    if (Convert.ToBoolean(r["chkRightKnee"]))
                    { bodypart += chkRightKnee.Text + ", "; }
                    else
                    { bodypart.Replace(chkRightKnee.Text + ", ", ""); }

                    if (Convert.ToBoolean(r["chkLeftAnkleFoot"]))
                    { bodypart += chkLeftAnkleFoot.Text + ", "; }
                    else
                    { bodypart.Replace(chkLeftAnkleFoot.Text + ", ", ""); }

                    if (Convert.ToBoolean(r["chkRightAnkleFoot"]))
                    { bodypart += chkRightAnkleFoot.Text + ", "; }
                    else
                    { bodypart.Replace(chkRightAnkleFoot.Text + ", ", ""); }


                    if (Convert.ToBoolean(r["chkLeftOther"]))
                    { bodypart += chkLeftOther.Text + ", "; }
                    else
                    { bodypart.Replace(chkLeftOther.Text + ", ", ""); }

                    //if (Convert.ToBoolean(r["chkRightOther"]))
                    //{ bodypart += chkRightOther.Text + ", "; }
                    //else
                    //{ bodypart.Replace(chkRightOther.Text + ", ", ""); }


                    bodypart = Convert.ToString(bodypart).TrimEnd(' ').TrimEnd(',');

                    //if (!string.IsNullOrEmpty(bodypart))
                    //{
                    //    doc.InsertParagraph(" ").Bold().FontSize(12d).Append("\n" + Convert.ToString(bodypart)).FontSize(10d).SpacingAfter(10d).Font("Times New Roman");
                    //}

                    doc.InsertParagraph("History of Present Illness:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtHistoryPresentillness"] + "\n")).FontSize(12d).Font("Times New Roman");

                    if (!string.IsNullOrEmpty(Convert.ToString(r["txtwccheck"])) && Convert.ToBoolean(Convert.ToString(r["chkWC"])))
                    {
                        doc.InsertParagraph("WC injury details:").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtwccheck"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(r["bpLShoulder"])) && Convert.ToBoolean(Convert.ToString(r["chkLeftShoulder"])))
                    {
                        //doc.InsertParagraph("Left Shoulder:").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpLShoulder"]) + "\n");
                        doc.InsertParagraph(" ").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpLShoulder"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(r["bpRShoulder"])) && Convert.ToBoolean(Convert.ToString(r["chkRightShoulder"])))
                    {
                        // doc.InsertParagraph("Right Shoulder:").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpRShoulder"]) + "\n");
                        doc.InsertParagraph(" ").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpRShoulder"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(r["bpLHip"])) && Convert.ToBoolean(Convert.ToString(r["chkLeftHip"])))
                    {
                        //doc.InsertParagraph("Left Hip:").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpLHip"]) + "\n");
                        doc.InsertParagraph(" ").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpLHip"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(r["bpRHip"])) && Convert.ToBoolean(Convert.ToString(r["chkRightHip"])))
                    {
                        //doc.InsertParagraph("Right Hip:").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpRHip"]) + "\n");
                        doc.InsertParagraph(" ").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpRHip"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(r["bpLKnee"])) && Convert.ToBoolean(Convert.ToString(r["chkLeftKnee"])))
                    {
                        //doc.InsertParagraph("Left Knee:").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpLKnee"]) + "\n");
                        doc.InsertParagraph(" ").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpLKnee"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(r["bpRKnee"])) && Convert.ToBoolean(Convert.ToString(r["chkRightKnee"])))
                    {
                        //doc.InsertParagraph("Right Knee:").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpRKnee"]) + "\n");
                        doc.InsertParagraph(" ").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpRKnee"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(r["bpLAnkleFoot"])) && Convert.ToBoolean(Convert.ToString(r["chkLeftAnkleFoot"])))
                    {
                        //doc.InsertParagraph("Left Ankle/Foot:").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpLAnkleFoot"]) + "\n");
                        doc.InsertParagraph(" ").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpLAnkleFoot"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(r["bpRAnkleFoot"])) && Convert.ToBoolean(Convert.ToString(r["chkRightAnkleFoot"])))
                    {
                        //doc.InsertParagraph("Right Ankle/Foot:").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpRAnkleFoot"]) + "\n");
                        doc.InsertParagraph(" ").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpRAnkleFoot"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }


                    if (!string.IsNullOrEmpty(Convert.ToString(r["bpLOtherFoot"])) && Convert.ToBoolean(Convert.ToString(r["chkLeftOther"])))
                    {
                        //doc.InsertParagraph("Left Ankle/Foot:").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpLAnkleFoot"]) + "\n");
                        doc.InsertParagraph(" ").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpLOtherFoot"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }
                    //if (!string.IsNullOrEmpty(Convert.ToString(r["bpROtherFoot"])) && Convert.ToBoolean(Convert.ToString(r["chkRightOther"])))
                    //{
                    //    //doc.InsertParagraph("Right Ankle/Foot:").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpRAnkleFoot"]) + "\n");
                    //    doc.InsertParagraph(" ").Bold().FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["bpROtherFoot"]) + "\n").FontSize(12d).Font("Times New Roman");
                    //}




                    doc.InsertParagraph("Past Medical History:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtPastMedicalHistory"]) + "\n").FontSize(12d).Font("Times New Roman");
                    doc.InsertParagraph("Past Surgical History:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtpastsurgicalhistory"]) + "\n").FontSize(12d).Font("Times New Roman");
                    doc.InsertParagraph("Past Accident/Injuries:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtpastaccideninjuries"]) + "\n").FontSize(12d).Font("Times New Roman");
                    doc.InsertParagraph("Daily Medications:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtdailyMedications"]) + "\n").FontSize(12d).Font("Times New Roman");
                    doc.InsertParagraph("Allergies:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtAllergies"]) + "\n").FontSize(12d).Font("Times New Roman");
                    doc.InsertParagraph("Social History:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtSocialHistory"]) + "\n").FontSize(12d).Font("Times New Roman");
                    //doc.InsertParagraph("Physical Examination:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtPhysicalExamination"]) + "\n").ReplaceText("Vitals:", "Vitals:", false, System.Text.RegularExpressions.RegexOptions.None, new Xceed.Words.NET.Formatting() { Bold = true });
                    doc.InsertParagraph("Physical Examination:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtPhysicalExamination"]) + "\n").FontSize(12d).Font("Times New Roman");



                    Xceed.Document.NET.Formatting fontFamily = new Xceed.Document.NET.Formatting();
                    fontFamily.UnderlineColor = System.Drawing.Color.Black;
                    fontFamily.Size = 12;
                    fontFamily.Bold = true;
                    doc.Paragraphs[(doc.Paragraphs.Count - 1)].ReplaceText("Vitals:", "Vitals:", false, System.Text.RegularExpressions.RegexOptions.None, fontFamily);
                    doc.Paragraphs[(doc.Paragraphs.Count - 1)].ReplaceText("General Appearance:", "General Appearance:", false, System.Text.RegularExpressions.RegexOptions.None, fontFamily);
                    doc.Paragraphs[(doc.Paragraphs.Count - 1)].ReplaceText("Gait and Station:", "Gait and Station:", false, System.Text.RegularExpressions.RegexOptions.None, fontFamily);

                    if (!string.IsNullOrEmpty(Convert.ToString(r["txtAtextbox"])) && Convert.ToBoolean(Convert.ToString(r["chkAtextbox"])))
                    {
                        string selectedpart = string.Empty;
                        if (Convert.ToBoolean(Convert.ToString(r["chkLeftAnkleFoot"])))
                        { selectedpart = "Left"; }
                        if (Convert.ToBoolean(Convert.ToString(r["chkRightAnkleFoot"])))
                        { selectedpart = "Right"; }

                        if (Convert.ToBoolean(Convert.ToString(r["chkLeftAnkleFoot"])) && Convert.ToBoolean(Convert.ToString(r["chkRightAnkleFoot"])))
                        { selectedpart = "Left and Right"; }

                        doc.InsertParagraph(selectedpart + " Ankle/Foot:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtAtextbox"]) + "\n").FontSize(12d).Font("Times New Roman");
                        //doc.InsertParagraph("").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtAtextbox"]) + "\n");
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(r["txtHtextbox"])) && Convert.ToBoolean(Convert.ToString(r["chkHtextbox"])))
                    {

                        string selectedpart = string.Empty;
                        if (Convert.ToBoolean(Convert.ToString(r["chkLeftHip"])))
                        { selectedpart = "Left"; }
                        if (Convert.ToBoolean(Convert.ToString(r["chkRightHip"])))
                        { selectedpart = "Right"; }

                        if (Convert.ToBoolean(Convert.ToString(r["chkLeftHip"])) && Convert.ToBoolean(Convert.ToString(r["chkRightHip"])))
                        { selectedpart = "Left and Right"; }

                        doc.InsertParagraph(selectedpart + " Hip:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtHtextbox"]) + "\n").FontSize(12d).Font("Times New Roman");
                        //doc.InsertParagraph("").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtHtextbox"]) + "\n");
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(r["txtKtextbox"])) && Convert.ToBoolean(Convert.ToString(r["chkKtextbox"])))
                    {

                        string selectedpart = string.Empty;
                        if (Convert.ToBoolean(Convert.ToString(r["chkLeftKnee"])))
                        { selectedpart = "Left"; }
                        if (Convert.ToBoolean(Convert.ToString(r["chkRightKnee"])))
                        { selectedpart = "Right"; }

                        if (Convert.ToBoolean(Convert.ToString(r["chkLeftKnee"])) && Convert.ToBoolean(Convert.ToString(r["chkRightKnee"])))
                        { selectedpart = "Left and Right"; }

                        doc.InsertParagraph(selectedpart + " Knee:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtKtextbox"]) + "\n").FontSize(12d).Font("Times New Roman");
                        //doc.InsertParagraph("").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtKtextbox"]) + "\n");
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(r["txtStext"])) && Convert.ToBoolean(Convert.ToString(r["chkStextbox"])))
                    {
                        string selectedpart = string.Empty;
                        if (Convert.ToBoolean(Convert.ToString(r["chkLeftShoulder"])))
                        { selectedpart = "Left"; }
                        if (Convert.ToBoolean(Convert.ToString(r["chkRightShoulder"])))
                        { selectedpart = "Right"; }

                        if (Convert.ToBoolean(Convert.ToString(r["chkLeftShoulder"])) && Convert.ToBoolean(Convert.ToString(r["chkRightShoulder"])))
                        { selectedpart = "Left and Right"; }

                        doc.InsertParagraph(selectedpart + " Shoulder:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtStext"]) + "\n").FontSize(12d).Font("Times New Roman");
                        //doc.InsertParagraph("").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtStext"]) + "\n");
                    }


                    if (!string.IsNullOrEmpty(Convert.ToString(r["txtOtextbox"])) && Convert.ToBoolean(Convert.ToString(r["chkOtextbox"])))
                    {
                        string selectedpart = string.Empty;
                        //if (Convert.ToBoolean(Convert.ToString(r["chkLeftOther"])))
                        //{ selectedpart = "Other"; }
                        //if (Convert.ToBoolean(Convert.ToString(r["chkRightOther"])))
                        //{ selectedpart = "Right"; }

                        //if (Convert.ToBoolean(Convert.ToString(r["chkLeftOther"])) && Convert.ToBoolean(Convert.ToString(r["chkRightOther"])))
                        //{ selectedpart = "Left and Right"; }

                        doc.InsertParagraph(selectedpart + "Other:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtOtextbox"]) + "\n").FontSize(12d).Font("Times New Roman");
                        //doc.InsertParagraph("").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtStext"]) + "\n");
                    }

                    doc.InsertParagraph("Diagnostic Imaging:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtDiagnosticImaging"]) + "\n").FontSize(12d).Font("Times New Roman");

                    doc.InsertParagraph("Assessment and Plan:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtAssestmentplan"]) + "\n").FontSize(12d).Font("Times New Roman");

                    if (!string.IsNullOrEmpty(Convert.ToString(r["txtchkAKnee"])) && Convert.ToBoolean(Convert.ToString(r["chkAKnee"])))
                    {
                        //doc.InsertParagraph("Knee:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtchkAKnee"]) + "\n");
                        doc.InsertParagraph(" ").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtchkAKnee"]) + "\n");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(r["txtchkAshoulder"])) && Convert.ToBoolean(Convert.ToString(r["chkAshoulder"])))
                    {
                        //doc.InsertParagraph("Sholder:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtchkAshoulder"]) + "\n");
                        doc.InsertParagraph(" ").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtchkAshoulder"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(r["txtchkAOther"])) && Convert.ToBoolean(Convert.ToString(r["chkAOther"])))
                    {
                        //doc.InsertParagraph("Other:").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtchkAOther"]) + "\n");
                        doc.InsertParagraph(" ").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtchkAOther"]) + "\n").FontSize(12d).Font("Times New Roman");
                    }

                    doc.InsertParagraph(" ").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtExaminedResult"]) + "\n").FontSize(12d).Font("Times New Roman");
                    doc.InsertParagraph(" ").Bold().UnderlineColor(System.Drawing.Color.Black).FontSize(12d).Font("Times New Roman").Append("\n" + Convert.ToString(r["txtDefault"]) + "\n").FontSize(12d).Font("Times New Roman");

                    // Add an image into the document.  
                    Xceed.Document.NET.Image image = null;
                    if (File.Exists(Server.MapPath("~/img/Sign/defaultSigl.jpg")))
                    {
                        image = doc.AddImage(Server.MapPath("~/img/Sign/defaultSigl.jpg"));
                    }
                    else
                    {
                        image = doc.AddImage(Server.MapPath("~/img/Sign/Blank.jpg"));
                    }

                    // Create a picture (A custom view of an Image).
                    Xceed.Document.NET.Picture picture = image.CreatePicture();

                    doc.InsertParagraph().AppendPicture(picture).UnderlineColor(System.Drawing.Color.Black);
                    doc.InsertParagraph("L Sean Thompson, M.D.").Bold();

                }


                // Add Headers and Footers to the document.
                doc.AddHeaders();
                doc.AddFooters();

                // Force the first page to have a different Header and Footer.
                doc.DifferentFirstPage = true;

                // Force odd & even pages to have different Headers and Footers.
                doc.DifferentOddAndEvenPages = true;

                // Insert a Paragraph into the first Header.
                doc.Headers.First.InsertParagraph("Prestige Pain Centers").Bold().Color(System.Drawing.Color.SkyBlue).FontSize(28).Font("Garamond").Alignment = Xceed.Document.NET.Alignment.center;
                doc.Headers.First.InsertParagraph("P.O. Box 370 \n Carteret, New Jersey 07008 \nT: (732) - 887 - 2004 \nF: (732) - 882 - 6364 \n ").FontSize(10).Font("Calibri").Alignment = Xceed.Document.NET.Alignment.center;

                // Insert a Paragraph into the even Header.
                doc.Headers.Even.InsertParagraph(PatientName).Bold().UnderlineColor(System.Drawing.Color.Black).Alignment = Xceed.Document.NET.Alignment.left;

                // Insert a Paragraph into the odd Header.
                doc.Headers.Odd.InsertParagraph(PatientName).Bold().UnderlineColor(System.Drawing.Color.Black).Alignment = Xceed.Document.NET.Alignment.left;

                //// Add the page number in the first Footer.
                //doc.Headers.First.InsertParagraph("Page").AppendPageNumber(Xceed.Document.NET.PageNumberFormat.normal);

                // Add the page number in the even Footers.
                doc.Headers.Even.InsertParagraph("Page").AppendPageNumber(Xceed.Document.NET.PageNumberFormat.normal);
                doc.Headers.Even.InsertParagraph("\n");

                // Add the page number in the odd Footers.
                doc.Headers.Odd.InsertParagraph("Page").AppendPageNumber(Xceed.Document.NET.PageNumberFormat.normal);
                doc.Headers.Odd.InsertParagraph("\n");


                //// Add the page number in the first Footer.
                //doc.Footers.First.InsertParagraph("Page").AppendPageNumber(Xceed.Document.NET.PageNumberFormat.normal);

                //// Add the page number in the even Footers.
                //doc.Footers.Even.InsertParagraph("Page").AppendPageNumber(Xceed.Document.NET.PageNumberFormat.normal);

                //// Add the page number in the odd Footers.
                //doc.Footers.Odd.InsertParagraph("Page").AppendPageNumber(Xceed.Document.NET.PageNumberFormat.normal);
                doc.Save();


            }
        }
        #endregion

    }

    protected void chkLeftOther_CheckedChanged(object sender, EventArgs e)
    {
        selectedbodypartsoap();
        binddynamicdataonchanges();
        if (chkLeftOther.Checked)
        {
            bpLOtherFoot.Visible = true;
            bpLOtherFoot.Text = "Other";

            chkOtextbox.Text = "Other";

            chkOtextbox.Checked = true;

            //if (chkRightOther.Checked && chkLeftOther.Checked)
            if (chkLeftOther.Checked)
            {
                chkOtextbox.Text = "Other";
            }

            CheckBox btn = new CheckBox();
            btn.Checked = true;
            chkOtextbox_CheckedChanged(btn, e);
        }
        else
        {
            bpLOtherFoot.Visible = false;
            bpLOtherFoot.Text = string.Empty;
        }

        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    //protected void chkRightOther_CheckedChanged(object sender, EventArgs e)
    //{
    //    selectedbodypartsoap();
    //    binddynamicdataonchanges();
    //    if (chkRightOther.Checked)
    //    {
    //        bpROtherFoot.Visible = true;
    //        bpROtherFoot.Text = "Right Other";

    //        chkOtextbox.Text = "Right Other";
    //        chkOtextbox.Checked = true;
    //        if (chkLeftOther.Checked && chkRightOther.Checked)
    //        {
    //            chkOtextbox.Text = "Right_Left Other";
    //        }
    //        CheckBox btn = new CheckBox();
    //        btn.Checked = true;
    //        chkOtextbox_CheckedChanged(btn, e);
    //    }
    //    else
    //    {
    //        bpROtherFoot.Visible = false;
    //        bpROtherFoot.Text = string.Empty;
    //    }
    //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    //}

    protected void chkOtextbox_CheckedChanged(object sender, EventArgs e)
    {
        if (chkOtextbox.Checked)
        {
            txtOtextbox.Visible = true;
            txtOtextbox.Text = "Examination reveals a _____. There is no heat, swelling, effusion, erythema, crepitus, instability, or atrophy appreciated. Range of motion reveals dorsiflexion at __ degrees (20 degrees normal), plantar flexion at __ degrees (40 degrees normal), sub inversion at __ degrees (30 degrees normal), and sub eversion at __ degrees (20 degrees normal).  Drawer – negative. ";
        }
        else
        {
            txtOtextbox.Text = string.Empty;
            txtOtextbox.Visible = false;

        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
    }

    protected void lnkFUsoap_Click(object sender, EventArgs e)
    {
        try
        {
            //  btnCreatnewFu.Visible = false;
            btnCreatnew.Visible = true;
            clearsoap();
            LinkButton btn = (LinkButton)(sender);
            btnCreatnew.CommandArgument = Convert.ToString(btn.CommandArgument);
            string patientie = btnCreatnew.CommandArgument.Split(',')[0];
            string patientfu = btnCreatnew.CommandArgument.Split(',')[1];
            //check for the value available or not in the soap table.
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connString_NV_Jo"].ConnectionString);
            DBHelperClass db = new DBHelperClass();
            string query = "select LEFT(DOS,9) AS DOS,* from tblSoap where PatientIE_ID = " + patientie + " and PatientFU_ID = " + patientfu;
            SqlCommand cm = new SqlCommand(query, cn);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            cn.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvEditSoap.DataSource = ds;
                gvEditSoap.DataBind();
                gvEditSoap.Visible = true;
                lblRecordnotfound.Visible = false;
            }
            else
            {
                gvEditSoap.Visible = false;
                lblRecordnotfound.Visible = true;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoapEditSoap();", true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void lnkFUsoap_Click1(object sender, EventArgs e)
    {
        try
        {
            hdnrodeditedfuid.Value = string.Empty;
            hdnrodeditedfuieid.Value = string.Empty;
            hdnSoapId.Value = string.Empty;
            btnsavesoap.Visible = true;
            btnupdaterecords.Visible = false;
            // end body parts. 
            btnupdaterecords.Visible = false;
            btnsavesoap.Visible = true;

            string SoapId = hdnrodieid.Value = hdnSoapId.Value = string.Empty;
            LinkButton btn = (LinkButton)(sender);
            DataTable dt = (DataTable)(Session["iedata"]);

            //if (btn.CommandArgument.Split('|').Count() > 1)
            //{
            //    hdnrodieid.Value = btn.CommandArgument.Split('|')[0];
            //    hdnSoapId.Value = btn.CommandArgument.Split('|')[1];
            //}


            if (btn.CommandArgument.Split(',').Count() > 1)
            {
                hdnrodieid.Value = btn.CommandArgument.Split(',')[0];
                hdnrodfuid.Value = btn.CommandArgument.Split(',')[1];

                //hdnSoapId.Value = btn.CommandArgument.Split('|')[1];
            }

            DataView dv = new DataView(dt);
            dv.RowFilter = "PatientIE_ID=" + Convert.ToInt32(hdnrodieid.Value); // query example = "id = 10"

            Session["ieid"] = hdnrodieid.Value;

            //check for the value available or not in the soap table.

            DataSet dsfudata = new DataSet();

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connString_NV_Jo"].ConnectionString);
            DBHelperClass db = new DBHelperClass();
            //string query = ("select * from tblSoap where PatientIE_ID= " + hdnrodieid.Value + " and PatientFU_ID = " + hdnrodfuid.Value);// + " and ID =" + hdnSoapId.Value);
            string query = ("select * from tblSoap where PatientIE_ID= " + hdnrodieid.Value + " and PatientFU_ID = (select MAX(PatientFU_ID) from tblSoap where PatientIE_ID = " + hdnrodieid.Value + ")  and ID = (select MAX(ID) from tblSoap where PatientIE_ID = " + hdnrodieid.Value + ")");
            SqlCommand cm = new SqlCommand(query, cn);
            SqlDataAdapter dafudata = new SqlDataAdapter(cm);
            cn.Open();
            dafudata.Fill(dsfudata);
            cn.Close();
            if (dsfudata.Tables[0].Rows.Count >= 1)
            {
                query = ("select * from tblSoap where PatientIE_ID= " + hdnrodieid.Value + " and PatientFU_ID = (select MAX(PatientFU_ID) from tblSoap where PatientIE_ID = " + hdnrodieid.Value + ")  and ID = (select MAX(ID) from tblSoap where PatientIE_ID = " + hdnrodieid.Value + ")");
            }
            else
            {
                query = ("select * from tblSoap where PatientIE_ID = " + hdnrodieid.Value + " and ID = (select MAX(ID) from tblSoap where PatientIE_ID = " + hdnrodieid.Value + ") and ID = (select MAX(ID) from tblSoap where PatientIE_ID = " + hdnrodieid.Value + ")");
            }

            SqlCommand cm1 = new SqlCommand(query, cn);
            SqlDataAdapter da = new SqlDataAdapter(cm1);
            cn.Open();

            DataSet ds1 = new DataSet();
            da.Fill(ds1);

            if (ds1.Tables[0].Rows.Count > 0)
            {
                hdnrodieid.Value = Convert.ToString(ds1.Tables[0].Rows[0]["PatientIE_ID"]);
                hdnrodfuid.Value = Convert.ToString(ds1.Tables[0].Rows[0]["PatientFU_ID"]);
                lblName.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Patient_Name"]);
                txtdobsoap.Text = Convert.ToString(ds1.Tables[0].Rows[0]["DOB"]);
                txtCreateSoapDate.Text = Convert.ToString(ds1.Tables[0].Rows[0]["DOS"]);
                chkLeftShoulder.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkLeftShoulder"]);
                chkRightShoulder.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkRightShoulder"]);
                chkLeftHip.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkLeftHip"]);
                chkRightHip.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkRightHip"]);
                chkLeftKnee.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkLeftKnee"]);
                chkRightKnee.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkRightKnee"]);
                chkLeftAnkleFoot.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkLeftAnkleFoot"]);
                chkRightAnkleFoot.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkRightAnkleFoot"]);
                txtHistoryPresentillness.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtHistoryPresentillness"]);
                chkWC.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkWC"]);
                txtwccheck.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtwccheck"]);

                if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpLShoulder"])))
                {
                    bpLShoulder.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpLShoulder"]);
                    bpLShoulder.Visible = true;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpRShoulder"])))
                {
                    bpRShoulder.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpRShoulder"]);
                    bpRShoulder.Visible = true;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpLHip"])))
                {
                    bpLHip.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpLHip"]);
                    bpLHip.Visible = true;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpRHip"])))
                {
                    bpRHip.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpRHip"]);
                    bpRHip.Visible = true;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpLKnee"])))
                {
                    bpLKnee.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpLKnee"]);
                    bpLKnee.Visible = true;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpRKnee"])))
                {
                    bpRKnee.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpRKnee"]);
                    bpRKnee.Visible = true;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpLAnkleFoot"])))
                {
                    bpLAnkleFoot.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpLAnkleFoot"]);
                    bpLAnkleFoot.Visible = true;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpRAnkleFoot"])))
                {
                    bpRAnkleFoot.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpRAnkleFoot"]);
                    bpRAnkleFoot.Visible = true;
                }


                txtPastMedicalHistory.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtPastMedicalHistory"]);
                txtpastsurgicalhistory.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtpastsurgicalhistory"]);
                txtpastaccideninjuries.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtpastaccideninjuries"]);
                txtdailyMedications.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtdailyMedications"]);
                txtAllergies.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtAllergies"]);
                txtSocialHistory.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtSocialHistory"]);
                txtPhysicalExamination.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtPhysicalExamination"]);
                chkStextbox.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkStextbox"]);
                txtStext.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtStext"]);
                chkAtextbox.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkAtextbox"]);
                txtAtextbox.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtAtextbox"]);
                chkHtextbox.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkHtextbox"]);
                txtHtextbox.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtHtextbox"]);
                chkKtextbox.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkKtextbox"]);
                txtKtextbox.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtKtextbox"]);
                txtDiagnosticImaging.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtDiagnosticImaging"]);
                txtAssestmentplan.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtAssestmentplan"]);
                chkAshoulder.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkAshoulder"]);
                txtchkAshoulder.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtchkAshoulder"]);
                chkAOther.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkAOther"]);
                txtchkAOther.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtchkAOther"]);
                txtExaminedResult.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtExaminedResult"]);
                chkAKnee.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkAKnee"]);
                txtchkAKnee.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtchkAKnee"]);

                chkLeftOther.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkLeftOther"]);

                if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0]["bpLOtherFoot"])))
                {
                    bpLOtherFoot.Text = Convert.ToString(ds1.Tables[0].Rows[0]["bpLOtherFoot"]);
                    bpLOtherFoot.Visible = true;
                }

                chkOtextbox.Checked = Convert.ToBoolean(ds1.Tables[0].Rows[0]["chkOtextbox"]);
                txtOtextbox.Text = Convert.ToString(ds1.Tables[0].Rows[0]["txtOtextbox"]);

                btnsavesoap.Visible = false;
                btnupdaterecords.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
            }



            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    if (dv != null)
            //    {
            //        lblName.Text = Convert.ToString(dv[0].Row.ItemArray[2]) + ", " + Convert.ToString(dv[0].Row.ItemArray[3]);//Last name +First Name;
            //        txtdobsoap.Text = !String.IsNullOrEmpty(Convert.ToString(dv[0].Row.ItemArray[4])) ? Convert.ToDateTime(dv[0].Row.ItemArray[4]).ToString("MM/dd/yyyy") : string.Empty;//DOB
            //        personage.Value = Convert.ToString(!String.IsNullOrEmpty(Convert.ToString(dv[0].Row.ItemArray[4])) ? get_age(Convert.ToDateTime(dv[0].Row.ItemArray[4])) : 0);//DOB);
            //        doa = !String.IsNullOrEmpty(Convert.ToString(dv[0].Row.ItemArray[5])) ? Convert.ToDateTime(dv[0].Row.ItemArray[5]).ToString("MM/dd/yyyy") : string.Empty;//DOA
            //        persongender.Value = Convert.ToString(dv[0].Row.ItemArray[1]) == "Ms." ? "female" : "male";
            //    }
            //    txtCreateSoapDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            //    chkWC.Checked = true;
            //    CheckBox chk = new CheckBox();
            //    chk.Checked = true;
            //    chkWC_CheckedChanged(chk, e);
            //}
            //string socialhistorty = string.Empty;
            //string query2 = "select PMH,PSH,Medications,Allergies,FamilyHistory,DeniesSmoking,DeniesDrinking,DeniesDrugs,DeniesSocialDrinking,Vitals from tblPatientIEDetailPage2 where PatientIE_ID=" + hdnrodieid.Value;
            //DataSet ds1 = gDbhelperobj.selectData(query2);

            //if (ds1.Tables[0].Rows.Count > 0)
            //{
            //    txtPastMedicalHistory.Text = ds1.Tables[0].Rows[0]["PMH"].ToString();
            //    txtpastsurgicalhistory.Text = ds1.Tables[0].Rows[0]["PSH"].ToString();
            //    txtdailyMedications.Text = ds1.Tables[0].Rows[0]["Medications"].ToString();
            //    txtAllergies.Text = ds1.Tables[0].Rows[0]["Allergies"].ToString();

            //    if (ds1.Tables[0].Rows[0]["DeniesSmoking"] != DBNull.Value)
            //    {
            //        if (Convert.ToBoolean(ds1.Tables[0].Rows[0]["DeniesSmoking"].ToString()))
            //        { socialhistorty += " Somking, "; }
            //    }
            //    if (ds1.Tables[0].Rows[0]["DeniesDrinking"] != DBNull.Value)
            //    {
            //        if (Convert.ToBoolean(ds1.Tables[0].Rows[0]["DeniesDrinking"].ToString()))
            //        { socialhistorty += " drinking, "; }
            //    }
            //    if (ds1.Tables[0].Rows[0]["DeniesDrugs"] != DBNull.Value)
            //    {
            //        if (Convert.ToBoolean(ds1.Tables[0].Rows[0]["DeniesDrugs"].ToString()))
            //        { socialhistorty += "  drugs, "; }
            //    }
            //    if (ds1.Tables[0].Rows[0]["DeniesSocialDrinking"] != DBNull.Value)
            //    {
            //        if (Convert.ToBoolean(ds1.Tables[0].Rows[0]["DeniesSocialDrinking"].ToString()))
            //        { socialhistorty += " social drinking."; }
            //    }
            //    txtSocialHistory.Text = socialhistorty;
            //}

            //string query1 = "select * from tblPatientIEDetailPage1 where PatientIE_ID=" + hdnrodieid.Value;

            //DataSet ds2 = gDbhelperobj.selectData(query1);
            //if (ds2.Tables[0].Rows.Count > 0)
            //{
            //    txtpastaccideninjuries.Text = ds2.Tables[0].Rows[0]["AccidentDetail"].ToString();
            //}





            // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "openModelPopupSoap();", true);
        }
        catch (Exception ex)
        {
            db.LogError(ex);
            throw;
        }
    }

    protected void gvPatientFUDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.DataItem != null)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (!string.IsNullOrEmpty(((GetFuDetailsResultORTHOPC)e.Row.DataItem).soap))
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }

    }
    public List<GetFuDetailsResultORTHOPC> GetFUDetailsORTHOPC(int PatientIEId)
    {
        List<GetFuDetailsResultORTHOPC> getFuDetailsResults = new List<GetFuDetailsResultORTHOPC>();
        DataAccess dal = new DataAccess();
        List<SqlParameter> param = new List<SqlParameter>();
        param.Add(new SqlParameter("@PatientIE_Id", PatientIEId));
        DataTable dt = dal.getDataTable("nusp_GetPatientsFUDetailsORTHOPC", param);

        foreach (DataRow dr in dt.Rows)
        {
            GetFuDetailsResultORTHOPC getFuDetailsResult = new GetFuDetailsResultORTHOPC();
            getFuDetailsResult.Sex = dr["Sex"].ToString();
            getFuDetailsResult.PatientId = Convert.ToInt32(dr["Patient_ID"]);
            getFuDetailsResult.PatientFUId = Convert.ToInt32(dr["PatientFU_ID"]);
            getFuDetailsResult.PatientIEId = Convert.ToInt32(dr["PatientIE_ID"]);
            getFuDetailsResult.FirstName = dr["FirstName"].ToString();
            getFuDetailsResult.LastName = dr["LastName"].ToString();
            getFuDetailsResult.Location = dr["Location"].ToString();
            getFuDetailsResult.MAProviders = dr["MA_Providers"].ToString();
            getFuDetailsResult.PrintStatus = dr["PrintStatus"].ToString();
            getFuDetailsResult.DOE = (dr["DOE"] != DBNull.Value) ? Convert.ToDateTime(dr["DOE"]) : new DateTime();
            getFuDetailsResult.soap = Convert.ToString(dr["soap"]);
            getFuDetailsResults.Add(getFuDetailsResult);
        }

        return getFuDetailsResults;
    }
}
public class GetFuDetailsResultORTHOPC
{
    public int PatientId { get; set; }
    public int PatientIEId { get; set; }
    public int PatientFUId { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Sex { get; set; }
    public DateTime DOE { get; set; }
    public string Location { get; set; }
    public string MAProviders { get; set; }
    public string PrintStatus { get; set; }
    public string soap { get; set; }

}
namespace Xceed.Words.NET
{
    internal class Formatting : Document.NET.Formatting
    {
        public bool Bold { get; set; }
    }
}