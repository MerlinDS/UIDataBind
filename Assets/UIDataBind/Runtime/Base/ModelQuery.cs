namespace UIDataBind.Base
{
    public struct ModelQuery : IEngineProvider
    {
        public readonly BindingPath Path;
        public readonly QueryType Type;
        public readonly BindingPath[] Filter;

        public ModelQuery(BindingPath modelPath, QueryType type, BindingPath[] filter = default)
        {
            Path = modelPath;
            Type = type;
            Filter = filter;
        }
    }
}