VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Warp点数据转换"
   ClientHeight    =   6345
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   9015
   LinkTopic       =   "Form1"
   ScaleHeight     =   6345
   ScaleWidth      =   9015
   StartUpPosition =   2  '屏幕中心
   Begin VB.Frame Frame3 
      Caption         =   "无法转换列表"
      Height          =   2415
      Left            =   120
      TabIndex        =   10
      Top             =   3720
      Width           =   8775
      Begin VB.TextBox Text5 
         Height          =   2055
         Left            =   120
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   11
         Top             =   240
         Width           =   8535
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "转换数据"
      Height          =   1815
      Left            =   120
      TabIndex        =   8
      Top             =   1800
      Width           =   8775
      Begin VB.TextBox Text4 
         Height          =   1455
         Left            =   120
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   9
         Top             =   240
         Width           =   8535
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "数据源"
      Height          =   1455
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   8775
      Begin VB.CommandButton Command1 
         Caption         =   "转换"
         Height          =   975
         Left            =   7560
         TabIndex        =   7
         Top             =   240
         Width           =   1095
      End
      Begin VB.TextBox Text3 
         Appearance      =   0  'Flat
         Height          =   270
         Left            =   1200
         TabIndex        =   6
         Text            =   "C:\Users\Administrator\Desktop\event\ECO_eventWARP.csv"
         Top             =   960
         Width           =   6255
      End
      Begin VB.TextBox Text2 
         Appearance      =   0  'Flat
         Height          =   270
         Left            =   1200
         TabIndex        =   4
         Text            =   "C:\Users\Administrator\Desktop\新建文件夹\warp.mdb"
         Top             =   600
         Width           =   6255
      End
      Begin VB.TextBox Text1 
         Appearance      =   0  'Flat
         Height          =   270
         Left            =   1200
         TabIndex        =   2
         Text            =   "D:\eco\Tools\MapScript\map_data.csv"
         Top             =   240
         Width           =   6255
      End
      Begin VB.Label Label3 
         Caption         =   "Warp数据"
         Height          =   255
         Left            =   120
         TabIndex        =   5
         Top             =   1080
         Width           =   1095
      End
      Begin VB.Label Label2 
         Caption         =   "Warp点数据"
         Height          =   255
         Left            =   120
         TabIndex        =   3
         Top             =   720
         Width           =   1095
      End
      Begin VB.Label Label1 
         Caption         =   "EventID数据"
         Height          =   255
         Left            =   120
         TabIndex        =   1
         Top             =   360
         Width           =   1215
      End
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim map_data As Object
Dim warp As Object
Dim warp_data As Object





Public Sub Command1_Click()
Set map_data = CreateObject("ADODB.Connection")
Set warp = CreateObject("ADODB.Connection")
Set warp_data = CreateObject("ADODB.Connection")
Dim ConnStr  As String
ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & getpath(Text1.Text) & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
map_data.Open ConnStr
ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Text2.Text
warp.Open ConnStr
ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & getpath(Text3.Text) & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
warp_data.Open ConnStr

'Set warp_datars = warp_data.Execute("SELECT * FROM " & gettable(Text3.Text))
Dim nowEventId As String
'While Not warp_datars.EOF
warpdatacsv = Split(OpenFile(Text3.Text), vbCrLf)
For i = 0 To UBound(warpdatacsv)
warp_datars = Split(warpdatacsv(i), ",")
        If nowEventId <> "" And Left(warp_datars(1), 3) = "EVT" Then
            Text5.SelText = "错误的Eventid:" & nowEventId & vbCrLf
        End If
        
        If warp_datars(1) = "EVT10000056" Then
        MsgBox ""
        End If
        
        If Left(warp_datars(1), 3) = "EVT" Then
            nowEventId = Mid(warp_datars(1), 4)
        End If
If UBound(warp_datars) >= 2 Then
        If Left(warp_datars(2), 4) = "WARP" And nowEventId <> "" Then
        If UBound(Split(warp_datars(2), " ")) = 1 Then
        
          '  Call GetWarpMap(Split(warp_datars(2), " ")(1), nowEventId)
            
            nowEventId = ""
            End If
        End If
        End If
Next
'    warp_datars.MoveNext
'Wend
End Sub
Function getpath(url)
Dim Length As Integer
Length = Len(url) - Len(Split(url, "\")(UBound(Split(url, "\"))))
getpath = Left(url, Length)
End Function
Function gettable(url)
gettable = Replace(Split(url, "\")(UBound(Split(url, "\"))), ".", "#")
End Function

Public Function GetWarpMap(ByVal id As String, eventid As String)

Set warprs = warp.Execute("SELECT * FROM warp where MapId <> Null and id = '" & id & "'")
If Not warprs.EOF Then
Text4.SelText = EventGetMapName(eventid) & "," & GetMapName(Val(warprs("MapId"))) & "," & warprs("MapId") & "," & eventid & "," & warprs("X") & "," & warprs("Y") & vbCrLf
End If
End Function
Public Function EventGetMapName(eventid As String)

Set map_datars = map_data.Execute("SELECT * FROM " & gettable(Text1.Text) & " where EVENTID = " & eventid)
If Not map_datars.EOF Then
EventGetMapName = map_datars("地图名称")
End If
End Function

Public Function GetMapName(mapid)

Set map_datars = map_data.Execute("SELECT * FROM " & gettable(Text1.Text) & " where 地图ID = " & mapid)
If Not map_datars.EOF Then
GetMapName = map_datars("地图名称")
End If
End Function
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

