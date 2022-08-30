using System;
using System.Net;

namespace Mirror.Discovery
{
    public struct ServerResponse : NetworkMessage
    {
       
        public IPEndPoint EndPoint { get; set; }

        public Uri uri;

        public string NameRoom;
        public string NumberClients;
        public long serverId;
    }
}