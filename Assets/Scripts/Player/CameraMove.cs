using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    private float posY = 2.5f; 
    private float posZ = 2.55f; 
    private float rotX = 28f;

    void Update()
    {
        Vector3 forward = target.forward.normalized;
        Vector3 targetPosition = target.position - forward * posY + Vector3.up * posZ;
        transform.position = targetPosition;
        transform.LookAt(target);
        transform.rotation = Quaternion.Euler(rotX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
