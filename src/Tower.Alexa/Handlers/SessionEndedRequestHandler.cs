using System.Threading.Tasks;
using Slight.Alexa.Framework.Models.Requests;
using Slight.Alexa.Framework.Models.Responses;

namespace Tower.Alexa.Handlers
{
    public interface ISessionEndedRequestHandler : IRequestHandler { }

    public class SessionEndedRequestHandler : ISessionEndedRequestHandler
    {
        public async Task<SkillResponse> HandleAsync(SkillRequest input)
        {
            await Task.Delay(0);
            return null;
        }
    }
}
