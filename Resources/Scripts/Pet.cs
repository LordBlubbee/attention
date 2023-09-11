using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATTENTION.Resources.Scripts
{
    public class Pet
    {
        float needFood; //The need for Food
        DateTime needFoodTime; //The next time Food must be reduced
        float needWater; //Water
        DateTime needWaterTime;
        float needAttention; //Boredom; Attention can also be *too much*, resulting in Overloaded
        DateTime needAttentionTime;
        float needCompany; //Need for company, solved by simply logging in
        DateTime needCompanyTime;
        float needRest; //Need rest
        DateTime needRestTime;

        public Pet()
        {
            //Initialize (Skip if loading data)
            needFoodTime = DateTime.Now;
            needWaterTime = DateTime.Now;
            needAttentionTime = DateTime.Now;
            needCompanyTime = DateTime.Now;
            needRestTime = DateTime.Now;
        }

        private int FoodTimer()
        {
            return 15; //Minutes
        }
        public void FoodReduce()
        {
            needFood--;
            needFoodTime = needFoodTime.AddMinutes(FoodTimer());
        }
        private int WaterTimer()
        {
            return 10; //Minutes
        }
        public void WaterReduce()
        {
            needWater--;
            needWaterTime = needWaterTime.AddMinutes(WaterTimer());
        }
        private int AttentionTimer()
        {
            return 8; //Minutes
        }
        public void AttentionReduce()
        {
            needAttention--;
            needAttentionTime = needAttentionTime.AddMinutes(AttentionTimer());
        }
        private int CompanyTimer()
        {
            return 6; //Minutes
        }
        public void CompanyReduce()
        {
            needCompany--;
            needCompanyTime = needCompanyTime.AddMinutes(CompanyTimer());
        }
        private int RestTimer()
        {
            return 30; //Minutes
        }
        public void RestReduce()
        {
            needRest--;
            needRestTime = needRestTime.AddMinutes(RestTimer());
        }
    }
}
/**/
/*public class PARSER
{
    public TextAsset[] script;
    void Start()
    {
        //Scan
        Dialog dialogScan = JsonUtility.FromJson<Dialog>(script[0].text);

        foreach (DialogElement elem in dialogScan.dialogelements)
        {
            Debug.WriteLine("Found text: " + elem.text);
            Debug.WriteLine("Found speed: " + elem.speakspeed);
        }
    }

    public Dialog obtainDialog(int day)
    {
        return JsonUtility.FromJson<Dialog>(script[day - 1].text);
    }
}

[System.Serializable]
public class DialogElement
{
    //Case sensitive, matches Json
    public string text;
    public string color;
    public float speakspeed;
    public int animation;
}

[System.Serializable]
public class Dialog
{
    //case sensitive, dialogelements matches json
    public DialogElement[] dialogelements;
}*/