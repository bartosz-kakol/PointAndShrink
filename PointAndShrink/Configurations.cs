using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PointAndShrink
{
    internal class Configurations
    {
        public class Configuration
        {
            public required PointerSettings.SizeVariant Size { get; set; }

            public Configuration Clone() => new()
            {
                Size = Size
            };
        }

        private static readonly Configuration defaultConfiguration = new()
        {
            Size = PointerSettings.SizeVariant.Default
        };

        private Dictionary<string, Configuration> cachedConfigurations;

        public Configurations()
        {
            var loadedConfigurations = Settings.Default.configurations;
            if (string.IsNullOrEmpty(loadedConfigurations))
            {
                loadedConfigurations = "{}";
            }

            cachedConfigurations = JsonSerializer.Deserialize<Dictionary<string, Configuration>>(loadedConfigurations)!;
        }

        public Configuration GetForDisplay(string deviceName) => cachedConfigurations.GetValueOrDefault(deviceName, defaultConfiguration).Clone();

        public void SetForDisplay(string deviceName, Configuration configuration)
        {
            cachedConfigurations[deviceName] = configuration.Clone();
            Settings.Default.configurations = JsonSerializer.Serialize(cachedConfigurations);
            Settings.Default.Save();
        }
    }
}
