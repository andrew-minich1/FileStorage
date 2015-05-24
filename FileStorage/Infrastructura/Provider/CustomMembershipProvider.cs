using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Security;
using FileStorage.Models;
using BLL_Interface.Services;
using BLL_Interface.Entities;
using DAL_Interface.Repository;
using BLL.Mappers;
using BLL.Services;
using System.Web.Mvc;
using System.Collections.Generic;

namespace FileStorage.Infrastructura.Provider
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private IUserService service;

        public override MembershipUser GetUser(string login, bool userIsOnline)
        {
            service = System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>();
            try
            {
                var user = service.GetAllByPredicate(u => u.Login == login).FirstOrDefault();
                if (user == null)
                    return null;

                MembershipUser memberUser = new MembershipUser("CustomMembershipProvider", user.Login, null, null, null, null,
                    false, false, user.CreationDate, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

                return memberUser;
            }
            catch
            {
                return null;
            }
        }


        public MembershipUser CreateUser(UserEntity user)
        {
            service = System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>();
            MembershipUser membershipUser = GetUser(user.Login, false);

            if (membershipUser == null)
            {
                try
                {
                    service.Create(user);
                    membershipUser = GetUser(user.Login, false);
                    return membershipUser;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public override bool ValidateUser(string username, string password)
        {
            service = System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>();
            bool isValid = false;
            try
            {
                var user = service.GetAllByPredicate(u => u.Login == username).FirstOrDefault();
                if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                {
                    isValid = true;
                }
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            return service.GetAllEntities();
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

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }


        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

    }
}