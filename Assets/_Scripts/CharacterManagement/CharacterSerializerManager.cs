using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;


static class CharacterSerializerManager
{

    public static String SerializeCharacterForHost(CharacterData characterData)
    {
        CharacterDataMini miniChar = new CharacterDataMini(characterData);
        XmlSerializer xmlSerializer = new XmlSerializer(miniChar.GetType());
        
        using (StringWriter textWriter = new StringWriter())
        {
            xmlSerializer.Serialize(textWriter, miniChar);
            return textWriter.ToString();
        }
        
        
    }

    public static CharacterDataMini ReadCharacterMiniXML(String characterDataMiniString)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CharacterDataMini));
        using (StringReader textReader = new StringReader(characterDataMiniString))
        {

            CharacterDataMini newChar = (CharacterDataMini)xmlSerializer.Deserialize(textReader);
            //newChar.ImportUpdate();

            return newChar;
        }
    }

    /// <summary>
    /// Serializes a Character Data down to xml
    /// </summary>
    /// <param name="characterData"></param>
    /// <returns></returns>
    public static String SerializeCharacter(CharacterData characterData)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(characterData.GetType());

        using (StringWriter textWriter = new StringWriter())
        {
            xmlSerializer.Serialize(textWriter, characterData);
            return textWriter.ToString();
        }
    }
    /// <summary>
    /// Deserializes a CharacterData from an xml string
    /// </summary>
    /// <param name="characterDataString"></param>
    /// <returns></returns>
    public static CharacterData ReadCharacterXML(String characterDataString)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CharacterData));
        using (StringReader textReader = new StringReader(characterDataString))
        {
            
            CharacterData newChar = (CharacterData)xmlSerializer.Deserialize(textReader);
            newChar.ImportUpdate();
            
            return newChar;
        }
    }
    public static void SaveCharacterDataToFile(int characterNum)
    {
        //get xml
        string characterXML = SerializeCharacter(CharacterManager.Instance.GetCharacter(characterNum));
        string path = Application.persistentDataPath + "/character";
        path += characterNum.ToString() + ".xml";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        using (FileStream fs = File.Create(path))
        {
            AddText(fs, characterXML);
            Debug.Log("Character #" + characterNum + " has been written to file");
        }
        
    }
    public static CharacterData ReadCharacterDataFromFile(int characterNum)
    {
        string path = Application.persistentDataPath + "/character";
        path += characterNum + ".xml";

        if (!File.Exists(path))
        {
            return new CharacterData();
        }

        string characterXML = "";

        StreamReader reader = new StreamReader(path);
        characterXML = reader.ReadToEnd();
        reader.Close();
        Debug.Log(path);
        Debug.Log("Character #" + characterNum + " has been read from file");
        return ReadCharacterXML(characterXML);
    }
    private static void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }
}