# Near WebGL API for Unity
Example scenes of how to do Near JavaScript API calls and Near RPC calls using the included but currently limited Near_API class.

<p>&nbsp;</p>

## Unity Project 

	Ø Unity version: 2021.3.21f1
	Ø Make sure you install 2 Unity Editor modules - WebGL Build Support & Windows Build Support (IL2CPP)
	Ø Build platform: WebGL
	Ø Newtonsoft.JSON
	Ø New Input sytem
	Ø Render Pipeline: URP

<p>&nbsp;</p>

## Installation


	1) Register a Near wallet on Testnet and/or Mainnet
	2) Fork this repo to your local machine
	3) Open local repo folder from Unity Hub
	4) Unity will report that their are compile errors, click Ignore
	5) File / Build Settings - Set platform to WebGL
	6) Edit / Project Settings / Player / Resolution and Presentation - Select Near WebGL template
	7) File / Build And Run - set your own build directory
	8) When the WebGL application opens in your browser you will see a screen with the login button.
	9) Select the relevant Near network testnet/mainnet from the dropdown and click the Login button
	10) Sign in using your relevant Near wallet.
	11) Use the interface to test various Near API calls, including calling a contract method and passing an argument. 


<p>&nbsp;</p>


## Classes

### Near_API class 
Class with a Near namespace that contains static methods that mainly calls JavaScript funtions in the JSLIB file (Plugin). Has static variables that stores the user account ID and login status.

<p>&nbsp;</p>

### WalletAuthenticate MonoBehavior Class
Used by the WalletLogin scene to calls the Near_API methods.

<p>&nbsp;</p>

### Near_RPC MonoBehavior Class
Example of posting json to the Near RPC API and returning a user's account details. Uses 2 other classes to handle the JSON fields.

	1) Post_ViewAccount class - JSON post fields
	2) ViewAccount class - Returned JSON fields
 
<p>&nbsp;</p>

## Scenes

### WalletLogin scene

Default scene with the following functions.

	1) Login
	2) Logout
	3) Check login status
	4) Get account ID
	5) Get account balance
	6) Navigate to the RPC scene
	7) Call a method on a contract passing in a JSON argument (Examples of retrieving a Mintbase NFT and the MFG NFT)



### RPC scene

Displays the user account details called from the RPC API.

<p>&nbsp;</p>

## Other Resources

### NEAR
> Near JavaScript API documentation - https://docs.near.org/tools/near-api-js/quick-reference

> Near testnet wallet - https://wallet.testnet.near.org/

> Near testnet explorer - https://explorer.testnet.near.org/

> Near Fungible Tokens docs - https://docs.near.org/tutorials/fts/simple-fts

> Near GitHub - https://github.com/orgs/near/repositories?type=all

> Near Client for Unity (Android / 3 year old repo) - https://github.com/near/near-api-unity

<p>&nbsp;</p>

### Morgan Page of Rogues
> Near API for Unity - GitHub https://github.com/morganpage/near-api-unity

> Adding blobkchain toUnity (video tutorial) https://youtu.be/vssV5ALChUM

> Near / Unity API Plugin (video tutorial) https://youtu.be/02_dk_gGePk

<p>&nbsp;</p>

### Mintbase
> Create NFTs on the Near testnet using a visual interface - https://testnet.mintbase.xyz/
