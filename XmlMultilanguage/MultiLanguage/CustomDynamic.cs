using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlMultilanguage
{
    public class CustomDynamicObject : DynamicObject
    {
        public Dictionary<string, object> DynamicProperties = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            DynamicProperties.Add(binder.Name, value);
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (DynamicProperties.ContainsKey(binder.Name))
            {
                result = DynamicProperties[binder.Name].ToString(); 
                return true;
            }
            else
            {
                result = binder.Name;
                return true;
            }
                
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var property in DynamicProperties)
            {
                sb.AppendLine($"Property '{property.Key}' = '{property.Value}'");
            }

            return sb.ToString();
        }
    }
}
