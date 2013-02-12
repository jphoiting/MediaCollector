using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaCollector
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var allMedia = MediaCollector.Controllers.MediaController.MediaController.GetAllMedia();

            var allMediaTypes = MediaCollector.Controllers.MediaController.MediaController.GetAllMediaTypes();

            repeaterMedia.DataSource = allMedia;
            repeaterMedia.DataBind();

            repeaterMediaTypes.DataSource = allMediaTypes;
            repeaterMediaTypes.DataBind();
        }
    }
}