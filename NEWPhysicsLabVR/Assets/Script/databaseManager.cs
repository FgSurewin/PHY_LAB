using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.UI;

public class databaseManager : MonoBehaviour
{
    public static databaseManager DM;

    DatabaseReference reference;

    public string userName;

    //window shows lab 1 data and the data 
    public string[] lab1Time = new string[6];
    public string[] lab1Length = new string[6];
    public string[] lab1Period = new string[6];

    public GameObject Lab1DataWindow;
    public Text[] showLength = new Text[6];
    public Text[] showTime = new Text[6];
    public Text[] showPeriod = new Text[6];

    //window shows lab 2 data and the data
    public string lab2RedAngle;
    public string lab2BlackAngle;
    public string lab2PurpleAngle;
    public string lab2RedMass;
    public string lab2BlackMass;
    public string lab2PurpleMass;

    public GameObject Lab2DataWindow;
    public Text showRedAngle;
    public Text showBlackAngle;
    public Text showPurpleAngle;
    public Text showRedMass;
    public Text showBlackMass;
    public Text showPurpleMass;

    //window shows lab 3 data and the data 
    public string[] lab3Weight = new string[3];
    public string[] lab3SpringLength = new string[3];

    public GameObject Lab3DataWindow;
    public Text[] showWeight = new Text[3];
    public Text[] showSpringLength = new Text[3];


    //window shows lab 4 data and the data
    public string[] lab4Speed = new string[3];
    public string[] lab4Angle = new string[3];
    public string[] lab4Distance = new string[3];

    public GameObject Lab4DataWindow;
    public Text[] showSpeed = new Text[3];
    public Text[] showAngle = new Text[3];
    public Text[] showDistance = new Text[3];



