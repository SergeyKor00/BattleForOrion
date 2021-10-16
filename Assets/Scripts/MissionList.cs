using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionList : MonoBehaviour
{

    public Text MissionText;

    private string sceneName;

    private string[] missionInfo = new string[3];

    public GameObject[] missionButtons;

    // Start is called before the first frame update
    void Start()
    {
        missionInfo[0] = "Наш флот начинает наступление в секторе Орион. Вы будете командовать авангардом наших войск. Ваша текущая задача - " +
         "малыми силами совершить прыжок в поле астероидов и захватить добывающие станции противника. По нашим данным, там нет серьезной защиты. "
         + " Подготовьте плацдарм для наступления основных сил. Учтите, от ваших действий может зависеть судьба всей кампании!";

        missionInfo[1] = "Отлично, теперь мы сможем перебросить  основной флот и развить наступление. Однако разведка сообщает, что враг мобилизует войска " +
        "по всему сектору. Они планируют начать контратаку и выбить вас с захваченной позиции. Ваша задача защищать базу любой ценой до подхода основных сил. " +
        "Если вы потерпите неудачу, противник сможет перехватить инициативу. Однако у них нет времени собрать большой флот, так что эскадры противника " +
        "будут появляться с разных сторон и с промежутком во времени. Используйте эти паузы с умом!";

        missionInfo[2] = "Ваша храбрость и мастерство впечатлили наше руководство. Вы назначаетесь командующим всеми  космическими силами в этом секторе." +
          " После неудачного наступления враг стягивает войска к планете Мейрон 4. Поэтому сейчас генеральное сражение пойдет нам на руку. За нами численное превосходство " +
          "и высокий боевой дух. Захватите орбиту планеты, чтобы мы могли высадить десант на поверхность. После этого противник уже не сможет оказать серьезное " +
          "сопротивление в этом секторе. Вперед командир, желаем удачи!";


        if (!PlayerPrefs.HasKey("mission"))
        {
            PlayerPrefs.SetInt("mission", 1);
        }
        int maxMission = PlayerPrefs.GetInt("mission");
        for (int i = 0; i < maxMission; i++)
        {
            missionButtons[i].SetActive(true);
        }


        MissionText.text = missionInfo[maxMission - 1];

        sceneName = "Mission" + maxMission.ToString();
    }



    public void SetMission(string name)
    {
        sceneName = name;
    }

    public void SetInfo(int number)
    {
        MissionText.text = missionInfo[number];
    }
    public void Exit()
    {
        Application.Quit();

    }
    public void OpenScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
        MissionText.text = "Идет загрузка. Пожалуйста, подождите";
    }
}
