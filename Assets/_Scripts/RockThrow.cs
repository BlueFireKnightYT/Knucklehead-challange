using UnityEngine;
using UnityEngine.Rendering;

public class RockThrow : MonoBehaviour, IUsable
{
    Rigidbody rb;
    public float throwForce;
    private void Start()
    {
        //get rigidbody
        rb = GetComponent<Rigidbody>();
    }
    public void UseItem(Direction hand, PickupDrop pickup)
    {
        //drop rock
        pickup.DropItem(hand);
        //add force to rock in the looking direction
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }
}
