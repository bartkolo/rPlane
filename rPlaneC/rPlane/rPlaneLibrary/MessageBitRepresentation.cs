//http://adsb-decode-guide.readthedocs.io/en/latest/introduction.html#ads-b

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace rPlaneLibrary
{
    public class MessageBitRepresentation
    {
        public const string Index = "#ABCDEFGHIJKLMNOPQRSTUVWXYZ#####_###############0123456789######";
        public BitArray MessageBitArray { get; set; }

        public List<bool> BitesOfMessage = new List<bool>();

        public List<bool> BitesOfEvenMessage;

        public List<bool> BitesOfOddMessage;

        public string FirsReceivedMessage { get; set; }

        public string SecondReceivedMessage { get; set; }

        public MessageBitRepresentation(string message)
        {
            FirsReceivedMessage = message;
            BitesOfMessage = StringToByteArray(message);
        }

        public MessageBitRepresentation(string firstMessage, string secondMessage)
        {
            FirsReceivedMessage = firstMessage;
            SecondReceivedMessage = secondMessage;

            BitesOfEvenMessage = StringToByteArray(FirsReceivedMessage);
            BitesOfOddMessage = StringToByteArray(SecondReceivedMessage);
        }

        #region Methods

        protected List<bool> StringToByteArray(string hex)
        {
            var bitesArray = new List<bool>();
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

                bitesArray.AddRange(itemBites.Reverse());
            }

            return bitesArray;
        }

        protected int GetIntFromBitArray(List<bool> bitArray)
        {
            var value = 0;
            bitArray.Reverse();
            for (var i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i])
                    value += Convert.ToInt32(Math.Pow(2, i));
            }

            return value;
        }

        public int DecodeMessageToInt(int from, int to, List<bool> bitsList = null)
        {
            var bitesArray = BitesOfMessage;

            if (bitsList != null)
                bitesArray = bitsList;

            var bits = new List<bool>();
            for (var a = from; a <= to; a++)
                bits.Add(bitesArray[a]);

            return GetIntFromBitArray(bits);
        }

        public List<bool> DecodeMessageToList(int from, int to, List<bool> bitsList = null)
        {
            var bitesArray = BitesOfMessage;

            if (bitsList != null)
                bitesArray = bitsList;

            var bits = new List<bool>();
            for (var a = from; a <= to; a++)
                bits.Add(bitesArray[a]);

            return bits;
        }

        #endregion Methods
    }
}