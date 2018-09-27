using System;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private List<Action> _handlers = new List<Action>(10000);

    private static UpdateManager _instance;

    public static UpdateManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<UpdateManager>();
            return _instance;
        }
    }

    public event Action Updated;

    public void Subscribe(Action handler)
    {
        if (handler != null)
            _handlers.Add(handler);
    }

    void Update()
    {
        //Updated?.Invoke();

        if (Updated != null) Updated();

        //foreach (var handler in _handlers)
        //{
        //    handler();
        //}
    }
}
