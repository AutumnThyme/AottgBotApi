using AottgBotApi.Models;
using System.Collections.Generic;

namespace AottgBotApi.Data
{
    public interface IAottgBotRepo
    {
        /// <summary>
        /// Gets an object containing a list of valid regions.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{string}"/> containing the region names.</returns>
        IEnumerable<string> GetValidRegions();

        /// <summary>
        /// Gets the server list for a given region.
        /// </summary>
        /// <param name="region">The string name of the region</param>
        /// <returns>A <see cref="IEnumerable{AottgRoomInfo}"/> containing the rooms under the region.</returns>
        IEnumerable<AottgRoomInfo> GetServerList(string region);
    }
}