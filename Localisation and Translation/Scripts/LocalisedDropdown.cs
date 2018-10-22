using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Dropdown))]
[AddComponentMenu ("UI/Localised Dropdown")]
public class LocalisedDropdown : MonoBehaviour
{
	private Dropdown m_dropdown;

	public string[] optionLocalisedKeys;

	private void Awake ()
	{
		m_dropdown = GetComponent<Dropdown> ();

		if (!m_dropdown)
		{
			Debug.LogWarning ("No dropdown component found. Be sure it is not being destroyed.");
		}
	}

	void Start ()
	{
		UpdateDropdown ();
	}

	void Update ()
	{

	}

	public void UpdateDropdown ()
	{
		CheckOptionList ();

		m_dropdown.captionText.text = GUITranslator.Instance.GetLocalisedText (optionLocalisedKeys[m_dropdown.value]);

		for (int i = 0; i < m_dropdown.options.Count; i++)
		{
			if (i < optionLocalisedKeys.Length)
				m_dropdown.options[i].text = GUITranslator.Instance.GetLocalisedText (optionLocalisedKeys[i]);
		}
	}

	private bool CheckOptionList ()
	{
		if (optionLocalisedKeys.Length == m_dropdown.options.Count)
			return true;

		if (optionLocalisedKeys.Length < m_dropdown.options.Count)
		{
			Debug.LogWarning ("Not every item in dropdown has a localised key. Some items will not be translated");
		}

		if (optionLocalisedKeys.Length > m_dropdown.options.Count)
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
