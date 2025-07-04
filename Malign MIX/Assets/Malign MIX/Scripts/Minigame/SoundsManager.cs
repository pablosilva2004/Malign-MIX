using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance { get; set; }

    [SerializeField] public AudioSource audioBullet;
    [SerializeField] public AudioSource audioReloading;
    [SerializeField] public AudioSource audioEmpty;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
