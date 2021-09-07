using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XmlMultilanguage.MultiLanguage
{
    public static class Lang
    {
        private static dynamic _keys = new CustomDynamicObject();
        public static dynamic Keys
        {
            get
            {
                var keys = _keys as CustomDynamicObject;
                if (keys.DynamicProperties.Count == 0)
                    LoadKeys();
                return _keys;
            }
        }

        private static string GetLanguageFileName()
        {
            return "MultiLanguage\\Languages\\" + Application.CurrentCulture.Name + ".xml";
        }

        private static void InserNewKeyToOtherLanguages(string keyName)
        {
            var languageFiles = Directory.GetFiles("MultiLanguage\\Languages\\").Where(prop=>prop != GetLanguageFileName()).ToArray();
            foreach (var langFile in languageFiles)
            {
                XDocument doc = XDocument.Load(langFile);
                XElement root = new XElement(keyName);
                root.Add(new XElement(keyName, keyName));
                doc.Element("language").Add(root);
                doc.Save(langFile);
            }
        }

        private static void SaveKeys()
        {
            var keys = _keys as CustomDynamicObject;

            using (XmlWriter writer = XmlWriter.Create(GetLanguageFileName()))
            {
                writer.WriteStartElement("language");
                foreach (var item in keys.DynamicProperties)
                {
                    writer.WriteElementString(item.Key, item.Value.ToString());
                }
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        public static void LoadKeys()
        {
            var keys = _keys as CustomDynamicObject;
            string fileName = GetLanguageFileName();

            if (!File.Exists(fileName))
                throw new Exception("There is no Language file for:" + fileName);

            if (keys.DynamicProperties.Count != 0) keys.DynamicProperties.Clear();
            

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            var keysElements = xmlDoc.GetElementsByTagName("language")[0].ChildNodes;
            foreach (XmlNode item in keysElements)
            {
                if (keys.DynamicProperties.ContainsKey(item.Name))
                    keys.DynamicProperties[item.Name] = item.InnerText;
                else
                    keys.DynamicProperties.Add(item.Name, item.InnerText);
            }
        }

        public static bool UpdateKey(string keyName, string newValue)
        {
            var keys = _keys as CustomDynamicObject;

            if (!keys.DynamicProperties.ContainsKey(keyName))
                return false;

            keys.DynamicProperties[keyName] = newValue;
            SaveKeys();
            return true;
        }

        public static bool InsertKey(string keyName, string keyValue)
        {
            var keys = _keys as CustomDynamicObject;

            if (keys.DynamicProperties.ContainsKey(keyName))
                return false;

            keys.DynamicProperties.Add(keyName, keyValue);
            SaveKeys();

            InserNewKeyToOtherLanguages(keyName);

            return true;
        }

    }
}
