using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Contracts.Models;
using UserManagement.Models;

namespace UserManagement.Repository
{
    public class UserManagementRepository
    {
        private readonly UserManagementContext _context;
        public UserManagementRepository(UserManagementContext context)
        {
            this._context = context;

        }

        public IEnumerable<UserResponse> GetAllUsers()
        {
            var listOfUsers = _context.Users.Include(x => x.UserRoles).Select(x => x).ToList();
            var usersResponse = UserMapper(listOfUsers);
            return usersResponse;
        }

        public void CreateUserRequest(CreateUserRequest request)
        {
            Users _user = new Users();
            _user.FirstName = request.FirstName;
            _user.LastName = request.LastName;
            _user.IsAdmin = request.IsAdmin;
            _user.IsTrialUser = request.IsTrialUser;
            _user.CustomerId = request.CustomerId;
            _user.Email = request.Email;

            _context.Users.Add(_user);
            _context.SaveChanges();

            List<UserRoles> userRoles = new List<UserRoles>();
            foreach (int _role in request.Roles)
            {
                if (_context.Roles.Any(x => x.Id == _role))
                {
                    UserRoles userRole = new UserRoles();
                    userRole.RoleId = _role;
                    userRole.UserId = _user.Id;
                    userRoles.Add(userRole);
                }
            }
            _context.UserRoles.AddRange(userRoles);
            _context.SaveChanges();
            return;
        }

        public Customer GetCustomer(int? cusotmerId) {
            return _context.Customer.FirstOrDefault(x => x.Id == cusotmerId);
        }

        private List<UserResponse> UserMapper(IEnumerable<Users> users) {
            List<UserResponse> usersresponse = new List<UserResponse>();

            foreach (Users user in users) {
                UserResponse _userresponse = new UserResponse();
                _userresponse.FirstName = user.FirstName;
                _userresponse.LastName = user.LastName;
                _userresponse.IsAdmin = user.IsAdmin;
                _userresponse.IsTrialUser = user.IsTrialUser;
                _userresponse.CustomerId = user.CustomerId;
                _userresponse.Email = user.Email;
                _userresponse.UserRoles = new List<int>(); ;
                foreach (UserRoles userRole in user.UserRoles) {
                    _userresponse.UserRoles.Add(userRole.RoleId);
                }
                usersresponse.Add(_userresponse);
            }
            return usersresponse;
        }
    }
}
