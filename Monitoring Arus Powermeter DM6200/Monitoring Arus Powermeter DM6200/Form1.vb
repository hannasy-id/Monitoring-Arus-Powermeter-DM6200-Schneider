Imports System.IO.Ports
Imports System.Threading


Public Class Form1
    Dim thread As System.Threading.Thread
    'Declare variables & constants
    Private serialPort As SerialPort = Nothing

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CheckForIllegalCrossThreadCalls = False

        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText("comport.txt")

        serialPort = New SerialPort(fileReader, 9600, Parity.None, 8, StopBits.One)
        serialPort.Open() 'Open COM1
        thread = New System.Threading.Thread(AddressOf dataSerialPort)
        thread.Start()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        serialPort.Close() ' Close COM1.
        thread.Abort()
    End Sub

    Private Sub dataSerialPort()
        Do While 1
            Dim data1 As String = Convert.ToString(Convert.ToInt32(mintaData(43929), 16), 2)
            Dim data2 As String = Convert.ToString(Convert.ToInt32(mintaData(43957), 16), 2)

            Dim lastValue1 As Double = binToDouble(data1)
            Dim lastValue2 As Double = binToDouble(data2)

            txtRespond.ForeColor = warnaNilai(lastValue1)
            txtRespond2.ForeColor = warnaNilai(lastValue2)

            txtRespond.Text = FormatNumber(lastValue1, belakangKoma(lastValue1))
            txtRespond2.Text = FormatNumber(lastValue2, belakangKoma(lastValue2))
        Loop
    End Sub

    Private Function mintaData(ByVal address As UShort) As String

        Dim strs As String = ""
        Dim slaveAddress As Byte = 1
        Dim functionCode As Byte = 3
        Dim startAddress As UShort = address - 40001
        Dim numberOfPoints = 2 ' Read 4 Float.

        Dim frame As Byte() = Me.ReadHolingRegisters(slaveAddress, functionCode, startAddress, numberOfPoints)
        serialPort.Write(frame, 0, frame.Length) ' Send frame to modbus slave.

        thread.Sleep(100) ' Delay 100ms.

        If serialPort.BytesToRead > 5 Then
            Dim buffRecei As Byte() = New Byte(serialPort.BytesToRead) {}
            serialPort.Read(buffRecei, 0, buffRecei.Length) ' Read data from modbus slave.
            Dim buf As String
            If buffRecei.Length = 10 Then
                Dim ss As String = Me.DisplayValue(buffRecei)
                strs = Mid(ss, 16, 2) + Mid(ss, 19, 2) + Mid(ss, 10, 2) + Mid(ss, 13, 2)
                buf = strs
                Return strs
            Else
                Return buf
            End If
        End If
    End Function

    Private Function warnaNilai(ByVal data As Double) As Color
        Dim C As Color

        If data < 50 Then
            C = Color.Green
        ElseIf (data >= 50) And (data < 65) Then
            C = Color.Yellow
        ElseIf data >= 65 Then
            C = Color.Red
        End If

        Return C
    End Function

    Private Function belakangKoma(ByVal koma As Double) As Integer
        Dim belKoma As Integer

        If koma < 10.0 Then
            belKoma = 3
        ElseIf (koma >= 10.0) Or (koma < 100.0) Then
            belKoma = 2
        ElseIf koma >= 100.0 Then
            belKoma = 1
        End If

        Return belKoma
    End Function

    Private Function ReadHolingRegisters(ByVal slaveAddress As Byte, ByVal functionCode As Byte, ByVal startAddress As UShort, ByVal numberOfPoints As UShort) As Byte()
        Dim frame As Byte() = New Byte(7) {} ' Total 8 Bytes
        frame(0) = slaveAddress ' Slave Address
        frame(1) = functionCode 'Function
        frame(2) = CByte(startAddress / 256) 'Starting Address Hi.
        frame(3) = CByte(startAddress Mod 256) 'Starting Address Lo.
        frame(4) = CByte(numberOfPoints / 256) ' Quantity of Registers Hi.
        frame(5) = CByte(numberOfPoints Mod 256) ' Quantity of Registers Lo.
        Dim crc As Byte() = Me.CRC(frame) ' Call CRC Calculate.
        frame(6) = crc(0) ' Error Check Lo
        frame(7) = crc(1) ' Error Check Hi
        Return frame '
    End Function

    ' Modbus CRC calculation in VB (Tested with success).
    Private Function CRC(ByVal data As Byte()) As Byte()
        Dim CRCFull As UShort = &HFFFF
        Dim CRCHigh As Byte = &HF, CRCLow As Byte = &HFF
        Dim CRCLSB As Char
        Dim result As Byte() = New Byte(1) {}
        For i As Integer = 0 To (data.Length) - 3
            CRCFull = CUShort(CRCFull Xor data(i))

            For j As Integer = 0 To 7
                CRCLSB = ChrW(CRCFull And &H1)
                CRCFull = CUShort((CRCFull >> 1) And &H7FFF)

                If Convert.ToInt32(CRCLSB) = 1 Then
                    CRCFull = CUShort(CRCFull Xor &HA001)
                End If
            Next
        Next
        CRCHigh = CByte((CRCFull >> 8) And &HFF)
        CRCLow = CByte(CRCFull And &HFF)
        Return New Byte(1) {CRCLow, CRCHigh}
    End Function

    'Display frame.
    Private Function DisplayValue(ByVal values As Byte()) As String
        Dim result As String = String.Empty
        For Each item As Byte In values
            result += String.Format("{0:X2} ", item)
        Next
        Return result
    End Function

    Private Function binToDouble(ByVal binstring As String) As Double
        Dim sad As Integer = Len(binstring)
        sad = 32 - sad
        Dim srp As String = ""
        If sad > 0 Then
            For sad = 0 To sad - 1
                srp += "0"
            Next
        End If
        srp = srp + binstring
        '----------------------------------------------
        Dim sign As Integer = (-1) ^ Mid(srp, 1, 1)
        '
        Dim c As Integer = 0
        Dim ex As Integer = 0
        For i As Integer = 9 To 2 Step -1
            If Mid(srp, i, 1) = "1" Then
                ex += (2 ^ c)
            End If
            c = c + 1
        Next
        ex = ex - 127
        '
        Dim cmantis As Double = 1
        Dim mantis As Double = 0
        For i As Integer = 10 To 32
            cmantis = cmantis * 0.5
            If Mid(srp, i, 1) = "1" Then
                mantis = mantis + cmantis
            End If
        Next
        '-----------------------------------------------
        Return sign * (1 + mantis) * 2 ^ ex
    End Function
End Class
