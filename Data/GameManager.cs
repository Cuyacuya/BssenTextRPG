using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Utils;

namespace TextRPG.Data
{
    internal class GameManager
    {
        //싱글톤 패턴 (Singleton Pattern) 구현
        #region 싱글톤 패턴
        //싱글톤 인스턴스 (내부 접근용 변수 : 필드)
        private static GameManager instance;

        //외부에서 인스턴스에 접근할 수 있는 정적 속성(프로퍼티)
        public static GameManager Instance
        {
            get
            {
                //인스턴스가  없으면 새로 생성
                if(instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        private GameManager()
        {
            //클래스가 생성될 때 초기화 작업 수행 (생성자)
            
        }
        #endregion

        #region 게임 시작/종료
        //게임 시작 메서드
        public void StartGame()
        {
            ConsoleUI.ShowTitle();
            Console.WriteLine("빡센 게임에 오신 것을 환영합니다!\n");
        }

        public void StopGame()
        {
            Console.WriteLine("빡센 게임에 오신 것을 환영합니다!\n");
        }
        #endregion
    }
}
