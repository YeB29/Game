// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace MijnGame14
// {
//     class Program{
//         static void Mail(string [] args){
//                 SnakeM.Start();
//         }
//     }
//     public static class SnakeM{
//         static PointType[,] Wereld{get;set;}
//         static int MaxFormaat{get;set;}
//         static int WereldBreedte{get;set;}
//         static int WereldLengte{get;set;}
//         static Tijd bijwerken{get;set;}
//         static event Action <ConsoleKey> Input;
        
//         public static void Start(int WereldBreedte=30 , int WereldLengte=30, int FruitCount = 10, int DeltaTime = 100){
//             if(WereldBreedte<3 || WereldLengte<3)throw new Exception("Deze wereld is te klein om te spelen.");
//             if(DeltaTime <1 )throw new Exception("De duur moet langer zijn dan 0 seconden.");

//             int ConsoleBreedte = WereldBreedte *2;
//             int ConsoleLengte = WereldLengte + 1;
//             Console.Title = "Snake";
//             Console.SetWindowSize(ConsoleBreedte,ConsoleLengte);
//             Console.CursorVisible=false;
//             Console.Clear();

//             Wereld = new PointType[WereldBreedte,WereldLengte];
//             MaxFormaat = (WereldBreedte -4) * (WereldLengte -4)-FruitCount;
//             int Positie = WereldLengte -1;
//             for(int i=0; i<WereldBreedte;i++)
//             {
//                     Wereld[i,0]=PointType.Goed;
//                     Painter.Teken(i,0,ConsoleColor.White);

//                     Wereld[i,Positie]=PointType.Goed;
//                     Painter.Teken(i,0,ConsoleColor.White);
//             }

//             int rechtPos = WereldBreedte -1;
//             for(int y =1; y< Positie;y++){
//                 Wereld[0,y] = PointType.Goed;
//     	        Painter.Teken(0,y,ConsoleColor.White);

//                 Wereld[rechtPos,y] = PointType.Goed;
//     	        Painter.Teken(rechtPos,y,ConsoleColor.White);
//             }
//             bijwerken = new Tijd(DeltaTime);
//             Snake snake = new Snake (new Coordinaat(1,1),4,ConsoleColor.DarkGreen);
            
//             for(int i = 0; i<FruitCount; i++){
//                 GenereerFruit();
//             }
//             bijwerken.Aan1 = true;
//             while(snake.IsLive){
//                 Input.Invoke(Console.ReadKey(true).Key);
//             }
//             bijwerken.Aan1 = false;
//             Console.Clear();
//             Console.ResetColor();

//         }
//         static void GenereerFruit(){
//             Coordinaat positie;
//             do positie = new Coordinaat(Willekeur.Bereik(1,WereldBreedte-1),Willekeur.Bereik(1,WereldLengte-1));
//             while(Wereld[positie.x,positie.y] != PointType.Gratis);
//             Wereld[positie.x,positie.y] = PointType.Fruit;
//             Painter.Teken(positie, ConsoleColor.Red);
//         }
//         static class Willekeur{
//                 private static System.Willekeur willekeur = new System.Willekeur();

//                 public static int Bereik(int min,int max) =>
//                     willekeur.volgende(min,max);
//         }
//         class Snake{
//             private List<Coordinaat> lijst{get;set;}
//             private Coordinaat vorige;
//             private Coordinaat Richting1;
//             public  Coordinaat Richting{
//                 get =>Richting;
//                 set{
//                     if (vorige.x != -value.x || vorige.y != -value.y);
//                         Richting1=value;
//                 }
//             }
//             private ConsoleColor color {get;set;}
//             public bool IsLive{get;set;}
//             public Snake(Coordinaat coordinaat, int grote, ConsoleColor color){
//                 if(grote <0) throw new Exception("De slang is niet lang genoeg.");
//                 if(coordinaat.x <0|| coordinaat.x>= WereldBreedte || coordinaat.y<0|| coordinaat.y >= WereldLengte)
//                     throw new Exception ("De slang moet bestaan in de wereld.");

//                     IsLive = true;
//                     lijst = new List<Coordinaat>(grote);
//                     for(int i=0; i<grote;i++)
//                         lijst.Add(coordinaat);

//                     ConsoleColor Color = color;   
//                     Painter.Teken(coordinaat,Color);
//                     Richting = Coordinaat.Rechts;
//                     Input += this.VeranderRichting;

