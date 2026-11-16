'//----------------------------------------------------------------------
'//  Copyright (c) 2011 myims.com.au  All Rights Reserved.
'//  This copyright notice MUST stay intact for use.
'//----------------------------------------------------------------------
#Region " File Information "

'=====================================================================
' This class is a generic globals object
'=====================================================================

#Region " Revision History "

'=====================================================================
' Name      Date        Description
'---------------------------------------------------------------------
' AW       12-SEP-2008  Implemented.
'=====================================================================

#End Region

#End Region

Imports System.Net
Imports System.Text
Imports System.Xml
Imports System.IO

Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebControl
Imports System.Data

'###########################################################################################
'###### Do not make changes to this file, it may be replaced in an a version update   ######
'###### Instead you may want to Inherit this class in order to extend and override it ######
'###########################################################################################
Public Class clsGlobals

#Region " Arrays"

    Public Shared BottomMenus() As String = {"Campaigns", "Planning", "Reporting", "Imports", "Maintenance"}

    Public Shared LeftMenus(,) As String = { _
                                    {"My Campaigns", "My Projects", "My Clients", "", "", "", "", "", ""}, _
                                    {"Strategy", "TV Plan", "Campaign Setup", "", "", "", "", "", ""}, _
                                    {"General", "Planning", "Performance", "", "", "", "", "", ""}, _
                                    {"BCC Data Import", "Calls Data Import", "", "", "", "", "", "", ""}, _
                                    {"Users", "TAS Numbers", "Media List L1", "Media List L2", "Media List L3", "Market List", "Delivery Defaults", "Call Tail Defaults", "Directories Product"}}

    Public Shared DGFieldsIns(,) As String = { _
            {"Title", "MediaList_L3", "", "1"}, _
            {"TAS", "TAS", "", ""}, _
            {"Creative", "Caption", "", ""}, _
            {"On-Sale Date", "FromDate", "{0:dd MMM}", ""}, _
            {"Material Deadline", "ToDate", "{0:dd MMM}", ""}, _
            {"000's", "Impressions", "{0:#,##0.00}", ""}, _
            {"Media CPM", "Media_CPM", "{0:C2}", ""}, _
            {"Media Cost", "Media_Cost", "{0:C0}", ""}, _
            {"Prod. CPM", "Prod_CPM", "{0:C2}", ""}, _
            {"Prod. Cost", "Prod_Cost", "{0:C0}", ""}, _
            {"Total Cost", "TotalCost", "{0:C0}", ""}, _
            {"RR", "RR", "{0:N4}", ""}, _
            {"Calls", "Calls", "{0:#,##0}", ""}, _
            {"CPC", "CPC", "{0:C2}", ""}, _
            {"Call Tail", "CallTail", "", ""} _
        }
    Public Shared DGFieldsBTL(,) As String = { _
            {"Title", "MediaList_L3", "", "1"}, _
            {"TAS", "TAS", "", ""}, _
            {"Creative", "Caption", "", ""}, _
            {"Drop Date", "FromDate", "{0:dd MMM}", ""}, _
            {"Material Deadline", "ToDate", "{0:dd MMM}", ""}, _
            {"Delivery Window", "Time_Band", "", ""}, _
            {"000's", "Impressions", "{0:#,##0.00}", ""}, _
            {"Media CPM", "Media_CPM", "{0:C2}", ""}, _
            {"Media Cost", "Media_Cost", "{0:C0}", ""}, _
            {"Prod. CPM", "Prod_CPM", "{0:C2}", ""}, _
            {"Prod. Cost", "Prod_Cost", "{0:C0}", ""}, _
            {"Total Cost", "TotalCost", "{0:C0}", ""}, _
            {"RR", "RR", "{0:N4}", ""}, _
            {"Calls", "Calls", "{0:#,##0}", ""}, _
            {"CPC", "CPC", "{0:C2}", ""}, _
            {"Call Tail", "CallTail", "", ""} _
        }
    Public Shared DGFieldsInfo(,) As String = { _
            {"Title", "MediaList_L3", "", "1"}, _
            {"TAS", "TAS", "", ""}, _
            {"Creative", "Caption", "", ""}, _
            {"On-Air Date", "FromDate", "{0:dd MMM}", ""}, _
            {"Time Band", "Time_Band", "", ""}, _
            {"Comm Length", "Duration", "", ""}, _
            {"000's", "Impressions", "{0:#,##0.00}", ""}, _
            {"Media CPM", "Media_CPM", "{0:C2}", ""}, _
            {"Media Cost", "Media_Cost", "{0:C0}", ""}, _
            {"Prod. Cost", "Prod_Cost", "{0:C0}", ""}, _
            {"Total Cost", "TotalCost", "{0:C0}", ""}, _
            {"RR", "RR", "{0:N4}", ""}, _
            {"Calls", "Calls", "{0:#,##0}", ""}, _
            {"CPC", "CPC", "{0:C2}", ""}, _
            {"Call Tail", "CallTail", "", ""} _
        }
    Public Shared DGFieldsOTP(,) As String = { _
            {"Title", "MediaList_L3", "", "1"}, _
            {"TAS", "TAS", "", ""}, _
            {"Creative", "Caption", "", ""}, _
            {"OnSale Date", "FromDate", "{0:dd MMM}", ""}, _
            {"Material Deadline", "ToDate", "{0:dd MMM}", ""}, _
            {"CMS", "CMS", "", ""}, _
            {"COL", "COL", "", ""}, _
            {"NIMs/Mags Size", "Press_Size", "", ""}, _
            {"SCC Rate", "SCC_Rate", "{0:C2}", ""}, _
            {"Flat Rate", "Flat_Rate", "{0:C2}", ""}, _
            {"Mono/Spot Col/Col", "M_S_C", "", ""}, _
            {"Colour Load", "Col_Load", "{0}", ""}, _
            {"Position", "ItemPosition", "", ""}, _
            {"Pos Load", "Pos_Load", "{0}", ""}, _
            {"Cost", "TotalCost", "{0:C0}", ""}, _
            {"Circ. 000s", "Impressions", "{0:#,##0.00}", ""}, _
            {"RR", "RR", "{0:N4}", ""}, _
            {"Calls", "Calls", "{0:#,##0}", ""}, _
            {"CPC", "CPC", "{0:C2}", ""}, _
            {"Call Tail", "CallTail", "", ""} _
        }
    Public Shared DGFieldsRadio(,) As String = { _
            {"Station", "MediaList_L3", "", "1"}, _
            {"TAS", "TAS", "", ""}, _
            {"Creative", "Caption", "", ""}, _
            {"Date Band", "FromDate", "{0:dd MMM}", ""}, _
            {"Material Deadline", "ToDate", "{0:dd MMM}", ""}, _
            {"Type", "ItemType", "", ""}, _
            {"Duration", "Duration", "", ""}, _
            {"Time Band", "Time_Band", "", ""}, _
            {"Rate/Spot", "R_S", "{0:C0}", ""}, _
            {"Spots", "Spot", "{0:#,##0}", ""}, _
            {"Cost", "TotalCost", "{0:C0}", ""}, _
            {"000's", "Impressions", "{0:#,##0.00}", ""}, _
            {"RR", "RR", "{0:N4}", ""}, _
            {"Calls", "Calls", "{0:#,##0}", ""}, _
            {"CPC", "CPC", "{0:C2}", ""}, _
            {"Call Tail", "CallTail", "", ""} _
        }
    Public Shared DGFieldsOnline(,) As String = { _
            {"Title", "MediaList_L3", "", "1"}, _
            {"TAS", "TAS", "", ""}, _
            {"Creative", "Caption", "", ""}, _
            {"Activity Date", "FromDate", "{0:dd MMM}", ""}, _
            {"Material Deadline", "ToDate", "{0:dd MMM}", ""}, _
            {"Planned Clicks", "Click", "{0:#,##0}", ""}, _
            {"Planned 000's", "Impressions", "{0:#,##0.00}", ""}, _
            {"Planned Acquisitions", "Acquisition", "{0:#,##0}", ""}, _
            {"Media CPClick", "Media_CPClick", "{0:C2}", ""}, _
            {"Media CPM", "Media_CPM", "{0:C2}", ""}, _
            {"Media CPA", "Media_CPA", "{0:C2}", ""}, _
            {"Media Cost", "Media_Cost", "{0:C0}", ""}, _
            {"Flat Prod. Cost", "Prod_Cost", "{0:C0}", ""}, _
            {"Total Cost", "TotalCost", "{0:C0}", ""}, _
            {"RR", "RR", "{0:N4}", ""}, _
            {"Calls", "Calls", "{0:#,##0}", ""}, _
            {"CPC", "CPC", "{0:C2}", ""}, _
            {"Call Tail", "CallTail", "", ""} _
        }
    Public Shared DGFieldsDir(,) As String = { _
            {"Print Category", "MediaList_L3", "", "1"}, _
            {"Product", "MediaList_Product", "", ""}, _
            {"TAS", "TAS", "", ""}, _
            {"From Date", "FromDate", "{0:dd MMM}", ""}, _
            {"To Date", "ToDate", "{0:dd MMM}", ""}, _
            {"000's", "Impressions", "{0:#,##0.00}", ""}, _
            {"AD Size", "AD_Size", "", ""}, _
            {"Rate", "TotalCost", "{0:C0}", ""}, _
            {"RR", "RR", "{0:N4}", ""}, _
            {"Calls", "Calls", "{0:#,##0}", ""}, _
            {"CPC", "CPC", "{0:C2}", ""}, _
            {"Call Tail", "CallTail", "", ""} _
        }
    Public Shared DGFieldsTV(,) As String = { _
            {"Title", "MediaList_L3", "", "1"}, _
            {"Peak Time", "PeakType", "", ""}, _
            {"Split", "Split", "{0:N0}%", ""}, _
            {"000's", "Impressions", "{0:#,##0.00}", ""}, _
            {"Spend", "TotalCost", "{0:C0}", ""}, _
            {"CPM", "Media_CPM", "{0:C2}", ""}, _
            {"RR", "RR", "{0:N4}", ""}, _
            {"Calls", "Calls", "{0:#,##0}", ""}, _
            {"CPC", "CPC", "{0:C2}", ""} _
        }

    Public Shared DGFields() As Object = {Nothing, DGFieldsIns, DGFieldsOTP, DGFieldsBTL, DGFieldsDir, DGFieldsRadio, DGFieldsOnline, DGFieldsTV, DGFieldsInfo}

