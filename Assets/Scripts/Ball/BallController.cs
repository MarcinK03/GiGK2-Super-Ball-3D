using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class BallController : MonoBehaviour
{
    [Header("Ruch i Kamera")]
    public float moveForce = 150f;
    public Transform cameraTransform;

    [Header("Rozmiary (promienie)")]
    public  float normalRadius = 0.5f;
    public  float smallRadius = 0.3f;
    public  float largeRadius = 0.8f;
    public float[] radiusTable;
    private int currentRadiusIndex = 1; // 0 - small, 1 - normal, 2 - large
    [Header("Materiały")]
    public BallMaterialData defaultMaterial;
    public BallMaterialData currentMaterial;

    private Rigidbody rb;
    private SphereCollider col;
    private Renderer meshRenderer;
    private float currentRadius;

    public bool IsLarge => currentRadius >= largeRadius - 0.05f;
    public bool IsSmall => currentRadius <= smallRadius + 0.05f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        meshRenderer = GetComponent<Renderer>();
        radiusTable = new float[] { smallRadius, normalRadius, largeRadius };
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        // Inicjalizacja domyślnego rozmiaru
        currentRadius = normalRadius;
        ApplyRadius(normalRadius);

        if (defaultMaterial != null) ApplyMaterial(defaultMaterial);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f; camRight.y = 0f;
        camForward.Normalize(); camRight.Normalize();

        Vector3 moveDir = (camForward * v + camRight * h).normalized;
        rb.AddForce(moveDir * moveForce, ForceMode.Force);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) SetRadius(-1);
        if (Input.GetKeyDown(KeyCode.E)) SetRadius(1);
    }

    public void SetRadius(int step)
    {
        int newRadiusIndex = currentRadiusIndex + step;

        if (newRadiusIndex < 0 || newRadiusIndex >= radiusTable.Length) return;
        if (newRadiusIndex == currentRadiusIndex) return;

        float newRadius = radiusTable[newRadiusIndex];

     

        // ZAPIS NOWEGO INDEKSU 
        currentRadiusIndex = newRadiusIndex;

        // 2. Aplikujemy nowy rozmiar
        currentRadius = newRadius;
        ApplyRadius(newRadius);

        // 3. Aktualizacja masy
        if (currentMaterial != null)
            rb.mass = currentMaterial.mass * (newRadius / normalRadius);

        rb.WakeUp();
    }

    private void ApplyRadius(float radius)
    {
        // Ustawiamy fizyczny promień
        float scaleMultiplier = radius / 0.5f;
        transform.localScale = Vector3.one * scaleMultiplier;
    }

    public void ApplyMaterial(BallMaterialData data)
    {
        currentMaterial = data;
        if (data == null) return;

        rb.linearDamping = data.drag;
        rb.angularDamping = data.angularDrag;
        rb.mass = data.mass * (currentRadius / normalRadius);

        if (data.visualMaterial != null && meshRenderer != null)
            meshRenderer.material = data.visualMaterial;

        Debug.Log("Zmieniono materiał na: " + data.materialName);
    }
}