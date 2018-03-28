using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Champion;
using System;

public class InitiativeManager : MonoBehaviourSingleton<InitiativeManager>
{
    List<InitiativeCharacter> initiativeCharacters;
    List<CharacterDataMini> charactersQueue;
    InitiativeCharacterButton[] initiativeCharacterButtons;
    private bool buttonsNeedToBeRefreshed = false;
    public Transform viewportContent;
    

    protected override void SingletonAwake()
    {
        initiativeCharacters = new List<InitiativeCharacter>();
        charactersQueue = new List<CharacterDataMini>();
        
    }
    // Use this for initialization
    void Start () {
		
	}
    private void RefreshButtonText(InitiativeCharacter initChar)
    {
        foreach(InitiativeCharacterButton b in initiativeCharacterButtons)
        {
            if (b.initCharacter.IsSameOriginDevice(initChar.characterDataMini))
            {
                b.UpdateText();
            }
        }
    }
    public void AddCharacter(InitiativeCharacter initChar)
    {
        initiativeCharacters.Add(initChar);
        RefreshButtons();
    }
    public void RemoveCharButton(InitiativeCharacterButton initCharButton)
    {
        for(int i = initiativeCharacters.Count - 1; i >= 0; i--)
        {
            if(initiativeCharacters[i] == initCharButton.initCharacter)
            {
                initiativeCharacters.RemoveAt(i);
            }
        }
        RefreshButtons();
    }
    public void RefreshButtons()
    {
        
        buttonsNeedToBeRefreshed = false;

        //remove buttons
        if (initiativeCharacterButtons != null)
        {
            for (int i = initiativeCharacterButtons.Length - 1; i >= 0; i--)
            {
                GameObject.Destroy(initiativeCharacterButtons[i].gameObject);
            }
        }

        initiativeCharacters.Sort((x, y) => y.initiativeNumber.CompareTo(x.initiativeNumber));
        InitiativeCharacter[] tempArray = initiativeCharacters.ToArray();
        //make new buttons
        initiativeCharacterButtons = new InitiativeCharacterButton[tempArray.Length];

        int buttonHeight = 100;




        viewportContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonHeight * tempArray.Length);

        for (int i = 0; i < initiativeCharacterButtons.Length; i++)
        {
            int temp = (-buttonHeight / 2 - buttonHeight * i);
            Debug.Log("Adding character button: " + i + " at this position: " + temp);
            GameObject go = Instantiate(Resources.Load("InitiativeCharButton", typeof(GameObject)), viewportContent) as GameObject;
            //RectTransform rt = go.GetComponent<RectTransform>();
            

            Vector3 pos = go.transform.localPosition;
            //go.transform.parent = viewportContent;
            //go.transform.SetParent(viewportContent, true);


            pos.y = (-buttonHeight / 2) - (buttonHeight * (i));
            go.transform.localPosition = pos;
            Debug.Log(pos);

            

            initiativeCharacterButtons[i] = go.GetComponent<InitiativeCharacterButton>();
            initiativeCharacterButtons[i].initCharacter = tempArray[i];

            initiativeCharacterButtons[i].UpdateText();
        }
        //adjust viewport content rect
        

    }
	// Update is called once per frame
	void Update () {
        lock (charactersQueue)
        {
            while(charactersQueue.Count > 0)
            {
                Debug.Log("charactersQueue > 0");
                lock (initiativeCharacters)
                {
                    for (int i = charactersQueue.Count -1; i >= 0; i--)
                    {
                        bool shouldAdd = true;
                        Debug.Log("charactersQueue loop " + i);
                        foreach (InitiativeCharacter initChar in initiativeCharacters)
                        {
                            Debug.Log("initChar loop");
                            if (initChar.IsSameOriginDevice(charactersQueue[i])){ //same clients character we already have
                                initChar.UpdateCharacterDataMini(charactersQueue[i]);
                                RefreshButtonText(initChar);
                                Debug.Log("we have this char");
                                shouldAdd = false;
                            }
                        }
                        if(initiativeCharacters.Count == 0 || shouldAdd)
                        {
                            Debug.Log("we dont have this char");
                            InitiativeCharacter newInitChar = new InitiativeCharacter(charactersQueue[i]);
                            initiativeCharacters.Add(newInitChar);
                            buttonsNeedToBeRefreshed = true;
                        }
                        charactersQueue.RemoveAt(i);
                    }
                }
            }
        }
        if (buttonsNeedToBeRefreshed)
        {
            RefreshButtons();
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {
            for(int i = 0; i < 10; i++)
            {
                CharacterDataMini newCharDat = new CharacterDataMini();
                newCharDat.maxHealth = i;
                newCharDat.ac = i;
                newCharDat.name = "test" + i.ToString();
                newCharDat.currentHealth = i;
                newCharDat.uniqueID = "host";
                newCharDat.tempHealth = 0;

                InitiativeCharacter newInitChar = new InitiativeCharacter(newCharDat);
                newInitChar.initiativeNumber = i;

                newInitChar.isClientCharacter = false;


                AddCharacter(newInitChar);
            }
        }

        
	}
    public void HandleCharacterUpdateFromServer(CharacterDataMini incomingCharacter)
    {
        Debug.Log("HandleCharacterUpdateFromServer");
        lock (charactersQueue)
        {
            Debug.Log("in lock");
            charactersQueue.Add(incomingCharacter);
        }
    }
}
