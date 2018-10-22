using UnityEngine;
using UnityEngine.UI;
using LocalisationAndTranslation;
using TMPro;

[AddComponentMenu ("UI/Text Translatable")]
public class LocalisedText : MonoBehaviour, ILocalisedObject
{
	/// <summary> Entry key Identifiable. </summary>
	[Tooltip ("Key of text to be translated. Must be unique.")]
	[SerializeField]
	private string key;

	private EntryType type = EntryType.Text;

	void Start ()
	{
		GUITranslator.Instance.AddLocalisedObject (this);
	}

	[System.Diagnostics.Conditional ("UNITY_EDITOR")]
	void Reset ()
	{
		Text textComponent = GetComponent<Text> ();
		TMP_Text tmpComponent = GetComponent<TMP_Text> ();

		if (textComponent == null && tmpComponent == null)
		{
			if (UnityEditor.EditorUtility.DisplayDialog ("Choose a Component", "You are missing one of the required componets. Please choose one to add", "Text", "TextMeshPro - Text"))
			{
				gameObject.AddComponent<Text> ();
			}
			else
			{
				gameObject.AddComponent<TextMeshProUGUI> ();
			}
		}
	}

	/// <summary>
	/// Sets content for Text Component
	/// </summary>
	/// <param name="content"></param>
	public void Set (string content)
	{
		Text text = gameObject.GetComponent<Text> ();
		TMP_Text TMPtext = GetComponent<TMP_Text> ();

		if (text != null)
		{
			text.text = content;
		}
		else if (TMPtext != null)
		{
			TMPtext.text = content;
		}
		else
		{
			Debug.LogWarningFormat ("LocalisedObject '{0}' has no text component associated.", gameObject.name);
		}
	}

	public string Key
	{
		get
		{
			return key;
		}

		set
		{
			key = value;
		}
	}

	public EntryType Type
	{
		get
		{
			return type;
		}
	}
}
