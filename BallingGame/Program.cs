using System;

namespace BallingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.KnockedDownPins(5);        // 1라운드 첫번째 투구 5점
            game.KnockedDownPins(5);        // 1라운드 두번째 투구 스페어
            game.KnockedDownPins(10);       // 2라운드 스트라이크
            game.KnockedDownPins(10);       // 3라운드 스트라이크
            game.KnockedDownPins(9);        // 4라운드 첫번째 투구 9점
            game.KnockedDownPins(1);        // 4라운드 두번째 투구 스페어
            game.KnockedDownPins(2);        // 5라운드 첫번째 투구 2점
            game.KnockedDownPins(7);        // 5라운드 두번째 투구 7점, 오픈
            game.KnockedDownPins(8);        // 6라운드 첫번째 투구 8점
            game.KnockedDownPins(2);        // 6라운드 두번째 투구 2점
            game.KnockedDownPins(10);       // 7라운드 스트라이크
            game.KnockedDownPins(6);        // 8라운드 첫번째 투구 6점
            game.KnockedDownPins(0);        // 8라운드 두번째 투구 0점
            game.KnockedDownPins(8);        // 9라운드 첫번째 투구 8점
            game.KnockedDownPins(2);        // 9라운드 두번째 투구 스페어
            game.KnockedDownPins(10);       // 10라운드 스트라이크
            game.KnockedDownPins(8);        // 10라운드 보너스 8점
            game.KnockedDownPins(2);        // 10라운드 보너스 스페어
        }
    }
}
