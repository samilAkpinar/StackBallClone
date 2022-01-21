using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    //Sahnedeki imagelara eri�mek i�in
    public Image levelSlider;
    public Image currentLevelImg;
    public Image nextLevelImg;

    //Player material 
    public Material playerMat;

    private PlayerController player;

    public GameObject homeUI;
    public GameObject gameUI;


    // Start is called before the first frame update
    //Oyun a��l�r a��lmaz renklendirmenin yap�lmas�
    void Start()
    {
        //B�l�m ge�ilince topun rengi de�i�ecektir
        playerMat = FindObjectOfType<PlayerController>().transform.GetChild(1).GetComponent<MeshRenderer>().material;

        player = FindObjectOfType<PlayerController>();

        //level ge�isinde frakl� rek olmas� i�in
        levelSlider.transform.GetComponent<Image>().color = playerMat.color + Color.gray;

        //rengin atanmas�
        levelSlider.color = playerMat.color;

        currentLevelImg.color = playerMat.color;

        nextLevelImg.color = playerMat.color;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !ignoreUI() && player.playerstate == PlayerController.PlayerState.Prepare )
        {
            player.playerstate = PlayerController.PlayerState.Playing;
            homeUI.SetActive(false);
            gameUI.SetActive(true);
        }
    }


    private bool ignoreUI()
    {

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData,raycastResults);

        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponent<IgnoreGameUI>()  != null)
            {
                raycastResults.RemoveAt(i);
                i--;
            }
        }

        return raycastResults.Count > 0;
    }

    //Slider�n ilerleme i�lemi
    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }
}
