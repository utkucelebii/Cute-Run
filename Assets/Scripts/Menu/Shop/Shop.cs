using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    private GameManager gameManager;
    private List<GameObject> characters;

    public Transform characterPanel;
    public Transform scrollViewer;

    private GameObject firstCharacter, g, firstButton;

    private int activeIndex = 0;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }


    private void Start()
    {
        characters = gameManager.characters.ToList<GameObject>();
        characterP();
    }

    private void characterP()
    {
        firstCharacter = characterPanel.GetChild(0).gameObject;
        firstButton = scrollViewer.GetChild(0).gameObject;
        for (int i = 0; i < characters.Count; i++)
        {
            g = Instantiate(firstCharacter, characterPanel);
            GameObject player = Instantiate(characters[i].gameObject, g.transform.GetChild(0).transform);
            g.transform.GetChild(1).GetComponent<Text>().text = player.GetComponent<CharacterManager>().characterName[0];

            int index = 0;
            index = i - 1;
            GameObject but = Instantiate(firstButton, scrollViewer);
            but.GetComponent<Button>().onClick.AddListener(() => displayCharacter(index));

            if (i != 0)
                g.SetActive(false);
            else
            {
                but.GetComponent<Button>().interactable = false;
                player.GetComponent<Animator>().SetInteger("RandomIdle", Random.Range(0, 7));
                player.GetComponent<Animator>().SetTrigger("Idle 0");
            }

            
            g = but;
            g.transform.GetChild(0).GetComponent<Image>().sprite = player.GetComponent<CharacterManager>().icon;
            g.transform.GetChild(1).GetComponent<Text>().text = player.GetComponent<CharacterManager>().characterPrice.ToString();
            index++;
        }
        Destroy(firstCharacter);
        Destroy(firstButton);
    }

    private void displayCharacter(int active)
    {
        Debug.Log(active);
        characterPanel.GetChild(activeIndex).gameObject.SetActive(false);
        scrollViewer.GetChild(activeIndex).GetComponent<Button>().interactable = true;
        activeIndex = active;
        characterPanel.GetChild(active).gameObject.SetActive(true);
        scrollViewer.GetChild(active).GetComponent<Button>().interactable = false;

        characterPanel.GetChild(active).GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().SetInteger("RandomIdle", Random.Range(0, 7));
        characterPanel.GetChild(active).GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().ResetTrigger("Idle 0");
        characterPanel.GetChild(active).GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("Idle 0");

    }
}
