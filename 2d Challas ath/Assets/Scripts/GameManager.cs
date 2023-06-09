using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    #region Variables

    public enum diceValue ///select dice value ///
    {
        none, one, two, three, four, eight
    }
    public diceValue selectDiceValue;


    private int totalBlueInHouse, totalRedInHouse, totalGreenInHouse, totalYellowInHouse;

    public GameObject frameRed, frameGreen, frameBlue, frameYellow;

    public GameObject blueScreen, greenScreen, redScreen, yellowScreen;


    public Vector3 redPlayerI_Pos, redPlayerII_Pos, redPlayerIII_Pos, redPlayerIV_Pos;
    public Vector3 greenPlayerI_Pos, greenPlayerII_Pos, greenPlayerIII_Pos, greenPlayerIV_Pos;
    public Vector3 bluePlayerI_Pos, bluePlayerII_Pos, bluePlayerIII_Pos, bluePlayerIV_Pos;
    public Vector3 yellowPlayerI_Pos, yellowPlayerII_Pos, yellowPlayerIII_Pos, yellowPlayerIV_Pos;

    public Button RedPlayerI_Button, RedPlayerII_Button, RedPlayerIII_Button, RedPlayerIV_Button;
    public Button GreenPlayerI_Button, GreenPlayerII_Button, GreenPlayerIII_Button, GreenPlayerIV_Button;
    public Button BluePlayerI_Button, BluePlayerII_Button, BluePlayerIII_Button, BluePlayerIV_Button;
    public Button YellowPlayerI_Button, YellowPlayerII_Button, YellowPlayerIII_Button, YellowPlayerIV_Button;


    public Transform diceRoll;
    public Button DiceRollButton;
    public Transform redDiceRollPos, greenDiceRollPos, blueDiceRollPos, yellowDiceRollPos;


    public GameObject redPlayerI_Border, redPlayerII_Border, redPlayerIII_Border, redPlayerIV_Border;
    public GameObject greenPlayerI_Border, greenPlayerII_Border, greenPlayerIII_Border, greenPlayerIV_Border;
    public GameObject bluePlayerI_Border, bluePlayerII_Border, bluePlayerIII_Border, bluePlayerIV_Border;
    public GameObject yellowPlayerI_Border, yellowPlayerII_Border, yellowPlayerIII_Border, yellowPlayerIV_Border;

    public string playerTurn = TagHolder.RED;
    public string curruntPlayer = TagHolder.NONE;
    public string curruntPlayerName = TagHolder.NONE;

    public GameObject redPlayerI, redPlayerII, redPlayerIII, redPlayerIV;
    public GameObject greenPlayerI, greenPlayerII, greenPlayerIII, greenPlayerIV;
    public GameObject bluePlayerI, bluePlayerII, bluePlayerIII, bluePlayerIV;
    public GameObject yellowPlayerI, yellowPlayerII, yellowPlayerIII, yellowPlayerIV;

    [SerializeField]
    private int redPlayerI_Steps, redPlayerII_Steps, redPlayerIII_Steps, redPlayerIV_Steps;
    [SerializeField]
    private int greenPlayerI_Steps, greenPlayerII_Steps, greenPlayerIII_Steps, greenPlayerIV_Steps;
    [SerializeField]
    private int bluePlayerI_Steps, bluePlayerII_Steps, bluePlayerIII_Steps, bluePlayerIV_Steps;
    [SerializeField]
    private int yellowPlayerI_Steps, yellowPlayerII_Steps, yellowPlayerIII_Steps, yellowPlayerIV_Steps;

    private int selectDiceNumAnimation;

    //--------------- Dice Animations------
    public GameObject dice1_Roll_Animation;
    public GameObject dice2_Roll_Animation;
    public GameObject dice3_Roll_Animation;
    public GameObject dice4_Roll_Animation;
    public GameObject dice8_Roll_Animation;

    // Players movement corenspoding to blocks...
    public List<GameObject> redMovementBlocks = new List<GameObject>();
    public List<GameObject> greenMovementBlocks = new List<GameObject>();
    public List<GameObject> yellowMovementBlocks = new List<GameObject>();
    public List<GameObject> blueMovementBlocks = new List<GameObject>();

    // Random generation of dice numbers...
    private System.Random randomNo;
    public GameObject confirmScreen;
    public GameObject gameCompletedScreen;


    //Players animation
    public List<BounceAnimation> animRedPlayer = new List<BounceAnimation>();
    public List<BounceAnimation> animGreenPlayer = new List<BounceAnimation>();
    public List<BounceAnimation> animBluePlayer = new List<BounceAnimation>();
    public List<BounceAnimation> animYellowPlayer = new List<BounceAnimation>();


    public List<GameObject> PlayersContainer = new List<GameObject>(4);




    #endregion
    #region Confirm Screeen
    public void yesMethod()
    {

        SoundManagerScript.buttonAudioSource.Play();
        SceneManager.LoadScene(0);


    }

    public void noMethod()
    {
        SoundManagerScript.buttonAudioSource.Play();
        confirmScreen.SetActive(false);
    }

    public void ExitMethod()
    {
        SoundManagerScript.buttonAudioSource.Play();
        confirmScreen.SetActive(true);
    }
    #endregion

    #region GameComplete
    IEnumerator GameCompletedRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        gameCompletedScreen.SetActive(true);
    }

    public void yesGameCompleted()
    {
        SoundManagerScript.buttonAudioSource.Play();
        SceneManager.LoadScene("Ludo");
    }

    public void noGameCompleted()
    {
        SoundManagerScript.buttonAudioSource.Play();
        SceneManager.LoadScene("Main Menu");
    }
    #endregion



    #region Utility Methods

    private void SetAllAnimationFalse()
    {
        dice1_Roll_Animation.SetActive(false);
        dice2_Roll_Animation.SetActive(false);
        dice3_Roll_Animation.SetActive(false);
        dice4_Roll_Animation.SetActive(false);
        dice8_Roll_Animation.SetActive(false);
    }
    private void SetAllAnimationFalseExcept(int animNo)
    {
        SetAllAnimationFalse();
        switch (animNo)
        {
            case 1:
                dice1_Roll_Animation.SetActive(true);
                break;
            case 2:
                dice2_Roll_Animation.SetActive(true);
                break;
            case 3:
                dice3_Roll_Animation.SetActive(true);
                break;
            case 4:
                dice4_Roll_Animation.SetActive(true);
                break;
            case 8:
                dice8_Roll_Animation.SetActive(true);
                break;

        }
    }

    public void SetPlayersAnimation(string str, bool I = false, bool II = false, bool III = false, bool IV = false)
    {

        //Debug.Log("animationPlayers " + str + "  values:" + I + II + III + IV);
        switch (str)
        {
            case TagHolder.RED:

                if (I) animRedPlayer[0].StartAnimation();
                else animRedPlayer[0].StopAnimation();

                if (II) animRedPlayer[1].StartAnimation();
                else animRedPlayer[1].StopAnimation();
                if (III) animRedPlayer[2].StartAnimation();
                else animRedPlayer[2].StopAnimation();
                if (IV) animRedPlayer[3].StartAnimation();
                else animRedPlayer[3].StopAnimation();

                break;

            case TagHolder.BLUE:
                if (I) animBluePlayer[0].StartAnimation();
                else animBluePlayer[0].StopAnimation();
                if (II) animBluePlayer[1].StartAnimation();
                else animBluePlayer[1].StopAnimation();
                if (III) animBluePlayer[2].StartAnimation();
                else animBluePlayer[2].StopAnimation();
                if (IV) animBluePlayer[3].StartAnimation();
                else animBluePlayer[3].StopAnimation();
                break;

            case TagHolder.GREEN:
                if (I) animGreenPlayer[0].StartAnimation();
                else animGreenPlayer[0].StopAnimation();
                if (II) animGreenPlayer[1].StartAnimation();
                else animGreenPlayer[1].StopAnimation();
                if (III) animGreenPlayer[2].StartAnimation();
                else animGreenPlayer[2].StopAnimation();
                if (IV) animGreenPlayer[3].StartAnimation();
                else animGreenPlayer[3].StopAnimation();
                break;

            case TagHolder.YELLOW:
                if (I) animYellowPlayer[0].StartAnimation();
                else animYellowPlayer[0].StopAnimation();
                if (II) animYellowPlayer[1].StartAnimation();
                else animYellowPlayer[1].StopAnimation();
                if (III) animYellowPlayer[2].StartAnimation();
                else animYellowPlayer[2].StopAnimation();
                if (IV) animYellowPlayer[3].StartAnimation();
                else animYellowPlayer[3].StopAnimation();
                break;
        }
    }
    public void SetBorder(string str, bool I = false, bool II = false, bool III = false, bool IV = false)
    {
        switch (str)
        {
            case TagHolder.RED:
                RedPlayerI_Button.interactable = I;
                RedPlayerII_Button.interactable = (II);
                RedPlayerIII_Button.interactable = (III);
                RedPlayerIV_Button.interactable = (IV);
                SetPlayersAnimation(TagHolder.RED, I, II, III, IV);


                break;

            case TagHolder.BLUE:
                BluePlayerI_Button.interactable = (I);
                BluePlayerII_Button.interactable = (II);
                BluePlayerIII_Button.interactable = (III);
                BluePlayerIV_Button.interactable = (IV);

                SetPlayersAnimation(TagHolder.BLUE, I, II, III, IV);

                break;

            case TagHolder.GREEN:
                GreenPlayerI_Button.interactable = (I);
                GreenPlayerII_Button.interactable = (II);
                GreenPlayerIII_Button.interactable = (III);
                GreenPlayerIV_Button.interactable = (IV);
                SetPlayersAnimation(TagHolder.GREEN, I, II, III, IV);

                break;

            case TagHolder.YELLOW:
                YellowPlayerI_Button.interactable = (I);
                YellowPlayerII_Button.interactable = (II);
                YellowPlayerIII_Button.interactable = (III);
                YellowPlayerIV_Button.interactable = (IV);

                SetPlayersAnimation(TagHolder.YELLOW, I, II, III, IV);

                break;
        }
    }
    public void SetButton(string str, bool I = false, bool II = false, bool III = false, bool IV = false)
    {
        switch (str)
        {
            case TagHolder.RED:
                redPlayerI_Border.SetActive(I);
                redPlayerII_Border.SetActive(II);
                redPlayerIII_Border.SetActive(III);
                redPlayerIV_Border.SetActive(IV);
                break;

            case TagHolder.BLUE:
                bluePlayerI_Border.SetActive(I);
                bluePlayerII_Border.SetActive(II);
                bluePlayerIII_Border.SetActive(III);
                bluePlayerIV_Border.SetActive(IV);
                break;

            case TagHolder.GREEN:
                greenPlayerI_Border.SetActive(I);
                greenPlayerII_Border.SetActive(II);
                greenPlayerIII_Border.SetActive(III);
                greenPlayerIV_Border.SetActive(IV);
                break;

            case TagHolder.YELLOW:
                yellowPlayerI_Border.SetActive(I);
                yellowPlayerII_Border.SetActive(II);
                yellowPlayerIII_Border.SetActive(III);
                yellowPlayerIV_Border.SetActive(IV);
                break;
        }
    }

    public void SetFrame(bool Red = false, bool Green = false, bool Blue = false, bool Yellow = false)
    {
        if (Red) PlayersContainer[0].transform.SetAsLastSibling();
        if (Green) PlayersContainer[1].transform.SetAsLastSibling();
        if (Blue) PlayersContainer[2].transform.SetAsLastSibling();
        if (Yellow) PlayersContainer[3].transform.SetAsLastSibling();

        frameRed.SetActive(Red);
        frameGreen.SetActive(Green);
        frameBlue.SetActive(Blue);
        frameYellow.SetActive(Yellow);
    }
    //private void SetFrameActive(string frameColor)
    //{
    //    switch (frameColor)
    //    {
    //        case TagHolder.RED:
    //            frameRed.SetActive(true);
    //            frameGreen.SetActive(false);
    //            frameBlue.SetActive(false);
    //            frameYellow.SetActive(false);
    //            break;
    //        case TagHolder.GREEN:
    //            frameRed.SetActive(false);
    //            frameGreen.SetActive(true);
    //            frameBlue.SetActive(false);
    //            frameYellow.SetActive(false);
    //            break;
    //        case TagHolder.BLUE:
    //            frameRed.SetActive(false);
    //            frameGreen.SetActive(false);
    //            frameBlue.SetActive(true);
    //            frameYellow.SetActive(false);
    //            break;
    //        case TagHolder.YELLOW:
    //            frameRed.SetActive(false);
    //            frameGreen.SetActive(false);
    //            frameBlue.SetActive(false);
    //            frameYellow.SetActive(true);
    //            break;
    //    }
    //}

    public void SetPlayer(string str, int num, bool beActive)
    {
        switch (str)
        {
            case TagHolder.RED:
                switch (num)
                {
                    case 1:
                        if (beActive)
                        {
                            redPlayerI_Border.SetActive(true);
                            RedPlayerI_Button.interactable = true;
                            animRedPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            redPlayerI_Border.SetActive(false);
                            RedPlayerI_Button.interactable = false;
                            animRedPlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 2:
                        if (beActive)
                        {
                            redPlayerII_Border.SetActive(true);
                            RedPlayerII_Button.interactable = true;
                            animRedPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            redPlayerII_Border.SetActive(false);
                            RedPlayerII_Button.interactable = false;
                            animRedPlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 3:
                        if (beActive)
                        {
                            redPlayerIII_Border.SetActive(true);
                            RedPlayerIII_Button.interactable = true;
                            animRedPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            redPlayerIII_Border.SetActive(false);
                            RedPlayerIII_Button.interactable = false;
                            animRedPlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 4:
                        if (beActive)
                        {
                            redPlayerIV_Border.SetActive(true);
                            RedPlayerIV_Button.interactable = true;
                            animRedPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            redPlayerIV_Border.SetActive(false);
                            RedPlayerIV_Button.interactable = false;
                            animRedPlayer[num - 1].StopAnimation();

                        }
                        break;




                }
                break;

            case TagHolder.BLUE:
                switch (num)
                {
                    case 1:
                        if (beActive)
                        {
                            bluePlayerI_Border.SetActive(true);
                            BluePlayerI_Button.interactable = true;
                            animBluePlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            bluePlayerI_Border.SetActive(false);
                            BluePlayerI_Button.interactable = false;
                            animBluePlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 2:
                        if (beActive)
                        {
                            bluePlayerII_Border.SetActive(true);
                            BluePlayerII_Button.interactable = true;
                            animBluePlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            bluePlayerII_Border.SetActive(false);
                            BluePlayerII_Button.interactable = false;
                            animBluePlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 3:
                        if (beActive)
                        {
                            bluePlayerIII_Border.SetActive(true);
                            BluePlayerIII_Button.interactable = true;
                            animBluePlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            bluePlayerIII_Border.SetActive(false);
                            BluePlayerIII_Button.interactable = false;
                            animBluePlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 4:
                        if (beActive)
                        {
                            bluePlayerIV_Border.SetActive(true);
                            BluePlayerIV_Button.interactable = true;
                            animBluePlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            bluePlayerIV_Border.SetActive(false);
                            BluePlayerIV_Button.interactable = false;
                            animBluePlayer[num - 1].StopAnimation();

                        }
                        break;
                }

                break;

            case TagHolder.GREEN:
                switch (num)
                {
                    case 1:
                        if (beActive)
                        {
                            greenPlayerI_Border.SetActive(true);
                            GreenPlayerI_Button.interactable = true;
                            animGreenPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            greenPlayerI_Border.SetActive(false);
                            GreenPlayerI_Button.interactable = false;
                            animGreenPlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 2:
                        if (beActive)
                        {
                            greenPlayerII_Border.SetActive(true);
                            GreenPlayerII_Button.interactable = true;
                            animGreenPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            greenPlayerII_Border.SetActive(false);
                            GreenPlayerII_Button.interactable = false;
                            animGreenPlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 3:
                        if (beActive)
                        {
                            greenPlayerIII_Border.SetActive(true);
                            GreenPlayerIII_Button.interactable = true;
                            animGreenPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            greenPlayerIII_Border.SetActive(false);
                            GreenPlayerIII_Button.interactable = false;
                            animGreenPlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 4:
                        if (beActive)
                        {
                            greenPlayerIV_Border.SetActive(true);
                            GreenPlayerIV_Button.interactable = true;
                            animGreenPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            greenPlayerIV_Border.SetActive(false);
                            GreenPlayerIV_Button.interactable = false;
                            animGreenPlayer[num - 1].StopAnimation();

                        }
                        break;
                }
                break;

            case TagHolder.YELLOW:
                switch (num)
                {
                    case 1:
                        if (beActive)
                        {
                            yellowPlayerI_Border.SetActive(true);
                            YellowPlayerI_Button.interactable = true;
                            animYellowPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            yellowPlayerI_Border.SetActive(false);
                            YellowPlayerI_Button.interactable = false;
                            animYellowPlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 2:
                        if (beActive)
                        {
                            yellowPlayerII_Border.SetActive(true);
                            YellowPlayerII_Button.interactable = true;
                            animYellowPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            yellowPlayerII_Border.SetActive(false);
                            YellowPlayerII_Button.interactable = false;
                            animYellowPlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 3:
                        if (beActive)
                        {
                            yellowPlayerIII_Border.SetActive(true);
                            YellowPlayerIII_Button.interactable = true;
                            animYellowPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            yellowPlayerIII_Border.SetActive(false);
                            YellowPlayerIII_Button.interactable = false;
                            animYellowPlayer[num - 1].StopAnimation();

                        }
                        break;

                    case 4:
                        if (beActive)
                        {
                            yellowPlayerIV_Border.SetActive(true);
                            YellowPlayerIV_Button.interactable = true;
                            animYellowPlayer[num - 1].StartAnimation();
                        }
                        else
                        {
                            yellowPlayerIV_Border.SetActive(false);
                            YellowPlayerIV_Button.interactable = false;
                            animYellowPlayer[num - 1].StopAnimation();

                        }
                        break;
                }

                break;
        }
    } // activates player border button and animation
    #endregion
    /* Tests
     * Blue cant kill-- solved -- test remain
     * on one players win no win screen and also include in next round
     * if all players cant be moved when dice eight, there is no second chance
    */
    private void InitializeDice()
    {

        DiceRollButton.interactable = true;
        SetAllAnimationFalse();
        #region //================= CHECKING PLAYERS WHO WINS DURING GAMEPLAY===================================

        switch (MainMenuScript.howManyPlayers)
        {
            case 2:
                if (totalRedInHouse > 3)
                {
                    SoundManagerScript.winAudioSource.Play();
                    redScreen.SetActive(true);
                    StartCoroutine("GameCompletedRoutine");
                    playerTurn = "NONE";
                }

                if (totalGreenInHouse > 3)
                {
                    SoundManagerScript.winAudioSource.Play();
                    greenScreen.SetActive(true);
                    StartCoroutine("GameCompletedRoutine");
                    playerTurn = "NONE";
                }
                break;

            case 3:
                // If any 1 of 3 player wins==============================================
                if (totalRedInHouse > 3 && totalBlueInHouse < 4 && totalYellowInHouse < 4 && playerTurn == "RED")
                {
                    if (!redScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }

                    redScreen.SetActive(true);
                    Debug.Log("Red Player WON");
                    playerTurn = "BLUE";
                }

                if (totalBlueInHouse > 3 && totalRedInHouse < 4 && totalYellowInHouse < 4 && playerTurn == "BLUE")
                {
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    blueScreen.SetActive(true);
                    Debug.Log("Blue Player WON");
                    playerTurn = "YELLOW";
                }

                if (totalYellowInHouse > 3 && totalRedInHouse < 4 && totalBlueInHouse < 4 && playerTurn == "YELLOW")
                {
                    if (!redScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    yellowScreen.SetActive(true);
                    Debug.Log("Yellow Player WON");
                    playerTurn = "RED";
                }
                // If any 2 of 3 player wins============================================== 
                if (totalRedInHouse > 3 && totalBlueInHouse > 3 && totalYellowInHouse < 4)
                {
                    if (!redScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    redScreen.SetActive(true);
                    blueScreen.SetActive(true);
                    StartCoroutine("GameCompletedRoutine");
                    Debug.Log("GAME ENDED");
                    playerTurn = "NONE";
                }

                if (totalBlueInHouse > 3 && totalYellowInHouse > 3 && totalRedInHouse < 4)
                {
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!yellowScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    yellowScreen.SetActive(true);
                    blueScreen.SetActive(true);
                    StartCoroutine("GameCompletedRoutine");
                    Debug.Log("GAME ENDED");
                    playerTurn = "NONE";
                }

                if (totalYellowInHouse > 3 && totalRedInHouse > 3 && totalBlueInHouse < 4)
                {
                    if (!yellowScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!redScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    redScreen.SetActive(true);
                    yellowScreen.SetActive(true);
                    StartCoroutine("GameCompletedRoutine");
                    Debug.Log("GAME ENDED");
                    playerTurn = "NONE";
                }

                break;

            case 4:
                // If any 1 of 4 player wins=======================================================================
                if (totalRedInHouse > 3 && totalBlueInHouse < 4 && totalGreenInHouse < 4 && totalYellowInHouse < 4 && playerTurn == "RED")
                {
                    if (!redScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }

                    redScreen.SetActive(true);
                    Debug.Log("Red Player WON");
                    playerTurn = "BLUE";
                }

                if (totalBlueInHouse > 3 && totalRedInHouse < 4 && totalGreenInHouse < 4 && totalYellowInHouse < 4 && playerTurn == "BLUE")
                {
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    blueScreen.SetActive(true);
                    Debug.Log("Blue Player WON");
                    playerTurn = "GREEN";
                }

                if (totalGreenInHouse > 3 && totalRedInHouse < 4 && totalBlueInHouse < 4 && totalYellowInHouse < 4 && playerTurn == "GREEN")
                {
                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    greenScreen.SetActive(true);
                    Debug.Log("Green Player WON");
                    playerTurn = "YELLOW";
                }

                if (totalYellowInHouse > 3 && totalRedInHouse < 4 && totalBlueInHouse < 4 && totalGreenInHouse < 4 && playerTurn == "YELLOW")
                {
                    if (!yellowScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    yellowScreen.SetActive(true);
                    Debug.Log("Yellow Player WON");
                    playerTurn = "RED";
                }
                // If any 2 of 4 player wins=======================================================================
                if (totalRedInHouse > 3 && totalBlueInHouse > 3 && totalGreenInHouse < 4 && totalYellowInHouse < 4 && (playerTurn == "RED" || playerTurn == "BLUE"))
                {
                    if (!redScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    redScreen.SetActive(true);
                    blueScreen.SetActive(true);
                    Debug.Log("RED & BLUE - ALREADY WON");
                    playerTurn = "GREEN";
                }

                if (totalBlueInHouse > 3 && totalGreenInHouse > 3 && totalRedInHouse < 4 && totalYellowInHouse < 4 && (playerTurn == "BLUE" || playerTurn == "GREEN"))
                {
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    blueScreen.SetActive(true);
                    greenScreen.SetActive(true);
                    Debug.Log("GREEN & BLUE - ALREADY WON");
                    playerTurn = "YELLOW";
                }

                if (totalGreenInHouse > 3 && totalYellowInHouse > 3 && totalBlueInHouse < 4 && totalRedInHouse < 4 && (playerTurn == "GREEN" || playerTurn == "YELLOW"))
                {
                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!yellowScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    greenScreen.SetActive(true);
                    yellowScreen.SetActive(true);
                    Debug.Log("GREEN & YELLOW - ALREADY WON");
                    playerTurn = "RED";
                }

                if (totalYellowInHouse > 3 && totalRedInHouse > 3 && totalBlueInHouse < 4 && totalGreenInHouse < 4 && (playerTurn == "YELLOW" || playerTurn == "RED"))
                {
                    if (!redScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!yellowScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    redScreen.SetActive(true);
                    yellowScreen.SetActive(true);
                    Debug.Log("YELLOW & RED - ALREADY WON");
                    playerTurn = "BLUE";
                }

                //	Diagonally----Red Vs Green ... Yellow Vs Blue winning
                if (totalYellowInHouse > 3 && totalBlueInHouse > 3 && totalRedInHouse < 4 && totalGreenInHouse < 4 && (playerTurn == "YELLOW" || playerTurn == "BLUE"))
                {
                    if (!yellowScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    yellowScreen.SetActive(true);
                    blueScreen.SetActive(true);
                    Debug.Log("BLUE & YELLOW - ALREADY WON");

                    if (playerTurn == "BLUE")
                    {
                        playerTurn = "GREEN";
                    }
                    else
                    {
                        if (playerTurn == "YELLOW")
                        {
                            playerTurn = "RED";
                        }
                    }
                }
                if (totalRedInHouse > 3 && totalGreenInHouse > 3 && totalYellowInHouse < 4 && totalBlueInHouse < 4 && (playerTurn == "RED" || playerTurn == "GREEN"))
                {
                    if (!redScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    redScreen.SetActive(true);
                    greenScreen.SetActive(true);
                    Debug.Log("RED & GREEN - ALREADY WON");

                    if (playerTurn == "RED")
                    {
                        playerTurn = "BLUE";
                    }
                    else
                    {
                        if (playerTurn == "GREEN")
                        {
                            playerTurn = "YELLOW";
                        }
                    }
                }
                if (totalRedInHouse > 3 && totalGreenInHouse > 3 && totalBlueInHouse > 3 && totalYellowInHouse < 4)
                {
                    if (!redScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    redScreen.SetActive(true);
                    greenScreen.SetActive(true);
                    blueScreen.SetActive(true);
                    StartCoroutine("GameCompletedRoutine");
                    playerTurn = "NONE";
                }

                if (totalRedInHouse > 3 && totalGreenInHouse > 3 && totalYellowInHouse > 3 && totalBlueInHouse < 4)
                {
                    if (!redScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!yellowScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    redScreen.SetActive(true);
                    greenScreen.SetActive(true);
                    yellowScreen.SetActive(true);
                    StartCoroutine("GameCompletedRoutine");
                    playerTurn = "NONE";
                }

                if (totalBlueInHouse > 3 && totalGreenInHouse > 3 && totalYellowInHouse > 3 && totalRedInHouse < 4)
                {
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!greenScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!yellowScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    blueScreen.SetActive(true);
                    greenScreen.SetActive(true);
                    yellowScreen.SetActive(true);
                    StartCoroutine("GameCompletedRoutine");
                    playerTurn = "NONE";
                }

                if (totalBlueInHouse > 3 && totalRedInHouse > 3 && totalYellowInHouse > 3 && totalGreenInHouse < 4)
                {
                    if (!blueScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!redScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    if (!yellowScreen.activeInHierarchy)
                    {
                        SoundManagerScript.winAudioSource.Play();
                    }
                    blueScreen.SetActive(true);
                    redScreen.SetActive(true);
                    yellowScreen.SetActive(true);
                    StartCoroutine("GameCompletedRoutine");
                    playerTurn = "NONE";
                }
                break;
        }
        #endregion

        #region ======== Getting curruntPlayer VALUE=======
        if (curruntPlayerName.Contains(TagHolder.RED_PLAYER))
        {
            if (curruntPlayerName == PlayerName.RED_PLAYER_1)
            {
                Debug.Log("curruntPlayerName = " + curruntPlayerName);
                curruntPlayer = RedPlayerI_Script.redPlayerI_ColName;

            }
            if (curruntPlayerName == PlayerName.RED_PLAYER_2)
            {
                Debug.Log("curruntPlayerName = " + curruntPlayerName);
                curruntPlayer = RedPlayerII_Script.redPlayerII_ColName;
            }
            if (curruntPlayerName == PlayerName.RED_PLAYER_3)
            {
                Debug.Log("curruntPlayerName = " + curruntPlayerName);
                curruntPlayer = RedPlayerIII_Script.redPlayerIII_ColName;
            }
            if (curruntPlayerName == PlayerName.RED_PLAYER_4)
            {
                Debug.Log("curruntPlayerName = " + curruntPlayerName);
                curruntPlayer = RedPlayerIV_Script.redPlayerIV_ColName;
            }
        }

        if (curruntPlayerName.Contains(TagHolder.BLUE_PLAYER))
        {
            if (curruntPlayerName == PlayerName.BLUE_PLAYER_1)
                curruntPlayer = BluePlayerI_Script.bluePlayerI_ColName;
            if (curruntPlayerName == PlayerName.BLUE_PLAYER_2)
                curruntPlayer = BluePlayerII_Script.bluePlayerII_ColName;
            if (curruntPlayerName == PlayerName.BLUE_PLAYER_3)
                curruntPlayer = BluePlayerIII_Script.bluePlayerIII_ColName;
            if (curruntPlayerName == PlayerName.BLUE_PLAYER_4)
                curruntPlayer = BluePlayerIV_Script.bluePlayerIV_ColName;


        }

        if (curruntPlayerName.Contains(TagHolder.GREEN_PLAYER))
        {
            if (curruntPlayerName == PlayerName.GREEN_PLAYER_1)
            {
                Debug.Log("curruntPlayerName = " + curruntPlayerName);
                curruntPlayer = GreenPlayerI_Script.greenPlayerI_ColName;
            }
            if (curruntPlayerName == PlayerName.GREEN_PLAYER_2)
            {
                Debug.Log("curruntPlayerName = " + curruntPlayerName);
                curruntPlayer = GreenPlayerII_Script.greenPlayerII_ColName;
            }
            if (curruntPlayerName == PlayerName.GREEN_PLAYER_3)
            {
                Debug.Log("curruntPlayerName = " + curruntPlayerName);
                curruntPlayer = GreenPlayerIII_Script.greenPlayerIII_ColName;
            }
            if (curruntPlayerName == PlayerName.GREEN_PLAYER_4)
            {
                Debug.Log("curruntPlayerName = " + curruntPlayerName);
                curruntPlayer = GreenPlayerIV_Script.greenPlayerIV_ColName;
            }
        }

        if (curruntPlayerName.Contains(TagHolder.YELLOW_PLAYER))
        {
            if (curruntPlayerName == PlayerName.YELLOW_PLAYER_1)
                curruntPlayer = YellowPlayerI_Script.yellowPlayerI_ColName;
            if (curruntPlayerName == PlayerName.YELLOW_PLAYER_2)
                curruntPlayer = YellowPlayerII_Script.yellowPlayerII_ColName;
            if (curruntPlayerName == PlayerName.YELLOW_PLAYER_3)
                curruntPlayer = YellowPlayerIII_Script.yellowPlayerIII_ColName;
            if (curruntPlayerName == PlayerName.YELLOW_PLAYER_4)
                curruntPlayer = YellowPlayerIV_Script.yellowPlayerIV_ColName;
        }

        //Debug.Log("curruntPlayerName = " + curruntPlayerName+ "    Currunt Step : "+curruntPlayer);
        #endregion

        #region //================== Player vs Opponent=========================================================
        if (curruntPlayerName != TagHolder.NONE)
        {
            switch (MainMenuScript.howManyPlayers)
            {
                case 2:
                    if (curruntPlayerName.Contains(TagHolder.RED_PLAYER))
                    {
                        // Checking if player can eliminate other player and they are on same block
                        // (blockName [spot 1 -24 ]== greenPlayer1 Spot name) and  (curruntSpot  not a star  and greenplayer1 spot not a star  
                        if (curruntPlayer == GreenPlayerI_Script.greenPlayerI_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerI_Script.greenPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerI.transform.position = greenPlayerI_Pos;
                            GreenPlayerI_Script.greenPlayerI_ColName = TagHolder.NONE;
                            greenPlayerI_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == GreenPlayerII_Script.greenPlayerII_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerII_Script.greenPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerII.transform.position = greenPlayerII_Pos;
                            GreenPlayerII_Script.greenPlayerII_ColName = TagHolder.NONE;
                            greenPlayerII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == GreenPlayerIII_Script.greenPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerIII_Script.greenPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerIII.transform.position = greenPlayerIII_Pos;
                            GreenPlayerIII_Script.greenPlayerIII_ColName = TagHolder.NONE;
                            greenPlayerIII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == GreenPlayerIV_Script.greenPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerIV_Script.greenPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerIV.transform.position = greenPlayerIV_Pos;
                            GreenPlayerIV_Script.greenPlayerIV_ColName = TagHolder.NONE;
                            greenPlayerIV_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                    }
                    if (curruntPlayerName.Contains(TagHolder.GREEN_PLAYER))
                    {
                        if (curruntPlayer == RedPlayerI_Script.redPlayerI_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerI_Script.redPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerI.transform.position = redPlayerI_Pos;
                            RedPlayerI_Script.redPlayerI_ColName = TagHolder.NONE;
                            redPlayerI_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == RedPlayerII_Script.redPlayerII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerII_Script.redPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerII.transform.position = redPlayerII_Pos;
                            RedPlayerII_Script.redPlayerII_ColName = TagHolder.NONE;
                            redPlayerII_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == RedPlayerIII_Script.redPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIII_Script.redPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIII.transform.position = redPlayerIII_Pos;
                            RedPlayerIII_Script.redPlayerIII_ColName = TagHolder.NONE;
                            redPlayerIII_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == RedPlayerIV_Script.redPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIV_Script.redPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIV.transform.position = redPlayerIV_Pos;
                            RedPlayerIV_Script.redPlayerIV_ColName = TagHolder.NONE;
                            redPlayerIV_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                    }
                    break;

                case 3:
                    if (curruntPlayerName.Contains(TagHolder.RED_PLAYER))
                    {
                        if (curruntPlayer == YellowPlayerI_Script.yellowPlayerI_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerI_Script.yellowPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerI.transform.position = yellowPlayerI_Pos;
                            YellowPlayerI_Script.yellowPlayerI_ColName = TagHolder.NONE;
                            yellowPlayerI_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == YellowPlayerII_Script.yellowPlayerII_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerII_Script.yellowPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerII.transform.position = yellowPlayerII_Pos;
                            YellowPlayerII_Script.yellowPlayerII_ColName = TagHolder.NONE;
                            yellowPlayerII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == YellowPlayerIII_Script.yellowPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerIII_Script.yellowPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerIII.transform.position = yellowPlayerIII_Pos;
                            YellowPlayerIII_Script.yellowPlayerIII_ColName = TagHolder.NONE;
                            yellowPlayerIII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == YellowPlayerIV_Script.yellowPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerIV_Script.yellowPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerIV.transform.position = yellowPlayerIV_Pos;
                            YellowPlayerIV_Script.yellowPlayerIV_ColName = TagHolder.NONE;
                            yellowPlayerIV_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }

                        if (curruntPlayer == BluePlayerI_Script.bluePlayerI_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerI_Script.bluePlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerI.transform.position = bluePlayerI_Pos;
                            BluePlayerI_Script.bluePlayerI_ColName = TagHolder.NONE;
                            bluePlayerI_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == BluePlayerII_Script.bluePlayerII_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerII_Script.bluePlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerII.transform.position = bluePlayerII_Pos;
                            BluePlayerII_Script.bluePlayerII_ColName = TagHolder.NONE;
                            bluePlayerII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == BluePlayerIII_Script.bluePlayerIII_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerIII_Script.bluePlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerIII.transform.position = bluePlayerIII_Pos;
                            BluePlayerIII_Script.bluePlayerIII_ColName = TagHolder.NONE;
                            bluePlayerIII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == BluePlayerIV_Script.bluePlayerIV_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerIV_Script.bluePlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerIV.transform.position = bluePlayerIV_Pos;
                            BluePlayerIV_Script.bluePlayerIV_ColName = TagHolder.NONE;
                            bluePlayerIV_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                    }

                    if (curruntPlayerName.Contains(TagHolder.YELLOW_PLAYER))
                    {
                        if (curruntPlayer == RedPlayerI_Script.redPlayerI_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerI_Script.redPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerI.transform.position = redPlayerI_Pos;
                            RedPlayerI_Script.redPlayerI_ColName = TagHolder.NONE;
                            redPlayerI_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == RedPlayerII_Script.redPlayerII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerII_Script.redPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerII.transform.position = redPlayerII_Pos;
                            RedPlayerII_Script.redPlayerII_ColName = TagHolder.NONE;
                            redPlayerII_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == RedPlayerIII_Script.redPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIII_Script.redPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIII.transform.position = redPlayerIII_Pos;
                            RedPlayerIII_Script.redPlayerIII_ColName = TagHolder.NONE;
                            redPlayerIII_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == RedPlayerIV_Script.redPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIV_Script.redPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIV.transform.position = redPlayerIV_Pos;
                            RedPlayerIV_Script.redPlayerIV_ColName = TagHolder.NONE;
                            redPlayerIV_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }

                        if (curruntPlayer == BluePlayerI_Script.bluePlayerI_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerI_Script.bluePlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerI.transform.position = bluePlayerI_Pos;
                            BluePlayerI_Script.bluePlayerI_ColName = TagHolder.NONE;
                            bluePlayerI_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == BluePlayerII_Script.bluePlayerII_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerII_Script.bluePlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerII.transform.position = bluePlayerII_Pos;
                            BluePlayerII_Script.bluePlayerII_ColName = TagHolder.NONE;
                            bluePlayerII_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == BluePlayerIII_Script.bluePlayerIII_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerIII_Script.bluePlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerIII.transform.position = bluePlayerIII_Pos;
                            BluePlayerIII_Script.bluePlayerIII_ColName = TagHolder.NONE;
                            bluePlayerIII_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == BluePlayerIV_Script.bluePlayerIV_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerIV_Script.bluePlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerIV.transform.position = bluePlayerIV_Pos;
                            BluePlayerIV_Script.bluePlayerIV_ColName = TagHolder.NONE;
                            bluePlayerIV_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                    }

                    if (curruntPlayerName.Contains(TagHolder.BLUE_PLAYER))
                    {
                        if (curruntPlayer == RedPlayerI_Script.redPlayerI_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerI_Script.redPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerI.transform.position = redPlayerI_Pos;
                            RedPlayerI_Script.redPlayerI_ColName = TagHolder.NONE;
                            redPlayerI_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == RedPlayerII_Script.redPlayerII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerII_Script.redPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerII.transform.position = redPlayerII_Pos;
                            RedPlayerII_Script.redPlayerII_ColName = TagHolder.NONE;
                            redPlayerII_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == RedPlayerIII_Script.redPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIII_Script.redPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIII.transform.position = redPlayerIII_Pos;
                            RedPlayerIII_Script.redPlayerIII_ColName = TagHolder.NONE;
                            redPlayerIII_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == RedPlayerIV_Script.redPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIV_Script.redPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIV.transform.position = redPlayerIV_Pos;
                            RedPlayerIV_Script.redPlayerIV_ColName = TagHolder.NONE;
                            redPlayerIV_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }

                        if (curruntPlayer == YellowPlayerI_Script.yellowPlayerI_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerI_Script.yellowPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerI.transform.position = yellowPlayerI_Pos;
                            YellowPlayerI_Script.yellowPlayerI_ColName = TagHolder.NONE;
                            yellowPlayerI_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == YellowPlayerII_Script.yellowPlayerII_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerII_Script.yellowPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerII.transform.position = yellowPlayerII_Pos;
                            YellowPlayerII_Script.yellowPlayerII_ColName = TagHolder.NONE;
                            yellowPlayerII_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == YellowPlayerIII_Script.yellowPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerIII_Script.yellowPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerIII.transform.position = yellowPlayerIII_Pos;
                            YellowPlayerIII_Script.yellowPlayerIII_ColName = TagHolder.NONE;
                            yellowPlayerIII_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == YellowPlayerIV_Script.yellowPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerIV_Script.yellowPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerIV.transform.position = yellowPlayerIV_Pos;
                            YellowPlayerIV_Script.yellowPlayerIV_ColName = TagHolder.NONE;
                            yellowPlayerIV_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                    }
                    break;

                case 4:
                    if (curruntPlayerName.Contains(TagHolder.RED_PLAYER))
                    {
                        if (curruntPlayer == GreenPlayerI_Script.greenPlayerI_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerI_Script.greenPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerI.transform.position = greenPlayerI_Pos;
                            GreenPlayerI_Script.greenPlayerI_ColName = TagHolder.NONE;
                            greenPlayerI_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == GreenPlayerII_Script.greenPlayerII_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerII_Script.greenPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerII.transform.position = greenPlayerII_Pos;
                            GreenPlayerII_Script.greenPlayerII_ColName = TagHolder.NONE;
                            greenPlayerII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == GreenPlayerIII_Script.greenPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerIII_Script.greenPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerIII.transform.position = greenPlayerIII_Pos;
                            GreenPlayerIII_Script.greenPlayerIII_ColName = TagHolder.NONE;
                            greenPlayerIII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == GreenPlayerIV_Script.greenPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerIV_Script.greenPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerIV.transform.position = greenPlayerIV_Pos;
                            GreenPlayerIV_Script.greenPlayerIV_ColName = TagHolder.NONE;
                            greenPlayerIV_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }

                        if (curruntPlayer == BluePlayerI_Script.bluePlayerI_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerI_Script.bluePlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerI.transform.position = bluePlayerI_Pos;
                            BluePlayerI_Script.bluePlayerI_ColName = TagHolder.NONE;
                            bluePlayerI_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == BluePlayerII_Script.bluePlayerII_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerII_Script.bluePlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerII.transform.position = bluePlayerII_Pos;
                            BluePlayerII_Script.bluePlayerII_ColName = TagHolder.NONE;
                            bluePlayerII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == BluePlayerIII_Script.bluePlayerIII_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerIII_Script.bluePlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerIII.transform.position = bluePlayerIII_Pos;
                            BluePlayerIII_Script.bluePlayerIII_ColName = TagHolder.NONE;
                            bluePlayerIII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == BluePlayerIV_Script.bluePlayerIV_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerIV_Script.bluePlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerIV.transform.position = bluePlayerIV_Pos;
                            BluePlayerIV_Script.bluePlayerIV_ColName = TagHolder.NONE;
                            bluePlayerIV_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }

                        if (curruntPlayer == YellowPlayerI_Script.yellowPlayerI_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerI_Script.yellowPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerI.transform.position = yellowPlayerI_Pos;
                            YellowPlayerI_Script.yellowPlayerI_ColName = TagHolder.NONE;
                            yellowPlayerI_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == YellowPlayerII_Script.yellowPlayerII_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerII_Script.yellowPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerII.transform.position = yellowPlayerII_Pos;
                            YellowPlayerII_Script.yellowPlayerII_ColName = TagHolder.NONE;
                            yellowPlayerII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == YellowPlayerIII_Script.yellowPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerIII_Script.yellowPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerIII.transform.position = yellowPlayerIII_Pos;
                            YellowPlayerIII_Script.yellowPlayerIII_ColName = TagHolder.NONE;
                            yellowPlayerIII_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                        if (curruntPlayer == YellowPlayerIV_Script.yellowPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerIV_Script.yellowPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerIV.transform.position = yellowPlayerIV_Pos;
                            YellowPlayerIV_Script.yellowPlayerIV_ColName = TagHolder.NONE;
                            yellowPlayerIV_Steps = 0;
                            playerTurn = TagHolder.RED;
                        }
                    }

                    if (curruntPlayerName.Contains(TagHolder.GREEN_PLAYER))
                    {
                        if (curruntPlayer == RedPlayerI_Script.redPlayerI_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerI_Script.redPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerI.transform.position = redPlayerI_Pos;
                            RedPlayerI_Script.redPlayerI_ColName = TagHolder.NONE;
                            redPlayerI_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == RedPlayerII_Script.redPlayerII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerII_Script.redPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerII.transform.position = redPlayerII_Pos;
                            RedPlayerII_Script.redPlayerII_ColName = TagHolder.NONE;
                            redPlayerII_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == RedPlayerIII_Script.redPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIII_Script.redPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIII.transform.position = redPlayerIII_Pos;
                            RedPlayerIII_Script.redPlayerIII_ColName = TagHolder.NONE;
                            redPlayerIII_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == RedPlayerIV_Script.redPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIV_Script.redPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIV.transform.position = redPlayerIV_Pos;
                            RedPlayerIV_Script.redPlayerIV_ColName = TagHolder.NONE;
                            redPlayerIV_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }

                        if (curruntPlayer == BluePlayerI_Script.bluePlayerI_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerI_Script.bluePlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerI.transform.position = bluePlayerI_Pos;
                            BluePlayerI_Script.bluePlayerI_ColName = TagHolder.NONE;
                            bluePlayerI_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == BluePlayerII_Script.bluePlayerII_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerII_Script.bluePlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerII.transform.position = bluePlayerII_Pos;
                            BluePlayerII_Script.bluePlayerII_ColName = TagHolder.NONE;
                            bluePlayerII_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == BluePlayerIII_Script.bluePlayerIII_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerIII_Script.bluePlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerIII.transform.position = bluePlayerIII_Pos;
                            BluePlayerIII_Script.bluePlayerIII_ColName = TagHolder.NONE;
                            bluePlayerIII_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == BluePlayerIV_Script.bluePlayerIV_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerIV_Script.bluePlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerIV.transform.position = bluePlayerIV_Pos;
                            BluePlayerIV_Script.bluePlayerIV_ColName = TagHolder.NONE;
                            bluePlayerIV_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }

                        if (curruntPlayer == YellowPlayerI_Script.yellowPlayerI_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerI_Script.yellowPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerI.transform.position = yellowPlayerI_Pos;
                            YellowPlayerI_Script.yellowPlayerI_ColName = TagHolder.NONE;
                            yellowPlayerI_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == YellowPlayerII_Script.yellowPlayerII_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerII_Script.yellowPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerII.transform.position = yellowPlayerII_Pos;
                            YellowPlayerII_Script.yellowPlayerII_ColName = TagHolder.NONE;
                            yellowPlayerII_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == YellowPlayerIII_Script.yellowPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerIII_Script.yellowPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerIII.transform.position = yellowPlayerIII_Pos;
                            YellowPlayerIII_Script.yellowPlayerIII_ColName = TagHolder.NONE;
                            yellowPlayerIII_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                        if (curruntPlayer == YellowPlayerIV_Script.yellowPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerIV_Script.yellowPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerIV.transform.position = yellowPlayerIV_Pos;
                            YellowPlayerIV_Script.yellowPlayerIV_ColName = TagHolder.NONE;
                            yellowPlayerIV_Steps = 0;
                            playerTurn = TagHolder.GREEN;
                        }
                    }

                    if (curruntPlayerName.Contains(TagHolder.BLUE_PLAYER))
                    {
                        if (curruntPlayer == RedPlayerI_Script.redPlayerI_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerI_Script.redPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerI.transform.position = redPlayerI_Pos;
                            RedPlayerI_Script.redPlayerI_ColName = TagHolder.NONE;
                            redPlayerI_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == RedPlayerII_Script.redPlayerII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerII_Script.redPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerII.transform.position = redPlayerII_Pos;
                            RedPlayerII_Script.redPlayerII_ColName = TagHolder.NONE;
                            redPlayerII_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == RedPlayerIII_Script.redPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIII_Script.redPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIII.transform.position = redPlayerIII_Pos;
                            RedPlayerIII_Script.redPlayerIII_ColName = TagHolder.NONE;
                            redPlayerIII_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == RedPlayerIV_Script.redPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIV_Script.redPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIV.transform.position = redPlayerIV_Pos;
                            RedPlayerIV_Script.redPlayerIV_ColName = TagHolder.NONE;
                            redPlayerIV_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }

                        if (curruntPlayer == GreenPlayerI_Script.greenPlayerI_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerI_Script.greenPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerI.transform.position = greenPlayerI_Pos;
                            GreenPlayerI_Script.greenPlayerI_ColName = TagHolder.NONE;
                            greenPlayerI_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == GreenPlayerII_Script.greenPlayerII_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerII_Script.greenPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerII.transform.position = greenPlayerII_Pos;
                            GreenPlayerII_Script.greenPlayerII_ColName = TagHolder.NONE;
                            greenPlayerII_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == GreenPlayerIII_Script.greenPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerIII_Script.greenPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerIII.transform.position = greenPlayerIII_Pos;
                            GreenPlayerIII_Script.greenPlayerIII_ColName = TagHolder.NONE;
                            greenPlayerIII_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == GreenPlayerIV_Script.greenPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerIV_Script.greenPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerIV.transform.position = greenPlayerIV_Pos;
                            GreenPlayerIV_Script.greenPlayerIV_ColName = TagHolder.NONE;
                            greenPlayerIV_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }

                        if (curruntPlayer == YellowPlayerI_Script.yellowPlayerI_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerI_Script.yellowPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerI.transform.position = yellowPlayerI_Pos;
                            YellowPlayerI_Script.yellowPlayerI_ColName = TagHolder.NONE;
                            yellowPlayerI_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == YellowPlayerII_Script.yellowPlayerII_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerII_Script.yellowPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerII.transform.position = yellowPlayerII_Pos;
                            YellowPlayerII_Script.yellowPlayerII_ColName = TagHolder.NONE;
                            yellowPlayerII_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == YellowPlayerIII_Script.yellowPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerIII_Script.yellowPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerIII.transform.position = yellowPlayerIII_Pos;
                            YellowPlayerIII_Script.yellowPlayerIII_ColName = TagHolder.NONE;
                            yellowPlayerIII_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                        if (curruntPlayer == YellowPlayerIV_Script.yellowPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && YellowPlayerIV_Script.yellowPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            yellowPlayerIV.transform.position = yellowPlayerIV_Pos;
                            YellowPlayerIV_Script.yellowPlayerIV_ColName = TagHolder.NONE;
                            yellowPlayerIV_Steps = 0;
                            playerTurn = TagHolder.BLUE;
                        }
                    }

                    if (curruntPlayerName.Contains(TagHolder.YELLOW_PLAYER))
                    {
                        if (curruntPlayer == RedPlayerI_Script.redPlayerI_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerI_Script.redPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerI.transform.position = redPlayerI_Pos;
                            RedPlayerI_Script.redPlayerI_ColName = TagHolder.NONE;
                            redPlayerI_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == RedPlayerII_Script.redPlayerII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerII_Script.redPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerII.transform.position = redPlayerII_Pos;
                            RedPlayerII_Script.redPlayerII_ColName = TagHolder.NONE;
                            redPlayerII_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == RedPlayerIII_Script.redPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIII_Script.redPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIII.transform.position = redPlayerIII_Pos;
                            RedPlayerIII_Script.redPlayerIII_ColName = TagHolder.NONE;
                            redPlayerIII_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == RedPlayerIV_Script.redPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && RedPlayerIV_Script.redPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            redPlayerIV.transform.position = redPlayerIV_Pos;
                            RedPlayerIV_Script.redPlayerIV_ColName = TagHolder.NONE;
                            redPlayerIV_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }

                        if (curruntPlayer == GreenPlayerI_Script.greenPlayerI_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerI_Script.greenPlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerI.transform.position = greenPlayerI_Pos;
                            GreenPlayerI_Script.greenPlayerI_ColName = TagHolder.NONE;
                            greenPlayerI_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == GreenPlayerII_Script.greenPlayerII_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerII_Script.greenPlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerII.transform.position = greenPlayerII_Pos;
                            GreenPlayerII_Script.greenPlayerII_ColName = TagHolder.NONE;
                            greenPlayerII_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == GreenPlayerIII_Script.greenPlayerIII_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerIII_Script.greenPlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerIII.transform.position = greenPlayerIII_Pos;
                            GreenPlayerIII_Script.greenPlayerIII_ColName = TagHolder.NONE;
                            greenPlayerIII_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == GreenPlayerIV_Script.greenPlayerIV_ColName && (curruntPlayer != TagHolder.STAR && GreenPlayerIV_Script.greenPlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            greenPlayerIV.transform.position = greenPlayerIV_Pos;
                            GreenPlayerIV_Script.greenPlayerIV_ColName = TagHolder.NONE;
                            greenPlayerIV_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }

                        if (curruntPlayer == BluePlayerI_Script.bluePlayerI_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerI_Script.bluePlayerI_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerI.transform.position = bluePlayerI_Pos;
                            BluePlayerI_Script.bluePlayerI_ColName = TagHolder.NONE;
                            bluePlayerI_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == BluePlayerII_Script.bluePlayerII_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerII_Script.bluePlayerII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerII.transform.position = bluePlayerII_Pos;
                            BluePlayerII_Script.bluePlayerII_ColName = TagHolder.NONE;
                            bluePlayerII_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == BluePlayerIII_Script.bluePlayerIII_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerIII_Script.bluePlayerIII_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerIII.transform.position = bluePlayerIII_Pos;
                            BluePlayerIII_Script.bluePlayerIII_ColName = TagHolder.NONE;
                            bluePlayerIII_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                        if (curruntPlayer == BluePlayerIV_Script.bluePlayerIV_ColName && (curruntPlayer != TagHolder.STAR && BluePlayerIV_Script.bluePlayerIV_ColName != TagHolder.STAR))
                        {
                            SoundManagerScript.dismissalAudioSource.Play();
                            bluePlayerIV.transform.position = bluePlayerIV_Pos;
                            BluePlayerIV_Script.bluePlayerIV_ColName = TagHolder.NONE;
                            bluePlayerIV_Steps = 0;
                            playerTurn = TagHolder.YELLOW;
                        }
                    }
                    break;
            }
        }
        #endregion

        switch (MainMenuScript.howManyPlayers)
        {
            case 2:
                if (playerTurn == TagHolder.RED)
                {
                    diceRoll.position = redDiceRollPos.position;

                    SetFrame(Red: true);
                }
                if (playerTurn == TagHolder.GREEN)
                {
                    diceRoll.position = greenDiceRollPos.position;

                    SetFrame(Green: true);

                }

                SetButton(TagHolder.GREEN);

                SetButton(TagHolder.RED);

                //=============ANIMATED ROUND BORDER=======
                SetBorder(TagHolder.RED);

                SetBorder(TagHolder.GREEN);
                //======================================
                break;

            case 3:
                if (playerTurn == TagHolder.RED)
                {
                    diceRoll.position = redDiceRollPos.position;

                    SetFrame(Red: true);
                }
                if (playerTurn == TagHolder.YELLOW)
                {
                    diceRoll.position = yellowDiceRollPos.position;

                    SetFrame(Yellow: true);
                }
                if (playerTurn == TagHolder.BLUE)
                {
                    diceRoll.position = blueDiceRollPos.position;

                    SetFrame(Blue: true);
                }

                SetButton(TagHolder.RED);
                SetButton(TagHolder.YELLOW);
                SetButton(TagHolder.BLUE);
                //=============ANIMATED ROUND BORDER==================================================================
                SetBorder(TagHolder.RED);
                SetBorder(TagHolder.YELLOW);
                SetBorder(TagHolder.BLUE);


                //======================================
                break;

            case 4:
                if (playerTurn == TagHolder.RED)
                {
                    diceRoll.position = redDiceRollPos.position;

                    SetFrame(Red: true);
                }
                if (playerTurn == TagHolder.GREEN)
                {
                    diceRoll.position = greenDiceRollPos.position;

                    SetFrame(Green: true);

                }
                if (playerTurn == TagHolder.YELLOW)
                {
                    diceRoll.position = yellowDiceRollPos.position;

                    SetFrame(Yellow: true);
                }
                if (playerTurn == TagHolder.BLUE)
                {
                    diceRoll.position = blueDiceRollPos.position;

                    SetFrame(Blue: true);
                }
                SetButton(TagHolder.RED);
                SetButton(TagHolder.YELLOW);
                SetButton(TagHolder.BLUE);
                SetButton(TagHolder.GREEN);
                //=============ANIMATED ROUND BORDER==================================================================
                SetBorder(TagHolder.RED);
                SetBorder(TagHolder.YELLOW);
                SetBorder(TagHolder.BLUE);
                SetBorder(TagHolder.GREEN);
                break;

        }
    }//initializeDice


    public void DiceRoll()
    {

        SoundManagerScript.diceAudioSource.Play();
        DiceRollButton.interactable = false;

        selectDiceNumAnimation = randomNo.Next(1, 6);
        if (selectDiceNumAnimation == 5) { selectDiceNumAnimation = 8; }


        switch (selectDiceValue)
        {
            case diceValue.none:

                break;
            case diceValue.one:
                selectDiceNumAnimation = 1;
                break;
            case diceValue.two:
                selectDiceNumAnimation = 2;
                break;
            case diceValue.three:
                selectDiceNumAnimation = 3;
                break;
            case diceValue.four:
                selectDiceNumAnimation = 4;
                break;
            case diceValue.eight:
                selectDiceNumAnimation = 8;
                break;
        }

        switch (selectDiceNumAnimation)
        {
            case 1:
                SetAllAnimationFalseExcept(1);
                break;

            case 2:
                SetAllAnimationFalseExcept(2);
                break;

            case 3:
                SetAllAnimationFalseExcept(3);
                break;

            case 4:
                SetAllAnimationFalseExcept(4);
                break;

            case 8:
                SetAllAnimationFalseExcept(8);
                break;

        }

        StartCoroutine("PlayersNotInitialized");
    }


    IEnumerator PlayersNotInitialized()
    {
        yield return new WaitForSeconds(0.8f);
        // Game Start Initial position of each player (Red, Green, Blue, Yellow)
        switch (playerTurn)
        {
            case TagHolder.RED:
                #region Red
                //==================== CONDITION FOR BORDER GLOW ========================

                if ((redMovementBlocks.Count - redPlayerI_Steps) >= selectDiceNumAnimation && redPlayerI_Steps >= 0 && (redMovementBlocks.Count > redPlayerI_Steps))
                {
                    SetPlayer(TagHolder.RED, 1, true);

                }
                else
                {
                    SetPlayer(TagHolder.RED, 1, false);
                }

                if ((redMovementBlocks.Count - redPlayerII_Steps) >= selectDiceNumAnimation && redPlayerII_Steps >= 0 && (redMovementBlocks.Count > redPlayerII_Steps))
                {
                    SetPlayer(TagHolder.RED, 2, true);
                }
                else
                {
                    SetPlayer(TagHolder.RED, 2, false);

                }

                if ((redMovementBlocks.Count - redPlayerIII_Steps) >= selectDiceNumAnimation && redPlayerIII_Steps >= 0 && (redMovementBlocks.Count > redPlayerIII_Steps))
                {
                    SetPlayer(TagHolder.RED, 3, true);

                }
                else
                {
                    SetPlayer(TagHolder.RED, 3, false);

                }

                if ((redMovementBlocks.Count - redPlayerIV_Steps) >= selectDiceNumAnimation && redPlayerIV_Steps >= 0 && (redMovementBlocks.Count > redPlayerIV_Steps))
                {
                    SetPlayer(TagHolder.RED, 4, true);

                }
                else
                {

                    SetPlayer(TagHolder.RED, 4, false);
                }

                //========================= PLAYERS BORDER GLOW WHEN OPENING ===========================================

                //if (selectDiceNumAnimation == 8 && redPlayerI_Steps == 0)
                //{

                //    SetPlayer(TagHolder.RED, 1, true);

                //}
                //if (selectDiceNumAnimation == 8 && redPlayerII_Steps == 0)
                //{
                //    SetPlayer(TagHolder.RED, 2, true);

                //}
                //if (selectDiceNumAnimation == 8 && redPlayerIII_Steps == 0)
                //{
                //    SetPlayer(TagHolder.RED, 3, true);

                //}
                //if (selectDiceNumAnimation == 8 && redPlayerIV_Steps == 0)
                //{
                //    SetPlayer(TagHolder.RED, 4, true);

                //}

                //====================== PLAYERS DON'T HAVE ANY MOVES ,SWITCH TO NEXT TURN===============================
                if (!redPlayerI_Border.activeInHierarchy && !redPlayerII_Border.activeInHierarchy &&
                   !redPlayerIII_Border.activeInHierarchy && !redPlayerIV_Border.activeInHierarchy)
                {
                    if (selectDiceNumAnimation == 8 || selectDiceNumAnimation == 4)
                    {
                        playerTurn = TagHolder.RED;
                        InitializeDice();

                        break;
                    }
                    SetButton(TagHolder.RED);

                    switch (MainMenuScript.howManyPlayers)
                    {
                        case 2:
                            playerTurn = TagHolder.GREEN;
                            InitializeDice();
                            break;

                        case 3:
                            playerTurn = TagHolder.BLUE;
                            InitializeDice();
                            break;

                        case 4:
                            playerTurn = TagHolder.BLUE;
                            InitializeDice();
                            break;
                    }
                }
                break;
            #endregion
            case TagHolder.GREEN:
                #region Green
                //==================== CONDITION FOR BORDER GLOW ========================
                if ((greenMovementBlocks.Count - greenPlayerI_Steps) >= selectDiceNumAnimation && greenPlayerI_Steps >= 0 && (greenMovementBlocks.Count > greenPlayerI_Steps))

                    SetPlayer(TagHolder.GREEN, 1, true);
                else SetPlayer(TagHolder.GREEN, 1, false);


                if ((greenMovementBlocks.Count - greenPlayerII_Steps) >= selectDiceNumAnimation && greenPlayerII_Steps >= 0 && (greenMovementBlocks.Count > greenPlayerII_Steps))

                    SetPlayer(TagHolder.GREEN, 2, true);
                else SetPlayer(TagHolder.GREEN, 2, false);


                if ((greenMovementBlocks.Count - greenPlayerIII_Steps) >= selectDiceNumAnimation && greenPlayerIII_Steps >= 0 && (greenMovementBlocks.Count > greenPlayerIII_Steps))
                    SetPlayer(TagHolder.GREEN, 3, true);
                else SetPlayer(TagHolder.GREEN, 3, false);


                if ((greenMovementBlocks.Count - greenPlayerIV_Steps) >= selectDiceNumAnimation && greenPlayerIV_Steps >= 0 && (greenMovementBlocks.Count > greenPlayerIV_Steps))
                    SetPlayer(TagHolder.GREEN, 4, true);
                else SetPlayer(TagHolder.GREEN, 4, false);

                //==========================Glow Border when dice hit 8 and player inside house==========================================

                //if (selectDiceNumAnimation == 8 && greenPlayerI_Steps == 0)
                //{


                //    SetPlayer(TagHolder.GREEN, 1, true);

                //}
                //if (selectDiceNumAnimation == 8 && greenPlayerII_Steps == 0)
                //{
                //    SetPlayer(TagHolder.GREEN, 2, true);

                //}
                //if (selectDiceNumAnimation == 8 && greenPlayerIII_Steps == 0)
                //{
                //    SetPlayer(TagHolder.GREEN, 3, true);

                //}
                //if (selectDiceNumAnimation == 8 && greenPlayerIV_Steps == 0)
                //{
                //    SetPlayer(TagHolder.GREEN, 4, true);

                //}


                //====================== PLAYERS DON'T HAVE ANY MOVES ,SWITCH TO NEXT TURN===============================
                if (!greenPlayerI_Border.activeInHierarchy && !greenPlayerII_Border.activeInHierarchy &&
                    !greenPlayerIII_Border.activeInHierarchy && !greenPlayerIV_Border.activeInHierarchy)
                {
                    if (selectDiceNumAnimation == 8 || selectDiceNumAnimation == 4)
                    {
                        playerTurn = TagHolder.GREEN;
                        InitializeDice();

                        break;
                    }
                    SetButton(TagHolder.GREEN);

                    switch (MainMenuScript.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "RED";
                            InitializeDice();
                            break;

                        case 3:
                            //GREEN PLAYER IS NOT AVAILABLE
                            break;

                        case 4:
                            playerTurn = TagHolder.YELLOW;
                            InitializeDice();
                            break;
                    }
                }
                break;
            #endregion
            case TagHolder.BLUE:
                #region Blue
                //==================== CONDITION FOR BORDER GLOW ========================
                if ((blueMovementBlocks.Count - bluePlayerI_Steps) >= selectDiceNumAnimation && bluePlayerI_Steps >= 0 && (blueMovementBlocks.Count > bluePlayerI_Steps))

                    SetPlayer(TagHolder.BLUE, 1, true);
                else

                    SetPlayer(TagHolder.BLUE, 1, false);

                if ((blueMovementBlocks.Count - bluePlayerII_Steps) >= selectDiceNumAnimation && bluePlayerII_Steps >= 0 && (blueMovementBlocks.Count > bluePlayerII_Steps))
                    SetPlayer(TagHolder.BLUE, 2, true);
                else
                    SetPlayer(TagHolder.BLUE, 2, false);




                if ((blueMovementBlocks.Count - bluePlayerIII_Steps) >= selectDiceNumAnimation && bluePlayerIII_Steps >= 0 && (blueMovementBlocks.Count > bluePlayerIII_Steps))
                    SetPlayer(TagHolder.BLUE, 3, true);
                else
                    SetPlayer(TagHolder.BLUE, 3, false);



                if ((blueMovementBlocks.Count - bluePlayerIV_Steps) >= selectDiceNumAnimation && bluePlayerIV_Steps >= 0 && (blueMovementBlocks.Count > bluePlayerIV_Steps))
                    SetPlayer(TagHolder.BLUE, 4, true);
                else
                    SetPlayer(TagHolder.BLUE, 4, false);


                //==========================Glow Border when dice hit 8 and player inside house==========================================
                //if (selectDiceNumAnimation == 8 && bluePlayerI_Steps == 0)
                //{
                //    SetPlayer(TagHolder.BLUE, 1, true);
                //}
                //if (selectDiceNumAnimation == 8 && bluePlayerII_Steps == 0)
                //{
                //    SetPlayer(TagHolder.BLUE, 2, true);

                //}
                //if (selectDiceNumAnimation == 8 && bluePlayerIII_Steps == 0)
                //{
                //    SetPlayer(TagHolder.BLUE, 3, true);

                //}
                //if (selectDiceNumAnimation == 8 && bluePlayerIV_Steps == 0)
                //{
                //    SetPlayer(TagHolder.BLUE, 4, true);

                //}

                //====================== PLAYERS DON'T HAVE ANY MOVES ,SWITCH TO NEXT TURN===============================
                if (!bluePlayerI_Border.activeInHierarchy && !bluePlayerII_Border.activeInHierarchy &&
                    !bluePlayerIII_Border.activeInHierarchy && !bluePlayerIV_Border.activeInHierarchy)
                {
                    if (selectDiceNumAnimation == 8 || selectDiceNumAnimation == 4)
                    {
                        playerTurn = TagHolder.BLUE;
                        InitializeDice();

                        break;
                    }
                    SetButton(TagHolder.BLUE);

                    switch (MainMenuScript.howManyPlayers)
                    {
                        case 2:
                            //BLUE PLAYER NOT AVAILABLE
                            break;

                        case 3:
                            playerTurn = TagHolder.YELLOW;
                            InitializeDice();
                            break;

                        case 4:
                            playerTurn = TagHolder.GREEN;
                            InitializeDice();
                            break;
                    }
                }
                break;
            #endregion
            case TagHolder.YELLOW:
                #region Yellow
                //==================== CONDITION FOR BORDER GLOW ========================
                if ((yellowMovementBlocks.Count - yellowPlayerI_Steps) >= selectDiceNumAnimation && yellowPlayerI_Steps >= 0 && (yellowMovementBlocks.Count > yellowPlayerI_Steps))
                {

                    SetPlayer(TagHolder.YELLOW, 1, true);
                }
                else
                {
                    SetPlayer(TagHolder.YELLOW, 1, false);

                }

                if ((yellowMovementBlocks.Count - yellowPlayerII_Steps) >= selectDiceNumAnimation && yellowPlayerII_Steps >= 0 && (yellowMovementBlocks.Count > yellowPlayerII_Steps))
                {

                    SetPlayer(TagHolder.YELLOW, 2, true);
                }
                else
                {

                    SetPlayer(TagHolder.YELLOW, 2, false);
                }

                if ((yellowMovementBlocks.Count - yellowPlayerIII_Steps) >= selectDiceNumAnimation && yellowPlayerIII_Steps >= 0 && (yellowMovementBlocks.Count > yellowPlayerIII_Steps))
                {
                    SetPlayer(TagHolder.YELLOW, 3, true);
                }
                else
                {
                    SetPlayer(TagHolder.YELLOW, 3, false);

                }

                if ((yellowMovementBlocks.Count - yellowPlayerIV_Steps) >= selectDiceNumAnimation && yellowPlayerIV_Steps >= 0 && (yellowMovementBlocks.Count > yellowPlayerIV_Steps))
                {
                    SetPlayer(TagHolder.YELLOW, 4, true);
                }
                else
                {
                    SetPlayer(TagHolder.YELLOW, 4, false);

                }
                //==========================Glow Border when dice hit 8 and player inside house==========================================


                //if (selectDiceNumAnimation == 8 && yellowPlayerI_Steps == 0)
                //{
                //    SetPlayer(TagHolder.YELLOW, 1, true);

                //}
                //if (selectDiceNumAnimation == 8 && yellowPlayerII_Steps == 0)
                //{
                //    SetPlayer(TagHolder.YELLOW, 2, true);

                //}
                //if (selectDiceNumAnimation == 8 && yellowPlayerIII_Steps == 0)
                //{
                //    SetPlayer(TagHolder.YELLOW, 3, true);

                //}
                //if (selectDiceNumAnimation == 8 && yellowPlayerIV_Steps == 0)
                //{
                //    SetPlayer(TagHolder.YELLOW, 4, true);

                //}

                //====================== PLAYERS DON'T HAVE ANY MOVES ,SWITCH TO NEXT TURN===============================
                if (!yellowPlayerI_Border.activeInHierarchy && !yellowPlayerII_Border.activeInHierarchy &&
                    !yellowPlayerIII_Border.activeInHierarchy && !yellowPlayerIV_Border.activeInHierarchy)
                {
                    if (selectDiceNumAnimation == 8 || selectDiceNumAnimation == 4)
                    {
                        playerTurn = TagHolder.YELLOW;
                        InitializeDice();

                        break;
                    }
                    SetButton(TagHolder.YELLOW);

                    switch (MainMenuScript.howManyPlayers)
                    {
                        case 2:
                            //yellow PLAYER NOT AVAILABLE
                            break;

                        case 3:
                            playerTurn = "RED";
                            InitializeDice();
                            break;

                        case 4:
                            playerTurn = "RED";
                            InitializeDice();
                            break;
                    }
                }
                break;
                #endregion
        }
    }//players Not initialized



    public void PlayerMovement(string _color, List<GameObject> _MovementBlocks, ref int playerSteps, string _curruntPlayerName, ref GameObject _playerObject,
                                        ref int _TotalPlayersInHouse, ref Button _PlayerButton, int _OtherPlayerStepsAddition)
    {
        SoundManagerScript.playerAudioSource.Play();
        SetBorder(_color);
        SetPlayersAnimation(_color);
        SetButton(_color);
        string organizePlayers = _color;
        // 24          -      [0-24]         
        if (playerTurn == _color && (_MovementBlocks.Count - playerSteps) > selectDiceNumAnimation)//dice outcome is small than remaining steps to take
        {

            if (playerSteps >= 0)
            {

                Vector3[] Player_Path = new Vector3[selectDiceNumAnimation];

                for (int i = 0; i < Player_Path.Length; i++)
                {
                    Player_Path[i] = _MovementBlocks[playerSteps + i].transform.position;
                }

                playerSteps += selectDiceNumAnimation;

                if (selectDiceNumAnimation == 8 || selectDiceNumAnimation == 4)
                {
                    playerTurn = _color;
                }
                else
                {
                    if (_color == TagHolder.RED)
                    {

                        switch (MainMenuScript.howManyPlayers)
                        {
                            case 2:
                                playerTurn = TagHolder.GREEN;
                                break;

                            case 3:
                                playerTurn = TagHolder.BLUE;
                                break;

                            case 4:
                                playerTurn = TagHolder.BLUE;
                                break;
                        }
                    }
                    if (_color == TagHolder.GREEN)
                    {
                        switch (MainMenuScript.howManyPlayers)
                        {
                            case 2:
                                playerTurn = "RED";
                                break;

                            case 3:
                                //Player is not available
                                break;

                            case 4:
                                playerTurn = TagHolder.YELLOW;
                                break;
                        }
                    }
                    if (_color == TagHolder.BLUE)
                    {
                        switch (MainMenuScript.howManyPlayers)
                        {
                            case 2:
                                // Player is not available...
                                break;

                            case 3:
                                playerTurn = TagHolder.YELLOW;
                                break;

                            case 4:
                                playerTurn = TagHolder.GREEN;
                                break;
                        }
                    }
                    if (_color == TagHolder.YELLOW)
                    {
                        switch (MainMenuScript.howManyPlayers)
                        {
                            case 2:
                                // Player is not available...
                                break;

                            case 3:
                                playerTurn = "RED";
                                break;

                            case 4:
                                playerTurn = "RED";
                                break;
                        }
                    }
                }

                curruntPlayerName = _curruntPlayerName;

                //if(playerSteps + selectDiceNumAnimation == redMovementBlocks.Count)
                if (Player_Path.Length > 1)
                {
                    //_playerObject.transform.DOPath (Player_Path, 2.0f, PathType.Linear, PathMode.Full3D, 10, Color.red);
                    iTween.MoveTo(_playerObject, iTween.Hash("path", Player_Path, "time", 1.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
                }
                else
                {
                    iTween.MoveTo(_playerObject, iTween.Hash("position", Player_Path[0], "time", 1.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
                }
                

            }
            //else
            //{
            //    if (selectDiceNumAnimation == 8 && playerSteps == 0)
            //    {
            //        Vector3[] Player_Path = new Vector3[1];
            //        Player_Path[0] = _MovementBlocks[playerSteps].transform.position;
            //        playerSteps += 1;
            //        playerTurn = _color;
            //        //curruntPlayer = RedPlayerI_Script.redPlayerI_ColName;
            //        curruntPlayerName = _curruntPlayerName;

            //        iTween.MoveTo(_playerObject, iTween.Hash("position", Player_Path[0],  "time", 1.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
            //    }
            //}

        }
        else
        {
            // Condition when Player Coin is reached successfully in House....(Actual Number of required moves to get into the House)
            if (playerTurn == _color && (_MovementBlocks.Count - playerSteps) == selectDiceNumAnimation)
            {
                Vector3[] Player_Path = new Vector3[selectDiceNumAnimation];

                for (int i = 0; i < selectDiceNumAnimation; i++)
                {
                    Player_Path[i] = _MovementBlocks[playerSteps + i].transform.position;
                }

                playerSteps += selectDiceNumAnimation;

                playerTurn = _color;

                //playerSteps = 0;

                if (Player_Path.Length > 1)
                {
                    iTween.MoveTo(_playerObject, iTween.Hash("path", Player_Path, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
                }
                else
                {
                    iTween.MoveTo(_playerObject, iTween.Hash("position", Player_Path[0], "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
                }
                _TotalPlayersInHouse += 1;
                Debug.Log("Cool !!");
                _PlayerButton.enabled = false;
                

            }
            else
            {
                Debug.Log("You need " + (_MovementBlocks.Count - playerSteps).ToString() + " to enter into the house.");

                if (_OtherPlayerStepsAddition == 0 && selectDiceNumAnimation != 6)
                {
                    if (_color == TagHolder.RED)
                    {

                        switch (MainMenuScript.howManyPlayers)
                        {
                            case 2:
                                playerTurn = TagHolder.GREEN;
                                break;

                            case 3:
                                playerTurn = TagHolder.BLUE;
                                break;

                            case 4:
                                playerTurn = TagHolder.BLUE;
                                break;
                        }
                    }
                    if (_color == TagHolder.GREEN)
                    {
                        switch (MainMenuScript.howManyPlayers)
                        {
                            case 2:
                                playerTurn = "RED";
                                break;

                            case 3:
                                //Player is not available
                                break;

                            case 4:
                                playerTurn = TagHolder.YELLOW;
                                break;
                        }
                    }
                    if (_color == TagHolder.BLUE)
                    {
                        switch (MainMenuScript.howManyPlayers)
                        {
                            case 2:
                                // Player is not available...
                                break;

                            case 3:
                                playerTurn = TagHolder.YELLOW;
                                break;

                            case 4:
                                playerTurn = TagHolder.GREEN;
                                break;
                        }
                    }
                    if (_color == TagHolder.YELLOW)
                    {
                        switch (MainMenuScript.howManyPlayers)
                        {
                            case 2:
                                // Player is not available...
                                break;

                            case 3:
                                playerTurn = "RED";
                                break;

                            case 4:
                                playerTurn = "RED";
                                break;
                        }
                    }
                    InitializeDice();
                }
            }
        }
        OrganizePlayer(organizePlayers);
    }

    struct PlayerScripts
    {
        GameObject _player;
        
    }
  void AddToDic(ref Dictionary<string, List<GameObject>> playerData ,string block, GameObject player)
    {
        
        if(playerData.ContainsKey(block))
        {
            playerData[block].Add(player);
        }
        else
        {
            playerData.Add(block , new List<GameObject>() { player});
        }
    }

    private void OrganizePlayer(string colorOfPlayer)
    {
        Dictionary<string, List<GameObject>> playerData = new Dictionary<string, List<GameObject>>();
       
        AddToDic(ref playerData, RedPlayerI_Script.redPlayerI_ColName, redPlayerI); 
        AddToDic(ref playerData, RedPlayerII_Script.redPlayerII_ColName, redPlayerII);
        AddToDic(ref playerData, RedPlayerIII_Script.redPlayerIII_ColName, redPlayerIII);
        AddToDic(ref playerData, RedPlayerIV_Script.redPlayerIV_ColName, redPlayerIV);
        AddToDic(ref playerData, GreenPlayerI_Script.greenPlayerI_ColName, greenPlayerI);
        AddToDic(ref playerData, GreenPlayerII_Script.greenPlayerII_ColName, greenPlayerII);
        AddToDic(ref playerData, GreenPlayerIII_Script.greenPlayerIII_ColName, greenPlayerIII);
        AddToDic(ref playerData, GreenPlayerIV_Script.greenPlayerIV_ColName, greenPlayerIV);

        //playerData[RedPlayerI_Script.redPlayerI_ColName].Add(redPlayerI);
        //playerData[RedPlayerII_Script.redPlayerII_ColName].Add(redPlayerII);
        //playerData[RedPlayerIII_Script.redPlayerIII_ColName].Add(redPlayerIII);
        //playerData[RedPlayerIV_Script.redPlayerIV_ColName].Add(redPlayerIV); 
        
        //playerData[BluePlayerI_Script.bluePlayerI_ColName].Add(bluePlayerI);
        //playerData[BluePlayerII_Script.bluePlayerII_ColName].Add(bluePlayerII);
        //playerData[BluePlayerIII_Script.bluePlayerIII_ColName].Add(bluePlayerIII);
        //playerData[BluePlayerIV_Script.bluePlayerIV_ColName].Add(bluePlayerIV);

        //playerData[GreenPlayerI_Script.greenPlayerI_ColName].Add(greenPlayerI);
        //playerData[GreenPlayerII_Script.greenPlayerII_ColName].Add(greenPlayerII);
        //playerData[GreenPlayerIII_Script.greenPlayerIII_ColName].Add(greenPlayerIII);
        //playerData[GreenPlayerIV_Script.greenPlayerIV_ColName].Add(greenPlayerIV);

        //playerData[YellowPlayerI_Script.yellowPlayerI_ColName].Add(yellowPlayerI);
        //playerData[YellowPlayerII_Script.yellowPlayerII_ColName].Add(yellowPlayerII);
        //playerData[YellowPlayerIII_Script.yellowPlayerIII_ColName].Add(yellowPlayerIII);
        //playerData[YellowPlayerIV_Script.yellowPlayerIV_ColName].Add(yellowPlayerIV);

        foreach(String block in playerData.Keys)
        {
            if (block == TagHolder.STAR) continue;
            Vector3 offsetByPixel = Vector3.zero;
            foreach(GameObject player in playerData[block])
            {   if (! player.gameObject.name.ToLower().Contains(colorOfPlayer.ToLower())) continue;
                player.gameObject.transform.position += offsetByPixel;
                offsetByPixel.x += 7f;
            }
        }


    }

    private void OrganiizePlayers()
    {
        //print("Organize player");
        float displacement = 6f;

        foreach (GameObject playerContainer in PlayersContainer)
        {
            List<Transform> positionList = new List<Transform>();
            //print(playerContainer.name);
            foreach (Transform player in playerContainer.gameObject.GetComponentInChildren<Transform>())
            {
                //print(player.gameObject.name);
                positionList.Add(player);
            }
            print(playerContainer.gameObject.name);

            for (int i = 0; i < positionList.Count - 1; i++)
            {


                for (int j = i + 1; j < positionList.Count; j++)
                {
                    //print(i+"  "+positionList[i].position +"\t"+j+"  "+ positionList[j].position + "\t "+ (positionList[i].position == positionList[j].position));
                    //print("(Math.Abs( positionList[i].position.x+2) >= Math.Abs(positionList[j].position.x) &&" + (Math.Abs(positionList[i].position.x + 2) >= Math.Abs(positionList[j].position.x) )+
                    //     " Math.Abs(positionList[j].position.x) >= Math.Abs(positionList[i].position.x - 2))" +( Math.Abs(positionList[j].position.x) >= Math.Abs(positionList[i].position.x - 2))+
                    //   " && (Math.Abs(positionList[i].position.y + 2) >= Math.Abs(positionList[j].position.y) &&" + (Math.Abs(positionList[i].position.y + 2) >= Math.Abs(positionList[j].position.y))+

                    //     "   Math.Abs(positionList[j].position.y) >= Math.Abs(positionList[i].position.y - 2))" + (Math.Abs(positionList[j].position.y) >= Math.Abs(positionList[i].position.y - 2)) );
                    print(i + "\t" + "x : " + Math.Abs(positionList[i].position.x) + "\t" + j + "\t" + "x : " + Math.Abs(positionList[j].position.x) + "\n" +
                          i + "\t" + "y : " + Math.Abs(positionList[i].position.y) + "\t" + j + "\t" + "y : " + Math.Abs(positionList[j].position.y));
                    if ((Math.Abs(positionList[i].position.x + 10) >= Math.Abs(positionList[j].position.x)) &&
                          (Math.Abs(positionList[j].position.x) >= Math.Abs(positionList[i].position.x - 10))
                        && (Math.Abs(positionList[i].position.y + 10) >= Math.Abs(positionList[j].position.y)) &&
                            (Math.Abs(positionList[j].position.y) >= Math.Abs(positionList[i].position.y - 10)))
                    {
                        print(i + " \t " + j);
                        positionList[j].position = positionList[i].position + new Vector3(displacement, 0f, 0f);

                    }
                }
            }
        }

    }

    #region Red Player Movement
    public void RedPlayer1Movement()
    {
        PlayerMovement(TagHolder.RED, redMovementBlocks, ref redPlayerI_Steps, PlayerName.RED_PLAYER_1, ref redPlayerI, ref totalRedInHouse, ref RedPlayerI_Button,
            redPlayerII_Steps + redPlayerIII_Steps + redPlayerIV_Steps);
    }

    public void RedPlayer2Movement()
    {
        PlayerMovement(TagHolder.RED, redMovementBlocks, ref redPlayerII_Steps, PlayerName.RED_PLAYER_2, ref redPlayerII, ref totalRedInHouse, ref RedPlayerII_Button,
            redPlayerI_Steps + redPlayerIII_Steps + redPlayerIV_Steps);
    }
    public void RedPlayer3Movement()
    {
        PlayerMovement(TagHolder.RED, redMovementBlocks, ref redPlayerIII_Steps, PlayerName.RED_PLAYER_3, ref redPlayerIII, ref totalRedInHouse, ref RedPlayerIII_Button,
            redPlayerII_Steps + redPlayerI_Steps + redPlayerIV_Steps);
    }
    public void RedPlayer4Movement()
    {
        PlayerMovement(TagHolder.RED, redMovementBlocks, ref redPlayerIV_Steps, PlayerName.RED_PLAYER_4, ref redPlayerIV, ref totalRedInHouse, ref RedPlayerIV_Button,
            redPlayerII_Steps + redPlayerIII_Steps + redPlayerI_Steps);
    }
    #endregion
    #region Blue Player Movement
    public void BluePlayer1Movement()
    {
        PlayerMovement(TagHolder.BLUE, blueMovementBlocks, ref bluePlayerI_Steps, PlayerName.BLUE_PLAYER_1, ref bluePlayerI,
            ref totalBlueInHouse, ref BluePlayerI_Button,
            bluePlayerII_Steps + bluePlayerIII_Steps + bluePlayerIV_Steps);
    }
    public void BluePlayer2Movement()
    {
        PlayerMovement(TagHolder.BLUE, blueMovementBlocks, ref bluePlayerII_Steps, PlayerName.BLUE_PLAYER_2, ref bluePlayerII,
            ref totalBlueInHouse, ref BluePlayerII_Button,
            bluePlayerI_Steps + bluePlayerIII_Steps + bluePlayerIV_Steps);
    }
    public void BluePlayer3Movement()
    {
        PlayerMovement(TagHolder.BLUE, blueMovementBlocks, ref bluePlayerIII_Steps, PlayerName.BLUE_PLAYER_3, ref bluePlayerIII,
            ref totalBlueInHouse, ref BluePlayerIII_Button,
            bluePlayerII_Steps + bluePlayerI_Steps + bluePlayerIV_Steps);
    }
    public void BluePlayer4Movement()
    {
        PlayerMovement(TagHolder.BLUE, blueMovementBlocks, ref bluePlayerIV_Steps, PlayerName.BLUE_PLAYER_4, ref bluePlayerIV,
            ref totalBlueInHouse, ref BluePlayerIV_Button,
            bluePlayerII_Steps + bluePlayerIII_Steps + bluePlayerI_Steps);
    }
    #endregion
    #region Green Player Movement
    public void GreenPlayer1Movement()
    {
        PlayerMovement(TagHolder.GREEN, greenMovementBlocks, ref greenPlayerI_Steps, PlayerName.GREEN_PLAYER_1, ref greenPlayerI, ref totalGreenInHouse, ref GreenPlayerI_Button,
            greenPlayerII_Steps + greenPlayerIII_Steps + greenPlayerIV_Steps);
    }
    public void GreenPlayer2Movement()
    {
        PlayerMovement(TagHolder.GREEN, greenMovementBlocks, ref greenPlayerII_Steps, PlayerName.GREEN_PLAYER_2, ref greenPlayerII, ref totalGreenInHouse, ref GreenPlayerII_Button,
            greenPlayerI_Steps + greenPlayerIII_Steps + greenPlayerIV_Steps);
    }
    public void GreenPlayer3Movement()
    {
        PlayerMovement(TagHolder.GREEN, greenMovementBlocks, ref greenPlayerIII_Steps, PlayerName.GREEN_PLAYER_3, ref greenPlayerIII, ref totalGreenInHouse, ref GreenPlayerIII_Button,
            greenPlayerII_Steps + greenPlayerI_Steps + greenPlayerIV_Steps);
    }
    public void GreenPlayer4Movement()
    {
        PlayerMovement(TagHolder.GREEN, greenMovementBlocks, ref greenPlayerIV_Steps, PlayerName.GREEN_PLAYER_4, ref greenPlayerIV, ref totalGreenInHouse, ref GreenPlayerIV_Button,
            greenPlayerII_Steps + greenPlayerIII_Steps + greenPlayerI_Steps);
    }
    #endregion
    #region Yellow Player Movement
    public void YellowPlayer1Movement()
    {
        PlayerMovement(TagHolder.YELLOW, yellowMovementBlocks, ref yellowPlayerI_Steps, PlayerName.YELLOW_PLAYER_1, ref yellowPlayerI,
            ref totalYellowInHouse, ref YellowPlayerI_Button,
            yellowPlayerII_Steps + yellowPlayerIII_Steps + yellowPlayerIV_Steps);
    }
    public void YellowPlayer2Movement()
    {
        PlayerMovement(TagHolder.YELLOW, yellowMovementBlocks, ref yellowPlayerII_Steps, PlayerName.YELLOW_PLAYER_2, ref yellowPlayerII,
            ref totalYellowInHouse, ref YellowPlayerII_Button,
            yellowPlayerI_Steps + yellowPlayerIII_Steps + yellowPlayerIV_Steps);
    }
    public void YellowPlayer3Movement()
    {
        PlayerMovement(TagHolder.YELLOW, yellowMovementBlocks, ref yellowPlayerIII_Steps, PlayerName.YELLOW_PLAYER_3, ref yellowPlayerIII,
            ref totalYellowInHouse, ref YellowPlayerIII_Button,
            yellowPlayerII_Steps + yellowPlayerI_Steps + yellowPlayerIV_Steps);
    }
    public void YellowPlayer4Movement()
    {
        PlayerMovement(TagHolder.YELLOW, yellowMovementBlocks, ref yellowPlayerIV_Steps, PlayerName.YELLOW_PLAYER_4, ref yellowPlayerIV,
            ref totalYellowInHouse, ref YellowPlayerIV_Button,
            yellowPlayerII_Steps + yellowPlayerIII_Steps + yellowPlayerI_Steps);
    }
    #endregion


    #region player movement commented


    //public void RedPlayer1Mmovement()
    //{
    //    SoundManagerScript.playerAudioSource.Play();
    //    SetBorder(TagHolder.RED);
    //    SetPlayersAnimation(TagHolder.RED);
    //    SetButton(TagHolder.RED);
    //    // 24          -      [0-24]                 
    //    if (playerTurn == "RED" && (redMovementBlocks.Count - redPlayerI_Steps) > selectDiceNumAnimation)//dice outcome is small than remaining steps to take
    //    {

    //        if (redPlayerI_Steps > 0)
    //        {

    //            Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

    //            for (int i = 0; i < redPlayer_Path.Length; i++)
    //            {
    //                redPlayer_Path[i] = redMovementBlocks[redPlayerI_Steps + i].transform.position;
    //            }

    //            redPlayerI_Steps += selectDiceNumAnimation;

    //            if (selectDiceNumAnimation == 8)
    //            {
    //                playerTurn = "RED";
    //            }
    //            else
    //            {
    //                switch (MainMenuScript.howManyPlayers)
    //                {
    //                    case 2:
    //                        playerTurn = TagHolder.GREEN;
    //                        break;

    //                    case 3:
    //                        playerTurn =  TagHolder.BLUE;
    //                        break;

    //                    case 4:
    //                        playerTurn =  TagHolder.BLUE;
    //                        break;
    //                }
    //            }

    //            curruntPlayerName = PlayerName.RED_PLAYER_1;

    //            //if(redPlayerI_Steps + selectDiceNumAnimation == redMovementBlocks.Count)
    //            if (redPlayer_Path.Length > 1)
    //            {
    //                //redPlayerI.transform.DOPath (redPlayer_Path, 2.0f, PathType.Linear, PathMode.Full3D, 10, Color.red);
    //                iTween.MoveTo(redPlayerI, iTween.Hash("path", redPlayer_Path, "speed", 175, "time", 1.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
    //            }
    //            else
    //            {
    //                iTween.MoveTo(redPlayerI, iTween.Hash("position", redPlayer_Path[0], "speed", 175, "time", 1.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
    //            }
    //        }
    //        else
    //        {
    //            if (selectDiceNumAnimation == 8 && redPlayerI_Steps == 0)
    //            {
    //                Vector3[] redPlayer_Path = new Vector3[1];
    //                redPlayer_Path[0] = redMovementBlocks[redPlayerI_Steps].transform.position;
    //                redPlayerI_Steps += 1;
    //                playerTurn = "RED";
    //                //curruntPlayer = RedPlayerI_Script.redPlayerI_ColName;
    //                curruntPlayerName = PlayerName.RED_PLAYER_1;

    //                iTween.MoveTo(redPlayerI, iTween.Hash("position", redPlayer_Path[0], "speed", 125, "time", 1.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
    //            }
    //        }

    //    }
    //    else
    //    {
    //        // Condition when Player Coin is reached successfully in House....(Actual Number of required moves to get into the House)
    //        if (playerTurn == "RED" && (redMovementBlocks.Count - redPlayerI_Steps) == selectDiceNumAnimation)
    //        {
    //            Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

    //            for (int i = 0; i < selectDiceNumAnimation; i++)
    //            {
    //                redPlayer_Path[i] = redMovementBlocks[redPlayerI_Steps + i].transform.position;
    //            }

    //            redPlayerI_Steps += selectDiceNumAnimation;

    //            playerTurn = TagHolder.RED;

    //            //redPlayerI_Steps = 0;

    //            if (redPlayer_Path.Length > 1)
    //            {
    //                iTween.MoveTo(redPlayerI, iTween.Hash("path", redPlayer_Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
    //            }
    //            else
    //            {
    //                iTween.MoveTo(redPlayerI, iTween.Hash("position", redPlayer_Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
    //            }
    //            totalRedInHouse += 1;
    //            Debug.Log("Cool !!");
    //            RedPlayerI_Button.enabled = false;
    //        }
    //        else
    //        {
    //            Debug.Log("You need " + (redMovementBlocks.Count - redPlayerI_Steps).ToString() + " to enter into the house.");

    //            if (redPlayerII_Steps + redPlayerIII_Steps + redPlayerIV_Steps == 0 && selectDiceNumAnimation != 6)
    //            {
    //                switch (MainMenuScript.howManyPlayers)
    //                {
    //                    case 2:
    //                        playerTurn = TagHolder.GREEN;
    //                        break;

    //                    case 3:
    //                        playerTurn =  TagHolder.BLUE;
    //                        break;

    //                    case 4:
    //                        playerTurn =  TagHolder.BLUE;
    //                        break;
    //                }
    //                InitializeDice();
    //            }
    //        }
    //    }
    //}
    #endregion

    void Start()
    {

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;

        randomNo = new System.Random();

        SetAllAnimationFalse();
        // Players initial positions.....
        redPlayerI_Pos = redPlayerI.transform.position;
        redPlayerII_Pos = redPlayerII.transform.position;
        redPlayerIII_Pos = redPlayerIII.transform.position;
        redPlayerIV_Pos = redPlayerIV.transform.position;

        greenPlayerI_Pos = greenPlayerI.transform.position;
        greenPlayerII_Pos = greenPlayerII.transform.position;
        greenPlayerIII_Pos = greenPlayerIII.transform.position;
        greenPlayerIV_Pos = greenPlayerIV.transform.position;

        bluePlayerI_Pos = bluePlayerI.transform.position;
        bluePlayerII_Pos = bluePlayerII.transform.position;
        bluePlayerIII_Pos = bluePlayerIII.transform.position;
        bluePlayerIV_Pos = bluePlayerIV.transform.position;

        yellowPlayerI_Pos = yellowPlayerI.transform.position;
        yellowPlayerII_Pos = yellowPlayerII.transform.position;
        yellowPlayerIII_Pos = yellowPlayerIII.transform.position;
        yellowPlayerIV_Pos = yellowPlayerIV.transform.position;

        //redScreen.SetActive(false);
        //greenScreen.SetActive(false);
        //yellowScreen.SetActive(false);
        //blueScreen.SetActive(false);
        switch (MainMenuScript.howManyPlayers)
        {
            case 2:
                playerTurn = "RED";

                SetFrame(Red: true);

                //diceRoll.position = redDiceRollPos.position;
                bluePlayerI.SetActive(false);
                bluePlayerII.SetActive(false);
                bluePlayerIII.SetActive(false);
                bluePlayerIV.SetActive(false);

                yellowPlayerI.SetActive(false);
                yellowPlayerII.SetActive(false);
                yellowPlayerIII.SetActive(false);
                yellowPlayerIV.SetActive(false);
                break;

            case 3:
                playerTurn = "RED";

                SetFrame(Red: true);

                diceRoll.position = redDiceRollPos.position;
                greenPlayerI.SetActive(false);
                greenPlayerII.SetActive(false);
                greenPlayerIII.SetActive(false);
                greenPlayerIV.SetActive(false);

                break;

            case 4:
                playerTurn = "RED";

                SetFrame(Red: true);

                diceRoll.position = redDiceRollPos.position;
                // keep all players active
                break;
        }


    }// Start

} // GameManager
