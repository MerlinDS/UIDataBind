namespace UIDataBind.Base
{
    public struct ModelQuery : IEngineProvider
    {
        public readonly BindingPath Path;
        public readonly QueryType QueryType;
        public readonly BindingPath[] Filter;

        public ModelQuery(BindingPath modelPath, QueryType queryType, BindingPath[] filter = default)
        {
            Path = modelPath;
            QueryType = queryType;
            Filter = filter;
        }
    }
}