using AottgBotApi.Models;
using System.Collections.Generic;
using System.Linq;

using AottgBotLib;
using Photon.Realtime;

namespace AottgBotApi.Data
{
    public class AottgBotRepo : IAottgBotRepo
    {
        private HostBotClient _reservedClient = null;
        private object _reservedClientLock = new object();

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

        public IEnumerable<AottgRoomInfo> GetServerList(string region)
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

        public IEnumerable<string> GetValidRegions()
        {
            IEnumerable<string> regions = _regionMapping.Select<KeyValuePair<string, PhotonRegion>, string>(e => e.Key);
            return regions;
        }
    }
}
