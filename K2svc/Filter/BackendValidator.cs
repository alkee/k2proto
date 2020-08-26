﻿using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace K2svc.Filter
{
    public class BackendValidator : Interceptor
    {
        private readonly ILogger<BackendValidator> logger;
        private readonly ServiceConfiguration config;

        public BackendValidator(ILogger<BackendValidator> _logger, ServiceConfiguration _config)
        {
            logger = _logger;
            config = _config;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        { // Server 의 handler 가 호출될 때
            if (IsBackendMethod(context.Method) == false) return base.UnaryServerHandler(request, context, continuation); // not interested
            var backendGroupId = context.RequestHeaders.FirstOrDefault(
                (me) => me.Key.Equals(nameof(config.BackendGroupId), StringComparison.OrdinalIgnoreCase)
                )?.Value;
            if (config.BackendGroupId == backendGroupId)
                return base.UnaryServerHandler(request, context, continuation);

            throw new ApplicationException($"invalid backend request of {context.Method} from {context.Peer}");
        }

        private static bool IsBackendMethod(string method)
        {
            return method.StartsWith("/K2B.");
        }
    }
}
