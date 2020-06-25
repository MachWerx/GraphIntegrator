using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGraph : MonoBehaviour
{
    [SerializeField] private GameObject m_Background;
    [SerializeField] private GameObject m_KnotPrefab;
    [SerializeField] private bool m_Integrate;
    [SerializeField] private InteractiveGraph m_OtherGraph;

    private const int kKnotNum = 12;
    private GameObject[] m_Knots = new GameObject[kKnotNum];
    private float m_Left;
    private float m_Width;
    private float m_Bottom;
    private float m_Height;

    public void OnDrag(Vector2 posXY)
    {
        // localize position
        var pos = new Vector3(
            posXY.x - transform.localPosition.x,
            posXY.y - transform.localPosition.y,
            0);

        // find closest knot index
        int index = (int)Mathf.Clamp(
            (pos.x - m_Left) * (kKnotNum - 1) / m_Width + .5f,
            0, kKnotNum - 1);

        // adjust height
        var localPos = m_Knots[index].transform.localPosition;
        localPos.y = pos.y;
        m_Knots[index].transform.localPosition = localPos;

        if (m_Integrate)
        {
            CalculateIntegral();
        }
        else
        {
            CalculateDerivative();
        }
    }

    private void CalculateDerivative()
    {
        float[] derivativeArray = new float[kKnotNum];
        float dx = 1.0f / kKnotNum;
        float lastY = 0;
        for (int i = 0; i < kKnotNum; i++)
        {
            float y = m_Knots[i].transform.localPosition.y / m_Height;
            derivativeArray[i] = (y - lastY) / dx;
            lastY = y;
        }

        m_OtherGraph.OnUpdateGraph(derivativeArray);
    }

    private void CalculateIntegral()
    {
        float[] integralArray = new float[kKnotNum];
        float runningIntegral = 0.0f;
        float dx = 1.0f / kKnotNum;
        for (int i = 0; i < kKnotNum; i++)
        {
            float y = m_Knots[i].transform.localPosition.y / m_Height;
            runningIntegral += y * dx;
            integralArray[i] = runningIntegral;
        }

        m_OtherGraph.OnUpdateGraph(integralArray);
    }

    private void OnUpdateGraph(float[] integralArray)
    {
        for (int i = 0; i < kKnotNum; i++)
        {
            var localPos = m_Knots[i].transform.localPosition;
            localPos.y = integralArray[i] * m_Height;
            m_Knots[i].transform.localPosition = localPos;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var backgroundPos = m_Background.transform.localPosition;
        var backgroundScale = m_Background.transform.localScale;
        m_Left = backgroundPos.x - 0.5f * backgroundScale.x;
        m_Width = backgroundScale.x;
        m_Bottom = backgroundPos.y - 0.5f * backgroundScale.y;
        m_Height = backgroundScale.y;

        for (int i = 0; i < kKnotNum; i++)
        {
            var knot = Instantiate(m_KnotPrefab, transform);
            knot.transform.localPosition = new Vector3(
                m_Left + i * m_Width / (kKnotNum - 1),
                0,
                0);
            m_Knots[i] = knot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
