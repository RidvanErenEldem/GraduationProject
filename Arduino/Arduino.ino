#include <MPU6050_tockn.h>
#include <MPU6050_tockn69.h>


#include <Wire.h>

MPU6050 mpu6050(Wire);
MPU605069 mpu60502(Wire);

int X, Y, Z, X1, Y1, Z1;
int OX, OY, OZ, OX1, OY1, OZ1; // Angle Variables for calucating gyroscope zero error
bool isClicking0 = false;
bool isClicking1 = false;

void setup() {
  Serial.begin(115200); // Initialise Serial communication
  Wire.begin(); // Initialise I2C communication
  mpu6050.begin(); // Initialise Gyro communication
  mpu6050.setGyroOffsets(25.29, 5.90, -0.72);
  mpu6050.update(); // Command to calculate the sensor data before using the get command
  
  mpu60502.begin();
  mpu60502.setGyroOffsets(-2.77, -2.47, -0.16);

  mpu60502.update();
  
  pinMode(4, INPUT_PULLUP);
  pinMode(3, INPUT_PULLUP);

  if (OX < 0) { // Inverting the sign of all the three offset values for zero error correction
    OX = OX * (-1);
  } else {
    OX = (OX - OX) - OX;
  }

  if (OY < 0) {
    OY = (OY * (-1));
  } else {
    OY = ((OY - OY) - OY);
  }
  
  if (OX1 < 0) { // Inverting the sign of all the three offset values for zero error correction
    OX1 = OX1 * (-1);
  } else {
    OX1 = (OX1 - OX1) - OX1;
  }

  if (OY1 < 0) {
    OY1 = (OY1 * (-1));
  } else {
    OY1 = ((OY1 - OY1) - OY1);
  }
}
void loop() {
  // put your main code here, to run repeatedly:
  mpu6050.update();
  X = OX + mpu6050.getAngleX(); // Getting current angle for X Y Z and correcting the zero error
  Y = OY + mpu6050.getAngleY();
  Z = mpu6050.getAngleZ();

  mpu60502.update();
  X1 = OX1 + mpu60502.getAngleX(); // Getting current angle for X Y Z and correcting the zero error
  Y1 = OY1 + mpu60502.getAngleY();
  Z1 = mpu60502.getAngleZ();
  
  Serial.print("0 " + String(X) + ' ' + String(Y) + ' ' + String(Z)); // Sends corrected gyro data to the Serial Port with the identifier "DATAL"
  if (digitalRead(4) == 1) { // Checks if Right Enabled Button is pushed
    isClicking0 = false;
    Serial.print(" 1");
  } else if(digitalRead(4) == 0 && isClicking0 == false) {
    isClicking0 = true;
    Serial.print(" 0");
  } else if (isClicking0 == true){
    Serial.print(" 1");
  }
  delay(5);
  Serial.println("");
  
  Serial.print("1 " + String(X1) + ' ' + String(Y1) + ' ' + String(Z1)); // Sends corrected gyro data to the Serial Port with the identifier "DATAL"
  if (digitalRead(3) == 1) { // Checks if Right Enabled Button is pushed
    isClicking1 = false;
    Serial.print(" 1");
  } else if(digitalRead(3) == 0 && isClicking1 == false) {
    isClicking1 = true;
    Serial.print(" 0");
  } else if (isClicking1 == true){
    Serial.print(" 1");
  }
  delay(5);
  Serial.println("");
}
