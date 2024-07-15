using GECA_Control.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GECA_Control.Services
{
    public class ApplicationService : JsonConverter<Coordinates[,]>
    {
        public override Coordinates[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Deserialization of Coordinates[,] is not implemented.");
        }
        public static string SerializeCoordinatesArray(Coordinates[,] multiArray)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ApplicationService());

            return JsonSerializer.Serialize(multiArray, options);
        }
        public static Coordinates[,] DeserializeCoordinatesArray(string json)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ApplicationService());

            return JsonSerializer.Deserialize<Coordinates[,]>(json, options);
        }
        public override void Write(Utf8JsonWriter writer, Coordinates[,] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            int rows = value.GetLength(0);
            int cols = value.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                writer.WriteStartArray();

                for (int j = 0; j < cols; j++)
                {
                    JsonSerializer.Serialize(writer, value[i, j], options);
                }

                writer.WriteEndArray();
            }

            writer.WriteEndArray();
        }
    }
}
