using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public static UIManager instance;

    [SerializeField] int life, maxLife;
    [SerializeField] int constantDamage = 10;
    [SerializeField] float contadorDanoConstante;

    [SerializeField] bool consumiuCafe, consumiuRefri, consumiuEnergetico;

    [SerializeField] Slider sliderLife;
    [SerializeField] Gradient gradientLife;
    [SerializeField] Image fillLife;

    [SerializeField] Animator animationDamage;
    [SerializeField] AudioSource audioDamage;

    [SerializeField] public static int money = 20;
    [SerializeField] TMP_Text textMoney;

    [SerializeField] public static int quantidadeCafe, quantidadeRefri, quantidadeEnergetico;
    [SerializeField] public static int totalCafe, totalRefri, totalEnergetico;
    [SerializeField] int precoCafe, precoRefri, precoEnergetico;
    [SerializeField] TMP_Text textCafe, textRefri, textEnergetico, textInsuficiente;
    [SerializeField] AudioSource tomouBebida;

    [SerializeField] public static int dias = 1;
    [SerializeField] TMP_Text textDias;
    [SerializeField] Animator dieAnimation;
    [SerializeField] GameObject game1, game2, game3, player, player2, player3;

    void Awake()
    {
        /*if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }*/

        if (FindObjectsOfType<UIManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        maxLife = life;
    }

    void Update()
    {
        if (life <= 0)
        {
            Die();
            sliderLife.gameObject.SetActive(false);
            life = maxLife;
        }
        textDias.text = $"{dias}";

        textCafe.text = $"{quantidadeCafe}";
        textRefri.text = $"{quantidadeRefri}";
        textEnergetico.text = $"{quantidadeEnergetico}";

        textMoney.text = $"${money}";

        sliderLife.value = life;

        fillLife.color = gradientLife.Evaluate(sliderLife.normalizedValue);

        contadorDanoConstante += Time.deltaTime;
        if (contadorDanoConstante >= 30f)
        {
            if (consumiuCafe && consumiuRefri && consumiuEnergetico)
            {
                animationDamage.SetTrigger("Dano");
                audioDamage.Play();

                constantDamage = 40;
                life -= constantDamage;
                contadorDanoConstante = 0f;
            }
            else if (consumiuCafe && consumiuRefri || consumiuCafe && consumiuEnergetico || consumiuRefri && consumiuEnergetico)
            {
                animationDamage.SetTrigger("Dano");
                audioDamage.Play();

                constantDamage = 30;
                life -= constantDamage;
                contadorDanoConstante = 0f;
            }
            else
            {
                animationDamage.SetTrigger("Dano");
                audioDamage.Play();

                constantDamage = 20;
                life -= constantDamage;
                contadorDanoConstante = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && quantidadeCafe > 0 && life < 100)
        {
            tomouBebida.PlayOneShot(tomouBebida.clip);
            consumiuCafe = true;
            totalCafe++;
            quantidadeCafe--;
            life = Mathf.Min(life + 10, maxLife);
        }
        if (Input.GetKeyDown(KeyCode.X) && quantidadeRefri > 0 && life < 100)
        {
            tomouBebida.PlayOneShot(tomouBebida.clip);
            consumiuRefri = true;
            totalRefri++;
            quantidadeRefri--;
            life = Mathf.Min(life + 25, maxLife);
        }
        if (Input.GetKeyDown(KeyCode.C) && quantidadeEnergetico > 0 && life < 100)
        {
            tomouBebida.PlayOneShot(tomouBebida.clip);
            consumiuEnergetico = true;
            totalEnergetico++;
            quantidadeEnergetico--;
            life = Mathf.Min(life + 50, maxLife);
        }
    }

    public void ComprarCafe()
    {
        if (money >= precoCafe)
        {
            quantidadeCafe++;
            money -= precoCafe;
            textInsuficiente.text = "";
        }
        else
        {
            textInsuficiente.text = $"insufficient money.";
        }
    }

    public void ComprarRefri()
    {
        if (money >= precoRefri)
        {
            quantidadeRefri++;
            money -= precoRefri;
            textInsuficiente.text = "";
        }
        else
        {
            textInsuficiente.text = $"insufficient money.";
        }
    }

    public void ComprarEnergetico()
    {
        if (money >= precoEnergetico)
        {
            quantidadeEnergetico++;
            money -= precoEnergetico;
            textInsuficiente.text = "";
        }
        else
        {
            textInsuficiente.text = $"insufficient money.";
        }
    }

    void Die()
    {
        StartCoroutine(DieSettings());
    }

    IEnumerator DieSettings()
    {
        dieAnimation.SetTrigger("Morrel");
        player.GetComponent<Player>().enabled = false;
        player2.GetComponent<MiniPlayer>().enabled = false;
        player3.GetComponent<Player>().enabled = false;
        yield return new WaitForSeconds(2f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(3);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
