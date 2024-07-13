#include <math.h>

#define PIN_TRIG 12 // датчик расстояния
#define PIN_ECHO 11 // датчик расстояния
#define btn1 2 // кнопка 1
#define btn2 3 // кнопка 2
#define pinX A2 // ось X джойстика
#define pinY A1 // ось Y джойстика
#define swPin 4 // кнопка джойстика

long duration, cm;
int bool_btn1 = 0;
int bool_btn2 = 0;
String status_btn1 = "no_click";
String status_btn_last1 = "no_click";
String status_btn2 = "no_click";
String status_btn_last2 = "no_click";

int pause_status = -1;

float angleBetweenVectors(float x1, float y1, float x2, float y2)
{
  float dotProduct = x1 * x2 + y1 * y2;
  float magnitude1 = sqrt(x1 * x1 + y1 * y1);
  float magnitude2 = sqrt(x2 * x2 + y2 * y2);
  float angle = acos(dotProduct / (magnitude1 * magnitude2)) * 180 / PI;

  return angle;
}

void setup()
{
  Serial.begin (115200);
  pinMode(PIN_TRIG, OUTPUT); // датчик
  pinMode(PIN_ECHO, INPUT); // датчик

  pinMode(btn1, INPUT); // кнопка 1
  pinMode(btn2, INPUT); // кнопка 2

  pinMode(pinX, INPUT); // джойстик
  pinMode(pinY, INPUT); // джойстик

  pinMode(swPin, INPUT); // кнопка джойстика
  digitalWrite(swPin, HIGH); // кнопка джойстика
}

bool ledState_last = true;

void loop()
{
  // Сначала генерируем короткий импульс длительностью 2-5 микросекунд.
  digitalWrite(PIN_TRIG, LOW);
  delayMicroseconds(5);
  digitalWrite(PIN_TRIG, HIGH);

  // Выставив высокий уровень сигнала, ждем около 10 микросекунд. В этот момент датчик будет посылать сигналы с частотой 40 КГц.
  delayMicroseconds(10);
  digitalWrite(PIN_TRIG, LOW);

  // Время задержки акустического сигнала на эхолокаторе.
  duration = pulseIn(PIN_ECHO, HIGH);

  // Теперь осталось преобразовать время в расстояние
  cm = (duration / 2) / 29.1;

  if (cm != 7)
  {
    Serial.println("start");
  }

  // Serial.print("Distance: ");
  // Serial.print(cm);
  // Serial.print(" cm.");
  // Serial.print("\t");

  bool_btn1 = digitalRead(btn1);
  bool_btn2 = digitalRead(btn2);

  if (bool_btn1 == 1 && bool_btn2 == 0)
  {
    status_btn1 = "first_btn";
    status_btn2 = "no_click";
  }
  else if (bool_btn1 == 0 && bool_btn2 == 1)
  {
    status_btn2 = "second_btn";
    status_btn1 = "no_click";
  }

  if (status_btn1 == "no_click" && status_btn_last1 == "first_btn")
  {
    pause_status = pause_status * (-1);

    if (pause_status == 1)
    {
      Serial.println("stop");
    }
    else
    {
      Serial.println("continue");
    }
    
  }
  else if (status_btn2 == "no_click" && status_btn_last2 == "second_btn")
  {
    Serial.println("exit");
  }

  status_btn_last1 = status_btn1;
  status_btn_last2 = status_btn2;
  status_btn1 = "no_click";
  status_btn2 = "no_click";

  float X = analogRead(pinX); // считываем значение оси Х
  float Y = analogRead(pinY); // считываем значение оси Y

  X = map(X, 0, 1023, -512, 512);
  Y = map(Y, 0, 1023, -512, 512);

  float x1 = 512.0;
  float y1 = 0.0;
  float x2 = X;
  float y2 = Y;
  float angle = 90;
  float double_angle;

  if (!(x2 < 30 && x2 > -30 && y2 < 30 && y2 > -30))
  {
    if (y2 <= 0)
    {
      angle = angleBetweenVectors(x1, y1, x2, y2);
      double_angle = angle;
    }
    else if (y2 > 0)
    {
      angle = angleBetweenVectors(x1, y1, x2, y2);

      if (x2 <= 0)
      {
        angle = 360 - angle;
      }
      else if (x2 > 0)
      {
        float dop_angle = map(angle, 91, 180, 1, 90);
        angle = 270 - dop_angle;
      }

      double_angle = angle;
    }
  }
  else
  {
    angle = double_angle;
  }


  angle = angle - 90; //чтобы угол норм работал в юнити
  int good_angle = ceil(angle);
  // Serial.println(good_angle);
  boolean ledState = digitalRead(swPin); // считываем состояние кнопки


//Обработка нажатий на джойстике (стрельба)
  if (ledState == 0 && ledState_last == 1)
  {
    Serial.println("click");
  }
  else
  {
    Serial.println(good_angle);
  }

  ledState_last = ledState;

  // Задержка между измерениями для корректной работы скеча
  delay(10);
}