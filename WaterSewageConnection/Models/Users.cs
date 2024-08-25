using Microsoft.Data.SqlClient;
using System.Data;

namespace WaterSewageConnection.Models
{
    public class Users : ConClass
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string Action { get; set; }
        public string userType { get; set; }
        public int userTypeId { get; set; }
        public string Role { get; set; }

        public DataSet getDataSet()
        {
            string connectionString = ConClass.ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("userMasterSP_crud", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@UserId", this.userId);

                da.SelectCommand.Parameters.AddWithValue("@UserName", this.userName);
                da.SelectCommand.Parameters.AddWithValue("@Password", this.password);
                da.SelectCommand.Parameters.AddWithValue("@UserTypeId", this.userTypeId);

                da.SelectCommand.Parameters.AddWithValue("@Action", this.Action);


                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        //public void save(out string message)
        //{
        //    string connectionString = conClass.connectString;

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("workMasterSP_crud", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@zoneId", this.zoneId);
        //        cmd.Parameters.AddWithValue("@projectId", this.projectId);
        //        cmd.Parameters.AddWithValue("@workId", this.workId);
        //        cmd.Parameters.AddWithValue("@guidDetail", this.guidDetail);
        //        cmd.Parameters.AddWithValue("@ctrl", this.ctrl);
        //        cmd.Parameters.AddWithValue("@path", this.path);
        //        cmd.Parameters.AddWithValue("@qty", this.qty);
        //        cmd.Parameters.AddWithValue("@PaidAmountBySuda", this.PaidAmountBySuda);
        //        cmd.Parameters.AddWithValue("@ExpenseAmountByDistrict", this.ExpenseAmountByDistrict);
        //        cmd.Parameters.AddWithValue("@goNo", this.goNo);

        //        if (this.goDate.ToString().Contains("000") || this.goDate.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@goUploadedDateofShasan", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@goUploadedDateofShasan", this.goDate);

        //        }


        //        cmd.Parameters.AddWithValue("@goFile", this.goFile);


        //        //proposed parameter

        //        if (this.proposedDate.ToString().Contains("000") || this.proposedDate.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@proposedDate", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@proposedDate", this.proposedDate);

        //        }
        //        if (this.anumodanDate.ToString().Contains("000") || this.anumodanDate.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@anumodanDate", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@anumodanDate", this.anumodanDate);

        //        }



        //        cmd.Parameters.AddWithValue("@workDetails", this.workDetails);
        //        cmd.Parameters.AddWithValue("@hqId", this.hqId);
        //        cmd.Parameters.AddWithValue("@districtId", this.districtId);
        //        cmd.Parameters.AddWithValue("@divisionId", this.divisionId);
        //        cmd.Parameters.AddWithValue("@bastiId", this.bastiId);
        //        cmd.Parameters.AddWithValue("@wardId", this.wardId);
        //        cmd.Parameters.AddWithValue("@planId", this.planId);
        //        cmd.Parameters.AddWithValue("@yearId", this.yearId);
        //        cmd.Parameters.AddWithValue("@workTypeId", this.workTypeId);
        //        cmd.Parameters.AddWithValue("@architectId", this.architectId);
        //        cmd.Parameters.AddWithValue("@workName", this.workName);
        //        cmd.Parameters.AddWithValue("@referenceName", this.referenceName);
        //        cmd.Parameters.AddWithValue("@sambodhitMahanubhav", this.sambodhitMahanubhav);
        //        cmd.Parameters.AddWithValue("@proposedAmount", this.proposedAmount);
        //        cmd.Parameters.AddWithValue("@currentStage", this.currentStage);
        //        cmd.Parameters.AddWithValue("@remarks", this.remarks);
        //        cmd.Parameters.AddWithValue("@firstInstallmentByHQ", this.firstInstallmentByHQ);
        //        cmd.Parameters.AddWithValue("@centageByHQ", this.centageByHQ);
        //        if (this.firstInstallmentByHQOnDate.ToString().Contains("000") || this.firstInstallmentByHQOnDate.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@firstInstallmentByHQOnDate", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@firstInstallmentByHQOnDate", this.firstInstallmentByHQOnDate);

        //        }
        //        cmd.Parameters.AddWithValue("@frstInstallmentLetterNoByHQ", this.frstInstallmentLetterNoByHQ);


        //        //Sanctioned parameter
        //        //if (this.optMode == "UpdateProposedWorkToSanction" || this.optMode == "insert")
        //        //{
        //        if (this.dateOfStart.ToString().Contains("000") || this.dateOfStart.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@dateOfStart", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@dateOfStart", this.dateOfStart);

        //        }
        //        if (this.dateOfCompletion.ToString().Contains("000") || this.dateOfCompletion.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@dateOfCompletion", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@dateOfCompletion", this.dateOfCompletion);

        //        }

        //        //}
        //        cmd.Parameters.AddWithValue("@acceptedMoney", this.acceptedMoney);
        //        cmd.Parameters.AddWithValue("@financialAcceptedMoney", this.financialAcceptedMoney);
        //        cmd.Parameters.AddWithValue("@firstEMIRecieved", this.firstEMIRecieved);
        //        cmd.Parameters.AddWithValue("@workOrderNo", this.workOrderNo);
        //        cmd.Parameters.AddWithValue("@workCompletedPercentage", this.workCompletedPercentage);
        //        cmd.Parameters.AddWithValue("@financialWorkCompletedPercentage", this.financialWorkCompletedPercentage);
        //        cmd.Parameters.AddWithValue("@bondAmount", this.bondAmount);

        //        cmd.Parameters.AddWithValue("@recentWorkStatus", this.recentWorkSatus);

        //        //if (this.optMode == "UpdateProposedWorkToSanction" || this.optMode == "insert")
        //        //{
        //        if (this.sanctionedDate.ToString().Contains("000") || this.sanctionedDate.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@sanctionedDate", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@sanctionedDate", this.sanctionedDate);

        //        }

        //        //}

        //        // allotment to vendor
        //        cmd.Parameters.AddWithValue("@vendorId", this.vendorId);
        //        //if (this.optMode == "allotWork")
        //        //{
        //        if (this.allotmentDate.ToString().Contains("000") || this.allotmentDate.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@allotmentDate", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@allotmentDate", this.allotmentDate);

        //        }

        //        //}

        //        // payment to vendor


        //        cmd.Parameters.AddWithValue("@TotalRecievedAmount", this.TotalRecievedAmount);
        //        cmd.Parameters.AddWithValue("@TotalPaidAmount", this.TotalPaidAmount);


        //        cmd.Parameters.AddWithValue("@documentId", this.documentId);
        //        cmd.Parameters.AddWithValue("@fileName", this.fileName);
        //        cmd.Parameters.AddWithValue("@filePath", this.filePath);
        //        cmd.Parameters.AddWithValue("@workDocumentId", this.workDocumentId);

        //        cmd.Parameters.AddWithValue("@isProposedByULB", this.isProposedByULB);
        //        cmd.Parameters.AddWithValue("@isConfirmByDistrict", this.isConfirmByDistrict);
        //        cmd.Parameters.AddWithValue("@isConfirmByHQ", this.isConfirmByHQ);
        //        cmd.Parameters.AddWithValue("@isConfirmByShasan", this.isConfirmByShasan);
        //        cmd.Parameters.AddWithValue("@isDPRByDistrict", this.isDPRByDistrict);
        //        cmd.Parameters.AddWithValue("@isDPRByHQ", this.isDPRByHQ);

        //        cmd.Parameters.AddWithValue("@isDPRByShasan", this.isDPRByShasan);
        //        cmd.Parameters.AddWithValue("@isDPRByPO", this.isDPRByPO);
        //        cmd.Parameters.AddWithValue("@isGoUploadedByShasan", this.isGoUploadedByShasan);
        //        cmd.Parameters.AddWithValue("@isFirstInstallmentByShasan", this.isFirstInstallmentByShasan);
        //        cmd.Parameters.AddWithValue("@isFirstInstallmentByHQ", this.isFirstInstallmentByHQ);
        //        cmd.Parameters.AddWithValue("@schemeType", this.schemeType);


        //        if (this.dateofApprovalOfDistrict.ToString().Contains("000") || this.dateofApprovalOfDistrict.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@dateofApprovalOfDistrict", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@dateofApprovalOfDistrict", this.dateofApprovalOfDistrict);

        //        }
        //        if (this.dateofApprovalOfHQ.ToString().Contains("000") || this.dateofApprovalOfHQ.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@dateofApprovalOfHQ", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@dateofApprovalOfHQ", this.dateofApprovalOfHQ);

        //        }
        //        if (this.dateofApprovalOfShasan.ToString().Contains("000") || this.dateofApprovalOfShasan.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@dateofApprovalOfShasan", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@dateofApprovalOfShasan", this.dateofApprovalOfShasan);

        //        }
        //        if (this.dateofDPRApprovalOfDistrict.ToString().Contains("000") || this.dateofDPRApprovalOfDistrict.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@dateofDPRApprovalOfDistrict", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@dateofDPRApprovalOfDistrict", this.dateofDPRApprovalOfDistrict);

        //        }
        //        if (this.dateofDPRApprovalOfHQ.ToString().Contains("000") || this.dateofDPRApprovalOfHQ.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@dateofDPRApprovalOfHQ", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@dateofDPRApprovalOfHQ", this.dateofDPRApprovalOfHQ);

        //        }
        //        if (this.dateofDPRApprovalOfShasan.ToString().Contains("000") || this.dateofDPRApprovalOfShasan.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@dateofDPRApprovalOfShasan", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@dateofDPRApprovalOfShasan", this.dateofDPRApprovalOfShasan);

        //        }
        //        if (this.dateoFApprovalDPRByPO.ToString().Contains("000") || this.dateoFApprovalDPRByPO.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@dateoFApprovalDPRByPO", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@dateoFApprovalDPRByPO", this.dateoFApprovalDPRByPO);

        //        }

        //        cmd.Parameters.AddWithValue("@firstInstallmentRecByDistrict", this.firstInstallmentRecByDistrict);
        //        cmd.Parameters.AddWithValue("@firstInstallmentCentageByHQ", this.firstInstallmentCentageByHQ);
        //        cmd.Parameters.AddWithValue("@firstInstallmentGivenByHQ", this.firstInstallmentGivenByHQ);
        //        cmd.Parameters.AddWithValue("@firstInstallmentGivenAmountbyShasan", this.firstInstallmentGivenAmountbyShasan);

        //        if (this.tenderStartDate.ToString().Contains("000") || this.tenderStartDate.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@tenderStartDate", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@tenderStartDate", this.tenderStartDate);

        //        }
        //        if (this.tenderEndDate.ToString().Contains("000") || this.tenderEndDate.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@tenderEndDate", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@tenderEndDate", this.tenderEndDate);

        //        }
        //        cmd.Parameters.AddWithValue("@tenderStatusId", this.tenderStatusId);
        //        cmd.Parameters.AddWithValue("@proposedAmountByHQ", this.proposedAmountByHQ);

        //        cmd.Parameters.AddWithValue("@IsRequestedForSecondInstallmentByPO", this.IsRequestedForSecondInstallmentByPO);
        //        if (this.RequestedForSecondInstallmentOn.ToString().Contains("000") || this.RequestedForSecondInstallmentOn.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@RequestedForSecondInstallmentOn", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@RequestedForSecondInstallmentOn", this.RequestedForSecondInstallmentOn);

        //        }

        //        cmd.Parameters.AddWithValue("@UCCertificate", this.UCCertificate);
        //        cmd.Parameters.AddWithValue("@SecondInstallmentAmountByPO", this.SecondInstallmentAmountByPO);
        //        cmd.Parameters.AddWithValue("@IsRequestedForSecondInstallmentByDistrict", this.IsRequestedForSecondInstallmentByDistrict);

        //        if (this.RequestedForSecondInstallmentOnByDistrict.ToString().Contains("000") || this.RequestedForSecondInstallmentOnByDistrict.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@RequestedForSecondInstallmentOnByDistrict", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@RequestedForSecondInstallmentOnByDistrict", this.RequestedForSecondInstallmentOnByDistrict);

        //        }

        //        cmd.Parameters.AddWithValue("@SecondInstallmentAmountByDistrict", this.SecondInstallmentAmountByDistrict);
        //        cmd.Parameters.AddWithValue("@IsRequestedForSecondInstallmentByHQ", this.IsRequestedForSecondInstallmentByHQ);

        //        if (this.SecondInstallmentOnByHQ.ToString().Contains("000") || this.SecondInstallmentOnByHQ.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@SecondInstallmentOnByHQ", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@SecondInstallmentOnByHQ", this.SecondInstallmentOnByHQ);

        //        }

        //        cmd.Parameters.AddWithValue("@SecondInstallmentAmountByHQ", this.SecondInstallmentAmountByHQ);
        //        cmd.Parameters.AddWithValue("@IsRequestedForSecondInstallmentByShasan", this.IsRequestedForSecondInstallmentByShasan);

        //        if (this.SecondInstallmentOnByShasan.ToString().Contains("000") || this.SecondInstallmentOnByShasan.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@SecondInstallmentOnByShasan", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@SecondInstallmentOnByShasan", this.SecondInstallmentOnByShasan);

        //        }

        //        cmd.Parameters.AddWithValue("@SecondInstallmentAmountByShasan", this.SecondInstallmentAmountByShasan);
        //        cmd.Parameters.AddWithValue("@SecondInstallmentShasandeshNo", this.SecondInstallmentShasandeshNo);
        //        cmd.Parameters.AddWithValue("@SecondInstallmentShasandeshDoc", this.SecondInstallmentShasandeshDoc);


        //        cmd.Parameters.AddWithValue("@createdBy", this.createdBy);
        //        cmd.Parameters.AddWithValue("@ipAddress", this.ipAddress);
        //        cmd.Parameters.AddWithValue("@mcAddress", this.mcAddress);
        //        cmd.Parameters.AddWithValue("@optMode", this.optMode);

        //        // add vendor mobile
        //        cmd.Parameters.AddWithValue("@vendorMobile", this.vendorMobile);


        //        // nirast(cancelled) works
        //        if (this.CancelledWorkFileDate.ToString().Contains("000") || this.CancelledWorkFileDate.ToString().Contains("1900"))
        //        {
        //            cmd.Parameters.AddWithValue("@CancelledWorkFileDate", "01/01/1900");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@CancelledWorkFileDate", this.CancelledWorkFileDate);

        //        }

        //        cmd.Parameters.AddWithValue("@CancelledWorkAmount", this.CancelledWorkAmount);
        //        cmd.Parameters.AddWithValue("@CancelledWorkFile", this.CancelledWorkFile);
        //        cmd.Parameters.AddWithValue("@CancelledWorkRemarks", this.CancelledWorkRemarks);


        //        // Remaining Amount
        //        cmd.Parameters.AddWithValue("@RemainingAmountByDistrict", this.RemainingAmountByDistrict);
        //        cmd.Parameters.AddWithValue("@RemainingAmountDoc", this.RemainingAmountDoc);

        //        // second installment status id
        //        cmd.Parameters.AddWithValue("@SecondInstallmentStatusId", this.SecondInstallmentStatusId);

        //        cmd.Parameters.Add("@msg", System.Data.SqlDbType.NVarChar, 250);
        //        cmd.Parameters["@msg"].Direction = ParameterDirection.Output;

        //        con.Open();
        //        cmd.ExecuteNonQuery();

        //        message = cmd.Parameters["@msg"].Value.ToString();

        //    }
        //}

        public bool getObject()
        {

            DataSet ds = new DataSet();
            ds = getDataSet();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    this.userId = Convert.ToInt32(dr["userId"].ToString());
                    this.userName = dr["userName"].ToString();
                    this.password = dr["password"].ToString();
                    this.userType = dr["userType"].ToString();


                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
