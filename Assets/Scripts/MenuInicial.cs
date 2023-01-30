using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void Audio(float volume)
    {
        audioMixer.SetFloat("Volumen", volume);
    }
    
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
