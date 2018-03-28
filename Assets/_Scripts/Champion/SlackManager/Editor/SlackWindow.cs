using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;


/// <summary>
/// Handles GUI and user input for the Slack Manager.
/// </summary>
public class SlackWindow : EditorWindow
{

	#region Vars
	/// <summary>
	/// Active Slack Manager in the scene
	/// </summary>
	private static SlackManager instance;
	/// <summary>
	/// Get / create an instance of the Slack Manager
	/// </summary>
	/// <returns>The instance.</returns>
	public static SlackManager GetInstance(){
		if (instance == null)
		{
			instance = FindObjectOfType<SlackManager>();
			if (instance == null)
				instance = (new GameObject("SlackManager")).AddComponent<SlackManager>();
		}
		return instance;
	}

	private Dictionary<string, string> webhooks = new Dictionary<string, string> ();

	string webhookURL = " ";
	#endregion

	#region Window Settings
	Vector2 windowSize = new Vector2(500, 700);
	static private SlackWindow window;
	#endregion
    /// <summary>
    /// Initalize this instance.
    /// </summary>
	[MenuItem ("Window/Slack")]
	static void Init ()
	{
		// Get existing open window or if none, make a new one:
		window = (SlackWindow)EditorWindow.GetWindow (typeof(SlackWindow), true);
		window.Show ();
		window.titleContent.text = "Slack Window";
		window.maxSize = new Vector2 (810, 810);
		window.minSize = new Vector2 (340, 340);
	}

	void OnDisable()
	{
	}

	/// <summary>
    /// Awake this instance.
    /// </summary>
	private void Awake ()
	{
		LoadWebhooks ();
		GetInstance ();
		instance._webhookURL = webhookURL;
	}

	#region GUI
    /// <summary>
    /// When the GUI is generated.
    /// </summary>
	void OnGUI ()
	{
		GUILayout.Label("Slack Manager Settings", EditorStyles.boldLabel);
		webhookURL = EditorGUILayout.TextField("Slack Webhook URL", webhookURL);
		GUIElementsSaveButton ();
	}
	void Update()
	{
	}

	private void GUIElementsSaveButton() 
	{
		if (GUI.Button (new Rect (window.position.width - 110, window.position.height - 30, 100, 20), "Save")) 
		{
			SaveWebhooks ();
			Close ();
		}
	}
	#endregion

	#region Load/Save Webhooks
	private void LoadWebhooks(){
		if (File.Exists(System.Environment.GetEnvironmentVariable ("HOME") + "/webhooks.txt"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(System.Environment.GetEnvironmentVariable ("HOME") + "/webhooks.txt", FileMode.Open);

			webhookURLs loadedHooks = (webhookURLs)bf.Deserialize(file);
			file.Close();
			webhooks = loadedHooks.hooks;

		} else {
			UnityEngine.Debug.LogError ("Webhooks not found!");
			UnityEngine.Debug.Log ("Creating webhooks...");
			CreateWebhooks ();
		}

		string value;
		if (webhooks.ContainsKey (Application.productName))
			webhookURL = webhooks [Application.productName];
		else {
			UnityEngine.Debug.Log ("No webhook for this URL");
			webhooks.Add (Application.productName, webhookURL);
			SaveWebhooks ();
		}

		Debug.Log (webhookURL);
	}

	/// <summary>
	/// Create the text file to store the webhooks
	/// </summary>
	private void CreateWebhooks(){
		if (!File.Exists ((System.Environment.GetEnvironmentVariable ("HOME") + "/webhooks.txt"))) {
			UnityEngine.Debug.Log ("Creating repository location text file...");
			System.IO.File.WriteAllText (System.Environment.GetEnvironmentVariable ("HOME") + "/webhooks.txt", "No saved webhooks");
		}
	}

	private void SaveWebhooks(){
		
		if (webhooks.ContainsKey (Application.productName))
			webhooks [Application.productName] = webhookURL;
		else
			webhooks.Add (Application.productName, webhookURL);

		if (File.Exists (System.Environment.GetEnvironmentVariable ("HOME") + "/webhooks.txt") && webhooks != null) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (System.Environment.GetEnvironmentVariable ("HOME") + "/webhooks.txt", FileMode.Open);

			webhookURLs tempHook = new webhookURLs ();
			tempHook.hooks = webhooks;
			bf.Serialize (file, tempHook);
			file.Close ();
		} else {
			UnityEngine.Debug.LogError ("Webhooks text file not found!");
		}

		instance._webhookURL = webhookURL;
	}
	#endregion
}

[Serializable]
class webhookURLs
{
	public Dictionary<string, string> hooks;
}