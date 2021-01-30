namespace Library.Helper.Interface
{
    public interface IConfigHelper
    {
        string GetConfiguration(ConfigSystemType systemType);
        T GetConfiguration<T>(ConfigSystemType systemType);
    }
}
