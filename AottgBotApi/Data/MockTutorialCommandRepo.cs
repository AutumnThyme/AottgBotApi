using AottgBotApi.Models;
using System.Collections.Generic;

namespace AottgBotApi.Data
{
    public class MockTutorialCommandRepo : ITutorialCommandRepo
    {
        public IEnumerable<TutorialCommand> GetAllCommands()
        {
            var commands = new List<TutorialCommand>
            {
                new TutorialCommand
                {
                    Id = 0,
                    HowTo = "You don't.",
                    Line = "Line",
                    Platform = "Heroku or MyPc."
                },
                new TutorialCommand
                {
                    Id = 1,
                    HowTo = "You can't.",
                    Line = "Also a Line",
                    Platform = "Heroku or MyPc."
                },
                new TutorialCommand
                {
                    Id = 2,
                    HowTo = "You won't.",
                    Line = "A Line once again",
                    Platform = "Heroku or MyPc."
                },
            };

            return commands;
        }

        public TutorialCommand GetCommandById(int id)
        {
            return new TutorialCommand
            { 
                Id = 0,
                HowTo = "You don't.",
                Line = "Line",
                Platform = "Heroku or MyPc."
            };
        }
    }
}
