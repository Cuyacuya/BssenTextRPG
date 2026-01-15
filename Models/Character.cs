using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Models;

//캐릭터 기본 추상 클래스
internal abstract class Character
{
    #region 프로퍼티
    public string Name { get; protected set; }
    public int CurHp { get; set; }
    public int MaxHp { get; set; }
    public int CurMp { get; set; }
    public int MaxMp { get; set; }

    public int AttackPower { get; set; }
    public int Defense { get; set; }
    public int Level { get; set; }
    public bool IsAlive => CurHp > 0;
    #endregion

    #region 생성자
    protected Character(string name, int maxHp, int maxMp, int attackPower, int defense, int level)
    {
        Name = name;
        CurHp = maxHp;
        CurMp = maxHp;
        AttackPower = attackPower;
        Defense = defense;
        Level = level;
    }
    #endregion

    #region 메서드

    #endregion

}