#End Region

#Region " Enumerations"

    Public Enum enumBottomMenus
        Campaign_List = 0
        Campaign_Plan = 1
        Campaign_Reports = 2
        Data_Upload = 3
        Maintenance = 4
    End Enum

    Public Enum enumLeftMenus
        LeftMenus1 = 0
        LeftMenus2 = 1
        LeftMenus3 = 2
        LeftMenus4 = 3
        LeftMenus5 = 4
        LeftMenus6 = 5
        LeftMenus7 = 6
        LeftMenus8 = 7
        LeftMenus9 = 8
    End Enum

#End Region

#Region " Constants "

    Public Const cPATH_IMAGES As String = "Images\"
    Public Const cDIR_PROFILES As String = "Profiles\"
    Public Const cDIR_ICONS As String = "Icons\"
    Public Const cDIR_BUTTONS As String = "Buttons\"
    Public Const cDIR_TEMP As String = "Temp\"
    Public Const cDIR_ERRORLOG As String = "ErrorLog\"
    Public Const cFILE_ERRORLOG As String = "Errors.txt"


    Public Const cstrSystemName As String = "IMS"
    Public Const cstrStyleSheet As String = "<LINK href=""/Styles/Global.css"" type=""text/css"" rel=""stylesheet"">"

    'Public Const cLogin_SP As String = "sp_LogInTeachers"
    'Public Const cUserDetails_SP As String = "sp_GetUserDetails_byID"
    Public Const cLogin_Page As String = "/_sys/ReLogin.aspx"

