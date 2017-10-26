using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class End : MonoBehaviour {

    [SerializeField]
    private Canvas end;
	// Use this for initialization
	void Start () {
        end = end.GetComponent<Canvas>();
        end.enabled = false;
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            end.enabled = true;
            StartCoroutine("Wait2");
            
        }
    }
    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(5);
        Application.Quit();
        
    }
}
