using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FileStorage.Models;
using BLL_Interface.Services;
using BLL_Interface.Entities;
using DAL_Interface.Repository;
using BLL.Mappers;
using BLL.Services;

namespace FileStorage.Infrastructura.Providers
{
    public class CustomRoleProvider : RoleProvider
    {

        private readonly IRoleService roleService;
        private readonly IUserService userService;

        public CustomRoleProvider(IRoleService roleService, IUserService userService)
        {
            this.roleService = roleService;
            this.userService = userService;
        }

        public override void CreateRole(string roleName)
        {
            roleService.Create(new RoleEntity() { Name = roleName });
        }


        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            var user = userService.GetOneByPredicate(u => u.Login == username);
            if (user == null) return roles;
            var userRole = user.Roles;
            if (userRole != null)
            {
                roles = new string[] { userRole.Select(r => r.Name).ToString()};
            }
            return roles;

        }


        public override bool IsUserInRole(string username, string roleName)
        {
            var user = userService.GetOneByPredicate(u => u.Login == username);
            if (user == null) return false;
            var role = user.Roles.Select(r => r.Name == roleName);
            if (role != null)
            {
                return true;
            }

            return false;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }


        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}