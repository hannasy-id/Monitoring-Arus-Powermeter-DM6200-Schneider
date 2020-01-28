Imports System.Collections.Generic
Imports System.Text

Namespace DataType
    Public Class DInt

#Region "Chuyển đổi mảng kiểu byte thành DInt."

        ''' <summary>
        ''' Phương thức chuyển mảng kiểu byte thành kiểu DInt.
        ''' </summary>
        ''' <param name="bytes"></param>
        ''' <returns></returns>
        Public Shared Function FromByteArray(bytes As Byte()) As Integer
            Array.Reverse(bytes)
            Return BitConverter.ToInt32(bytes, 0)
        End Function


        Public Shared Function FromBytes(v1 As Byte, v2 As Byte, v3 As Byte, v4 As Byte) As Integer
            Return CInt(Math.Truncate(v1 + v2 * Math.Pow(2, 8) + v3 * Math.Pow(2, 16) + v4 * Math.Pow(2, 24)))
        End Function

#End Region

#Region "Chuyển kiểu DInt thành mảng byte."

        ''' <summary>
        ''' Phương thức chuyển kiểu DInt thành mảng byte.
        ''' </summary>
        ''' <param name="value">Giá trị kiểu DInt</param>
        ''' <returns>Trả về mảng dữ liệu kiểu byte</returns>
        Public Shared Function ToByteArray(value As Integer) As Byte()
            Dim array__1 As Byte() = BitConverter.GetBytes(value)
            Array.Reverse(array__1)
            Return array__1
        End Function

        ''' <summary>
        ''' Phương thức chuyển mảng dữ liệu kiểu DInt thành mảng byte.
        ''' </summary>
        ''' <param name="value">Mảng giá trị kiểu DInt</param>
        ''' <returns>Trả về mảng dữ liệu kiểu byte</returns>
        Public Shared Function ToByteArray(value As Integer()) As Byte()
            Dim arr As New ByteArray()
            For Each val As Integer In value
                arr.Add(ToByteArray(val))
            Next
            Return arr.array
        End Function

#End Region

#Region "ToArray"
        Public Shared Function ToArray(bytes As Byte()) As Integer()
            Dim values As Integer() = New Integer(bytes.Length \ 4 - 1) {}

            Dim counter As Integer = 0
            For cnt As Integer = 0 To bytes.Length \ 4 - 1
                values(cnt) = FromByteArray(New Byte() {bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1)), bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1)), bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1)), bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1))})
            Next

            Return values
        End Function
#End Region

        ' conversion
        Public Shared Function CDWord(value As Int64) As Integer
            If value > Integer.MaxValue Then
                value -= CLng(Integer.MaxValue) + 1
                value = CLng(Integer.MaxValue) + 1 - value
                value *= -1
            End If
            Return CInt(value)
        End Function
    End Class
End Namespace
