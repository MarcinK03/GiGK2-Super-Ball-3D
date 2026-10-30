using UnityEngine;

public class magi_kaf : MonoBehaviour
{
    public GameObject droga;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            droga.SetActive(true); 
        }
    }
}
