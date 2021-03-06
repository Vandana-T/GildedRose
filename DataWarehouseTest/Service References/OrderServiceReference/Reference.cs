﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataWarehouseTest.OrderServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OrderServiceReference.IOrderService")]
    public interface IOrderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetItems", ReplyAction="http://tempuri.org/IOrderService/GetItemsResponse")]
        DataWarehouse.Response GetItems();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/GetItems", ReplyAction="http://tempuri.org/IOrderService/GetItemsResponse")]
        System.Threading.Tasks.Task<DataWarehouse.Response> GetItemsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/PurchaseItem", ReplyAction="http://tempuri.org/IOrderService/PurchaseItemResponse")]
        DataWarehouse.Response PurchaseItem(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOrderService/PurchaseItem", ReplyAction="http://tempuri.org/IOrderService/PurchaseItemResponse")]
        System.Threading.Tasks.Task<DataWarehouse.Response> PurchaseItemAsync(string id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderServiceChannel : DataWarehouseTest.OrderServiceReference.IOrderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderServiceClient : System.ServiceModel.ClientBase<DataWarehouseTest.OrderServiceReference.IOrderService>, DataWarehouseTest.OrderServiceReference.IOrderService {
        
        public OrderServiceClient() {
        }
        
        public OrderServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public DataWarehouse.Response GetItems() {
            return base.Channel.GetItems();
        }
        
        public System.Threading.Tasks.Task<DataWarehouse.Response> GetItemsAsync() {
            return base.Channel.GetItemsAsync();
        }
        
        public DataWarehouse.Response PurchaseItem(string id) {
            return base.Channel.PurchaseItem(id);
        }
        
        public System.Threading.Tasks.Task<DataWarehouse.Response> PurchaseItemAsync(string id) {
            return base.Channel.PurchaseItemAsync(id);
        }
    }
}
