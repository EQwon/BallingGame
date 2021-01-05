using System;
using System.Collections.Generic;
using System.Text;

namespace BallingGame
{
    public class Game
    {
        private Round[] rounds;
        private int nowRound;

        public Game()
        {
            rounds = new Round[10];
            for (int i = 0; i < 9; i++)
                rounds[i] = new Round();
            rounds[9] = new LastRound();
            nowRound = 0;
        }

        public void KnockedDownPins(int pinCnt)
        {
            // 마지막 라운드의 점수가 확정이 났다면 게임이 끝난 것
            if (rounds[9].isScoreConfirmed)
            {
                Console.WriteLine("Game is already End.");
                return;
            }

            // 유효하지 않은 Input
            if (pinCnt < 0 || pinCnt > 10)
            {
                Console.WriteLine("Invalid Input. Input must be between 0 to 10.");
                return;
            }

            Round targetRound = rounds[nowRound];

            // 해당 라운드에 유효하지 않은 Input
            if (targetRound.IsValidRoundInput(pinCnt) == false)
            {
                Console.WriteLine("Invalid Round Input. Sum of knocked down pins should equal or under 10.");
                return;
            }

            targetRound.RecordPin(pinCnt);                  // 해당 라운드에 pin 결과 전달

            for (int i = 0; i < nowRound; i++)              // Spare나 Strike로 인한 보너스 점수 전달
                rounds[i].RecordBonusPin(pinCnt);

            if (targetRound.isRoundEnd) nowRound += 1;      // 라운드가 끝났는지 확인

            PrintScoreBoard();                              // 점수판 출력
        }

        private void PrintScoreBoard()
        {
            #region 첫번째 줄 출력
            // 1에서 9라운드까지
            for (int i = 0; i < 9; i++)
                Console.Write("{0}:[{1,-3}] ", i + 1, RecordToString(rounds[i]));

            // 10 라운드만 따로
            Console.Write("{0}:[{1,-5}] ", 10, RecordToString(rounds[9]));
            #endregion

            // 한 줄 띄고
            Console.Write("\n");

            #region 두번째 줄 출력
            int? totalScore = 0;

            // 1에서 9라운드까지
            for (int i = 0; i < 9; i++)
            {
                int? roundScore = rounds[i].score;
                if (roundScore.HasValue) totalScore += roundScore.Value;
                else totalScore = null;

                Console.Write("  [{0, 3}] ", TotalScoreToString(totalScore));
            }

            // 10 라운드만 따로
            if (rounds[9].score.HasValue)
                totalScore += rounds[9].score.Value;
            else totalScore = null;

            Console.Write("   [{0, 5}] ", TotalScoreToString(totalScore));
            #endregion

            Console.Write("\n");
        }

        // 한 라운드의 기록을 문자로 변환합니다.
        private string RecordToString(Round round)
        {
            int?[] records = round.getRecords;
            State state = round.getState;

            switch (state)
            {
                case State.None:
                    if (records[0].HasValue)
                        return records[0] + ",";
                    else return "";
                case State.Open:
                    if (records[1] == 0)
                        return records[0] + ",-";
                    else
                        return records[0] + "," + records[1];
                case State.Spare:
                    // 일반 라운드 스페어
                    if (records.Length == 2)
                        return records[0] + ",/";
                    // 마지막 라운드 스페어
                    else
                    {
                        if (records[2].HasValue)
                            return records[0] + ",/," + (records[2] == 10 ? "X" : records[2].ToString());
                        else
                            return records[0] + ",/,";
                    }
                case State.Strike:
                    // 일반 라운드 스트라이크
                    if (records.Length == 2)
                        return " X ";
                    // 마지막 라운드 스트라이크
                    else
                    {
                        string str = "X,";
                        if (records[1].HasValue) str += SingleRecordToString(records[1].Value) + ",";
                        if (records[2].HasValue)
                        {
                            if (records[1].Value == 10) str += SingleRecordToString(records[2].Value);
                            else if (records[1] + records[2] == 10) str += "/";
                            else if (records[2] == 0) str += "-";
                            else str += records[2];
                        }
                        return str;
                    }
            }

            Console.WriteLine("Fatal Error : Round state is " + state);
            return "";
        }

        // 하나의 기록을 문자로 변환합니다. 10점이면 X, 나머지는 그대로
        private string SingleRecordToString(int record)
        {
            return record == 10 ? "X" : record.ToString();
        }

        // 전체 스코어를 문자로 변환해줍니다.
        private string TotalScoreToString(int? total)
        {
            if (total == null)
                return "";
            else
                return total.ToString();
        }
    }
}
