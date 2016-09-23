Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.IO

Public Class CIncidentDB
    Private Const GoogleAPIKey = "AIzaSyCwccF4RQFbbqQf8aGdRZ8LIZgm5UpcP6g"

    Public IncidentList As Dictionary(Of String, String)
    ' key is for sorting 2016/02/12  843
    ' object is line from Master Table:     "02/24/16 Number 157": { address: "10400 block of W BEL AIR CIRCLE", incCity: "CASA GRANDE AZ 85194", category: "BURGLARY", incCategory: "BURGLARY, RESIDENT, UNLAWF ENT", incTime: "9:41", lat: 32.943, lon: -111.6794 },


    Public Sub New()
        IncidentList = New Dictionary(Of String, String)

    End Sub

    Public Function ParseHTML(category As String, s As String) As Integer
        ' the input string is a bunch of html from the website. Looks like
        '    .....<td width=80>Incident :</td><td width=260> 03/24/16 Number 194</td>....various fields delimited by <td></td>
        ' Break up the string into each Incident substring, then parse the individual incident strings
        Dim sIncidentSubs() As String
        Dim count As Integer = 0

        sIncidentSubs = Regex.Split(s, "Incident :</td>")    's.Split("Incident :</td>")     ' get the individual incident substrings
        Dim dataComp() As String
        Dim i As Integer
        For i = 1 To sIncidentSubs.GetUpperBound(0)       ' skip first string, it is all header stuff
            dataComp = ParseIncidentHTML(category, sIncidentSubs(i))    ' returns Nothing if already in list
            If (Not IsNothing(dataComp)) Then
                IncidentList.Add(dataComp(0), dataComp(1))     ' Adds key, object
                count = count + 1
            End If
        Next
        Return count
    End Function

    Public Sub SortList()
        ' Get list of keys.
        Dim keys As List(Of String) = IncidentList.Keys.ToList
        Dim sortedList As New Dictionary(Of String, String)

        ' Sort the keys.
        keys.Sort()
        keys.Reverse()

        ' Loop over the sorted keys.
        Dim str As String
        For Each str In keys
            sortedList.Add(str, IncidentList.Item(str))
        Next
        IncidentList = sortedList
    End Sub

    Public Function LoadMaster(filePath As String) As Integer
        ' read in the master file, parsing it into the dictionary
        Dim objReader As System.IO.StreamReader = Nothing
        If (Not System.IO.File.Exists(filePath)) Then
            ' create empty file
            Dim objWriter As StreamWriter = New StreamWriter(filePath)
            objWriter.Close()
        End If

        Try
            objReader = New System.IO.StreamReader(filePath)
        Catch ex As Exception

        End Try

        Dim s As String
        Dim count As Integer = 0
        While (Not objReader.EndOfStream)
            s = objReader.ReadLine()
            AddMasterLineToDict(s)
            count = count + 1
        End While
        objReader.Close()
        Return count
    End Function

    Public Sub SaveMaster(filePath As String)
        ' Write out the incidents to the Master
        ' Back up prev master first...
        If (System.IO.File.Exists(filePath)) Then
            Dim bakFilePath As String = filePath.Replace(".txt", ".bak")
            If (System.IO.File.Exists(bakFilePath)) Then
                ' delete prev bak
                System.IO.File.Delete(bakFilePath)
            End If
            bakFilePath = My.Computer.FileSystem.GetName(bakFilePath)
            My.Computer.FileSystem.RenameFile(filePath, bakFilePath)
        End If

        Dim objWriter As System.IO.StreamWriter = New System.IO.StreamWriter(filePath)
        Dim s As String
        For Each rec As KeyValuePair(Of String, String) In IncidentList
            s = rec.Value
            objWriter.WriteLine(s)
        Next
        objWriter.Close()

    End Sub

    Private Sub AddMasterLineToDict(s As String)
        ' the string s is like       
        '      "02/24/16 Number 157": { address: "10400 block of W BEL AIR CIRCLE", incCity: "CASA GRANDE AZ 85194", category: "BURGLARY", incCategory: "BURGLARY, RESIDENT, UNLAWF ENT", incTime: "9:41", lat: 32.943, lon: -111.6794 },
        Dim sortKey As String        ' the sort key
        Dim masterkey As String

        ' get the masterkey, first part of string s
        Dim pieces() As String = s.Split(":")
        masterkey = Trim(pieces(0))      ' remove quotes
        masterkey = masterkey.Replace("""", "")
        sortKey = MakeSortKey(masterkey)
        IncidentList.Add(sortKey, s)
    End Sub

    Private Function ParseIncidentHTML(category As String, s As String) As String()
        ' string is like 
        '<td width=260> 03/24/16 Number 194</td>                                  key
        '<td width=500> BURGLARY, RESIDENT, UNLAWF ENT          </td>             Type of incident
        '<td width=120> 19:41</td>                                                Time
        '</tr></table><table border=0><tr><td width=80>Address  :</td>       
        '<td width=680> 700 block of E GOLDMINE LN              </td>             Address
        '<td width=200> SAN TAN VALLEY AZ   </td>                                 City
        '</tr></table><table border=0><tr><td width=80>Cad Info :</td>
        '<td width=880> </td></tr></table><br><hr><br><table border=0><tr>
        '<td width=80>
        Dim fields() As String
        Dim components(2) As String
        fields = Regex.Split(s, "<td ")
        Dim fKey, fType, fTime, fAddress, fCity As String
        Dim lat As Double = 0
        Dim lon As Double = 0
        fKey = Trim(FieldCleanupHTML(fields(1)))
        fType = FieldCleanupHTML(fields(2))
        fTime = FieldCleanupHTML(fields(3))
        fAddress = FieldCleanupHTML(fields(5))
        fCity = FieldCleanupHTML(fields(6))
        
        components(0) = MakeSortKey(fKey)
        If (IncidentList.ContainsKey(components(0))) Then
            Return Nothing
        End If

        ' only look up Lat/Lon for new records to keep under 2500 google accesses per day
        GetLatLong(fAddress, fCity, lat, lon)
        components(1) = """" & fKey & """ : { category: """ & category & """, incCategory: """ & fType & """, incTime: """ & fTime & """, address: """ & fAddress & """, incCity: """ & fCity & """,  lat: """ & Format(lat, "0.00000") & """, lon: """ & Format(lon, "0.00000") & """}, "
        Return components
    End Function

    Private Function MakeSortKey(inKey As String) As String
        ' fKey is 02/12/16 Number 157   
        ' Convert to 2016/02/12  843 so that sort in reverse order will work
        Dim pieces() As String = inKey.Split(" ")
        Dim dateParts() As String = pieces(0).Split("/")

        ' rebuild key
        Dim inv As Integer = 10000 - CInt(pieces(2))     ' reverse sort now puts smaller incident numbers at top
        Dim newKey As String = "20" & dateParts(2) & "/" & dateParts(0) & "/" & dateParts(1) & " " & inv.ToString()
        Return newKey
    End Function

    Private Function FieldCleanupHTML(s As String) As String
        ' Remove trailing </td>
        ' remove leading stuff like <...>   Find last ">", remove everthing up to / including that point
        ' trim
        Dim f As String = ""
        Dim pos As Integer
        s = s.Replace(vbLf, "")
        s = s.Replace("</td>", "")
        s = s.Replace("<td ", "")
        s = s.Replace("</table>", "")
        s = s.Replace("<table border=0>", "")
        s = s.Replace("<table>", "")
        s = s.Replace("</tr>", "")
        s = s.Replace("<tr>", "")
        pos = s.IndexOf(">")
        While (pos > -1)
            s = s.Remove(0, pos + 1)
            pos = s.IndexOf(">")
        End While

        f = Trim(s)
        Return f
    End Function

    Private Function GetLatLong(addr As String, city As String, ByRef lat As Double, ByRef lon As Double) As Boolean
        Dim result As Boolean = False                   ' did the conversion work?
        Dim u1 As String = "https://maps.googleapis.com/maps/api/geocode/xml?key=" & GoogleAPIKey & "&address="
        'Dim u2 As String = "&sensor=false&key=API-KEY"
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        addr = addr.Replace(",", "%2C")
        addr = addr.Replace(" ", "+")
        addr = addr.Replace("&", "and")
        city = city.Replace("AP JNCTN (CITY)", "APACHE JUNCTION")      ' city does not separate city and state
        city = city.Replace("CASA GR (CITY)", "CASA GRANDE")      ' city does not separate city and state
        city = city.Replace("COOLIDGE (CITY)", "COOLIDGE")      ' city does not separate city and state
        city = city.Replace(" AZ", "%2C AZ")      ' city does not separate city and state
        city = city.Replace(" ", "+")
        u1 = u1 + addr + "," + city     ' + u2


        m_xmld = New XmlDocument()
        Try
            m_xmld.Load(u1)
        Catch ex As Exception
            MsgBox("Load of " & u1 & " failed. Err " & ex.Message)
        End Try

        m_nodelist = m_xmld.SelectNodes("/GeocodeResponse/result/geometry/location")
        If (m_nodelist.Count < 1) Then
            ' retry load
            m_xmld.Load(u1)
            m_nodelist = m_xmld.SelectNodes("/GeocodeResponse/result/geometry/location")
            If (m_nodelist.Count < 1) Then
                'MsgBox("No nodes found for " & u1 & " after 2 tries")
            End If
        End If

        For Each m_node In m_nodelist      ' what if multiple nodes returned?
            lat = m_node.ChildNodes.Item(0).InnerText
            lon = m_node.ChildNodes.Item(1).InnerText
            result = True
        Next

        Return result
    End Function
End Class
