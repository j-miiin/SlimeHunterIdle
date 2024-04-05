using System.Collections.Generic;

public class DataManager : Singleton<DataManager>
{
    private readonly Dictionary<string, DataHandler> dataHandlerDic = new Dictionary<string, DataHandler>();

    public T GetDataHandler<T>() where T : DataHandler, new()
    {
        string key = typeof(T).Name;
        if (!dataHandlerDic.ContainsKey(key))
        {
            dataHandlerDic.Add(key, new T());
        }

        return dataHandlerDic[key] as T;
    }
}
