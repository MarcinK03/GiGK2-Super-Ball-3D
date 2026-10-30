    using UnityEngine;

    public class ZagadkaKlocki : MonoBehaviour
    {
        public GameObject brama;
    public AudioSource dzwiek;
    public AudioSource dzwiek2;
    private int zieloneWTriggrze = 0;

        void OnTriggerEnter(Collider other)
        {
            // Wykrywamy tylko zielone klocki
            if (other.CompareTag("BoxGreen"))
            {
            dzwiek.Play();
            zieloneWTriggrze++; // Dodaje 1 do licznika

                // Jeśli licznik wskaże 2, brama znika
                if (zieloneWTriggrze >= 2)
                {
                dzwiek2.Play();
                brama.SetActive(false);
                }
            }
        }
    }
