Imports System.ComponentModel
Imports System.ComponentModel.Design.Serialization
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Xpo.Metadata.Helpers
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo

Namespace Controls

    <Designer(GetType(Design.RepositoryItemCustomTextEditDesigner), GetType(ComponentModel.Design.IDesigner))>
    <UserRepositoryItem(NameOf(RepositoryItemCustomTextEdit.RegisterCustomTextEdit))>
    Public Class RepositoryItemCustomTextEdit
        Inherits RepositoryItemTextEdit
        Implements IXPDictionaryProvider

        Shared Sub New()
            RegisterCustomTextEdit()
        End Sub

        Public Const CustomEditName As String = NameOf(CustomTextEdit)

        Public Sub New()

        End Sub

        Public Overrides ReadOnly Property EditorTypeName As String
            Get
                Return CustomEditName
            End Get
        End Property

        Private designDictionary As DesignTimeReflection
        Friend ReadOnly Property Dictionary As XPDictionary Implements IXPDictionaryProvider.Dictionary
            Get
                If designDictionary Is Nothing Then
                    If Not Me.IsDesignMode Then Throw New InvalidOperationException()
                    If Me.OwnerEdit IsNot Nothing AndAlso Me.OwnerEdit.Site IsNot Nothing Then
                        designDictionary = New DesignTimeReflection(Me.OwnerEdit.Site)
                    ElseIf Me.Site IsNot Nothing Then
                        designDictionary = New DesignTimeReflection(Me.Site)
                    End If
                End If
                Return designDictionary
            End Get
        End Property

        Private fObjectType As Type
        <Browsable(False)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Description("Obtenir ou définir le type utilisé pour la liaison de données")>
        <DefaultValue(TryCast(Nothing, Object))>
        <TypeConverter(GetType(Design.ObjectTypeConverter))>
        Public Property ObjectType As Type
            Get
                Return fObjectType
            End Get
            Set(ByVal value As Type)
                If Not Me.IsLoading AndAlso Not Me.IsDesignMode Then Throw New InvalidOperationException(String.Format("Cannot change property '{0}' when the {1} is not in design.", NameOf(ObjectType), Me.GetType.Name))
                fObjectType = value
                OnPropertiesChanged()
            End Set
        End Property

        Public Shared Sub RegisterCustomTextEdit()
            EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(CustomEditName, GetType(CustomTextEdit), GetType(RepositoryItemCustomTextEdit), GetType(CustomTextEditViewInfo), New CustomTextEditPainter(), True, EditImageIndexes.TextEdit))
        End Sub

        Public Overrides Sub Assign(item As RepositoryItem)
            BeginUpdate()
            Try
                MyBase.Assign(item)
                Dim source As RepositoryItemCustomTextEdit = TryCast(item, RepositoryItemCustomTextEdit)
                If source IsNot Nothing Then
                    Me.ObjectType = source.ObjectType
                End If
                '
            Finally
                EndUpdate()
            End Try

        End Sub
    End Class

    <ToolboxItem(True)>
    <Designer(GetType(Design.CustomTextEditDesigner))>
    <DesignerSerializer(GetType(Design.CustomRepositoryItemSerializer), GetType(CodeDomSerializer))>
    Public Class CustomTextEdit
        Inherits TextEdit
        Implements IXPDictionaryProvider

        Shared Sub New()
            RepositoryItemCustomTextEdit.RegisterCustomTextEdit()
        End Sub

        Public Sub New()

        End Sub

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public Shadows ReadOnly Property Properties As RepositoryItemCustomTextEdit
            Get
                Return TryCast(MyBase.Properties, RepositoryItemCustomTextEdit)
            End Get
        End Property

        <DefaultValue(TryCast(Nothing, String))>
        <Browsable(False)>
        <Description("Obtenir ou définir le type utilisé pour la liaison de données")>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        <RefreshProperties(RefreshProperties.All)>
        <NotifyParentProperty(True)>
        <TypeConverter(GetType(Design.ObjectTypeConverter))>
        Public Property ObjectType As Type
            Get
                Return Me.Properties.ObjectType
            End Get
            Set(ByVal value As Type)
                Me.Properties.ObjectType = value
            End Set
        End Property

        Friend ReadOnly Property Dictionary As XPDictionary Implements IXPDictionaryProvider.Dictionary
            Get
                Return Me.Properties.Dictionary
            End Get
        End Property

        Public Overrides ReadOnly Property EditorTypeName As String
            Get
                Return RepositoryItemCustomTextEdit.CustomEditName
            End Get
        End Property

    End Class

    Public Class CustomTextEditViewInfo
        Inherits TextEditViewInfo

        Public Sub New(item As RepositoryItem)
            MyBase.New(item)

        End Sub
    End Class

    Public Class CustomTextEditPainter
        Inherits TextEditPainter

        Public Sub New()
            MyBase.New()

        End Sub

    End Class

End Namespace
