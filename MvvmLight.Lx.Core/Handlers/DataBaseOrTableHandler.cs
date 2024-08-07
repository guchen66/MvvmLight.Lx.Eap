using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Handlers
{
    public class DataBaseOrTableHandler
    {
        private static GeneratorData _jsonData;
      /*  private static GeneratorData GetJsonData()
        {
            if (_jsonData == null)
            {
                var provider = new JsonConfigProvider()
                {
                    FileName = "AppConfig.json"
                };

                _jsonData = provider.Load<GeneratorData>("GeneratorData");
            }

            return _jsonData;
        }*/
      /*  public static bool IsGenerated
        {
            get => GetJsonData().IsGenerator;
        }

        public static bool IsSeedData
        {
            get => GetJsonData().IsSeedData;
        }*/
        public static bool IsCreated => CreateTable();

        private static bool CreateTable()
        {
            var json = File.ReadAllText("AppConfig.json");
            var jsonObject = JsonConvert.DeserializeObject<GeneratorData>(json);
            bool value = jsonObject.IsGenerator;
            return value;
        }
    }

    public class GeneratorData
    {
        public string Title { get; set; }
        public bool IsGenerator { get; set; }
        public bool IsSeedData { get; set; }
    }
}
