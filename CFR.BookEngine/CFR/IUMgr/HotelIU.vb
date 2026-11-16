Imports Microsoft.VisualBasic


Namespace cfr.iu


    Public Class HotelIU



#Region "Category image"
        ''' <summary>
        ''' get image for category 
        ''' </summary>
        ''' <param name="cod_categoria"></param>
        ''' <param name="color">0 for normal background, 1 if white</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getCategoryImghtml(ByVal cod_categoria As cfr.obj.Category.categoryEnum, ByVal color As Integer) As String

            Dim catHtml As String
            If color = 0 Then
                Select Case cod_categoria
                    Case obj.Category.categoryEnum.c5star_Luxe, obj.Category.categoryEnum.c5star
                        catHtml = ("star5r.gif")
                    Case obj.Category.categoryEnum.c4star
                        catHtml = ("star4r.gif")
                    Case obj.Category.categoryEnum.c3star
                        catHtml = ("star3r.gif")
                    Case obj.Category.categoryEnum.c2star
                        catHtml = ("star2r.gif")
                    Case obj.Category.categoryEnum.c1star
                        catHtml = ("star1r.gif")
                    Case obj.Category.categoryEnum.c3_LLAVES
                        catHtml = ("llave3.gif")
                    Case obj.Category.categoryEnum.c2_LLAVES
                        catHtml = ("llave2.gif")
                    Case obj.Category.categoryEnum.c1_LLAVE
                        catHtml = ("llave.gif")
                    Case obj.Category.categoryEnum.cCasas
                        catHtml = ("casrur.gif")
                    Case Else
                        catHtml = ("")
                End Select
            Else
                Select Case cod_categoria
                    Case obj.Category.categoryEnum.c5star_Luxe, obj.Category.categoryEnum.c5star
                        catHtml = ("star5.gif")
                    Case obj.Category.categoryEnum.c4star
                        catHtml = ("star4.gif")
                    Case obj.Category.categoryEnum.c3star
                        catHtml = ("star3.gif")
                    Case obj.Category.categoryEnum.c2star
                        catHtml = ("star2.gif")
                    Case obj.Category.categoryEnum.c1star
                        catHtml = ("star1.gif")
                    Case obj.Category.categoryEnum.c3_LLAVES
                        catHtml = ("llave3.gif")
                    Case obj.Category.categoryEnum.c2_LLAVES
                        catHtml = ("llave2.gif")
                    Case obj.Category.categoryEnum.c1_LLAVE
                        catHtml = ("llave.gif")
                    Case obj.Category.categoryEnum.cCasas
                        catHtml = ("casrur.gif")
                    Case Else
                        catHtml = ("")
                End Select
            End If
            If (catHtml = "") Then
                Return "&nbsp;"
            Else
                Return "<img src=""images/" & catHtml & """>"
            End If
        End Function

#End Region

#Region "Images"

        Public Shared Function getPhotoFirstImgURL(ByVal hotel As cfr.obj.Hotel) As String
            Dim ret As String = ""
            For Each p As cfr.obj.Photo In hotel.photos.Values
                ret = "./fotos/" & p.FotoFileName
                Exit For
            Next

            Return ret
        End Function

        Public Shared Function getOnePhotoImgURL(ByVal hotel As cfr.obj.Hotel) As String
            Dim ret As String = ""
            For Each p As cfr.obj.Photo In hotel.photos.Values
                ret = getPhotoImgURL(p)
                Exit For
            Next

            Return ret
        End Function

        Public Shared Function getPhotoImgURL(ByVal hPhoto As cfr.obj.Photo) As String
            Dim ImgURL As String
            Dim ImgURLg As String
            Dim ret As String = ""

            ImgURL = "fotosp/" & hPhoto.FotoFileName
            ImgURLg = "fotos/" & hPhoto.FotoFileName
            ret = "<IMG SRC='" & ImgURL & "'  ALT=''>"
            ret += "<IMG SRC='" & ImgURLg & "' class='preview' ALT=''>"

            Return ret
        End Function

#End Region

    End Class


End Namespace