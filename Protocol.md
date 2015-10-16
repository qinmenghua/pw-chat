PW Chat network data transfer specification

The server expects a JSON array formatted like so
`[encryption, sha512checksum, data, iv = null]`
Encryption is either 1 or 0 that representing if the data is encrypted with
the shared secret AES (Rinjdael) key

sha512checksum is for data integrity check, it is of the UNENCRYPTED data
not of the ENCRYPTED data represented in base64 of the hash NOT in hex

data is either a JSON object, in which case the array would look like
`[encryption, sha512checksum, base64object]`
base64object is the object (for example:
`{"action" : "value", "key" : "value", "key" : "value" }`)
in base64 EVEN IF the data is not encrypted, if encrypted
the object is still the same but encrypted

A login object is
`{"action" : "login", "username" : "usernameString", "password" : "passwordString"} `
which is transferred as a standard data message (as specified above)
The passwordString is ALWAYS plaintext, the client should use encryption
but is not required (and can be disabled any time during communication)

A logout object is only
`{"action" : "logout"}`

iv does not need to be sent as the key generated and given by the client
contains an IV to be used but it can be specified, the PSK is still used
the PSK can not be transmitted in the message ever

By default the server and client use [PKCS7](http://tools.ietf.org/html/rfc5652#section-6.3) padding for the encryption, but
you may use any padding mode you desire as long as the server and client
are configured to know about it.