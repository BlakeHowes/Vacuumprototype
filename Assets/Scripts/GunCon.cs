using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCon : MonoBehaviour
{
    [SerializeField]
    private float Range;
    [SerializeField]
    private float Blowdelay;
    [SerializeField]
    private float recoilscale;
    [SerializeField]
    private float BlowRandomization;
    [SerializeField]
    private GameObject Suction;
    [SerializeField]
    private GameObject Blow;
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private GameObject Player;
    private Rigidbody2D prb;
    [SerializeField]
    private GameObject BlowLocation;
    [SerializeField]
    private Collider2D Gunentrance;
    private float blowtimer;
    [SerializeField]
    private List<GameObject> Objectinvacuum = new List<GameObject>();
    [SerializeField]
    private ParticleSystem Beam;
    [SerializeField]
    private GameObject GunSprite;
    [SerializeField]
    private GameObject GunSprite2;
    private float jointtimer;
    private GameObject Targetedfixedjoint;
    private float startxscale;
    [SerializeField]
    private bool endlessmode = false;
    [SerializeField]
    private GameObject SpriteEmpty;

    public float BeamScale;
    public float offsetmax;
    public float shakeforce;
    public float magnitudeofshake;
    public float thrust;
    private void Awake()
    {
        prb = Player.GetComponent<Rigidbody2D>();
        startxscale = GunSprite.transform.localScale[1];
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            Gunentrance.enabled = true;
            Suction.SetActive(true);
            ScaleSuction();
            Disablefixedjoints();
            PressButton();
            Beam.Play(true);
            GunSprite.SetActive(false);
            GunSprite2.SetActive(true);
            AddForceToPlayer(thrust);
            RemoveChunk();
        }
        else
        {
            Gunentrance.enabled = false;
            Suction.SetActive(false);
        }

        if (!Input.GetMouseButton(0))
        {
            jointtimer = 0f;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            StartCoroutine(Shake(0.1f, magnitudeofshake));
        }

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            GunSprite.SetActive(true);
            GunSprite2.SetActive(false);
        }

        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
        {
            Blow.SetActive(true);
            ScaleBlow();
            Beam.Play(true);
            blowtimer += Time.deltaTime;

            GunSprite.SetActive(false);
            GunSprite2.SetActive(true);
            AddForceToPlayer(thrust * -1);
            if (blowtimer > Blowdelay)
            {
                blowobjectsout();
                blowtimer = 0f;
            }
        }
        else
        {
            Blow.SetActive(false);
        }
        Rotatetowardsmouse();
    }

    //spins the gun
    private void Rotatetowardsmouse()
    {
        var mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        var object_pos = UnityEngine.Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;

        var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            if (angle <= -90 || angle >= 90)
            {
                var scale = GunSprite.transform.localScale;
                GunSprite.transform.localScale = new Vector3(scale[0], startxscale * -1, scale[2]);
            }
            else
            {
                var scale = GunSprite.transform.localScale;
                GunSprite.transform.localScale = new Vector3(scale[0], startxscale, scale[2]);
            }
        }
        else
        {
            if (angle <= -90 || angle >= 90)
            {
                var scale = GunSprite.transform.localScale;
                GunSprite2.transform.localScale = new Vector3(scale[0], startxscale * -1, scale[2]);
            }
            else
            {
                var scale = GunSprite.transform.localScale;
                GunSprite2.transform.localScale = new Vector3(scale[0], startxscale, scale[2]);
            }
        }


        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    //returns the distance to the wall
    private float ReturnVacuumDistance()
    {
        float distancetohit = 0f;
        LayerMask mask = LayerMask.GetMask("Wall");
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.right),Range, mask);
        if (hit.collider != null)
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            distancetohit = Vector2.Distance(position,hit.point);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * Range, Color.yellow);
        }
        else
        {
            distancetohit = Range;
        }
        return distancetohit;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {

        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 offset = new Vector3(Player.transform.position.x * offsetmax, Player.transform.position.y * offsetmax, Player.transform.position.z * offsetmax);

            float x = Random.Range(-shakeforce, shakeforce) * magnitude;
            float y = Random.Range(-shakeforce, shakeforce) * magnitude;
            float z = Random.Range(-shakeforce, shakeforce) * magnitude;

            Vector3 pos = transform.position;

            magnitude -= 0.02f;

            Vector3 shake = new Vector3(x + pos[0], y + pos[1], z + pos[2]);
            SpriteEmpty.transform.position = shake + offset;
            elapsed += Time.deltaTime;
            yield return 0;
        }

    }

    public void AddForceToPlayer(float force)
    {
        var rb = Player.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * force);
    }

    private void Disablefixedjoints()
    {
        LayerMask mask = LayerMask.GetMask("VacuumObject");
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.right), Range, mask);
        if(hit.collider != null)
        {
            GameObject target = hit.collider.gameObject;
            if (hit.collider.gameObject.TryGetComponent(out FixedJoint2D joint))
            {
                if(target.GetComponent<Rigidbody2D>().mass < 1) //set mass of object if below 1
                {
                    target.GetComponent<Rigidbody2D>().mass = 1;
                }

                if (target != Targetedfixedjoint)
                {
                    Targetedfixedjoint = target;
                    jointtimer = 0f;
                }
                if (joint != null)
                {
                    jointtimer += Time.deltaTime;
                    if(jointtimer > 0.2)
                    {
                        joint.enabled = (false);
                    }
                }
            }
        }
    }

    private void RemoveChunk()
    {
        LayerMask mask = LayerMask.GetMask("Chunk");
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.right), Range, mask);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.TryGetComponent(out ChunkCon chunkcon))
            {
                hit.collider.gameObject.GetComponent<ChunkCon>().RemoveChunk();
            }
        }
    }

    private void PressButton()
    {
        LayerMask mask = LayerMask.GetMask("Button");
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.right), Range, mask);
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<Button>().Press();
        }
    }

    //scaling and chaning the local position so the vacuum doesnt go through walls
    private void ScaleSuction()
    {
        float suctionlength = ReturnVacuumDistance()/2;
        Vector3 scaleupdate = new Vector3(suctionlength, 1, 1);
        Vector3 positionupdate = new Vector3(suctionlength, 0, 0);

        Suction.transform.localScale = scaleupdate;
        Suction.transform.localPosition = positionupdate;
        Beam.startLifetime = suctionlength/BeamScale;
        Camera.GetComponent<DynamicCamera>().camerashake(); //camerashake
    }

    private void ScaleBlow()
    {
        float suctionlength = ReturnVacuumDistance() / 2;
        Vector3 scaleupdate = new Vector3(suctionlength, 1, 1);
        Vector3 positionupdate = new Vector3(suctionlength, 0, 0);

        Blow.transform.localScale = scaleupdate;
        Blow.transform.localPosition = positionupdate;

        Camera.GetComponent<DynamicCamera>().camerashake(); //camerashake
    }

    //This is the collision for removing and object from the scene by setting it to inactive and adding it to the Objectinvacuum list
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0))
        {
            if (collision.gameObject.tag == "VacuumObject")
            {
                Objectinvacuum.Add(collision.gameObject);
                collision.gameObject.SetActive(false);
            }
        }
    }

    //this removes objects from the Objectinvacuum list and sets the to active
    //not this can easily be changed to do weird shit, like clone objects ect
    private void blowobjectsout()
    {
        int lastobject = Objectinvacuum.Count;
        if(lastobject > 0)
        {
            Objectinvacuum[lastobject - 1].SetActive(true);

            if(Objectinvacuum[lastobject - 1].gameObject.TryGetComponent<Rocket>(out Rocket rocket))
            {
                rocket.GetComponent<Rocket>().TurnOffSeeking();
            }
            if (Objectinvacuum[lastobject - 1].gameObject.TryGetComponent(out FixedJoint2D joint))
            {
                joint.enabled = false;
            }
            Vector3 RandomOffset = new Vector3(Random.Range(-BlowRandomization, BlowRandomization), Random.Range(-BlowRandomization, BlowRandomization), Random.Range(-BlowRandomization, BlowRandomization));
            if(endlessmode == false)
            {
                Objectinvacuum[lastobject - 1].transform.position = BlowLocation.transform.position + RandomOffset;
            }
            else
            {
                Instantiate(Objectinvacuum[lastobject - 1], BlowLocation.transform.position + RandomOffset, Quaternion.identity);
            }
            Quaternion oppositedirection = Quaternion.Euler(0,0,transform.rotation.z + 180);
            Objectinvacuum[lastobject - 1].transform.rotation = oppositedirection;
            if(endlessmode == false)
            {
                Objectinvacuum.Remove(Objectinvacuum[lastobject - 1]);
            }
            prb.AddForce(transform.TransformDirection(Vector2.left) * 1000 * recoilscale);
        }
    }
}