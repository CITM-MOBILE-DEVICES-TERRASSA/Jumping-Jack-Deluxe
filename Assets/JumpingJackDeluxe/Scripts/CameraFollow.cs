using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0.3f;
    public float cameraMinimunY = -0.6f;

    [Header("Camera Size")]
    public float normalSize = 5f;
    public float jumpSize = 6f;
    public float sizeChangeSpeed = 2f;

    private PlayerMovement playerMovement;
    private Vector3 velocity;
    private Camera cam;

    private void Start()
    {
        playerMovement = target.GetComponent<PlayerMovement>();
        cam = GetComponent<Camera>();
        cam.orthographicSize = normalSize;
    }

    private void LateUpdate()
    {
        // Position follow
        Vector3 currentOffset = offset;
        currentOffset.x = playerMovement.IsFacingRight ? offset.x : -offset.x;
        transform.position = Vector3.SmoothDamp(transform.position, target.position + currentOffset, ref velocity, smoothTime);

        transform.position = new Vector3(transform.position.x, Mathf.Max(cameraMinimunY, transform.position.y), transform.position.z);

        // Camera size adjustment
        float targetSize = playerMovement.IsInAir ? jumpSize : normalSize;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, sizeChangeSpeed * Time.deltaTime);
    }
}