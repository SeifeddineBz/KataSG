using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeurBoisson.DAL
{
    public class JsonFileReader
    {
        public static JObject ReadJsonFile(string filePath)
        {
            using (StreamReader file = File.OpenText(filePath))
            {
                return JObject.Parse(file.ReadToEnd());
            }
        }
    }
}
