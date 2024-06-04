using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Trade
{
    internal class Info
    {
        private static string socketUrl = "wss://api-feed.dhan.co";
        private static string httpUrl = "https://api.dhan.co/charts/";
        private static string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJpc3MiOiJkaGFuIiwicGFydG5lcklkIjoiNThmYWJlMWEiLCJleHAiOjE3MDk2MDgyNzUsInRva2VuQ29uc3VtZXJUeXBlIjoiUEFSVE5FUiIsImRoYW5DbGllbnRJZCI6IjEwMDA2MzExNTIifQ.89ZMjqGbtqy7YIJZk_fu7MDgZqaI8ihI41A848S8fHe557tnN8-xNvj-9q9xX1Ce5jR636CyoTip0pD5_uppiA";
        private static object[,] segment = { {1, "10666"} };
        private static string[] symbol = { "CRUDEOIL24MARFUT" };
        private static string clientID = "1000631152";
        private static string instrument = "EQUITY";

        public static void setInstrument(string newInstrument) { 
            instrument = newInstrument;
        }

        public static string getInstrument()
        {
            return instrument;
        }


        public static string getSocketUrl() { 
            return socketUrl; 
        }
        public static void setSocketUrl(string newUrl = "") {
            socketUrl = newUrl; 
        }

        public static string getHttpUrl() { return httpUrl; }   
        public static void setHttpUrl(string newUrl)
        {
            httpUrl = newUrl;
        } 

        public static string getToken() { 
            return token;
        }
        public static void setToken(string newToken)
        {
            token = newToken;
        }

        public static object[,] getSegment() { return segment; }
        public static void setSegment(object[,] newSegment)
        {
            segment = newSegment;
        }

        public static string[] getSymbol() {  return symbol; }
        public static void setSymbol(string[] newSymbol)
        {
            symbol = newSymbol;
        }

        public static string getClientID() { return clientID; }
        public static void setClientID(string newClientID)
        {
            clientID = newClientID;
        }    
    }
}