#End Region

#Region " Methods "

#Region " Shared "

    Public Shared Function FormatShortDateTime(ByVal dt As DateTime, Optional strFormat As String = "dd MMM yyyy HH:mm") As String
        Dim strReturn As String = dt.ToString(strFormat)
        If Right(strReturn, 6) = " 00:00" Then strReturn = strReturn.Substring(0, strReturn.Length - 6)
        Return strReturn
    End Function

    Public Shared Function WriteLog(ByVal text1 As String, ByVal Text2 As String, ByVal req As System.Web.HttpRequest, Optional ByVal strDir As String = "") As Boolean
        Dim bReturn As Boolean = True
        Dim sw As System.IO.StreamWriter = Nothing
        Try

            'Dim strDir As String = ""
            If strDir = "" Then strDir = System.IO.Directory.GetCurrentDirectory() & "\Logs\"

            If Not System.IO.Directory.Exists(strDir) Then
                System.IO.Directory.CreateDirectory(strDir)
            End If

            Dim myLogName As String = strDir & "Running.log"

            'If System.IO.File.Exists(myLogName) Then
            '    Dim FI As New System.IO.FileInfo(myLogName)

            '    If FI.Length > 100000 Then
            '        Dim sMonth As String = CStr(Today.Month + 1)
            '        If sMonth.Length > 2 Then sMonth = "0" & sMonth
            '        Dim dS As String = Today.Year & sMonth & Today.Day
            '        Dim dT As String = Now.Hour & Now.Minute & Now.Second
            '        System.IO.File.Move(myLogName, strDir & "Log_" & dS & dT & ".bak")
            '    End If
            'End If


            Dim strLang As String = ""
            Dim userLang() As String = req.UserLanguages
            For count As Integer = 0 To userLang.GetUpperBound(0)
                If strLang.Length > 0 Then strLang &= ";"
                strLang &= CStr(userLang(count))
            Next

            Dim sUrlReferrer As String = ""
            If Not req.UrlReferrer Is Nothing Then sUrlReferrer = req.UrlReferrer.ToString
            Dim ip As String = req.ServerVariables("HTTP_X_FORWARDED_FOR")
            'If there is no proxy, get the standard remote address 
            If ip Is Nothing OrElse (ip = "") OrElse (ip.ToLower() = "unknown") Then
                ip = req.ServerVariables("REMOTE_ADDR")
            End If

            sw = New System.IO.StreamWriter(myLogName, True)
            sw.WriteLine(Today.ToShortDateString & " " & Now.ToLongTimeString _
                         & vbTab & text1 _
                         & vbTab & ip _
                         & vbTab & sUrlReferrer _
                         & vbTab & strLang _
                         & vbTab & req.QueryString.ToString _
                         & vbTab & req.UserAgent.ToString _
                        )
            '& vbTab & req.LogonUserIdentity.Name.ToString _
            '& vbTab & req.LogonUserIdentity.AuthenticationType.ToString _
            '& vbTab & req.UserHostAddress.ToString _
            '& vbTab & System.Net.Dns.GetHostEntry(req.UserHostAddress).HostName.ToString _

            '& vbTab & req.LogonUserIdentity.Owner.ToString _
            '& vbTab & req.LogonUserIdentity.Token.ToString _
            '& vbTab & req.LogonUserIdentity.ImpersonationLevel.ToString _
            '& vbTab & req.LogonUserIdentity.IsAnonymous.ToString _
            '& vbTab & req.LogonUserIdentity.IsGuest.ToString _
            '& vbTab & req.LogonUserIdentity.IsSystem.ToString _

            'sw.Close()


        Catch ex As Exception
            bReturn = False
            'MsgBox(ex.Message)

        Finally
            If Not sw Is Nothing Then sw.Close()
            sw = Nothing
        End Try

        Return bReturn

    End Function

    Public Shared Function PrintLog(Optional ByVal strDir As String = "") As String
        Dim sReturn As String = ""
        Dim sr As System.IO.StreamReader = Nothing
        Try
            'Dim strDir As String = ""
            If strDir = "" Then strDir = System.IO.Directory.GetCurrentDirectory() & "\Logs\"

            If Not System.IO.Directory.Exists(strDir) Then
                System.IO.Directory.CreateDirectory(strDir)
            End If

            Dim myLogName As String = strDir & "Running.log"

            If System.IO.File.Exists(myLogName) Then

                sr = New System.IO.StreamReader(myLogName)

                Do While sr.Peek() >= 0
                    sReturn &= sr.ReadLine() & vbNewLine
                Loop
                'sr.Close()

            End If
        Catch ex As Exception
            'MsgBox(ex.Message)

        Finally
            If Not sr Is Nothing Then sr.Close()
            sr = Nothing
        End Try




        Return sReturn
    End Function

    Public Shared Function ByteArrayToString(ByVal bytArray() As Byte) As String
        Dim UTF8 As New UTF8Encoding()
        Return UTF8.GetString(bytArray)
    End Function

    Public Shared Function GetString(ByVal obj As Object) As String
        Dim str As String = ""
        If Not obj Is Nothing AndAlso Not IsDBNull(obj) Then
            str = obj.ToString
        End If
        Return str
    End Function

    Public Shared Function GetCredentials_SMTP() As System.Net.NetworkCredential
        Dim CredentialCache As CredentialCache = New System.Net.CredentialCache

        'Dim sDomain As String = clsGlobals.ReadConfig("SMTP_Domain")
        Dim sUser As String = clsGlobals.ReadConfig("SMTP_User")
        Dim sPassword As String = clsGlobals.ReadConfig("SMTP_Password")
        sPassword = clsEncrypt.GetDecryptString(sPassword)

        Return New System.Net.NetworkCredential(sUser, sPassword)

    End Function

    Public Shared Function GetCredentials_POP3() As System.Net.NetworkCredential
        Dim CredentialCache As CredentialCache = New System.Net.CredentialCache

        Dim sDomain As String = clsGlobals.ReadConfig("POP3ClientAddress")
        Dim sUser As String = clsGlobals.ReadConfig("POP3ClientUserName")
        Dim sPassword As String = clsGlobals.ReadConfig("POP3ClientPassword")
        sPassword = clsEncrypt.GetDecryptString(sPassword)

        Return New System.Net.NetworkCredential(sUser, sPassword, sDomain)

    End Function

    Public Shared Function GetCredentials() As System.Net.NetworkCredential
        Dim CredentialCache As CredentialCache = New System.Net.CredentialCache

        Dim sDomain As String = clsGlobals.ReadConfig("WSUDomain")
        Dim sUser As String = clsGlobals.ReadConfig("WSUUser")
        Dim sPassword As String = clsGlobals.ReadConfig("WSUPassword")
        sPassword = clsEncrypt.GetDecryptString(sPassword)

        Return New System.Net.NetworkCredential(sUser, sPassword, sDomain)

    End Function

    Public Shared Function ConnectionString() As String
        Return "Provider=SQLOLEDB; Data Source=[SERVER NAME]; Initial Catalog=[DB NAME]; user id=[USER NAME]; password=[PASSWORD];"
    End Function


    Public Shared Function GetFilteredTable(ByRef dtOld As DataTable, ByVal sFilter As String) As DataTable
        Dim obDataView As DataView = New DataView(dtOld, sFilter, "", DataViewRowState.CurrentRows)

        If obDataView Is Nothing Then
            Throw New ArgumentNullException("DataView", "Invalid DataView object specified")
        End If

        Dim obNewDt As DataTable = dtOld.Clone()
        Dim idx As Integer = 0
        Dim strColNames(obNewDt.Columns.Count) As String
        For Each col As DataColumn In obNewDt.Columns
            strColNames(idx) = col.ColumnName
            idx = idx + 1
        Next

        Dim viewEnumerator As IEnumerator = obDataView.GetEnumerator
        While viewEnumerator.MoveNext
            Dim drv As DataRowView = CType(viewEnumerator.Current, DataRowView)
            Dim dr As DataRow = obNewDt.NewRow
            Try
                For Each strName As String In strColNames
                    dr(strName) = drv(strName)
                Next
            Catch ex As Exception

            End Try
            obNewDt.Rows.Add(dr)
        End While

        Return obNewDt
    End Function