    private void Awake()
    {
        DM = this;
    }


    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://physiclab1-18247.firebaseio.com/");

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        Lab1DataWindow.SetActive(false);
        Lab3DataWindow.SetActive(false);
        Lab2DataWindow.SetActive(false);
        Lab4DataWindow.SetActive(false);
    }

    //show lab 1 data 
    public void OnLab1DataButtonClick()
    {
        Lab1DataWindow.SetActive(true);
        loadLab1Data();
    }

    //close data 1 window 
    public void OnLab1DataCloseButtonClick()
    {
        Lab1DataWindow.SetActive(false);
    }
    
    //save user name 
    public void saveUserName(User user, string ID)
    {
        Debug.Log("Calling save user name function");
        reference.Child("Users").Child(ID).Child("Name").SetValueAsync(user.userName);
    }


    //Load user name function1
    public void loadUserName()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").ValueChanged += LoadUserName;
    }

    //Load user name function2
    private void LoadUserName(object sender, ValueChangedEventArgs e)
    {
        userName  = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Name").GetValue(true).ToString();
        Debug.Log("user name is " + userName);
    }
    


    //save lab 1 length, time and peroid data into realtime database 
    public void saveLab1Data(string ID)
    {
        Debug.Log("Calling save lab1 data function");
        for (int i = 0; i < 6; i++)
        {
            reference.Child("Users").Child(ID).Child("Pendulum Lab").Child("Length").Child("Length "+ i.ToString()).SetValueAsync(PendulumData.penData.length[i]);
            reference.Child("Users").Child(ID).Child("Pendulum Lab").Child("Time").Child("Time " + i.ToString()).SetValueAsync(PendulumData.penData.totalTime[i]);
            reference.Child("Users").Child(ID).Child("Pendulum Lab").Child("Period").Child("Period " + i.ToString()).SetValueAsync(PendulumData.penData.period[i]);
        }

    }


    //load lab1 length, time and period data function1
    public void loadLab1Data()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").ValueChanged += loadPendulumData;
    }

    //load lab1 length, time and period data function2
    private void loadPendulumData(object sender, ValueChangedEventArgs e)
    {
        Debug.Log("calling load lab 1 data function");
     
        for (int i = 0; i<6; i++)
        {
            lab1Time[i] = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Pendulum Lab").Child("Time").Child("Time " + i.ToString()).GetValue(true).ToString();
            lab1Length[i] = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Pendulum Lab").Child("Length").Child("Length " + i.ToString()).GetValue(true).ToString();
            lab1Period[i] = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Pendulum Lab").Child("Period").Child("Period " + i.ToString()).GetValue(true).ToString();
        }

        for (int i = 0; i<6; i++)
        {
            showLength[i].text = lab1Length[i];
            showTime[i].text = lab1Time[i];
            showPeriod[i].text = lab1Period[i];
        }
   
    }

    //show lab 2 data window 
    public void OnLab2DataButtonClick()
    {
        Lab2DataWindow.SetActive(true);
        loadLab2Data();
    }

    //close lab 2 data window 
    public void OnLab2DataCloseButtonClick()
    {
        Lab2DataWindow.SetActive(false);
    }

    public void saveLab2Data(string ID)
    {
        Debug.Log("Calling save lab2 data function");

        reference.Child("Users").Child(ID).Child("Force Table Lab").Child("Red Angle").SetValueAsync(ForceTableData.ftd.redAngle.text);
        reference.Child("Users").Child(ID).Child("Force Table Lab").Child("Black Angle").SetValueAsync(ForceTableData.ftd.blackAngle.text);
        reference.Child("Users").Child(ID).Child("Force Table Lab").Child("Purple Angle").SetValueAsync(ForceTableData.ftd.purpleAngle.text);

        reference.Child("Users").Child(ID).Child("Force Table Lab").Child("Red Mass").SetValueAsync(ForceTableData.ftd.redMass.text);
        reference.Child("Users").Child(ID).Child("Force Table Lab").Child("Black Mass").SetValueAsync(ForceTableData.ftd.blackMass.text);
        reference.Child("Users").Child(ID).Child("Force Table Lab").Child("Purple Mass").SetValueAsync(ForceTableData.ftd.purpleMass.text);
    }

    //load lab 2 data function 1
    public void loadLab2Data()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").ValueChanged += loadForceTableData;
    }

    //load lab 3 data function 2
    private void loadForceTableData(object sender, ValueChangedEventArgs e)
    {
        Debug.Log("calling load lab 2 data function");

        lab2RedAngle = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Force Table Lab").Child("Red Angle").GetValue(true).ToString();
        lab2BlackAngle = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Force Table Lab").Child("Black Angle").GetValue(true).ToString();
        lab2PurpleAngle = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Force Table Lab").Child("Purple Angle").GetValue(true).ToString();

        lab2RedMass = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Force Table Lab").Child("Red Mass").GetValue(true).ToString();
        lab2BlackMass = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Force Table Lab").Child("Black Mass").GetValue(true).ToString();
        lab2PurpleMass = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Force Table Lab").Child("Purple Mass").GetValue(true).ToString();

        showRedAngle.text = lab2RedAngle;
        showBlackAngle.text = lab2BlackAngle;
        showPurpleAngle.text = lab2PurpleAngle;

        showRedMass.text = lab2RedMass;
        showBlackMass.text = lab2BlackMass;
        showPurpleMass.text = lab2PurpleMass;

    }

    //show lab 3 data window 
    public void OnLab3DataButtonClick()
    {
        Lab3DataWindow.SetActive(true);
        loadLab3Data();
    }

    //close lab 3 data window 
    public void OnLab3DataCloseButtonClick()
    {
        Lab3DataWindow.SetActive(false);
    }

    //save lab 3 data into realtime database 
    public void saveLab3Data(string ID)
    {
        Debug.Log("Calling save lab3 data function");
        for (int i = 0; i < 3; i++)
        {
            reference.Child("Users").Child(ID).Child("Spring Constant Lab").Child("Weight").Child("Weight " + i.ToString()).SetValueAsync(SpringData.spData.weight[i]);
            reference.Child("Users").Child(ID).Child("Spring Constant Lab").Child("Length").Child("Length " + i.ToString()).SetValueAsync(SpringData.spData.length[i]);
        }
    }

    //load lab 3 data function 1
    public void loadLab3Data()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").ValueChanged += loadSpringData;
    }

    //load lab 3 data function 2
    private void loadSpringData(object sender, ValueChangedEventArgs e)
    {
        Debug.Log("calling load lab 3 data function");

        for (int i = 0; i < 3; i++)
        {
            lab3Weight[i] = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Spring Constant Lab").Child("Weight").Child("Weight " + i.ToString()).GetValue(true).ToString();
            lab3SpringLength[i] = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Spring Constant Lab").Child("Length").Child("Length " + i.ToString()).GetValue(true).ToString();
        }

        for (int i = 0; i < 3; i++)
        {
            showWeight[i].text = lab3Weight[i];
            showSpringLength[i].text = lab3SpringLength[i];
        }

    }


    //show lab 4 data 
    public void OnLab4DataButtonClick()
    {
        Lab4DataWindow.SetActive(true);
        loadLab4Data();
    }

    //close data 4 window 
    public void OnLab4DataCloseButtonClick()
    {
        Lab4DataWindow.SetActive(false);
    }

    //save lab 4 speed, angle and distance data into realtime database 
    public void saveLab4Data(string ID)
    {
        Debug.Log("Calling save lab4 data function");
        for (int i = 0; i < 3; i++)
        {
            reference.Child("Users").Child(ID).Child("Projectile Lab").Child("Speed").Child("Speed " + i.ToString()).SetValueAsync(ProjectileData.pjtData.speed[i]);
            reference.Child("Users").Child(ID).Child("Projectile Lab").Child("Angle").Child("Angle " + i.ToString()).SetValueAsync(ProjectileData.pjtData.angle[i]);
            reference.Child("Users").Child(ID).Child("Projectile Lab").Child("Distance").Child("Distance " + i.ToString()).SetValueAsync(ProjectileData.pjtData.distance[i]);
        }

    }


    //load lab1 length, time and period data function1
    public void loadLab4Data()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").ValueChanged += loadProjectileData;
    }

    //load lab1 length, time and period data function2
    private void loadProjectileData(object sender, ValueChangedEventArgs e)
    {
        Debug.Log("calling load lab 4 data function");

        for (int i = 0; i < 3; i++)
        {
            lab4Speed[i] = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Projectile Lab").Child("Speed").Child("Speed " + i.ToString()).GetValue(true).ToString();
            lab4Angle[i] = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Projectile Lab").Child("Angle").Child("Angle " + i.ToString()).GetValue(true).ToString();
            lab4Distance[i] = e.Snapshot.Child(loginManager.LM.user.UserId).Child("Projectile Lab").Child("Distance").Child("Distance " + i.ToString()).GetValue(true).ToString();
        }

        for (int i = 0; i < 3; i++)
        {
            showSpeed[i].text = lab4Speed[i];
            showAngle[i].text = lab4Angle[i];
            showDistance[i].text = lab4Distance[i];
        }

    }


}


