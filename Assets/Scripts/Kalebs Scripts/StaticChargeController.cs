using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaticChargeController : MonoBehaviour
{
    public bool hasCharge = true;

    public GameObject chargeIndicator;

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
            SendStaticCharge();
        }
    }

    void SendStaticCharge()
    {
        if (inCooldown) return;

        StartCoroutine(Cooldown());

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, ~LayerMask.GetMask("Player")))
        {
            if (hit.collider.gameObject.CompareTag("Powerable"))
            {
                hasCharge = hit.collider.gameObject.GetComponent<PowerableInterface>().FlipPower(hasCharge);

                if (hasCharge)
                {
                    // Turn red
                    SetEmission(Color.red, chargeIndicator);
                }
                else
                {
                    // Turn black
                    SetEmission(Color.black, chargeIndicator);
                }
            }
        }
    }

    private IEnumerator Cooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(1.0f);
        inCooldown = false;
    }

    void SetEmission(Color color, GameObject obj)
    {
        var mat = obj.GetComponent<Renderer>();
        mat.material.color = color;
        mat.material.EnableKeyword("_EMISSION");
        mat.material.SetColor("_EmissionColor", color);
    }
}
