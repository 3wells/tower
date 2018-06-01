using System.Threading.Tasks;
using Slight.Alexa.Framework.Models.Requests;
using Slight.Alexa.Framework.Models.Responses;

namespace Tower.Alexa.Handlers
{
    public interface ILaunchRequestHandler : IRequestHandler { }

    public class LaunchRequestHandler : ILaunchRequestHandler
    {
        public Task<SkillResponse> HandleAsync(SkillRequest input)
        {                        
            return Task.Run(() => new SkillResponse
            {
                Response = new Response
                {
                    ShouldEndSession = false,
                    OutputSpeech = new PlainTextOutputSpeech
                    {
                        Text = "Welcome to Control Tower. You can ask Control Tower for weather at an airport by name, or 4 letter identifier. " +
                               "You can also ask Control Tower for the 4 letter airport identifier given a name. " +
                               "Let's start with weather. What airport would you like weather at? "
                    }
                },
                Version = "1.0"
            });
        }
    }
}
