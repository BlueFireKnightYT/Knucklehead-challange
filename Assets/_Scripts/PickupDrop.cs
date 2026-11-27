using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public enum Direction {Left, Right}

public class PickupDrop : MonoBehaviour
{
    [Header("Components")]
    public GameObject cam;
    public Transform lefthandPos;
    public Transform rightHandPos;

    bool hasItemLeft = false;
    bool hasItemRight = false;

    public GameObject heldItemLeft;
    public GameObject heldItemRight;

    [Header("Raycast")]
    RaycastHit hit;
    public float pickupDistance;
    public LayerMask pickupLayer;

    Direction activeHand;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void PickupItem(Direction hand)
    {
        //shoots raycast
        bool hasHit = Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickupDistance, pickupLayer);
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 5, Color.red);
        //raycast checks hit object for the Pickup tag
        if(hasHit)
        {
            if(hit.transform.CompareTag("Pickup"))
            {
                //if true, objects rigidbody becomes static
                hit.rigidbody.isKinematic = true;

                //childs the gameobject to the player
                hit.transform.SetParent(cam.transform);

                //then the objects position becomes the hand's position
                if(hand == Direction.Left)
                {
                    hit.transform.position = lefthandPos.position;

                    //makes hasitem true
                    hasItemLeft = true;

                    heldItemLeft = hit.transform.gameObject;
                }
                else
                {
                    hit.transform.position = rightHandPos.position;

                    //makes hasitem true
                    hasItemRight = true;

                    heldItemRight = hit.transform.gameObject;
                }

                hit.transform.rotation = cam.transform.rotation;
                //object becomes Usable
                
            }
        }

    }

    void Dropitem(Direction hand)
    {
        GameObject dropitem = null;

        //Checks for item in hand
        if (hand == Direction.Left)
        {
            if(hasItemLeft)
            {
                dropitem = heldItemLeft;
                //Makes hasitem false
                hasItemLeft = false;
            }
        }
        else
        {
            if(hasItemRight)
            {
                dropitem = heldItemRight;
                //Makes hasitem false
                hasItemRight = false;
            }
        }
        //unchilds the object in hand
        dropitem.transform.SetParent(null);
        //rigidbody becomes affected by gravity again
        dropitem.GetComponent<Rigidbody>().isKinematic = false;
        //Object becomes unusable
        
    }

    public void GetLeftInput(InputAction.CallbackContext context)
    {
        //Lets boolean know its the left hand
        activeHand = Direction.Left;

        if (context.performed && !hasItemLeft)
        {
            //Calls pickupitem with the direction as ovcerload
            PickupItem(activeHand);
        }
        else if(context.performed && hasItemLeft)
        {

            Dropitem(activeHand);
        }
    }

    public void GetRightInput(InputAction.CallbackContext context)
    {
        //makes active hand right
        activeHand = Direction.Right;

        if (context.performed && !hasItemRight)
        {
            //calls pickup with activehand overload
            PickupItem(activeHand);
        }
        else if (context.performed && hasItemRight)
        {
            Dropitem(activeHand);
        }
    }
}
