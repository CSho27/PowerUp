using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.Algolia
{
    public class AlgoliaSearchResults<T>
    {
        [JsonPropertyName("results")]
        public AlgoliaSearchResult<T>[] Results { get; set; } = [];
    }

    public class AlgoliaSearchResult<T>
    {
        [JsonPropertyName("hits")]
        public T[] Hits { get; set; } = [];

        [JsonPropertyName("nbHits")]
        public long NbHits { get; set; }

        [JsonPropertyName("page")]
        public long Page { get; set; }

        [JsonPropertyName("nbPages")]
        public long NbPages { get; set; }

        [JsonPropertyName("hitsPerPage")]
        public long HitsPerPage { get; set; }

        [JsonPropertyName("exhaustiveNbHits")]
        public bool ExhaustiveNbHits { get; set; }

        [JsonPropertyName("exhaustiveTypo")]
        public bool ExhaustiveTypo { get; set; }

        [JsonPropertyName("exhaustive")]
        public Exhaustive? Exhaustive { get; set; }

        [JsonPropertyName("query")]
        public string? Query { get; set; }

        [JsonPropertyName("params")]
        public string? Params { get; set; }

        [JsonPropertyName("index")]
        public string? Index { get; set; }

        [JsonPropertyName("processingTimeMS")]
        public long ProcessingTimeMs { get; set; }

        [JsonPropertyName("processingTimingsMS")]
        public ProcessingTimingsMs? ProcessingTimingsMs { get; set; }

        [JsonPropertyName("serverTimeMS")]
        public long ServerTimeMs { get; set; }
    }

    public class Exhaustive
    {
        [JsonPropertyName("nbHits")]
        public bool NbHits { get; set; }

        [JsonPropertyName("typo")]
        public bool Typo { get; set; }
    }

    public class HighlightResult
    {
        [JsonPropertyName("title")]
        public Title? Title { get; set; }
    }

    public class Title
    {
        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("matchLevel")]
        public string? MatchLevel { get; set; }

        [JsonPropertyName("fullyHighlighted")]
        public bool FullyHighlighted { get; set; }

        [JsonPropertyName("matchedWords")]
        public string[] MatchedWords { get; set; } = [];
    }

    public partial class ProspectBio
    {
        [JsonPropertyName("contentTitle")]
        public string? ContentTitle { get; set; }

        [JsonPropertyName("contentText")]
        public string? ContentText { get; set; }
    }

    public partial class ProcessingTimingsMs
    {
        [JsonPropertyName("_request")]
        public Request? Request { get; set; }

        [JsonPropertyName("getIdx")]
        public GetIdx? GetIdx { get; set; }

        [JsonPropertyName("total")]
        public long Total { get; set; }
    }

    public partial class GetIdx
    {
        [JsonPropertyName("load")]
        public Load? Load { get; set; }

        [JsonPropertyName("total")]
        public long Total { get; set; }
    }

    public partial class Load
    {
        [JsonPropertyName("total")]
        public long Total { get; set; }
    }

    public partial class Request
    {
        [JsonPropertyName("roundTrip")]
        public long RoundTrip { get; set; }
    }
}
