﻿namespace CAAS.CryptoLib.Interfaces
{

    public interface IMac
    {
        byte[] Generate(byte[] data, byte[] key);
    }
}
