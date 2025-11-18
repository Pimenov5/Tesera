using System.Text.Json;
using Tesera.Types;

namespace Tesera
{
	public  class TeseraClient
	{
		readonly HttpClient _httpClient;
		readonly JsonSerializerOptions _jsonSerializerOptions = new() { IncludeFields = true, PropertyNameCaseInsensitive = true };

		private static TeseraClient? s_instance;
		public static TeseraClient Instance { get => s_instance ?? throw new NullReferenceException("Не был задан экземпляр клиента Tesera.ru"); set { s_instance = value; } }

		public TeseraClient(HttpClient? httpClient) => _httpClient = httpClient ?? new();
		public TeseraClient() : this(null) { }

		public async Task<T?> GetAsync<T>(string requestUri, JsonSerializerOptions? jsonSerializerOptions = null)
		{
			using Stream stream = await _httpClient.GetStreamAsync(requestUri);
			return JsonSerializer.Deserialize<T>(stream, jsonSerializerOptions ?? _jsonSerializerOptions);
		}

		public async Task<T?> GetAsync<T>(UriGetRequest<T> uriGetRequest) => await this.GetAsync<T>(uriGetRequest.GetUri());
	}
}