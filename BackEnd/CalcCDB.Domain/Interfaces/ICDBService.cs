using CalcCDB.Domain.Entities;

namespace CalcCDB.Domain.Interfaces
{
    public interface ICDBService
    {
        Task<InvestingResult> CalCdb(decimal value, int months);
    }
}
