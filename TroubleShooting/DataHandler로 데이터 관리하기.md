![header](https://capsule-render.vercel.app/api?type=cylinder&color=98D8AA&height=150&section=header&text=DataHandler&fontSize=60&fontColor=FFF&animation=fadeIn)

<br>

### ⚠️ 문제
- 데이터를 불러오거나 저장하는 동작이 필요한 곳에서 각각 따로 데이터를 처리하는 방식
  - 데이터를 처리하는 곳이 분산되어 데이터를 일관된 방식으로 처리하기 어렵고 디버깅이 복잡해지는 단점
  - 코드 가독성 저하
    
<br>

### 🛠️ 시도
- DataManager를 통해서 모든 데이터를 불러오거나 저장하는 방식
  - DataManager의 역할이 방대해지고 코드 가독성이 저하됨

<br>

### 💡 선택
- 각 데이터를 다루는 DataHandler를 생성하고, DataManager를 통해 DataHandler에 접근하는 방식
  ```cs
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
  ```
- 데이터의 처리는 각 DataHandler 내부에서만 이루어져 디버깅이 쉬워지고 코드 가독성이 향상됨
  ```cs
  public class SummonDataHandler : DataHandler
  {
    ...

    // 소환 정보 리스트 저장
    public void SaveSummonDictionary()
    {
        ES3.Save<Dictionary<SummonType, Summon>>("summonDic", _summonDic);
    }

    // 소환 정보 리스트 로드
    public Dictionary<SummonType, Summon> LoadSummonDictionary()
    {
        LoadSummonDataSOList();
        if (ES3.KeyExists("summonDic"))
        {
            _summonDic = ES3.Load<Dictionary<SummonType, Summon>>("summonDic");
            SetSummonDataSO();
        }
        else
        {
            CreateSummonList();
        }
        return _summonDic;
    }
  }
  ```
- 데이터가 필요한 곳에서는 DataHandler를 통해 일관된 방식으로 데이터 처리 가능
  ```cs
  public class SummonSystem 
  {
    ...

    private SummonDataHandler _dataHandler;

    public void InitSummonSystem()
    {
        ...

        _dataHandler = DataManager.Instance.GetDataHandler<SummonDataHandler>();
        _summonDic = _dataHandler.LoadSummonDictionary();
    }

    public void SummonWeapon(SummonCountType countType)
    {
        ... 

        _dataHandler.SaveSummonDictionary();
    }
  }
  ```

<br><br>

### 상세 코드 보기

|클래스|기능|
|:---:|:---:|
|[DataManager](https://github.com/j-miiin/SlimeHunterIdle/blob/main/Scripts/Managers/DataManager.cs)|DataHandler를 반환하는 제네릭 메서드를 제공한다.|
|[DataHandler](https://github.com/j-miiin/SlimeHunterIdle/blob/main/Scripts/Handlers/Data/DataHandler.cs)|모든 DataHandler의 부모 클래스이다.|
|[CurrencyDataHandler](https://github.com/j-miiin/SlimeHunterIdle/blob/main/Scripts/Handlers/Data/CurrencyDataHandler.cs)|DataHandler의 자식 클래스 중 하나로, 재화와 관련된 데이터를 처리한다.|

<br><br>

#### [🍄 Main README로 돌아가기 🍄](/README.md)
