using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Numerics;
using System.Security.Cryptography;

namespace dh_server
{
    /* SecureClient class:
     *      This class inherits from Client class and adds the encryption and decryption functionality.
     *      This class is for the server side of the connection- it decides on the DH parameters and sends it to the client.
     * */

    public class SecureClient : Client
    {
        /* SHA-1 abstract object for SHA-1 hash */
        static SHA1 sha = new SHA1CryptoServiceProvider();

        /* Diffie Hellman object and DH shared secret: */
        DiffieHellman DHObject;
        BigInteger sharedSecret = BigInteger.Zero;

        /* RC4 streams */
        RC4Stream rc4InStream; // RC4 from client to server
        RC4Stream rc4OutStream; // RC4 from server to client

        /* Security status: True after key exchange completion and RC4 initialization (ready for communication) */
        bool clientReady = false;

        /* SecureClient(): Constructor */
        public SecureClient(Socket sock, ServerMain form) : base(sock, form)
        {
            /* Create an instance of Diffie Hellman */
            DHObject = new DiffieHellman(DiffieHellmanParams.RFC_P, DiffieHellmanParams.RFC_G);

            /* Tranform the DH object to a transferable string and send it to the client */
            string exchangeString = DHObject.ToExchangeString();
            base.Send(exchangeString);
        }

        /* KeyExchangeComplete(): This is called upon key exchange completion */
        void KeyExchangeComplete()
        {
            if (sharedSecret != BigInteger.Zero)
            {
                // We have a shared Diffie-Hellman key. Now create RC4 streams for in & out traffic:
                byte[] RC4InKey = CreateRC4Key(sharedSecret, "client-to-server"); // Indicating in stream direction
                rc4InStream = new RC4Stream(RC4InKey);

                byte[] RC4OutKey = CreateRC4Key(sharedSecret, "server-to-client"); // Indicating out stream direction
                rc4OutStream = new RC4Stream(RC4OutKey);

                // Set security status to ready and allow further communication
                clientReady = true;
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
            // Are we awaiting an exchange message from the client?
            if (!clientReady)
            {
                string exchangeString = Encoding.ASCII.GetString(messageData);
                if (exchangeString.StartsWith("DiffieHellman"))
                {
                    // Use Regular Expressions to extract key:
                    Match match = Regex.Match(exchangeString, "key=(.*)$");
                    if (match.Success)
                    {
                        string keyString = match.Groups[1].Value;
                        BigInteger key = BigInteger.Parse(keyString, System.Globalization.NumberStyles.HexNumber);

                        sharedSecret = DHObject.CalculateSharedSecret(key);
                        KeyExchangeComplete();
                    }
                }
            }
            else
            {
                // Handling an encrypted message
                // Decrypt the message
                byte[] decrypted = rc4InStream.RC4(messageData);
                // Forward it to Client class
                base.HandleMessage(decrypted);
            }
        }
    }
}
