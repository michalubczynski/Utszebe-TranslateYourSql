using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utszebe.Core.Entities.TranslationMembers
{
    public class TranslationData
    {
        [JsonProperty("translatedText")]
        public string TranslatedText { get; set; } = string.Empty;
    }
}
