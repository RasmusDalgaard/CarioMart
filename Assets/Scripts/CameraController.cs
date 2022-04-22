using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform objectToFollow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed = 10;
    [SerializeField] private float lookSpeed = 10;

    public static CameraController mainCam;

    public void LookAtTarget()
    {
        Vector3 lookDirection = objectToFollow.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }

    public void MoveToTarget()
    {
        Vector3 targetPosition = objectToFollow.position +
                             objectToFollow.forward * offset.z +
                             objectToFollow.right * offset.x +
                             objectToFollow.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
    
    private void FixedUpdate()
    {
        LookAtTarget();
       MoveToTarget();
    }
}