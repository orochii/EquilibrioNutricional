using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] clips;

    bool musicStarted;

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void PlaySound(int i) {
        audioSource.PlayOneShot(clips[i]);
    }

    public void StartMusic() {
        if (musicStarted) return;
        audioSource.Play();
        musicStarted = true;
    }

    public void OpenLink(string url) {
        Application.OpenURL(url);
    }
}
