using System;
using System.Collections;

namespace rPlaneLibrary
{
    public class MessageBitRepresentation
    {
        //http://adsb-decode-guide.readthedocs.io/en/latest/introduction.html#ads-b
        public MessageBitRepresentation(string message)
        {
            
        }


        #region private

        protected BitArray StringToByteArray(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return new BitArray(bytes);
        }

        protected int GetIntFromBitArray(BitArray bitArray)
        {
            var value = 0;

            for (var i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i])
                    value += Convert.ToInt16(Math.Pow(2, i));
            }

            return value;
        }

        #endregion private
    }
}
