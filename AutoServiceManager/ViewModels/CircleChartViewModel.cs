using System.Collections.Generic;
using System.Linq;
using AutoServiceManager.ViewModels;
using AutoServiceManager.Views;
using AutoServiceModel;

namespace AutoServiceManager.ViewModels
{
    class CircleChartViewModel:BaseChartViewModel
    {
        public CircleChartViewModel()
        {
            Mediator.Mediator.Register("CircleDiagramStatistics", ShowDiagram);            
        }

    }
}
