using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using UnityEngine.Purchasing;
//Contains platform specific methods

public class TrianglesMenu : MonoBehaviour, IStoreListener
{

    public Texture2D Player;
    public Texture2D Icon_Player;
    public Texture2D Rookie;
    public Texture2D UpAndComer;
    public Texture2D Master;
    public Texture2D Veteran;
    public Texture2D GodLike;
    public Texture2D Donut;
    public Texture2D Illusion;
    public Texture2D Illuminati;
    public Texture2D CautionSign;
    public Texture2D Pizza;

    public Texture2D BlackIcon;
    public Texture2D BlackDonut;
    public Texture2D BlackIllusion;
    public Texture2D BlackIlluminati;
    public Texture2D BlackCautionSign;

    public Texture2D Coin1;
    public Texture2D Coin2;
    public Texture2D Coin3;

    public bool availableIconPlayer;

    public string activeTriangle;

    bool buttons = true;
    bool buyTriangle = false;
    bool equipTriangle = false;
    bool buyCoins = false;
    bool bottomButtons = true;

    string equipingTriangle;
    string purchasingTriangle;
    string purchaseIdentifier;
    public int currentTrianglePrice;

    public int selectedCoinPurchase = 0;

    public Font MagnetoB;

    bool achievementTriangleClick = false;

    //In App Purchase Stuff
    private IStoreController controller;
    private IExtensionProvider extensions;


    void Start()
    {
        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
        builder.AddProduct("small_coins_pack", ProductType.Consumable);
        builder.AddProduct("medium_coins_pack", ProductType.Consumable);
        builder.AddProduct("large_coins_pack", ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);

    }

