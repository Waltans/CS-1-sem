﻿// ReSharper disable CommentTypo
namespace GameOfLife;

/* Реализуйте игру в жизнь на прямоугольном конечном поле.
 
 На каждом ходе клетка меняет свое состояние по таким правилам:
 1. Если у нее менее 2 живых соседей или более трех живых — она становится мертвой (false).
 2. Если ровно 3 живых соседа, то клетка становится живой (true)
 3. Если ровно 2 живых соседа, то клетка сохраняет своё состояние.

 У каждой неграничной клетки есть 8 соседей (в том числе по диагонали)

Работу над игрой постройте итеративно в стиле TDD:
    1. Сначала напишите какой-нибудь простейший тест в соседнем файле GameTest.cs. Тест должен быть красным.
    То есть должен проверять ещё нереализованное требование.
    2. Только потом напшишите простейшую реализацию, которая делает тест зеленым. 
    Не старайтесь реализовать всю логику, просто сделайте тест зеленым как можно быстрее.
    3. Повторяйте процесс, пока ещё можете придумать новые красные тесты.

 На каждый шаг (тест и реализация) у вас должно уходить не более 5 минут.
 Если вы не успели поднять тест за 5 минут — удалите этот тест и придумайте тест попроще.
 Засекайте время таймером на телефоне.

 После каждого шага (тест или реализация) меняйте активного человека за клавиатурой.

 Начните с простейших тестов. 

 Проект настроен так, что при каждой сборке запускаются все тесты и отчет выводится на консоль
*/
public class Game
{
    public static bool[,] NextStep(bool[,] field)
    {

        // field[5, 5] = false;
        field[0, 0] = false;
        
        Random idexFirst = new Random();
        Random indexSecond = new Random();
        int valueFirst = idexFirst.Next(3);
        int valueSecond = indexSecond.Next(3);
        
        if (field[valueFirst, valueSecond])
        {
            
        }
        if (field[0, 1] == true && field[1,0] == true && field[1, 1] == true)
        {
            field[0, 0] = true;
        }
        return field;
    }
}