using System.Text;
using UnityEngine;

public class BodyUIPresenter
{
    
    private PlayerUIView _view;
    
    //플레이어의 바디
    private BaseBody _body;

    private PartDetector _detector;

    private StringBuilder dropSB = new StringBuilder();
    private StringBuilder playerSB = new StringBuilder();
    
    #region BodyPresenter Init시에 BodyModel(_bodyModel)초기화
    
    public void Init(PlayerUIView view)
    {
        _view = view;
        
        InitializeBodyModel(view);

        _detector = view.gameObject.GetComponent<PartDetector>();
        _detector.OnBodyDetected += OnBodyDetected;
        _detector.OnPartNotDetected += OnPartNotDetected;
    }

    private void InitializeBodyModel(PlayerUIView view)
    {
        Player _player = view.gameObject.GetComponent<Player>();

        var body = _player.Body.GetComponent<BaseBody>();
        
        string MakeName(string name)
        {
            string n = name.Split(" (")[1].Split(')')[0];
            var makeName = n.Substring(0, 1).ToLower() + n.Substring(1);
            return makeName;
        }

        // 바디 이름 기준으로 CommonTier DTO 추출
        var b = Managers.Managers.PartsData.BodyData[MakeName(body.ToString())][3];

        BodyDTO bB = Managers.Managers.PartsData.MakeBodyDTO(MakeName(body.ToString()), b);

        body.Init(bB);

        _body = body;
    }

    #endregion

    private void OnBodyDetected(BaseBody body)
    {
        ChangeDropBodyUI(body);
        ChangePlayerBodyUI(_body);
        _view.SetPartUIActive(true);
        // 파츠 정보 업데이트
    }
    
    //todo 게임오브젝트 ui로 img에 넣기
    private void ChangeDropBodyUI(BaseBody dropBody)
    {
        dropSB.Clear();
        // 제목
        string title = dropBody.Tier + "\n" + dropBody.BodyName;

        // 체력 및 방어막
        dropSB.AppendLine($"체력: {dropBody.Hp}s");
        dropSB.AppendLine($"방어막: {dropBody.Shield}");

        // 반감여부
        dropSB.AppendLine("반감: " + (dropBody.IsDmgHalf ? "50%" : "100%"));
        

        string finalText = dropSB.ToString();

        _view.ChangeDropPartInfo(null, dropBody.Tier, title, finalText);
    }

    //todo 게임오브젝트 ui로 img에 넣기
    private void ChangePlayerBodyUI(BaseBody modelBody)
    {
        playerSB.Clear();
        
        // 제목
        string title = modelBody.Tier + "\n" + modelBody.BodyName;

        // 체력 및 방어막
        playerSB.AppendLine($"체력: {modelBody.Hp}s");
        playerSB.AppendLine($"방어막: {modelBody.Shield}");

        // 반감여부
        playerSB.AppendLine("반감: " + (modelBody.IsDmgHalf ? "50%" : "100%"));
        

        string finalText = playerSB.ToString();

        _view.ChangePlayerPartInfo(true, null, modelBody.Tier, title, finalText);
    }

    private void OnPartNotDetected() { _view.SetPartUIActive(false); }
}