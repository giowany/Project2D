using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAbout : MonoBehaviour
{
    public void showHideText()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
