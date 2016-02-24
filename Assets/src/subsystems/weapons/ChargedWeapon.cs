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
    public float chargeTime; //How long it takes the weapon to fire
    //public ParticleSystem chargingParticles; //A partical system to draw while the weapon is charging.
    
    public ChargedWeaponStateCharging() {
        Debug.Log("Began Charging");
    }
    
    public void handleInput(ChargedWeapon weapon) {
        if(!Input.GetButtonDown("Fire1")) {
            weapon.currentState = new ChargedWeaponStateIdle();
        }
    }
    
    public void update(ChargedWeapon weapon) {
        weapon.currentChargeTime += Time.deltaTime;
        if (weapon.currentChargeTime >= chargeTime) {
            weapon.currentState = new ChargedWeaponStateFiring();
        }
    }
    
    ~ChargedWeaponStateCharging() {
        //Stop animating particle system.
    }
}

class ChargedWeaponStateFiring : ChargedWeaponState {
    private float maxFireTime;
    private float currentFireTime;
    
    public ChargedWeaponStateFiring() {
        currentFireTime = 0F;
        Debug.Log("Began Firing");
    }
    
    public void handleInput(ChargedWeapon weapon) {}
    
    public void update(ChargedWeapon weapon) {
        GameObject.Instantiate(weapon.projectile, new Vector3(0F,0F,0F), weapon.transform.rotation);
    }
}

class ChargedWeaponStateIdle : ChargedWeaponState {
    
    public ChargedWeaponStateIdle() {
        Debug.Log("Began Idle");
    }
    
    public void handleInput(ChargedWeapon weapon) {
        if (Input.GetButtonDown("Fire1")) {
            weapon.currentState = new ChargedWeaponStateCharging();
        }
    }
    
    public void update(ChargedWeapon weapon) {}
}

class ChargedWeaponStateDischarging : ChargedWeaponState {
    
    public ChargedWeaponStateDischarging() {
        Debug.Log("Began Discharging");
    }
    
    public void handleInput(ChargedWeapon weapon) {
        if (Input.GetButtonDown("Fire1")) {
            weapon.currentState = new ChargedWeaponStateCharging();
        }
    }
    
    public void update(ChargedWeapon weapon) {
        weapon.currentChargeTime -= Time.deltaTime;
        if (weapon.currentChargeTime <= 0) {
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