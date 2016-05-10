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
