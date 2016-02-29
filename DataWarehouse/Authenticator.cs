namespace DataWarehouse
{
    using System.IdentityModel.Selectors;
    using System.ServiceModel;

    public class Authenticator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (!(userName == "Vandana" && password == "password"))
            {
                throw new FaultException("UserName password combination is invalid");
            }
        }
    }
}