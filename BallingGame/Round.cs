using System;
using System.Collections.Generic;
using System.Text;

namespace BallingGame
{
    public enum State { None, Open, Spare, Strike }

    public class Round
    {
        protected State state;
        protected int?[] record;
        protected int?[] bonus;

        #region Constructor
        public Round()
        {
            state = State.None;
            record = new int?[2];
            bonus = new int?[0];
        }
        #endregion

        #region Property
        private bool needBonus
        {
            get
            {
                if (state == State.None || state == State.Open)
                    return false;
                else
                {
                    foreach (int? cap in bonus)
                        if (cap.HasValue == false) return true;
                    return false;
                }
            }
        }
        public virtual bool isRoundEnd => state != State.None;
        public bool isScoreConfirmed
        {
            get
            {
                if (state == State.None) return false;
                if (needBonus) return false;
                return true;
            }
        }
        public int? score
        {
            get
            {
                if (isScoreConfirmed == false) return null;

                int value = record[0].Value + record[1].Value;
                foreach (int? b in bonus)
                    value += b.Value;

                return value;
            }
        }
        /// <summary>
        /// 해당 라운드의 점수 기록을 반환.
        /// </summary>
        public virtual int?[] getRecords => record;
        public State getState => state;
        #endregion

        #region Method
        /// <summary>
        /// 들어온 인풋(pinCnt)이 그 라운드에 적합한지 확인
        /// </summary>
        /// <param name="pinCnt">쓰러뜨린 핀의 개수</param>
        /// <returns>true라면 적합한 입력입니다.</returns>
        public virtual bool IsValidRoundInput(int pinCnt)
        {
            if (record[0].HasValue && record[0].Value + pinCnt > 10)
                return false;
            return true;
        }

        /// <summary>
        /// 쓰러뜨린 핀의 개수를 기록
        /// </summary>
        /// <param name="pinCnt">쓰러뜨린 핀의 개수</param>
        public virtual void RecordPin(int pinCnt)
        {
            // 라운드 첫번째 공
            if (!record[0].HasValue)
            {
                record[0] = pinCnt;

                // 스트라이크라면 보너스 칸 2개를 만들고 상태 확정
                if (pinCnt == 10)
                {
                    record[1] = 0;
                    state = State.Strike;
                    bonus = new int?[2];
                }
            }
            // 라운드 두번째 공
            else if (!record[1].HasValue)
            {
                record[1] = pinCnt;

                // 스페어라면 보너스 칸 1개를 만들고 상태 확정
                if (record[0] + record[1] == 10)
                {
                    state = State.Spare;
                    bonus = new int?[1];
                }
                // 오픈이라면 보너스 칸 0개를 만들고 상태 확정
                else if (record[0] + record[1] < 10)
                {
                    state = State.Open;
                    bonus = new int?[0];
                }
            }
            else
                Console.WriteLine("Error : 첫번째도 두번째도 값이 있는데 어떻게 된거지?");
        }

        /// <summary>
        /// Spare나 Strike로 인해 얻어야할 보너스 점수를 기록
        /// </summary>
        /// <param name="pinCnt">쓰러뜨린 핀의 개수</param>
        public void RecordBonusPin(int pinCnt)
        {
            if (needBonus)
            {
                for (int i = 0; i < bonus.Length; i++)
                {
                    if (bonus[i].HasValue == false)
                    {
                        bonus[i] = pinCnt;
                        return;
                    }
                }
            }
        }
        #endregion
    }
}
