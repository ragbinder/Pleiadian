//This class will be used for weapons that do not fire as soon as their fire button is pressed
//but rather require the fire button to be held down for a certain interval, and then will
//begin firing/discharging when the charge time is reached.

using UnityEngine;
using System.Collections;

class ChargedWeaponState {
    public virtual void handleInput(ChargedWeapon weapon) {}
    public virtual void update(ChargedWeapon weapon) {}
}

class ChargedWeaponStateCharging : ChargedWeaponState {
    public float chargeTime = 2.0F; //How long it takes the weapon to fire
    private float currentChargeTime; //How long the weapon has been charging
    
    public ChargedWeaponStateCharging() {
        Debug.Log("Began Charging");
        currentChargeTime = 0F;
    }
    
    public override void handleInput(ChargedWeapon weapon) {
        if (Input.GetButton("Fire1")) {
            currentChargeTime += Time.deltaTime;
            if (currentChargeTime >= chargeTime) {
                weapon.currentState = new ChargedWeaponStateFiring();
            }
        }
        else {
            currentChargeTime -= Time.deltaTime;
            if (currentChargeTime <= 0) {
                weapon.currentState = new ChargedWeaponStateIdle();
            }
        }
        Debug.Log("Charged For: " + currentChargeTime + " / " + chargeTime);
    }
}

class ChargedWeaponStateFiring : ChargedWeaponState {
    
    public ChargedWeaponStateFiring() {
        Debug.Log("Began Firing");
    }
    
    public override void update(ChargedWeapon weapon) {
        GameObject.Instantiate(weapon.projectile, weapon.transform.parent.transform.position, weapon.transform.parent.rotation);
        weapon.currentState = new ChargedWeaponStateCooldown();
    }
}

class ChargedWeaponStateIdle : ChargedWeaponState {
    
    public ChargedWeaponStateIdle() {
        Debug.Log("Began Idle");
    }
    
    public override void handleInput(ChargedWeapon weapon) {
        if (Input.GetButton("Fire1")) {
            weapon.currentState = new ChargedWeaponStateCharging();
        }
    }
}

class ChargedWeaponStateCooldown : ChargedWeaponState {
    public float cooldown = 1.0F;
    private float currentCooldown;
    
    public ChargedWeaponStateCooldown() {
        Debug.Log("Began Cooldown");
        currentCooldown = 0F;
    }
    
    public override void update(ChargedWeapon weapon) {
        currentCooldown += Time.deltaTime;
        
        if (currentCooldown >= cooldown) {
            weapon.currentState = new ChargedWeaponStateIdle();
        }
    }
}

public class ChargedWeapon : Weapon {
    public Vector3 localTransform; //Where the weapon is on the ship
    public Transform projectile; //The projectile that is spawned when the weapon fires
    
    internal ChargedWeaponState currentState;
    internal float currentChargeTime; //How long the weapon has been charging
    
	// Use this for initialization
	void Start () {
        currentState = new ChargedWeaponStateIdle();
	}
	
	// Update is called once per frame
	void Update () {
        currentState.handleInput(this);
	    currentState.update(this);
	}
}