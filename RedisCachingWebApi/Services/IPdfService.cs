using RedisCachingWebApi.Domain;

namespace RedisCachingWebApi.Services
{
    public interface IPdfService
    {
        byte[] GenerateEmployeePdf(List<EmployeeData> employees);
    }
}