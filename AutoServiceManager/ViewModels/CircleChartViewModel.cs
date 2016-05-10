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
