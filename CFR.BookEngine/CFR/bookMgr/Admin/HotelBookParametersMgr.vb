Imports Microsoft.VisualBasic
Imports System.Data
Imports cfr.tool



Namespace cfr.mgr


    Public Class HotelBookParameters

       
        Public Shared Function GetEntity(ByVal id As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As cfr.obj.HotelBookParameters

            Dim hotparammgr As New ind.bk.obj.R_Res_Hot_Param_Manager
            Dim dt As DataTable = hotparammgr.getDataTableByCod_hotel(id)

            Dim obj As cfr.obj.HotelBookParameters = Nothing

            If dt.Rows.Count > 0 Then
                obj = New cfr.obj.HotelBookParameters

                obj.FillByRow(dt.Rows(0))
                
                obj.ValidateRegimenCount()
                
            End If
            Return obj
        End Function
    End Class
End Namespace
