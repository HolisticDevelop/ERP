namespace ERP.Services.Inventorying.API.Infrastructure.Services;

public interface IIdentityService
{
    string GetUserIdentity();

    string GetUserName();
}