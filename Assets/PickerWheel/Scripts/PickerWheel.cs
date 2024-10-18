using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections.Generic;
using TMPro;

namespace EasyUI.PickerWheelUI
{

    // PickerWheel 클래스: 룰렛 휠을 구현하는 클래스
    public class PickerWheel : MonoBehaviour
    {
        public static PickerWheel instance;

        [Header("References :")]
        [SerializeField] private GameObject linePrefab; // 룰렛 구간 사이의 선을 나타내는 프리팹
        [SerializeField] private Transform linesParent; // 선들을 담을 부모 오브젝트

        [Space]
        [SerializeField] private Transform PickerWheelTransform; // 룰렛의 Transform 객체
        [SerializeField] private Transform wheelCircle; // 룰렛의 중심 원 Transform
        [SerializeField] private GameObject wheelPiecePrefab; // 룰렛의 각 구간 조각을 나타내는 프리팹
        [SerializeField] private Transform wheelPiecesParent; // 룰렛 조각들을 담을 부모 오브젝트

        [Space]
        public Sprite[] outSide_tokenSprites;
        public Sprite[] inSide_tokenSprites;
        public List<TextMeshProUGUI> scoreTexts;
        public List<Animator> scoreAnimators;

        [Space]
        [Header("Sounds :")]
        [SerializeField] private AudioSource audioSource; // 오디오 소스 (사운드를 재생하는 컴포넌트)
        [SerializeField] private AudioClip tickAudioClip; // 룰렛이 돌 때마다 발생하는 틱 소리
        [SerializeField][Range(0f, 1f)] private float volume = .5f; // 틱 소리의 볼륨
        [SerializeField][Range(-3f, 3f)] private float pitch = 1f; // 틱 소리의 피치

        [Space]
        [Header("Picker wheel settings :")]
        [Range(1, 20)] public int spinDuration = 8; // 룰렛이 도는 시간(초)
        [Range(0f, 1080f)] public float spinPower = 1f; // 룰렛이 도는 시간(초)
        [SerializeField][Range(.2f, 2f)] private float wheelSize = 1f; // 룰렛의 크기

        [Space]
        [Header("Picker wheel pieces :")]
        public WheelPiece[] wheelPieces; // 룰렛의 각 구간을 나타내는 WheelPiece 배열

        // 이벤트
        private UnityAction onSpinStartEvent; // 룰렛 시작 시 호출되는 이벤트
        private UnityAction<WheelPiece> onSpinEndEvent; // 룰렛이 멈췄을 때 호출되는 이벤트

        private bool _isSpinning = false; // 룰렛이 돌고 있는지 여부를 나타내는 플래그

        public bool IsSpinning { get { return _isSpinning; } } // 룰렛이 돌고 있는지 확인하는 프로퍼티

        // 조각 크기 설정
        private Vector2 pieceMinSize = new Vector2(81f, 146f); // 조각의 최소 크기
        private Vector2 pieceMaxSize = new Vector2(144f, 213f); // 조각의 최대 크기
        private int piecesMin = 2; // 최소 조각 개수
        private int piecesMax = 12; // 최대 조각 개수

        private float pieceAngle; // 각 조각의 각도
        private float halfPieceAngle; // 각 조각의 절반 각도
        private float halfPieceAngleWithPaddings; // 패딩을 포함한 절반 각도

        private double accumulatedWeight; // 조각들의 가중치 합산 값
        private System.Random rand = new System.Random(); // 랜덤 값을 생성하는 객체

        private List<int> nonZeroChancesIndices = new List<int>(); // 0 이상의 확률을 가진 조각들의 인덱스 리스트

        private void Awake()
        {
            instance = this;
        }

        public void WheelSetting()
        {
            spinPower = 120;
            scoreAnimators = new List<Animator>();
            scoreTexts = new List<TextMeshProUGUI>();

            DestroyAllChildren(linesParent);
            DestroyAllChildren(wheelPiecesParent);

            // 각 조각의 각도를 계산
            pieceAngle = 360 / wheelPieces.Length;
            halfPieceAngle = pieceAngle / 2f;
            halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle / 4f);

            Generate(); // 룰렛 조각 생성

