using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Champion;

public class CharacterManager : MonoBehaviourSingleton<CharacterManager> {
    //public CharacterData data;
    private CharacterData[] characters;
    private int _activeCharacter =0;
    public int activeCharacterNum
    {
        get { return _activeCharacter; }
        private set { _activeCharacter = value; }
    }

    protected override void SingletonAwake()
    {
        characters = new CharacterData[3];
        //TODO LOAD DATA FROM FILE
        characters[0] = new CharacterData();
        characters[1] = new CharacterData();
        characters[2] = new CharacterData();
        UpdateCharactersFromFile();
    }
    public void SetActiveCharacter(int i)
    {
        
        if (i < 0)
            i = 0;
        if (i > 2) i = 2;
        activeCharacterNum = i;
    }
    public CharacterData GetActiveCharacter()
    {
        return characters[activeCharacterNum];
    }
    public CharacterData GetCharacter(int i)
    {
        return characters[i];
    }

#region saved data methods
    public void SaveActiveCharacterDataToFile()
    {
        SaveCharacterDataToFile(activeCharacterNum);
    }
    public void SaveCharacterDataToFile(int characterNum)
    {
        CharacterSerializerManager.SaveCharacterDataToFile(characterNum);
    }
    public void SaveAllCharactersToFile()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            CharacterSerializerManager.SaveCharacterDataToFile(i);
        }
    }
    public void UpdateCharacterFromFile(int characterNum)
    {
        characters[characterNum] = CharacterSerializerManager.ReadCharacterDataFromFile(characterNum);
    }
    public void UpdateCharactersFromFile()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i] = CharacterSerializerManager.ReadCharacterDataFromFile(i);
        }
    }
#endregion
}
