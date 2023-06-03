Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration.WebConfigurationManager


Public Class Product
    Public Property Name As String
    Public Property Quantity As String
    Public Property Price As Double
    Public Property Units As Integer
    Public Property ID As Integer

    Public Sub New()
    End Sub
    Public Sub New(ByVal id As Integer, ByVal name As String, ByVal quantity As String, ByVal price As Double, ByVal units As Integer)
        Me.ID = id
        Me.Name = name
        Me.Quantity = quantity
        Me.Price = price
        Me.Units = units
    End Sub

    Public Function ConvertReader(ByVal reader As SqlDataReader)
        Dim products As List(Of Product) = New List(Of Product)
        If reader.HasRows Then
            While reader.Read()
                products.Add(New Product(CInt(reader.Item("RID")), reader.Item("ProductName").ToString(), _
                                     reader.Item("QuantityPerUnit").ToString(), _
                                     CDbl(reader.Item("UnitPrice")), _
                                     CInt(reader.Item("UnitsInStock"))))
            End While
        End If
        Return products
    End Function

    Public Function GetProducts() As IEnumerable(Of Product)
        Dim cn As SqlConnection = New SqlConnection(ConnectionStrings("connName").ToString())
        Dim cmd As New SqlCommand("GetProducts", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cn.Open()
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        Dim products As List(Of Product) = ConvertReader(reader)
        cn.Close()
        Return products
    End Function


    Public Function GetProductByID(id As Integer) As Product
        Dim cn As SqlConnection = New SqlConnection(ConnectionStrings("connName").ToString())
        Dim cmd As New SqlCommand("GetProducts", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cn.Open()
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        Dim products As List(Of Product) = ConvertReader(reader)
        cn.Close()
        Return products.Find(Function(p) p.ID = id)
    End Function

    Public Function GetProductByName(name As String, id As Integer) As IEnumerable(Of Product)
        Dim cn As SqlConnection = New SqlConnection(ConnectionStrings("connName").ToString())
        Dim cmd As New SqlCommand("GetProducts", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cn.Open()
        Dim reader As SqlDataReader = cmd.ExecuteReader()
        Dim products As List(Of Product) = ConvertReader(reader)
        cn.Close()
        Return products.Where(Function(p) String.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase) Or p.ID = id)
    End Function


End Class
