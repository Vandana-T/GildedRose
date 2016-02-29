namespace DataWarehouseTest
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    using DataWarehouse;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AssemblyInitializer
    {
        [AssemblyInitialize]
        public static void StartService(TestContext context)
        {
            Uri baseAddress = new Uri(TestUtils.BaseUrl);
            ServiceHost host = new ServiceHost(typeof(DataWarehouse.OrderService), baseAddress);

            try
            {
                host.AddServiceEndpoint(typeof(IOrderService), new WSHttpBinding(), "OrderService");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                host.Description.Behaviors.Add(smb);

                host.Open();
                Console.WriteLine("Service is ready");
                //Console.WriteLine("Press Enter to terminate service");
                //Console.WriteLine();
                //Console.ReadLine();

                //host.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("Exception occurred {0}", ce.Message);
                host.Abort();
            }
        }
    }
}
