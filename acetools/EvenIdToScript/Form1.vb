Imports System.Data.OleDb
Imports System.Data
Imports EvenIdToScript.tool
Public Class Main
    Dim AccessConn5 As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\npc.mdb")

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If OD1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = OD1.FileName
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If OD2.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = OD2.FileName
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If FD1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox3.Text = FD1.SelectedPath
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox1.Text.Length = 0 Or TextBox2.Text.Length = 0 Or TextBox3.Text.Length = 0 Then
            MsgBox("请将目录设定！")
            Exit Sub
        End If
        Dim constr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Replace(TextBox1.Text, TextBox1.Text.Split("\")(TextBox1.Text.Split("\").Length - 1), "") & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
        Dim con As OleDbConnection = New OleDbConnection(constr)
        con.Open()
        Dim rs As OleDbCommand = New OleDbCommand("SELECT * FROM " & TextBox1.Text.Split("\")(TextBox1.Text.Split("\").Length - 1).Replace(".", "#") & ";", con)
        Dim constr2 As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Replace(TextBox2.Text, TextBox2.Text.Split("\")(TextBox2.Text.Split("\").Length - 1), "") & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
        Dim con2 As OleDbConnection = New OleDbConnection(constr2)
        con2.Open()
        Dim rs2 As OleDbCommand
        Dim reader As OleDbDataReader = rs.ExecuteReader
        Dim reader2 As OleDbDataReader
        Dim att As String
        Dim tmp, tmp2 As String
        Dim log As String = ""
        While reader.Read()
            If reader(0).ToString <> "" And Mid(reader(0).ToString, 1, 1) <> "#" Then
                att = "SELECT * FROM " & TextBox2.Text.Split("\")(TextBox2.Text.Split("\").Length - 1).Replace(".", "#") & " where MAPID =" & reader(0).ToString() & ";"
                rs2 = New OleDbCommand(att, con2)
                reader2 = rs2.ExecuteReader
                If reader2.HasRows = True Then
                    If CheckBox1.Checked = True Then
                        tmp = TextBox3.Text & "\" & chs2py.ToPinyin((reader(1).ToString & "(" & reader(0).ToString & ")").Replace("?", "？").Replace(":", "："))
                    Else
                        tmp = TextBox3.Text & "\" & (reader(1).ToString & "(" & reader(0).ToString & ")").Replace("?", "？").Replace(":", "：")
                    End If
                    System.IO.Directory.CreateDirectory(tmp)


                    While reader2.Read()

                        If reader2(0).ToString <> "" And Mid(reader2(0).ToString, 1, 1) <> "#" Then
                            If CheckBox1.Checked = True Then
                                tmp2 = chs2py.ToPinyin((reader2(1).ToString & "-" & reader2(0).ToString).Replace("?", "？").Replace(":", "："))
                            Else
                                tmp2 = (reader2(1).ToString & "-" & reader2(0).ToString).Replace("?", "？").Replace(":", "：")
                            End If
                            My.Computer.FileSystem.WriteAllText(tmp & "\" & tmp2 & ".cs", Cscript(reader2(0).ToString, reader2(0).ToString, reader2, reader), False)
                            log += tmp & "\" & tmp2 & ".cs" & "," & reader2(0).ToString & vbCrLf

                            'ListBox1.Items.Add(reader(1).ToString & "-" & reader2(1).ToString & "-" & reader2(0).ToString)
                        End If
                    End While
                End If
            End If
        End While
        My.Computer.FileSystem.WriteAllText(".\log.csv", log, False)
        MsgBox("生成成功!")






    End Sub

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    Public Function Cscript(ByVal classname As String, ByVal EventId As String, ByRef reader As OleDbDataReader, ByRef reader2 As OleDbDataReader, Optional ByVal ScriptText As String = "", Optional ByVal ScriptText2 As String = "")
        Dim script As String = ""
        script += "using System;" & vbCrLf
        script += "using System.Collections.Generic;" & vbCrLf
        script += "using System.Text;" & vbCrLf
        script += "" & vbCrLf
        script += "using SagaDB.Actor;" & vbCrLf
        script += "using SagaMap.Scripting;" & vbCrLf
        script += "//所在地图:" & (reader2(1).ToString & "(" & reader2(0).ToString & ")").Replace("?", "？").Replace(":", "：") & "NPC基本信息:" & reader(0).ToString & "-" & reader(1).ToString & "- X:" & reader(3).ToString & " Y:" & reader(4).ToString & vbCrLf
        script += "namespace SagaScript.M" & reader2(0).ToString & vbCrLf
        script += "{" & vbCrLf
        script += "    public class S" & classname & " : Event" & vbCrLf
        script += "    {" & vbCrLf
        script += "    public S" & classname & "()" & vbCrLf
        script += "        {" & vbCrLf
        script += "            this.EventID = " & EventId & ";" & vbCrLf
        script += ScriptText2
        script += "        }" & vbCrLf
        script += "" & vbCrLf
        script += "" & vbCrLf
        script += "        public override void OnEvent(ActorPC pc)" & vbCrLf
        script += "        {" & vbCrLf
        script += ScriptText & vbCrLf
        script += "        }" & vbCrLf
        script += "    }" & vbCrLf
        script += "}" & vbCrLf
        Return script
    End Function

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        TextBox6.Text = ""
        Dim tmp(), eventid, tmp2 As String
        Dim startline As Integer
        If TextBox5.Text.Substring(0, 8) = "EventId:" Then
            '存在EventId数据
            tmp = TextBox5.Text.Split(vbCrLf)
            startline = 1
            eventid = tmp(0).Substring(8)
        Else
            tmp = TextBox5.Text.Split(vbCrLf)
            startline = 0
            eventid = 0
        End If
        '开始生成脚本数据
        Dim i As Integer
        For i = startline To tmp.Length - 1
            tmp2 = ""


            If tmp(i).Substring(1, 5) = "WAIT:" Then
                TextBox6.Text += "            Wait(pc," & tmp(i).Substring(6) & ");" & vbCrLf
                Continue For
            End If

            If tmp(i).Substring(1, 5) = "Menu:" Then
                Dim k As Integer
                tmp(i) = tmp(i).Substring(6)
                tmp2 += String.Format("            switch (Select(pc, ""{0}"",""{1}""", tmp(i).Split("|")(0), tmp(i).Split("|")(2))
                For k = 0 To tmp(i).Split("|")(1).Split(":").Length - 2
                    tmp2 += ",""" & tmp(i).Split("|")(1).Split(":")(k) & """"
                Next
                tmp2 += "))" & vbCrLf & "            {" & vbCrLf
                For k = 0 To tmp(i).Split("|")(1).Split(":").Length - 2
                    tmp2 += "                case " & k + 1 & ":" & vbCrLf & "                    break;" & vbCrLf

                Next
                tmp2 += "            }"
                TextBox6.Text += tmp2 & vbCrLf
                tmp2 = ""
                Continue For
            End If

            Do
                If tmp(i).Substring(1, 1) <> "$" Then
                    tmp2 = "            Say(pc," & tmp(i).Split(",")(1) & ",""" & tmp(i).Split(",")(0).Substring(1) & ";"" +" & vbCrLf
                    i += 1
                    If tmp.Length = i Then
                        If tmp2.Substring(tmp2.Length - 3) = "+" & vbCrLf Then
                            tmp2 = tmp2.Substring(0, tmp2.Length - 4) & ", """ & tmp(i - 1).Split(",")(2) & """);"
                        End If


                        TextBox6.Text += tmp2 & vbCrLf
                        Exit Do
                    End If
                    eventid = tmp(i).Substring(1, 1)
                    If tmp(i).Substring(1, 1) <> "$" Then
                        '输出之前脚本
                        If tmp2.Substring(tmp2.Length - 3) = "+" & vbCrLf Then
                            tmp2 = tmp2.Substring(0, tmp2.Length - 4) & ", """ & tmp(i - 1).Split(",")(2) & """);"
                        End If

                        TextBox6.Text += tmp2 & vbCrLf
                        i -= 1
                        Exit Do
                    End If
                Else
                    tmp2 += "                """ & tmp(i).Split(",")(0).Substring(1) & """ +" & vbCrLf
                    i += 1
                    If tmp.Length = i Then
                        If tmp2.Substring(tmp2.Length - 3) = "+" & vbCrLf Then
                            tmp2 = tmp2.Substring(0, tmp2.Length - 4) & ", """ & tmp(i - 1).Split(",")(2) & """);"
                        End If
                        TextBox6.Text += tmp2 & vbCrLf
                        Exit Do
                    End If
                    If tmp(i).Substring(1, 1) <> "$" Then
                        '输出之前脚本
                        If tmp2.Substring(tmp2.Length - 3) = "+" & vbCrLf Then
                            tmp2 = tmp2.Substring(0, tmp2.Length - 4) & ", """ & tmp(i - 1).Split(",")(2) & """);"
                        End If
                        TextBox6.Text += tmp2 & vbCrLf
                        i -= 1
                        Exit Do
                    End If
                End If

            Loop
        Next


    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim str As String = ""
        Dim constr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Replace(TextBox1.Text, TextBox1.Text.Split("\")(TextBox1.Text.Split("\").Length - 1), "") & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
        Dim con As OleDbConnection = New OleDbConnection(constr)
        con.Open()
        Dim rs As OleDbCommand = New OleDbCommand("SELECT * FROM " & TextBox1.Text.Split("\")(TextBox1.Text.Split("\").Length - 1).Replace(".", "#") & ";", con)
        Dim reader As OleDbDataReader = rs.ExecuteReader
        While reader.Read()
            str += "<mapID>" & reader(0).ToString & "</mapID>" & vbCrLf
        End While
        TextBox6.Text = str
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If TextBox1.Text.Length = 0 Or TextBox2.Text.Length = 0 Then
            MsgBox("请将目录设定！再读入NPC数据库")
            Exit Sub
        End If

        Button9.Enabled = False

        Dim constr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Replace(TextBox1.Text, TextBox1.Text.Split("\")(TextBox1.Text.Split("\").Length - 1), "") & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
        Dim con As OleDbConnection = New OleDbConnection(constr)
        con.Open()
        Dim rs As OleDbCommand = New OleDbCommand("SELECT * FROM " & TextBox1.Text.Split("\")(TextBox1.Text.Split("\").Length - 1).Replace(".", "#") & ";", con)
        Dim constr2 As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Replace(TextBox2.Text, TextBox2.Text.Split("\")(TextBox2.Text.Split("\").Length - 1), "") & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
        Dim con2 As OleDbConnection = New OleDbConnection(constr2)
        con2.Open()
        Dim rs2 As OleDbCommand
        Dim reader As OleDbDataReader = rs.ExecuteReader
        Dim reader2 As OleDbDataReader
        Dim att As String
        Dim tmp As TreeNode
        Dim log As String = ""
        
        While reader.Read()
            If reader(0).ToString <> "" And Mid(reader(0).ToString, 1, 1) <> "#" Then
                att = "SELECT * FROM " & TextBox2.Text.Split("\")(TextBox2.Text.Split("\").Length - 1).Replace(".", "#") & " where MAPID =" & reader(0).ToString() & ";"
                rs2 = New OleDbCommand(att, con2)
                reader2 = rs2.ExecuteReader
                If reader2.HasRows = True Then

                    tmp = T.Nodes.Add(reader(0).ToString, reader(1).ToString & "(" & reader(0).ToString & ")")
                    While reader2.Read()

                        If reader2(0).ToString <> "" And Mid(reader2(0).ToString, 1, 1) <> "#" Then

                            tmp.Nodes.Add(reader2(0).ToString, reader2(1).ToString & "-" & reader2(0).ToString)
                        End If
                    End While
                End If
            End If
        End While

    End Sub

    Private Sub T_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles T.AfterSelect
        If T.SelectedNode.IsSelected() And T.SelectedNode.Level = 1 Then
            Dim AccessConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\npc.mdb")
            AccessConn.Open()
            Dim cmd As OleDbCommand 

            If TextBox7.Text <> "" Then
                '保存
                cmd = New OleDbCommand
                cmd.CommandText = "SELECT count(*) FROM Script where eventid = " & GroupBox5.Text.Split(":")(1)
                cmd.Connection = AccessConn
                If cmd.ExecuteScalar = 0 Then
                    '插入数据
                    cmd = New OleDbCommand
                    cmd.CommandText = String.Format("INSERT INTO script(eventid,script,script2) Values('{0}','{1}','{2}')", GroupBox5.Text.Split(":")(1), TextBox7.Text, TextBox8.Text)
                    cmd.Connection = AccessConn
                    cmd.ExecuteNonQuery()
                Else
                    '刷新数据
                    cmd = New OleDbCommand
                    cmd.CommandText = String.Format("Update Script set script='{1}',Script2='{2}' where eventid={0}", GroupBox5.Text.Split(":")(1), TextBox7.Text, TextBox8.Text)
                    cmd.Connection = AccessConn
                    cmd.ExecuteNonQuery()
                End If

            End If



            cmd = New OleDbCommand
            cmd.CommandText = "SELECT script,script2 FROM Script where eventid = " & T.SelectedNode.Name
            cmd.Connection = AccessConn



            If cmd.ExecuteScalar <> "" Then
                GroupBox5.Text = "正在编辑:" & T.SelectedNode.Name
                Dim a As OleDbDataReader = cmd.ExecuteReader
                a.Read()
                TextBox7.Text = a(0).ToString
                TextBox8.Text = a(1).ToString
            Else
                GroupBox5.Text = "新建的编辑:" & T.SelectedNode.Name
                TextBox7.Text = ""
                TextBox8.Text = ""
            End If
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim AccessConn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\npc.mdb")
        AccessConn.Open()
        Dim cmd As OleDbCommand

        If TextBox7.Text <> "" Then
            '保存
            cmd = New OleDbCommand
            cmd.CommandText = "SELECT count(*) FROM Script where eventid = " & GroupBox5.Text.Split(":")(1)
            cmd.Connection = AccessConn
            If cmd.ExecuteScalar = 0 Then
                '插入数据
                cmd = New OleDbCommand
                cmd.CommandText = String.Format("INSERT INTO script(eventid,script,script2) Values('{0}','{1}','{2}')", GroupBox5.Text.Split(":")(1), TextBox7.Text, TextBox8.Text)
                cmd.Connection = AccessConn
                cmd.ExecuteNonQuery()
            Else
                '刷新数据
                cmd = New OleDbCommand
                cmd.CommandText = String.Format("Update Script set script='{1}',Script2='{2}' where eventid={0}", GroupBox5.Text.Split(":")(1), TextBox7.Text, TextBox8.Text)
                cmd.Connection = AccessConn
                cmd.ExecuteNonQuery()
            End If

        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If TextBox1.Text.Length = 0 Or TextBox2.Text.Length = 0 Or TextBox3.Text.Length = 0 Then
            MsgBox("请将目录设定！")
            Exit Sub
        End If
        AccessConn5.Open()
        Dim constr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Replace(TextBox1.Text, TextBox1.Text.Split("\")(TextBox1.Text.Split("\").Length - 1), "") & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
        Dim con As OleDbConnection = New OleDbConnection(constr)
        con.Open()
        Dim rs As OleDbCommand = New OleDbCommand("SELECT * FROM " & TextBox1.Text.Split("\")(TextBox1.Text.Split("\").Length - 1).Replace(".", "#") & ";", con)
        Dim constr2 As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Replace(TextBox2.Text, TextBox2.Text.Split("\")(TextBox2.Text.Split("\").Length - 1), "") & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
        Dim con2 As OleDbConnection = New OleDbConnection(constr2)
        con2.Open()
        Dim rs2 As OleDbCommand
        Dim reader As OleDbDataReader = rs.ExecuteReader
        Dim reader2 As OleDbDataReader
        Dim att As String
        Dim tmp, tmp2, tmp3() As String
        Dim log As String = ""
        While reader.Read()
            If reader(0).ToString <> "" And Mid(reader(0).ToString, 1, 1) <> "#" Then
                att = "SELECT * FROM " & TextBox2.Text.Split("\")(TextBox2.Text.Split("\").Length - 1).Replace(".", "#") & " where MAPID =" & reader(0).ToString() & ";"
                rs2 = New OleDbCommand(att, con2)
                reader2 = rs2.ExecuteReader
                If reader2.HasRows = True Then
                    If CheckBox1.Checked = True Then
                        tmp = TextBox3.Text & "\" & chs2py.ToPinyin((reader(1).ToString & "(" & reader(0).ToString & ")").Replace("?", "？").Replace(":", "："))
                    Else
                        tmp = TextBox3.Text & "\" & (reader(1).ToString & "(" & reader(0).ToString & ")").Replace("?", "？").Replace(":", "：")
                    End If



                    While reader2.Read()

                        If reader2(0).ToString <> "" And Mid(reader2(0).ToString, 1, 1) <> "#" Then
                            If CheckBox1.Checked = True Then
                                tmp2 = chs2py.ToPinyin((reader2(1).ToString & "-" & reader2(0).ToString).Replace("?", "？").Replace(":", "："))
                            Else
                                tmp2 = (reader2(1).ToString & "-" & reader2(0).ToString).Replace("?", "？").Replace(":", "：")
                            End If

                            tmp3 = geteventid(reader2(0).ToString)
                            If tmp3(0) <> "" Then
                                If System.IO.Directory.Exists(tmp) = False Then
                                    System.IO.Directory.CreateDirectory(tmp)
                                End If
                                My.Computer.FileSystem.WriteAllText(tmp & "\" & tmp2 & ".cs", Cscript(reader2(0).ToString, reader2(0).ToString, reader2, reader, tmp3(0), tmp3(1)), False)
                            End If
                            log += tmp & "\" & tmp2 & ".cs" & "," & reader2(0).ToString & vbCrLf

                            'ListBox1.Items.Add(reader(1).ToString & "-" & reader2(1).ToString & "-" & reader2(0).ToString)
                        End If
                    End While
                End If
            End If
        End While
        My.Computer.FileSystem.WriteAllText(".\log.csv", log, False)
        MsgBox("生成成功!")
    End Sub


    Public Function geteventid(ByVal eventid As String)

        Dim cmd As OleDbCommand = New OleDbCommand
        cmd.CommandText = "SELECT script,script2 FROM Script where eventid = " & eventid
        cmd.Connection = AccessConn5
        Dim sr(2) As String
        If cmd.ExecuteScalar <> "" Then
            Dim a As OleDbDataReader = cmd.ExecuteReader
            a.Read()
            sr(0) = a(0).ToString()
            sr(1) = a(1).ToString()
            Return sr
        Else
            Return sr
        End If


    End Function

    Private Sub GroupBox4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox4.Enter

    End Sub
End Class
