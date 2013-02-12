using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.ServiceModel;

using MediaCollector.Controllers.MediaController;
using MediaCollector.Controllers.Models;

namespace MediaCollector
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonSearchAmazon_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textboxIdentifierInput.Text))
            {
                string ean = textboxIdentifierInput.Text;

                Media item = MediaController.SearchMedia(ean);

                if (item != null)
                {
                    literalTitles.Text = item.Title;
                    imageCover.ImageUrl = item.Image;
                }
            }
        }
    }
    
}