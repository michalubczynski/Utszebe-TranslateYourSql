using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Utszebe.Infrastracture.Services
{
    public class Request
    {
        public Request(string prompt)
        {
            Body = new Dictionary<string, object>()
            {
                { "prompt", prompt },
                { "max_new_tokens", 250 },
                { "preset", "None" },
                { "do_sample", true },
                { "temperature", 0.7 },
                { "top_p", 0.1 },
                { "typical_p", 1 },
                { "epsilon_cutoff", 0 },
                { "eta_cutoff", 0 },
                { "tfs", 1 },
                { "top_a", 0 },
                { "repetition_penalty", 1.18 },
                { "repetition_penalty_range", 0 },
                { "top_k", 40 },
                { "min_length", 0 },
                { "no_repeat_ngram_size", 0 },
                { "num_beams", 1 },
                { "penalty_alpha", 0 },
                { "length_penalty", 1 },
                { "early_stopping", false },
                { "mirostat_mode", 0 },
                { "mirostat_tau", 5 },
                { "mirostat_eta", 0.1 },
                { "seed", -1 },
                { "add_bos_token", true },
                { "truncation_length", 2048 },
                { "ban_eos_token", false },
                { "skip_special_tokens", true },
                { "stopping_strings", new List<string>() }
            };
        }
        Dictionary<string, object> Body { get; set; }

        public ArraySegment<byte> AsByteArray()
        {
            var serializedRequest = JsonSerializer.Serialize(Body);
            return new ArraySegment<byte>(Encoding.UTF8.GetBytes(serializedRequest));
        }
    }
}
