using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BssenTextRPG.Models;

public class Player : Character
{
    #region 프로퍼티
    //직업
    public JobType Job {  get; private set; }
    //골드
    public int Gold { get; }
    //TODO : 장착 무기
    //TODO : 장착 방어구
    #endregion

    #region 생성자
    public Player(string name, JobType job) : base(
    name: name,
    maxHp: 100,
    maxMp: 50,
    attackPower: 20,
    defense: 10,
    level: 1)
    {
        Job = job;
        Gold = 1000;
    }
    #endregion
}
