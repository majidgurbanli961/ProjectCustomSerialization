using PersonalProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PersonalProject.Helper
{
    public class HelperSrlz
    {
        private static string helperRecursion(PropertyInfo propertiInfo, Type _type, object graph,string result)

        {
            if (propertiInfo.PropertyType.IsClass
                   && propertiInfo.PropertyType.Assembly.FullName == _type.Assembly.FullName)
            {
                var test = propertiInfo.GetValue(graph);
                result += String.Format(@"""{0}"":", propertiInfo.Name);


                result += Serialize(propertiInfo.GetValue(graph), propertiInfo.GetValue(graph).GetType());
            }
            else
            {
                result += String.Format(@"""{0}"": ""{1}""", propertiInfo.Name, propertiInfo.GetValue(graph));

            }
            return result;

        }
        public static string Serialize(object graph, Type _type)
        {
            string result = "";

            List<PropertyInfo> properties = _type.GetProperties().ToList();


            result += "{";
            result += "\n";
            for (int i = 0; i < properties.Count; i++)
            {
                var propertiInfo = properties[i];

                if (i == (properties.Count - 1))
                {
                    
                    result = helperRecursion(propertiInfo, _type, graph, result);

                }
                else
                {
                    result = helperRecursion(propertiInfo, _type, graph, result);

                }
                result += "\n";

            }
            result += "}";
            return result;
        }


        public static object Deserialize(Type _type, string contents)
        {
            
            Object obj = Activator.CreateInstance(_type);
            object ttest = null;


            List<string> pairs = contents.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            string key, value;
            for (int i = 0; i < pairs.Count; i++)
            {
                var pair = pairs[i];
                if (pair.Contains(':'))
                {
                    string[] keyValue = pair.Split(':');
                    key = keyValue[0];
                    value = keyValue[1];
                    List<string> deleteKey = new List<string>()
                    {
                         @""""

                    };
                    key = clearString(key, deleteKey);
                    key = Regex.Replace(key, @"\t|\n|\r", "");

                    List<string> deleteValue = new List<string>()
                    {
                         ",",
                         @"""",
                         "'",
                          "‘",
                           "’"

                    };
                   
                    value = clearString(value, deleteValue);

                    

                    if (value.Contains('{'))
                    {
                        var newObj = newObjBuilder(pairs.GetRange(i + 1, pairs.Count - i - 1));
                       
                        var tempKey = key;
                        key = key.FirstLetterToUpperCaseOrConvertNullToEmptyString();
                        key += "Dto";
                        Type type = Type.GetType("PersonalProject.DTO." + key);
                        var lastIndex = firstIndex(pairs.GetRange(i, pairs.Count-i-1));
                         pairs.RemoveRange(4, lastIndex);
                        ttest = Deserialize(type, newObj);
                       
                        key = tempKey;

                    }

                    PropertyInfo propertyInfo = _type.GetProperty(key);
                    if (propertyInfo != null)
                    {
                        if (ttest == null)
                        {
                            int n;
                            var isNumeric = int.TryParse(value, out  n);
                            if (isNumeric)
                            {
                                propertyInfo.SetValue(obj, n, null);


                            }
                            else
                            {
                                propertyInfo.SetValue(obj, value, null);

                            }

                        }
                        else
                        {
                            propertyInfo.SetValue(obj, ttest, null);
                            ttest = null;
                        }
                    }



                }

            }

            return obj;
        }
        private static string newObjBuilder(List<string> pairs)
        {
            string result = "";
            foreach (var pair in pairs)
            {


                result += pair;
                result += "\n";
                if (pair.Contains('}'))
                {
                    break;
                }

            }
            return result;


        }
        private static string clearString(string val, List<string> whatClear)
        {
            foreach (var item in whatClear)
            {
                val = ClearFromStr(val, item);
            }
            val = val.Trim();
            return val;
        }
        private static int firstIndex(List<string> pairs)
        {
            for (int i = 0; i < pairs.Count; i++)
            {
                if (pairs[i].Contains('}'))
                {
                    return i;
                }
            }
            return -1;
        }
        private  static string ClearFromStr(string inp, string whatClear)
        {
            if (inp.Contains(whatClear))
            {
                inp = inp.Replace(whatClear, string.Empty);
            }
            return inp;
        }
    }
}