#Region "Public Shared Function SelectDistinct"
    Public Shared Function SelectDistinct(ByVal SourceTable As DataTable, ByVal ParamArray FieldNames() As String) As DataTable
        Dim lastValues() As Object
        Dim newTable As DataTable

        If FieldNames Is Nothing OrElse FieldNames.Length = 0 Then
            Throw New ArgumentNullException("FieldNames")
        End If

        lastValues = New Object(FieldNames.Length - 1) {}
        newTable = New DataTable

        For Each field As String In FieldNames
            newTable.Columns.Add(field, SourceTable.Columns(field).DataType)
        Next

        For Each Row As DataRow In SourceTable.Select("", String.Join(", ", FieldNames))
            If Not fieldValuesAreEqual(lastValues, Row, FieldNames) Then
                newTable.Rows.Add(createRowClone(Row, newTable.NewRow(), FieldNames))

                setLastValues(lastValues, Row, FieldNames)
            End If
        Next

        Return newTable
    End Function
    Private Shared Function fieldValuesAreEqual(ByVal lastValues() As Object, ByVal currentRow As DataRow, ByVal fieldNames() As String) As Boolean
        Dim areEqual As Boolean = True

        For i As Integer = 0 To fieldNames.Length - 1
            If lastValues(i) Is Nothing OrElse Not lastValues(i).Equals(currentRow(fieldNames(i))) Then
                areEqual = False
                Exit For
            End If
        Next

        Return areEqual
    End Function
    Private Shared Function createRowClone(ByVal sourceRow As DataRow, ByVal newRow As DataRow, ByVal fieldNames() As String) As DataRow
        For Each field As String In fieldNames
            newRow(field) = sourceRow(field)
        Next

        Return newRow
    End Function
    Private Shared Sub setLastValues(ByVal lastValues() As Object, ByVal sourceRow As DataRow, ByVal fieldNames() As String)
        For i As Integer = 0 To fieldNames.Length - 1
            lastValues(i) = sourceRow(fieldNames(i))
        Next
    End Sub
