Imports System.IO
Public Class Aggiorna
    Dim Path As String = Application.StartupPath
    Dim WithEvents up As New Net.WebClient
    Dim TipoUpd As String

    Private Sub up_DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles up.DownloadFileCompleted
        Try
            If e.Error Is Nothing = True Then
                Application.DoEvents()
                LblInCorso.Text = "Database Aggiornato"
            Else
                MsgBox(e.Error.Message & vbCrLf & "Database non aggiornato. Controllare la connessione e riprovare", MsgBoxStyle.Exclamation, "Errore di connessione")
            End If
        Catch ex As Exception
            MsgBox(e.Error.Message)            
        End Try

        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub up_DownloadProgressChanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles up.DownloadProgressChanged
        Application.DoEvents()
        Progr.Maximum = e.TotalBytesToReceive
        Progr.Value = e.BytesReceived
        'LblProgr.Text = "Scaricati " & e.BytesReceived & " di " & e.TotalBytesToReceive
    End Sub

    Private Sub Aggiorna_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If TipoUpd = "AggiornaVPN" Then
            Exit Sub
        End If

        LblInCorso.Text = "Copia Database in corso"

        Try
            Dim Cetiri As New Uri("\\noemanas\CustomerService\GestioneHD\database\DatiGestioneHD.mdb")
            'TODO: COPIO SOLO SE SONO DIVERSI (TOLTA LA PARTE DLE COMPARAFILE PERCHE' CI METTE UNA VITA...)           
            Try
                up.DownloadFileAsync(Cetiri, Application.StartupPath & "\DatiGestioneHD.mdb")
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & "Database non aggiornato. Controllare la connessione e riprovare", MsgBoxStyle.Exclamation, "Errore di connessione")
            Me.Close()
        End Try
    End Sub
    Sub AggiornaVPN()
        TipoUpd = "AggiornaVPN"
        Me.Show()

        Try            
            'AGGIORNO LE MICROSOFT VPN:        
            LblInCorso.Text = "Copia Microsoft VPN in corso"
            Application.DoEvents()
            Dim FileListMVPN As New ArrayList(Directory.GetFiles("\\noemanas\CustomerService\GestioneHD\HdManager\vpn_hdmanager\Mvpn\"))
            Progr.Maximum = FileListMVPN.Count
            Progr.Value = 0
            For b = 0 To FileListMVPN.Count - 1
                Dim RasFile As New FileInfo(FileListMVPN.Item(b).ToString)
                RasFile.CopyTo(Application.StartupPath & "\vpn_hdmanager\Mvpn\" & RasFile.Name, True)
                Progr.Value += 1
            Next
            'AGGIORNO LE CISCO:        
            LblInCorso.Text = "Copia VPN Cisco in corso"
            Application.DoEvents()
            Dim FileList As New ArrayList(Directory.GetFiles("\\noemanas\CustomerService\GestioneHD\HdManager\vpn_hdmanager\cisco\"))
            Progr.Maximum = FileList.Count
            Progr.Value = 0
            For b = 0 To FileList.Count - 1
                Dim PcfFile As New FileInfo(FileList.Item(b).ToString)
                'If ComparaFile(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco Systems\VPN Client\profiles\" & PcfFile.Name, PcfFile.FullName) = False Then
                PcfFile.CopyTo(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco Systems\VPN Client\profiles\" & PcfFile.Name, True)
                'End If
                Progr.Value += 1
            Next
            ''AGGIORNO LE OPEN:                
            LblInCorso.Text = "Copia OPEN VPN in corso"
            Application.DoEvents()
            Dim DirList As New ArrayList(Directory.GetDirectories("\\noemanas\CustomerService\GestioneHD\HdManager\vpn_hdmanager\openvpn\"))
            Progr.Maximum = DirList.Count
            Progr.Value = 0
            For b = 0 To DirList.Count - 1
                Dim OVPNDir As New DirectoryInfo(DirList.Item(b).ToString)
                My.Computer.FileSystem.CopyDirectory(OVPNDir.FullName, Application.StartupPath & "\vpn_hdmanager\openvpn\" & OVPNDir.Name, True)
                Progr.Value += 1
            Next
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Information, "Errore nell'aggiornamento VPN")
        End Try

        Me.Close()
    End Sub
    Public Function ComparaFile(ByVal file1 As String, ByVal file2 As String) As Boolean
        Dim file1byte As Integer
        Dim file2byte As Integer
        Dim fs1 As FileStream
        Dim fs2 As FileStream

        ' Determine if the same file was referenced two times.
        If (file1 = file2) Then
            ' Return 0 to indicate that the files are the same.
            Return True
        End If

        ' Open the two files.
        fs1 = New FileStream(file1, FileMode.Open, FileAccess.Read, FileShare.Read)
        fs2 = New FileStream(file2, FileMode.Open, FileAccess.Read, FileShare.Read)

        ' Check the file sizes. If they are not the same, the files
        ' are not equal.
        If (fs1.Length <> fs2.Length) Then
            ' Close the file
            fs1.Close()
            fs2.Close()

            ' Return a non-zero value to indicate that the files are different.
            Return False
        End If

        ' Read and compare a byte from each file until either a
        ' non-matching set of bytes is found or until the end of
        ' file1 is reached.
        Do            
            ' Read one byte from each file.
            file1byte = fs1.ReadByte()
            file2byte = fs2.ReadByte()
        Loop While ((file1byte = file2byte) And (file1byte <> -1))

        ' Close the files.
        fs1.Close()
        fs2.Close()

        ' Return the success of the comparison. "file1byte" is
        ' equal to "file2byte" at this point only if the files are 
        ' the same.
        Return ((file1byte - file2byte) = 0)
    End Function
End Class