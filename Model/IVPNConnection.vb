Public Interface IVPNConnection
    'Dim tipoConnessione As String
    ReadOnly Property getName() As String
    Sub connect()
    Sub disconnect()
End Interface
