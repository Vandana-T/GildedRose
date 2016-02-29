1)  Choice of DataFormat
	a) GetItems:  Gets all items from the repository 
		Sample Request: 
		Sample Response: 
 	b) PurchaseItem: Purchases an item from the repository.  
		Sample Request: 
		Sample Response: 

Enhancements that can be made further
1) Tests for Database.cs :  This will be some kind of database in real world. The tests for the individual methods of Database.cs were not written, although the unit tests for OrderService should cover it. 
2) Separating Functional and Unit tests:  Generally these should be separated out but to keep it simple they have been combined into a single project. 
3) Authentication and Authorization:  The idea is to use the WCF settings for role based authorization to prohibit the access of purchase items to just the authenticated users.  I wasn't able to get this working. 


	