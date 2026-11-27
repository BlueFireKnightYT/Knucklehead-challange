using UnityEngine;

public class Flashlight : MonoBehaviour, IUsable
{
    //reference light
    Light light;

    bool isOn = false;

    private void Start()
    {
        //get light
        light = GetComponent<Light>();

        //security check
        if(light.enabled == true)
        {
            light.enabled = false;
        }
    }
    public void UseItem()
    {
        //if isOn = true
        if(isOn)
        {
            //set light to inactive
            light.enabled = false;
            //set isOn to false
            isOn = false;
        }
        //if isOn = false
        else
        {
            //set light to active
            light.enabled = true;
            //set isOn to true
            isOn = true;
        }


    }
}
