using Microsoft.AspNetCore.Components;

namespace BrightInvest.Web.Services
{
	public class CustomHttpClientService
	{
		private readonly HttpClient _httpClient;

		public CustomHttpClientService(IConfiguration configuration, IHostEnvironment env)
		{
			string baseUrl = env.IsDevelopment()
				? "http://localhost:5074/"  
				: configuration["ApiBaseUrl"];

			_httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
		}

		public HttpClient GetHttpClient() => _httpClient;
	}
}
