using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkAnimation : MonoBehaviour {
    Animator anim;
    SpriteRenderer sprite;

    bool hasJumped = false;

	void Start () {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
	
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        bool vertical = Input.GetKeyDown(KeyCode.Space);

        if (horizontal > 0) {
            sprite.flipX = false;
            anim.SetBool("Walking", true);
        } else if(horizontal < 0) {
            sprite.flipX = true;
            anim.SetBool("Walking", true);
        } else {
            anim.SetBool("Walking", false);
        }

        if(vertical && !hasJumped) {
            anim.SetTrigger("Jump");
            hasJumped = true;
        } else if(!vertical && hasJumped) {
            hasJumped = false;
        }

        if (GetComponent<Player>().gravity > 0) {
            sprite.flipY = true;
        } else {
            sprite.flipY = false;
        }
	}
}
