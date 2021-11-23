Attribute VB_Name = "Module1"
Public Function OpenFile(FileName As String)
    Dim Str() As Byte
    Dim Length As Long

    If Dir$(FileName) = "" Then
        OpenFile = Null
        Exit Function
    End If

    Length = FileLen(FileName)

    ReDim Str(Length) As Byte
    Open FileName For Binary As #1
    Get #1, , Str
    Close #1
    OpenFile = StrConv(Str, vbUnicode)
End Function
