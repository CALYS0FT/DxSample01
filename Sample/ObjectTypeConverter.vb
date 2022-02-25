Imports System.ComponentModel
Imports System.Globalization
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Xpo.Metadata.Helpers
Imports System.Windows.Forms.Design

Namespace Design

    Public Class ObjectTypeConverter
        Inherits ReferenceConverter

        Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal val As Object) As Object
            If TypeOf val Is String Then
                If CStr(val) = "(none)" Then Return Nothing

                Try
                    Dim dictionary As XPDictionary = (CType(context.Instance, IXPDictionaryProvider)).Dictionary
                    If dictionary IsNot Nothing Then

                        Dim ClassInfo As XPClassInfo = dictionary.QueryClassInfo("", CStr(val))
                        If ClassInfo IsNot Nothing Then
                            Return ClassInfo.ClassType
                        Else
                            Return Nothing
                        End If

                    End If
                Catch e As Exception
                    Dim s As IUIService = CType(context.GetService(GetType(IUIService)), IUIService)
                    If s IsNot Nothing Then s.ShowError(e)
                End Try
            End If

            Return MyBase.ConvertFrom(context, culture, val)
        End Function

        Public Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean
            If destinationType = GetType(String) Then
                Return True
            End If

            Return MyBase.CanConvertTo(context, destinationType)
        End Function

        Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal val As Object, ByVal destType As Type) As Object
            If destType = GetType(String) Then
                If val Is Nothing Then Return "(none)"
                If TypeOf val Is Type Then Return (CType(val, Type)).FullName
            End If

            Return MyBase.ConvertTo(context, culture, val, destType)
        End Function

        Public Sub New()
            MyBase.New(GetType(Type))
        End Sub

        Public Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
            Dim list As New SortedList From {
                {"(none)", Nothing}
            }

            Try
                Dim dictionary As XPDictionary = (CType(context.Instance, IXPDictionaryProvider)).Dictionary

                If dictionary IsNot Nothing Then

                    For Each obj As XPClassInfo In dictionary.Classes
                        If obj.IsVisibleInDesignTime Then list.Add(obj.ClassType.FullName, obj.ClassType)
                    Next

                End If

            Catch e As Exception
                Dim s As IUIService = CType(context.GetService(GetType(IUIService)), IUIService)
                If s IsNot Nothing Then s.ShowError(e)
            End Try

            Return New StandardValuesCollection(list.Values)
        End Function

        Public Overrides Function GetStandardValuesExclusive(ByVal context As ITypeDescriptorContext) As Boolean
            Return False
        End Function

    End Class

End Namespace