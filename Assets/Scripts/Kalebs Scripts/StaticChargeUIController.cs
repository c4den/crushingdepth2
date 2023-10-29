using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaticChargeUIController : MonoBehaviour
{
    [SerializeField] StaticChargeController staticChargeController;

    [SerializeField] TextMeshProUGUI staticChargeUIText;
    

    // Update is called once per frame
    void Update()
    {
        if (staticChargeController.hasCharge)
        {
            staticChargeUIText.text = "Is Charged: True";
        }
        else
        {
            staticChargeUIText.text = "Is Charged: False";
        }
    }
}
