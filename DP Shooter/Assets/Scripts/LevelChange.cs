using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class LevelChange : MonoBehaviour
{
    public string LevelName;
    //Vignette vig;
    //Bloom bloom;
    //public PostProcessVolume postProcess;

    private void Start()
    {
        //postProcess = gameObject.GetComponent<PostProcessVolume>();
        //postProcess.profile.TryGetSettings(out bloom); //<< 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(LevelName);
            //StartCoroutine(TransitionLevel());
        }
    }

    //private IEnumerator TransitionLevel()
    //{
    //    if (bloom != null)
    //    {
    //        bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 15, 0.5f * Time.deltaTime); ////<<
    //    }
    //    yield return new WaitForSeconds(0.5f);
    //    SceneManager.LoadScene(LevelName);
    //}
}
