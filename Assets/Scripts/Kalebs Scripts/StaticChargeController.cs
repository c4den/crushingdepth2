using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticChargeController : MonoBehaviour
{
    public bool hasCharge = true;

    GameObject cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SendStaticCharge();
        }
    }

    void SendStaticCharge()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Powerable"))
            {
                hasCharge = hit.collider.gameObject.GetComponent<PowerableInterface>().FlipPower(hasCharge);
            }
        }
    }
}
