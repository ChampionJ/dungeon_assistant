using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TakeAsh {

    /// <summary>
    /// Serialize Class containing Dictionary member
    /// http://stackoverflow.com/questions/495647/
    /// </summary>
    /// <typeparam name="TKey">Type of Key</typeparam>
    /// <typeparam name="TValue">Type of Value</typeparam>
    [XmlRoot("SerializableDictionary")]
    public class SerializableDictionary<TKey, TValue> :
        Dictionary<TKey, TValue>,
        IXmlSerializable,
        IXmlHelper {

        public enum SerializeTypes {
            Element_Element,        // Key:Element, Value:Element
            Element_Attribute,      // Key:Element, Value:Attribute
            Attribute_Element,      // Key:Attribute, Value:Element
            Attribute_Attribute,    // Key:Attribute, Value:Attribute
        };

        const string itemElementName = "item";
        const string keyTypeAttributeName = "key_type";
        const string keyValueAttributeName = "key";
        const string valueTypeAttributeName = "value_type";
        const string valueValueAttributeName = "value";
        static readonly Type keyType = typeof(TKey);
        static readonly Type valueType = typeof(TValue);
        static readonly string keyElementName = keyType.IsGenericType ?
                keyType.Name.Remove(keyType.Name.IndexOf('`')) :
                keyType.Name;
        static readonly string valueElementName = valueType.IsGenericType ?
                valueType.Name.Remove(valueType.Name.IndexOf('`')) :
                valueType.Name;

        static TypeConverter keyConverter = TypeDescriptor.GetConverter(typeof(TKey));
        static TypeConverter valueConverter = TypeDescriptor.GetConverter(typeof(TValue));

        static private SerializeTypes _serializeType;

        static public SerializeTypes SerializeType {
            get { return _serializeType; }
        }

        static SerializableDictionary() {
            bool keyCanStringify = keyConverter.CanConvertFrom(typeof(string))
                && keyConverter.CanConvertTo(typeof(string));
            bool valueCanStringify = valueConverter.CanConvertFrom(typeof(string))
                && valueConverter.CanConvertTo(typeof(string));
            _serializeType = (SerializeTypes)((keyCanStringify ? 2 : 0) + (valueCanStringify ? 1 : 0));
        }

        #region IXmlSerializable Members

        public XmlSchema GetSchema() {
            return null;
        }

        public void ReadXml(XmlReader reader) {
            var keyTypeName = reader.GetAttribute(keyTypeAttributeName);
            var valueTypeName = reader.GetAttribute(valueTypeAttributeName);
            if (keyTypeName != keyElementName || valueTypeName != valueElementName) {
                throw new XmlException("key_type/value_type mismatch");
            }

            switch (SerializeType) {
                case SerializeTypes.Element_Element:
                    read_Element_Element(reader);
                    break;
                case SerializeTypes.Element_Attribute:
                    read_Element_Attribute(reader);
                    break;
                case SerializeTypes.Attribute_Element:
                    read_Attribute_Element(reader);
                    break;
                case SerializeTypes.Attribute_Attribute:
                    read_Attribute_Attribute(reader);
                    break;
            }
        }

        public void WriteXml(XmlWriter writer) {
            writer.WriteAttributeString(keyTypeAttributeName, keyElementName);
            writer.WriteAttributeString(valueTypeAttributeName, valueElementName);

            switch (SerializeType) {
                case SerializeTypes.Element_Element:
                    write_Element_Element(writer);
                    break;
                case SerializeTypes.Element_Attribute:
                    write_Element_Attribute(writer);
                    break;
                case SerializeTypes.Attribute_Element:
                    write_Attribute_Element(writer);
                    break;
                case SerializeTypes.Attribute_Attribute:
                    write_Attribute_Attribute(writer);
                    break;
            }
        }

        #endregion

        private void read_Element_Element(XmlReader reader) {
            while (reader.Read()) {
                if (reader.NodeType != XmlNodeType.EndElement && reader.Name == itemElementName) {
                    XmlReader inner = reader.ReadSubtree();

                    inner.ReadToDescendant(keyElementName);
                    TKey key = XmlHelper<TKey>.readElement(inner);

                    TValue value = XmlHelper<TValue>.readElement(inner);

                    inner.Close();

                    this.Add(key, value);
                } else {
                    reader.Skip();
                    break;
                }
            }
        }

        private void write_Element_Element(XmlWriter writer) {
            foreach (TKey key in this.Keys) {
                writer.WriteStartElement(itemElementName);

                XmlHelper<TKey>.writeElement(writer, key);

                XmlHelper<TValue>.writeElement(writer, this[key]);

                writer.WriteEndElement();
            }
        }

        private void read_Element_Attribute(XmlReader reader) {
            while (reader.Read()) {
                if (reader.NodeType != XmlNodeType.EndElement && reader.Name == itemElementName) {
                    string strValue = reader.GetAttribute(valueValueAttributeName);
                    TValue value = strValue != null ?
                        (TValue)valueConverter.ConvertFromString(strValue) :
                        default(TValue);

                    XmlReader inner = reader.ReadSubtree();
                    inner.ReadToDescendant(keyElementName);
                    TKey key = XmlHelper<TKey>.readElement(inner);
                    inner.Close();

                    this.Add(key, value);
                } else {
                    reader.Skip();
                    break;
                }
            }
        }

        private void write_Element_Attribute(XmlWriter writer) {
            foreach (TKey key in this.Keys) {
                writer.WriteStartElement(itemElementName);

                writer.WriteAttributeString(valueValueAttributeName, this[key].ToString());

                XmlHelper<TKey>.writeElement(writer, key);

                writer.WriteEndElement();
            }
        }

        private void read_Attribute_Element(XmlReader reader) {
            while (reader.Read()) {
                if (reader.NodeType != XmlNodeType.EndElement && reader.Name == itemElementName) {
                    string strKey = reader.GetAttribute(keyValueAttributeName);
                    TKey key = strKey != null ?
                        (TKey)keyConverter.ConvertFromString(strKey) :
                        default(TKey);

                    XmlReader inner = reader.ReadSubtree();
                    inner.ReadToDescendant(valueElementName);
                    TValue value = XmlHelper<TValue>.readElement(inner);
                    inner.Close();

                    this.Add(key, value);
                } else {
                    reader.Skip();
                    break;
                }
            }
        }

        private void write_Attribute_Element(XmlWriter writer) {
            foreach (TKey key in this.Keys) {
                writer.WriteStartElement(itemElementName);

                writer.WriteAttributeString(keyValueAttributeName, key.ToString());

                XmlHelper<TValue>.writeElement(writer, this[key]);

                writer.WriteEndElement();
            }
        }

        private void read_Attribute_Attribute(XmlReader reader) {
            while (reader.Read()) {
                if (reader.NodeType != XmlNodeType.EndElement && reader.Name == itemElementName) {
                    string strKey = reader.GetAttribute(keyValueAttributeName);
                    TKey key = strKey != null ?
                        (TKey)keyConverter.ConvertFromString(strKey) :
                        default(TKey);

                    string strValue = reader.GetAttribute(valueValueAttributeName);
                    TValue value = strValue != null ?
                        (TValue)valueConverter.ConvertFromString(strValue) :
                        default(TValue);

                    this.Add(key, value);
                } else {
                    reader.Skip();
                    break;
                }
            }
        }

        private void write_Attribute_Attribute(XmlWriter writer) {
            foreach (TKey key in this.Keys) {
                writer.WriteStartElement(itemElementName);

                writer.WriteAttributeString(keyValueAttributeName, key.ToString());

                writer.WriteAttributeString(valueValueAttributeName, this[key].ToString());

                writer.WriteEndElement();
            }
        }
    }

    public static class IEnumerableKeyValuePairExtensionMethods {

        public static SerializableDictionary<TKey, TValue> ToSerializableDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source
        ) {
            var ret = new SerializableDictionary<TKey, TValue>();
            foreach (var kvp in source) {
                ret[kvp.Key] = kvp.Value;
            }
            return ret;
        }
    }
}
