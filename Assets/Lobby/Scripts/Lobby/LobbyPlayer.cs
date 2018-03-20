using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.NetworkLobby
{
    //Player entry in the lobby. Handle selecting color/setting name & getting ready for the game
    //Any LobbyHook can then grab it and pass those value to the game player prefab (see the Pong Example in the Samples Scenes)
    public class LobbyPlayer : NetworkLobbyPlayer
    {

        static Color[] ColorsRed = new Color[] { Color.red, Color.magenta};
		static Color[] ColorsBlue = new Color[] { Color.blue, Color.cyan  };
		//used on server to avoid assigning the same color to two player
		static List<int> _colorInUseBlue = new List<int>();
		static List<int> _colorInUseRed = new List<int>();

		//WHAT IM ADDING
		[SyncVar]
		public int team;

		public Button redTeam;
		public Button blueTeam;

		public override void OnStartClient() {
			base.OnStartClient();
			redTeam.gameObject.SetActive(false);
			blueTeam.gameObject.SetActive(false);

		}

		public override void OnStartLocalPlayer() {
			base.OnStartLocalPlayer();
			redTeam.gameObject.SetActive(true);
			blueTeam.gameObject.SetActive(true);
			OnClickRed();

			// Add listeners
			redTeam.onClick.RemoveAllListeners();
			redTeam.onClick.AddListener(OnClickRed);

			blueTeam.onClick.RemoveAllListeners();
			blueTeam.onClick.AddListener(OnClickBlue);
		}

		public void OnClickRed() {
			// Clicked red team (team nr 0)
			redTeam.interactable = false;
			blueTeam.interactable = true;
			CmdSelectTeam(0);
			CmdColorChange();

		}

		public void OnClickBlue() {
			// Clicked blueteam (team nr 1)
			redTeam.interactable = true;
			blueTeam.interactable = false;
			CmdSelectTeam(1);
			CmdColorChange();
		}

		[Command]
		public void CmdSelectTeam(int teamIndex) {
			// Set team of player on the server.
			team = teamIndex;
		}

		//^^WHAT IM ADDING


		public Button colorButton;
        public InputField nameInput;
        public Button readyButton;
        public Button waitingPlayerButton;
        public Button removePlayerButton;

        public GameObject localIcone;
        public GameObject remoteIcone;

        //OnMyName function will be invoked on clients when server change the value of playerName
        [SyncVar(hook = "OnMyName")]
        public string playerName = "";
        [SyncVar(hook = "OnMyColor")]
        public Color playerColor = Color.white;

        public Color OddRowColor = new Color(250.0f / 255.0f, 250.0f / 255.0f, 250.0f / 255.0f, 1.0f);
        public Color EvenRowColor = new Color(180.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f, 1.0f);

        static Color JoinColor = new Color(255.0f/255.0f, 0.0f, 101.0f/255.0f,1.0f);
        static Color NotReadyColor = new Color(34.0f / 255.0f, 44 / 255.0f, 55.0f / 255.0f, 1.0f);
        static Color ReadyColor = new Color(0.0f, 204.0f / 255.0f, 204.0f / 255.0f, 1.0f);
        static Color TransparentColor = new Color(0, 0, 0, 0);

        //static Color OddRowColor = new Color(250.0f / 255.0f, 250.0f / 255.0f, 250.0f / 255.0f, 1.0f);
        //static Color EvenRowColor = new Color(180.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f, 1.0f);


        public override void OnClientEnterLobby()
        {
            base.OnClientEnterLobby();

            if (LobbyManager.s_Singleton != null) LobbyManager.s_Singleton.OnPlayersNumberModified(1);

            LobbyPlayerList._instance.AddPlayer(this);
            LobbyPlayerList._instance.DisplayDirectServerWarning(isServer && LobbyManager.s_Singleton.matchMaker == null);

            if (isLocalPlayer)
            {
                SetupLocalPlayer();
            }
            else
            {
                SetupOtherPlayer();
            }

            //setup the player data on UI. The value are SyncVar so the player
            //will be created with the right value currently on server
            OnMyName(playerName);
            OnMyColor(playerColor);
        }

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();

            //if we return from a game, color of text can still be the one for "Ready"
            readyButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;

           SetupLocalPlayer();
        }

        void ChangeReadyButtonColor(Color c)
        {
            ColorBlock b = readyButton.colors;
            b.normalColor = c;
            b.pressedColor = c;
            b.highlightedColor = c;
            b.disabledColor = c;
            readyButton.colors = b;
        }

        void SetupOtherPlayer()
        {
            nameInput.interactable = false;
            removePlayerButton.interactable = NetworkServer.active;

            ChangeReadyButtonColor(NotReadyColor);

            readyButton.transform.GetChild(0).GetComponent<Text>().text = "...";
            readyButton.interactable = false;

            OnClientReady(false);
        }

        void SetupLocalPlayer()
        {
            nameInput.interactable = true;
            remoteIcone.gameObject.SetActive(false);
            localIcone.gameObject.SetActive(true);

            CheckRemoveButton();

            if (playerColor == Color.white)
                CmdColorChange();

            ChangeReadyButtonColor(JoinColor);

            readyButton.transform.GetChild(0).GetComponent<Text>().text = "JOIN";
            readyButton.interactable = true;

            //have to use child count of player prefab already setup as "this.slot" is not set yet
            if (playerName == "")
                CmdNameChanged("Player" + (LobbyPlayerList._instance.playerListContentTransform.childCount-1));

            //we switch from simple name display to name input
            colorButton.interactable = true;
            nameInput.interactable = true;

            nameInput.onEndEdit.RemoveAllListeners();
            nameInput.onEndEdit.AddListener(OnNameChanged);

            colorButton.onClick.RemoveAllListeners();
            colorButton.onClick.AddListener(OnColorClicked);

            readyButton.onClick.RemoveAllListeners();
            readyButton.onClick.AddListener(OnReadyClicked);

            //when OnClientEnterLobby is called, the loval PlayerController is not yet created, so we need to redo that here to disable
            //the add button if we reach maxLocalPlayer. We pass 0, as it was already counted on OnClientEnterLobby
            if (LobbyManager.s_Singleton != null) LobbyManager.s_Singleton.OnPlayersNumberModified(0);
        }

        //This enable/disable the remove button depending on if that is the only local player or not
        public void CheckRemoveButton()
        {
            if (!isLocalPlayer)
                return;

            int localPlayerCount = 0;
            foreach (PlayerController p in ClientScene.localPlayers)
                localPlayerCount += (p == null || p.playerControllerId == -1) ? 0 : 1;

            removePlayerButton.interactable = localPlayerCount > 1;
        }

        public override void OnClientReady(bool readyState)
        {
            if (readyState)
            {
                ChangeReadyButtonColor(TransparentColor);

                Text textComponent = readyButton.transform.GetChild(0).GetComponent<Text>();
                textComponent.text = "READY";
                textComponent.color = ReadyColor;
                readyButton.interactable = false;
                colorButton.interactable = false;
                nameInput.interactable = false;
            }
            else
            {
                ChangeReadyButtonColor(isLocalPlayer ? JoinColor : NotReadyColor);

                Text textComponent = readyButton.transform.GetChild(0).GetComponent<Text>();
                textComponent.text = isLocalPlayer ? "JOIN" : "...";
                textComponent.color = Color.white;
                readyButton.interactable = isLocalPlayer;
                colorButton.interactable = isLocalPlayer;
                nameInput.interactable = isLocalPlayer;
            }
        }

        public void OnPlayerListChanged(int idx)
        { 
            GetComponent<Image>().color = (idx % 2 == 0) ? EvenRowColor : OddRowColor;
        }

        ///===== callback from sync var

        public void OnMyName(string newName)
        {
            playerName = newName;
            nameInput.text = playerName;
        }

        public void OnMyColor(Color newColor)
        {
            playerColor = newColor;
            colorButton.GetComponent<Image>().color = newColor;
        }

        //===== UI Handler

        //Note that those handler use Command function, as we need to change the value on the server not locally
        //so that all client get the new value throught syncvar
        public void OnColorClicked()
        {
            CmdColorChange();
        }

        public void OnReadyClicked()
        {
            SendReadyToBeginMessage();
        }

        public void OnNameChanged(string str)
        {
            CmdNameChanged(str);
        }

        public void OnRemovePlayerClick()
        {
            if (isLocalPlayer)
            {
                RemovePlayer();
            }
            else if (isServer)
                LobbyManager.s_Singleton.KickPlayer(connectionToClient);
                
        }

        public void ToggleJoinButton(bool enabled)
        {
            readyButton.gameObject.SetActive(enabled);
            waitingPlayerButton.gameObject.SetActive(!enabled);
        }

        [ClientRpc]
        public void RpcUpdateCountdown(int countdown)
        {
            LobbyManager.s_Singleton.countdownPanel.UIText.text = "Match Starting in " + countdown;
            LobbyManager.s_Singleton.countdownPanel.gameObject.SetActive(countdown != 0);
        }

        [ClientRpc]
        public void RpcUpdateRemoveButton()
        {
            CheckRemoveButton();
        }

        //====== Server Command

        [Command]
        public void CmdColorChange()
        {

			if (team == 1) {
				/*
				int idx = System.Array.IndexOf(ColorsBlue, playerColor);
				
				int inUseIdx = _colorInUseBlue.IndexOf(idx);

				if (idx < 0) idx = 0;

				idx = (idx + 1) % ColorsBlue.Length;

				bool alreadyInUse = false;

				do {
					alreadyInUse = false;
					for (int i = 0; i < _colorInUseBlue.Count; ++i) {
						if (_colorInUseBlue[i] == idx) {//that color is already in use
							alreadyInUse = true;
							idx = (idx + 1) % ColorsBlue.Length;
						}
					}
				}
				while (alreadyInUse);

				if (inUseIdx >= 0) {//if we already add an entry in the colorTabs, we change it
					_colorInUseBlue[inUseIdx] = idx;
				} else {//else we add it
					_colorInUseBlue.Add(idx);
				}
				*/
				
				playerColor = ColorsBlue[0];
			}else if(team == 0) {
				/*
				int idx = System.Array.IndexOf(ColorsRed, playerColor);

				int inUseIdx = _colorInUseRed.IndexOf(idx);

				if (idx < 0) idx = 0;

				idx = (idx + 1) % ColorsRed.Length;

				bool alreadyInUse = false;

				do {
					alreadyInUse = false;
					for (int i = 0; i < _colorInUseRed.Count; ++i) {
						if (_colorInUseRed[i] == idx) {//that color is already in use
							alreadyInUse = true;
							idx = (idx + 1) % ColorsRed.Length;
						}
					}
				}
				while (alreadyInUse);

				if (inUseIdx >= 0) {//if we already add an entry in the colorTabs, we change it
					_colorInUseRed[inUseIdx] = idx;
				} else {//else we add it
					_colorInUseRed.Add(idx);
				}
				*/
				playerColor = ColorsRed[0];
			} else {
				Debug.Log("team doesnt equal 0 or 1");
			}
        }

        [Command]
        public void CmdNameChanged(string name)
        {
            playerName = name;
        }

        //Cleanup thing when get destroy (which happen when client kick or disconnect)
        public void OnDestroy()
        {
            LobbyPlayerList._instance.RemovePlayer(this);
            if (LobbyManager.s_Singleton != null) LobbyManager.s_Singleton.OnPlayersNumberModified(-1);
			if(team == 1) {
				int idx = System.Array.IndexOf(ColorsBlue, playerColor);

				if (idx < 0)
					return;

				for (int i = 0; i < _colorInUseBlue.Count; ++i) {
					if (_colorInUseBlue[i] == idx) {//that color is already in use
						_colorInUseBlue.RemoveAt(i);
						break;
					}
				}
			} else if(team == 0) {
				int idx = System.Array.IndexOf(ColorsRed, playerColor);

				if (idx < 0)
					return;

				for (int i = 0; i < _colorInUseRed.Count; ++i) {
					if (_colorInUseRed[i] == idx) {//that color is already in use
						_colorInUseRed.RemoveAt(i);
						break;
					}
				}
			} else {
				Debug.Log("On Destroy team doesn = 0 or 1");
			}
            
        }
    }
}
