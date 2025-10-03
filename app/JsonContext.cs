using System.Text.Json.Serialization;

namespace Nightmare;

[JsonSerializable(typeof(Collection))]
[JsonSerializable(typeof(Profile))]
[JsonSerializable(typeof(RequestOrFolder))]
[JsonSerializable(typeof(Dictionary<string, Profile>))]
[JsonSerializable(typeof(Dictionary<string, RequestOrFolder>))]
[JsonSerializable(typeof(Dictionary<string, object>))]
[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
internal partial class JsonContext : JsonSerializerContext
{
}