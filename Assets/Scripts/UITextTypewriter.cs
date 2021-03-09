using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// attach to UI Text component (with the full text already there)

public class UITextTypewriter : MonoBehaviour
{
	//Variables
	public int textNumber = 1;
	public bool textDrawNow = false;
	public float waitBetweenChars = 0.125f;
	public float destroyAfterTime = 0f;
	public bool runMethodAfterText = false;
	public int methodTextNumber = -1;
	public int finalTextNumber = 4;
	public string nextSceneName;

	Text txt;
	string story;

	#region EventListeners

	private void OnEnable()
	{
		TextHandler.TextWrite += CheckMatchingTextNumber;
	}

	private void OnDisable()
	{
		TextHandler.TextWrite -= CheckMatchingTextNumber;
	}

	#endregion EventListeners

	//Awake
	void Awake()
	{
		txt = GetComponent<Text>();
		story = txt.text;
		txt.text = "";
	}

	//Update
    void Update()
    {
		if (textDrawNow)
        {
			StartCoroutine("PlayText");
			textDrawNow = false;
		}
    }

	//Check Matching Text Number
	void CheckMatchingTextNumber(int textNumberToCheck)
    {
		if (textNumber == textNumberToCheck)
        {
			textDrawNow = true;
        }
    }

	//Write Out Text (typewriter style)
    IEnumerator PlayText()
	{
		foreach (char c in story)
		{
			txt.text += c;
			yield return new WaitForSeconds(waitBetweenChars);
		}
		if (destroyAfterTime > 0)
        {
			StartCoroutine("DestroyText");
		}
	}

	//Destroy Text (after delay if entered)
	IEnumerator DestroyText()
    {
		yield return new WaitForSeconds(destroyAfterTime);
		txt.text = "";
		textDrawNow = false;
		//check if method should run
		if (runMethodAfterText)
        {
			TextHandler.TextEndMethod?.Invoke(methodTextNumber);
        }
		//check if next scene should load
		if (textNumber == finalTextNumber)
        {
			SceneManager.LoadSceneAsync(nextSceneName);
        }
	}
}