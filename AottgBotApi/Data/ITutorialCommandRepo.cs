using AottgBotApi.Models;
using System.Collections.Generic;

namespace AottgBotApi.Data
{
    public interface ITutorialCommandRepo
    {
        /// <summary>
        /// Returns all the application commands.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{TutorialCommand}"/>.</returns>
        IEnumerable<TutorialCommand> GetAllCommands();

        /// <summary>
        /// Gets the <see cref="TutorialCommand"/> by its integer Id.
        /// </summary>
        /// <param name="id">The Id of the object.</param>
        /// <returns>A <see cref="TutorialCommand"/> object.</returns>
        TutorialCommand GetCommandById(int id);
    }
}