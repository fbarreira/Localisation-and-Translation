using UnityEngine;
using UnityEngine.UI;
using TMPro;

[AddComponentMenu ("UI/Dropdown Localised")]
public class LocalisedDropdown : MonoBehaviour
{
	private Dropdown m_dropdown;
	private TMP_Dropdown m_TMPdropdown;

	public string[] optionLocalisedKeys;

	private void Awake ()
	{
		m_dropdown = GetComponent<Dropdown> ();
		m_TMPdropdown = GetComponent<TMP_Dropdown> ();

		if (!m_dropdown && !m_TMPdropdown)
		{
			Debug.LogWarning ("No dropdown component found. Be sure it is not being destroyed.");
		}
	}

	void Start ()
	{
		if (GUITranslator.Instance == null)
		{
			Debug.LogWarning ("No GUITranslator detected on scene, be sure to add one before calling it.");
		}

		GUITranslator.OnGUIupdate += UpdateDropdown;

		UpdateDropdown ();
	}

	private void OnDestroy ()
	{
		GUITranslator.OnGUIupdate -= UpdateDropdown;
	}

	[System.Diagnostics.Conditional ("UNITY_EDITOR")]
	void Reset ()
	{
		Dropdown dropdownDefault = GetComponent<Dropdown> ();
		TMP_Dropdown dropdownTMP = GetComponent<TMP_Dropdown> ();

		if (dropdownDefault == null && dropdownTMP == null)
		{
			if (UnityEditor.EditorUtility.DisplayDialog ("Choose a Component", "You are missing one of the required componets. Please choose one to add", "Dropdown", "TextMeshPro - Dropdown"))
			{
				gameObject.AddComponent<Dropdown> ();
			}
			else
			{
				gameObject.AddComponent<TMP_Dropdown> ();
			}
		}
	}

	void Update ()
	{

	}

	public void UpdateDropdown ()
	{
		if (m_TMPdropdown)
			UpdateTMP ();

		if (m_dropdown)
			UpdateDefault ();
	}

	private void UpdateDefault ()
	{
		CheckOptionList (m_dropdown.options.Count);

		m_dropdown.captionText.text = GUITranslator.Instance.GetLocalisedText (optionLocalisedKeys[m_dropdown.value]);

		for (int i = 0; i < m_dropdown.options.Count; i++)
		{
			m_dropdown.options[i].text = GUITranslator.Instance.GetLocalisedText (optionLocalisedKeys[i]);
		}
	}

	private void UpdateTMP ()
	{
		CheckOptionList (m_TMPdropdown.options.Count);

		m_TMPdropdown.captionText.text = GUITranslator.Instance.GetLocalisedText (optionLocalisedKeys[m_TMPdropdown.value]);

		for (int i = 0; i < m_TMPdropdown.options.Count; i++)
		{
			m_TMPdropdown.options[i].text = GUITranslator.Instance.GetLocalisedText (optionLocalisedKeys[i]);
		}
	}

	private bool CheckOptionList (int dropdownOptionsCount)
	{
		if (optionLocalisedKeys.Length == dropdownOptionsCount)
			return true;

		if (optionLocalisedKeys.Length < dropdownOptionsCount)
		{
			Debug.LogWarning ("Not every item in dropdown has a localised key. Some items will not be translated");
		}

		if (optionLocalisedKeys.Length > dropdownOptionsCount)
		{
			Debug.LogWarning ("There are more keys than items in the dropdown. Make sure the option list is not being altered.");
		}

		return false;
	}

	public Dropdown Dropdown
	{
		get
		{
			return m_dropdown;
		}
	}
}
