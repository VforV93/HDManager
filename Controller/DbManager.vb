Public NotInheritable Class DbManager
    Private Shared ReadOnly _instance As New DbManager 

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Instance() As DbManager
        Get
            Return _instance
        End Get
    End Property
End Class