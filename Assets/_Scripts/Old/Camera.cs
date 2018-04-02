using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]private Transform[] m_Targets;
    private Vector3 m_Offset;

    private void Start()
    {
        m_Offset = this.transform.position - m_Targets[0].position;
    }

    private void LateUpdate()
    {
        this.transform.position = m_Offset + m_Targets[0].position;
    }
}
