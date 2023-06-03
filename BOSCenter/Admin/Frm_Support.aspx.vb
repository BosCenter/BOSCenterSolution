Public Class Frm_Support
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("ADMIN")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub lnkbtn_RaiseRequest_Click(sender As Object, e As ImageClickEventArgs) Handles lnkbtn_RaiseRequest.Click
        Try
            Response.Redirect("BOS_Raise_Request_Complaint.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub lnkbtn_PendingReport_Click(sender As Object, e As ImageClickEventArgs) Handles lnkbtn_PendingReport.Click
        Try
            Response.Redirect("BOS_PendingForm_Report.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub lnkbtn_CloseReport_Click(sender As Object, e As ImageClickEventArgs) Handles lnkbtn_CloseReport.Click
        Try
            Response.Redirect("BOS_ClosedComplaint_Report.aspx")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
End Class