public class StageDataHandler : DataHandler
{
    public void SaveStageInfo(Stage stage)
    {
        ES3.Save<Stage>("curStage", stage);
    }

    public Stage LoadStageInfo()
    {
        Stage curStage;

        if (ES3.KeyExists("curStage")) curStage = ES3.Load<Stage>("curStage");
        else curStage = new Stage(level: 1);

        return curStage;
    }
}
