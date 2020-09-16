using System;
using System.Net.Http;

namespace HayTeknoloji.BigBlueButton.Tests.Utils
{
    public sealed class DefaultHttpClientFactory : IHttpClientFactory
    {
        private static readonly Lazy<HttpClient> HttpClientLazy =
            new Lazy<HttpClient>(() => new HttpClient());

        // NOTE: This will always return the same HttpClient instance.
        public HttpClient CreateClient(string name) => HttpClientLazy.Value;
    }
}