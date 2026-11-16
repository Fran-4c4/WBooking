Imports Microsoft.VisualBasic
Imports System.Data

Namespace ind.bk.admin
    Public Class AdminTariffManager
        Public Shared Function getDvPricesByRoomByTariff(cod_tarifa As Integer) As DataView

            Dim tableRes As New ind.bk.obj.R_tarifas_Manager

            Dim dv As DataView = tableRes.getDT_PricesByRoomByTariff(cod_tarifa).DefaultView
            dv.Sort = ind.bk.obj.T_Hoteles_Habitaciones.DBCol.habitacionhotel
            Return dv

        End Function

        ''' <summary>
        ''' based on database category returns the ui string
        ''' </summary>
        ''' <param name="tariffCategoryVal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getTariffCategoryUI(tariffCategoryVal As cfr.CFRConst.ClientTypeEnum) As String
            Dim strCategoria As String = ""

            If tariffCategoryVal = CFRConst.ClientTypeEnum.NotClasified Then
                strCategoria = "Todos los clientes"
            ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.HotelAgency Then
                strCategoria = "Agencias"
            ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.CRS Then
                strCategoria = "CRS"
            ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.TourOperators Then
                strCategoria = "TourOperadores"
            ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.Company Then
                strCategoria = "Empresas"
            ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.PrivateClient Then
                strCategoria = "Cliente Particular"
            ElseIf tariffCategoryVal = CFRConst.ClientTypeEnum.WebSite Then
                strCategoria = "Portal Web(Websites)"
            End If

            Return strCategoria
        End Function

        Public Shared Function createCheckbox(chkID As String, checked As Boolean, description As String) As String
            Dim s As New StringBuilder()

            If checked = True Then
                s.AppendFormat("<div class='checkbox'><label for='relation_{0}' class='b-relation-selected'><input id='relation_{0}' type='checkbox' name='relation_{0}' checked class='chk-rel'>{1}</label></div>", chkID, description)
            Else
                s.AppendFormat("<div class='checkbox'><label for='relation_{0}'><input id='relation_{0}' type='checkbox' name='relation_{0}' class='chk-rel b-relation'>{1}</label></div>", chkID, description)
            End If

            Return s.ToString
        End Function

        Public Shared Sub setDiscount(priceObj As ind.bk.obj.R_Tarifa_habitacion, discountUI As String, ByRef DescAdultosExtra As Double, ByRef camaExtra As Double)
            If discountUI Is Nothing Then
                discountUI = 0
            Else
                If discountUI.Trim = "" Then
                    discountUI = 0
                End If
            End If

            If discountUI.EndsWith("%") Then
                Dim newValue As Double = discountUI.Replace("%", "")
                If newValue <> DescAdultosExtra Then
                    priceObj.objChanged = True
                End If
                DescAdultosExtra = newValue
                camaExtra = 0
            Else
                Dim newValue As Double = discountUI
                If newValue <> camaExtra Then
                    priceObj.objChanged = True
                End If
                DescAdultosExtra = 0
                camaExtra = newValue
            End If
        End Sub


#Region "Relations matrix"
        Public Shared Function getdtTariffOperatorMatrix(cod_hotel As Integer, Optional activeOnly As Integer = -1) As DataTable
            Dim sql As String = "Select R_tarifas.cod_tarifa, R_tarifas.tarifa, R_operadores.cod_operador, R_operadores.Operador, R_operadores.FreeSale, R_operadores.Active " _
                    & " FROM         R_tarifas INNER JOIN " _
          & " R_Tarifa_operador On R_tarifas.cod_tarifa = R_Tarifa_operador.cod_tarifa INNER Join " _
          & " R_operadores On R_Tarifa_operador.cod_operador = R_operadores.cod_operador " _
          & " WHERE     (R_tarifas.cod_hotel = {0}) "
            If activeOnly <> -1 Then
                sql += String.Format(" And (R_operadores.Active = {0}) ", activeOnly)
            End If
            sql += "  order by tarifa ,cod_operador "
            sql = String.Format(sql, cod_hotel)

            Return ind.bk.DBMgr.getDT(sql)
        End Function
#End Region


    End Class
End Namespace

