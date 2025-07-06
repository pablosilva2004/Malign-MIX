using UnityEngine;
using TMPro;

public class EnemyKillCounter : MonoBehaviour
{
    public static EnemyKillCounter instance;

    int killCount = 0;

    [SerializeField] TMP_Text killCountText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterKill()
    {
        killCount++;
        DonateManager.inimigosMortos++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (killCountText != null)
        {
            killCountText.text = $"Enemy Kiled: {killCount}";
        }
    }
}

