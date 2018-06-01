using System.Threading.Tasks;
using Slight.Alexa.Framework.Models.Requests;
using Slight.Alexa.Framework.Models.Responses;

namespace Tower.Alexa.Handlers
{
    public interface IRequestHandler
    {
        Task<SkillResponse> HandleAsync(SkillRequest input);
    }
}
