﻿using UnityEngine;
using System.Collections;

public class BoltShot : MonoBehaviour {
    public float speed;

	void Start () {
   
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
	
	void Update () {
	
	}
}
