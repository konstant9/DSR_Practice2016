using System.Collections.Generic;
using System.Linq;
using AutoServiceModel;

namespace AutoServiceManager.ViewModels
{
    class ColumnChartViewModel : BaseChartViewModel
    {
        public ColumnChartViewModel()
        {
            Mediator.Mediator.Register("ColumnDiagramStatistics", ShowDiagram);
        }

    }
}
