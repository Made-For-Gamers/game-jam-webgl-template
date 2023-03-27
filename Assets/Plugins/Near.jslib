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
        const status = walletConnection.isSignedIn();
        SendMessage('Scripts', 'ChangeLoginStatus', status ? 'true' : 'false');
    },

    //Login status check
    AccountId: async function (networkId) {
        const nearConnection = await connect(connectionConfig(UTF8ToString(networkId)));
        const walletConnection = new WalletConnection(nearConnection);
        const accountId = walletConnection.getAccountId();
        SendMessage('Scripts', 'ChangeText', accountId);
    },
});

