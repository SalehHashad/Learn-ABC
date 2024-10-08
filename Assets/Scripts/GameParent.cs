

using UnityEngine;
using System.Collections;

/// <summary>
/// Class parent untuk Script utama pada hampir semua minigame
/// </summary>

public class GameParent : MonoBehaviour
{
	public string backtoScene;

    // Updated From Me 

    public int fromIndex = 0;
	public int toIndex = 25;

    //Finished Here

    [HideInInspector]
	public static string
		alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public static int alphabetIndex = 0;

    private void Awake()
    {
        alphabetIndex = GameManager.Instance.fromIndex;
        fromIndex = GameManager.Instance.fromIndex;
        toIndex = GameManager.Instance.toIndex;
    }

    /// Jika user menekan tombol back, game akan 
    /// kembali pada menu sebelumnya
    void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			BackToScene ();
	}

	public virtual void BackToScene ()
	{
		Application.LoadLevel (backtoScene);
	}

	public virtual void OnPrevButtonClick ()
	{
		if (alphabetIndex == fromIndex)
			alphabetIndex = toIndex;
		else
			alphabetIndex--;
		InitAlphabets ();
	}

	public virtual void OnNextButtonClick ()
	{
		if (alphabetIndex >= toIndex)
			alphabetIndex = fromIndex;
		else
			alphabetIndex++;
		InitAlphabets ();
	}

    protected virtual void InitAlphabets ()
	{

	}

	protected char changeAlphabet ()
	{
		return alphabet [alphabetIndex];
	}
}
