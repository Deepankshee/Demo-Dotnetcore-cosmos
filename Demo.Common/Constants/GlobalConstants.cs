using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Common.Constants
{
    public static class GlobalConstants
    {
        public const string CollectionType = "Demo";
        public const string BasePartitionKey = "basePartitionKey";
    }

    public static class DocumentType
    {
        public const string User = "User";
        public const string Product = "Product";
    }

    public static class Triggers
    {
        public const string AddUpdateOnUpdate = "addUpdatedOnUpdate";
    }

    public static class SPNames
    {
        public const string SPCreateItem = "sp_CreateItem";
        public const string SPGetProductByUsrId = "sp_GetProductsByUserId";
    }

    public static class ClaimType
    {
        public const string Id = "LoginId";
    }

    public static class ResponseMessages
    {
        public const string AuthErrorMessage = "Invalid User Name or Password";
        public const string UserAddSuccess = "User Added Successfully";
        public const string UserUpdateSuccess = "User Added Successfully";
        public const string ProductAddSuccess = "Product added successfully";
    }
}
