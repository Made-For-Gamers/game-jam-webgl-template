mergeInto(LibraryManager.library, {

    //Request to sign into Near wallet
    Login: async function (contractId, networkId) {
        const nearConnection = await connect(connectionConfig(UTF8ToString(networkId)));
        const walletConnection = new WalletConnection(nearConnection);
        walletConnection.requestSignIn(UTF8ToString(contractId));
    },

    //Logout of Near wallet
    Logout: async function (networkId) {
        const nearConnection = await connect(connectionConfig(UTF8ToString(networkId)));
        const walletConnection = new WalletConnection(nearConnection);
        walletConnection.signOut();      
    },

    //Login status check
    LoginStatus: async function (networkId) {
        const nearConnection = await connect(connectionConfig(UTF8ToString(networkId)));
        const walletConnection = new WalletConnection(nearConnection);
        var status = walletConnection.isSignedIn();
        console.log("Login Status: ", status);
        SendMessage('Scripts', 'ChangeText', status ? 'true' : 'false');
    },
});

