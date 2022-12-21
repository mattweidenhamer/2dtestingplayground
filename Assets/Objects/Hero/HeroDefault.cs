using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDefault : MonoBehaviour
{
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material knockbackMaterial;
    public int hitpoints = 3;
    [SerializeField] float damageHitDuration;
    //One hitpoint should be equal to a quarter or half a heart
    
    [SerializeField] float timeUntilChainStops = 3f;
    private int chainedHits = 0;
    private bool checkingForChain = false;
    private Flasher myFlasher;
    private void FixedUpdate() {
        //TODO in the future when making these, split as much code as psosible into their own fuctions for maximum modularity
    }
    private void Start() {
        myFlasher = GetComponent<Flasher>();
    }

    void onHit(GameObject attack){
        if(!checkingForChain){
            StartCoroutine("falloffOnHit");
            checkingForChain = true;
        }
        switch(chainedHits++){
            case 1:
                //Only take damage
                takeDamage(attack);
                break;
            case 2:
                //Damage and slight knockback
                break;
            case 3:
                //Damage and reeling
                break;
            case 4:
                //Damage and become bouncy
                break;
            default:
                break;
        }
    }
    public void takeDamage(GameObject attack){
        //If we ever do something like defense modifiers it would be here
        int damageToTake = attack.GetComponent<Fireball>().damage;
        if(damageToTake > 0){
            hitpoints -= damageToTake;
            if(hitpoints <= 0){
                //Die
            }
            myFlasher.startFlasher(damageHitDuration);

        }
    }
    IEnumerator falloffOnHit(){
        yield return new WaitForSeconds(timeUntilChainStops);
        chainedHits = 0;
        checkingForChain = false;
    }
    IEnumerator knockbackBounce(){
         yield return null;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "EnemyAttack"){
            onHit(other.gameObject);
        }
    }
}
