using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MediaCollector.Amazon
{
    public class AmazonUtil
    {
        public static AmazonService.Item GetAmazonItem(string eanCode)
        {
            AmazonService.Item foundItem = null;

            if(!string.IsNullOrEmpty(eanCode))
            {
                // make sure we have 13 chars.
                eanCode = FillEanCode(eanCode);

                // create a WCF Amazon ECS client
                BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
                binding.MaxReceivedMessageSize = int.MaxValue;
                AmazonService.AWSECommerceServicePortTypeClient client = new AmazonService.AWSECommerceServicePortTypeClient(
                    binding,
                    new EndpointAddress("https://webservices.amazon.com/onca/soap?Service=AWSECommerceService"));

                // add authentication to the ECS client
                client.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(ConfigurationManager.AppSettings["accessKeyId"], ConfigurationManager.AppSettings["secretKeyId"]));
                
                AmazonService.ItemLookupRequest lookupRequest = new AmazonService.ItemLookupRequest();
                
                lookupRequest.IdType = AmazonService.ItemLookupRequestIdType.EAN;
                lookupRequest.IdTypeSpecified = true;
                lookupRequest.SearchIndex = "All";
                lookupRequest.ResponseGroup = new string[] { "Images", "ItemAttributes" };
                lookupRequest.ItemId = new string[] { eanCode };
                
                
                AmazonService.ItemLookup itemLookup = new AmazonService.ItemLookup();

                itemLookup.AWSAccessKeyId = ConfigurationManager.AppSettings["accessKeyId"];
                itemLookup.AssociateTag = "aztag-20";
                itemLookup.Request = new AmazonService.ItemLookupRequest[] { lookupRequest };

                AmazonService.ItemLookupResponse response = client.ItemLookup(itemLookup);
            
                if (response.Items[0].Request.Errors != null && response.Items[0].Request.Errors.Count<AmazonService.ErrorsError>() > 0)
                {
                    //name = response.Items[0].Request.Errors[0].Message;
                }
                else
                {
                    foundItem = response.Items[0].Item.FirstOrDefault<AmazonService.Item>();                 
                }

            }

            return foundItem;
        }

        private static string FillEanCode(string eanCode)
        {
            string tempEanCode = eanCode;

            if (tempEanCode.Length < 13)
            {
                tempEanCode = "0" + eanCode;

                if (tempEanCode.Length < 13)
                {
                    tempEanCode = FillEanCode(tempEanCode);
                }
            }

            return tempEanCode;
        }
    }
}
