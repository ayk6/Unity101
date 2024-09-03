using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public float health, sawSpeed;

    public bool isDead=false;

    Transform sawLauncher, floatingText, bloodParicle;

    public Transform saw;

    bool mouseIsOverUI;
    // Start is called before the first frame update
    void Start()
    {
        sawLauncher = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        mouseIsOverUI = EventSystem.current.currentSelectedGameObject == null;
        if( Input.GetMouseButtonDown(0) && mouseIsOverUI)
        {
            ThrowSaw();
        }
    }

    public void GetDamage(float damage)
    {
        Instantiate(floatingText, transform.position, Quaternion.identity).GetComponent<TextMesh>().text = damage.ToString();
        if (health >= damage)
        {
            health -= damage;
        }
        else { health = 0; }

        DeathCheck();
    }

    void DeathCheck()
    {
        if (health == 0) {
            Destroy(Instantiate(bloodParicle, transform.position, Quaternion.identity), 3f);
            DataManager.instance.loseProcess();
            isDead = true;
            Destroy(gameObject); 
        }
    }

    void ThrowSaw()
    {
        Transform tempSaw;
        tempSaw = Instantiate(saw, sawLauncher.position, Quaternion.identity);
        tempSaw.GetComponent<Rigidbody2D>().AddForce(sawLauncher.forward * sawSpeed);
    }
}
