Imports cfr

Partial Class UC_B_Paid
    Inherits System.Web.UI.UserControl
    Public bookData As cfr.BookRes
    Public bookOperator As OperatorObj

#Region "Internal control accesor "

    ''' <summary>
    ''' return the selected type by user.
    ''' </summary>
    ''' <param name="paidType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
#End Region
    ''' <summary>
    ''' return the string with information for transfer in book process.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function writeTransferData() As String
        Dim bp As cfr.obj.HotelBookParameters = bookData.hotel.BookParam
        Dim s As String = ""
        s += "<DIV id='fpccc' class='paidContainer cashtype clearfix alert alert-warning alert-dismissible' role='alert' {0} >"
        s += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>"
        
        If Me.bookOperator.forma_pago = CFRConst.formaPago.Transfer Then
            s = String.Format(s, " style='display:block;' ")
        Else
            s = String.Format(s, " style='display:none;' ")
        End If

        s += cfr.Language.GetCaption(130)
        s += "</div>"
        Return s
    End Function


    ''' <summary>
    ''' Configure IU page based on the type of paids admited
    ''' </summary>
    ''' <param name="bookData"></param>
    ''' <remarks></remarks>
    Public Sub configureUI(ByVal bookData As BookRes)
        Me.bookData = bookData
        Me.bookOperator = Me.bookData.bookOperator
        If Me.bookData.bookOperator.forma_pago = cfr.CFRConst.formaPago.Transfer Then
            Me.lblTransferOnly.Text = cfr.Language.getCaption(129)
        End If


    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.bookData = cfr.book.UserHelper.getBookSession(Me.Context)
        If Not Me.bookData Is Nothing Then
            Me.bookOperator = Me.bookData.bookOperator
        End If

        If Not Page.IsPostBack Then
            Me.configCaptions()
        End If

    End Sub


    Private Sub configCaptions()
        Me.lblPaidTypes.Text = cfr.Language.getCaption(127)
    End Sub
End Class
