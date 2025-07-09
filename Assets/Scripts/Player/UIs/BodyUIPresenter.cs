using System.Text;
using UnityEngine;

public class BodyUIPresenter
{
    
    private PlayerUIView _view;
    //플레이어의 바디
    private BaseBody _modelPlayer;

    private PartDetector _detector;

    private StringBuilder dropSB = new StringBuilder();
    private StringBuilder playerSB = new StringBuilder();

    public BodyUIPresenter(PlayerUIView view, BaseBody model)
    {
        _view = view;
        _modelPlayer = model;
        _detector = view.gameObject.GetComponent<PartDetector>();
        _detector.OnBodyDetected += OnBodyDetected;
        _detector.OnPartNotDetected += OnPartNotDetected;
    }

    private void OnBodyDetected(BaseBody body)
    {
        Debug.Log($"{body.BodyName}, {body.Tier}");
        ChangeDropBody(body);
        //ChangePlayerBody(_modelPlayer);
        _view.SetPartUIActive(true);
        // 파츠 정보 업데이트
    }
    
    private void ChangeDropBody(BaseBody dropBody)
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

    //todo 수정 + 런하면 null값이 됨
    private void ChangePlayerBody(BaseBody modelBody)
    {
        dropSB.Clear();
        
        // 제목
        string title = modelBody.Tier + "\n" + modelBody.BodyName;

        // 체력 및 방어막
        dropSB.AppendLine($"체력: {modelBody.Hp}s");
        dropSB.AppendLine($"방어막: {modelBody.Shield}");

        // 반감여부
        dropSB.AppendLine("반감: " + (modelBody.IsDmgHalf ? "50%" : "100%"));
        

        string finalText = dropSB.ToString();

        _view.ChangeDropPartInfo(null, modelBody.Tier, title, finalText);
    }

    private void OnPartNotDetected() { _view.SetPartUIActive(false); }
}