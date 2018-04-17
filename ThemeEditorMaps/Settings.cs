using ColossalFramework.IO;
using System;
using System.IO;
using System.Xml.Serialization;

namespace ThemeEditorMaps
{
    [XmlRoot("ThemeEditorMapsSettings")]
    public class ThemeEditorMapsSettings
    {
        [XmlIgnore]
        private static readonly string configurationPath = Path.Combine(DataLocation.localApplicationData, "ThemeEditorMapsSettings.xml");

        public string SelectedOption = "Foggy Hills";

        public static string ConfigurationPath
        {
            get
            {
                return configurationPath;
            }
        }

        public ThemeEditorMapsSettings() { }

        public void OnPreSerialize() { }

        public void OnPostDeserialize() { }

        public void Save()
        {
            var fileName = ConfigurationPath;
            var config = ThemeEditorMaps.Settings;
            var serializer = new XmlSerializer(typeof(ThemeEditorMapsSettings));
            using (var writer = new StreamWriter(fileName))
            {
                config.OnPreSerialize();
                serializer.Serialize(writer, config);
            }
        }


        public static ThemeEditorMapsSettings Load()
        {
            var fileName = ConfigurationPath;
            var serializer = new XmlSerializer(typeof(ThemeEditorMapsSettings));
            try
            {
                using (var reader = new StreamReader(fileName))
                {
                    var config = serializer.Deserialize(reader) as ThemeEditorMapsSettings;
                    return config;
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log($"Error Parsing {fileName}: {ex}");
                return null;
            }
        }
    }
}
