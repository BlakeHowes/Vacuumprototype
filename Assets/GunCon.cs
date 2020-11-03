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
    private GameObject Suction;
    [SerializeField]
    private GameObject Blow;
    private float blowtimer;
    [SerializeField]
    private List<GameObject> Objectinvacuum = new List<GameObject>();
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            Suction.SetActive(true);
            ScaleSuction();
        }
        else
        {
            Suction.SetActive(false);
        }

        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
        {
            Blow.SetActive(true);
            ScaleBlow();
            blowtimer += Time.deltaTime;
            if(blowtimer > Blowdelay)
            {
                blowobjectsout();
                blowtimer = 0f;
            }
        }
        else
        {
            Blow.SetActive(false);
        }
    }
    void Update()
    {
        Rotatetowardsmouse();
    }

    //spins the gun
    private void Rotatetowardsmouse()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
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

    //scaling and chaning the local position so the vacuum doesnt go through walls
    private void ScaleSuction()
    {
        float suctionlength = ReturnVacuumDistance()/2;
        Vector3 scaleupdate = new Vector3(suctionlength, 1, 1);
        Vector3 positionupdate = new Vector3(suctionlength, 0, 0);

        Suction.transform.localScale = scaleupdate;
        Suction.transform.localPosition = positionupdate;
    }

    private void ScaleBlow()
    {
        float suctionlength = ReturnVacuumDistance() / 2;
        Vector3 scaleupdate = new Vector3(suctionlength, 1, 1);
        Vector3 positionupdate = new Vector3(suctionlength, 0, 0);

        Blow.transform.localScale = scaleupdate;
        Blow.transform.localPosition = positionupdate;
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
            Objectinvacuum[lastobject - 1].transform.position = transform.position; //change this to end of gun
            Objectinvacuum[lastobject - 1].transform.rotation = Quaternion.identity;
            Objectinvacuum.Remove(Objectinvacuum[lastobject-1]);
        }
    }
}