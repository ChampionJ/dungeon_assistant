# Dungeon Assistant v0.5.0
## Description
Dungeon Assistant is a mobile application to assist players of Dungeons and Dragons. By pairing this Unity mobile app with a web server created in node.js it will allow players to spend more time playing the game and less time managing tasks.

This project is simply a small tool created for my own personal use mainly, designed specifically for Android (it should work fine with IPhone as well). 

**This source code should be used for reference purposes only.**

## First Time Setup
This project requires [Text Mesh Pro](https://www.assetstore.unity3d.com/en/#!/content/84126) to work, download the free package by Unity Technologies from the Asset Store and import it to the Asset folder. After this is done you will need to close and re-open the project (or possibly just reopen the scene you're in) to refresh the scripts and text assets. 

## Architecture
### Screen Management
The Screen Manager manages all of the various ui screens. It is accessable though it's singleton instance, which can be called with `ScreenManager.instance`.

Switching screens/menus is handled with this object, with it's various public screen switching functions. 

#### Overlay Management
Overlays are managed with the Overlay Manager, which is stored in the Screen Manager. 

#### Screen Options ####
The Screen Manager has a Screen Options object. This object is mostly used for global settings, currently it's handling themes.

Screen Options has a UnityEvent variable called themeChanged, this Event is invoked to trigger all of the [SpriteColorSetter](Assets/_Scripts/UI/SpriteColorSetter.cs) and [TextColorSetter](Assets/_Scripts/UI/TextColorSetter.cs) objects to update their colors to the current/new theme. 

### Character Management
The [Character Manager](Assets/_Scripts/CharacterManagement/CharacterManager.cs) manages all loaded characters and all access to these characters must be done through the Character Manager Singleton. 
The data for these characters is stored in an array of [Character Data](Assets/_Scripts/CharacterManagement/CharacterData.cs) objects. 

## Documentation
* [Design Doc](https://docs.google.com/document/d/1sxUEIAG3Xb5lRGYlQCq7Vzik_HYskY0a0iSkcAPu8Q0/edit?usp=sharing)
* [Change Log](CHANGELOG.md)

## External Plugins/Code
[Third Party Notice](third-party-notices.txt)
* [Text Mesh Pro](https://www.assetstore.unity3d.com/en/#!/content/84126)
  * Used for all of the text to allow for dynamic scaling and it's various other benefits
* [DoTween](http://dotween.demigiant.com/)
  * Used to handle tweens and animations
* [Take Ash's Serializable Dictionary](https://github.com/TakeAsh/cs-SerializableDictionary)
  * Used for serializing dicionaries found in CharacterData to xml to be stored on device 
* [Unity UI Extensions](https://bitbucket.org/UnityUIExtensions/unity-ui-extensions)
  * Used for their Horizontal Scroll Snap script, which is used on the Character Screen.
