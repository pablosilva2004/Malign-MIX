using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TransitionGame : MonoBehaviour
{
    [SerializeField] TMP_Text playText;
    bool playerInside;
    [SerializeField] Player jogador;
    [SerializeField] GameObject game1, game2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInside)
        {
            StartCoroutine(TransitionToGame());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playText.gameObject.SetActive(true);
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playText.gameObject.SetActive(false);
            playerInside = false;
        }
    }

    IEnumerator TransitionToGame()
    {
        jogador.GetComponent<Player>().enabled = false;
        playText.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        playText.gameObject.SetActive(true);
        jogador.GetComponent<Player>().enabled = true;
        game1.SetActive(false);
        game2.SetActive(true);
    }
}
