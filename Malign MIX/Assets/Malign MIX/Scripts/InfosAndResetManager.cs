using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class InfosAndResetManager : MonoBehaviour
{
    [SerializeField] TMP_Text diasSobrevividos, cafeConsumido, refriConsumido, energeticoConsumido;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        diasSobrevividos.text = $"Days Survived: {UIManager.dias}";
        cafeConsumido.text = $"{UIManager.totalCafe}";
        refriConsumido.text = $"{UIManager.totalRefri}";
        energeticoConsumido.text = $"{UIManager.totalEnergetico}";
    }

    public void irProMenu()
    {
        UIManager.totalCafe = 0;
        UIManager.totalRefri = 0;
        UIManager.totalEnergetico = 0;

        UIManager.quantidadeCafe = 0;
        UIManager.quantidadeRefri = 0;
        UIManager.quantidadeEnergetico = 0;
        UIManager.money = 20;

        SceneManager.LoadScene(1);
    }
}
