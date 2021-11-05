using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour

{
    [SerializeField]
    private float _speedPowerup = 3f;
    [SerializeField] // 0 = Triple Shot, 1 = Speed, 2 = Shields   
    private int powerupID;

    void Update()
    {
        transform.Translate(Vector3.down * _speedPowerup * Time.deltaTime);

        if (transform.position.y < -5.2f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }

            }
            Destroy(this.gameObject);
        }
    }
}
