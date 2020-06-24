using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchPlane : UIBehaviour, IDragHandler
{
    [SerializeField] private Camera m_MainCam;
    [SerializeField] private InteractiveGraph m_LeftGraph;
    [SerializeField] private InteractiveGraph m_RightGraph;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = new Vector2(
            m_MainCam.orthographicSize * (eventData.position.x / Screen.width - 0.5f),
            -m_MainCam.orthographicSize * (eventData.position.y / Screen.height - 0.5f));
        if (pos.x < 0)
        {
            m_LeftGraph.OnDrag(pos);
        }
        else
        {
            m_RightGraph.OnDrag(pos);
        }
    }
}
