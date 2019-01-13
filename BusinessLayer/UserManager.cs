using System.Collections.Generic;
using Peoplee.API.DataAccesLayer;
using Peoplee.API.DtoModels;
using Peoplee.API.Models;

namespace Peoplee.API.BusinessLayer
{
    public class UserManager
    {
         IRepository<User> userManager;
   

        public UserManager(IRepository<User> um)
        {
        userManager = um;
    
        }

        public List<User> List(){

            return userManager.GetList();

        }

        //create a hashpassword and salt of password to make it more complex
         public User Register(UserViewModel userModel){

            byte[] passwordHash, passwordSalt;

            //string hash = securityProvider.GetSha256Hash(userModel.passaword);
            CreatePasswordHash(userModel.passaword, out passwordHash, out passwordSalt);

           // userModel.PasswordHash = passwordHash;
           // userModel.PasswordSalt = passwordSalt;

            int check = userManager.Insert(new User{
                Username = userModel.username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt      
            });

            if(check>0){
                User user = userManager.Get(x=>x.Username == userModel.username);
                return user;
            } 

             return null;

        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt){
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

      
        public User Login(UserViewModel userViewModel){

            User user = userManager.Get(x => x.Username == userViewModel.username);

            if(user != null){
                if(VerifyPassword(userViewModel.passaword, user.PasswordHash, user.PasswordSalt)){
                    return user;
                }
            }

            return null;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                     return false;
                }
                return true;
            }
        }

        //this method will use for check if the user exist or not
        private bool CheckUser(string username){

            User user = userManager.Get(x => x.Username == username);

            if(user != null) return true;

            return false;
        }


    }
}