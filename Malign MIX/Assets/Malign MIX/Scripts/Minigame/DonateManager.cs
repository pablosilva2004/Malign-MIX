using System.Collections;
using UnityEngine;
using TMPro;

public class DonateManager : MonoBehaviour
{
    [SerializeField] TMP_Text textoDonate;
    [SerializeField] Animator donateAnimation;
    [SerializeField] float tempoPercorrido, intervalo = 60f;
    //[SerializeField] public static float tempoJogo;
    [SerializeField] public static int checagensFeitas;
    [SerializeField] public static int inimigosMortos;
    [SerializeField] AudioSource donateAudio;

    bool corrotinaAtiva = false;
    void Start()
    {
        //tempoJogo = 0;

        checagensFeitas = 0;
        inimigosMortos = 0;
        StartCoroutine(ContadorDeTempo());
        corrotinaAtiva = true;
    }

    IEnumerator ContadorDeTempo()
    {
        while (checagensFeitas < 2)
        {
            yield return new WaitForSeconds(intervalo);
            donateAnimation.SetTrigger("Donate");
            donateAudio.Play();
            checagensFeitas++;
            int donate;

            if (inimigosMortos > 5)
            {
                if (inimigosMortos <= 9)
                {
                    donate = Random.Range(1, 6);
                }
                else
                {
                    donate = Random.Range(5, 11);
                }
            }
            else
            {
                donate = 0;
            }

            UIManager.money += donate;
            textoDonate.text = $"{donate}";

            inimigosMortos = 0;
        }

        corrotinaAtiva = false;
    }

    void Update()
    {
        //tempoJogo += Time.deltaTime;

        if (checagensFeitas == 2 /*&& tempoJogo >= 120f*/)
        {
            //tempoJogo = 0;
            checagensFeitas = 0;
            inimigosMortos = 0;

            if (!corrotinaAtiva)
            {
                StartCoroutine(ContadorDeTempo());
                corrotinaAtiva = true;
            }
        }
    }
}
