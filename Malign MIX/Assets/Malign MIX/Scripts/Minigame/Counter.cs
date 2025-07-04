using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    float counterFloat;
    [SerializeField] TMP_Text counter;
    void Start()
    {
        
    }

    void Update()
    {
        counterFloat += Time.deltaTime;

        counter.text = $"Tempo: {counterFloat}";
    }
}
