using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputTest : MonoBehaviour
{
    private void Update()
    {
        if (SwipeInput.Instance.Tap)
            Debug.Log("Tap");
        if (SwipeInput.Instance.DoubleTap)
            Debug.Log("DoubleTap");
        if (SwipeInput.Instance.SwipeUp)
            Debug.Log("SwipeUp");
        if (SwipeInput.Instance.SwipeDown)
            Debug.Log("SwipeDown");
        if (SwipeInput.Instance.SwipeLeft)
            Debug.Log("SwipeLeft");
        if (SwipeInput.Instance.SwipeRight)
            Debug.Log("SwipeRight");
    }
}
