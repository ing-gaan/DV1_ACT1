using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class ButtonsActions : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void InitMenu()
    {
        _audioSource.Play();
        StartCoroutine(LoadScene("Menu"));
    }


    public void PlayGame()
    {
        _audioSource.Play();
        StartCoroutine(LoadScene("Level_01"));
    }


    public void ExitGame()
    {
        Application.Quit();
    }


    IEnumerator LoadScene(string scene)
    {        
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(scene);
    }

}
