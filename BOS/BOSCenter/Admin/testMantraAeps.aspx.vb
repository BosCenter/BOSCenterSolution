Public Class testMantraAeps
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Dim fingureprint As String = txtDeviceInfo1.Text
            Dim pid As String = txtPidData1.Text


            fingureprint = Request.Form("txtDeviceInfo")
            fingureprint = fingureprint
            fingureprint = Request.Form("txtPidData")
            fingureprint = fingureprint
            fingureprint = Request.Form("devIndo")
            fingureprint = fingureprint
            fingureprint = Request.Form("devPid")
            fingureprint = fingureprint


            Response.Redirect("frm_Aeps_ps.aspx")
        Catch ex As Exception

        End Try

    End Sub
End Class