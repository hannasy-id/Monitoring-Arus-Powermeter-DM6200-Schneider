Imports System.Collections.Generic
Imports System.Text

Namespace DataType
    Public Class Word
#Region "Chuyển đổi mảng bytes thành kiểu Word."

        ''' <summary>
        ''' Phương thức chuyển đổi mảng bytes thành kiểu Word.
        ''' </summary>
        ''' <param name="bytes">Mảng byte cần chuyển đổi</param>
        ''' <returns>Trả về giá trị kiểu Word</returns>
        Public Shared Function FromByteArray(bytes As Byte()) As UInt16
            ' bytes[0] -> HighByte
            ' bytes[1] -> LowByte
            Return FromBytes(bytes(1), bytes(0))
        End Function

        ''' <summary>
        ''' Phương thức chuyển đổi mảng bytes thành kiểu Word.
        ''' </summary>
        ''' <param name="LoVal">Giá trị byte thấp</param>
        ''' <param name="HiVal">Giá trị byte cao</param>
        ''' <returns>Trả về giá trị kiểu Word</returns>
        Public Shared Function FromBytes(LoVal As Byte, HiVal As Byte) As UInt16
            Return CType(HiVal * 256 + LoVal, UInt16)
        End Function

#End Region

#Region "Chuyển đổi kiểu Word thành mảng bytes."

        ''' <summary>
        ''' Phương thức chuyển đổi kiểu Word thành mảng bytes.
        ''' </summary>
        ''' <param name="value">Giá trị kiểu Word</param>
        ''' <returns>Trả về giá trị mảng kiểu byte</returns>
        Public Shared Function ToByteArray(value As UInt16) As Byte()
            Dim array1 As Byte() = BitConverter.GetBytes(value)
            Array.Reverse(array1)
            Return array1
        End Function

        ''' <summary>
        ''' Phương thức chuyển đổi mảng kiểu Word thành mảng bytes.
        ''' </summary>
        ''' <param name="value">Mảng kiểu Word</param>
        ''' <returns>Trả về mảng kiểu byte</returns>
        Public Shared Function ToByteArray(value As UInt16()) As Byte()
            Dim arr As New ByteArray()
            For Each val As UInt16 In value
                arr.Add(ToByteArray(val))
            Next
            Return arr.array
        End Function

        ''' <summary>
        ''' Phương thức chuyển đổi kiểu mảng bytes thành mảng word.
        ''' </summary>
        ''' <param name="bytes">Giá trị mảng bytes</param>
        ''' <returns>Trả về giá trị mảng kiểu Word</returns>
        Public Shared Function ToArray(bytes As Byte()) As UInt16()
            Dim values As UInt16() = New UInt16(bytes.Length \ 2 - 1) {}
            Dim counter As Integer = 0
            For cnt As Integer = 0 To values.Length - 1 Step 2
                values(cnt) = FromByteArray(New Byte() {bytes(cnt), bytes(cnt + 1)})
            Next
            Return values
        End Function


#End Region

    End Class
End Namespace
