using AottgBotLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AottgBotApi.AottgBotLib
{
    public class ClientResource
    {
        private static uint _globalId;
        private uint _resourceId;

        public HostBotClient client;

        public ClientResource()
        {
            Console.WriteLine("here");
            _resourceId = _globalId;
            _globalId++;
            client = null;
        }

        public ClientResource(uint resourceId)
        {
            _resourceId = resourceId;
            client = null;
        }

        public uint GetResourceId()
        {
            return _resourceId;
        }
    }
}
