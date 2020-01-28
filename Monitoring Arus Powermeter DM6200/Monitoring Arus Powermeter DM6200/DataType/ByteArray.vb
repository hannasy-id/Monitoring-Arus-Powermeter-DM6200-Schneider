Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace DataType
    Public Class ByteArray
        Private list As New List(Of Byte)()

        Public ReadOnly Property array() As Byte()
            Get
                Return list.ToArray()
            End Get
        End Property

        Public Sub New()
            list = New List(Of Byte)()
        End Sub

        Public Sub New(size As Integer)
            list = New List(Of Byte)(size)
        End Sub

        Public Sub Clear()
            list = New List(Of Byte)()
        End Sub

        Public Sub Add(item As Byte)
            list.Add(item)
        End Sub

        Public Sub Add(items As Byte())
            list.AddRange(items)
        End Sub
    End Class
End Namespace
