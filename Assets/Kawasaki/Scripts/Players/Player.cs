using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using SetsunaTsuyuri;

namespace Kawasaki
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    public class Player : MonoBehaviour, IStunned, IItemObtainer
    {
        /// <summary>
        /// 設定
        /// </summary>
        [SerializeField]
        PlayersSettings _settings = null;

        /// <summary>
        /// 足元のトランスフォーム
        /// </summary>
        [SerializeField]
        Transform _foot = null;

        /// <summary>
        /// フォトンビュー
        /// </summary>
        public PhotonView PhotonView { get; private set; } = null;

        /// <summary>
        /// 他のプレイヤーより低い位置にいる
        /// </summary>
        public bool IsInTheLowestPosition { get; set; } = false;

        /// <summary>
        /// 順位
        /// </summary>
        public int Rank { get; set; } = 0;

        /// <summary>
        /// リジッドボディ2D
        /// </summary>
        Rigidbody2D _rigidbody2D = null;

        /// <summary>
        /// アニメーター
        /// </summary>
        Animator _animator = null;

        /// <summary>
        /// シネマシーンインパルスソース
        /// </summary>
        CinemachineImpulseSource _cinemachineImpulseSource = null;

        /// <summary>
        /// プレイヤーの移動制御
        /// </summary>
        Karaki.PlayerMovement _movement = null;

        /// <summary>
        /// バレットランチャー
        /// </summary>
        Karaki.BulletLauncher _bulletLauncher = null;

        /// <summary>
        /// 接地フラグ設定に使うボックスキャストの結果配列
        /// </summary>
        RaycastHit2D[] _boxCastingResultsForGroundedFlag = { };

        /// <summary>
        /// 初期の回転(Y軸)
        /// </summary>
        float _defaultRotationY = 0.0f;

        /// <summary>
        /// 仮想軸(水平)の入力
        /// </summary>
        float _horizontalAxisInput = 0.0f;

        /// <summary>
        /// 攻撃中である
        /// </summary>
        bool _isAttacking = false;

        /// <summary>
        /// ジャンプの入力
        /// </summary>
        bool _jumpInput = false;

        /// <summary>
        /// 攻撃の入力
        /// </summary>
        bool _attackInput = false;

        private void Awake()
        {
            PhotonView = GetComponent<PhotonView>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
            _movement = GetComponent<Karaki.PlayerMovement>();
            _bulletLauncher = GetComponent<Karaki.BulletLauncher>();

            _boxCastingResultsForGroundedFlag = new RaycastHit2D[_settings.CastingResultsLength];
            _defaultRotationY = transform.rotation.y;
        }

        private void Start()
        {
            // 管理者に自身を登録する
            PlayersManager.Current.Register(this);

            // マップとキルゾーンはマスタークライアント側で生成する
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            // 自分がログインした時はマップを生成する
            if (PhotonView.IsMine)
            {
                MapsManager.Current.CreateMaps();
                //MapsManager.Current.CreateKillZone();

            }
            else  // 2人目が入ってきた時にキルゾーンを生成する
            {
                MapsManager.Current.CreateKillZone();
            }
        }

        private void Update()
        {
            if (!PhotonView.IsMine)
            {
                return;
            }

            // 入力の更新処理
            UpdateInput();

            // 移動の更新処理
            UpdateMove();

            // 射撃の更新処理
            UpdateAttack();

            // アニメーターパラメーターを更新する
            UpdateAnimatorParameters();
        }

        /// <summary>
        /// 入力の更新処理
        /// </summary>
        private void UpdateInput()
        {
            // 仮想軸(水平)
            _horizontalAxisInput = Input.GetAxisRaw("Horizontal");

            // ジャンプボタン
            _jumpInput = Input.GetButtonDown("Jump");

            // 攻撃ボタン
            _attackInput = Input.GetButtonDown("Fire1");
        }

        /// <summary>
        /// 移動の更新処理
        /// </summary>
        private void UpdateMove()
        {
            // 地上フラグ更新
            if (_rigidbody2D.velocity.y <= 0.0f)
            {
                _movement.UpdateGroundedFlag(_rigidbody2D.velocity.y, _foot.position, _boxCastingResultsForGroundedFlag);
            }

            // Y軸回転
            _movement.SetRotationY(_defaultRotationY, _horizontalAxisInput);

            // 移動   
            _movement.Move(_horizontalAxisInput, _jumpInput);
        }

        /// <summary>
        /// 攻撃の更新処理
        /// </summary>
        private void UpdateAttack()
        {
            if (_attackInput && CanAttack())
            {
                StartAttack();
            }
        }

        /// <summary>
        /// 攻撃を開始する
        /// </summary>
        private void StartAttack()
        {
            _isAttacking = true;
            _animator.SetTrigger("Attack");
        }

        /// <summary>
        /// 射撃時のアニメーションイベント
        /// </summary>
        public void OnFireAnimation()
        {
            _bulletLauncher.Fire();
        }

        /// <summary>
        /// 攻撃終了時のアニメーションイベント
        /// </summary>
        public void OnAttackAnimationEnd()
        {
            _isAttacking = false;
        }

        /// <summary>
        /// アニメーターパラメーターを更新する
        /// </summary>
        private void UpdateAnimatorParameters()
        {
            // 軸(水平)の絶対値
            float horizontalAxisAbsolute = Mathf.Abs(_horizontalAxisInput);
            _animator.SetFloat("HorizontalAxisAbsolute", horizontalAxisAbsolute);

            // Y軸速度
            float velocityY = _rigidbody2D.velocity.y;
            _animator.SetFloat("VelocityY", velocityY);

            // 地上フラグ
            bool isGrounded = _movement.IsGrounded;
            _animator.SetBool("IsGrounded", isGrounded);

            // 気絶時間
            float stunnedTime = _movement.StunTimeCount;
            _animator.SetFloat("StunnedTime", stunnedTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Hit(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Hit(collision.gameObject);
        }

        /// <summary>
        /// 接触する
        /// </summary>
        /// <param name="other">接触したゲームオブジェクト</param>
        private void Hit(GameObject other)
        {
            IPlayerHit hit = other.GetComponentInParent<IPlayerHit>();
            if (hit is not null)
            {
                hit.OnHit(this);
            }
        }

        /// <summary>
        /// 加速する
        /// </summary>
        /// <param name="scale">加速倍率</param>
        /// <param name="duration">持続時間</param>
        public void Accelerate(float scale, float duration)
        {
            if (!PhotonView.IsMine)
            {
                return;
            }

            _movement.Accelerate(scale, duration);
        }

        public void BeStunned(float duration)
        {
            if (!PhotonView.IsMine)
            {
                return;
            }

            // スタン効果音を再生する
            AudioManager.PlaySE("Stunned");

            // 衝撃を生成する
            _cinemachineImpulseSource.GenerateImpulse();

            // 攻撃中フラグOFF
            _isAttacking = false;

            // 速度を0にする
            _rigidbody2D.velocity = Vector2.zero;
            
            // PlayerMovementの気絶処理を行う
            _movement.BeStunned(duration);
        }

        public void Obtain(Item item)
        {
            if (PhotonView.IsMine)
            {
                // 最も低い位置にいる場合
                if (IsInTheLowestPosition)
                {
                    // 有利な効果を適用する
                    ApplyGoodEffect(item);
                }
                else
                {
                    // 不利な効果を適用する
                    ApplyBadEffect(item);
                }
            }

            // アイテムを破壊する
            Destroy(item.gameObject);
        }

        /// <summary>
        /// 良い効果を適用する
        /// </summary>
        /// <param name="item">入手したアイテム</param>
        private void ApplyGoodEffect(Item item)
        {
            // 加速する
            float scale = item.Settings.AccelerationScale;
            float duration = item.Settings.AccelerationDuration;
            Accelerate(scale, duration);
        }

        /// <summary>
        /// 悪い効果を適用する
        /// </summary>
        /// <param name="item">入手したアイテム</param>
        private void ApplyBadEffect(Item item)
        {
            // 気絶する
            float duration = item.Settings.StunnedDutation;
            BeStunned(duration);
        }


        /// <summary>
        /// 攻撃可能である
        /// </summary>
        /// <returns></returns>
        private bool CanAttack()
        {
            bool result = !_isAttacking && !IsStunned();
            return result;
        }

        /// <summary>
        /// 気絶している
        /// </summary>
        /// <returns></returns>
        private bool IsStunned()
        {
            return _movement.StunTimeCount > 0.0f;
        }
    }
}
