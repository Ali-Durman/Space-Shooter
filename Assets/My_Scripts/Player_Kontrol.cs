using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Kontrol : MonoBehaviour
{
    Rigidbody Fizik;
    float horizontal = 0;
    float vertical = 0;
    Vector3 vec;
    public float KarakterHiz;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float egim;
    float AtesZamani = 0;
    public float AtesGecenSure;
    public GameObject Lazer;
    public Transform LazerComeFrom;
    AudioSource players_lazer_sound;

    // Start is called before the first frame update
    void Start()
    {
        Fizik = GetComponent<Rigidbody>();           // Fizi�i Rigidbody componentine e�itliyoruz.
        players_lazer_sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1")&& Time.time>AtesZamani)
        {
            AtesZamani = Time.time + AtesGecenSure;
            // Instantiate (object, position, rotation)
            Instantiate (Lazer, LazerComeFrom.position, Quaternion.identity);
            players_lazer_sound.Play();
        }
    }

    // Update is called once per frame
    void FixedUpdate()              // fizik olaylar�yla u�ra��rken FixedUpdate kullan�yoruz.
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        vec = new Vector3(horizontal, 0, vertical);         // burada vector3 m�z�n gidece�i yerler ayarl�yoruz y ye 0 vermemizin sebebi 2D oyun yapt���m�z i�in.
        Fizik.velocity = vec*KarakterHiz;                  // Burada H�z�n� ayarl�yoruz.


        Fizik.position = new Vector3(                         // Bu a�a��da ise position i�erisinde gidebilice�i s�n�rlar� �izdik ve c# scriptimizin i�erisinde public de�erlerle ayarlad�k.
            Mathf.Clamp(Fizik.position.x,minX,maxX),
            0.0f,
            Mathf.Clamp(Fizik.position.z,minZ,maxZ)
            );
        Fizik.rotation = Quaternion.Euler(0,0,Fizik.velocity.x*-egim);    // rotation transformun i�indeki bir �ey fizikte �uan i�erisindeki rigidbody compenenti 2 componenti �a��r�p rotation�n ald���
        // Quaternion diye bir de�i�ken al�yor ve . ile i�erisinde euler anlad���m kadar�yla e�im vermeye yar�yor x sine ve y sine 0 verdikten sonra z sine rigidbodyimizin velocitysine x i at�yoruz ve
        // bunu egimin -lisiyle �arp�yoruz. K�saca bu sat�r haraket halinde sa�a sola giderken sa�a giderken sa�a e�im sola giderken sola e�im vermesini sa�l�yor.

    }
}
