Imports System.Collections.Generic
Imports System.Text

Namespace DataType
    Public Class DWord

#Region "Chuyển đổi mảng bytes thành kiểu DWord."

        ''' <summary>
        ''' Phương thức chuyển đổi mảng bytes thành DWord.
        ''' </summary>
        ''' <param name="bytes">Mảng bytes</param>
        ''' <returns>Giá trị kiểu DWord</returns>
        Public Shared Function FromByteArray(bytes As Byte()) As UInt32
            Array.Reverse(bytes)
            Return BitConverter.ToUInt32(bytes, 0)
        End Function

        ''' <summary>
        ''' Phương thức chuyển đổi mảng bytes thành DWord.
        ''' </summary>
        ''' <param name="param1">byte thứ 1</param>
        ''' <param name="param2">byte thứ 2</param>
        ''' <param name="param3">byte thứ 3</param>
        ''' <param name="param4">byte thứ 4</param>
        ''' <returns>Giá trị kiểu DWord</returns>
        Public Shared Function FromBytes(param1 As Byte, param2 As Byte, param3 As Byte, param4 As Byte) As UInt32
            Dim bytes As Byte() = New Byte() {param1, param2, param3, param4}
            Return BitConverter.ToUInt32(bytes, 0)
        End Function

        ''' <summary>
        ''' Phương thức chuyển đổi mảng bytes thành DWord.
        ''' </summary>
        ''' <param name="bytes">Mảng bytes</param>
        ''' <returns>Giá trị kiểu DWord</returns>
        Public Shared Function ToArray(bytes As Byte()) As UInt32()
            Dim values As UInt32() = New UInt32(bytes.Length \ 4 - 1) {}
            Dim counter As Integer = 0
            For cnt As Integer = 0 To bytes.Length \ 4 - 1
                values(cnt) = FromByteArray(New Byte() {bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1)), bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1)), bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1)), bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1))})
            Next
            Return values
        End Function

#End Region

#Region "Chuyển đổi kiểu DWord thành mảng bytes."

        ''' <summary>
        ''' Phương thức chuyển đổi kiểu DWord thành mảng bytes.
        ''' </summary>
        ''' <param name="value">DWord cần chuyển</param>
        ''' <returns>Mảng bytes</returns>
        Public Shared Function ToByteArray(value As UInt32) As Byte()
            Dim byteArray As Byte() = BitConverter.GetBytes(value)
            Array.Reverse(byteArray)
            Return byteArray
        End Function

        ''' <summary>
        ''' Phương thức chuyển đổi kiểu DWord thành mảng bytes.
        ''' </summary>
        ''' <param name="value">DWord cần chuyển</param>
        ''' <returns>Mảng bytes</returns>
        Public Shared Function ToByteArray(value As UInt32()) As Byte()
            Dim arr As New ByteArray()
            For Each val As UInt32 In value
                arr.Add(ToByteArray(val))
            Next
            Return arr.array
        End Function

#End Region
    End Class
End Namespace
