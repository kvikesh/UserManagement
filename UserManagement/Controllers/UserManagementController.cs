using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.Contracts.Models;
using UserManagement.Models;
using UserManagement.Repository;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManagementRepository _userManagementRepository;

        public UserManagementController(UserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        [HttpGet]
        [Route("GetUsers")]
        public IEnumerable<UserResponse> GetAllUsers()
        {
            var result = _userManagementRepository.GetAllUsers();
            return result;
        }

        [HttpPost]
        [Route("CreateUser")]
        public HttpResponseMessage CreateUser(CreateUserRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (String.IsNullOrEmpty(request.FirstName) || String.IsNullOrEmpty(request.LastName))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;

            }
            if (request.CustomerId != null && !this.IsValidCustomer(request.CustomerId))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            _userManagementRepository.CreateUserRequest(request);
            response.StatusCode = HttpStatusCode.OK;
            return response;

        }

        private bool IsValidCustomer(int? customerId)
        {
            var role = _userManagementRepository.GetCustomer(customerId);
            if (role == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
