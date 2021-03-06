﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchPlane : UIBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] private Camera m_MainCam;
    [SerializeField] private InteractiveGraph m_LeftGraph;
    [SerializeField] private InteractiveGraph m_RightGraph;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = new Vector2(
            2 * m_MainCam.orthographicSize * m_MainCam.aspect * (eventData.position.x / Screen.width - 0.5f),
            2 * m_MainCam.orthographicSize * (eventData.position.y / Screen.height - 0.5f));
        if (pos.x < 0)
        {
            m_LeftGraph.OnDrag(pos);
        }
        else
        {
            m_RightGraph.OnDrag(pos);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
}
