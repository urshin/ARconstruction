using System.Collections;
using TMPro;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Firebase;
using System;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Functions;
using BackEnd.Game;
using Unity.PlasticSCM.Editor.WebApi;

public class FireBaseAuthManager : MonoBehaviour
{
    public string databaseUri = "https://arcontruction-c3e50-default-rtdb.asia-southeast1.firebasedatabase.app";

    [Header("Sign Up Properties")]
    [SerializeField] GameObject signUpPanel;
    [SerializeField] TMP_InputField nameInputFields;
    [SerializeField] TMP_InputField emailInputFields;
    [SerializeField] TMP_InputField passwordInputFields;
    [SerializeField] TMP_InputField comfirmedPasswordInputFields;

    [Header("Verification Properties")]
    [SerializeField] GameObject verificationPanel;
    [SerializeField] TextMeshProUGUI verificationText;

    [Header("Log In Properties")]
    [SerializeField] GameObject logInPanel;
    [SerializeField] TMP_InputField logInEmailInputFields;
    [SerializeField] TMP_InputField logInPasswordInputFields;

    [SerializeField] SceneChange sceneChange;
    FirebaseAuth auth;
    FirebaseUser user;
    FirebaseFunctions functions;

    bool isSignIn = false;
    bool isLogin = false;

    private void Start()
    {
        
        InitializeFirebase();
    }


    void InitializeFirebase()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(databaseUri);
        functions = Firebase.Functions.FirebaseFunctions.DefaultInstance;
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        string displayName;
        string emailAddress;

        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
                && auth.CurrentUser.IsValid();
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

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    public void Login()
    {
        StartCoroutine(LoginTry());
    }

    public IEnumerator LoginTry()
    {
        
       var logInTask = auth.SignInWithEmailAndPasswordAsync(logInEmailInputFields.text, logInPasswordInputFields.text).ContinueWith(
            task =>
            {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    Debug.Log(logInEmailInputFields.text + " 로 로그인 하셨습니다.");
                    isLogin = true;
                }
                else
                {
                    Debug.Log("로그인에 실패하셨습니다.");
                }
            }

        );

        yield return new WaitUntil(() => logInTask.IsCompleted);

        if (isLogin == true)
            SceneManager.LoadScene("ARconstruction");
    }

    public void Register()
    {
        // 제공되는 함수 : 이메일과 비밀번호로 회원가입 시켜 줌
        auth.CreateUserWithEmailAndPasswordAsync(emailInputFields.text, passwordInputFields.text).ContinueWith(
            task =>
            {
                if (!task.IsCanceled && !task.IsFaulted)
                {
                    Debug.Log(emailInputFields.text + "로 회원가입\n");
                    isSignIn = true;
                    SendData(this);
                }
                else
                    Debug.Log("회원가입 실패\n");
            }
            );

    }

    private static void SendData(FireBaseAuthManager instance)
    {
        DatabaseReference dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        UserData userData = new UserData(instance.emailInputFields.text, instance.passwordInputFields.text, instance.isSignIn);
        string dataJson = JsonUtility.ToJson(userData);
        dbReference.Child("DataList").Child("Data").SetRawJsonValueAsync(dataJson);

    }
}


public class UserData
{
    public string eMail;
    public string passWord;
    public bool acceptRegister;

    public UserData(string eMail, string passWord, bool acceptRegister)
    {
        this.eMail = eMail;
        this.passWord = passWord;
        this.acceptRegister = acceptRegister;
    }
}


