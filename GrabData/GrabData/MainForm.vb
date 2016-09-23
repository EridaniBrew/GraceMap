Imports System.Net
Imports System.IO

Public Class MainForm
    '?ctgy=CIVIL+COMPLAINT&city=ANY+CITY&query=&street=&Submit=Search+For+Incidents
    '    ANY+CITY
    
    Private IncidentType() As String = {"BURGL-NO+FORCED+ENTRY",
                                        "BURGLARY",
                                        "SUSPICIOUS+ACTIVITY",
                                        "THEFT%2C+PROPERTY",
                                        "VEHICLE+THEFT"
                                        }
    'Private IncidentType() As String = {"BURGL-NO+FORCED+ENTRY",
    '                                    "BURGLARY"
    '                                    }
    ' "ACCIDENT",
    '"ALARMS",
    '"ANIMAL+CONTROL",
    '"ASSAULT",
    '"ASSIST%3AMOTORIST+OR+PUBLIC",
    '"CHILD+NEGLECT",
    '"CIVIL+COMPLAINT",
    '"CONSERVATION-ENVIROMENT",
    '"CUSTODIAL+INTERFERENCE",
    '"DAMAGE+PROP",
    '"DEATH+INVEST",
    '"DISORDERLY+CONDUCT",
    '"DISTURBANCE",
    '"DRIVING+SUSP%2FREVOKED+DRIV+LIC",
    '"DRUG+OFFENSES",
    '"EXPLOSIVES+-+USING",
    '"FIREARMS+ILLEGAL+DISCHARGE",
    '"FIRES",
    '"FIREWORKS+ILLEGAL+USE",
    '"FOUND+PROPERTY",
    '"FRAUD",
    '"HARRASSMENT",
    '"HOME+ALONE",
    '"HOMICIDE+-+GUN",
    '"INCORRIGIBLE+JUVENILE",
    '"LOST+MISSING+PERSON",
    '"MEDICAL+EMERGENCY",
    '"MISCELLANEOUS",
    '"OTHER+TRAFFIC+INCIDENTS",
    '"PERS+INJURY+COLL+W+MOTOR+VEH",
    '"PROP+DAMAGE+MISCELLANEOUS",
    '"RECOVERED+VEHICLE-STOLEN",
    '"REPOSSESSION+OF+VEHICLE",
    '"RUNAWAY+JUVENILE",
    '"SEARCH+WARRANT",
    '"SUSPICIOUS+PERSON",
    '"THREAT%2FINTIM%2FDOMESTIC+VIOLENCE",
    '"TRAFFIC+OFFENSE",
    '"TRESPASSING",
    '"VEHICLE+IMPOUND",
    '"VIOLATION+OF+COURT+ORDER",
    '"WARRANT"
    '}
    ' used to populate the selection list
    Private IncidentLabel() As String = {"BURGL-NO FORCED ENTRY",
                                         "BURGLARY",
                                         "SUSPICIOUS ACTIVITY",
                                         "THEFT, PROPERTY",
                                         "VEHICLE THEFT"
                                        }
    'Private IncidentLabel() As String = {"BURGL-NO FORCED ENTRY",
    '                                     "BURGLARY"
    '                                    }
    '"ACCIDENT",
    '"ALARMS",
    '"ANIMAL CONTROL",
    '"DAMAGE PROP",
    '"DEATH INVEST",
    '"DISORDERLY CONDUCT",
    '"DISTURBANCE",
    '"DRIVING SUSP/REVOKED DRIV LIC",
    '"DRUG OFFENSES",
    '"EXPLOSIVES - USING",
    '"FIREARMS ILLEGAL DISCHARGE",
    '"FIRES",
    '"FIREWORKS ILLEGAL USE",
    '"FOUND PROPERTY",
    '"FRAUD",
    '"HARRASSMENT",
    '"HOME ALONE",
    '"HOMICIDE - GUN",
    '"INCORRIGIBLE JUVENILE",
    '"LOST MISSING PERSON",
    '"MEDICAL EMERGENCY",
    '"MISCELLANEOUS",
    '"OTHER TRAFFIC INCIDENTS",
    '"PERS INJURY COLL W MOTOR VEH",
    '"PROP DAMAGE MISCELLANEOUS",
    '"RECOVERED VEHICLE-STOLEN",
    '"REPOSSESSION OF VEHICLE",
    '"RUNAWAY JUVENILE",
    '"SEARCH WARRANT",
    '"SUSPICIOUS PERSON",
    '"THREAT/INTIM/DOMESTIC VIOLENCE",
    '"TRAFFIC OFFENSE",
    '"TRESPASSING",
    '"VEHICLE IMPOUND",
    '"VIOLATION OF COURT ORDER",
    '"WARRANT"}

    Dim theIncidentList As CIncidentDB


    Private Sub ReadWebData(incident As String)
        ' Don't forget the Imports System.IO line ;-}
        '
        Dim Str As System.IO.Stream
        Dim srRead As System.IO.StreamReader
        Dim s As String
        Dim finishedData As Boolean = False
        Dim startNum As Integer = 0
        Dim website As String
        Dim dataS As String = ""
        Dim count As Integer = 0

        LogMsg("Downloading " & incident)
        While (Not finishedData)
            Try
                ' make a Web request    AIzaSyCwccF4RQFbbqQf8aGdRZ8LIZgm5UpcP6g
                website = "http://pcsocop.com/mem_inc/get.php?s=" & startNum & "&ctgy=" & incident
                ' LogMsg("Accessing " & website)
                Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(website)
                req.Credentials = New NetworkCredential("cop235", "gbCOP346")

                Dim resp As System.Net.WebResponse = req.GetResponse
                Str = resp.GetResponseStream
                srRead = New System.IO.StreamReader(Str)
                ' read all the text
                s = srRead.ReadToEnd
                srRead.Close()
                Str.Close()
                ' If no data is found, Sorry, your search is found and we are done
                ' if data is found, we are done when there is no Next 10 button
                If (s.Contains("Sorry, your search")) Then
                    finishedData = True
                Else
                    count = count + theIncidentList.ParseHTML(incident, s)
                    txtLog.AppendText(".")
                    If (Not s.Contains("Next 10")) Then
                        finishedData = True
                    Else
                        startNum = startNum + 10
                    End If
                End If

            Catch ex As Exception
                s = "Unable to download content"

            End Try

        End While
        LogMsg("Added " & count & " incidents")
    End Sub

    Private Sub btnStartCollection_Click(sender As Object, e As EventArgs) Handles btnStartCollection.Click
        
        theIncidentList = New CIncidentDB
        Try
            Dim count As Integer = theIncidentList.LoadMaster(My.Settings.DataFolder & "/MasterIncidents.txt")
            LogMsg("Loaded " & count.ToString() & " entries from MasterIncidents.txt")
        Catch ex As Exception
            MsgBox("Failed to load Master Database " & My.Settings.DataFolder & "/MasterIncidents.txt" & vbCrLf & ex.Message)
            Exit Sub
        End Try
        
        For Each sel In IncidentLabel
            ReadWebData(sel)
        Next

        ' Saving new Master file
        Try
            theIncidentList.SaveMaster(My.Settings.DataFolder & "/MasterIncidents.txt")
            LogMsg("Master File Saved")
        Catch ex As Exception
            MsgBox("Failed to save master file. " & vbCrLf & ex.Message)
        End Try

        Try
            BuildHTMLpage(My.Settings.DataFolder & "\GraceMapPart1.html",
                          My.Settings.DataFolder & "\GraceMapPart2.html",
                          My.Settings.DataFolder & "\MasterIncidents.txt",
                          My.Settings.DataFolder & "\GraceMap.html")
            LogMsg("GraceMap.html rebuilt")
        Catch ex As Exception
            MsgBox("Failed to create GraceMap. " & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub btnSelectFolder_Click(sender As Object, e As EventArgs) Handles btnSelectFolder.Click
        ' Select the target directory to hold the files.
        ' This folder should contain the html start and end files.
        FolderBrowserDialog1.SelectedPath = My.Settings.DataFolder
        If (FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            txtDataFolder.Text = FolderBrowserDialog1.SelectedPath
            txtDataFolder.SelectionStart = txtDataFolder.Text.Length
            My.Settings.DataFolder = FolderBrowserDialog1.SelectedPath
            My.Settings.Save()
        End If
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = Me.Text & FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion
        ReadSettings()
        txtDataFolder.Text = My.Settings.DataFolder
        txtDataFolder.SelectionStart = txtDataFolder.Text.Length

        ' Fill in Incident types
        Dim i As Integer
        For i = 0 To IncidentLabel.GetUpperBound(0)
            lstIncidentType.Items.Add(IncidentLabel(i))
        Next i
    End Sub

    Private Sub ReadSettings()
        FolderBrowserDialog1.SelectedPath = My.Settings.DataFolder
    End Sub

    Private Sub LogMsg(s As String)
        txtLog.AppendText(s & vbCrLf)
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        ' Test various routines
        BuildHTMLpage("C:\Users\erida\Dropbox\BrewSky\Programs\GraceMap\CreateMap\CreateMap\GraceMapPart1.html",
                      "C:\Users\erida\Dropbox\BrewSky\Programs\GraceMap\CreateMap\CreateMap\GraceMapPart2.html",
                      My.Settings.DataFolder & "\MasterIncidents.txt",
                      My.Settings.DataFolder & "\GraceMap.html")

    End Sub

    Private Function ReadDataFile(filePath As String) As String
        Dim objRead As New System.IO.StreamReader(filePath)

        Dim s As String = objRead.ReadToEnd()
        objRead.Close()
        Return s
    End Function

    Private Sub BuildHTMLpage(part1 As String, part2 As String, database As String, htmlFile As String)
        ' Combine Part1, database, and Part2 to make the final outut html file
        ' All files are pathnames
        Dim p1, p2, db As String
        p1 = ReadDataFile(part1)
        p2 = ReadDataFile(part2)
        db = ReadDataFile(database)

        Dim objWriter As New StreamWriter(htmlFile)
        objWriter.WriteLine(p1)
        objWriter.WriteLine(db)
        objWriter.WriteLine(p2)
        objWriter.Close()
    End Sub


End Class
