namespace VnStyle.Services.Business
{
    public interface IAppContextUrlRouting
    {
        string SalonDetail(string hashId, string salonName);
        string Home();
    }
}