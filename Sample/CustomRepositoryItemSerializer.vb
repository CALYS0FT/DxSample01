Imports System.CodeDom
Imports System.ComponentModel
Imports System.ComponentModel.Design.Serialization

Namespace Design

    Public Class CustomRepositoryItemSerializer
        Inherits CodeDomSerializer

        Private Shared Sub CheckType(ByVal manager As IDesignerSerializationManager, ByVal expr As CodeTypeOfExpression)
            Dim str As String = expr.Type.BaseType
            Dim type As Type = manager.[GetType](str)

            If type Is Nothing Then
                manager.ReportError(New System.Runtime.Serialization.SerializationException(String.Format("Could not find type '{0}'. Please make sure that the assembly that contains this type is referenced. If this type is a part of your development project, make sure that the project has been successfully built.", str)))
            End If

        End Sub

        Public Overrides Function Deserialize(ByVal manager As IDesignerSerializationManager, ByVal codeDomObject As Object) As Object

            Dim baseSerializer As CodeDomSerializer = CType(manager.GetSerializer(GetType(Component), GetType(CodeDomSerializer)), CodeDomSerializer)

            Dim o As Object = baseSerializer.Deserialize(manager, codeDomObject)

            If TypeOf codeDomObject Is CodeStatementCollection Then

                If TypeOf o Is Controls.CustomTextEdit Then

                    For Each cs As CodeStatement In CType(codeDomObject, CodeStatementCollection)

                        If TypeOf cs Is CodeAssignStatement Then

                            If TypeOf CType(cs, CodeAssignStatement).Left Is CodePropertyReferenceExpression AndAlso CType(CType(cs, CodeAssignStatement).Left, CodePropertyReferenceExpression).PropertyName = NameOf(Controls.CustomTextEdit.ObjectType) Then

                                CheckType(manager, CType(CType(cs, CodeAssignStatement).Right, CodeTypeOfExpression))

                            End If

                        End If

                    Next

                End If

            End If

            Return o
        End Function

        Public Overrides Function Serialize(ByVal manager As IDesignerSerializationManager, ByVal value As Object) As Object
            Dim baseSerializer As CodeDomSerializer = CType(manager.GetSerializer(GetType(Component), GetType(CodeDomSerializer)), CodeDomSerializer)
            Return baseSerializer.Serialize(manager, value)
        End Function

    End Class

End Namespace


