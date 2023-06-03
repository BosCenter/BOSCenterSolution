Imports System.Net
Imports System.Web.Http

Public Class ProductsController
    Inherits ApiController

    '' GET api/<controller>
    'Public Function GetValues() As IEnumerable(Of String)
    '    Return New String() {"value1", "value2"}
    'End Function

    Public Function GetProducts() As IEnumerable(Of Product)
        Dim Product As Product = New Product
        Return Product.GetProducts()
    End Function

    '' GET api/<controller>/5
    'Public Function GetValue(ByVal id As Integer) As String
    '    Return "value"
    'End Function


    Public Function GetProductByID(id As Integer) As Product
        Dim Product As Product = New Product
        Return Product.GetProductByID(id)
    End Function

    Public Function GetProductsByName(name As String, id As Integer) As IEnumerable(Of Product)
        Dim Product As Product = New Product
        Return Product.GetProductByName(name, id)
    End Function


    ' POST api/<controller>
    Public Sub PostValue(<FromBody()> ByVal value As String)

    End Sub

    ' PUT api/<controller>/5
    Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub

    ' DELETE api/<controller>/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub
End Class
