using UnityEngine;

public class Brama_trg : MonoBehaviour
{
    public GameObject bramaDoOtwarcia;
    public AudioSource dzwiek;
    public AudioSource dzwiek2;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            dzwiek.Play();
            dzwiek2.Play();
            bramaDoOtwarcia.SetActive(false);
        }
    }
}