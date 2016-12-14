//http://adsb-decode-guide.readthedocs.io/en/latest/introduction.html#ads-b

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace rPlaneLibrary
{
    public class MessageBitRepresentation
    {
        public BitArray MessageBitArray { get; set; }

        public List<bool> BitesOfMessage = new List<bool>();

        public string TestMessage { get; set; }

        public MessageBitRepresentation(string message)
        {
            TestMessage = message;
            StringToByteArray(message);
        }

        public string Index = "#ABCDEFGHIJKLMNOPQRSTUVWXYZ#####_###############0123456789######";

        #region Methods

        protected void StringToByteArray(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new int[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            foreach (var item in bytes)
            {
                var itemBites = new bool[8];
                for (var i = 0; i < 8; i++)
                {
                    itemBites[i] = ((item >> i) & 1) == 1;
                }

                foreach (var bite in itemBites.Reverse())
                    BitesOfMessage.Add(bite);
            }
        }

        protected int GetIntFromBitArray(List<bool> bitArray)
        {
            var value = 0;

            for (var i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i])
                    value += Convert.ToInt16(Math.Pow(2, i));
            }

            return value;
        }

        public int DecodeMessageToInt(int from, int to)
        {
            var bits = new List<bool>();
            for (var a = from; a <= to; a++)
                bits.Add(BitesOfMessage[a]);

            return GetIntFromBitArray(bits);
        }

        public List<bool> DecodeMessageToList(int from, int to)
        {
            var bits = new List<bool>();
            for (var a = from; a <= to; a++)
                bits.Add(BitesOfMessage[a]);

            return bits;
        }

        #endregion Methods
    }
}