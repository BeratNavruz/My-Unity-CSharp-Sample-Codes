using UnityEngine;

public class Stage6SpecialSelection : MonoBehaviour
{
    public GameObject[] specials;
    public GameObject startButton;

    public void SelectSpecial(int index)
    {
        specials[index].SetActive(true);
        startButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
