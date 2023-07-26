using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCharacter.PlayerStateMachine.ScriptableObjectsBase;
using PlayerCharacter.PlayerStateMachine.States;
using PlayerCharacter;

public class CharacterAbilitie : MonoBehaviour
{
    private Player player;

    [SerializeField] private PlayerMoveSO playerDashMove;
    [SerializeField] private PlayerAttackSO playerAttack;
    [SerializeField] private PlayerAttackSO playerSpecialAttack;
    [SerializeField] private PlayerBuffSO[] playerBuff;

    public PlayerMoveSO PlayerMoveInstance { get; private set; }
    public PlayerAttackSO PlayerAttackInstance { get; private set; }
    public PlayerAttackSO PlayerSpecialAttackInstance { get; private set; }
    public PlayerBuffSO PlayerBuffInstance { get; private set; }


    private void Awake()
    {
        player = GetComponent<Player>();
        PlayerMoveInstance = Instantiate(playerDashMove);
        PlayerAttackInstance = Instantiate(playerAttack);
        PlayerSpecialAttackInstance = Instantiate(playerSpecialAttack);
    }

    private void Start()
    {
        PlayerMoveInstance.Initialize(gameObject, player);
        PlayerAttackInstance.Initialize(gameObject, player);
        PlayerSpecialAttackInstance.Initialize(gameObject, player);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            player.ChangePlayerAbilitie(PlayerAttackInstance);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            player.ChangePlayerAbilitie(PlayerSpecialAttackInstance);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            player.ChangePlayerAbilitie(PlayerMoveInstance);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangePlayerBuff(0);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ChangePlayerBuff(1);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangePlayerBuff(2);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangePlayerBuff(3);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePlayerBuff(4);
        }
    }

    public void ChangePlayerBuff(int _buffNumber)
    {
        PlayerBuffInstance = Instantiate(playerBuff[_buffNumber]);
        PlayerBuffInstance.Initialize(gameObject, player);
        player.ChangePlayerAbilitie(PlayerBuffInstance);
        player._stateMachine.ChangeState(new PlayerBuffState(player, player._stateMachine));
    }
}
