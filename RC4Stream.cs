class RC4Stream
{
    /* RC4 state table */
    byte[] table = new byte[256];

    /* RC4 Stream constructor: Key-scheduling algorithm (KSA) */
    public RC4Stream(byte[] key)
    {
        // Initialize the permutation:
        for (int i = 0; i < 256; i++)
            table[i] = (byte)i;

        // Scramble the permutation according to the key:
        int j = 0;
        for (int i = 0; i < 256; i++)
        {
            j = (j + table[i] + key[i % key.Length]) % 256;

            // Swap values of table[i] and table[j]
            byte t = table[i];
            table[i] = table[j];
            table[j] = t; 
        }
    }

    /* RC4 Cipher function: Encrypt or decrypt input (PRGA) */
    public byte[] RC4(byte[] input)
    {
        byte[] output = new byte[input.Length];
        int counter = 0;

        int i = 0;
        int j = 0;
        while (counter < input.Length)
        {
            i = (i + 1) % 256;
            j = (j + table[i]) % 256;

            // Swap values of table[i] and table[j]
            byte t = table[i];
            table[i] = table[j];
            table[j] = t;

            output[counter] = (byte)(input[counter] ^ table[(table[i] + table[j]) % 256]);
            counter++;
        }

        return output;
    }
}