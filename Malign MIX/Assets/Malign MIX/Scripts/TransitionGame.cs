using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TransitionGame : MonoBehaviour
{
    [SerializeField] TMP_Text playText;
    bool playerInside;
    [SerializeField] Player jogador;
    [SerializeField] Animator transitionAnim;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInside)
        {
            StartCoroutine(TransitionToGame(3));
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
    
    IEnumerator TransitionToGame(int index)
    {
        jogador.GetComponent<Player>().enabled = false;
        transitionAnim.SetTrigger("EClicked");
        playText.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
