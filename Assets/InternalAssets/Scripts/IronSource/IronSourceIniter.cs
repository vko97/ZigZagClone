using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceIniter : MonoBehaviour
{
    private void Awake()
    {
        IronSource.Agent.init("e8a43159");
        IronSource.Agent.validateIntegration();
    }
}
