using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{

	public float health;
	public float damage;

	bool isColliderBusy = false;

	public Slider slider;
	// Start is called before the first frame update
	void Start()
    {
		slider.maxValue = health;
		slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !isColliderBusy) {
			isColliderBusy = true;
			collision.GetComponent<PlayerManager>().GetDamage(damage);	
		}
		else if (collision.CompareTag("Saw"))
			{ 
				GetDamage(collision.GetComponent<SawManager>().sawDamage);
				Destroy(collision.gameObject);
			}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isColliderBusy = false;
		}
	}

	public void GetDamage(float damage)
	{
		if (health >= damage)
		{
			health -= damage;
		}
		else { health = 0; }
		slider.value = health;
		DeathCheck();
	}

	void DeathCheck()
	{
		if (health == 0)
		{
			Destroy(gameObject);
		}
	}
}
