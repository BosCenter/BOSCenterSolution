Imports System.Data.OleDb
Imports System.Data
Imports System.Net
Imports System.IO
Public Class BOS_Customer_UPI_QRCode
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")
    Dim DS As DataSet
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

        Dim Qry As String = ""
        Try

            Qry = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim & "'"
            Dim ds As DataSet = New DataSet
            ds = GV.FL.OpenDsWithSelectQuery(Qry)

            If Not DS Is Nothing Then
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID")) Then
                            If Not DS.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID").ToString = "" Then
                                txt_UPIID.Text = DS.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID").ToString
                            Else
                                txt_UPIID.Text = ""
                            End If
                        Else
                            txt_UPIID.Text = ""
                        End If
                        txt_UPIID.ReadOnly = True
                        txt_UPIID.ForeColor = Drawing.Color.Blue
                        txt_UPIID.Font.Bold = True
                        If Not IsDBNull(DS.Tables(0).Rows(0).Item("easeBuzz_QrCode")) Then
                            If Not DS.Tables(0).Rows(0).Item("easeBuzz_QrCode").ToString = "" Then
                                Dim filepath As String = DS.Tables(0).Rows(0).Item("easeBuzz_QrCode").ToString
                                Dim fileName As String = Path.GetFileName(filepath)
                                Dim ext As String = Path.GetExtension(fileName)
                                If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".GIF")) Then
                                    Image_UPIQRCode_URL.ImageUrl = GV.parseString(DS.Tables(0).Rows(0).Item("easeBuzz_QrCode").ToString)
                                End If
                            Else
                                Image_UPIQRCode_URL.ImageUrl = "~/imaes/uploadimage.png"
                            End If
                        Else
                            Image_UPIQRCode_URL.ImageUrl = "~/images/uploadimage.png"
                        End If

                        If GV.parseString(txt_UPIID.Text) = "" Then
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