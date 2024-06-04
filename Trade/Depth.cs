﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;

namespace Trade
{
    internal class Depth
    {
        private static WebSocket webSocket = null;
        private static byte feedcode = 19;

        public Depth()
        {
            webSocket = null;
            feedcode = 19;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void ConsoleCancelEventHandler(object sender, ConsoleCancelEventArgs e)
        {
            feedcode = 20;
            getDepth();
            Thread.Sleep(2000);
            this.Dispose();
        }

        public void getSocket()
        {
            WebSocketConnet weConnect = new WebSocketConnet();
            webSocket = weConnect.open();

            webSocket.Opened += Opened;
            Console.CancelKeyPress += ConsoleCancelEventHandler;

            while (!Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    webSocket.Close();
                    webSocket.Dispose();
                    break;
                }
            }
            return;
        }

        public void Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Connection opened.");

            webSocket.DataReceived += getData;
            webSocket.MessageReceived += getMsg;
            webSocket.Closed += Close;

            getDepth();
            return;
        }

        public void Close(object sender, EventArgs e)
        {
            Console.WriteLine("Connection closed.");
            return;
            //getTicker();
        }

        public void getDepth()
        {
            Console.WriteLine("Start getting Depth Data...");

            object[,] instruments = Info.getSegment();

            byte feedtype = feedcode;
            ushort messageLen = 129;
            string clientId = Info.getClientID();
            string dhanAuth = new string('\0', 50);
            Int32 instrumentLen = instruments.Length / 2;

            clientId = clientId.PadRight(30, '\0');

            int offset = 0;
            byte[] request = new byte[sizeof(byte) + sizeof(short) + clientId.Length + dhanAuth.Length + sizeof(Int32) + 21 * 100];

            request[offset++] = feedtype;
            BitConverter.GetBytes(messageLen).CopyTo(request, offset);
            offset += sizeof(ushort);
            System.Text.Encoding.ASCII.GetBytes(clientId).CopyTo(request, offset);
            offset += clientId.Length;
            System.Text.Encoding.ASCII.GetBytes(dhanAuth).CopyTo(request, offset);
            offset += dhanAuth.Length;
            BitConverter.GetBytes(instrumentLen).CopyTo(request, offset);
            offset += sizeof(Int32);
            for (int i = 0; i < (instruments.Length) / 2; i++)
            {
                byte temp = byte.Parse(instruments[i, 0].ToString());
                BitConverter.GetBytes(temp).CopyTo(request, offset);
                offset += sizeof(byte);
                string strTemp = instruments[i, 1].ToString().PadRight(20, '\0');
                System.Text.Encoding.ASCII.GetBytes(strTemp).CopyTo(request, offset);
                offset += 20;
            }
            for (int i = instrumentLen; i < 100; i++)
            {
                byte temp = byte.Parse(instruments[0, 0].ToString());
                BitConverter.GetBytes(temp).CopyTo(request, offset);
                offset += sizeof(byte);
                string strTemp = instruments[0, 1].ToString().PadRight(20, '\0');
                System.Text.Encoding.ASCII.GetBytes(strTemp).CopyTo(request, offset);
                offset += 20;
            }

            // make Request Header

            webSocket.Send(request, 0, request.Length);
            return;
        }

        public void getData(object sender, DataReceivedEventArgs e)
        {
            byte[] data = new byte[4];
            byte[] twodata = new byte[2];

            Console.ForegroundColor = ConsoleColor.Yellow; // Set text color to red

            Console.WriteLine("------------------------------");
            Console.ForegroundColor = ConsoleColor.Green; // Set text color to red
            Console.WriteLine("--> Feed Code : " + e.Data[0]);
            Console.WriteLine("--> Message Length : " + (int.Parse(e.Data[1].ToString()) * 100 + int.Parse(e.Data[2].ToString())));
            Console.WriteLine("--> Exchange Segment : " + e.Data[3]);

            Array.Copy(e.Data, 4, data, 0, 4);
            Console.WriteLine("--> Security ID : " + BitConverter.ToInt32(data, 0).ToString());

            Array.Copy(e.Data, 8, data, 0, 4);
            Console.WriteLine("--> Last Traded Price : " + BitConverter.ToInt32(data, 0).ToString());

            for (int i = 0; i < 5; i++)
            {
                Array.Copy(e.Data, 12 + 20 * i, data, 0, 4);
                Console.WriteLine("--> Bid Quantity : " + BitConverter.ToInt32(data, 0).ToString());
                Array.Copy(e.Data, 16 + 20 * i, data, 0, 4);
                Console.WriteLine("--> Ask Quantiry : " + BitConverter.ToInt32(data, 0).ToString());

                Array.Copy(e.Data, 20 + 20 * i, twodata, 0, 2);
                Console.WriteLine("--> No. of Bid Order : " + BitConverter.ToInt16(twodata, 0).ToString());
                Array.Copy(e.Data, 22 + 20 * i, twodata, 0, 2);
                Console.WriteLine("--> No. of Ask Orders : " + BitConverter.ToInt16(twodata, 0).ToString());

                Array.Copy(e.Data, 24 + 20 * i, data, 0, 4);
                Console.WriteLine("--> Bid Price : " + BitConverter.ToInt32(data, 0).ToString());
                Array.Copy(e.Data, 28 + 20 * i, data, 0, 4);
                Console.WriteLine("--> Ask Price : " + BitConverter.ToInt32(data, 0).ToString());
            }
        }

        public void getMsg(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Connection MessageReceived : " + e.Message);
        }
    }
}
