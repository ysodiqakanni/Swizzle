using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Swizzle.DTOs.Responses;

namespace Swizzle.Services
{

    // Services/IHttpClientService.cs
    public interface IHttpClientService
    {
        Task<BaseApiResponse<T>> GetAsync<T>(string endpoint);
        Task<BaseApiResponse<T>> PostAsync<T>(string endpoint, object data);
        Task<BaseApiResponse<T>> PutAsync<T>(string endpoint, object data);
        Task<BaseApiResponse<T>> DeleteAsync<T>(string endpoint);
    }

    // Services/HttpClientService.cs
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HttpClientService> _logger;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public HttpClientService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<HttpClientService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;

            _baseUrl = "http://localhost:8090/api/v1/"; // _configuration["ApiSettings:BaseUrl"];
            _apiKey = "";   // _configuration["ApiSettings:ApiKey"];

            ConfigureHttpClient();
        }

        private void ConfigureHttpClient()
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(_apiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("X-API-Key", _apiKey);
            }
        }

        public async Task<BaseApiResponse<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during GET request to {Endpoint}", endpoint);
                return CreateErrorResponse<T>(ex.Message);
            }
        }

        public async Task<BaseApiResponse<T>> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(data),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PostAsync(endpoint, content);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during POST request to {Endpoint}", endpoint);
                return CreateErrorResponse<T>(ex.Message);
            }
        }

        public async Task<BaseApiResponse<T>> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(data),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.PutAsync(endpoint, content);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during PUT request to {Endpoint}", endpoint);
                return CreateErrorResponse<T>(ex.Message);
            }
        }

        public async Task<BaseApiResponse<T>> DeleteAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during DELETE request to {Endpoint}", endpoint);
                return CreateErrorResponse<T>(ex.Message);
            }
        }

        private async Task<BaseApiResponse<T>> HandleResponseOld<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var data1 = JsonSerializer.Deserialize<BaseApiResponse<T>>(content);
                    var data = JsonSerializer.Deserialize<T>(content);
                    return new BaseApiResponse<T>
                    {
                        Success = true,
                        Data = data,
                        Message = "Request successful"
                    };
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Error deserializing response");
                    return CreateErrorResponse<T>("Error processing response data");
                }
            }

            _logger.LogWarning("API request failed with status code {StatusCode}: {Content}",
                response.StatusCode, content);

            return new BaseApiResponse<T>
            {
                Success = false,
                Message = $"API request failed with status code {response.StatusCode}",
                Errors = TryParseErrors(content)
            };
        }

        private async Task<BaseApiResponse<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Raw API Response: {Content}", content);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = false, // Changed to false since we want exact matching
                        PropertyNamingPolicy = null // Removed camelCase policy to maintain exact casing
                    };

                    var result = JsonSerializer.Deserialize<BaseApiResponse<T>>(content, options);
                    if (result != null)
                    {
                        return result;
                    }

                    throw new JsonException("Deserialization resulted in null object");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deserializing response: {Content}", content);
                    return new BaseApiResponse<T>
                    {
                        Success = false,
                        Message = "Error processing response data",
                        Errors = new Dictionary<string, string[]>
                {
                    { "Deserialization", new[] { ex.Message } }
                }
                    };
                }
            }

            _logger.LogWarning("API request failed with status code {StatusCode}: {Content}",
                response.StatusCode, content);

            return new BaseApiResponse<T>
            {
                Success = false,
                Message = $"API request failed with status code {response.StatusCode}",
                Errors = TryParseErrors(content)
            };
        }

        private Dictionary<string, string[]> TryParseErrors(string content)
        {
            try
            {
                var error = JsonSerializer.Deserialize<Dictionary<string, string[]>>(content);
                return error ?? new Dictionary<string, string[]>();
            }
            catch
            {
                return new Dictionary<string, string[]>
            {
                { "General", new[] { content } }
            };
            }
        }

        private BaseApiResponse<T> CreateErrorResponse<T>(string message)
        {
            return new BaseApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = new Dictionary<string, string[]>
            {
                { "General", new[] { message } }
            }
            };
        }
    }

}
