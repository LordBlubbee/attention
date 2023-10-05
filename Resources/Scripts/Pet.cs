using Newtonsoft.Json;
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ATTENTION.Resources.Scripts
{
    [Serializable]
    public class Pet : INotifyPropertyChanged
    {
        public static Pet pet;
        public float needFood { get; set; } //The need for Food
        public DateTime needFoodTime; //The next time Food must be reduced
        public float needWater { get; set; } //Water
        public DateTime needWaterTime;
        public float needAttention { get; set; } //Boredom; Attention can also be *too much*, resulting in Overloaded
        public DateTime needAttentionTime;
        public float needCompany { get; set; } //Need for company, solved by simply logging in
        public DateTime needCompanyTime;
        public float needRest { get; set; } //Need rest
        public DateTime needRestTime;

        public string MostPressingNeed;

        public WaterPage waterPage = new WaterPage();
        public MainPage mainPage;

        private static System.Timers.Timer aTimer;

        public event PropertyChangedEventHandler PropertyChanged;
        public static void MainRun()
        {
            Console.WriteLine("Running Main");
            // Create a timer and set a two second interval.
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000;

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;

            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            // Start the timer
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Advance variables
            /*JSON?
             * string String = JsonConvert.SerializeObject(Class)
             * 
             * */
            //Console.WriteLine("Tick!");

            DateTime now = DateTime.Now;
            while (now > Pet.pet.needWaterTime) Pet.pet.WaterReduce(); //Hydrate: 72 per day reduction, 1.5L per day at least
            while (now > Pet.pet.needFoodTime) Pet.pet.FoodReduce(); //Health: 24 per day reduction, eat healthy meal once every 2.5 days
            while (now > Pet.pet.needRestTime) Pet.pet.RestReduce(); //Sleep: Sleep once a day, 96 per day reduction
            while (now > Pet.pet.needCompanyTime) Pet.pet.CompanyReduce(); //Exercise: 24 per day reduction, once every 3 day workout
            while (now > Pet.pet.needAttentionTime) Pet.pet.AttentionReduce(); //Homework: 24 per day reduction, 1 hour per day

            Pet.pet.MostPressingNeed = setNeed();

            if (Pet.pet.mainPage != null)
            {
                Console.WriteLine("Updating");
                Pet.pet.mainPage.update();
            }
            Pet.pet.saveGame();
        }
        public Pet()
        {
            if (pet == null)
            {
                pet = this;
                MainRun();
                loadGame();
            }
        }

        private static string setNeed()
        {
            int NeedID = 1;
            float NeedHeight = Pet.pet.needWater;
            if (Pet.pet.needFood < NeedHeight)
            {
                NeedID = 2;
                NeedHeight = Pet.pet.needFood;
            }
            if (Pet.pet.needCompany < NeedHeight)
            {
                NeedID = 3;
                NeedHeight = Pet.pet.needCompany;
            }
            if (Pet.pet.needAttention < NeedHeight)
            {
                NeedID = 4;
                NeedHeight = Pet.pet.needAttention;
            }
            if (Pet.pet.needRest < NeedHeight)
            {
                NeedID = 5;
                NeedHeight = Pet.pet.needRest;
            }
            if (NeedHeight < 80)
            {
                switch (NeedID)
                {
                    case 1:
                        if (NeedHeight > 30)
                        {
                            return "You could drink a bit more.";
                        }
                        else if (NeedHeight > 0)
                        {
                            return "You should drink more water!";
                        }
                        else
                        {
                            return "YOU FOOL!! DRINK OR DIE!!";
                        }
                    case 2:
                        if (NeedHeight > 30)
                        {
                            return "A healthy bite will do.";
                        }
                        else if (NeedHeight > 0)
                        {
                            return "You lack nutrients. Eat!";
                        }
                        else
                        {
                            return "DO YOU WANT TO STARVE?!?!";
                        }
                    case 3: //Company (Exercise)
                        if (NeedHeight > 30)
                        {
                            return "You could use some exercise.";
                        }
                        else if (NeedHeight > 0)
                        {
                            return "Your stiff body needs some working.";
                        }
                        else
                        {
                            return "DO YOU WANT TO WITHER AND DIE?!?!";
                        }
                    case 4: //Attention (WorK)
                        if (NeedHeight > 30)
                        {
                            return "Perhaps going ahead with homework is smart.";
                        }
                        else if (NeedHeight > 0)
                        {
                            return "You should do something useful with your life.";
                        }
                        else
                        {
                            return "GET TO WORK, LAZY-ASS!!";
                        }
                    case 5: //Rest
                        if (NeedHeight > 20)
                        {
                            return "Take it easy.";
                        }
                        else
                        {
                            return "Prepare to go and SLEEP!";
                        }
                }
            }
            return "You are doing well!";
        }
        private int FoodTimer()
        {
            return (int)Math.Round(1440 / healthPerDay);
        }
        public void FoodReduce()
        {
            needFood--;
            if (needFood <= -50)
            {
                needFoodTime = DateTime.Now;
            }
            needFoodTime = needFoodTime.AddMinutes(FoodTimer());
        }

        public float waterPerDay;
        public float healthPerDay;
        public float exercisePerDay;
        public float workPerDay;
        private int WaterTimer()
        {
            return (int)Math.Round(1440/waterPerDay); //10 Minutes (-240 per day) (-72 per day = 30)
        }
        public void WaterReduce()
        {
            needWater--;
            if (needWater <= -50)
            {
                needWaterTime = DateTime.Now;
            }
            //needWaterTime = needWaterTime.AddSeconds(5);
            needWaterTime = needWaterTime.AddMinutes(WaterTimer());
        }
        private int AttentionTimer()
        {
            return (int)Math.Round(1440 / workPerDay);
        }
        public void AttentionReduce()
        {
            needAttention--;
            if (needAttention <= -50)
            {
                needAttentionTime = DateTime.Now;
            }
            needAttentionTime = needAttentionTime.AddMinutes(AttentionTimer());
        }
        private int CompanyTimer()
        {
            return (int)Math.Round(1440 / exercisePerDay);
        }
        public void CompanyReduce()
        {
            needCompany--;
            if (needCompany <= -50)
            {
                needCompanyTime = DateTime.Now;
            }
            needCompanyTime = needCompanyTime.AddMinutes(CompanyTimer());
        }
        private int RestTimer()
        {
            return 10; //Minutes
        }
        public void RestReduce()
        {
            needRest--;
            if (needRest <= -10)
            {
                needRestTime = DateTime.Now;
            }
            needRestTime = needRestTime.AddMinutes(RestTimer());
        }
        public async void PlayAudio(string str)
        {
            var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(str));
            audioPlayer.Play();
        }
        public void loadGame()
        {
            //Preferences.Clear();

            //string String = JsonConvert.SerializeObject(Class);

            if (Preferences.Default.Get<bool>("Saved", false)) {
                needFoodTime = Preferences.Default.Get<DateTime>("FoodTimer", DateTime.Now);
                needWaterTime = Preferences.Default.Get<DateTime>("WaterTimer", DateTime.Now);
                needCompanyTime = Preferences.Default.Get<DateTime>("CompanyTimer", DateTime.Now);
                needAttentionTime = Preferences.Default.Get<DateTime>("AttentionTimer", DateTime.Now);
                needRestTime = Preferences.Default.Get<DateTime>("RestTimer", DateTime.Now);
                needFood = Preferences.Default.Get<float>("Food", 100f);
                needWater = Preferences.Default.Get<float>("Water", 100f);
                needCompany = Preferences.Default.Get<float>("Company", 100f);
                needAttention = Preferences.Default.Get<float>("Attention", 100f);
                needRest = Preferences.Default.Get<float>("Rest", 100f);
                waterPerDay = Preferences.Default.Get<float>("dayWater", 72f);
                healthPerDay = Preferences.Default.Get<float>("dayHealth", 24f);
                workPerDay = Preferences.Default.Get<float>("dayWork", 24f);
                exercisePerDay = Preferences.Default.Get<float>("dayExercise", 24f);
            }
        }
        public Pet savePet { get; set; }
        public void saveGame()
        {
            /*savePet = new Pet
            {
                needAttention= Pet.pet.needAttention,
                needRest= Pet.pet.needRest,
                needFood= Pet.pet.needFood,
                needWater= Pet.pet.needWater,
                needCompany= Pet.pet.needCompany,
                needAttentionTime = Pet.pet.needAttentionTime,
                needRestTime= Pet.pet.needRestTime,
                needFoodTime= Pet.pet.needFoodTime,
                needWaterTime= Pet.pet.needWaterTime,
                needCompanyTime= Pet.pet.needCompanyTime
            };*/
            //string Save = JsonConvert.SerializeObject(Pet.pet);
            //Pet savedPet = JsonConvert.DeserializeObject<Pet>(Save);

            /* TEST RESET
            needWater = -50;
            needRest = -10;
            needFood = -50;
            needCompany = -50;
            needAttention = -50;*/

            var dataStore = DependencyService.Get<IDataStore<Pet>>();
            dataStore.UpdateItem(Pet.pet);
            //
            Preferences.Default.Set("Saved", true);
            Preferences.Default.Set("FoodTimer", needFoodTime);
            Preferences.Default.Set("WaterTimer", needWaterTime);
            Preferences.Default.Set("CompanyTimer", needCompanyTime);
            Preferences.Default.Set("AttentionTimer", needAttentionTime);
            Preferences.Default.Set("RestTimer", needRestTime);
            Preferences.Default.Set("Food", needFood);
            Preferences.Default.Set("Water", needWater);
            Preferences.Default.Set("Company", needCompany);
            Preferences.Default.Set("Attention", needAttention);
            Preferences.Default.Set("Rest", needRest);
            Preferences.Default.Set("dayHealth", healthPerDay);
            Preferences.Default.Set("dayWater", waterPerDay);
            Preferences.Default.Set("dayWork", workPerDay);
            Preferences.Default.Set("dayExercise", exercisePerDay);
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