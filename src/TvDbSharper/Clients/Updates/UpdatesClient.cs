﻿namespace TvDbSharper.Clients.Updates
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using TvDbSharper.BaseSchemas;
    using TvDbSharper.Clients.Updates.Json;
    using TvDbSharper.Errors;
    using TvDbSharper.JsonClient;

    public class UpdatesClient : BaseClient, IUpdatesClient
    {
        internal UpdatesClient(IJsonClient jsonClient, IErrorMessages errorMessages)
            : base(jsonClient, errorMessages)
        {
        }

        public Task<TvDbResponse<Update[]>> GetAsync(DateTime fromTime, CancellationToken cancellationToken)
        {
            try
            {
                string requestUri = $"/updated/query?fromTime={fromTime.ToUnixEpochTime()}";

                return this.GetAsync<Update[]>(requestUri, cancellationToken);
            }
            catch (TvDbServerException ex)
            {
                string message = this.GetMessage(ex.StatusCode, this.ErrorMessages.Updates.GetAsync);

                if (message == null)
                {
                    throw;
                }

                throw new TvDbServerException(message, ex.StatusCode, ex);
            }
        }

        public Task<TvDbResponse<Update[]>> GetAsync(DateTime fromTime, DateTime toTime, CancellationToken cancellationToken)
        {
            try
            {
                string requestUri = $"/updated/query?fromTime={fromTime.ToUnixEpochTime()}&toTime={toTime.ToUnixEpochTime()}";

                return this.GetAsync<Update[]>(requestUri, cancellationToken);
            }
            catch (TvDbServerException ex)
            {
                string message = this.GetMessage(ex.StatusCode, this.ErrorMessages.Updates.GetAsync);

                if (message == null)
                {
                    throw;
                }

                throw new TvDbServerException(message, ex.StatusCode, ex);
            }
        }

        public Task<TvDbResponse<Update[]>> GetAsync(DateTime fromTime)
        {
            return this.GetAsync(fromTime, CancellationToken.None);
        }

        public Task<TvDbResponse<Update[]>> GetAsync(DateTime fromTime, DateTime toTime)
        {
            return this.GetAsync(fromTime, toTime, CancellationToken.None);
        }
    }
}