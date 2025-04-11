exports.onExecutePostLogin = async (event, api) => {
    const token = api.accessToken;
    token.setCustomClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", event.user.email);
    token.setCustomClaim("custom_name", event.user.email);
};