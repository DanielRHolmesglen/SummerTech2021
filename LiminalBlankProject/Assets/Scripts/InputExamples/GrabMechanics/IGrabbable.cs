using UnityEngine;
using System.Collections;
/// <summary>
/// any object that the player can grab, either by pointer or collision and buttons, should
/// access this interface. To use this add the following lines to a script that a grabable object uses
/// 1. ", IGrabbable" directly after MonoBehaviour in the class declaratione. e.g. "public class BoxGrabber : Monobehaviour, IGrabbable"
/// 2. include a public function "Grab()" in the script. This is the function the interface will trigger.
/// fill this function with whatever you want, or leave the function blank, since IGrabbable is mostly being used as a pseudo tag
/// </summary>
public interface IGrabbable
{
    void Grab();
}
