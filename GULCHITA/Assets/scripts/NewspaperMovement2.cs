using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewspaperMovement2 : MonoBehaviour
{
    public List<GameObject> allNewspaper = new List<GameObject>();
    private bool _isNewspaperActive = false;
    public GameObject obj;
    private GameObject _newspap;
    private Rigidbody _rigidbody;
    private PersonMovement move;
    private int _personLine;

    private void Start()
    {
        move = GetComponent<PersonMovement>();
        
    }
    private void Update()
    {
        _personLine = move.getLaneNumber();
        if(_isNewspaperActive)
        {
            switch (_personLine)
            {
                case 0:
                    _rigidbody.AddForce(new Vector3(15, 15, 7) * 30);
                    break;
                case 2:
                    _rigidbody.AddForce(new Vector3(15, 15, -7) * 30);
                    break;
            }
            _isNewspaperActive = false;
        }
        foreach(var newspaper in allNewspaper.ToList())
        {
            newspaper.GetComponent<Rigidbody>().AddTorque(transform.right * 3);
            if (newspaper.transform.position.y < 0)
            {
                DestroyNewspaper(newspaper);
            }
        }
        
    }
    public void CreateNewspaper()
    {
        _isNewspaperActive = true;
        switch (move.getLaneNumber())
        {
            case 0:
                _newspap = (GameObject)Instantiate(obj, new Vector3(0, 1f, 3.5f), transform.rotation);
                break;
            case 2:
                _newspap = (GameObject)Instantiate(obj, new Vector3(0, 1f, -3.5f), transform.rotation);
                break;
        }
        _rigidbody = _newspap.GetComponent<Rigidbody>();
        allNewspaper.Add(_newspap);

    }
    public void DestroyNewspaper(GameObject newspaper)
    { 
        Destroy(newspaper);
        allNewspaper.RemoveAt(0);
        _isNewspaperActive = false;
    }
}
