using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TakeAsh {

    /// <summary>
    /// Add extension methods (Import, Export, FromXml, ToXml, Clone)
    /// </summary>
    public interface IXmlHelper { }

    public static class XmlHelperExtensionMethods {

        /// <summary>
        /// Import object as T from file
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="obj">object (not used)</param>
        /// <param name="fileName">file name</param>
        /// <returns>T type object</returns>
        /// <remarks>
        /// How to call: (null as T).Import(fileName)
        /// </remarks>
        static public T Import<T>(this T obj, string fileName)
            where T : IXmlHelper {

            return XmlHelper<T>.importFile(fileName);
        }

        /// <summary>
        /// Export object to file
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="obj">object</param>
        /// <param name="fileName">file name</param>
        /// <returns>
        /// <list type="table">
        /// <item><term>true</term><description>success</description></item>
        /// <item><term>false</term><description>fail</description></item>
        /// </list>
        /// </returns>
        static public bool Export<T>(this T obj, string fileName)
            where T : IXmlHelper {

            return XmlHelper<T>.exportFile(fileName, obj);
        }

        /// <summary>
        /// Convert text to object as T
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="obj">object (not used)</param>
        /// <param name="text">XML text</param>
        /// <returns>T type object</returns>
        /// <remarks>
        /// How to call: (null as T).FromXml(text)
        /// </remarks>
        static public T FromXml<T>(this T obj, string text)
            where T : IXmlHelper {

            return XmlHelper<T>.convertFromString(text);
        }

        /// <summary>
        /// Convert object to XML text
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="obj">object</param>
        /// <returns>XML text</returns>
        static public string ToXml<T>(this T obj)
            where T : IXmlHelper {

            return XmlHelper<T>.convertToString(obj);
        }

        /// <summary>
        /// Create clone (deep copy, except the properties that have XmlIgnore attribute)
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="obj">object</param>
        /// <returns>clone of object</returns>
        static public T Clone<T>(this T obj)
            where T : IXmlHelper {

            return obj != null ?
                obj.FromXml(obj.ToXml()) :
                default(T);
        }

        public static T ReadElement<T>(this XmlReader reader)
            where T : IXmlHelper {

            return XmlHelper<T>.readElement(reader);
        }

        public static void WriteElement<T>(this XmlWriter writer, T value)
            where T : IXmlHelper {

            XmlHelper<T>.writeElement(writer, value);
        }
    }
}
