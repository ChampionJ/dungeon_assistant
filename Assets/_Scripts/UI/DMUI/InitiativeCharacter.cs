using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeCharacter {
    public CharacterDataMini characterDataMini;
    public bool isClientCharacter = true;
    private int _initiativeNumber;
    public int initiativeNumber { get { return _initiativeNumber; } set { if (value < 0) _initiativeNumber = 0; else if (value > 30) _initiativeNumber = 30; else _initiativeNumber = value; }  }
    public InitiativeCharacter()
    {
        characterDataMini = new CharacterDataMini();
        initiativeNumber = 0;
    }
    public InitiativeCharacter(CharacterDataMini charDataMini)
    {
        characterDataMini = charDataMini;
        initiativeNumber = 0;
    }
    public void UpdateCharacterDataMini(CharacterDataMini charDataMini)
    {
        characterDataMini = charDataMini;
    }
    public bool IsSameOriginDevice(CharacterDataMini charDataMini)
    {
        return (charDataMini.uniqueID == characterDataMini.uniqueID);
    }
    public int CompareTo(InitiativeCharacter other)
    {
        if (other == null)
            return 1;
        else 
            return this.initiativeNumber.CompareTo(other.initiativeNumber);
    }
    

}
