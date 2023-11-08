using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public TextMeshProUGUI Name;

    private void Start()
    {
        Name.text = "Привет";
    }
}
