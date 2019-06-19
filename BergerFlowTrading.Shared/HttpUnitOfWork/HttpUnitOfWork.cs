using BergerFlowTrading.Shared.HttpUnitOfWork.Identity;
using BergerFlowTrading.Shared.HttpUnitOfWork.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BergerFlowTrading.Shared.HttpUnitOfWork
{
    public class HttpUnitOfWork
    {
        private readonly IHttpContextAccessor context;
        private readonly HttpClient http;

        public IdentityService IdenitytService { get; private set; }

        public HttpExchangeRepository HttpExchangeRepository { get; private set; }
        public HttpUserExchangeSecretRepository HttpUserExchangeSecretRepository { get; private set; }
        public HttpLimitArbitrageStrategy4SettingsRepository HttpLimitArbitrageStrategy4SettingsRepository { get; private set; }
        public HttpPlatformLiveLogsRepository HttpPlatformLiveLogsRepository { get; private set; }

        public HttpUnitOfWork(
                                IHttpContextAccessor context
                                , HttpClient http
                                , IdentityService idService
                                , HttpExchangeRepository HttpExchangeRepository
                                , HttpUserExchangeSecretRepository HttpUserExchangeSecretRepository
                                , HttpLimitArbitrageStrategy4SettingsRepository HttpLimitArbitrageStrategy4SettingsRepository
                                , HttpPlatformLiveLogsRepository HttpPlatformLiveLogsRepository
                                )
        {
            this.context = context;
            this.http = http;
            this.IdenitytService = idService;
            this.HttpExchangeRepository = HttpExchangeRepository;
            this.HttpUserExchangeSecretRepository = HttpUserExchangeSecretRepository;
            this.HttpLimitArbitrageStrategy4SettingsRepository = HttpLimitArbitrageStrategy4SettingsRepository;
            this.HttpPlatformLiveLogsRepository = HttpPlatformLiveLogsRepository;
        }
    }
}
