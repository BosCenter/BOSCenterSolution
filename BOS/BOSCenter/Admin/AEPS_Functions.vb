Imports System.Security.Cryptography
Imports System.IO
Imports System.Net

Public Class AEPS_Functions

    Public Shared Function CryptAESIn(ByVal textToCrypt As String, ByVal crypt_key As String, ByVal init_Vector As String) As String
        Try
            Dim cryptkey As Byte() = Encoding.ASCII.GetBytes(crypt_key)
            Dim initVector As Byte() = Encoding.ASCII.GetBytes(init_Vector)

            Using rijndaelManaged = New RijndaelManaged With {
                .Key = cryptkey,
                .IV = initVector,
                .Mode = CipherMode.CBC
            }

                Using memoryStream = New MemoryStream()

                    Using cryptoStream = New CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(cryptkey, initVector), CryptoStreamMode.Write)

                        Using ws = New StreamWriter(cryptoStream)
                            ws.Write(textToCrypt)
                        End Using

                        Return Convert.ToBase64String(memoryStream.ToArray())
                    End Using
                End Using
            End Using

        Catch e As CryptographicException
            Return "A Cryptographic error occurred: {0} " & e.Message
        End Try
    End Function

    Public Shared Function DecryptAESIn(ByVal cipherData As String, ByVal crypt_key As String, ByVal init_Vector As String) As String
        Try
            Dim cryptkey As Byte() = Encoding.ASCII.GetBytes(crypt_key)
            Dim initVector As Byte() = Encoding.ASCII.GetBytes(init_Vector)

            Using rijndaelManaged = New RijndaelManaged With {
                .Key = cryptkey,
                .IV = initVector,
                .Mode = CipherMode.CBC
            }

                Using memoryStream = New MemoryStream(Convert.FromBase64String(cipherData))

                    Using cryptoStream = New CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(cryptkey, initVector), CryptoStreamMode.Read)
                        Return New StreamReader(cryptoStream).ReadToEnd()
                    End Using
                End Using
            End Using

        Catch e As CryptographicException
            Return "A Cryptographic error occurred: {0} " & e.Message
        End Try
    End Function
    Public Shared Function EncryptTestWa(ByVal plainText As String, ByVal crypt_key As String, ByVal init_Vector As String) As String
        Dim Key As Byte() = Encoding.ASCII.GetBytes(crypt_key)
        Dim IV As Byte() = Encoding.ASCII.GetBytes(init_Vector)
        Dim encrypted As Byte()

        Using aes As AesManaged = New AesManaged()
            Dim encryptor As ICryptoTransform = aes.CreateEncryptor(Key, IV)

            Using ms As MemoryStream = New MemoryStream()

                Using cs As CryptoStream = New CryptoStream(ms, encryptor, CryptoStreamMode.Write)

                    Using sw As StreamWriter = New StreamWriter(cs)
                        sw.Write(plainText)
                    End Using

                    encrypted = ms.ToArray()
                End Using
            End Using
        End Using

        Return Convert.ToBase64String(encrypted)
    End Function
    Public Shared Function DecryptTestWa(ByVal cipher_Text As String, ByVal crypt_key As String, ByVal init_Vector As String) As String
        Dim cipherText As Byte() = Encoding.ASCII.GetBytes(cipher_Text)
        Dim Key As Byte() = Encoding.ASCII.GetBytes(crypt_key)
        Dim IV As Byte() = Encoding.ASCII.GetBytes(init_Vector)
        Dim plaintext As String = Nothing

        Using aes As AesManaged = New AesManaged()
            Dim decryptor As ICryptoTransform = aes.CreateDecryptor(Key, IV)

            Using ms As MemoryStream = New MemoryStream(cipherText)

                Using cs As CryptoStream = New CryptoStream(ms, decryptor, CryptoStreamMode.Read)

                    Using reader As StreamReader = New StreamReader(cs)
                        plaintext = reader.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using

        Return plaintext
    End Function
    Public Shared Function EncryptStringToBytesAes(ByVal plainText As String, ByVal key_k As String, ByVal iv_v As String, ByVal Optional KeySize As Int32 = 256) As String
        Dim blockSize As Int32 = 128
        Dim key As Byte() = Encoding.ASCII.GetBytes(key_k)
        Dim iv As Byte() = Encoding.ASCII.GetBytes(iv_v)
        If plainText Is Nothing OrElse plainText.Length <= 0 Then Throw New ArgumentNullException("plainText")
        If key Is Nothing OrElse key.Length <= 0 Then Throw New ArgumentNullException("key")
        If iv Is Nothing OrElse iv.Length <= 0 Then Throw New ArgumentNullException("iv")
        Dim msEncrypt As MemoryStream
        Dim aesAlg As RijndaelManaged = Nothing

        Try
            aesAlg = New RijndaelManaged With {
                .Mode = CipherMode.CBC,
                .KeySize = KeySize,
                .blockSize = blockSize,
                .key = key,
                .iv = iv
            }
            Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)
            msEncrypt = New MemoryStream()

            Using csEncrypt As CryptoStream = New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)

                Using swEncrypt As StreamWriter = New StreamWriter(csEncrypt)
                    swEncrypt.Write(plainText)
                    swEncrypt.Flush()
                    swEncrypt.Close()
                End Using
            End Using

        Finally
            If aesAlg IsNot Nothing Then aesAlg.Clear()
        End Try

        Return Convert.ToBase64String(msEncrypt.ToArray())
    End Function


    Public Shared Function DecryptStringFromBytesAes(ByVal cipherText_c As String, ByVal key_k As String, ByVal iv_v As String, ByVal Optional KeySize As Int32 = 256) As String
        Dim blockSize As Int32 = 128
        Dim cipherText As Byte() = Encoding.ASCII.GetBytes(cipherText_c)
        Dim key As Byte() = Encoding.ASCII.GetBytes(key_k)
        Dim iv As Byte() = Encoding.ASCII.GetBytes(iv_v)
        If cipherText Is Nothing OrElse cipherText.Length <= 0 Then Throw New ArgumentNullException("cipherText")
        If key Is Nothing OrElse key.Length <= 0 Then Throw New ArgumentNullException("key")
        If iv Is Nothing OrElse iv.Length <= 0 Then Throw New ArgumentNullException("iv")
        Dim aesAlg As RijndaelManaged = Nothing
        Dim plaintext As String

        Try
            aesAlg = New RijndaelManaged With {
                .Mode = CipherMode.CBC,
                .KeySize = KeySize,
                .blockSize = blockSize,
                .key = key,
                .iv = iv
            }
            Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)

            Using msDecrypt As MemoryStream = New MemoryStream(cipherText)

                Using csDecrypt As CryptoStream = New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)

                    Using srDecrypt As StreamReader = New StreamReader(csDecrypt)
                        plaintext = srDecrypt.ReadToEnd()
                        srDecrypt.Close()
                    End Using
                End Using
            End Using

        Finally
            If aesAlg IsNot Nothing Then aesAlg.Clear()
        End Try

        Return plaintext
    End Function

End Class
