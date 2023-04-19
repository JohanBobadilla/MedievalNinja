using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{

    public float tutorialTime;
    void Start()
    {
        StartCoroutine(CloseWindow());
    }


    IEnumerator CloseWindow()
    {
        yield return new WaitForSeconds(tutorialTime);
        this.gameObject.SetActive(false);
    }

}