#End Region

    Public Shared Function SQLDATETIME(ByVal td As DateTime, Optional ByVal bAddTime As Boolean = False) As String
        'Return td.ToString("YYYY-MM-DD hh:mm:ss")
        Dim sReturn As String = ""
        sReturn &= td.Year & "." & td.Month & "." & td.Day
        If bAddTime Then sReturn &= " " & td.Hour & ":" & td.Minute & ":" & td.Second
        Return sReturn
    End Function

    Public Shared Function ShowMessage(ByVal msg As String) As String
        Return "<script type=""text/javascript"">alert('" + Replace(Replace(msg, vbNewLine, "\n"), "'", "\'") + "');</script>"
    End Function

    Public Shared Function ReadConfig(ByVal Key As String, Optional ByVal strDefault As String = "") As String
        If System.Configuration.ConfigurationManager.AppSettings.Item(Key) Is Nothing OrElse System.Configuration.ConfigurationManager.AppSettings.Item(Key).Length = 0 Then
            If strDefault.Length > 0 Then
                Return strDefault
            Else
                Return String.Empty
            End If
        Else
            Return System.Configuration.ConfigurationManager.AppSettings.Item(Key)
        End If
    End Function

    Public Shared Function GetDBName() As String
        Return ReadConfig("DBName")
    End Function
    Public Shared Function GetDBServer() As String
        Return ReadConfig("DBServer")
    End Function
    Public Shared Function GetDBUser() As String
        Return ReadConfig("DBUser")
    End Function
    Public Shared Function GetDBPassword() As String
        Return ReadConfig("DBPassword")
    End Function
    Public Shared Function GetApplicDTSPackageNameForExport() As String
        Return ReadConfig("DTSApplicExport")
    End Function
    Public Shared Function GetApplicDTSPackageNameForImport() As String
        Return ReadConfig("DTSApplicImport")
    End Function

    Public Shared Sub SetDataGridCell(ByRef tc As TableCell, ByVal CssClass As String, ByVal Text As String, ByVal Width As Unit)
        If Not CssClass Is Nothing Then
            tc.CssClass = CssClass
        End If

        If Not Text Is Nothing Then
            tc.Text = Text
        End If

        If Not Width.IsEmpty Then
            tc.Width = Width
        End If
    End Sub

    Public Shared Function GetNodeInnerText(ByVal node As XmlNode, ByVal name As String) As String
        Try
            Return node(name).InnerText
        Catch
            Return ""
        End Try
    End Function

    Public Shared Function StripOutHTML(ByVal s As String) As String
        'get the number of occurances of html open and closing tag charactors
        Dim s1() As String = s.Split("<")
        Dim s2() As String = s.Split(">")

        'if the number of occurances is not the same then the string is considered to 
        'contain bad HTML and can not be submitted to the CRM web services
        If s1.GetUpperBound(0) <> s2.GetUpperBound(0) Then
            Throw New Exception("Bad html charactors in string")
        End If

        'loop while html elements are present
        While s.IndexOf("<") <> -1
            Dim p1 As Int16
            Dim p2 As Int16

            'find charactor position of html open and closings tag charactors
            p1 = s.IndexOf("<")
            p2 = s.IndexOf(">") + 1

            'remove html element from string
            s = s.Remove(p1, p2 - p1)
        End While

        'return new string
        Return s
    End Function

    Public Shared Function ConvertBreaks(ByVal s As String) As String
        Return s.Replace("<br>", Environment.NewLine).Replace("<br />", Environment.NewLine)
    End Function

