using System.Collections;
using UnityEngine;
using TMPro;

public class DonateManager : MonoBehaviour
{
    [SerializeField] TMP_Text textoDonate;
    [SerializeField] Animator donateAnimation;
    [SerializeField] float tempoJogo = 180f, tempoPercorrido, intervalo = 60f;
    [SerializeField] int checagensFeitas;
    [SerializeField] public static int inimigosMortos;

    [SerializeField] GameObject transitionHome;
    [SerializeField] AudioSource donateAudio;
    void Start()
    {
        StartCoroutine(ContadorDeTempo());
    }

    IEnumerator ContadorDeTempo()
    {
        while (checagensFeitas < 3)
        {
            yield return new WaitForSeconds(intervalo);
            donateAnimation.SetTrigger("Donate");
            donateAudio.Play();
            checagensFeitas++;
            int donate;
            if (inimigosMortos <= 9)
            {
                donate = Random.Range(1, 6);
            }
            else
            {
                donate = Random.Range(5, 11);
            }

            UIManager.money += donate;
            textoDonate.text = $"{donate}";

            inimigosMortos = 0;
        }
    }
}
