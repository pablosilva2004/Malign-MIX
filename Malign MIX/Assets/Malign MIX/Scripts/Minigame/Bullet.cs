using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            SoundsManager.instance.audioImpact.PlayOneShot(SoundsManager.instance.audioImpact.clip);
            Debug.Log("Acertou inimigo");
            Destroy(gameObject);    
        }

    }
}
