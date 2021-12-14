using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this is an example of the script using IGrabbable.
/// put this on the parent object of an item you want to pick up
/// </summary>
public class ItemToGrab : MonoBehaviour, IGrabbable
{
   public void Grab()
   {
        //replace this with any functionality you want to run when the hand hovers over this object.
        Debug.Log("pickup " + transform.name);
   }
}
