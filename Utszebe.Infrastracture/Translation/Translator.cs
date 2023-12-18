using Newtonsoft.Json;
using Utszebe.Core.Entities.TranslationMembers;
using Utszebe.Core.Interfaces;
using static Utszebe.Infrastructure.Translation.ResponseEnum;

namespace Utszebe.Infrastracture.Translation
{
    public class Translator : ITranslator
    {
        private const string API_URL = "http://api.mymemory.translated.net/get?q=";
        private HttpClient _client;
        public Translator()
        {
            _client = new HttpClient();
        }
        public async Task<string> Translate(string text)
        {
            var translatedText = string.Empty;
            var sourceLang = "pl";
            var targetLang = "en";

            if (string.IsNullOrEmpty(text))
                return translatedText;

            try
            {
                translatedText = await TranslateAsync(text, sourceLang, targetLang);
            }
            catch
            {
                throw new Exception("Error translating text");
            }
            return translatedText;
        }
        private async Task<string> TranslateAsync(string text, string sourceLang, string targetLang)
        {
            var url = $"{API_URL}{Uri.EscapeDataString(text)}&langpair={sourceLang}|{targetLang}";

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var translationResult = JsonConvert.DeserializeObject<TranslationResponse>(responseJson);

            if (translationResult.ResponseStatus == Convert.ToInt32(Response.Success))
            {
                return translationResult.TranslatedText;
            }

            return string.Empty;
        }
    }
}
