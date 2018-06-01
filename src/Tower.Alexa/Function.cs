using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Slight.Alexa.Framework.Models.Requests;
using Slight.Alexa.Framework.Models.Requests.RequestTypes;
using Slight.Alexa.Framework.Models.Responses;
using Tower.Alexa.Handlers;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Tower.Alexa
{
    public class Function
    {
        private readonly Dictionary<Type, Func<SkillRequest, Task<SkillResponse>>> _requestStrategy;

        private readonly IServiceProvider _serviceProvider;
        private readonly IIntentRequestHandler _intentRequestHandler;
        private readonly ILaunchRequestHandler _launchRequestHandler;
        private readonly ISessionEndedRequestHandler _sessionEndedRequestHandler;

        public Function()
        {
            _serviceProvider = new ServiceProviderBuilder().Build();
            _intentRequestHandler = _serviceProvider.GetService<IIntentRequestHandler>();
            _launchRequestHandler = _serviceProvider.GetService<ILaunchRequestHandler>();
            _sessionEndedRequestHandler = _serviceProvider.GetService<ISessionEndedRequestHandler>();

            _requestStrategy = new Dictionary<Type, Func<SkillRequest, Task<SkillResponse>>>()
            {
                { typeof(ILaunchRequest), _launchRequestHandler.HandleAsync },
                { typeof(IIntentRequest), _intentRequestHandler.HandleAsync },
                { typeof(ISessionEndedRequest), _sessionEndedRequestHandler.HandleAsync }
            };            
        }

        public Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            if (!_requestStrategy.ContainsKey(input.GetRequestType()))
                throw new Exception("Unknown request type.");

            return _requestStrategy[input.GetRequestType()](input);
        }
    }
}
