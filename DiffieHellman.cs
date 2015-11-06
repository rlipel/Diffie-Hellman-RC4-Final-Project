using System;
using System.Numerics;
using System.Security.Cryptography;

public class DiffieHellman
{
    /*** Config ***/
    const int SECRET_KEY_SIZE = 16; // Minimum byte length of secret key

    /*** Diffie Hellman variables ***/
    int g; // Generator number
    BigInteger p; // Prime modulo number

    BigInteger secretKey;
    public BigInteger publicKey { get; private set; }

    /* Default class constructor:  Use predefined values */
    public DiffieHellman(BigInteger prime, int generator)
    {
        this.p = prime;
        this.g = generator;
        GenerateKeys();
    }

    /* Generate the secret and public keys */
    void GenerateKeys()
    {
        this.secretKey = GenerateRandomKey(SECRET_KEY_SIZE);
        this.publicKey = CalculatePublicKey();
    }

    /* Calculate and return the public distributable key */
    BigInteger CalculatePublicKey()
    {
        return BigInteger.ModPow(this.g, this.secretKey, this.p);
    }

    /* Generate a cryptographicly strong random integer */
    BigInteger GenerateRandomKey(int sizebytes)
    {
        byte[] bytes = new byte[sizebytes + 1];
        // Use RNGCryptoServiceProvider class to generate a random sequence of bytes:
        new RNGCryptoServiceProvider().GetBytes(bytes);
        bytes[sizebytes] = 0x0;
        return new BigInteger(bytes);
    }

    /* Calculate the shared Diffie-Hellman secret using the other party's public key */
    public BigInteger CalculateSharedSecret(BigInteger otherKey)
    {
        return BigInteger.ModPow(otherKey, this.secretKey, this.p);
    }

    /* Pack g, p, and publicKey so we can send it to the other DH party */
    public string ToExchangeString()
    {
        return string.Format("DiffieHellman:g={0:x},p={1:x},key={2:x}", this.g, this.p, this.publicKey);
    }
}