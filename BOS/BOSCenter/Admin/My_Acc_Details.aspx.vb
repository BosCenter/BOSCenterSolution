Imports System.Data.OleDb
Imports System.Data
Imports System.Net
Imports System.IO


Public Class My_Acc_Details
    Inherits System.Web.UI.Page
    Dim GV As New GlobalVariable("ADMIN")
    Dim aa() As String
    Dim qry As String = ""



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim grp As String = GV.get_SuperAdmin_SessionVariables("Group", Request, Response).Trim.ToUpper

                If grp = "Master Distributor".ToUpper Or grp = "Distributor".ToUpper Or grp = "Retailer".ToUpper Or grp = "Customer".ToUpper Then
                    Fill_Details()
                End If

                'If btn_update.Text.Trim.ToUpper = "Update".Trim.ToUpper Then
                '    lblError.Visible = True
                '    lblError.Text = "Record Updated Successfully"
                '    lblError.CssClass = "successLabels"
                'Else
                '    lblError.Visible = True
                '    lblError.Text = "Record Updation Failed"
                '    lblError.CssClass = "errorLabels"
                'End If


            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub Fill_Details()
        Try
            qry = "select * from " & GV.get_SuperAdmin_SessionVariables("DataBaseName", Request, Response).Trim & ".dbo.BOS_Dis_SubDis_Retailer_Registration where RegistrationId='" & GV.get_SuperAdmin_SessionVariables("LoginID", Request, Response).Trim & "'"
            Dim ds As DataSet = New DataSet
            ds = GV.FL.OpenDsWithSelectQuery(qry)
            If Not ds Is Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_Acc_No")) Then
                            If Not ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_Acc_No").ToString = "" Then
                                txt_account_no.Text = ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_Acc_No").ToString
                            Else
                                txt_account_no.Text = ""
                            End If
                        Else
                            txt_account_no.Text = ""
                        End If
                        txt_account_no.ReadOnly = True
                        txt_account_no.ForeColor = Drawing.Color.Blue
                        txt_account_no.Font.Bold = True

                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("easeBuzz_IFSC")) Then
                            If Not ds.Tables(0).Rows(0).Item("easeBuzz_IFSC").ToString = "" Then
                                txt_ifsc.Text = ds.Tables(0).Rows(0).Item("easeBuzz_IFSC").ToString
                            Else
                                txt_ifsc.Text = ""
                            End If
                        Else
                            txt_ifsc.Text = ""
                        End If
                        txt_ifsc.ReadOnly = True
                        txt_ifsc.ForeColor = Drawing.Color.Blue
                        txt_ifsc.Font.Bold = True


                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID")) Then
                            If Not ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID").ToString = "" Then
                                txt_upi_id.Text = ds.Tables(0).Rows(0).Item("easeBuzz_Virtual_UPI_ID").ToString
                            Else
                                txt_upi_id.Text = ""
                            End If
                        Else
                            txt_upi_id.Text = ""
                        End If
                        txt_upi_id.ReadOnly = True
                        txt_upi_id.ForeColor = Drawing.Color.Blue
                        txt_upi_id.Font.Bold = True


                        If Not IsDBNull(ds.Tables(0).Rows(0).Item("easeBuzz_QrCode")) Then

                            If Not ds.Tables(0).Rows(0).Item("easeBuzz_QrCode").ToString() = "" Then
                                Dim filePath As String = ds.Tables(0).Rows(0).Item("easeBuzz_QrCode").ToString()
                                Dim filename As String = Path.GetFileName(filePath)
                                Dim ext As String = Path.GetExtension(filename)
                                If (UCase(ext) = UCase(".jpg")) Or (UCase(ext) = UCase(".jpeg")) Or (UCase(ext) = UCase(".png")) Or (UCase(ext) = UCase(".gif")) Then
                                    Image_UPIQRCode_url.ImageUrl = GV.parseString(ds.Tables(0).Rows(0).Item("easeBuzz_QrCode").ToString())
                                End If
                            Else
                                Image_UPIQRCode_url.ImageUrl = "~/images/uploadimage.png"
                            End If
                        Else
                            Image_UPIQRCode_url.ImageUrl = "~/images/uploadimage.png"
                        End If

                        If GV.parseString(txt_account_no.Text) = "" And GV.parseString(txt_ifsc.Text) = "" And GV.parseString(txt_upi_id.Text) = "" Then
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

        End Try
    End Sub

End Class