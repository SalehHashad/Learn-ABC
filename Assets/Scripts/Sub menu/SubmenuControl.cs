

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Script untuk meng-handle Submenu
/// </summary>
public class SubmenuControl : MonoBehaviour
{
	public Font textFont;
	public static string gotoScene;
	public Button submenuButton;
	public static SubMenuType menuType = SubMenuType.LearntoRead;
	public List<string> LearntoRead = new List<string> ();
	public List<string> LearntoWrite = new List<string> ();
	public List<string> Pattern = new List<string> ();
	
	public List<string> Chars = new List<string> ();
	public bool IsCharCount = false;

	public enum SubMenuType
	{
		none = 0,
		LearntoRead = 1,
		LearntoWrite = 2,
		Pattern = 3,
        FindCorrectImage = 4,
        Puzzle = 5,
        Puzzle2 = 6
	}

	void Start ()
	{
		GameParent.alphabetIndex = 0;
		if (IsCharCount)
		{
			if (menuType == 0)
				Application.LoadLevel (gotoScene);
			else {
				switch (menuType) {
				case SubMenuType.LearntoRead:
					ButtonCloning (LearntoRead);
					break;
				case SubMenuType.LearntoWrite:
					ButtonCloning (LearntoWrite);
					break;
				case SubMenuType.Pattern:
					ButtonCloning (Pattern);
					break;
				}
			}
		}
		else
		{
            ButtonCloningForChoices(Chars);
        }
        //AdmobManager.bannerShow(false);
    }

	/// Method untuk meng-clone Button Submenu sesuai dengan jumlah minigame
	/// pada menu awal yang telah dipilih oleh user
	private void ButtonCloning (List<string> buttonName)
	{
		Button temp;
		for (int i=0; i<buttonName.Count; i++) {
			temp = Instantiate (submenuButton, transform.position, transform.rotation) as Button;
			temp.transform.SetParent (transform);
			temp.transform.GetChild (0).GetComponent<Text> ().text = buttonName [i];

			temp.transform.GetChild (0).GetComponent<Text> ().font= textFont;

			temp.gameObject.name = buttonName [i] + " Button";

			temp.GetComponent<SubmenuButtonScript> ().miniGameID = i;

			if (temp.transform.localScale == new Vector3(48, 48, 48))
				temp.transform.localScale = Vector3.one;

		}
	}
    private void ButtonCloningForChoices(List<string> buttonName)
    {
        Button temp;
        for (int i = 0; i < buttonName.Count; i++)
        {
            temp = Instantiate(submenuButton, transform.position, transform.rotation) as Button;
            temp.transform.SetParent(transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = buttonName[i];
            temp.transform.GetChild(0).GetComponent<Text>().font = textFont;

            temp.gameObject.name = buttonName[i] + " Button";

            int index = i;

            temp.onClick = new Button.ButtonClickedEvent();
            temp.onClick.AddListener(() => OnChooseButtonClicked(index));

            if (temp.transform.localScale == new Vector3(48, 48, 48))
                temp.transform.localScale = Vector3.one;
        }
    }

    void OnChooseButtonClicked(int i)
    {
        switch (i)
        {
            case 0:
                GameManager.Instance.fromIndex = 0;
                GameManager.Instance.toIndex = 5;
                break;
            case 1:
                GameManager.Instance.fromIndex = 6;
                GameManager.Instance.toIndex = 11;
                break;
            case 2:
                GameManager.Instance.fromIndex = 12;
                GameManager.Instance.toIndex = 17;
                break;
            case 3:
                GameManager.Instance.fromIndex = 18;
                GameManager.Instance.toIndex = 25;
                break;
        }
		ResetButtons();
		InstButtonForQuiz();
    }
	void ResetButtons()
	{
		foreach(Transform t in transform)
		{
			Destroy(t.gameObject);
		}
	}

    void InstButtonForQuiz()
	{
        // for instantiate buttons to Choose which Quiz
        GameParent.alphabetIndex = 0;

        if (menuType == 0)
            Application.LoadLevel(gotoScene);
        else
        {
            switch (menuType)
            {
                case SubMenuType.LearntoRead:
                    ButtonCloning(LearntoRead);
                    break;
                case SubMenuType.LearntoWrite:
                    ButtonCloning(LearntoWrite);
                    break;
                case SubMenuType.Pattern:
                    ButtonCloning(Pattern);
                    break;
				case SubMenuType.FindCorrectImage:
                    Application.LoadLevel("Find the Answer");
                    break;
				case SubMenuType.Puzzle:
                    Application.LoadLevel("Puzzle");
                    break;
				case SubMenuType.Puzzle2:
                    Application.LoadLevel("Puzzle2");
                    break;
            }
        }
    }
	

	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Escape))
			BackToScene ();
	}

	public void BackToScene ()
	{
		Application.LoadLevel (Application.loadedLevel - 1);
	}

	public float getWidth ()
	{
		RectTransform temp = GetComponent<RectTransform> ();
		return (temp.anchorMax.x - temp.anchorMin.x) * Screen.width;
	}
}
