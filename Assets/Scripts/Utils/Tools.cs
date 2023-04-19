using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Tools : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource buttonSound;
    public static Tools instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        //buttonSound = GetComponent<AudioSource>();
    }


    public void LoadSceneAsync(string scene, GameObject loadingPanel, bool savePrevious = true)
    {
        loadingPanel.SetActive(true);
        if (savePrevious)
        {
            //UserData.instance.previousScene = SceneManager.GetActiveScene().name;
        }
        StartCoroutine(StartLoadScene(scene));
    }

    IEnumerator StartLoadScene(string scene)
    {
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(scene);

        while (!sceneLoading.isDone)
        {
            yield return null;
        }
    }

    // public void AssingButtonSound()
    // {
    //     Button[] buttons = FindObjectsOfType<Button>(true);
    //     foreach (Button button in buttons)
    //     {
    //         button.gameObject.AddComponent<ButtonHanlder>().AssignValues(buttonSound, sounds[0], sounds[1]);
    //     }
    // }

    // public void ClickButtonSound()
    // {
    //     buttonSound.Play();
    // }

}
