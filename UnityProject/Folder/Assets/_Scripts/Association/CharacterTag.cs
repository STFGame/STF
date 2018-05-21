using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTag : MonoBehaviour
{
    [SerializeField] private Transform m_Target = null;

    private RectTransform m_RectTransform;

    // Use this for initialization
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 viewport = Camera.main.WorldToViewportPoint(m_Target.position);
        m_RectTransform.anchorMin = viewport;
        m_RectTransform.anchorMax = viewport;
    }
}
