using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public void GoToCredits()
    {
        GameManager.instance.Credits();
    }
}