/*
 * SETUP make sure to set the _webhook URL to the correct hook.
 *  in the future this will probably be grabbed from an external source for security?
 * 
 * 
 * Either Attatch this script to an object in the initial scene, or call SlackManager.GetInstance() to create one
 * 
 * Example: send a single message with SlackManager.GetInstance().SendSlackMessage(SlackManager.messageField("message text"));
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Champion;

public class SlackManager : MonoBehaviourSingleton<SlackManager> {
#region vars
    /// <summary>
    /// The sidebar color that will appear next to the message in Slack
    /// </summary>
    private Color32 barColor = new Color32 (50, 150, 200, 255);
    /// <summary>
    /// The username that appears in Slack
    /// </summary>
    private string username = "Dungeon Assistant Slack Manager";
    /// <summary>
    /// The webhook URL, this field should probably empty and reference something locally for security
    /// </summary>
	public string _webhookURL;
	public string webhookURL{ get { return _webhookURL; }}
    /// <summary>
    /// The text that appears above the message/fields
    /// </summary>
	private string prefixText;
    /// <summary>
    /// The emojii that will appear in Slack as the integration profile picture
    /// </summary>
	private string iconEmojii = ":large_blue_diamond:";
    #endregion
    protected override void SingletonAwake()
    {
        SendSlackMessage(StartupMessage());
    }
    

    /// <summary>
    /// This is the main method you will use to send messages to slack
    /// </summary>
    /// <param name="fields">An array of strings "fields"</param>
	public void SendSlackMessage(string[] fields){
		StartCoroutine(PostJSON(BuildMessageJSON(fields)));
	}
	/// <summary>
	/// This is the main method you will use to send messages to slack
	/// </summary>
	/// <param name="field">A single string "field".</param>
	public void SendSlackMessage(string field)
	{
        string[] f = new string[1] { field };
		StartCoroutine(PostJSON(BuildMessageJSON(f)));
	}
    /// <summary>
    /// An example fields array, sent when the SlackManager is awakened
    /// </summary>
    /// <returns>The message.</returns>
	public string[] StartupMessage(){
		prefixText = "Incoming Message from Dungeon Assistant Slack Manager";
		List<string> fieldsList = new List<string> ();
		fieldsList.Add (CreateField ("Device Name", SystemInfo.deviceName, true));
		fieldsList.Add (CreateField("OS", SystemInfo.operatingSystem, true));
		fieldsList.Add (CreateField ("Model", SystemInfo.deviceModel, true));
		fieldsList.Add (CreateField ("App Version", Application.version, true));
		fieldsList.Add (CreateField ("Device Type", SystemInfo.deviceType.ToString (), true));
		fieldsList.Add (CreateField("Message", "Launched App"));
		return fieldsList.ToArray ();
	}

#region Custom Messages
/// <summary>
/// Customs the message.
/// </summary>
/// <returns>The message.</returns>
/// <param name="infoInput"> Send each field with field Title, and Value separated by a comma. example: ("Title,Message")</param>.</param>
	public void CustomMessage(string[] infoInput){
		prefixText = "Incoming Message from " + Application.productName + "'s Slack Manager";
		string[] jsonMessage;
		List<string> strMessage = new List<string> ();
		foreach (var item in infoInput) {
			string[] field;
			field = item.Split(',');
			if (!IsOdd (field.Length)) {
				Debug.Log ("Message is not odd!");
				List<string> paramaters = new List<string> ();
				for (int i = 0; i < field.Length; i++) {
					paramaters.Add (field [i]);
					if (IsOdd (i)) {
						strMessage.Add(CreateField(paramaters[0], paramaters[1]));
						paramaters.Clear ();
					}
				}
			} else
				return;
		}
		jsonMessage = strMessage.ToArray ();

		SendSlackMessage (jsonMessage);
	}

	private bool IsOdd(int num){
		return num % 2 != 0;
	}
#endregion

#region Fields
//    static public string deviceNameField 
//	{ 
//		get { return ; }
//	}
//	static public string osField
//	{
//        get { return ; }
//	}
//	static public string modelField
//	{ 
//		get { return ; }
//	}
//	static public string versionField 
//	{ 
//		get { return ; }
//	}
//	static public string deviceTypeField
//	{
//        get { return ; }
//	}
//    static public string messageField(string message){
//        return 
//    }

    /// <summary>
    /// Creates a new Field for the Slack JSON
    /// </summary>
    /// <returns>The created field</returns>
    /// <param name="title">Field Title</param>
    /// <param name="value">Field Value</param>
    /// <param name="halfSize">Should This Take Up Half Size of the Window</param>
    static public string CreateField(string title, string value, bool halfSize = false){
        string s = @" {
			""title"": """ + title + @""",
			""value"": """ + value + @"""
			";
		if(halfSize)
			s += @", ""short"": true";
		s+= @"} ";
		return s;
	}
#endregion
#region JSON
    private string BuildJSONHeader(){
		string s = @"{
			""username"":"""+ username + @""",
            ""icon_emoji"":""" + iconEmojii + @""",
            ""text"": """ + prefixText + @" "",
            ""attachments"": [
            {
				""color"" : """ + CMath.ColorToHex(barColor) + @""" ,
				""fields"": [ ";
		return s;
	}
	private string BuildJSONFooter(){
		string s = @"
			            ]
			        }
			    ]
			}";
		return s;
	}

	/// <summary>
	/// Builds the Complete and Final Message JSON
	/// </summary>
	/// <returns>The Finalized JSON for Slack.</returns>
	/// <param name="messages">Fields</param>
	private string BuildMessageJSON(string[] fields){
		string s = ""; 
		s = s + BuildJSONHeader();
		for (int i = 0; i <= fields.Length -1; i++) {
			s += fields [i];
			if (i != fields.Length - 1)
				s += @",";
		}
		s = s + BuildJSONFooter();
//		UnityEngine.Debug.Log (s);
		return s;
	}
#endregion

    //apparently this is how to actually send the json to slack
    public IEnumerator PostJSON(string message)
	{
		//Dictionary<string, object> mess = Json.Deserialize(message) as Dictionary<string, object>;
        Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/x-www-form-urlencoded");
		WWWForm form = new WWWForm();

        form.AddField("payload", message);

		WWW www = new WWW(webhookURL, form.data, headers);
		yield return www;
		Debug.LogFormat("slack result {0}", www.text);
	}

}
