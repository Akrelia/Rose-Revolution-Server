using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RevolutionCore.Configurations
{
    /// <summary>
    /// Loads and saves a configuration from/to a JSON file.
    /// </summary>
    public static class ConfigurationLoader<T> where T : ServerConfiguration, new()
    {
        public static string fileName = "serverconfig";

        /// <summary>
        /// Initializes and loads the configuration.
        /// </summary>
        public static T Initialize()
        {
            string path = GetPath(fileName);

            if (File.Exists(path))
            {
                return Load();
            }

            else
            {
                var config = new T();

                Save(config);

                return config;
            }
        }

        /// <summary>
        /// Loads the configuration from the file.
        /// </summary>
        public static T Load()
        {
            var config = new T();

            #if DEBUG
                return config;
            #endif

            string path = GetPath(fileName);

            JObject json;

            if (File.Exists(path))
            {
                json = JObject.Parse(File.ReadAllText(path));
            }

            else
            {
                json = new JObject();
            }

            bool modified = false;

            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() == null).ToArray();

            foreach (FieldInfo field in fields)
            {
                JToken token = json[field.Name];

                if (token != null)
                {
                    try
                    {
                        field.SetValue(config, token.ToObject(field.FieldType));
                    }
                    catch
                    {
                        json[field.Name] = JToken.FromObject(field.GetValue(config));
                        modified = true;
                    }
                }

                else
                {
                    json[field.Name] = JToken.FromObject(field.GetValue(config));
                    modified = true;
                }
            }

            foreach (JProperty property in json.Properties().ToList())
            {
                bool exists = fields.Any(x => x.Name == property.Name);

                if (!exists)
                {
                    property.Remove();
                    modified = true;
                }
            }

            if (modified)
            {
                File.WriteAllText(path, json.ToString(Formatting.Indented));
            }

            return config;
        }

        /// <summary>
        /// Saves the current configuration to the file.
        /// </summary>
        public static void Save(T config)
        {
            string path = GetPath(fileName);

            JObject json = new JObject();

            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() == null);

            foreach (FieldInfo field in fields)
            {
                json[field.Name] = JToken.FromObject(field.GetValue(config));
            }

            File.WriteAllText(path, json.ToString(Formatting.Indented));
        }

        private static string GetPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = typeof(T).Name;

            if (!fileName.EndsWith(".json"))
                fileName += ".json";

            return Path.Combine(AppContext.BaseDirectory, fileName);
        }
    }
}