using UnityEngine;
using System.Collections;

public class VRController : MonoBehaviour {

    SteamVR_TrackedObject trackedObj; //Tracked Object of Controller
    SteamVR_Controller.Device device; //
    SteamVR_Controller.Device headset;

    Transform player;
    Rigidbody rbPlayer;
    Transform controller;
    Transform hmd;

    void Awake ()
    {

        player = transform.parent; //transform of CameraRig
        rbPlayer = player.GetComponent<Rigidbody>(); //rigidbody of CameraRig
        controller = this.transform; //controller Transform
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }



    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (hmd == null)
        {
            SteamVR_TrackedObject[] trackedObjects = FindObjectsOfType<SteamVR_TrackedObject>();
            foreach (SteamVR_TrackedObject tracked in trackedObjects)
            {
                if (tracked.index == SteamVR_TrackedObject.EIndex.Hmd)
                {
                    hmd = tracked.transform;
                    break;
                }
            }
        }



        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {

        }
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {

        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {

        }

        /*Common needs
         *         
        RaycastHit hit;
        Ray handBeam = new Ray(controller.position, controller.rotation*Vector3.forward);
        if (Physics.Raycast(handBeam, out hit, attackRange)){}

        Rigidbody fireballClone = (Rigidbody)Instantiate(fireballInstance, controller.position, controller.rotation);
        fireballClone.GetComponent<Rigidbody>().AddForce(controller.rotation *Vector3.forward*powerLevel);
        */


    }


}

