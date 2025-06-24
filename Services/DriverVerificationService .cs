using System.Text.Json.Serialization;

namespace RakbnyMa_aak.Services
{
    public class DriverVerificationService : IDriverVerificationService
    {
        private readonly HttpClient _httpClient;

        public DriverVerificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> MatchFaceAsync(string selfieUrl, string nationalIdUrl)
        {
            var requestPayload = new
            {
                image1 = selfieUrl,
                image2 = nationalIdUrl
            };

            var response = await _httpClient.PostAsJsonAsync("https://api-free-face-verify.com/compare", requestPayload);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Face verification API failed. Status: {response.StatusCode}, Response: {errorContent}");
            }

            var result = await response.Content.ReadFromJsonAsync<FaceVerificationResponse>();
            return result is not null && result.IsMatch && result.Confidence >= 80;
        }
    }

    public class FaceVerificationResponse
    {
        [JsonPropertyName("is_match")]
        public bool IsMatch { get; set; }

        // Optional: Add confidence or other fields if needed
        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }
    }
}
