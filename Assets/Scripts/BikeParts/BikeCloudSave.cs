using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;

#if UNITY_IPHONE
using UnityEngine.iOS;
#endif

public partial class Bike
{
    void SaveCloud()
    {
        if (signin == 1)
        {
#if UNITY_ANDROID
                if (Social.localUser.authenticated)
                {
                    isSaving = true;
                    ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution("Stats",
                        DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
                }
#elif UNITY_IPHONE

#endif
        }
    }

    public void LoadCloud()
    {

        if (signin == 1)
        {
#if UNITY_ANDROID

                if (Social.localUser.authenticated)
                {
                    isSaving = false;
                    ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution("Stats",
                        DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
                }

#endif
        }
    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData,
        ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        if (originalData == null)
            resolver.ChooseMetadata(unmerged);
        else if (unmergedData == null)
            resolver.ChooseMetadata(original);
        else
        {

            string originalStr = Encoding.ASCII.GetString(originalData);
            string unmergedStr = Encoding.ASCII.GetString(unmergedData);

            int originalNum = int.Parse(originalStr);
            int unmergedNum = int.Parse(unmergedStr);

            if (originalNum > unmergedNum)
            {
                resolver.ChooseMetadata(original);
                return;
            }

            else if (unmergedNum > originalNum)
            {
                resolver.ChooseMetadata(unmerged);
                return;
            }

            resolver.ChooseMetadata(original);
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {

        if (status == SavedGameRequestStatus.Success)
        {

            if (!isSaving)
                LoadGame(game);

            else
                SaveGame(game);
        }

    }

    private void LoadGame(ISavedGameMetadata game)
    {
#if UNITY_ANDROID

        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
#endif
    }

    private void SaveGame(ISavedGameMetadata game)
    {
#if UNITY_ANDROID
        string stringToSave = GameDataToString();

        byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);

        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();

        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave,
            OnSavedGameDataWritten);
#endif
    }

    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {

    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {

        if (status == SavedGameRequestStatus.Success)
        {
            string cloudDataString;

            if (savedData.Length == 0)
            {
                cloudDataString = "0";
            }

            else
            {
                cloudDataString = Encoding.ASCII.GetString(savedData);

                allstats = cloudDataString.Split(":"[0]);
                PlayerPrefs.SetInt("coins", int.Parse(allstats[0]));
                PlayerPrefs.SetInt("slowmotion", int.Parse(allstats[1]));
                PlayerPrefs.SetInt("extracount", int.Parse(allstats[2]));
                PlayerPrefs.SetInt("antiminecount", int.Parse(allstats[3]));
                PlayerPrefs.SetInt("keypasscount", int.Parse(allstats[4]));
                PlayerPrefs.SetInt("passed", int.Parse(allstats[5]));
                passed = int.Parse(allstats[5]);
                string scores = "";
                string stars = "";
                for (int i = 6; i < 38; i++)
                {
                    if (i < 37)
                    {
                        scores = scores + allstats[i] + ":";
                    }
                    else
                    {
                        scores = scores + allstats[i];
                    }
                }
                for (int i = 38; i < 71; i++)
                {
                    if (i < 70)
                    {
                        stars = stars + allstats[i] + ":";
                    }
                    else
                    {
                        stars = stars + allstats[i];
                    }
                }
                PlayerPrefs.SetString("scores", scores);
                PlayerPrefs.SetString("stars", stars);
                allscores = scores.Split(":"[0]);
                allstars = stars.Split(":"[0]);
                AllScores();
                AllStars();
                if(set == 8)
                {
                    LevelReturn();
                }
                alevels = 0;
                foreach (Transform child in levels.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                supdated.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    string GameDataToString()
    {

        string final = null;
        passed = PlayerPrefs.GetInt("passed");
        string scores = PlayerPrefs.GetString("scores");
        stars = PlayerPrefs.GetString("stars");
        slowmotion = PlayerPrefs.GetInt("slowmotion");
        extracount = PlayerPrefs.GetInt("extracount");
        int antiminecount = PlayerPrefs.GetInt("antiminecount");
        int keypasscount = PlayerPrefs.GetInt("keypasscount");
        int coins = PlayerPrefs.GetInt("coins");
        final = coins + ":"  + slowmotion + ":" + extracount  + ":" + antiminecount + ":" + keypasscount + ":" + passed + ":" + scores + ":" + stars;

        return final;
    }

}
