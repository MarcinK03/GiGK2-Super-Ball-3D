using UnityEngine;

public class magi_kaf_2 : MonoBehaviour
{
    public GameObject Droga_1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Droga_1.SetActive(true);
        }
    }
}
