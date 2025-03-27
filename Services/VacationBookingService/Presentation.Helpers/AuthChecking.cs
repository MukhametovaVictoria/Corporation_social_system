using Domain.Interfaces;
using System.Security.Claims;

namespace Presentation.Helpers
{
    public static class AuthChecking
    {
        public static bool CheckIsAuthorized(ClaimsPrincipal user, IEmployeeService _employeeService)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Получение ID пользователя из токена

            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            if (Guid.TryParse(userId, out var guid))
            {
                var employee = _employeeService.GetEmployeeByIdAsync(guid);
                if (employee != null)
                    return true;
            }

            return false;
        }
    }
}
