namespace Gateway.API.Interfaces
{
    public interface ICartAggregatesService
    {
        Task<(bool IsSuccess, dynamic FullCartDetails)> GetFullCartDetails(string customerId);
    }
}
