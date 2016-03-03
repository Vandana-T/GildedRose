namespace DataWarehouse
{
    using System.IdentityModel.Selectors;
    using System.Security.Cryptography;
    using System.ServiceModel;
    using System.Text;

    public class Authenticator : UserNamePasswordValidator
    {
        /// <summary>
        /// Verifies the username and password combination for the user. 
        /// DB should store the one way hash for the password. 
        /// </summary>
        /// <param name="userName">Username for the user</param>
        /// <param name="password">Password for the user</param>
        public override void Validate(string userName, string password)
        {
            SHA256 shaHash = SHA256Managed.Create();

            if (!(userName == "ShouldComeFromDB" && password == "OneWayHashOfPasswordFromDB"))
            {
                throw new FaultException("UserName password combination is invalid");
            }
        }
    }
}