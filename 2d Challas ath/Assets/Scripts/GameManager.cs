using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private int totalBlueInHouse, totalRedInHouse, totalGreenInHouse, totalYellowInHouse;

    public GameObject frameRed, frameGreen, frameBlue, frameYellow;

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

    private int redPlayerI_Steps, redPlayerII_Steps, redPlayerIII_Steps, redPlayerIV_Steps;
    private int greenPlayerI_Steps, greenPlayerII_Steps, greenPlayerIII_Steps, greenPlayerIV_Steps;
    private int bluePlayerI_Steps, bluePlayerII_Steps, bluePlayerIII_Steps, bluePlayerIV_Steps;
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

    //public void SetPlayersAnimation(string str, bool I = false, bool II = false, bool III = false, bool IV = false)
    //{
    //    Debug.Log("animationPlayers " + str + "  values:" + I + II+ III + IV);
    //    switch (str)
    //    {
    //        case TagHolder.RED:

    //            if (I) animRedPlayer[0].StartAnimation();
    //            else  animRedPlayer[0].StopAnimation();

    //            if (II) animRedPlayer[1].StartAnimation();
    //            else animRedPlayer[1].StopAnimation();
    //            if (III) animRedPlayer[2].StartAnimation();
    //            else animRedPlayer[2].StopAnimation();
    //            if (IV) animRedPlayer[3].StartAnimation();
    //            else animRedPlayer[3].StopAnimation();

    //            break;

    //        case TagHolder.BLUE:
    //            if (I) animBluePlayer[0].StartAnimation();
    //            else animBluePlayer[0].StopAnimation();
    //            if (II) animBluePlayer[1].StartAnimation();
    //            else animBluePlayer[1].StopAnimation();
    //            if (III) animBluePlayer[2].StartAnimation();
    //            else animBluePlayer[2].StopAnimation();
    //            if (IV) animBluePlayer[3].StartAnimation();
    //            else animBluePlayer[3].StopAnimation();
    //            break;

    //        case TagHolder.GREEN:
    //            if (I) animGreenPlayer [0].StartAnimation();
    //            else animGreenPlayer[0].StopAnimation();
    //            if (II) animGreenPlayer[1].StartAnimation();
    //            else animGreenPlayer[1].StopAnimation();
    //            if (III) animGreenPlayer[2].StartAnimation();
    //            else animGreenPlayer[2].StopAnimation();
    //            if (IV) animGreenPlayer[3].StartAnimation();
    //            else animGreenPlayer[3].StopAnimation();
    //            break;

    //        case TagHolder.YELLOW:
    //            if (I) animYellowPlayer [0].StartAnimation();
    //            else animYellowPlayer [0].StopAnimation();
    //            if (II) animYellowPlayer [1].StartAnimation();
    //            else animYellowPlayer [1].StopAnimation();
    //            if (III) animYellowPlayer [2].StartAnimation();
    //            else animYellowPlayer [2].StopAnimation();
    //            if (IV) animYellowPlayer [3].StartAnimation();
    //            else animYellowPlayer [3].StopAnimation();
    //            break;
    //    }
    //}
    public void SetBorder(string str, bool I = false, bool II = false, bool III = false, bool IV = false)
    {
        switch (str)
        {
            case TagHolder.RED:
                RedPlayerI_Button.interactable = I;
                RedPlayerII_Button.interactable = (II);
                RedPlayerIII_Button.interactable = (III);
                RedPlayerIV_Button.interactable = (IV);

                
                break;

            case TagHolder.BLUE:
                BluePlayerI_Button.interactable = (I);
                BluePlayerII_Button.interactable = (II);
                BluePlayerIII_Button.interactable = (III);
                BluePlayerIV_Button.interactable = (IV);
             

                break;

            case TagHolder.GREEN:
                GreenPlayerI_Button.interactable = (I);
                GreenPlayerII_Button.interactable = (II);
                GreenPlayerIII_Button.interactable = (III);
                GreenPlayerIV_Button.interactable = (IV);
                break;

            case TagHolder.YELLOW:
                YellowPlayerI_Button.interactable = (I);
                YellowPlayerII_Button.interactable = (II);
                YellowPlayerIII_Button.interactable = (III);
                YellowPlayerIV_Button.interactable = (IV);
               

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
        frameRed.SetActive(Red);
        frameGreen.SetActive(Green);
        frameBlue.SetActive(Blue);
        frameYellow.SetActive(Yellow);
    }
    private void SetFrameActive(string frameColor)
    {
        switch (frameColor)
        {
            case TagHolder.RED:
                frameRed.SetActive(true);
                frameGreen.SetActive(false);
                frameBlue.SetActive(false);
                frameYellow.SetActive(false);
                break;
            case TagHolder.GREEN:
                frameRed.SetActive(false);
                frameGreen.SetActive(true);
                frameBlue.SetActive(false);
                frameYellow.SetActive(false);
                break;
            case TagHolder.BLUE:
                frameRed.SetActive(false);
                frameGreen.SetActive(false);
                frameBlue.SetActive(true);
                frameYellow.SetActive(false);
                break;
            case TagHolder.YELLOW:
                frameRed.SetActive(false);
                frameGreen.SetActive(false);
                frameBlue.SetActive(false);
                frameYellow.SetActive(true);
                break;
        }
    }

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
                            animRedPlayer[num-1].StartAnimation();
                        }
                        else
                        {
                            redPlayerI_Border.SetActive(false);
                            RedPlayerI_Button.interactable = false;
                            animRedPlayer[num-1].StopAnimation();

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
    }
    #endregion


    
    public void DiceRoll()
    {
        SoundManagerScript.diceAudioSource.Play();
        DiceRollButton.interactable = false;

        selectDiceNumAnimation = randomNo.Next(1, 6);
        if (selectDiceNumAnimation == 5) { selectDiceNumAnimation = 8; }
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
                //========================= PLAYERS BORDER GLOW WHEN OPENING ===========================================

                if (selectDiceNumAnimation == 8 && redPlayerI_Steps == 0)
                {

                    SetPlayer(TagHolder.RED, 1, true);

                }
                if (selectDiceNumAnimation == 8 && redPlayerII_Steps == 0)
                {
                    SetPlayer(TagHolder.RED, 2, true);

                }
                if (selectDiceNumAnimation == 8 && redPlayerIII_Steps == 0)
                {
                    SetPlayer(TagHolder.RED, 3, true);

                }
                if (selectDiceNumAnimation == 8 && redPlayerIV_Steps == 0)
                {
                    SetPlayer(TagHolder.RED, 4, true);

                }
                //====================== PLAYERS DON'T HAVE ANY MOVES ,SWITCH TO NEXT TURN===============================
                if (!redPlayerI_Border.activeInHierarchy && !redPlayerII_Border.activeInHierarchy &&
                   !redPlayerIII_Border.activeInHierarchy && !redPlayerIV_Border.activeInHierarchy)
                {
                 
                    SetButton(TagHolder.RED);

                    switch (MainMenuScript.howManyPlayers)
                    {
                        case 2:
                            playerTurn = "GREEN";
                            InitializeDice();
                            break;

                        case 3:
                            playerTurn = "BLUE";
                            InitializeDice();
                            break;

                        case 4:
                            playerTurn = "BLUE";
                            InitializeDice();
                            break;
                    }
                }
                break;

            case TagHolder.GREEN:

                //==========================Glow Border when dice hit 8 and player inside house==========================================

                if (selectDiceNumAnimation == 8 && greenPlayerI_Steps == 0)
                {


                    SetPlayer(TagHolder.GREEN, 1, true);

                }
                if (selectDiceNumAnimation == 8 && greenPlayerII_Steps == 0)
                {
                    SetPlayer(TagHolder.GREEN, 2, true);

                }
                if (selectDiceNumAnimation == 8 && greenPlayerIII_Steps == 0)
                {
                    SetPlayer(TagHolder.GREEN, 3, true);

                }
                if (selectDiceNumAnimation == 8 && greenPlayerIV_Steps == 0)
                {
                    SetPlayer(TagHolder.GREEN, 4, true);

                }


                //====================== PLAYERS DON'T HAVE ANY MOVES ,SWITCH TO NEXT TURN===============================
                if (!greenPlayerI_Border.activeInHierarchy && !greenPlayerII_Border.activeInHierarchy &&
                    !greenPlayerIII_Border.activeInHierarchy && !greenPlayerIV_Border.activeInHierarchy)
                {
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
                            playerTurn = "YELLOW";
                            InitializeDice();
                            break;
                    }
                }
                break;

            case TagHolder.BLUE:

                //==========================Glow Border when dice hit 8 and player inside house==========================================
                if (selectDiceNumAnimation == 8 && bluePlayerI_Steps == 0)
                {
                    SetPlayer(TagHolder.BLUE, 1, true);
                }
                if (selectDiceNumAnimation == 8 && bluePlayerII_Steps == 0)
                {
                    SetPlayer(TagHolder.BLUE, 2, true);

                }
                if (selectDiceNumAnimation == 8 && bluePlayerIII_Steps == 0)
                {
                    SetPlayer(TagHolder.BLUE, 3, true);

                }
                if (selectDiceNumAnimation == 8 && bluePlayerIV_Steps == 0)
                {
                    SetPlayer(TagHolder.BLUE, 4, true);

                }

                //====================== PLAYERS DON'T HAVE ANY MOVES ,SWITCH TO NEXT TURN===============================
                if (!bluePlayerI_Border.activeInHierarchy && !bluePlayerII_Border.activeInHierarchy &&
                    !bluePlayerIII_Border.activeInHierarchy && !bluePlayerIV_Border.activeInHierarchy)
                {
                    SetButton(TagHolder.BLUE);

                    switch (MainMenuScript.howManyPlayers)
                    {
                        case 2:
                            //BLUE PLAYER NOT AVAILABLE
                            break;

                        case 3:
                            playerTurn = "YELLOW";
                            InitializeDice();
                            break;

                        case 4:
                            playerTurn = "GREEN";
                            InitializeDice();
                            break;
                    }
                }
                break;

            case TagHolder.YELLOW:
                //==========================Glow Border when dice hit 8 and player inside house==========================================


                if (selectDiceNumAnimation == 8 && yellowPlayerI_Steps == 0)
                {
                    SetPlayer(TagHolder.YELLOW, 1, true);

                }
                if (selectDiceNumAnimation == 8 && yellowPlayerII_Steps == 0)
                {
                    SetPlayer(TagHolder.YELLOW, 2, true);

                }
                if (selectDiceNumAnimation == 8 && yellowPlayerIII_Steps == 0)
                {
                    SetPlayer(TagHolder.YELLOW, 3, true);

                }
                if (selectDiceNumAnimation == 8 && yellowPlayerIV_Steps == 0)
                {
                    SetPlayer(TagHolder.YELLOW, 4, true);

                }

                //====================== PLAYERS DON'T HAVE ANY MOVES ,SWITCH TO NEXT TURN===============================
                if (!yellowPlayerI_Border.activeInHierarchy && !yellowPlayerII_Border.activeInHierarchy &&
                    !yellowPlayerIII_Border.activeInHierarchy && !yellowPlayerIV_Border.activeInHierarchy)
                {
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

        }
    }//players Not initialized

    private void InitializeDice()
    {

        DiceRollButton.interactable = true;
        SetAllAnimationFalse();

        switch (MainMenuScript.howManyPlayers)
        {
            case 2:
                if (playerTurn == "RED")
                {
                    diceRoll.position = redDiceRollPos.position;

                    SetFrame(Red:true);
                }
                if (playerTurn == "GREEN")
                {
                    diceRoll.position = greenDiceRollPos.position;

                    SetFrame(Green:true);

                }

                SetButton(TagHolder.GREEN);

                SetButton(TagHolder.RED);

                //=============ANIMATED ROUND BORDER=======
                SetBorder(TagHolder.RED);

                SetBorder(TagHolder.GREEN);
                //======================================
                break;

            case 3:
                if (playerTurn == "RED")
                {
                    diceRoll.position = redDiceRollPos.position;

                    SetFrame(Red: true);
                }
                if (playerTurn == "YELLOW")
                {
                    diceRoll.position = yellowDiceRollPos.position;

                    SetFrame(Yellow: true);
                }
                if (playerTurn == "BLUE")
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
                if (playerTurn == "RED")
                {
                    diceRoll.position = redDiceRollPos.position;

                    SetFrame(Red: true);
                }
                if (playerTurn == "GREEN")
                {
                    diceRoll.position = greenDiceRollPos.position;

                    SetFrame(Green: true);

                }
                if (playerTurn == "YELLOW")
                {
                    diceRoll.position = yellowDiceRollPos.position;

                    SetFrame(Yellow: true);
                }
                if (playerTurn == "BLUE")
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

    void Start()
    {
        Debug.Log("Game started");
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

                frameRed.SetActive(true);
                frameGreen.SetActive(false);
                frameBlue.SetActive(false);
                frameYellow.SetActive(false);
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

                frameRed.SetActive(true);
                frameGreen.SetActive(false);
                frameBlue.SetActive(false);
                frameYellow.SetActive(false);

                diceRoll.position = redDiceRollPos.position;
                // keep all players active
                break;
        }


    }// Start


    void Update()
    {

    }//Update

} // GameManager
