$textToEncrypt = Get-Content -Raw -Path "temp.txt"
$key = [Text.Encoding]::UTF8.GetBytes("tak3Th1sShit")
$keyLength = $key.Length
$bytesToEncrypt = [Text.Encoding]::UTF8.GetBytes($textToEncrypt)

for ($i = 0; $i -lt $bytesToEncrypt.Length; $i++) {
    $bytesToEncrypt[$i] = $bytesToEncrypt[$i] -bxor $key[$i % $keyLength]
}

$encryptedText = [Convert]::ToBase64String($bytesToEncrypt)
Write-Output $encryptedText
