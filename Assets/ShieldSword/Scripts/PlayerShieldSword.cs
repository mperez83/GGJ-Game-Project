﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldSword : MonoBehaviour
{
    public AudioSource swipe;
    public AudioSource hurt;
    public AudioSource foiled;
    public Sprite dfd;
    public Sprite atk;
    private SpriteRenderer sprite_render;
    public int pts;
    AudioSource sources;
    public GameObject bandit;

    bool swingOnCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite_render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Point();
        Sword();
    }

    void Sword()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !swingOnCooldown)
        {
            swingOnCooldown = true;
            sprite_render.sprite = atk;
            swipe.Play();
            GetComponent<BoxCollider2D>().size = new Vector2(0.3790514f, 0.3276514f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0.08952573f, 0.1489272f);

            LeanTween.delayedCall(0.5f, () =>
            {
                sprite_render.sprite = dfd;
                LeanTween.delayedCall(0.5f, () => { swingOnCooldown = false; });
            });
        }
    }

    void Point()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sprite_render.sprite == dfd)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                pts -= 5;
                hurt.Play();
            }
            else
            {
                foiled.Play();
            }
        }
        if (sprite_render.sprite == atk)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                pts += 5;
                bandit.GetComponent<AudioSource>().Play();
            }
            if (collision.gameObject.CompareTag("Projectile"))
            {
                pts -= 10;
                hurt.Play();
            }
        }
    }
}
