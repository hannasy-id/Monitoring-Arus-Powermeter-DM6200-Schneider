Imports System.Collections.Generic
Imports System.Text
Imports System.Collections

Namespace DataType
    Public Class [Boolean]

        ''' <summary>
        ''' Phương thức chuyển đổi mảng dữ liệu kiểu byte thành mảng dữ liệu kiểu bool.
        ''' </summary>
        ''' <param name="value">Mảng giá trị kiểu byte</param>
        ''' <returns>Mảng giá trị kiểu bool</returns>
        Public Shared Function ToArray(value As Byte()) As Boolean()
            Dim result As New List(Of Boolean)()
            Dim bits As New BitArray(value)
            For i As Integer = 0 To bits.Count - 1
                result.Add(bits(i))
            Next
            Return result.ToArray()
        End Function

        ''' <summary>
        ''' Phương thức chuyển đổi mảng dữ liệu kiểu bool thành mảng dữ liệu kiểu byte.
        ''' </summary>
        ''' <param name="bits">Mảng giá trị kiểu bool</param>
        ''' <returns>Trả về mảng giá trị kiểu byte</returns>
        Public Shared Function ToByteArray(bits As Boolean()) As Byte()
            Dim numBytes As Integer = bits.Length \ 8
            Dim bitEven As Integer = bits.Length Mod 8
            If bitEven <> 0 Then
                'List<bool> bitsTemp = new List<bool>();
                'for (int i = 0; i < bitEven; i++)
                '{

                '}
                numBytes += 1
            End If
            Array.Reverse(bits)
            Dim bytes As Byte() = New Byte(numBytes - 1) {}
            Dim byteIndex As Integer = 0, bitIndex As Integer = 0

            For i As Integer = 0 To bits.Length - 1
                If bits(i) Then
                    bytes(byteIndex) = bytes(byteIndex) Or CByte(1 << (7 - bitIndex))
                End If

                bitIndex += 1
                If bitIndex = 8 Then
                    bitIndex = 0
                    byteIndex += 1
                End If
            Next
            Array.Reverse(bytes)
            Return bytes
        End Function

        Public Shared Function GetValue(value As Byte, bit As Integer) As Boolean
            If (value And CInt(Math.Truncate(Math.Pow(2, bit)))) <> 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Function SetBit(value As Byte, bit As Integer) As Byte
            Return CByte(value Or CByte(Math.Truncate(Math.Pow(2, bit))))
        End Function

        Public Shared Function ClearBit(value As Byte, bit As Integer) As Byte
            Return CByte(value And CByte(Not CByte(Math.Truncate(Math.Pow(2, bit)))))
        End Function
    End Class
End Namespace
