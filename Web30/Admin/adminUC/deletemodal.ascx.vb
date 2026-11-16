
Partial Class Admin_adminUC_deletemodal
    Inherits System.Web.UI.UserControl


    Public Event btnDelete_Click(sender As Object, e As EventArgs)

    Public Sub btndeletemodal_Click1(sender As Object, e As EventArgs) Handles btndeletemodal.Click

        RaiseEvent btnDelete_Click(sender, e)
    End Sub

    Public Property modalTitle As String

        Get
            Return Me.litmodaltitle.Text
        End Get
        Set(value As String)
            Me.litmodaltitle.Text = value
        End Set
    End Property

    Public Property modalmessagebody As String

        Get
            Return Me.litMessage.Text
        End Get
        Set(value As String)
            Me.litMessage.Text = value
        End Set
    End Property

    Public ReadOnly Property modalitemdeletecode As HiddenField
        Get
            Return Me.itemdeletecode
        End Get
    End Property

    Public Property modalbuttonprimaryActionCaption As String

        Get
            Return Me.litbtnprimarycaption.Text
        End Get
        Set(value As String)
            Me.litbtnprimarycaption.Text = value
        End Set
    End Property

    Public ReadOnly Property modalbuttonprimary As LinkButton
        Get
            Return Me.btndeletemodal
        End Get
    End Property




End Class
