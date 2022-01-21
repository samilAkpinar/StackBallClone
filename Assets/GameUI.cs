using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    //Sahnedeki imagelara eriþmek için
    public Image levelSlider;
    public Image currentLevelImg;
    public Image nextLevelImg;

    //Player material 
    public Material playerMat;

    private PlayerController player;

    public GameObject homeUI;
    public GameObject gameUI;


    // Start is called before the first frame update
    //Oyun açýlýr açýlmaz renklendirmenin yapýlmasý
    void Start()
    {
        //Bölüm geçilince topun rengi deðiþecektir
        playerMat = FindObjectOfType<PlayerController>().transform.GetChild(1).GetComponent<MeshRenderer>().material;

        player = FindObjectOfType<PlayerController>();

        //level geçisinde fraklý rek olmasý için
        levelSlider.transform.GetComponent<Image>().color = playerMat.color + Color.gray;

        //rengin atanmasý
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

    //Sliderýn ilerleme iþlemi
    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }
}
