using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject city, house;
    [SerializeField] GameObject cenaInicio, cenaUI;
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
        jogador.GetComponent<Player>().enabled = false;
        house.SetActive(false);
        city.SetActive(false);
        cenaUI.SetActive(false);
        cenaInicio.SetActive(true);
        yield return new WaitForSeconds(28);
        house.SetActive(true);
        cenaInicio.SetActive(false);
        cenaUI.SetActive(true);
        jogador.GetComponent<Player>().enabled = true;
    }

    public void PularInicio()
    {
        jogador.GetComponent<Player>().enabled = true;
        house.SetActive(true);
        cenaInicio.SetActive(false);
        cenaUI.SetActive(true);
    }
}
