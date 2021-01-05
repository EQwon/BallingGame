using System;
using System.Collections.Generic;
using System.Text;

namespace BallingGame
{
    public class LastRound : Round
    {
        public override bool isRoundEnd
        {
            get
            {
                if (state == State.None) return false;
                else if (state == State.Open) return true;
                else return getRecords[2].HasValue;
            }
        }
        public override int?[] getRecords
        {
            get
            {
                int?[] lastRecord = new int?[3];
                for (int i = 0; i < record.Length; i++)
                    lastRecord[i] = record[i];

                if (state == State.Spare)
                    lastRecord[2] = bonus[0];
                else if (state == State.Strike)
                {
                    lastRecord[1] = bonus[0];
                    lastRecord[2] = bonus[1];
                }
                return lastRecord;
            }
        }

        public override bool IsValidRoundInput(int pinCnt)
        {
            if (state == State.None)
                return base.IsValidRoundInput(pinCnt);
            else if (state == State.Strike)
                if (bonus[0].HasValue && bonus[0] != 10 && bonus[0].Value + pinCnt > 10)
                    return false;

            return true;
        }

        public override void RecordPin(int pinCnt)
        {
            if (state == State.None)
                base.RecordPin(pinCnt);
            else
                RecordBonusPin(pinCnt);
        }
    }
}
