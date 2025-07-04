using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Se a bala colidir com o inimigo, destruir
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            Debug.Log("Acertou inimigo");
        }

        // A cápsula lida com a destruição dela mesma
        Destroy(gameObject);
    }
}
