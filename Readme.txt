1)  Choice of DataFormat
	a) GetItems:  Gets all items from the repository 
		Sample Request: http://<hostname>/OrderService.svc/getitems 
		Sample Response: 

		{"GetItemsResult":{"Body":"[{\"key\":\"57d63d32-6201-4e2e-83ce-f549337af9bc\",\"value\":{\"Description\":\"Description4\",\"Name\":\"Name4\",\"Price\":4}},{\"key\":\"892ce372-2fc9-4489-b41a-65fcd88bd50a\",\"value\":{\"Description\":\"Description3\",\"Name\":\"Name3\",\"Price\":3}},{\"key\":\"6b6f7411-0631-445d-b967-c6b9c7386613\",\"value\":{\"Description\":\"Description2\",\"Name\":\"Name2\",\"Price\":2}},{\"key\":\"0f0928d3-6a71-46dd-93ad-cde21f47e5e6\",\"value\":{\"Description\":\"Description5\",\"Name\":\"Name5\",\"Price\":5}},{\"key\":\"96852902-1baf-4883-9321-51ce809ef9a0\",\"value\":{\"Description\":\"Description1\",\"Name\":\"Name1\",\"Price\":1}},{\"key\":\"4bfb7657-5ce6-43f9-a3da-1f6c74a5510a\",\"value\":{\"Description\":\"Description0\",\"Name\":\"Name0\",\"Price\":0}}]","Message":"Succeeded","RequestStatus":"OK"}}

 	b) PurchaseItem: Purchases an item from the repository.  
		Sample Request: http://<hostname>/OrderService.svc/purchaseitem/57d63d32-6201-4e2e-83ce-f549337af9bc
		Sample Response: 

		{"PurchaseItemResult":{"Body":"{\"Description\":\"Description4\",\"Name\":\"Name4\",\"Price\":4}","Message":"Succeeded","RequestStatus":"OK"}}

Enhancements that can be made further
1) Tests for Database.cs :  This will be some kind of database in real world. The tests for the individual methods of Database.cs were not written, although the unit tests for OrderService should cover it. 

2) Separating Functional and Unit tests:  Generally these should be separated out but to keep it simple they have been combined into a single project. 

3) Authentication and Authorization (Not complete):  The idea is to use the WCF settings for role based authorization to prohibit the access of purchase items to just the authenticated users.  GetItems can be open to everyone.  I wasn't able to get this working. web.config file in the Datawarehouse project has a commented out section describing the settings.  But seems like there is an issue with it. If setup correctly it should call the Authenticator.Validate method. 	