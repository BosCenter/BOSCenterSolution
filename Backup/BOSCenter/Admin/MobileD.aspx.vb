
Public Class MobileD
    Inherits System.Web.UI.Page

    Dim GV As New GlobalVariable("ADMIN")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("PageCall") = "Fresh" Then
            '    Session("PageCall") = "Old"
            '    '   lblWelcom.Text = "Hello " & StrConv(GV.get_SuperAdmin_SessionVariables("UserName", Request, Response).ToString, VbStrConv.ProperCase) & ", Welcome To COMPUTEC Academy !! "
            'Else
            '    '  lblWelcom.Text = ""
            'End If

        End If
    End Sub

End Class