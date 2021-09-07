using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlMultilanguage
{
    public static class Test
    {
        public static dynamic Lang = new ExpandoObject();
        public static void Add()
        {
            var exDict = Lang as IDictionary<string, object>;
            if (exDict.ContainsKey("test"))
                exDict["test"] = "Dynamic test";
            else
                exDict.Add("test", "Dynamic test");
        }

    }
}