            CalculateWeightsAndIndices(); // 가중치와 인덱스 계산
            if (nonZeroChancesIndices.Count == 0)
                Debug.LogError("You can't set all pieces chance to zero"); // 모든 조각의 확률이 0일 수 없다는 오류 메시지

            SetupAudio(); // 오디오 설정

        }

        private void SetupAudio()
        {
            // 오디오 설정: 틱 사운드와 볼륨, 피치를 설정
            audioSource.clip = tickAudioClip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
        }

        private void Generate()
        {
            // 룰렛의 각 조각을 생성하고 설정
            //wheelPiecePrefab = InstantiatePiece();

            // 각 조각의 크기를 조정
            RectTransform rt = wheelPiecePrefab.transform.GetChild(0).GetComponent<RectTransform>();
            float pieceWidth = Mathf.Lerp(pieceMinSize.x, pieceMaxSize.x, 1f - Mathf.InverseLerp(piecesMin, piecesMax, wheelPieces.Length));
            float pieceHeight = Mathf.Lerp(pieceMinSize.y, pieceMaxSize.y, 1f - Mathf.InverseLerp(piecesMin, piecesMax, wheelPieces.Length));
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pieceWidth);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pieceHeight);

            for (int i = 0; i < wheelPieces.Length; i++)
                DrawPiece(i); // 각 조각을 그리는 함수 호출

            //Destroy(wheelPiecePrefab); // 임시로 생성한 프리팹을 삭제
        }

        private void DrawPiece(int index)
        {
            // 특정 인덱스의 룰렛 조각을 그림
            WheelPiece piece = wheelPieces[index];
            Transform pieceTrns = InstantiatePiece().transform.GetChild(0);

            // 스피드 토큰에 따른 휠 속도 조절
            if (piece.token.outSideToken == Token.OutSideToken.pSpeedToken) spinPower += 60;
            if (piece.token.outSideToken == Token.OutSideToken.nSpeedToken) spinPower -= 60;

            scoreAnimators.Add(pieceTrns.GetChild(0).GetComponent<Animator>());
            scoreTexts.Add(pieceTrns.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>());
            // 아이콘, 라벨, 금액 텍스트를 설정
            piece.token.TokenSetting(
                pieceTrns.GetChild(0).GetComponent<Image>(),
                pieceTrns.GetChild(1).GetComponent<Image>(),
                pieceTrns.GetChild(0).GetComponentInChildren<TextMeshProUGUI>(),
                pieceTrns.GetChild(1).GetComponentInChildren<TextMeshProUGUI>());

            /*pieceTrns.GetChild(0).GetComponent<Image>().sprite = piece.outside_Icon;
            pieceTrns.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = piece.outside_Label;

            pieceTrns.GetChild(1).GetComponent<Image>().sprite = piece.inside_Icon;
            pieceTrns.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = piece.inside_Label;*/

            // 구간 사이의 선을 그리기
            Transform lineTrns = Instantiate(linePrefab, linesParent.position, Quaternion.identity, linesParent).transform;
            lineTrns.RotateAround(wheelPiecesParent.position, Vector3.back, (pieceAngle * index) + halfPieceAngle);

            // 조각의 회전을 설정
            pieceTrns.RotateAround(wheelPiecesParent.position, Vector3.back, pieceAngle * index);
        }

        private GameObject InstantiatePiece()
        {
            // 룰렛 조각 프리팹을 인스턴스화
            return Instantiate(wheelPiecePrefab, wheelPiecesParent.position, Quaternion.identity, wheelPiecesParent);
        }

        public void Spin()
        {
            if (!_isSpinning)
            {
                _isSpinning = true;
                if (onSpinStartEvent != null)
                    onSpinStartEvent.Invoke();

                int index = GetRandomPieceIndex();
                WheelPiece piece = wheelPieces[index];

                if (piece.Chance == 0 && nonZeroChancesIndices.Count != 0)
                {
                    index = nonZeroChancesIndices[Random.Range(0, nonZeroChancesIndices.Count)];
                    piece = wheelPieces[index];
                }

                float angle = -(pieceAngle * index);
                float rightOffset = (angle - halfPieceAngleWithPaddings) % 360;
                float leftOffset = (angle + halfPieceAngleWithPaddings) % 360;
                float randomAngle = Random.Range(leftOffset, rightOffset);
                Vector3 targetRotation = Vector3.back * (randomAngle + spinPower * spinDuration);

                // prevAngle과 각도를 초기화
                float prevAngle = wheelCircle.eulerAngles.z;
                float currentAngle = prevAngle;
                bool isIndicatorOnTheLine = false;

                // 돌입할 피스 인덱스 추적 (반시계 방향)
                int nextPieceIndex = Mathf.CeilToInt((currentAngle) / pieceAngle) % wheelPieces.Length;

                wheelCircle
                .DORotate(targetRotation, spinDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutQuart)
                .OnUpdate(() => {
                    currentAngle = wheelCircle.eulerAngles.z;

                    // 각도 차이를 계산하고 다음 피스 돌입 시점 처리
                    float angleDiff = Mathf.Abs(prevAngle - currentAngle);
                    if (angleDiff >= halfPieceAngle)
                    {
                        if (isIndicatorOnTheLine)
                        {
                            audioSource.PlayOneShot(audioSource.clip);

                            // 다음 돌입할 피스의 인덱스 계산 (반시계 방향)
                            nextPieceIndex = Mathf.CeilToInt((currentAngle - halfPieceAngle) / pieceAngle) % wheelPieces.Length;
                            if (nextPieceIndex < 0)
                                nextPieceIndex += wheelPieces.Length;

                            // 돌입하는 피스의 정보 출력 또는 사용
                            WheelPiece incomingPiece = wheelPieces[nextPieceIndex];
                            incomingPiece.token.OutSideTokenSocre(nextPieceIndex);
                        }

                        prevAngle = currentAngle;
                        isIndicatorOnTheLine = !isIndicatorOnTheLine; // 틱 소리 상태를 토글
                    }
                })
                .OnComplete(() => {
                    _isSpinning = false;

                    // 스핀 완료 후 변수 초기화
                    prevAngle = 0;
                    currentAngle = 0;
                    isIndicatorOnTheLine = false;

                    if (onSpinEndEvent != null)
                        onSpinEndEvent.Invoke(piece);

                    onSpinStartEvent = null;
                    onSpinEndEvent = null;
                    wheelCircle.eulerAngles = Vector3.zero;
                });
            }
        }





        public void OnSpinStart(UnityAction action)
        {
            // 룰렛 시작 이벤트 설정
            onSpinStartEvent = action;
        }

        public void OnSpinEnd(UnityAction<WheelPiece> action)
        {
            // 룰렛 종료 이벤트 설정
            onSpinEndEvent = action;
        }

        private int GetRandomPieceIndex()
        {
            // 가중치를 기반으로 랜덤하게 조각의 인덱스를 반환
            double r = rand.NextDouble() * accumulatedWeight;

            for (int i = 0; i < wheelPieces.Length; i++)
                if (wheelPieces[i]._weight >= r)
                    return i;

            return 0;
        }

        private void CalculateWeightsAndIndices()
        {
            // 각 조각의 가중치와 인덱스를 계산
            for (int i = 0; i < wheelPieces.Length; i++)
            {
                WheelPiece piece = wheelPieces[i];

                // 가중치를 합산
                accumulatedWeight += piece.Chance;
                piece._weight = accumulatedWeight;

                // 인덱스를 설정
                piece.Index = i;

                // 0이 아닌 확률을 가진 인덱스를 리스트에 추가
                if (piece.Chance > 0)
                    nonZeroChancesIndices.Add(i);
            }
        }

        private void OnValidate()
        {
            // 유니티 에디터에서 값을 변경했을 때 호출되는 함수
            if (PickerWheelTransform != null)
                PickerWheelTransform.localScale = new Vector3(wheelSize, wheelSize, 1f);

            // 조각 개수가 범위를 벗어나면 오류 메시지 출력
            if (wheelPieces.Length > piecesMax || wheelPieces.Length < piecesMin)
                Debug.LogError("[ PickerWheelwheel ]  pieces length must be between " + piecesMin + " and " + piecesMax);
        }

        private void DestroyAllChildren(Transform parent)
        {
            // 부모 오브젝트의 모든 자식들을 순회
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);  // 각 자식 오브젝트를 파괴
            }
        }
    }
}
