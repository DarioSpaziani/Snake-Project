using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject buttons;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        buttons.SetActive(true);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
