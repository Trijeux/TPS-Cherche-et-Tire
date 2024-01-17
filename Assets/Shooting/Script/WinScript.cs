using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatorFelicitationText : MonoBehaviour
{
    [SerializeField] private ShootAllTarget _shootAllTarget;
    [SerializeField] private Text _reussiteText;
    [SerializeField] private Text _felicitationText;
    [SerializeField] private AudioSource  _winAudioSource;
    [SerializeField] private bool _songOn;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_shootAllTarget._end && !_songOn)
        {
            _reussiteText.text = "Vous avez réussi a tirer sur toute les cible !!!";
            _felicitationText.text = "Félicitations";
            _songOn = true;
            if (_songOn)
            {
                _winAudioSource.Play();
            }
            
        }
    }
}
