using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaCollector.Controllers.Models;

namespace MediaCollector.Controllers.MediaController
{
    public class MediaController
    {
        public static Media GetMedia(string eanCode)
        {
            MediaModelsDataContext dbContext = new MediaModelsDataContext();

            Media selectedMedia = null;

            selectedMedia = dbContext.Medias.SingleOrDefault<Media>(med => med.EAN.Equals(eanCode));

            return selectedMedia;
        }

        public static Media SearchMedia(string eanCode)
        {
            Media foundMedia = GetMedia(eanCode);

            // could not find it in the database so Search for it in Amazon
            if (foundMedia == null)
            {
                MediaCollector.Amazon.AmazonService.Item amazonItem = Amazon.AmazonUtil.GetAmazonItem(eanCode);

                if (amazonItem != null)
                {
                    foundMedia = BuildMediaModel(amazonItem);

                    // save to DB
                    SaveMedia(foundMedia);
                }
            }

            return foundMedia;
        }

        public static void SaveMedia(Media itemToBeSaved)
        {
            MediaModelsDataContext dbContext = new MediaModelsDataContext();

            dbContext.Medias.InsertOnSubmit(itemToBeSaved);

            dbContext.SubmitChanges();
        }

        private static Media BuildMediaModel(Amazon.AmazonService.Item amazonItem)
        {
            Media returnMedia = new Media();

            returnMedia.Title = amazonItem.ItemAttributes.Title;
            returnMedia.EAN = amazonItem.ItemAttributes.EAN;

            int mediaId = UpdateMediaTypes(amazonItem.ItemAttributes.ProductGroup);

            returnMedia.fkMediaTypeId = mediaId;
            
            if(amazonItem.LargeImage != null)
            {
                returnMedia.Image = amazonItem.LargeImage.URL;
            }

            return returnMedia;
        }

        private static int UpdateMediaTypes(string MediaType)
        {
            MediaModelsDataContext dbContext = new MediaModelsDataContext();

            MediaType foundType = dbContext.MediaTypes.SingleOrDefault<MediaType>(mType => mType.MediaType1.Equals(MediaType));

            if (foundType == null)
            {
                foundType = new MediaType();
                foundType.MediaType1 = MediaType;

                dbContext.MediaTypes.InsertOnSubmit(foundType);

                dbContext.SubmitChanges();
            }

            return foundType.MediaTypeId;
        }

        public static List<Media> GetAllMedia()
        {
            MediaModelsDataContext dbContext = new MediaModelsDataContext();

            return dbContext.Medias.ToList<Media>();
        }

        public static List<MediaType> GetAllMediaTypes()
        {
            MediaModelsDataContext dbContext = new MediaModelsDataContext();

            return dbContext.MediaTypes.ToList<MediaType>();
        }
    }
}