//                     bijwerken.Tick += this.Beweging();
//             }
//             private void Beweging(){
//                 vorige = Richting1;
//                 Coordinaat laatste = lijst[lijst.Count -1];
//                 if(laatste != lijst[lijst.Count -2]){
//                     Painter.Leeg(laatste);
//                     Wereld[laatste.x,laatste.y] = PointType.Gratis;
//                 }
//                 for(int i = lijst.Count -1; i>0; i++){
//                     lijst[i] = lijst[i-1];
//                 }
//                       lijst[0] += Richting;
//                       if(Wereld[lijst[0].x,lijst[0].y] == PointType.Goed){
//                           for( int i =1; i<lijst.Count;i++){
//                               Painter.Leeg(lijst[i]);
//                           }
//                           IsLive= false;
//                           bijwerken.Tick -= this.Beweging;
//                           return;
//                       }
//                       if(Wereld[lijst[0].x,lijst[0].y] == PointType.Fruit){
//                           lijst.Add(laatste);
//                           if(lijst.Count == MaxFormaat){
//                               IsLive =false;
//                               bijwerken.Tick -= this.Beweging;
//                               Painter.Teken(laatste,color);
//                               Wereld[laatste.x,laatste.y] = PointType.Goed;
                              
//                               Painter.Teken(lijst[0],color);
//                               Wereld[lijst[0].x,lijst[0].y] = PointType.Goed;
//                               return;
//                           }
//                           GenereerFruit();
//                       }
//                          Painter.Teken(lijst[0],color);
//                          Wereld[lijst[0].x,lijst[0].y] = PointType.Goed;

//             }     
//             private void VeranderRichting(ConsoleKey key){
//                 switch(key){
//                     case ConsoleKey.LeftArrow: Richting = Coordinaat.Links; break;
//                     case ConsoleKey.RightArrow: Richting = Coordinaat.Rechts; break;
//                     case ConsoleKey.UpArrow: Richting = Coordinaat.Boven; break;
//                     case ConsoleKey.DownArrow: Richting = Coordinaat.Onder; break;

//                 }
//             }
//         }
//         enum PointType
//         {
//             Gratis,
//             Goed,
//             Fruit,
            
//         }

//         static class Painter{
//             public static void Teken(Coordinaat  coordinaat, ConsoleColor color){
//                 Console.SetCursorPosition(coordinaat.x *2,coordinaat.y);
//                 Console.ForegroundColor =color;
//                 Console.Write("|");
//             }
//             public static void Teken(int x,int y, ConsoleColor color){
//                 Console.SetCursorPosition(x *2,y);
//                 Console.ForegroundColor =color;
//                 Console.Write("|");
//             }
//              public static void Leeg(Coordinaat coordinaat){
//                 Console.SetCursorPosition(coordinaat.x *2,coordinaat.y);
//                 Console.Write(" ");
//             } public static void Leeg(int x,int y){
//                 Console.SetCursorPosition(x *2,y);
//                 Console.Write(" ");
//             }
//         }
//         struct Coordinaat {
//             public static readonly Coordinaat Nul = new Coordinaat(0,0);
//             public static readonly Coordinaat Links = new Coordinaat(-1,0);
//             public static readonly Coordinaat Rechts = new Coordinaat(1,0);
//             public static readonly Coordinaat Boven = new Coordinaat(0,-1);
//             public static readonly Coordinaat Onder = new Coordinaat(0,1);

//             public int x {get;set;}
//             public int y{get;set;}

//             public Coordinaat(int x,int y){
//                 this.x = x;
//                 this.y =y;
//             }

//             public static bool operator ==(Coordinaat ob1, Coordinaat ob2)=>
//                 ob1.x == ob2.x && ob1.y == ob2.y;
//              public static bool operator !=(Coordinaat ob1, Coordinaat ob2)=>
//                 ob1.x != ob2.x || ob1.y != ob2.y;
//              public static Coordinaat operator +(Coordinaat ob1, Coordinaat ob2)=>
//                new Coordinaat( ob1.x + ob2.x , ob1.y+ ob2.y);
//         }
//         class Tijd{
//             public int Interval{get;set;}
//             public event Action Tick;
//             private bool Aan;
//             public bool Aan1{
//                 get=> Aan;
//                 set{
//                     if(value &&!Aan)
//                         Werk();
//                         Aan=value;
//                 }
//             }

//             public Tijd(int interval){
//                     Interval = interval;
//             }
//             private async void Werk(){
//                 Aan=true;
//                 while(Aan){
//                     await Task.Delay(Interval);
//                     Tick?.Invoke();
//                 }
//             }
//         }

//     }
// }