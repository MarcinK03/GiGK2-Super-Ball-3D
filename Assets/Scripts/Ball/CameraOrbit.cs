using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // Kula
    public float distance = 5f;
    public float height = 2f;
    public float rotateSpeed = 100f;

    private float currentYaw = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        // Obrót strzałkami (wymaga ustawienia "HorizontalArrow" w Input Managerze lub zmiany na Mouse X)
        float inputX = Input.GetAxis("Mouse X") * 2f; // Na start dajemy myszkę dla łatwości
        currentYaw += inputX * rotateSpeed * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(0f, currentYaw, 0f);
        Vector3 offset = rotation * new Vector3(0f, height, -distance);

        transform.position = target.position + offset;
        transform.LookAt(target.position + Vector3.up * 0.5f);
    }
}