using UnityEngine;
using TMPro;

public class CapsuleTarget : MonoBehaviour
{
    public System.Action<CapsuleTarget> OnCapsuleDestroyed;

    public int hitCount = 0;
    [SerializeField] TMP_Text enemyKilledText;

    void Update()
    {
        if (DonateManager.checagensFeitas == 2)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitCount++;
            Debug.Log("$Dano na cápsula: {hitCount}");
            if (hitCount >= 2)
            {
                OnCapsuleDestroyed?.Invoke(this);
                EnemyKillCounter.instance.RegisterKill();
                Destroy(gameObject);
            }

            Destroy(collision.gameObject);
        }
    }
}