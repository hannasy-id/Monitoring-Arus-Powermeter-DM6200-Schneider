Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace DataType
    Public NotInheritable Class Conversion
        Private Sub New()
        End Sub
        Public Shared Function ToDoubleInver(value As Byte()) As Double
            Return BitConverter.ToDouble(value, 0)
        End Function

        Public Shared Function ToDoubleInverses(values As Byte()) As Double()
            Array.Reverse(values)
            Dim result As Double() = New Double(values.Length \ 8 - 1) {}
            For i As Integer = 0 To values.Length - 1 Step 8
                result(i \ 8) = BitConverter.ToDouble(values, i)
            Next
            Array.Reverse(result)
            Return result
        End Function

        Public Shared Function ToDouble(bytes As Byte()) As Double
            Array.Reverse(bytes)
            Dim length As Integer = bytes.Length \ 8
            Dim size As Integer = 2
            For i As Integer = 0 To length - 1
                bytes(i) += bytes(i + size)
                bytes(i + size) = CByte(bytes(i) - bytes(i + size))
                bytes(i) = CByte(bytes(i) - bytes(i + size))
            Next
            Return BitConverter.ToDouble(bytes, 0)
        End Function

        Public Shared Function ToDoubles(bytes As Byte()) As Double()
            Dim size As Integer = 8
            Dim idx As Integer = 0
            Dim result As Double() = New Double(bytes.Length \ size - 1) {}
            Do
                Dim data As Byte() = New Byte(7) {}
                Array.Copy(bytes, idx, data, 0, data.Length)
                result(idx \ size) = ToDouble(data)
                idx += size
            Loop While idx < bytes.Length
            Return result
        End Function

        Public Shared Function ToLong(value As Byte()) As Long
            Array.Reverse(value)
            Return BitConverter.ToInt64(value, 0)
        End Function

        Public Shared Function ToLongs(values As Byte()) As Long()
            Array.Reverse(values)
            Dim result As Long() = New Long(values.Length \ 8 - 1) {}
            For i As Integer = 0 To values.Length - 1 Step 8
                result(i \ 8) = BitConverter.ToInt64(values, i)
            Next
            Array.Reverse(result)
            Return result
        End Function
    End Class
End Namespace
