using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tornado : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform tornadoCenter;
    public float pullforce;
    public float refreshRate;
    
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "OBJ")
            {
                StartCoroutine(pullObject(other, true));
                Debug.Log("collided with player");
                other.GetComponent<PacMovement>().enabled=false;
                
                other.GetComponent<Rigidbody>().isKinematic = false;
                Invoke("endGame", 4.0f);
        }
        }

        public void endGame()
        {
            SceneManager.LoadScene("RestartMenu");
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "OBJ")
            {
                StartCoroutine(pullObject(other, false));
            }
        }

        IEnumerator pullObject(Collider x, bool shouldPull)
        {
            if(shouldPull)
            {
                Vector3 ForDir = tornadoCenter.position - x.transform.position;
                x.GetComponent<Rigidbody>().AddForce(ForDir.normalized * pullforce * Time.deltaTime);
                yield return refreshRate;
                StartCoroutine(pullObject(x, shouldPull));
            }
        }

    
}
