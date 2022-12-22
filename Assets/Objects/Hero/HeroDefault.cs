using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDefault : MonoBehaviour
{
    //TODO: Clean code
    private PhysicsMaterial2D defaultMaterial;
    private Rigidbody2D myBody;
    [SerializeField] PhysicsMaterial2D knockbackMaterial;
    public int hitpoints = 3;
    [SerializeField] float flashOnHitDuration;
    //One hitpoint should be equal to a quarter or half a heart
    
    [SerializeField] float timeUntilChainCheckingStops = 3f;
    [SerializeField] float disableDurationOnMaxCombo = 5f;
    private int chainedHits = 0;
    private bool checkingForChain = false;
    private Flasher myFlasher;
    private bool invuln = false;
    [HideInInspector] public bool alive = true;

    [SerializeField] Vector2 impact2Hits;
    [SerializeField] Vector2 impact3Hits;
    [SerializeField] Vector2 impact4Hits;
    [SerializeField] Vector2 knockbackModifier;
    private float defaultDrag;
    private void FixedUpdate() {
        //TODO in the future when making these, split as much code as psosible into their own fuctions for maximum modularity
    }
    private void Start() {
        defaultMaterial = GetComponent<Rigidbody2D>().sharedMaterial;
        myFlasher = GetComponent<Flasher>();
        myBody = GetComponent<Rigidbody2D>();
        defaultDrag = myBody.drag;
    }

    void onHit(Collision2D attack){
        Debug.Log("Got hit");
        if(!checkingForChain){
            Debug.Log("Starting falloff subroutine");
            StartCoroutine("falloffOnHit");
            checkingForChain = true;
        }
        switch(chainedHits++){
            case 0:
                Debug.Log("1 Chained hit");
                //Only take damage
                takeDamage(attack);
                break;
            case 1:
                Debug.Log("2 Chained hit");
                //Damage and slight knockback
                takeDamage(attack);
                if(alive){
                    takeKnockback(attack, impact2Hits);
                }
                break;
            case 2:
                Debug.Log("3 Chained hit");
                //Damage and reeling
                takeDamage(attack);
                if(alive){
                    takeKnockback(attack, impact3Hits);
                }
                break;
            case 3:
                Debug.Log("4 Chained hit");
                //Damage and become bouncy
                takeDamage(attack);
                if(alive){
                    StartCoroutine(becomeBouncy());
                    takeKnockback(attack, impact4Hits);               
                }
                break;
            default:
                Debug.Log("Fell through the case statement!");
                break;
        }
    }
    public void takeDamage(Collision2D attack){
        //If we ever do something like defense modifiers it would be here
        int damageToTake = attack.gameObject.GetComponent<Fireball>().damage;
        if(damageToTake > 0){
            hitpoints -= damageToTake;
            if(hitpoints <= 0){
                //Die
            }
            myFlasher.startFlasher(flashOnHitDuration);
        }
    }
    IEnumerator falloffOnHit(){
        yield return new WaitForSeconds(timeUntilChainCheckingStops);
        chainedHits = 0;
        checkingForChain = false;
        Debug.Log("Done with chain hitting, resetting");
    }
    void takeKnockback(Collision2D impact, Vector2 modifier){
        Vector2 direction = (gameObject.transform.position - impact.gameObject.transform.position);
        myBody.AddForce(direction * modifier * knockbackModifier);
    }
    IEnumerator becomeBouncy(){
        Debug.Log("Doing big knockback");
        myBody.drag = 1f;
        myBody.sharedMaterial = knockbackMaterial;
        invuln = true;
        //Idea: Increase the drag slowly again until normal again, based on how quickly
        yield return new WaitForSeconds(disableDurationOnMaxCombo);
        Debug.Log("Done with knockback");
        myBody.drag = defaultDrag;
        myBody.sharedMaterial = defaultMaterial;
        invuln = false;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy Attack"){
            onHit(other);
        }
    }
}
