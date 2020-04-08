using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{


    public GameObject Player;

    public Animator playerAnimator;
    public Animation volcanoAnimation;
    public Animation tornadoAnimation;
    public Animation waterTankAnimation;
    public Animation cloudAnimation;
    public SpriteRenderer bullet;

    public SpriteRenderer background;

    public bool volcanoPlayerSwitch;
    public bool tornadoPlayerSwitch;
    public bool waterPlayerSwitch;
    public bool cloudPlayerSwitch;

    public Sprite[] backgroundSprites;



    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = Player.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToTornado()
    {
        playerAnimator.Play("tornado");
    }
    public void ChangeToVolcano()
    {
        playerAnimator.Play("volcano");
    }


}
