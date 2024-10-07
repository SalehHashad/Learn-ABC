
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IDropHandler
{
    public string partAnswer;

    QuizManager parent;

    private void Start()
    {
        SetZPositionToZero(transform);
    }
    // add this to make the position in Z is set to 0 
    private void SetZPositionToZero(Transform trans)
    {
        Vector3 position = trans.localPosition;
        position.z = 0;
        trans.localPosition = position;

        for (int i = 0; i < trans.childCount; i++)
        {
            SetZPositionToZero(trans.GetChild(i));
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragObj = eventData.pointerDrag;
        parent = GameObject.FindGameObjectWithTag("Quiz Manager").GetComponent<QuizManager>();

        if (dragObj.tag == "Letter" &&
            compareAnswer(dragObj.GetComponent<VR_LetterTile>().alphabetLetter))
        {
            dragObj.GetComponent<VR_LetterTile>().setParentPos(this.transform);
        }
        else
            parent.PlaySound(false);
    }

    private bool compareAnswer(string a)
    {
        return partAnswer == a;
    }
}