#End Region

#End Region

#Region " Upload Functions "

    Public Shared Sub UploadFile(ByVal btnFileUpload As System.Web.UI.HtmlControls.HtmlInputFile, _
                               ByVal strDestFileName As String, ByRef strErrorMessage As String)

        Dim strFileName As String = ""
        Try
            'Check PostedFile Object is available
            If Not IsNothing(btnFileUpload.PostedFile) Then

                If strDestFileName.Length > 0 Then

                    'Check the file has an extension and whether is doc,xls,pdf,txt - to confirm the uploaded file is a valid file.
                    If Path.HasExtension(btnFileUpload.PostedFile.FileName) And _
                       (LCase(Path.GetExtension(btnFileUpload.PostedFile.FileName).ToString.Trim) = ".doc" Or _
                        LCase(Path.GetExtension(btnFileUpload.PostedFile.FileName).ToString.Trim) = ".xls" Or _
                        LCase(Path.GetExtension(btnFileUpload.PostedFile.FileName).ToString.Trim) = ".pdf" Or _
                        LCase(Path.GetExtension(btnFileUpload.PostedFile.FileName).ToString.Trim) = ".txt") Then

                        'Check the file exists
                        If Not File.Exists(strDestFileName) Then
                            btnFileUpload.PostedFile.SaveAs(strDestFileName)
                        Else
                            strErrorMessage = "The File ( " & strDestFileName & " ) you are trying to upload already exists. "
                            Exit Try
                        End If
                    Else
                        strErrorMessage = "The file you are trying to upload is not a report file."
                        Exit Try

                    End If  'Checking whether file has an extension and whether is doc,xls,pdf,txt - to confirm the uploaded file is a valid file.
                End If  'strFileName.Length > 0 
            End If      'Not IsNothing(btnFileUpload.PostedFile) 

        Catch Ex As Exception
            strErrorMessage = strErrorMessage & " Error inside Upload click event - " & Ex.Message & " - " & Ex.Source
        End Try

    End Sub

#End Region

End Class


