using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGraph : MonoBehaviour
{
    [SerializeField] private GameObject m_Background;
    [SerializeField] private GameObject m_KnotPrefab;

    private const int kKnotNum = 20;
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
                m_Bottom + i * m_Height / (kKnotNum - 1),
                0);
            m_Knots[i] = knot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
