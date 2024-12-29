# About:
This game is my first experience working properly with net code and networking in Unity. It is a 1v1 multiplayer game that people can join over the local internet (they must be on the same Wi-Fi router). 
One player starts the game server by hosting it, and other players (theoretically up to 10) can join and play. The game initially used synchronized network variables to track game states, but due to the bugs, they were replaced by
RPCs (remote procedure calls). The players' physics, snowballs, and gliders are synced across the network in real-time.

# How to play
The game occurs in a remote holiday village, where you spawn in a random radius from the center. When you spawn in, you are around 100m above the map, and you have a "glider" equipped, 
which you can use to move freely through the scene and find a safe landing spot. By left-clicking you can shoot a snowball, which does damage to other players, and players die when their health reaches 0, which updates the games' score on the HUD.
There is also an additional feature of dashing which you can activate by pressing E, dashing briefly in the direction of your velocity. The movement is modeled after CS:GO, and the dash feels like a Valorant Jett dash, making the movement 
very fun and engaging.


## How to run Multiplayer (1v1) on Local Network (LAN)
1. Download the zip file in the releases from GitHub
2. Extract the contents into a folder
3. Open Windows Defender Settings - Make sure the firewall is OFF (Disabled) for both computers
4. Run the game "WinterWarzone.exe" file on both computers
5. On one computer, click "Start Host", and note the IP Address listed on the top left
6. On a separate computer, type the IP Address of the Host PC, and click "Join Game"

## How to test run Multiplayer on your computer (no LAN)
1. Download the zip file in the releases from GitHub
2. Extract the contents into a folder
4. Run the game "WinterWarzone.exe" file twice, opening 2 windows
5. On one window, click "Start Host", and note the IP Address listed on the top left
6. On a separate window, type the IP Address of the Host Window, and click "Join Game"
