using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{

    private GameManager gameManager;
    private List<GameObject> characters;


    public Transform characterPanel;
    public Transform scrollViewer;

    private GameObject firstCharacter, g, firstButton;
    public GameObject priceBtn;

    [SerializeField]private GameObject player;

    private int activeIndex = 0;

    public string[] ownCharacters;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }


    private void Start()
    {
        activeIndex = gameManager.currentCharacterIndex;
        characters = gameManager.characters.ToList<GameObject>();
        characterP();
    }

    private void characterP()
    {
        firstCharacter = characterPanel.GetChild(0).gameObject;
        firstButton = scrollViewer.GetChild(0).gameObject;

        ownCharacters = PlayerPrefs.GetString("own_characters").Split(',');

        for (int i = 0; i < characters.Count; i++)
        {
            g = Instantiate(firstCharacter, characterPanel);
            GameObject player = Instantiate(characters[i].gameObject, g.transform); 
            int index = 0;
            index = i - 1;
            GameObject but = Instantiate(firstButton, scrollViewer);
            but.GetComponent<Button>().onClick.AddListener(() => displayCharacter(index));

            if (i != activeIndex)
                g.SetActive(false);
            else
            {
                priceBtn.GetComponent<Button>().interactable = false;
                priceBtn.GetComponent<Image>().color = Color.grey;
                priceBtn.transform.GetChild(0).gameObject.SetActive(false);
                priceBtn.transform.GetChild(1).gameObject.SetActive(false);
                priceBtn.transform.GetChild(2).gameObject.SetActive(true);
                priceBtn.transform.GetChild(2).GetComponent<Text>().text = "SEÇÝLÝ";
                but.GetComponent<Button>().interactable = false;
                player.GetComponent<Animator>().SetInteger("Idle", Random.Range(1, 7));
                player.GetComponent<Animator>().SetTrigger("IdleTrigger");
            }


            g = but;
            g.transform.GetChild(0).GetComponent<Image>().sprite = player.GetComponent<CharacterManager>().icon;
            g.transform.GetChild(2).GetComponent<Text>().text = player.GetComponent<CharacterManager>().characterName[0].ToString();
            if (player.GetComponent<CharacterManager>().characterEnable == false)
                g.SetActive(false);

            index++;
        }

        Destroy(firstCharacter);
        Destroy(firstButton);

        scrollViewer.GetComponent<RectTransform>().localPosition = Vector3.zero;
        scrollViewer.GetComponent<RectTransform>().localPosition = new Vector3(activeIndex * -150f, 0, scrollViewer.GetComponent<RectTransform>().localPosition.z);
    }

    private void displayCharacter(int active)
    {
        characterPanel.GetChild(activeIndex).gameObject.SetActive(false);
        scrollViewer.GetChild(activeIndex).GetComponent<Button>().interactable = true;
        characterPanel.GetChild(active).gameObject.SetActive(true);
        scrollViewer.GetChild(active).GetComponent<Button>().interactable = false;

        characterPanel.GetChild(active).GetChild(0).gameObject.GetComponent<Animator>().SetInteger("Idle", Random.Range(1, 7));
        characterPanel.GetChild(active).GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("IdleTrigger");

        if (characters[active].GetComponent<CharacterManager>().characterIndex == gameManager.currentCharacterIndex)
        {
            priceBtn.GetComponent<Button>().interactable = false;
            priceBtn.GetComponent<Image>().color = Color.grey;
            priceBtn.transform.GetChild(0).gameObject.SetActive(false);
            priceBtn.transform.GetChild(1).gameObject.SetActive(false);
            priceBtn.transform.GetChild(2).gameObject.SetActive(true);
            priceBtn.transform.GetChild(2).GetComponent<Text>().text = "SEÇÝLÝ";
            priceBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        else if (ownCharacters.Any(characters[active].GetComponent<CharacterManager>().characterIndex.ToString().Equals))
        {
            priceBtn.GetComponent<Button>().interactable = true;
            priceBtn.GetComponent<Image>().color = new Color32(101, 41, 99, 255);
            priceBtn.transform.GetChild(0).gameObject.SetActive(false);
            priceBtn.transform.GetChild(1).gameObject.SetActive(false);
            priceBtn.transform.GetChild(2).gameObject.SetActive(true);
            priceBtn.transform.GetChild(2).GetComponent<Text>().text = "SEÇ";
            priceBtn.GetComponent<Button>().onClick.RemoveAllListeners();
            priceBtn.GetComponent<Button>().onClick.AddListener(() => selectCharacter(characters[active].GetComponent<CharacterManager>()));
        }
        else
        {
            priceBtn.GetComponent<Button>().interactable = true;
            priceBtn.GetComponent<Image>().color = new Color32(0, 255, 22, 255);
            priceBtn.transform.GetChild(0).gameObject.SetActive(true);
            priceBtn.transform.GetChild(1).gameObject.SetActive(true);
            priceBtn.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = characters[active].GetComponent<CharacterManager>().characterPrice.ToString();
            priceBtn.transform.GetChild(2).gameObject.SetActive(false);
            priceBtn.GetComponent<Button>().onClick.RemoveAllListeners();
            priceBtn.GetComponent<Button>().onClick.AddListener(() => buyCharacter(characters[active].GetComponent<CharacterManager>()));
        }

        activeIndex = active;
    }

    private void buyCharacter(CharacterManager character)
    {
        if (gameManager.currency >= character.characterPrice)
        {
            gameManager.currency -= character.characterPrice;
            PlayerPrefs.SetString("own_characters", PlayerPrefs.GetString("own_characters") + "," + character.characterIndex.ToString());
            priceBtn.GetComponent<Button>().interactable = true;
            priceBtn.GetComponent<Image>().color = new Color32(101, 41, 99, 255);
            priceBtn.transform.GetChild(0).gameObject.SetActive(false);
            priceBtn.transform.GetChild(1).gameObject.SetActive(false);
            priceBtn.transform.GetChild(2).gameObject.SetActive(true);
            priceBtn.transform.GetChild(2).GetComponent<Text>().text = "SEÇ";
            priceBtn.GetComponent<Button>().onClick.RemoveAllListeners();
            priceBtn.GetComponent<Button>().onClick.AddListener(() => selectCharacter(characters[character.characterIndex].GetComponent<CharacterManager>()));
            ownCharacters = PlayerPrefs.GetString("own_characters").Split(',');
            gameManager.Save();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    private void selectCharacter(CharacterManager character)
    {
        PlayerPrefs.SetInt("current_selected_character", character.characterIndex);
        //Destroy(player.transform.GetChild(0).gameObject);
        player.transform.Find(gameManager.currentCharacterIndex.ToString()).gameObject.SetActive(false);
        player.transform.Find(character.characterIndex.ToString()).gameObject.SetActive(true);
        gameManager.currentCharacterIndex = character.characterIndex; 
        priceBtn.GetComponent<Button>().interactable = false;
        priceBtn.GetComponent<Image>().color = Color.grey;
        priceBtn.transform.GetChild(0).gameObject.SetActive(false);
        priceBtn.transform.GetChild(1).gameObject.SetActive(false);
        priceBtn.transform.GetChild(2).gameObject.SetActive(true);
        priceBtn.transform.GetChild(2).GetComponent<Text>().text = "SEÇÝLÝ";
        priceBtn.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
