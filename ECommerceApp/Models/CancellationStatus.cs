using System.Text.Json.Serialization;

namespace ECommerceApp
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CancellationStatus
    {
        Pending = 1,
        Approved = 8,
        Rejected = 9
    }
}