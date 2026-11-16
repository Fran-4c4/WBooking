Imports System.Runtime.Serialization

Namespace ind.bk.obj
    <Serializable()>
    <DataContractAttribute(IsReference:=True)>
    Public Class ObjBaseClass
        Private mObjChanged As Boolean = False
        Public Property objChanged As Boolean
            Get
                Return Me.mObjChanged
            End Get
            Set(value As Boolean)
                Me.mObjChanged = value
            End Set
        End Property


        <AttributeUsage(AttributeTargets.Property, Inherited:=True)> _
        Public Class DBColumnNameAttribute
            Inherits Attribute

            Public dbColumn As String = ""
            Public Sub New(dbColumn As String)
                Me.dbColumn = dbColumn
            End Sub

        End Class

    End Class
End Namespace