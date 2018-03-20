using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;

public class GameLobbyHook : LobbyHook {

	private void Awake() {
		gameObject.SetActive(true);
	}

	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer) {
		LobbyPlayer lPlayer = lobbyPlayer.GetComponent<LobbyPlayer>();
		PlayerControl gPlayer = gamePlayer.GetComponent<PlayerControl>();

		gPlayer.playerTeam = lPlayer.team;
		gPlayer.playerName = lPlayer.playerName;
		gPlayer.playerColor = lPlayer.playerColor;

	}

}
