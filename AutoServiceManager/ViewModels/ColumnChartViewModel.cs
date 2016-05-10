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
