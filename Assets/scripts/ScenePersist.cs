using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int startingIndex;

    private void Awake()
    {
        int scenePersistNum = FindObjectsOfType<ScenePersist>().Length;

        if(scenePersistNum > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        startingIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if ( startingIndex != currentIndex)
        {
            Destroy(gameObject);
        }
    }
}
