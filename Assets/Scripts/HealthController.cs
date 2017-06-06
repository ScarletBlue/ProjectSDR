using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	public int maxHealth;

	public HealthBarController healthBarController;

	int health;
	public int GetHealth() {
		return health;
	}

	public delegate void CharacterDied();
	public event CharacterDied OnCharacterDeath;

	void Start() {
		health = maxHealth;

		UpdateHealthBar ();
	}

	public void TakeDamage(int damage) {
		health -= damage;

		if (health < 0) {
			health = 0;
		}

		UpdateHealthBar ();

		if (health == 0) {
			if(OnCharacterDeath != null) {
				OnCharacterDeath();
			}
		}
	}

	public void Heal(int amount) {
		health += amount;

		if (health > maxHealth) {
			health = maxHealth;
		}

		UpdateHealthBar ();
	}

	void UpdateHealthBar() {
		float percentOfHealth = (float) health / maxHealth;
		healthBarController.setHealthBar (percentOfHealth);
	}
}
