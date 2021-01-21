using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using System;
using Newtonsoft.Json;

public class FirebaseController
{
    #region singleton
    private static FirebaseController instance;

    public static FirebaseController Instance()
    {
        if (instance == null)
        {
            instance = new FirebaseController();
        }
        return instance;
    }

    #endregion

    public delegate void OnLoginHandler();
    public event OnLoginHandler onLogin;

    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference reference;
    public FirebaseApp app;


    public FirebaseController()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
                Debug.Log("firebaseInited");
            }
            else
            {
                Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
            Login();
        });
        
    }

    private void InitializeFirebase()
    {
        app = FirebaseApp.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(auth, null);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
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
            }
        }
    }

    public void Login()
    {
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(async task =>
       {
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
           FirebaseUser newUser = task.Result;
           Debug.LogFormat("User signed in successfully: {0} ({1})",
               newUser.DisplayName, newUser.UserId);
           onLogin?.Invoke();
       });
    }

    public void Save(PlayerInfo info)
    {
        string json = JsonConvert.SerializeObject(info);
        reference.Child("Users").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
        Debug.Log("saved");
    }

    public async Task<PlayerInfo> Load()
    {

        PlayerInfo info = new PlayerInfo();
        try
        {
            var snapshot = await FirebaseDatabase.DefaultInstance
                .GetReference("Users")
                .Child(auth.CurrentUser.UserId)
                .GetValueAsync();
            if (snapshot.Exists)
            {
                info = JsonConvert.DeserializeObject<PlayerInfo>(snapshot.GetRawJsonValue());
            }
            else
            {
                info = null;
            }

        }
        catch (FirebaseException e)
        {
            Debug.Log(e);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        return info;
    }

    public async Task<List<PlayerInfo>> LoadLeaders()
    {
        List<PlayerInfo> leaders = new List<PlayerInfo>();
        try
        {
            var snapshot = await FirebaseDatabase.DefaultInstance
                .GetReference("Users")
                .OrderByChild("bestScore")
                .GetValueAsync();
            if (snapshot.Exists)
            {
                leaders = JsonConvert.DeserializeObject<List<PlayerInfo>>(snapshot.GetRawJsonValue());
            }
            else
            {
                leaders = null;
            }
        }
        catch (FirebaseException e)
        {
            Debug.Log(e);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        return leaders;
    }
}