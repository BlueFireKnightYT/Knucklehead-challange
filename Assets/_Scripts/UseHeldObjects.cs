using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class UseHeldObjects : MonoBehaviour
{
    //reference interface
    IUsable useInterface;
    //reference pickup script
    PickupDrop pickup;
    Direction hand;
    private void Start()
    {
        //get the pickup script
        pickup = GetComponent<PickupDrop>();
    }

    public void Left(InputAction.CallbackContext context)
    {
        //get left held object from pickup script
        GameObject heldLeft = pickup.heldItemLeft;
        hand = Direction.Left;

        //call useobject
        UseObject(heldLeft);
    }

    public void Right(InputAction.CallbackContext context)
    {
        //Get  right held object from pickup script
        GameObject heldRight = pickup.heldItemRight;
        hand = Direction.Right;

        //call useobject
        UseObject(heldRight);
    }

    void UseObject(GameObject heldObject)
    {
        //check if its not null
        if(heldObject != null)
        {
            //get interface from object
            useInterface = heldObject.GetComponent<IUsable>();
            //call interface.UseItem
            useInterface.UseItem(hand, pickup);
        }
    }
}
