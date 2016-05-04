using System.Collections.Generic;
using System.Linq;
using AutoServiceModel;

namespace AutoServiceManager.ViewModels
{
    class LinearChartViewModel : BaseChartViewModel
    {
        public LinearChartViewModel()
        {
            Mediator.Mediator.Register("LinearDiagramStatistics", ShowDiagram);
        }
    }
}
