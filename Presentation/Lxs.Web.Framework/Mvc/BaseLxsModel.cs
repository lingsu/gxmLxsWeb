using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lxs.Web.Framework.Mvc
{
    public class BaseLxsModel
    {
        public BaseLxsModel()
        {
            this.CustomProperties = new Dictionary<string, object>();
        }
        public Dictionary<string, object> CustomProperties { get; set; }
    }
    public partial class BaseLxsEntityModel : BaseLxsModel
    {
        public virtual int Id { get; set; }
    }
}
