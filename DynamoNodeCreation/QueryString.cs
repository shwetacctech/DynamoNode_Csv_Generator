using Autodesk.DesignScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoJsonCreator
{
    public static class QueryString
    {
      /// <summary>
      /// Query for fetching data
      /// </summary>
        public static readonly string PropertyByURN = @"query GetElementsByExchangeFile ($exchangeFileId: ID!,$elementPagination: PaginationInput){
  exchangeByFileId(exchangeFileId: $exchangeFileId){
      id
      name
      elements(pagination: $elementPagination){
          pagination{
              pageSize
              cursor
          },
          results{
             name
             id
              properties{
                  results{
                      name
                      value
                  }
              }
            
          }
      }
    }
  }

";
    }
}
