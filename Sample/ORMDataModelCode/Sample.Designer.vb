'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Reflection
Namespace ORMDataModel

    Partial Public Class Sample
        Inherits XPObject
        Dim fName As String
        Public Property Name() As String
            Get
                Return fName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Name), fName, value)
            End Set
        End Property
        Dim fCaption As String
        Public Property Caption() As String
            Get
                Return fCaption
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Caption), fCaption, value)
            End Set
        End Property
    End Class

End Namespace
