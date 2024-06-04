using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WebSocket4Net;

namespace Trade
{
    internal class WebSocketConnet
    {
        public WebSocket open()
        {
            WebSocket4Net.WebSocket webSocket = new WebSocket4Net.WebSocket(Info.getSocketUrl(), sslProtocols: SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls);
            
            // Event handler for WebSocket opened
            webSocket.Error += Error;

            // Open the WebSocket connection
            webSocket.Open();
            return webSocket;
        }

        private void Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Console.WriteLine("Connection Error : " + e.Exception.ToString());
        }
    }
}