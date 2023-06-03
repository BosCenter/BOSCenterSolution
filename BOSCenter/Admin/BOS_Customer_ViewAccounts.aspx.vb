Imports System.Data.SqlClient
Imports AjaxControlToolkit
Imports System.Data.OleDb
Imports System.Data
Imports System.Net
Imports System.IO

Public Class BOS_Customer_ViewAccounts
    Inherits System.Web.UI.Page

    Dim Qry As String = ""
    Dim DS As DataSet
    Dim GV As New GlobalVariable("ADMIN")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim grp As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper

                If grp = "Master Distributor".ToUpper Or grp = "Distributor".ToUpper Or grp = "Retailer".ToUpper Or grp = "Customer".ToUpper Then
                    Fill_Details()
                End If


            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

    Public Sub Fill_Details()
        Try
            Qry = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim & "'"
            Dim ds As DataSet = New DataSet
            ds = GV.FL.OpenDsWithSelectQuery(Qry)

            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("easeBuzz_Virtual_Acc_No")) Then
                            If Not DS.Tables(0).Rows(0).Item("easeBuzz_Virtual_Acc_No").ToString = "" Then
                                txtAccountNo.Text = DS.Tables(0).Rows(0).Item("easeBuzz_Virtual_Acc_No").ToString
                            Else
                                txtAccountNo.Text = ""
                            End If
                        Else
                            txtAccountNo.Text = ""
                        End If
                        txtAccountNo.ReadOnly = True
                        txtAccountNo.ForeColor = Drawing.Color.Blue
                        txtAccountNo.Font.Bold = True
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("easeBuzz_IFSC")) Then
                            If Not DS.Tables(0).Rows(0).Item("easeBuzz_IFSC").ToString = "" Then
                                txtIFSC_Code.Text = DS.Tables(0).Rows(0).Item("easeBuzz_IFSC").ToString
                            Else
                                txtIFSC_Code.Text = ""
                            End If
                        Else
                            txtIFSC_Code.Text = ""
                        End If
                        txtIFSC_Code.ReadOnly = True
                        txtIFSC_Code.ForeColor = Drawing.Color.Blue
                        txtIFSC_Code.Font.Bold = True
                        If GV.parseString(txtAccountNo.Text) = "" And GV.parseString(txtIFSC_Code.Text) = "" Then
                            lblError.Visible = True
                            lblError.Text = "Sorry!! Your Account Is Not Linked"
                            lblError.CssClass = "errorLabels"
                        Else
                            lblError.Visible = True
                            lblError.Text = "Your Account Is Linked"
                            lblError.CssClass = "successLabels"
                        End If
                    Else
                        lblError.Visible = True
                        lblError.Text = "Sorry!! Your Account Is Not Linked"
                        lblError.CssClass = "errorLabels"
                    End If
                Else
                    lblError.Visible = True
                    lblError.Text = "Sorry!! Your Account Is Not Linked"
                    lblError.CssClass = "errorLabels"
                End If
            Else
                lblError.Visible = True
                lblError.Text = "Sorry!! Your Account Is Not Linked"
                lblError.CssClass = "errorLabels"
            End If

        Catch ex As Exception
            GV.logerrors(ex.Message + "StackTrace : " + ex.StackTrace)

        End Try
    End Sub

End Class