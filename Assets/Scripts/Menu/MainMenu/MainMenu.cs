using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject character;
    public GameObject player;
    private CharacterController controller;
    private Animator anim;
    public Text currenyText;
    public GameObject cameraHolder;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        //Instantiate(gameManager.characters.ToList<GameObject>().Find(x => x.GetComponent<CharacterManager>().characterIndex == gameManager.currentCharacterIndex), player.transform);
        if (player.transform.childCount == 0)
            for (int i = 0; i < gameManager.characters.Length; i++)
            {
                GameObject charac = Instantiate(gameManager.characters.ToList<GameObject>().Find(x => x.GetComponent<CharacterManager>().characterIndex == i), player.transform);
                charac.name = i.ToString();
                if (i != gameManager.currentCharacterIndex)
                    charac.SetActive(false);
            }

        controller = player.GetComponent<CharacterController>();
        character = player.transform.Find(gameManager.currentCharacterIndex.ToString()).gameObject;
        anim = GetComponent<Animator>();
    }



    public void startGame()
    {
        character = player.transform.Find(gameManager.currentCharacterIndex.ToString()).gameObject;
        character.GetComponent<Animator>().SetTrigger("Run");
        gameManager.isGameStarted = true;
        cameraHolder.GetComponent<CameraOverrider>().IsMoving = true;

    }




}
