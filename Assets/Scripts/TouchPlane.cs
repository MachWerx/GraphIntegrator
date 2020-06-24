using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchPlane : UIBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting touch plane");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"position: {eventData.position}");
    }
}
