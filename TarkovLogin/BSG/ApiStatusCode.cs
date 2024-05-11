namespace ConsoleApp1.BSG;

public enum ApiStatusCode
{
    Ok = 0,
    ErrorBase = 200,
    ErrorClientNotAuthorized = 201,
    ErrorPassRestoreWrongEmail = 202,
    ErrorPassRestoreFailToSend = 203,
    ErrorWrongBackendVersion = 204,
    ErrorBadAccount = 205,
    ErrorWrongEmailOrPass = 206,
    ErrorWrongParameters = 207,
    ErrorBadUserRegion = 208,
    ErrorReceivedNewHardware = 209,
    ErrorGameVersionNotFound = 210,
    ErrorWrongActivationCode = 211,
    ErrorProfileIsBanned = 229,
    ErrorMaxLoginCount = 230,
    ErrorWrongTaxonomyVersion = 231,
    ErrorWrongMajorVersion = 232,
    ErrorNoAccessToServer = 233,
    ErrorPhoneNumberActivationRequired = 240,
    ErrorPhoneValidationCodeRequired = 243,
    ErrorAuthLockedTemp = 248,
    ErrorAuthLockedPermanent = 249,
    ErrorMaintenance = 260,
    VersionObsolete = 301,
    Unauthorized = 401001
}