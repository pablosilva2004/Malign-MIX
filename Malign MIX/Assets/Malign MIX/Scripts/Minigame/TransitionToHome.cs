using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class TransitionToHome : MonoBehaviour
{
    [SerializeField] TMP_Text quitText;
    bool playerInside;
    [SerializeField] MiniPlayer jogador;
    [SerializeField] int index;
    [SerializeField] GameObject game2, game3;

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
            quitText.gameObject.SetActive(true);
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            quitText.gameObject.SetActive(false);
            playerInside = false;
        }
    }

    IEnumerator TransitionToGame()
    {  
        jogador.GetComponent<MiniPlayer>().enabled = false;
        quitText.gameObject.SetActive(false);
        //DonateManager.tempoJogo = 0f;
        DonateManager.checagensFeitas = 0;
        DonateManager.inimigosMortos = 0;
        yield return new WaitForSeconds(.5f);
        jogador.GetComponent<MiniPlayer>().enabled = true;
        game2.SetActive(false);
        game3.SetActive(true);
    }
}
