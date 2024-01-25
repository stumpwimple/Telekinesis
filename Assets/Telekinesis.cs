using UnityEngine;
using System.Collections;
using System;

public class Telekinesis : MonoBehaviour {

    SteamVR_TrackedObject trackedObj; //Tracked Object of Controller
    SteamVR_Controller.Device device; //

    Transform player;
    Rigidbody rbPlayer;
    Transform controller;
    public Transform hmd;

    RaycastHit hit;

    public Rigidbody controlledObject;
    public Vector3 controlledObjectXZ;


    public GameObject targetDot;
    public GameObject targetDot2;
    private bool dotOn=false;
    private bool dot2On = false;

    public float attackRange;
    public float telekinesisSpin=10;
    public float heldDistance;
    public float powerLevel=5f;
    public float throwPowerLevel = 5f;

    void Awake ()
    {

        player = transform.parent; //transform of CameraRig
        rbPlayer = player.GetComponent<Rigidbody>(); //rigidbody of CameraRig
        controller = this.transform; //controller Transform
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        /*if (hmd == null)
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
        }*/


    }



    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        //PlaceDotOnGaze();
        //PlaceDotOnPoint();
        PlaceDotOnPalm();
        //GazeGrabandMoveTelekinesis();
        //GazeGrabMotionMoveTelekinesis();
        //MotionGrabandMoveTelekinesis();
        MotionGrabandMovePalmTelekinesis();




        /*Common needs
         *         
        RaycastHit hit;
        Ray handBeam = new Ray(controller.position, controller.rotation*Vector3.forward);
        if (Physics.Raycast(handBeam, out hit, attackRange)){}

        Rigidbody fireballClone = (Rigidbody)Instantiate(fireballInstance, controller.position, controller.rotation);
        fireballClone.GetComponent<Rigidbody>().AddForce(controller.rotation *Vector3.forward*powerLevel);*/

    }


    private void PlaceDotOnGaze()
    {
        Ray faceBeam = new Ray(hmd.position, hmd.rotation * Vector3.forward);
        if (Physics.Raycast(faceBeam, out hit, attackRange))
        {
            targetDot.SetActive(true);

            targetDot.transform.position = hit.point;
            targetDot.transform.localScale = Vector3.one*(hit.distance*.01f);
        }
        else { targetDot2.SetActive(false); }
    }
    private void PlaceDotOnPoint()
    {
        Ray handBeam = new Ray(controller.position, controller.rotation * Vector3.forward);
        if (Physics.Raycast(handBeam, out hit, attackRange))
        {
            targetDot2.SetActive(true);

            targetDot2.transform.position = hit.point;
            targetDot2.transform.localScale = Vector3.one*(hit.distance*.01f);
        }
        else { targetDot2.SetActive(false); }
    }
    private void PlaceDotOnPalm()
    {
        Ray handBeam = new Ray(controller.position, controller.rotation * Vector3.down);
        if (Physics.Raycast(handBeam, out hit, attackRange))
        {
            targetDot2.SetActive(true);

            targetDot2.transform.position = hit.point;
            targetDot2.transform.localScale = Vector3.one * (hit.distance * .01f);
        }
        else { targetDot2.SetActive(false); }
    }
    private void GazeGrabandMoveTelekinesis()
    {

        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (controlledObject == null)
            {
                controlledObject = hit.rigidbody;
                heldDistance = controlledObject.position.magnitude;
                controlledObject.useGravity = false;
                controlledObject.GetComponent<BoxCollider>().size *= .75f;
                controlledObject.AddTorque(UnityEngine.Random.insideUnitSphere * telekinesisSpin, ForceMode.Acceleration);
            }
            else
            {
                //if (controlledObject.position.y < 1) { controlledObject.position = Vector3.Lerp(controlledObject.position, controlledObject.position+Vector3.up*3, .01f); }
                //else {  }    
                //controlledObject.position = Vector3.Lerp(controlledObject.position, hmd.rotation * Vector3.forward*heldDistance, .01f); //
                controlledObject.velocity = (hmd.rotation * Vector3.forward * heldDistance - controlledObject.GetComponent<Rigidbody>().transform.position) * powerLevel;

            }



        }
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {

        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {

            controlledObject.useGravity = true;
            controlledObject.GetComponent<BoxCollider>().size = Vector3.one;
            controlledObject.velocity *= throwPowerLevel;
            controlledObject = null;

        }
    }

    private void GazeGrabMotionMoveTelekinesis()
    {
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (controlledObject == null)
            {
                controlledObject = hit.rigidbody;
                heldDistance = controlledObject.position.magnitude;
                controlledObject.useGravity = false;
                controlledObject.GetComponent<BoxCollider>().size *= .75f;
                controlledObject.AddTorque(UnityEngine.Random.insideUnitSphere * telekinesisSpin, ForceMode.Acceleration);
            }
            else
            {
                controlledObject.velocity = (controller.rotation * Vector3.forward * heldDistance - controlledObject.GetComponent<Rigidbody>().transform.position) * powerLevel;

            }
        }
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {

        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {

            controlledObject.useGravity = true;
            controlledObject.GetComponent<BoxCollider>().size = Vector3.one;
            controlledObject.velocity *= throwPowerLevel;
            controlledObject = null;

        }
    }

    private void MotionGrabandMoveTelekinesis()
    {
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (controlledObject == null)
            {
                controlledObject = hit.rigidbody;
                heldDistance = controlledObject.position.magnitude;
                controlledObject.useGravity = false;
                controlledObject.GetComponent<BoxCollider>().size *= .75f;
                controlledObject.AddTorque(UnityEngine.Random.insideUnitSphere * telekinesisSpin, ForceMode.Acceleration);
            }
            else
            {




            }
        }
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {

        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {

            controlledObject.useGravity = true;
            controlledObject.GetComponent<BoxCollider>().size = Vector3.one;
            controlledObject.velocity *= throwPowerLevel;
            controlledObject = null;

        }
    }
    private void MotionGrabandMovePalmTelekinesis()
    {
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (controlledObject == null)
            {
                controlledObject = hit.rigidbody;
                heldDistance = controlledObject.position.magnitude;
                controlledObject.useGravity = false;
                controlledObject.GetComponent<BoxCollider>().size *= .75f;
                controlledObject.AddTorque(UnityEngine.Random.insideUnitSphere * telekinesisSpin, ForceMode.Acceleration);
            }
            else
            {




            }
        }
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {

        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {

            controlledObject.useGravity = true;
            controlledObject.GetComponent<BoxCollider>().size = Vector3.one;
            controlledObject.velocity *= throwPowerLevel;
            controlledObject = null;

        }
    }

}
