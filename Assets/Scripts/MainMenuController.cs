using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] clips;

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
        audioSource.Play();
    }
}
