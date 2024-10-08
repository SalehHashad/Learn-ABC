
using UnityEngine;
using System.Collections;

/// <summary>
/// Script untuk setiap tombol pada menu Utama
/// </summary>

public class MenuButtonsScript : MonoBehaviour
{

	void Start ()
	{
		GameParent.alphabetIndex = 0;
	}

	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
	
	public void OnUpperButtonClick (int i)
	{
		switch (i) {
		case 1:
			{
				SubmenuControl.menuType = SubmenuControl.SubMenuType.LearntoRead;
				SubmenuControl.gotoScene = "Learn to Read";
				Application.LoadLevel ("Submenu Select");
			}
			break;
		case 2:
			{
				SubmenuControl.menuType = SubmenuControl.SubMenuType.LearntoWrite;
				SubmenuControl.gotoScene = "Learn to Write";
				Application.LoadLevel ("Submenu Select");
			}
			break;
		case 3:
			{
				SubmenuControl.menuType = SubmenuControl.SubMenuType.Pattern;
				SubmenuControl.gotoScene = "Patterns";
				Application.LoadLevel ("Submenu Select");
			}
			break;
		case 4:
			{
				SubmenuControl.menuType = SubmenuControl.SubMenuType.FindCorrectImage;
				SubmenuControl.gotoScene = "Find the Answer";
				Application.LoadLevel ("Submenu Select");
			}
			break;
		case 5:
			{
				SubmenuControl.menuType = SubmenuControl.SubMenuType.Puzzle;
				SubmenuControl.gotoScene = "Puzzle";
				Application.LoadLevel ("Submenu Select");
			}
			break;
		case 6:
			{
				SubmenuControl.menuType = SubmenuControl.SubMenuType.Puzzle2;
				SubmenuControl.gotoScene = "Puzzle2";
				Application.LoadLevel ("Submenu Select");
			}
			break;
		case 7:
			{
				SubmenuControl.menuType = SubmenuControl.SubMenuType.Puzzle2;
				SubmenuControl.gotoScene = "Quiz";
				Application.LoadLevel ("Quiz");
			}
			break;
		default:
			break;
		}
		//randomChanceInterstitial();
	}

	public void OnLowerButtonClick (int i)
	{
		switch (i) {
		case 4:
			Application.LoadLevel ("Find the Answer");
			break;
		case 5:
			Application.LoadLevel ("Puzzle");
			break;
		case 6:
			Application.LoadLevel ("Quiz");
			break;
        case 7:
            Application.LoadLevel("Puzzle2");
            break;
        }
		//randomChanceInterstitial();
	}

	private void randomChanceInterstitial ()
	{
//		int rnd = Random.Range (0, 100);
//		if (rnd >= 20)
//			AdmobManager.RequestInterstitial ();
	}
}
