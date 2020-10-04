using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake () {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Invoke("loadFirstScene", 10.5f);
    }

    // Update is called once per frame
    void loadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
