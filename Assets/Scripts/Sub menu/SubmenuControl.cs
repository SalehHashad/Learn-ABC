

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

	public enum SubMenuType
	{
		none = 0,
		LearntoRead = 1,
		LearntoWrite = 2,
		Pattern = 3
	}

	void Start ()
	{
		GameParent.alphabetIndex = 0;

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
