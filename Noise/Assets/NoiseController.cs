using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoiseController : MonoBehaviour
{
    public static NoiseController instance;

    private void Awake()
    {
        instance = this;
    }

    public UnityEvent Sound;
    [System.Serializable]
    public class myIntEvent : UnityEvent<int> { }

    // Start is called before the first frame update
    void Start()
    {
        if (Sound == null)
        {
            Sound = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
