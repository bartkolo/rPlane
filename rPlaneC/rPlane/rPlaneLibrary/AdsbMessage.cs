using System;
using System.Collections;

namespace rPlaneLibrary
{
    public class AdsbMessage : MessageBitRepresentation
    {
        public BitArray MessageBitArray { get; set; }

        public string TestMessage { get; private set; }

        public AdsbMessage(string message) : base(message)
        {
            TestMessage = message;
            MessageBitArray = StringToByteArray(TestMessage);
        }


    }
}

/*
 *
 *1	5	DF	Downlink Format
6	8	CA	Message Subtype
9	32	ICAO24	ICAO aircraft address
33	88	DATA	Data frame
89	112	PC	Parity check
 *
 */