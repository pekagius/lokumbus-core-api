namespace Lokumbus.CoreAPI.Helpers;

/// <summary>
/// Extension methods for serialization.
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// Serializes an object to its JSON representation.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object instance.</param>
    /// <returns>A JSON string representing the object.</returns>
    public static string ToJson<T>(this T obj)
    {
        return System.Text.Json.JsonSerializer.Serialize(obj);
    }
}