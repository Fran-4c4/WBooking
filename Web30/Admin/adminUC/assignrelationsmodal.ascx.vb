Imports System.Data


Partial Class Admin_adminUC_assignrelationsmodal
    Inherits System.Web.UI.UserControl

    Public Event btnsavetariffrelations_Click(sender As Object, e As EventArgs)

    Public Sub btnsavetariffrelations_Click1(sender As Object, e As EventArgs) Handles btnsavetariffrelations.Click

        RaiseEvent btnsavetariffrelations_Click(sender, e)
    End Sub

    Public Property litresultmsgHandler As String
        Get
            Return Me.litmsgresult.Text
        End Get
        Set(value As String)
            Me.litmsgresult.Text = value
        End Set
    End Property


    Public Property litrelationsHandler As String
        Get
            Return Me.litrelations.Text
        End Get
        Set(value As String)
            Me.litrelations.Text = value
        End Set
    End Property


End Class
