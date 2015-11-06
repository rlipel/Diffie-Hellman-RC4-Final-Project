using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Numerics;
using System.Security.Cryptography;

namespace dh_client
{
    /* SecureConnection class:
     *      This class inherits from Connection class and adds the encryption and decryption functionality.
     *      This class is for the client side of the connection, as it receives Diffie-Hellman parameters from the server and exchanges the key accordingly.
     * */

    public class SecureConnection : Connection
    {
        /* SHA-1 abstract object for SHA-1 hash */
        static SHA1 sha = new SHA1CryptoServiceProvider();

        /* Diffie Hellman object and DH shared secret: */
        DiffieHellman DHObject;
        BigInteger sharedSecret = BigInteger.Zero;

        /* RC4 streams */
        RC4Stream rc4InStream; // RC4 from server to client
        RC4Stream rc4OutStream; // RC4 from client to server

        /* Security status: True after key exchange completion and RC4 initialization (ready for communication) */
        bool clientReady = false;

        /* SecureConnection(): Constructor */
        public SecureConnection(string ipaddress, int port, ClientMain form) : base(ipaddress, port, form)
        {
            /* because it is clientside, we first need to get the DH parameters from the server in order to initialize */
        }

        /* GotDHParameters(): Called once received the DH info from server - all initialization in here */
        void GotDHParameters(int g, BigInteger p, BigInteger otherKey)
        {
            // Create Diffie Hellman instance using p, g:
            DHObject = new DiffieHellman(p, g);

            // Send the server the Diffie Hellman client key:
            string exchangeString = DHObject.ToExchangeString();
            base.Send(exchangeString);

            // Calculate sharedSecret:
            sharedSecret = DHObject.CalculateSharedSecret(otherKey);

            // End key exchange:
            KeyExchangeComplete();
        }

        /* KeyExchangeComplete(): This is called upon key exchange completion */
        void KeyExchangeComplete()
        {
            if (sharedSecret != BigInteger.Zero)
            {
                // We have a shared Diffie-Hellman key. Now create RC4 streams for in & out traffic:
                byte[] RC4InKey = CreateRC4Key(sharedSecret, "server-to-client"); // Indicating in stream direction
                rc4InStream = new RC4Stream(RC4InKey);

                byte[] RC4OutKey = CreateRC4Key(sharedSecret, "client-to-server"); // Indicating out stream direction
                rc4OutStream = new RC4Stream(RC4OutKey);

                // Set security status to ready and allow further communication
                clientReady = true;
                main.OnSecureReady();
            }
        }

        /* CreateRC4Key(): Transform the shared DH key combined with a string to an RC4 key */ 
        byte[] CreateRC4Key(BigInteger masterKey, string otherInfo)
        {
            byte[] masterKeyBytes = masterKey.ToByteArray();

            // Concatenate parameters:
            byte[] concatenation = new byte[masterKeyBytes.Length + otherInfo.Length];
            masterKeyBytes.CopyTo(concatenation, 0);

            byte[] otherInfoBytes = Encoding.ASCII.GetBytes(otherInfo);
            otherInfoBytes.CopyTo(concatenation, masterKeyBytes.Length);

            // Hash the result:
            return sha.ComputeHash(concatenation);
        }

        /**** Overridden Client class methods: ****/

        /* These are overriden send & receive methods that add decryption and encryption of the traffic */

        /* Send(byte[]): Encrypt the data, then use the base's send function to send it */
        public override int Send(byte[] data)
        {
            if (clientReady)
            {
                byte[] encrypted = rc4OutStream.RC4(data);
                return base.Send(encrypted);
            }
            else return base.Send(data);
        }

        /* HandleMessage(): Handle an incoming message */
        protected override void HandleMessage(byte[] messageData)
        {
            // Are we awaiting an exchange message from the server?
            if (!clientReady)
            {
                string exchangeString = Encoding.ASCII.GetString(messageData);
                if (exchangeString.StartsWith("DiffieHellman"))
                {
                    // This is the Diffie Hellman message from the server- parse it.
                    // Use Regular Expressions to extract g, p, and key:
                    Match match = Regex.Match(exchangeString, "g=(.*),p=(.*),key=(.*)$");
                    if (match.Success)
                    {
                        string gString = match.Groups[1].Value;
                        string pString = match.Groups[2].Value;
                        string keyString = match.Groups[3].Value;

                        int g = Convert.ToInt32(gString, 16);
                        BigInteger p = BigInteger.Parse(pString, System.Globalization.NumberStyles.HexNumber);
                        BigInteger key = BigInteger.Parse(keyString, System.Globalization.NumberStyles.HexNumber);

                        GotDHParameters(g, p, key);
                    }
                }
            }
            else
            {
                // Handling an encrypted message
                // Decrypt the message
                byte[] decrypted = rc4InStream.RC4(messageData);
                // Forward it to Connection class
                base.HandleMessage(decrypted);
            }
        }
    }
}
