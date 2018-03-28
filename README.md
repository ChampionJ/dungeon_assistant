# Dungeon Assistant v0.5.0
## Description
Dungeon Assistant is a mobile application to assist players of Dungeons and Dragons. By pairing this Unity mobile app with a web server created in node.js it will allow players to spend more time playing the game and less time managing tasks.

This project is simply a small tool created for my own personal use mainly, designed for Android. 
## Setup
This project requires [Text Mesh Pro](https://www.assetstore.unity3d.com/en/#!/content/84126) to work, download the free package from the Asset Store and import it to the Asset folder. After this is done you will need to close and re-open the project to refresh the scripts and text assets. 


## Architecture
### Screen Management
The Screen Manager manages all of the various ui screens. It is accessable though it's singleton instance: ScreenManager.instance
#### Overlay Management
Overlays are managed with the Overlay Manager, which is stored in the Screen Manager. 
### Character Management
The [Character Manager](Assets/_Scripts/CharacterManagement/CharacterManager.cs) manages all loaded characters and all access to these characters must be done through the Character Manager Singleton. 
The data for these characters is stored in an array of [Character Data](Assets/_Scripts/CharacterManagement/CharacterData.cs) objects. 

## Documentation
* [Design Doc](https://docs.google.com/document/d/1sxUEIAG3Xb5lRGYlQCq7Vzik_HYskY0a0iSkcAPu8Q0/edit?usp=sharing)
* [Change Log](CHANGELOG.md)

## External Plugins/Code
* [Text Mesh Pro](https://www.assetstore.unity3d.com/en/#!/content/84126)
* [DoTween](http://dotween.demigiant.com/)
* [Paul Welter's XML Serializable Generic Dictionary Class](https://weblogs.asp.net/pwelter34/444961)
* [Unity UI Extensions](https://bitbucket.org/UnityUIExtensions/unity-ui-extensions)

