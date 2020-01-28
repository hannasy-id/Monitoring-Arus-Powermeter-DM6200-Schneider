Imports System.Collections.Generic
Imports System.Text

Namespace DataType
    Public Class Real

#Region "Chuyển đổi mảng dữ liệu byte thành kiểu dữ liệu float."

        ''' <summary>
        ''' Phương thức chuyển mảng dữ liệu kiểu byte thành kiểu dữ liệu float. 
        ''' </summary>
        ''' <param name="bytes"></param>
        ''' <returns>Trả về giá trị kiểu float.</returns>
        Public Shared Function FromByteArray(bytes As Byte()) As Single
            Array.Reverse(bytes)
            Return BitConverter.ToSingle(bytes, 0)
        End Function

#End Region

#Region "Chuyển đổi kiểu dữ liệu DWord thành kiểu dữ liệu float."

        ''' <summary>
        ''' Phương thức chuyển đổi dữ liệu từ kiểu DWord thành kiểu dữ liệu float.
        ''' </summary>
        ''' <param name="value">Giá trị kiểu DWord</param>
        ''' <returns>Trả về giá trị kiểu float.</returns>
        Public Shared Function FromDWord(value As Int32) As Single
            Dim b As Byte() = DInt.ToByteArray(value)
            Dim d As Single = FromByteArray(b)
            Return d
        End Function

        ''' <summary>
        ''' Phương thức chuyển đổi dữ liệu từ kiểu DWord thành kiểu dữ liệu float.
        ''' </summary>
        ''' <param name="value">Giá trị kiểu DWord</param>
        ''' <returns>Trả về giá trị kiểu float.</returns>
        Public Shared Function FromDWord(value As UInt32) As Single
            Dim b As Byte() = DWord.ToByteArray(value)
            Dim d As Single = FromByteArray(b)
            Return d
        End Function

#End Region

#Region "Chuyển đổi kiểu dữ liệu float thành mảng dữ liệu kiểu byte."

        ''' <summary>
        ''' Phương thức chuyển đổi kiểu dữ liệu float thành mảng dữ liệu kiểu byte.
        ''' </summary>
        ''' <param name="value">Giá trị kiểu float</param>
        ''' <returns>Trả về mảng giá trị kiểu byte</returns>
        Public Shared Function ToByteArray(value As Single) As Byte()
            Dim array__1 As Byte() = BitConverter.GetBytes(value)
            Array.Reverse(array__1)
            Return array__1
        End Function

        ''' <summary>
        ''' Phương thức chuyển đổi mảng dữ liệu kiểu float thành mảng dữ liệu kiểu byte.
        ''' </summary>
        ''' <param name="value">Mảng giá trị kiểu float</param>
        ''' <returns>Trả về mảng giá trị kiểu byte</returns>
        Public Shared Function ToByteArray(value As Single()) As Byte()
            Dim arr As New ByteArray()
            For Each val As Single In value
                arr.Add(ToByteArray(val))
            Next
            Return arr.array
        End Function

#End Region

#Region "Chuyển đổi mảng dữ liệu kiểu byte thành mảng dữ liệu kiểu float."

        ''' <summary>
        ''' Phương thức chuyển đổi mảng dữ liệu kiểu byte thành mảng dữ liệu kiểu float.
        ''' </summary>
        ''' <param name="bytes">Mảng giá trị kiểu byte</param>
        ''' <returns>Trả về mảng giá trị kiểu float</returns>
        Public Shared Function ToArrayInverse(bytes As Byte()) As Single()
            Dim values As Single() = New Single(bytes.Length \ 4 - 1) {}

            Dim counter As Integer = 0
            For cnt As Integer = 0 To bytes.Length \ 4 - 1
                values(cnt) = FromByteArray(New Byte() {bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1)), bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1)), bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1)), bytes(System.Math.Max(System.Threading.Interlocked.Increment(counter), counter - 1))})
            Next

            Return values
        End Function

        Public Shared Function ToFloat(bytes As Byte()) As Single
            If bytes.Length <> 4 Then
                Throw New FormatException("Size of byte array > 4)")
            End If
            Array.Reverse(bytes)
            Dim size As Integer = bytes.Length \ 2
            For i As Integer = 0 To size - 1
                bytes(i) += bytes(i + size)
                bytes(i + size) = CByte(bytes(i) - bytes(i + size))
                bytes(i) = CByte(bytes(i) - bytes(i + size))
            Next
            Return BitConverter.ToSingle(bytes, 0)
        End Function

        Public Shared Function ToArray(bytes As Byte()) As Single()
            Dim size As Integer = 4
            Dim idx As Integer = 0
            Dim result As Single() = New Single(bytes.Length \ size - 1) {}
            Do
                Dim data As Byte() = New Byte(size - 1) {}
                Array.Copy(bytes, idx, data, 0, data.Length)
                result(idx \ size) = ToFloat(data)
                idx += size
            Loop While idx < bytes.Length
            Return result
        End Function

#End Region

#Region "Chuyển byte thành chuỗi binary."

        ''' <summary>
        ''' Phương thức chuyển byte thành chuỗi binary.
        ''' </summary>
        ''' <param name="value">Giá trị kiểu byte</param>
        ''' <returns>Trả về chuỗi binary</returns>
        Private Shared Function ValToBinString(value As Byte) As String
            Dim txt As String = ""

            Dim cnt As Integer = 7
            While cnt >= 0
                If (value And CByte(Math.Truncate(Math.Pow(2, cnt)))) > 0 Then
                    txt += "1"
                Else
                    txt += "0"
                End If
                cnt += -1
            End While
            Return txt
        End Function
#End Region

#Region "Chuyển chuỗi binary thành byte."

        ''' <summary>
        ''' Phương thức chuyển đổi chuỗi binary thành kiểu byte
        ''' </summary>
        ''' <param name="txt">Giá trị là 1 chuỗi binary</param>
        ''' <returns>Trả về giá trị kiểu byte</returns>
        Private Shared Function BinStringToByte(txt As String) As System.Nullable(Of Byte)
            Dim cnt As Integer = 0
            Dim ret As Integer = 0

            If txt.Length = 8 Then
                cnt = 7
                While cnt >= 0
                    If Integer.Parse(txt.Substring(cnt, 1)) = 1 Then
                        ret += CInt(Math.Truncate(Math.Pow(2, (txt.Length - 1 - cnt))))
                    End If
                    cnt += -1
                End While
                Return CByte(ret)
            End If
            Return Nothing
        End Function
#End Region
    End Class
End Namespace
