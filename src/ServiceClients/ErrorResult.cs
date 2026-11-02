namespace HomeBudgetManager.Bff.WebUI.ServiceClients;

public sealed class ErrorResult
{
    public enum ErrorType
    {
        None,

        NotFound,

        Unauthorized,

        Forbidden,

        BadRequest,

        InternalServerError,

        ServiceUnavailable
    }
}
