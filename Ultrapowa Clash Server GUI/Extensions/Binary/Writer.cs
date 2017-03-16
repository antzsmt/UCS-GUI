﻿namespace UCS.Extensions.Binary
{
    #region Usings

    using System;
    using System.IO;

    using UCS.Core.Settings;

    #endregion Usings

    public class Writer : BinaryWriter
    {
        public Writer()
            : base(new MemoryStream(new byte[Constants.MaxBuffer]))
        {
        }

        public void Write(int _Value, bool _Inverted = false)
        {
            if (_Inverted)
            {
                byte[] _Bytes = BitConverter.GetBytes(_Value);
                Array.Reverse(_Bytes);
                this.Write(_Bytes);
            }
            else
            {
                this.Write(_Value);
            }
        }

        public void Write(long _Value, bool _Inverted = false)
        {
            if (_Inverted)
            {
                byte[] _Bytes = BitConverter.GetBytes(_Value);
                Array.Reverse(_Bytes);
                this.Write(_Bytes);
            }
            else
            {
                this.Write(_Value);
            }
        }

        public void Write(short _Value, bool _Inverted = false)
        {
            if (_Inverted)
            {
                byte[] _Bytes = BitConverter.GetBytes(_Value);
                Array.Reverse(_Bytes);
                this.Write(_Bytes);
            }
            else
            {
                this.Write(_Value);
            }
        }

        public void Write(bool _Value, bool _Inverted = false)
        {
            if (_Inverted)
            {
                byte[] _Bytes = BitConverter.GetBytes(_Value);
                Array.Reverse(_Bytes);
                this.Write(_Bytes);
            }
            else
            {
                this.Write(_Value);
            }
        }
    }
}