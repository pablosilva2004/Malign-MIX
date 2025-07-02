using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Transition : MonoBehaviour
{
    [SerializeField] TMP_Text textoUI;
    [SerializeField] GameObject city, house;
    [SerializeField] bool transitionMercado, transitionToHouse, playerInside;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInside)
        {
            if (!transitionMercado)
            {
                if (transitionToHouse)
                {
                    city.SetActive(false);
                    house.SetActive(true);
                    textoUI.gameObject.SetActive(false);
                }
                else
                {
                    city.SetActive(true);
                    house.SetActive(false);
                    textoUI.gameObject.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textoUI.gameObject.SetActive(true);
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textoUI.gameObject.SetActive(false);
            playerInside = false;
        }
    }
}
