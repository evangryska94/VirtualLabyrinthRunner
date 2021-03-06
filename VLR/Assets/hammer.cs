﻿using UnityEngine;
using System.Collections;

public class hammer : MonoBehaviour
{
    public Material wood;
    public Material metal;
    public Material glass;
    public int hitsToKill;

    private float swingAngle;
    private bool swinging;
    private bool backSwing;
    private int numHits;

    private bool rising;
    private float height;

    private GameObject centerEye;

    public MazeBroken brokenPrefab;

    // Use this for initialization
    void Start()
    {
        height = 0;
        rising = true;
        swingAngle = 0.0f;
        swinging = false;
        backSwing = false;
        numHits = 0;
        centerEye = GameObject.Find("OVRPlayerVLR/OVRCameraRig/TrackingSpace/CenterEyeAnchor");
    }

    // Update is called once per frame
    void Update()
    {
        if(tag == "item")
        {
            Vector3 oldRot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(oldRot.x, oldRot.y + Time.deltaTime*20, oldRot.z));
            if (rising)
            {
                height += Time.deltaTime;
                transform.position += new Vector3(0, Time.deltaTime, 0);
                if (height > 2.5f)
                {
                    rising = false;
                }
            }
        } else if ((Input.GetMouseButtonDown(0) || Input.GetAxis("Oculus_GearVR_RIndexTrigger") > 0.3f) && tag == "inventory")
        {
            swinging = true;
            backSwing = false;
            breakWall();
        }

        if (swinging)
        {
            if (swingAngle < 40 && !backSwing)
            {
                swingAngle += Time.deltaTime * 200;
                transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x + Time.deltaTime * 200, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
            }
            else
            {
                backSwing = true;
                swingAngle -= Time.deltaTime * 100;
                transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x + Time.deltaTime * -100, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
            }
            if (swingAngle <= 0)
            {
                swinging = false;
            }
        }

    }

    private void breakWall()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out hit, 5))
        if (Physics.Raycast(centerEye.transform.position, centerEye.transform.forward, out hit, 5))
        {
            if (hit.collider.gameObject.tag == "breakableWall")
            {
                numHits++;
                if(numHits == hitsToKill)
                {
                    /*Transform t = hit.transform;
                    MazeBroken mazeObject = Instantiate(brokenPrefab) as MazeBroken;
                    (mazeObject).name = brokenPrefab.name + " " + t.position.x + ", " + t.position.z;
                    mazeObject.transform.rotation = Quaternion.Euler(new Vector3(t.rotation.x, t.rotation.y, t.rotation.z));
                    mazeObject.transform.position = new Vector3(t.position.x, -.2f, t.position.z);
                    //set material
                    string matName = hit.collider.gameObject.GetComponent<Renderer>().material.name;
                    if (matName.Contains("Wood"))
                    {
                        mazeObject.transform.GetChild(0).GetComponent<Renderer>().material = wood;
                    } else if (matName.Contains("pattern 35"))
                    {
                        mazeObject.transform.GetChild(0).GetComponent<Renderer>().material = metal;
                    }
                    else
                    {
                        mazeObject.transform.GetChild(0).GetComponent<Renderer>().material = glass;
                    }*/
                    Destroy(hit.collider.gameObject);
                    numHits = 0;
                }
            }
            else
            {
                numHits = 0;
            }
        }
    }
}
