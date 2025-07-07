using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class TransitionToHouseDay : MonoBehaviour
{
    [SerializeField] TMP_Text sleepText;
    bool playerInside;
    [SerializeField] Player jogador;
    [SerializeField] GameObject game3, game1;
    void Start()
    {

    }

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
            sleepText.gameObject.SetActive(true);
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sleepText.gameObject.SetActive(false);
            playerInside = false;
        }
    }

    IEnumerator TransitionToGame()
    {
        jogador.GetComponent<Player>().enabled = false;
        sleepText.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        UIManager.dias++;
        jogador.GetComponent<Player>().enabled = true;
        game3.SetActive(false);
        game1.SetActive(true);
    }
}
