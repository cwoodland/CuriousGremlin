﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using CuriousGremlin.Objects;

namespace CuriousGremlin
{
    public abstract class GraphQuery
    {
        public enum RepeatTypeEnum { DoWhile, WhileDo };

        protected static string Sanitize(string input)
        {
            if (input is null)
                throw new ArgumentNullException("Input cannot be null");
            return input.Replace("'", @"\'");
        }

        internal static string GetObjectString(object item)
        {
            if (item is null)
                throw new ArgumentNullException("Provided object is null");
            string prop_type = item.GetType().ToString().ToLower();
            switch (prop_type)
            {
                case "system.string":
                    return "'" + Sanitize(item as string) + "'";
                case "system.boolean":
                    return (bool)item ? "true" : "false";
                case "system.single":
                    return ((float)item).ToString();
                case "system.double":
                    return ((double)item).ToString();
                case "system.decimal":
                    return ((decimal)item).ToString();
                case "system.int32":
                    return ((int)item).ToString();
                case "system.int64":
                    return ((long)item).ToString();
                case "system.datetime":
                    return "'" + Sanitize((item as DateTime?).Value.ToString("s")) + "'";
                default:
                    return GetObjectString(item.ToString());
            }
        }

        protected static string SeralizeProperties(Dictionary<string,object> properties)
        {
            List<string> outputs = new List<string>();
            foreach (var property in properties)
            {
                if (property.Value is null)
                    continue;
                outputs.Add("'" + Sanitize(property.Key) + "', " + GetObjectString(property.Value));
            }
            return string.Join(",", outputs);
        }
    }
}
