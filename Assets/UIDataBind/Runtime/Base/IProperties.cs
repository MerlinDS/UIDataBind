namespace UIDataBind.Base
{
    public interface IProperties
    {
        void    UpdateModel<TViewModel>(ref TViewModel model) where TViewModel : IViewModel;
        void    Fetch<TViewModel>(TViewModel model) where TViewModel : IViewModel;
        TEntity GetPropertyEntity<TEntity>(string propertyName, bool createIfNull = false) where TEntity : class;
    }
}