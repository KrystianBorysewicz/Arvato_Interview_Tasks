namespace System.Text.Json
{
    using System.Collections.Generic;

    internal static class JsonElementExtensions
    {
        /// <summary>
        /// Safely gets a property from a <see cref="JsonElement"/>.
        /// </summary>
        /// <param name="jsonElement"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static JsonElement SafeGetProperty(this JsonElement jsonElement, string propertyName)
        {
            try
            {
                return jsonElement.GetProperty(propertyName);
            }
            catch (KeyNotFoundException e)
            {
                var exception = new JsonException($"Couldn't retrieve {propertyName} property from json", e);
                exception.Data["jsonElement"] = jsonElement.ToString();
                exception.Data["propertyName"] = propertyName;
                throw exception;
            }
        }
    }
}