    // Called when Unity IAP is ready to make purchases.
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }


    /// Called when Unity IAP encounters an unrecoverable initialization error.
    /// Note that this will not be called if Internet is unavailable; Unity IAP
    /// will attempt initialization until it becomes available.
    public void OnInitializeFailed(InitializationFailureReason error)
    {

    }

    /// Called when a purchase completes.
    /// May be called at any time after OnInitialized().
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        Debug.Log("Purchase Successful");

        switch (selectedCoinPurchase)
        {
            case 1:
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1000);
                break;
            case 2:
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 3500);
                break;
            case 3:
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 6000);
                break;
        }


        return PurchaseProcessingResult.Complete;
    }


    /// Called when a purchase fails.
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {

    }









    void OnGUI()
    {
        GUI.skin.label.alignment = TextAnchor.UpperLeft;

        GUIStyle buttonFont = new GUIStyle("Button");
        buttonFont.fontSize = Screen.width / 19;

        GUIStyle labelBuyFont = new GUIStyle("Label");
        labelBuyFont.fontSize = Screen.width / 6;

        GUIStyle ComingSoonFont = new GUIStyle("Label");
        ComingSoonFont.fontSize = Screen.width / 10;

        GUI.skin.font = MagnetoB;

        /*
		if (buyCoins == true) { //Handles the buying of coins -- Default old version
			buttons = false;
			bottomButtons = false;
			buyTriangle = false;
			achievementTriangleClick = false;
			equipTriangle = false;
			GUI.Label(new Rect(Screen.width / 2 - Screen.width / 5, Screen.height / 2 - Screen.height / 6, Screen.width / 2, Screen.height / 4), "Coming Soon", ComingSoonFont);
		}
        */


        if (buyCoins == true)
        { //Purchasing of Coins with real money
            bottomButtons = false;
            buyTriangle = false;
            achievementTriangleClick = false;
            equipTriangle = false;

            if (GUI.Button(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 12, Screen.height / 2 - Screen.height / 3.8f, Screen.width / 3, Screen.height / 6), Coin1))
            {
                selectedCoinPurchase = 1;
                Analytics.Transaction("small_coins_pack", 0.99m, "USD", null, null);
            }
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 - Screen.height / 4.2f, Screen.width, Screen.height / 4), "$1", labelBuyFont);


            if (GUI.Button(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 12, Screen.height / 2 - Screen.height / 14, Screen.width / 3, Screen.height / 6), Coin2))
            {
                selectedCoinPurchase = 2;
                Analytics.Transaction("medium_coins_pack", 2.99m, "USD", null, null);
            }
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 - Screen.height / 22, Screen.width, Screen.height / 4), "$3", labelBuyFont);


            if (GUI.Button(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 12, Screen.height / 2 + Screen.height / 8, Screen.width / 3, Screen.height / 6), Coin3))
            {
                selectedCoinPurchase = 3;
                Analytics.Transaction("large_coins_pack", 4.99m, "USD", null, null);
            }
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 + Screen.height / 6.3f, Screen.width, Screen.height / 4), "$5", labelBuyFont);
        }




        if (buttons == true)
        {

            //Default Triangle Button
            if (GUI.Button(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 20, (Screen.height / 2 - Screen.height / 2) + Screen.height / 8, Screen.width / 4, Screen.height / 8), Player))
            {
                equipTriangle = true;
                equipingTriangle = "DefaultPlayer";
                purchasingTriangle = "Default";
            }


            //Cascade Icon Triangle
            if (PlayerPrefs.GetInt("IconPurchased") == 1)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 7.6f, (Screen.height / 2 - Screen.height / 2) + Screen.height / 8, Screen.width / 4, Screen.height / 8), Icon_Player))
                {
                    equipTriangle = true;
                    equipingTriangle = "Cascade";
                    purchasingTriangle = "Squared";
                }
            }
            if (PlayerPrefs.GetInt("IconPurchased") == 0)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 7.6f, (Screen.height / 2 - Screen.height / 2) + Screen.height / 8, Screen.width / 4, Screen.height / 8), BlackIcon))
                {
                    buyTriangle = true;
                    purchaseIdentifier = "IconPurchased";
                    purchasingTriangle = "Squared";
                    currentTrianglePrice = 25;
                }
            }


            //Rookie Triangle
            if (PlayerPrefs.GetInt("highScore") >= 50)
            {
                if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 5, (Screen.height / 2 - Screen.height / 2) + Screen.height / 8, Screen.width / 4, Screen.height / 8), Rookie))
                {
                    equipTriangle = true;
                    equipingTriangle = "Rookie";
                    purchasingTriangle = "Rookie";
                }
            }
            if (PlayerPrefs.GetInt("highScore") < 50)
            {
                if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 5, (Screen.height / 2 - Screen.height / 2) + Screen.height / 8, Screen.width / 4, Screen.height / 8), BlackIcon))
                {
                    achievementTriangleClick = true;
                    purchasingTriangle = "Rookie";
                    currentTrianglePrice = 50;
                }
            }





            //Up And Comer Triangle
            if (PlayerPrefs.GetInt("highScore") >= 200)
            {
                if (GUI.Button(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 20, Screen.height / 2 - Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), UpAndComer))
                {
                    equipTriangle = true;
                    equipingTriangle = "UpAndComer";
                    purchasingTriangle = "Up And Comer";
                }
            }
            if (PlayerPrefs.GetInt("highScore") < 200)
            {
                if (GUI.Button(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 20, Screen.height / 2 - Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), BlackIcon))
                {
                    achievementTriangleClick = true;
                    purchasingTriangle = "Up And Comer";
                    currentTrianglePrice = 200;
                }
            }


            //Master Triangle
            if (PlayerPrefs.GetInt("highScore") >= 300)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 7.6f, Screen.height / 2 - Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), Master))
                {
                    equipTriangle = true;
                    equipingTriangle = "Master";
                    purchasingTriangle = "Master";
                }
            }
            if (PlayerPrefs.GetInt("highScore") < 300)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 7.6f, Screen.height / 2 - Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), BlackIcon))
                {
                    achievementTriangleClick = true;
                    purchasingTriangle = "Master";
                    currentTrianglePrice = 300;
                }
            }


            //Veteran Triangle
            if (PlayerPrefs.GetInt("highScore") >= 500)
            {
                if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2 - Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), Veteran))
                {
                    equipTriangle = true;
                    equipingTriangle = "Veteran";
                    purchasingTriangle = "Veteran";
                }
            }
            if (PlayerPrefs.GetInt("highScore") < 500)
            {
                if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2 - Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), BlackIcon))
                {
                    achievementTriangleClick = true;
                    purchasingTriangle = "Veteran";
                    currentTrianglePrice = 500;
                }
            }





            //GodLike Triangle
            if (PlayerPrefs.GetInt("highScore") >= 1000)
            {
                if (GUI.Button(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 20, Screen.height / 2, Screen.width / 4, Screen.height / 8), GodLike))
                {
                    equipTriangle = true;
                    equipingTriangle = "GodLike";
                    purchasingTriangle = "God-Like";
                }
            }
            if (PlayerPrefs.GetInt("highScore") < 1000)
            {
                if (GUI.Button(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 20, Screen.height / 2, Screen.width / 4, Screen.height / 8), BlackIcon))
                {
                    achievementTriangleClick = true;
                    purchasingTriangle = "God-Like";
                    currentTrianglePrice = 1000;
                }
            }


            //Donut Triangle
            if (PlayerPrefs.GetInt("DonutPurchased") == 1)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 7.6f, Screen.height / 2, Screen.width / 4, Screen.height / 8), Donut))
                {
                    equipTriangle = true;
                    equipingTriangle = "Donut";
                    purchasingTriangle = "Donut";
                }
            }
            if (PlayerPrefs.GetInt("DonutPurchased") == 0)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 7.6f, Screen.height / 2, Screen.width / 4, Screen.height / 8), BlackDonut))
                {
                    buyTriangle = true;
                    purchaseIdentifier = "DonutPurchased";
                    purchasingTriangle = "Donut";
                    currentTrianglePrice = 750;
                }
            }


            //Caution Sign Triangle
            if (PlayerPrefs.GetInt("CautionSignPurchased") == 1)
            {
                if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2, Screen.width / 4, Screen.height / 8), CautionSign))
                {
                    equipTriangle = true;
                    equipingTriangle = "CautionSign";
                    purchasingTriangle = "Caution Sign";
                }
            }
            if (PlayerPrefs.GetInt("CautionSignPurchased") == 0)
            {
                if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2, Screen.width / 4, Screen.height / 8), BlackCautionSign))
                {
                    buyTriangle = true;
                    purchaseIdentifier = "CautionSignPurchased";
                    purchasingTriangle = "Road Sign";
                    currentTrianglePrice = 1500;
                }
            }





            //Illuminati Triangle
            if (PlayerPrefs.GetInt("IlluminatiPurchased") == 1)
            {
                if (GUI.Button(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 20, Screen.height / 2 + Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), Illuminati))
                {
                    equipTriangle = true;
                    equipingTriangle = "Illuminati";
                    purchasingTriangle = "Illuminati";
                }
            }
            if (PlayerPrefs.GetInt("IlluminatiPurchased") == 0)
            {
                if (GUI.Button(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 20, Screen.height / 2 + Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), BlackIlluminati))
                {
                    buyTriangle = true;
                    purchaseIdentifier = "IlluminatiPurchased";
                    purchasingTriangle = "Illuminati";
                    currentTrianglePrice = 2000;
                }
            }


            //Illusion Triangle
            if (PlayerPrefs.GetInt("IllusionPurchased") == 1)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 7.6f, Screen.height / 2 + Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), Illusion))
                {
                    equipTriangle = true;
                    equipingTriangle = "Illusion";
                    purchasingTriangle = "Illusion";
                }
            }
            if (PlayerPrefs.GetInt("IllusionPurchased") == 0)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 7.6f, Screen.height / 2 + Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), BlackIllusion))
                {
                    buyTriangle = true;
                    purchaseIdentifier = "IllusionPurchased";
                    purchasingTriangle = "Illusion";
                    currentTrianglePrice = 3000;
                }
            }


            //Pizza Triangle
            if (PlayerPrefs.GetInt("PizzaPurchased") == 1)
            {
                if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2 + Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), Pizza))
                {
                    equipTriangle = true;
                    equipingTriangle = "Pizza";
                    purchasingTriangle = "Pizza";
                }
            }
            if (PlayerPrefs.GetInt("PizzaPurchased") == 0)
            {
                if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 5, Screen.height / 2 + Screen.height / 5.5f, Screen.width / 4, Screen.height / 8), BlackIlluminati))
                {
                    buyTriangle = true;
                    purchaseIdentifier = "PizzaPurchased";
                    purchasingTriangle = "Pizza";
                    currentTrianglePrice = 4000;
                }
            }
        }





        GUIStyle buyFont = new GUIStyle("Label");
        buyFont.fontSize = Screen.width / 19;

        GUIStyle coinsFont = new GUIStyle("Label");
        coinsFont.fontSize = Screen.width / 9;

        //Handles the Purchasing of Triangles
        GUI.color = Color.black;
        if (buyTriangle == true)
        {
            buttons = false;
            GUI.Label(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 12, Screen.height / 2 - Screen.height / 6, Screen.width / 1.2f, Screen.height / 5), "Do you want to buy the " + purchasingTriangle + " triangle for " + currentTrianglePrice.ToString() + " coins?", buyFont);

            if (PlayerPrefs.GetInt("Coins") < currentTrianglePrice)
            {
                GUI.enabled = false;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 3.2f, Screen.height / 2 + Screen.height / 14, Screen.width / 4, Screen.height / 8), "Buy", buttonFont))
            {
                int coinsHolder = PlayerPrefs.GetInt("Coins") - currentTrianglePrice;
                PlayerPrefs.SetInt("Coins", coinsHolder);
                PlayerPrefs.SetString("ActivePlayer", purchasingTriangle);
                PlayerPrefs.SetInt(purchaseIdentifier, 1);
                buyTriangle = false;
                buttons = true;
            }

            GUI.enabled = true;
            if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 20, Screen.height / 2 + Screen.height / 14, Screen.width / 4, Screen.height / 8), "Cancel", buttonFont))
            {
                buyTriangle = false;
                buttons = true;
            }
        }



        //Handles a click on an achievement Triangle
        GUI.color = Color.black;
        if (achievementTriangleClick == true)
        {
            buttons = false;
            GUI.Label(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 12, Screen.height / 2 - Screen.height / 6, Screen.width / 1.2f, Screen.height / 5), "You need a highscore of more than or equal to " + currentTrianglePrice.ToString() + " points to unlock the " + purchasingTriangle + " Triangle", buyFont);


            GUI.enabled = true;
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 8, Screen.height / 2 + Screen.height / 14, Screen.width / 4, Screen.height / 8), "Cancel", buttonFont))
            {
                achievementTriangleClick = false;
                buttons = true;
            }
        }



        //Handles the equiping of Triangles
        GUI.color = Color.black;
        if (equipTriangle == true)
        {
            buttons = false;
            GUI.Label(new Rect((Screen.width / 2 - Screen.width / 2) + Screen.width / 12, Screen.height / 2 - Screen.height / 6, Screen.width / 1.2f, Screen.height / 5), "Do you want to equip the " + purchasingTriangle + " Triangle?", buyFont);

            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 3.2f, Screen.height / 2 + Screen.height / 14, Screen.width / 4, Screen.height / 8), "Equip", buttonFont))
            {
                PlayerPrefs.SetString("ActivePlayer", equipingTriangle);
                Application.LoadLevel("Menu");
                //equipTriangle = false;
                //buttons = true;
            }

            if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 20, Screen.height / 2 + Screen.height / 14, Screen.width / 4, Screen.height / 8), "Cancel", buttonFont))
            {
                equipTriangle = false;
                buttons = true;
            }
        }

        if (buyCoins == true)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 7, Screen.height / 2 + Screen.height / 3, Screen.width / 3, Screen.height / 8), "Cancel", buttonFont))
            {
                buyCoins = false;
                bottomButtons = true;
                buttons = true;
            }
        }


        //Displays the Amount of coins
        GUI.Label(new Rect(Screen.width / 2 - Screen.width / 3, (Screen.height / 2 - Screen.height / 2) + Screen.height / 25, Screen.width, Screen.height / 4), "Coins: " + PlayerPrefs.GetInt("Coins").ToString(), coinsFont);

        if (bottomButtons == true)
        {
            //Menu Button
            GUI.enabled = true;
            GUI.color = Color.black;
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 2.7f, Screen.height / 2 + Screen.height / 3, Screen.width / 3, Screen.height / 8), "Main \n Menu", buttonFont))
            {
                Application.LoadLevel("Menu");
            }
            //More Coins Button
            if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 20, Screen.height / 2 + Screen.height / 3, Screen.width / 3, Screen.height / 8), "Get More \n Coins", buttonFont))
            {
                buttons = false;
                buyCoins = true;
            }
        }
    }
}
