using CalcCDB.Domain.Entities;
using CalcCDB.Domain.Interfaces;

namespace CalcCDB.Domain.Services
{
    public class CDBService : ICDBService
    {
        public Task<InvestingResult> CalCdb(decimal investedvalue, int months)
        {
			try
			{
                const decimal Tb = 1.08m;
                const decimal CDI = 0.009m; 
                decimal finalValue = investedvalue;                
                decimal taxRate; 
                
                // Calcula o rendimento 
                for (int i = 0; i < months; i++)
                {
                    // VF = VI x [1 + (CDI x TB)]
                    finalValue = finalValue * (1 + (CDI * Tb));
                }                
                
                if (months <= 6)
                    taxRate = 0.225m; // 22,5%
                else if (months <= 12)
                    taxRate = 0.20m;  // 20%
                else if (months <= 24)
                    taxRate = 0.175m; // 17,5%
                else
                    taxRate = 0.15m;  // 15%

                // O imposto é calculado sobre o rendimento (finalValue - investedvalue)
                decimal grossYield = finalValue - investedvalue;
                decimal taxAmount = grossYield * taxRate;
                decimal netFinalValue = finalValue - taxAmount;

                if (grossYield <= 0 || netFinalValue <= 0)
                    throw new Exception("Error ao obter calculo. Valor negativo");

                return Task.FromResult(new InvestingResult()
                {
                    GrossYield = (Convert.ToDecimal(investedvalue.ToString("F2")) + Convert.ToDecimal(grossYield.ToString("F2"))),
                    NetValue = Convert.ToDecimal(netFinalValue.ToString("F2")) 
                });
            }
			catch (Exception ex)
			{
                throw ex;
			}
        }
    }
}
