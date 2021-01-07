using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperMovement2 : MonoBehaviour
{
    private float _movement;

    private bool _isNewspaperActive = false;
    public GameObject obj;
    private GameObject _newspap;

    private Vector3 _endPos = new Vector3(15, 0, 20);
    private void Update()
    {
        if (_isNewspaperActive)
        {
            _movement += Time.deltaTime;
            _movement = _movement % 5f;
            _newspap.transform.position = MathParabola.Parabola(transform.position, _endPos, 7, _movement);
            
            if(Vector3.Distance(_newspap.transform.position, _endPos) <= 0)
            {
                destroyNewspaper();
            }
        }
    }
    public void createNewspaper()
    {
        _isNewspaperActive = true;
        _newspap = (GameObject)Instantiate(obj, transform.position, transform.rotation);

    }
    public void destroyNewspaper()
    { 
        Destroy(_newspap);
        _isNewspaperActive = false;
    }
}
