using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ApiTlumaczen
{
    private HttpClient klientHttp = new HttpClient();
    private string kluczApi = "ff67208c-07be-4773-ae2a-4634278d7c3c:fx";

    public ApiTlumaczen()
    {
        klientHttp.DefaultRequestHeaders.Add("Authorization", $"DeepL-Auth-Key {kluczApi}");
    }

    public async Task<string> PrzetlumaczTekst(string tekstDoTlumaczenia, string jezykZrodlowy, string jezykDocelowy)
    {
        var requestBody = new
        {
            text = new[] { tekstDoTlumaczenia },
            source_lang = jezykZrodlowy.ToUpper(),
            target_lang = jezykDocelowy.ToUpper()
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await klientHttp.PostAsync("https://api-free.deepl.com/v2/translate", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var translationResult = JsonConvert.DeserializeObject<TranslationResult>(responseBody);
            return translationResult.translations[0].text;
        }
        else
        {
            throw new Exception("Błąd podczas tłumaczenia tekstu: " + responseBody);
        }
    }

    class TranslationResult
    {
        public Translation[] translations { get; set; }
    }

    class Translation
    {
        public string text { get; set; }
    }

}
