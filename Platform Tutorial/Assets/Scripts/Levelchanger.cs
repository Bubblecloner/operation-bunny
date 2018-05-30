
using UnityEngine;

public class Levelchanger : MonoBehaviour {

    public Animator animator;

	void Update ()

        private void OnTriggerEnter2D(Collider2D exit)

    {
        FadeToLevel(6);    
    }
  

    public void FadeToLevel (int levelIndex)
    {
        animator.SetTrigger("blackfadeout");
    }
}
