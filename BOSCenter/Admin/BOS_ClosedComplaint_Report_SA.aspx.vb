﻿
Imports System.Text

Imports System.Web.UI.HtmlControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.Net




Public Class BOS_ClosedComplaint_Report_SA
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")


    Dim Querystring As String = ""
    Public Sub Bind()
        Try
            Dim Filter As String = ""
            Dim grp As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)
            Dim GROUPFilter As String = ""
            Dim GroupCode As String = ""
            GridView1.Columns(4).Visible = True
            GridView1.Columns(5).Visible = True
            GridView1.Columns(9).Visible = True
            GridView1.Columns(11).Visible = True

            If grp.Trim.ToUpper = "Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Retailer".Trim.ToUpper Or grp.Trim.ToUpper = "Customer".Trim.ToUpper Then

            ElseIf grp.Trim.ToUpper = "Super Admin".Trim.ToUpper Then
                GROUPFilter = ""
                GroupCode = ",kCode,kCodeType"
            Else
                GROUPFilter = " And CompanyCode='" & GV.get_SuperAdmin_SessionVariables("CompanyCode", Request, Response) & "'"
                GroupCode = ",kCode,kCodeType"
            End If

            If ddlSelectCriteria.SelectedValue.Trim.ToUpper = "All Records".Trim.ToUpper Then
                Filter = ""
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Register ID".Trim.ToUpper Then
                Filter = " And kCode ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Complaint ID".Trim.ToUpper Then
                Filter = " And ComplaintID ='" & GV.parseString(txtSearchingValue.Text.Trim) & "'"
            ElseIf ddlSelectCriteria.SelectedValue.Trim.ToUpper = "Product Service".Trim.ToUpper Then
                Filter = " And Product like '" & GV.parseString(txtSearchingValue.Text.Trim) & "%'"
            End If
            Querystring = "select RID as SrNo,(CONVERT(VARCHAR(11),ComplaintDate,106)) as ComplaintDate,ComplaintID,kCode,kCodeType,Product,(Problem+'/'+Complaint) as ComplaintProblem,isnull(Attachment,'') as Attachment,ComplaintStatus,AllotedTo,(CONVERT(VARCHAR(11),AllotedDateTime,106)) as AllotedDateTime,Remarks,(CONVERT(VARCHAR(11),ClosedDateTime,106)) as ClosedDateTime,ClosedBy,CompanyCode from " & GV.DefaultDatabase.Trim & ".dbo.BOS_Complaint_Master_SA where ComplaintStatus='Closed' " & GROUPFilter & " " & Filter & " order by RID Desc"


            If Not Querystring = "" Then

                GV.FL.AddInGridViewWithFieldName(GridView1, Querystring)

                GridView1.DataBind()

                If GridView1.Rows.Count > 0 Then
                    If grp.Trim.ToUpper = "Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Retailer".Trim.ToUpper Or grp.Trim.ToUpper = "Customer".Trim.ToUpper Then
                        GridView1.Columns(3).Visible = False
                        GridView1.Columns(4).Visible = False
                        GridView1.Columns(10).Visible = False
                        GridView1.Columns(12).Visible = False

                    End If
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        Dim lnkdwnload As LinkButton = DirectCast(GridView1.Rows(i).FindControl("lnkdwnload"), LinkButton)
                        Dim lblAttachment As Label = DirectCast(GridView1.Rows(i).FindControl("lblAttachment"), Label)
                        Dim LnkbtnChat As LinkButton = DirectCast(GridView1.Rows(i).FindControl("LinkButton1"), LinkButton)
                        If Not GV.FL.RecCount(" " & GV.DefaultDatabase.Trim & ".dbo.BOS_Complaint_Master_Chat_SA where ComplaintID='" & GV.parseString(GridView1.Rows(i).Cells(2).Text) & "'") > 0 Then
                            LnkbtnChat.Visible = False
                        Else
                            LnkbtnChat.Visible = True
                        End If

                        If lblAttachment.Text = "" Then
                            lnkdwnload.Visible = False
                        Else
                            lnkdwnload.Visible = True
                        End If
                        GridView1.Rows(i).Cells(0).Text = i + 1
                    Next
                    lblNoRecords.Text = ""
                    lblNoRecords.CssClass = ""
                    'GV.FL.showSerialnoOnGridView(GridView1, 0)
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
                Dim grp As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response)


                If grp.Trim.ToUpper = "Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Master Distributor".Trim.ToUpper Or grp.Trim.ToUpper = "Retailer".Trim.ToUpper Or grp.Trim.ToUpper = "Customer".Trim.ToUpper Then

                ElseIf grp.Trim.ToUpper = "Super Admin".Trim.ToUpper Then

                Else
                    ddlSelectCriteria.Items.Remove("Register ID")
                End If

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
            GV.ExportToExcel(GridView1, Response, "UnallotedReport")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try

    End Sub

    Protected Sub ImagebtnPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnPdf.Click
        Try
            GV.ExportToPdf(GridView1, Response, "UnallotedReport")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Do nothing 
    End Sub

    Protected Sub Imagebtnword_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImagebtnWOrd.Click
        Try
            GV.ExportToWord(GridView1, Response, "UnallotedReport")
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Try
            If Not lblRID.Text = "" Then

                Dim str As String

                str = ("update " & GV.DefaultDatabase.Trim & ".dbo.BOS_Complaint_Master_SA set AllotedTo='" & GV.parseString(ddlEmployee.SelectedValue.Trim) & "',AllotedDateTime=getdate() where RID=" & GV.parseString(lblRID.Text) & " ")
                Dim result As Boolean = GV.FL.DMLQueries(str)
                lblDialogMsg.Text = result
                If result = True Then
                    lblDialogMsg.Text = "Record Updated Successfully."
                    lblDialogMsg.CssClass = "Successlabels"
                    Bind()

                Else
                    lblDialogMsg.Text = "Sorry !! Record Failed."
                    lblDialogMsg.CssClass = "errorlabels"
                End If
                btnCancel.Text = "OK"
                btnUpdate.Visible = False
                ModalPopupExtender1.Show()
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            lblExportQry.Text = ""

            lblError1.Text = ""
            lblNoRecords.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.CssClass = ""

            GridView1.DataSource = Nothing
            GridView1.DataBind()

            If Not ddlSelectCriteria.SelectedValue = "All Records" And GV.parseString(txtSearchingValue.Text.Trim) = "" Then
                lblError1.Text = "Enter Search value"
                lblError1.CssClass = "errorlabels"
                Exit Sub
            End If
            Bind()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Protected Sub DownloadRow_click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lblAttachment As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).FindControl("lblAttachment"), Label)
            DownloadDoc(lblAttachment.Text)
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub

    Public Sub DownloadDoc(ByVal DocPath As String)
        Try

            Dim fi As New FileInfo(DocPath)
            Dim strURL As String = DocPath
            Dim req As New WebClient()
            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.ClearContent()
            response.ClearHeaders()
            response.Buffer = True
            response.AddHeader("Content-Disposition", "attachment;filename=""" & fi.Name & """")
            Dim data As Byte() = req.DownloadData(Server.MapPath(strURL))
            response.BinaryWrite(data)


            response.[End]()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)
        End Try
    End Sub
    Public Sub clear()
        Try
            lblError1.Text = ""
            lblError1.CssClass = ""
            lblNoRecords.Text = ""
            lblNoRecords.CssClass = ""
            ddlSelectCriteria.SelectedIndex = 0
            txtSearchingValue.Text = ""
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            ddlNoOfRecords.SelectedIndex = 0
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            clear()
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



    Public Sub ChatBind()
        Try
            Dim Qry As String = "select (MessageFrom +' : '+Chat_Message) as 'Chat_Message'  from " & GV.DefaultDatabase.Trim & ".dbo.BOS_Complaint_Master_Chat_SA where ComplaintID='" & GV.parseString(lblComplaintMsgID.Text.Trim) & "' order by rid asc"
            If Not Qry = "" Then
                ds = New DataSet
                ds = GV.FL.OpenDsWithSelectQuery(Qry)
                If ds.Tables(0).Rows.Count > 0 Then
                    lstChat.DataSource = ds.Tables(0)
                    lstChat.DataTextField = "Chat_Message"
                    lstChat.DataBind()
                Else
                    lstChat.Items.Clear()
                    lstChat.DataSource = Nothing
                    lstChat.DataTextField = ""
                    lstChat.DataBind()
                End If


            End If
        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub



    Protected Sub ChatDetails_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim btndetails As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
            Dim lbl As Label = DirectCast(GridView1.Rows(gvrow.RowIndex).Cells(0).FindControl("lblgrdRID"), Label)
            lblRID.Text = lbl.Text.Trim

            lblComplaintMsgID.Text = GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(2).Text)
            lblInfoMsg.Text = " Complaint ID= <font color=""red"">" & lblComplaintMsgID.Text.Trim & "</font> ::: Compalint Against= <font color=""red"">" & GV.parseString(GridView1.Rows(gvrow.RowIndex).Cells(6).Text) & "</font>"

            ChatBind()

            ModalPopupChat.Show()

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub




End Class