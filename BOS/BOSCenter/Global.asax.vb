Imports System.Web.SessionState
Imports System.Web.Routing
Imports System.Web.Http
Public Class Global_asax
    Inherits System.Web.HttpApplication


    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        RouteTable.Routes.MapHttpRoute(name:="DefaultApi", _
routeTemplate:="api/{controller}/{id}", _
defaults:=New With {.id = System.Web.Http.RouteParameter.Optional})

        Dim config As New HttpConfiguration
        config.Formatters.JsonFormatter.SupportedMediaTypes.Add(New System.Net.Http.Headers.MediaTypeHeaderValue("text/html"))

        'config.Formatters.Remove(config.Formatters.XmlFormatter)
        'https://www.youtube.com/watch?v=tNzgXjqqIjI
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started

    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class