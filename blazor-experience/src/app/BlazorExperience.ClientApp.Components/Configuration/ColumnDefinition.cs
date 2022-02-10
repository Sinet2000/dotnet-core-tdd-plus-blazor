using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExperience.ClientApp.Components.Configuration
{
    public class ColumnDefinition
    {

        public ColumnDefinition()
        {
            this.DataType = DataType.NotSet;
            this.Alingment = Alingment.NotSet;
        }

        public string DataField { get; set; }

        public string Caption { get; set; }

        public DataType DataType { get; set; }
        public string Format { get; set; }

        public Alingment Alingment { get; set; }
    }
}
