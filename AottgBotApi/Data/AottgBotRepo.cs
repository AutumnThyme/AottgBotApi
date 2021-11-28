using AottgBotApi.Models;
using System.Collections.Generic;
using System.Linq;

using AottgBotLib;
using Photon.Realtime;
using System.Threading;
using AottgBotApi.AottgBotLib;
using System;

namespace AottgBotApi.Data
{
    public class AottgBotRepo : IAottgBotRepo
    {
        private HostBotClient _reservedClient = null;
        private object _reservedClientLock = new object();

        private const uint _MAX_CLIENTS = 3;

        private uint _clientId = 0;

        /// <summary>
        /// The HostBotClient resource which a user of this code should create new when the resource is unlocked.
        /// </summary>
        private ClientResource[] _reservedClients = new ClientResource[_MAX_CLIENTS];

        private object _arrayReadLock = new object();

        private readonly Dictionary<string, PhotonRegion> _regionMapping = new Dictionary<string, PhotonRegion>
        {
            { 
                "Europe", PhotonRegion.Europe
            },
            {
                "USA", PhotonRegion.USA
            },
            {
                "Asia", PhotonRegion.Asia
            },
            {
                "SA", PhotonRegion.SA
            },
        };

        /// <summary>
        /// name, map, diff, time, daylight, pass, random
        /// </summary>
        private enum RoomName { NAME, MAP, DIFF, TIME, DAYLIGHT, PASSWORD, RANDOM };


        public AottgBotRepo()
        {
            for (int i = 0; i < _reservedClients.Length; i++)
            {
                _reservedClients[i] = new ClientResource();
            }
        }

        public IEnumerable<AottgRoomInfo> GetServerListSingleResource(string region)
        {
            if (!_regionMapping.ContainsKey(region))
            {
                // bad request
                return null;
            }
            lock(_reservedClientLock)
            {
                _reservedClient = new HostBotClient("reserved_client");
                _reservedClient.Region = _regionMapping[region];
                _reservedClient.ConnectToMasterAsync().Wait();

                IReadOnlyList<RoomInfo> list = _reservedClient.RoomList;

                IEnumerable<AottgRoomInfo> serverlist = list.Select<RoomInfo, AottgRoomInfo>(roominfo =>
                {
                    var name = roominfo.Name.Split("`");
                    return new AottgRoomInfo
                    {
                        Name = name[(int)RoomName.NAME],
                        Map = name[(int)RoomName.MAP],
                        Difficulty = name[(int)RoomName.DIFF],
                        Time = int.Parse(name[(int)RoomName.TIME]),
                        Daylight = name[(int)RoomName.DAYLIGHT],
                        EncryptedPassword = name[(int)RoomName.PASSWORD],
                        RandomNumber = int.Parse(name[(int)RoomName.RANDOM]),
                    };
                });
                _reservedClient.Disconnect();
                _reservedClient = null;

                return serverlist;
            }
        }

        public IEnumerable<AottgRoomInfo> GetServerList(string region)
        {
            if (!_regionMapping.ContainsKey(region))
            {
                // bad request
                return null;
            }
            uint resource = 0;
            lock (_arrayReadLock)
            {
                resource = _clientId;
                _clientId = (_clientId + 1) % _MAX_CLIENTS;
            }
            lock (_reservedClients[resource])
            {
                _reservedClients[resource].client = new HostBotClient("reserved_client");
                _reservedClients[resource].client.Region = _regionMapping[region];
                _reservedClients[resource].client.ConnectToMasterAsync().Wait();

                IReadOnlyList<RoomInfo> list = _reservedClients[resource].client.RoomList;

                IEnumerable<AottgRoomInfo> serverlist = list.Select<RoomInfo, AottgRoomInfo>(roominfo =>
                {
                    var name = roominfo.Name.Split("`");
                    return new AottgRoomInfo
                    {
                        Name = name[(int)RoomName.NAME],
                        Map = name[(int)RoomName.MAP],
                        Difficulty = name[(int)RoomName.DIFF],
                        Time = int.Parse(name[(int)RoomName.TIME]),
                        Daylight = name[(int)RoomName.DAYLIGHT],
                        EncryptedPassword = name[(int)RoomName.PASSWORD],
                        RandomNumber = int.Parse(name[(int)RoomName.RANDOM]),
                    };
                });
                _reservedClients[resource].client.Disconnect();
                _reservedClients[resource].client = null;
                return serverlist;
            }
        }

        public IEnumerable<string> GetValidRegions()
        {
            IEnumerable<string> regions = _regionMapping.Select<KeyValuePair<string, PhotonRegion>, string>(e => e.Key);
            return regions;
        }
    }
}
