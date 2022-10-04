using UnityEngine;

public class MovementController : MonoBehaviour, IMoveable
{

    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;
    
    [SerializeField] private float _speed = 11; 
    [SerializeField] private float _rotationSpeed = 140;

    public void Travel(Vector3 direction, Rigidbody rb) =>
        rb.MovePosition(transform.position + direction * (_speed * Time.deltaTime));

    public void Rotate(Vector3 direction,Rigidbody rb)
    {
        Quaternion deltaRotation = Quaternion.Euler(direction * (_rotationSpeed * Time.deltaTime));
 
        rb.MoveRotation(rb.rotation * deltaRotation);


    }
}