using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaticChargeController : MonoBehaviour
{
    public bool hasCharge = true;

    bool inCooldown = false;

    GameObject cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKey("joystick button 2"))
        {
            print("Static charge shot");
            SendStaticCharge();
        }
    }

    void SendStaticCharge()
    {
        if (inCooldown) return;

        StartCoroutine(Cooldown());

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Powerable"))
            {
                hasCharge = hit.collider.gameObject.GetComponent<PowerableInterface>().FlipPower(hasCharge);
            }
        }
    }

    private IEnumerator Cooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(1.0f);
        inCooldown = false;
    }
}
