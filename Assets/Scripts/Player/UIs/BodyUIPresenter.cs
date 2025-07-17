using System.Text;
using UnityEngine;

public class BodyUIPresenter
{
    
    private PlayerUIView _view;
    
    //플레이어의 바디
    private BaseBody _body;

    private PartDetector _detector;

    private StringBuilder sb = new StringBuilder();
    
    #region BodyPresenter Init시에 BodyModel(_bodyModel)초기화
    
    public void Init(PlayerUIView view)
    {
        _view = view;
        
        InitializeBodyModel(view);

        _detector = view.gameObject.GetComponent<PartDetector>();
        
        _detector.OnBodyDetected += OnBodyDetected;
        _detector.OnInteractBodyUIToggle += OnInteractBodyUIDetected;
        _detector.OnPartNotDetected += OnPartNotDetected;
        _detector.OnCancelDetect += OnCloseAllUI;
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
    
    //todo 게임오브젝트 ui로 img에 넣기
    private void ChangeDropBodyUI(BaseBody dropBody)
    {
        // 제목
        string title = dropBody.Tier + "\n" + dropBody.BodyName;

        string desc = DrawBodyDescUI(dropBody);

        _view.ChangeDropInfo(null, dropBody.Tier, title, desc);
    }

    //todo 게임오브젝트 ui로 img에 넣기
    private void ChangePlayerBodyUI(BaseBody modelBody)
    {
        // 제목
        string title = modelBody.Tier + "\n" + modelBody.BodyName;

        string desc = DrawBodyDescUI(modelBody);

        _view.ChangePlayerBodyInfo(null, modelBody.Tier, title, desc);
    }
    
    private string DrawBodyDescUI(BaseBody body)
    {
        sb.Clear();
        
        // 체력 및 방어막
        sb.AppendLine($"체력: {body.Hp}s");
        sb.AppendLine($"방어막: {body.Shield}");

        // 반감여부
        sb.AppendLine("반감: " + (body.IsDmgHalf ? "50%" : "100%"));

        return sb.ToString();
    }

    #region Detector Callback Action

    //파트 감지 후 상호작용 키 누름
    private void OnInteractBodyUIDetected()
    {
        _view.PressedInteractKeyOnBody();
    }

    //파트 감지 성공
    private void OnBodyDetected(BaseBody body)
    {
        if (body == null) return;
        
        ChangeDropBodyUI(body);
        ChangePlayerBodyUI(_body);
        _view.SetPartUIActive(true);
        // 파츠 정보 업데이트
    }
    
    //파트 감지 안됨
    private void OnPartNotDetected()
    {
        sb.Clear();
        _view.Undetected();
    }
    
    private void OnCloseAllUI()
    {
        _view.QuitInteractUI();
    }

    #endregion
}