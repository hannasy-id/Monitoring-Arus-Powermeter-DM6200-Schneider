Imports System.Collections.Generic
Imports System.Text

Namespace DataType
    Public Class Int

#Region "Khai báo các hằng số và các biến."

        Private _value As Short

#End Region

#Region "Chuyển đổi mảng kiểu byte thành kiểu Int."

        ''' <summary>
        ''' Phương thức chuyển đổi mảng byte thành kiểu int.
        ''' </summary>
        ''' <param name="bytes">Mảng giá trị kiểu byte</param>
        ''' <returns>Trả về giá trị kiểu int</returns>
        Public Shared Function FromByteArray(bytes As Byte()) As Short

            ' bytes[0] -> HighByte
            ' bytes[1] -> LowByte
            Return FromBytes(bytes(1), bytes(0))
        End Function

        ''' <summary>
        ''' Phương thức chuyển đổi 2 byte thành kiểu int.
        ''' </summary>
        ''' <param name="LoVal">Giá trị byte thấp</param>
        ''' <param name="HiVal">Giá trị byte cao</param>
        ''' <returns>Trả về giá trị kiểu int</returns>
        Public Shared Function FromBytes(LoVal As Byte, HiVal As Byte) As Short
            Return CShort(HiVal * 256 + LoVal)
        End Function

#End Region

#Region "Chuyển đổi kiểu Int/Int[] thành mảng kiểu byte."

        ''' <summary>
        ''' Phương thức chuyển kiểu Int thành mảng kiểu byte.
        ''' </summary>
        ''' <param name="value">Giá trị kiểu int</param>
        ''' <returns>Trả về mảng giá trị kiểu byte</returns>
        Public Shared Function ToByteArray(value As Short) As Byte()
            Dim byteArray As Byte() = BitConverter.GetBytes(value)
            Array.Reverse(byteArray)
            Return byteArray
        End Function

        ''' <summary>
        ''' Phương thức chuyển mảng kiểu Int thành mảng kiểu byte.
        ''' </summary>
        ''' <param name="value">Mảng giá trị kiểu Int</param>
        ''' <returns>Trả về mảng giá trị kiểu byte</returns>
        Public Shared Function ToByteArray(value As Short()) As Byte()
            Dim arr As New ByteArray()
            For Each val As Short In value
                arr.Add(ToByteArray(val))
            Next
            Return arr.array
        End Function

#End Region

#Region "ToArray"
        Public Shared Function ToArray(bytes As Byte()) As Short()
            Dim values As Short() = New Short(bytes.Length \ 2 - 1) {}
            Dim counter As Integer = 0
            For cnt As Integer = 0 To bytes.Length \ 2 - 1
                values(cnt) = FromByteArray(New Byte() {bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1)), bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1))})
            Next

            Return values
        End Function
#End Region

        ' conversion
        Public Shared Function CWord(value As Integer) As Short
            If value > 32767 Then
                value -= 32768
                value = 32768 - value
                value *= -1
            End If
            Return CShort(value)
        End Function

#Region "Các thuộc tính."

        Public Property Value() As Short
            Get
                Return Me._value
            End Get
            Set(value As Short)
                Me._value = value
            End Set
        End Property

#End Region

    End Class
End Namespace
