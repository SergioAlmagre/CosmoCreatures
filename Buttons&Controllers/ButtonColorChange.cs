using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChange : MonoBehaviour
{   
    public Material defaultMaterial;
    public Material pressedMaterial;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
 


   

    }


    public void OnPointerDown()
    {
        button.GetComponent<Renderer>().material = pressedMaterial;
    }

    public void OnPointerUp()
    {
        button.GetComponent<Renderer>().material = defaultMaterial;
    }



}
