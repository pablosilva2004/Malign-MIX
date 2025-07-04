using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Transition : MonoBehaviour
{
    [SerializeField] TMP_Text textoUI;
    [SerializeField] GameObject city, house;
    [SerializeField] GameObject cenaMercado, cenaUI;
    [SerializeField] bool transitionMercado, transitionToHouse, playerInside;
    [SerializeField] Animator transitionAnim;
    [SerializeField] AudioSource audioPorta;
    public GameObject jogador;

    [SerializeField] Material open, closed;
    MeshRenderer transitionMesh;

    [SerializeField] bool estaDeNoite;
    void Start()
    {
        cenaMercado.SetActive(false);
        transitionMesh = GetComponent<MeshRenderer>();

        transitionMesh.material = open;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && playerInside)
        {
            if (!transitionMercado)
            {
                if (transitionToHouse)
                {
                    StartCoroutine(TransitionToHouse());
                }
                else
                {
                    StartCoroutine(TransitionToCity());
                }
            }
            else
            {
                transitionMesh.material = open;
                jogador.GetComponent<Player>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cenaMercado.SetActive(true);
            }
        }
    }

    IEnumerator TransitionToHouse()
    {
        audioPorta.Play();
        jogador.GetComponent<Player>().enabled = false;
        transitionAnim.SetTrigger("EClicked");
        yield return new WaitForSeconds(.5f);
        city.SetActive(false);
        house.SetActive(true);
        textoUI.gameObject.SetActive(false);
        transitionAnim.SetTrigger("Transition");
        jogador.GetComponent<Player>().enabled = true;
    }

    IEnumerator TransitionToCity()
    {
        audioPorta.Play();
        jogador.GetComponent<Player>().enabled = false;
        transitionAnim.SetTrigger("EClicked");
        yield return new WaitForSeconds(.5f);
        house.SetActive(false);
        city.SetActive(true);
        textoUI.gameObject.SetActive(false);
        transitionAnim.SetTrigger("Transition");
        jogador.GetComponent<Player>().enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (other.gameObject.CompareTag("Player"))
        {
            textoUI.gameObject.SetActive(true);
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (other.gameObject.CompareTag("Player"))
        {
            textoUI.gameObject.SetActive(false);
            playerInside = false;
        }
    }
}
