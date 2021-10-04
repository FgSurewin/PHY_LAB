using UnityEngine;
using System.Collections;
using Firebase.Auth;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class loginManager : MonoBehaviourPunCallbacks
{
    public static loginManager LM;

    // user input fields 
    public InputField loginEmail;
    public InputField loginPassword;
    public InputField signUpemail;
    public InputField signupPassword;
    public InputField signupName;


    public static bool loggedIn;
    
    // Firebase SDK objects 
    protected FirebaseAuth auth;
    public FirebaseUser user;


    protected string email = "";
    protected string password = "";
    protected string userName = "";

    protected string displayName = "";
    protected string emailAddress = "";
    
    public GameObject loginWindow;
    public GameObject RoomChoiceWindow;
    public GameObject registerWindow;

    public string roomName = "";  // use for creating new room

    private void Awake()
    {
        LM = this; //create the singleton, lives withing the login scene
    }

    void Start()
    {
        //Set the auth state of firebase
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

        PhotonNetwork.ConnectUsingSettings(); // connects to master thoton server

        //only display login window at the beginning
        loginWindow.SetActive(true);
        RoomChoiceWindow.SetActive(false);
        registerWindow.SetActive(false);
        loggedIn = false;

    }

    // Update is called once per frame
    void Update()
    {
        //after login, display room choice window
        if (loggedIn)
        {
            loginWindow.SetActive(false);
            RoomChoiceWindow.SetActive(true);
        }
        
    }


    public void loginUser()
    {
        //take user input
        email = loginEmail.text;
        password = loginPassword.text;
      

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser User = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                User.DisplayName, User.UserId);
            
            loggedIn = true;

            //Get user's name from firebase once login
            databaseManager.DM.loadUserName();
        });
    }


    public void signUpaNewUser()
    {
        //take user input
        email = signUpemail.text;
        password = signupPassword.text;
        userName = signupName.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase new user has been created. save user name and email to user object.
            User user1 = new User(userName, email);
          
            FirebaseUser newUser = task.Result;

            //store user's name to realtime database once sign up
            databaseManager.DM.saveUserName(user1, newUser.UserId);
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

        });
    }


    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }

            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
            }
        }
    }


    //Call back function after connect to photon server 
    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    //create or join photon room based on the room button
    public void OnRoomChoiceButtonClick( GameObject button)
    {

        if (button.name == "ChoosePendulumButton")
        {
            roomName = "Pendulum";
        }
        else if (button.name == "ChooseForceBalanceButton")
        {
            roomName = "Force Balance";
        }
        else if (button.name == "ChooseSpringConstantButton")
        {
            roomName = "Spring Constant";
        }
        else roomName = "Projectile";
        
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 20 };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOps, null);
    }

    //display sign up window when user want to sign up a new account 
     public void OngoSignUpButtonClick(GameObject button)
    {
        if (button.name == "SignUpButton")
        {
            registerWindow.SetActive(true);
            loginWindow.SetActive(false);
        }
      if(button.name == "ReturnButton")
        {
            registerWindow.SetActive(false);
            loginWindow.SetActive(true);
        }
    }

}