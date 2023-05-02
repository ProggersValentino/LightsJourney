using UnityEngine.SceneManagement;
using UnityEngine;
 
public class SceneManage : MonoBehaviour
{
        void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadScene("WinScreen");
        }
 
    
}