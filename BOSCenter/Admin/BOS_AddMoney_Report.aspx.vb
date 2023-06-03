
Imports System.Text

Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO




Public Class BOS_AddMoney_Report
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            Dim Filter As String = ""

            Dim CompanyCode As String = GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response)
            Dim DataBaseName As String = GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response)
            Dim LoginID As String = GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response)
            Dim group As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)

            'If group.Trim.ToUpper = "Admin".Trim.ToUpper Or group.Trim.ToUpper = "SuperAdmin".Trim.ToUpper Or group.Trim.ToUpper = "SuperAdmin".Trim.ToUpper Then
            '    Querystring = "select RID as SrNo,(CONVERT(VARCHAR(20),Request_DateTime,113)) as 'Request_Date',Request_AgentID as 'AgentID',Request_Transaction_Id,Request_name,Request_email,Request_phone,Request_amount,(CONVERT(VARCHAR(20),Response_DateTime,113)) as 'Response_DateTime',Response_Payment_Id as 'Payment ID',Response_Payment_Status as 'Status',Response_Action_Taken as 'Action',Reference_Id,Reference_Type,Ref_Plan_Code,Response_Transaction_Id,Request_redirect_url,Request_CompanyCode as 'CompanyCode',Request_Purpose as 'Purpose',Request_TransID from " & DataBaseName & ".dbo.BOS_InstaMojo_Gateway_Request_Details where Request_CompanyCode='" & CompanyCode & "'  order by RID Desc"
            'Else
            '    Querystring = "select RID as SrNo,(CONVERT(VARCHAR(20),Request_DateTime,113)) as 'Request_Date',Request_Transaction_Id,Request_amount,Request_TransID as 'TransID',Response_Payment_Id as 'Payment ID',Response_Payment_Status as 'Status',Response_Action_Taken as 'Action' from " & DataBaseName & ".dbo.BOS_InstaMojo_Gateway_Request_Details where Request_CompanyCode='" & CompanyCode & "' and Request_AgentID='" & LoginID & "' order by rid desc"
            'End If

            If group.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or group.Trim.ToUpper = "Distributor".Trim.ToUpper Or group.Trim.ToUpper = "Retailer".Trim.ToUpper Or group.Trim.ToUpper = "Customer".Trim.ToUpper Then
                Querystring = "select RID as SrNo,(CONVERT(VARCHAR(20),Request_DateTime,113)) as 'Request_Date',Request_Transaction_Id,Request_amount,Request_TransID as 'TransID',Response_Payment_Id as 'Payment ID',Response_Payment_Status as 'Status',Response_Action_Taken as 'Action' from " & DataBaseName & ".dbo.BOS_InstaMojo_Gateway_Request_Details where Request_CompanyCode='" & CompanyCode & "' and Request_AgentID='" & LoginID & "' order by rid desc"
            Else
                Querystring = "select RID as SrNo,(CONVERT(VARCHAR(20),Request_DateTime,113)) as 'Request_Date',Request_AgentID as 'AgentID',Request_Transaction_Id,Request_name,Request_email,Request_phone,Request_amount,(CONVERT(VARCHAR(20),Response_DateTime,113)) as 'Response_DateTime',Response_Payment_Id as 'Payment ID',Response_Payment_Status as 'Status',Response_Action_Taken as 'Action',Reference_Id,Reference_Type,Ref_Plan_Code,Response_Transaction_Id,Request_redirect_url,Request_CompanyCode as 'CompanyCode',Request_Purpose as 'Purpose',Request_TransID from " & DataBaseName & ".dbo.BOS_InstaMojo_Gateway_Request_Details where Request_CompanyCode='" & CompanyCode & "'  order by RID Desc"
            End If


            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                If GridView1.Rows.Count > 0 Then
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    GV.FL.showSerialnoOnGridView(GridView1, 0)
                Else

                    lblNoRecords.Text = "No Records Found"
                    lblNoRecords.CssClass = "errorlabels"
                End If
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Bind()
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



    Protected Sub ddlNoOfRecords_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlNoOfRecords.SelectedIndexChanged
        Try
            If ddlNoOfRecords.SelectedValue = "10 Record(s)" Then
                GridView1.PageSize = 10
            ElseIf ddlNoOfRecords.SelectedValue = "25 Record(s)" Then
                GridView1.PageSize = 25
            ElseIf ddlNoOfRecords.SelectedValue = "50 Record(s)" Then
                GridView1.PageSize = 50
            ElseIf ddlNoOfRecords.SelectedValue = "100 Record(s)" Then
                GridView1.PageSize = 100
            ElseIf ddlNoOfRecords.SelectedValue = "200 Record(s)" Then
                GridView1.PageSize = 200
            ElseIf ddlNoOfRecords.SelectedValue = "500 Record(s)" Then
                GridView1.PageSize = 500
            End If
            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            Try
                GridView1.PageIndex = e.NewPageIndex
            Catch ex As Exception
                GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

            End Try
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.PageIndexChanged
        Try
            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try

    End Sub



    Protected Sub ImagebtnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnExcel.Click
        Try
            GV.ExportToExcel(GridView1, Response, "AddMoneyDetails")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try

    End Sub

    Protected Sub ImagebtnPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnPdf.Click
        Try
            GV.ExportToPdf(GridView1, Response, "AddMoneyDetails")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub


    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imagebtnword.Click
        Try
            GV.ExportToWord(GridView1, Response, "AddMoneyDetails")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub


End Class