
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPausa : MonoBehaviour
{

    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    private bool juegopausado = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegopausado)
            {
                Reanudar();
            }
            else Pausa();
        }
    }
        
        
    public void Pausa()
    {
        juegopausado = false;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar()
    {
        juegopausado = true;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);


    }
    public void Reinciar()
    {
        juegopausado = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);

    }
    public void Cerrar()
    {
        Debug.Log("Cerrar");
        Application.Quit();
    }

}
