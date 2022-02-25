Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Xpo.Metadata.Helpers
Imports DevExpress.XtraEditors.Design

Namespace Design

    Public Class RepositoryItemCustomTextEditDesigner
        Inherits BaseRepositoryItemDesigner

        'Protected Overrides Sub PostFilterProperties(properties As IDictionary)
        '    MyBase.PostFilterProperties(properties)
        '    'DXPropertyDescriptor.ConvertDescriptors(properties)
        'End Sub

    End Class

    Public Class CustomTextEditDesigner
        Inherits TextEditDesigner

        Protected Overrides Sub RegisterActionLists(list As DesignerActionListCollection)
            MyBase.RegisterActionLists(list)
            list.Add(New BindingProviderActionList(Me))
        End Sub

    End Class

    Public Class BindingProviderActionList
        Inherits DesignerActionList
        Implements IXPDictionaryProvider

        ReadOnly Property Designer As CustomTextEditDesigner

        Public Sub New(designer As CustomTextEditDesigner)
            MyBase.New(designer.Component)

        End Sub

        Public Overrides Function GetSortedActionItems() As DesignerActionItemCollection

            Dim res As New DesignerActionItemCollection From {
                New DesignerActionPropertyItem(NameOf(Controls.CustomTextEdit.ObjectType), "Type d'objet")
                }

            Return res
        End Function

        Public ReadOnly Property CustomTextEdit As Controls.CustomTextEdit
            Get
                Return TryCast(Me.Component, Controls.CustomTextEdit)
            End Get
        End Property

        <TypeConverter(GetType(Design.ObjectTypeConverter))>
        Public Property ObjectType As Type
            Get
                Return CustomTextEdit.ObjectType
            End Get
            Set(ByVal value As Type)
                Me.CustomTextEdit.ObjectType = value
            End Set
        End Property

        Public ReadOnly Property Dictionary As XPDictionary Implements IXPDictionaryProvider.Dictionary
            Get
                Return CustomTextEdit.Dictionary
            End Get
        End Property

    End Class

End Namespace