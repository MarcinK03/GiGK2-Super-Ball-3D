using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class BallController : MonoBehaviour
{
    [Header("Ruch i Kamera")]
    public float moveForce = 150f; // Zwiększone z 15 na 150, żeby kula żwawiej reagowała
    public Transform cameraTransform;

    [Header("Rozmiary")]
    public float normalRadius = 0.5f;
    public float smallRadius = 0.3f;
    public float largeRadius = 0.8f;

    [Header("Materiały")]
    public BallMaterialData defaultMaterial;
    public BallMaterialData currentMaterial;

    private Rigidbody rb;
    private SphereCollider col;
    private Renderer meshRenderer;

    public bool IsLarge => col.radius >= largeRadius - 0.05f;
    public bool IsSmall => col.radius <= smallRadius + 0.05f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        meshRenderer = GetComponent<Renderer>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        SetRadius(normalRadius);
        if (defaultMaterial != null) ApplyMaterial(defaultMaterial);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Kierunek ruchu zależny od kamery
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f; camRight.y = 0f;
        camForward.Normalize(); camRight.Normalize();

        Vector3 moveDir = (camForward * v + camRight * h).normalized;

        // Siła poruszająca kulę
        rb.AddForce(moveDir * moveForce, ForceMode.Force);
    }

    void Update()
    {
        // Zmiana rozmiaru (Q - mała, E - duża)
        if (Input.GetKeyDown(KeyCode.Q)) SetRadius(smallRadius);
        if (Input.GetKeyDown(KeyCode.E)) SetRadius(largeRadius);
    }

    public void SetRadius(float newRadius)
    {
        col.radius = newRadius;
        transform.localScale = Vector3.one * (newRadius * 2f);

        if (currentMaterial != null)
            rb.mass = currentMaterial.mass * (newRadius / normalRadius);
    }

    public void ApplyMaterial(BallMaterialData data)
    {
        currentMaterial = data;
        if (data == null) return;

        rb.linearDamping = data.drag;
        rb.angularDamping = data.angularDrag;
        rb.mass = data.mass * (col.radius / normalRadius);

        if (data.visualMaterial != null)
            meshRenderer.material = data.visualMaterial;

        Debug.Log("Zmieniono materiał na: " + data.materialName);
    }
}