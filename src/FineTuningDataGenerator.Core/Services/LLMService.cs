using System.Text;
using System.Text.Json;
using FineTuningDataGenerator.Core.Models;
using Microsoft.Extensions.Logging;

namespace FineTuningDataGenerator.Core.Services;

/// <summary>
/// Service für die Kommunikation mit OpenAI-kompatiblen LLMs
/// </summary>
public class LLMService : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly LLMConfig _config;
    private readonly ILogger<LLMService>? _logger;    public LLMService(LLMConfig config)
    {
        _config = config;
        _logger = null; // Keep logger for future enhancement
        _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(_config.RequestTimeout)
        };
        
        if (!string.IsNullOrEmpty(_config.ApiKey))
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_config.ApiKey}");
        }
    }/// <summary>
    /// Sendet eine Chat-Anfrage an das LLM
    /// </summary>
    public async Task<string> ChatCompletionAsync(List<Message> messages)
    {        // Log the input messages
        if (_logger != null)
        {
            _logger.LogInformation("🤖 LLM Chat Anfrage an Model: {Model}", _config.Model);
            foreach (var message in messages)
            {
                var truncatedContent = message.Content.Length > 500 
                    ? message.Content.Substring(0, 500) + "..." 
                    : message.Content;
                _logger.LogInformation("📝 {Role}: {Content}", message.Role.ToUpper(), truncatedContent);
            }
        }
        else
        {
            // Fallback to Console logging
            Console.WriteLine($"🤖 LLM Chat Anfrage an Model: {_config.Model}");
            foreach (var message in messages)
            {
                var truncatedContent = message.Content.Length > 500 
                    ? message.Content.Substring(0, 500) + "..." 
                    : message.Content;
                Console.WriteLine($"📝 {message.Role.ToUpper()}: {truncatedContent}");
            }
        }

        var requestBody = new
        {
            model = _config.Model,
            messages = messages.Select(m => new { role = m.Role, content = m.Content }).ToArray(),
            max_tokens = _config.MaxTokens,
            temperature = _config.Temperature
        };        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Construct endpoint URL, avoiding double /v1/ paths
        var baseUrl = _config.ApiHost.TrimEnd('/');
        var endpoint = baseUrl.EndsWith("/v1") 
            ? baseUrl + "/chat/completions"
            : baseUrl + "/v1/chat/completions";
            
        var response = await _httpClient.PostAsync(endpoint, content);        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            if (_logger != null)
                _logger.LogError("❌ LLM API Fehler: {StatusCode} - {Error}", response.StatusCode, errorContent);
            else
                Console.WriteLine($"❌ LLM API Fehler: {response.StatusCode} - {errorContent}");
            throw new Exception($"LLM API Error: {response.StatusCode} - {errorContent}");
        }

        var responseJson = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<JsonElement>(responseJson);
        
        var responseContent = responseObject
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? string.Empty;        // Log the response
        if (_logger != null)
        {
            var truncatedResponse = responseContent.Length > 500 
                ? responseContent.Substring(0, 500) + "..." 
                : responseContent;
            _logger.LogInformation("✅ LLM Antwort ({Length} Zeichen): {Response}", 
                responseContent.Length, truncatedResponse);
        }
        else
        {
            // Fallback to Console logging
            var truncatedResponse = responseContent.Length > 500 
                ? responseContent.Substring(0, 500) + "..." 
                : responseContent;
            Console.WriteLine($"✅ LLM Antwort ({responseContent.Length} Zeichen): {truncatedResponse}");
        }

        return responseContent;
    }

    /// <summary>
    /// Überprüft die Verbindung zum LLM
    /// </summary>
    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            var testMessages = new List<Message>
            {
                new() { Role = "system", Content = "Sie sind ein hilfreicher Assistent." },
                new() { Role = "user", Content = "Hallo, können Sie mich hören?" }
            };

            var response = await ChatCompletionAsync(testMessages);
            return !string.IsNullOrEmpty(response);
        }
        catch
        {
            return false;
        }
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
