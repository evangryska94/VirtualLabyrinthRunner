﻿using UnityEngine;
using System.Collections;

public class pickup : MonoBehaviour {
    public int distance;
    public GameObject mydefault;
    private int counter;
	// Use this for initialization
	void Start () {
        distance = 5;
    }
	
	// Update is called once per frame
	void Update () {
        counter = 1;
        Collect();   
	}
    void Collect()
    {
        if (Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, distance))
            {
                if(hit.collider.gameObject.tag == "item")
                {
                    Debug.Log("picked");
                    hit.collider.gameObject.transform.parent = this.transform;
                    hit.collider.gameObject.transform.position = mydefault.gameObject.transform.position;
                    hit.collider.gameObject.transform.rotation = mydefault.gameObject.transform.rotation;
                    hit.collider.gameObject.active = false;
                    foreach (Transform child in transform)
                    {
                        //Debug.Log(child.tag);
                        if (child.tag == "item")
                        {
                            child.name = counter.ToString();
                            counter += 1;
                        }
                    }
                }
            }

        }
    }
}
