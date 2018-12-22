using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cms.Service.Helpers
{
   internal class UtilsServiice
    {
        public static string FormataModalidade(int SomaModalidade)
        {
            string str;
            if (SomaModalidade < 60)
                str = $"{SomaModalidade} minutos";
            else if (SomaModalidade == 60)
                str = $"1 hora";
            else
                str = $"{SomaModalidade / 60} horas e {SomaModalidade % 60} minutos";

            return str;
        }

        public static T JsonConvertObject<T>(String jsonString)
        {
             return JsonConvert.DeserializeObject<T>(jsonString,
                       new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss" });
               
        }
    }
}