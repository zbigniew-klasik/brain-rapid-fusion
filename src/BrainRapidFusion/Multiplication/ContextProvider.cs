namespace BrainRapidFusion.Multiplication
{
    public class ContextProvider : IContextProvider
    {
        private static Context Context = Context.CreateNew();

        public Context Get()
        {
            return Context;
        }
    }
}
