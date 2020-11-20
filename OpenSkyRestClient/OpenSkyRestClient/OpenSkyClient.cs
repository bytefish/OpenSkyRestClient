// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using OpenSkyRestClient.Http.Builder;
using OpenSkyRestClient.Model;
using OpenSkyRestClient.Model.Response;
using OpenSkyRestClient.Options;
using OpenSkyRestClient.Parser;
using OpenSkyRestClient.Utils;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSkyRestClient
{
    public class OpenSkyClient
    {
        private readonly string apiUrl;
        private readonly HttpClient httpClient;

        public OpenSkyClient()
            : this(new HttpClient())
        {
        }

        public OpenSkyClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.apiUrl = "https://opensky-network.org/api";
        }

        public async Task<StateVectorResponse> GetAllStateVectorsAsync(int? time = null, string icao24 = null, BoundingBox boundingBox = null, Credentials credentials = null, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            var url = $"{apiUrl}/states/all";

            var httpRequestMessageBuilder = new HttpRequestMessageBuilder(url, HttpMethod.Get);

            if (time.HasValue)
            {
                httpRequestMessageBuilder.AddQueryString("time", time.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (icao24 != null)
            {
                httpRequestMessageBuilder.AddQueryString("icao24", icao24);
            }

            if (boundingBox != null)
            {
                httpRequestMessageBuilder.AddQueryString("lamin", boundingBox.LaMin.ToString(CultureInfo.InvariantCulture));
                httpRequestMessageBuilder.AddQueryString("lomin", boundingBox.LoMin.ToString(CultureInfo.InvariantCulture));
                httpRequestMessageBuilder.AddQueryString("lamax", boundingBox.LaMax.ToString(CultureInfo.InvariantCulture));
                httpRequestMessageBuilder.AddQueryString("lomax", boundingBox.LoMax.ToString(CultureInfo.InvariantCulture));
            }

            var httpRequestMessage = httpRequestMessageBuilder.Build();

            if (credentials != null)
            {
                SetBasicAuthHeader(httpRequestMessage, credentials);
            }

            var httpResponse = await httpClient
                .SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken)
                .ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var statusCode = httpResponse.StatusCode;
                var reason = httpResponse.ReasonPhrase;

                throw new Exception($"API Request failed with Status Code {statusCode} and Reason {reason}. For additional information, see the HttpResponseMessage in this Exception.");
            }

            var json = await httpResponse.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            var stateVectorResponse = StateVectorResponseParser.Parse(json);

            return stateVectorResponse;
        }

        public async Task<StateVectorResponse> GetOwnStateVectorsAsync(int? time = null, string icao24 = null, int[] serials = null, Credentials credentials = null, BoundingBox boundingBox = null, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            var url = $"{apiUrl}/states/own";

            var httpRequestMessageBuilder = new HttpRequestMessageBuilder(url, HttpMethod.Get);

            if (time.HasValue)
            {
                httpRequestMessageBuilder.AddQueryString("time", time.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (icao24 != null)
            {
                httpRequestMessageBuilder.AddQueryString("icao24", icao24);
            }

            if (serials != null)
            {
                foreach (var serial in serials)
                {
                    httpRequestMessageBuilder.AddQueryString("serials", serial.ToString(CultureInfo.InvariantCulture));
                }
            }

            if (boundingBox != null)
            {
                httpRequestMessageBuilder.AddQueryString("lamin", boundingBox.LaMin.ToString(CultureInfo.InvariantCulture));
                httpRequestMessageBuilder.AddQueryString("lomin", boundingBox.LoMin.ToString(CultureInfo.InvariantCulture));
                httpRequestMessageBuilder.AddQueryString("lamax", boundingBox.LaMax.ToString(CultureInfo.InvariantCulture));
                httpRequestMessageBuilder.AddQueryString("lomax", boundingBox.LoMax.ToString(CultureInfo.InvariantCulture));
            }

            var httpRequestMessage = httpRequestMessageBuilder.Build();

            if (credentials != null)
            {
                SetBasicAuthHeader(httpRequestMessage, credentials);
            }

            var httpResponse = await httpClient
                .SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken)
                .ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var statusCode = httpResponse.StatusCode;
                var reason = httpResponse.ReasonPhrase;

                throw new Exception($"API Request failed with Status Code {statusCode} and Reason {reason}. For additional information, see the HttpResponseMessage in this Exception.");
            }

            var json = await httpResponse.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            var stateVectorResponse = StateVectorResponseParser.Parse(json);

            return stateVectorResponse;
        }

        public async Task<FlightResponse> GetAllFlightsBetweenAsync(DateTime begin, DateTime end, Credentials credentials = null, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            var beginUnixTs = DateTimeUtils.GetUnixTimestamp(begin);
            var endUnixTs = DateTimeUtils.GetUnixTimestamp(end);

            var url = $"{apiUrl}/flights/all";

            var httpRequestMessageBuilder = new HttpRequestMessageBuilder(url, HttpMethod.Get)
                .AddQueryString("begin", beginUnixTs.ToString(CultureInfo.InvariantCulture))
                .AddQueryString("end", endUnixTs.ToString(CultureInfo.InvariantCulture));

            var httpRequestMessage = httpRequestMessageBuilder.Build();

            if (credentials != null)
            {
                SetBasicAuthHeader(httpRequestMessage, credentials);
            }

            var httpResponse = await httpClient
                .SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken)
                .ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var statusCode = httpResponse.StatusCode;
                var reason = httpResponse.ReasonPhrase;

                throw new Exception($"API Request failed with Status Code {statusCode} and Reason {reason}. For additional information, see the HttpResponseMessage in this Exception.");
            }

            var json = await httpResponse.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            return FlightResponseParser.Parse(json);
        }

        public async Task<FlightResponse> GetFlightsByAircraftAsync(string icao24, DateTime begin, DateTime end, Credentials credentials = null, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            var beginUnixTs = DateTimeUtils.GetUnixTimestamp(begin);
            var endUnixTs = DateTimeUtils.GetUnixTimestamp(end);

            var url = $"{apiUrl}/flights/aircraft";

            var httpRequestMessageBuilder = new HttpRequestMessageBuilder(url, HttpMethod.Get)
                .AddQueryString("icao24", icao24)
                .AddQueryString("begin", beginUnixTs.ToString(CultureInfo.InvariantCulture))
                .AddQueryString("end", endUnixTs.ToString(CultureInfo.InvariantCulture));

            var httpRequestMessage = httpRequestMessageBuilder.Build();

            if (credentials != null)
            {
                SetBasicAuthHeader(httpRequestMessage, credentials);
            }

            var httpResponse = await httpClient
                .SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken)
                .ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var statusCode = httpResponse.StatusCode;
                var reason = httpResponse.ReasonPhrase;

                throw new Exception($"API Request failed with Status Code {statusCode} and Reason {reason}. For additional information, see the HttpResponseMessage in this Exception.");
            }

            var json = await httpResponse.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            return FlightResponseParser.Parse(json);
        }

        public async Task<FlightResponse> GetArrivalsByAirportAsync(string airport, DateTime begin, DateTime end, Credentials credentials = null, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            var beginUnixTs = DateTimeUtils.GetUnixTimestamp(begin);
            var endUnixTs = DateTimeUtils.GetUnixTimestamp(end);

            var url = $"{apiUrl}/flights/arrival";

            var httpRequestMessageBuilder = new HttpRequestMessageBuilder(url, HttpMethod.Get)
                .AddQueryString("airport", airport)
                .AddQueryString("begin", beginUnixTs.ToString(CultureInfo.InvariantCulture))
                .AddQueryString("end", endUnixTs.ToString(CultureInfo.InvariantCulture));

            var httpRequestMessage = httpRequestMessageBuilder.Build();

            if (credentials != null)
            {
                SetBasicAuthHeader(httpRequestMessage, credentials);
            }

            var httpResponse = await httpClient
                .SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken)
                .ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var statusCode = httpResponse.StatusCode;
                var reason = httpResponse.ReasonPhrase;

                throw new Exception($"API Request failed with Status Code {statusCode} and Reason {reason}. For additional information, see the HttpResponseMessage in this Exception.");
            }

            var json = await httpResponse.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            return FlightResponseParser.Parse(json);
        }

        public async Task<FlightResponse> GetDeparturesByAirportAsync(Credentials credentials, string airport, DateTime begin, DateTime end, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            var beginUnixTs = DateTimeUtils.GetUnixTimestamp(begin);
            var endUnixTs = DateTimeUtils.GetUnixTimestamp(end);

            var url = $"{apiUrl}/flights/departure";

            var httpRequestMessageBuilder = new HttpRequestMessageBuilder(url, HttpMethod.Get)
                .AddQueryString("airport", airport)
                .AddQueryString("begin", beginUnixTs.ToString(CultureInfo.InvariantCulture))
                .AddQueryString("end", endUnixTs.ToString(CultureInfo.InvariantCulture));

            var httpRequestMessage = httpRequestMessageBuilder.Build();

            if (credentials != null)
            {
                SetBasicAuthHeader(httpRequestMessage, credentials);
            }

            var httpResponse = await httpClient
                .SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken)
                .ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var statusCode = httpResponse.StatusCode;
                var reason = httpResponse.ReasonPhrase;

                throw new Exception($"API Request failed with Status Code {statusCode} and Reason {reason}. For additional information, see the HttpResponseMessage in this Exception.");
            }

            var json = await httpResponse.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            return FlightResponseParser.Parse(json);
        }

        private void SetBasicAuthHeader(HttpRequestMessage httpRequestMessage, Credentials credentials)
        {
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", GetCredentialsAsBase64(credentials));
        }

        private string GetCredentialsAsBase64(Credentials credentials)
        {
            var bytes = Encoding.UTF8.GetBytes($"{credentials.Username}:{credentials.Password}");

            return Convert.ToBase64String(bytes);
        }

    }
}
