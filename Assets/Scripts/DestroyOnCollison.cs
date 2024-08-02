using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField ] GameObject Particles;
    // This function is called when another collider enters the trigger collider attached to the object this script is attached to.
    void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with this one is the player.
        if (collision.gameObject.tag == "Player")
        {
            // Destroy this game object after 2 seconds.
            Destroy(gameObject, 10.0f);
             GameObject Exp=Instantiate(Particles,transform.position,transform.rotation);
            Destroy(Exp,0.5f);
            }        
    }
}
