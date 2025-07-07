using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject city, house;
    [SerializeField] GameObject cenaInicio, cenaUI, canvas;
    [SerializeField] GameObject game1, game2, game3;

    public GameObject jogador;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        StartCoroutine(AnimInicio());
    }

    IEnumerator AnimInicio()
    {
        game1.SetActive(true);
        game2.SetActive(false);
        game3.SetActive(false);
        jogador.GetComponent<Player>().enabled = false;
        house.SetActive(false);
        city.SetActive(false);
        cenaUI.SetActive(false);
        canvas.SetActive(false);
        cenaInicio.SetActive(true);
        yield return new WaitForSeconds(28);
        house.SetActive(true);
        cenaUI.SetActive(true);
        cenaInicio.SetActive(false);
        canvas.SetActive(true);
        jogador.GetComponent<Player>().enabled = true;
    }

    public void PularInicio()
    {
        jogador.GetComponent<Player>().enabled = true;
        house.SetActive(true);
        cenaInicio.SetActive(false);
        cenaUI.SetActive(true);
        canvas.SetActive(true);
    }
}
